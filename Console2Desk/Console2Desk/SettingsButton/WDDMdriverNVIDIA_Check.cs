using Console2Desk.ToggleSwitchDev;
using Microsoft.Win32;

namespace Console2Desk.SettingsButton
{
    internal class WDDMdriverNVIDIA_Check
    {
        public static void CheckWDDMdriverNVIDIAStatus(ControlToggles controlWDDMnvidia, MessagesBoxImplementation messagesBoxImplementation)
        {
            Thread checkThread = new Thread(() =>
            {
                try
                {
                    bool iscontrolWDDMnvidiaEnabled = CheckUACRegistryKey();

                    // Update the toggle switch state on the main thread
                    controlWDDMnvidia.Invoke((MethodInvoker)delegate
                    {
                        controlWDDMnvidia.IsOn = iscontrolWDDMnvidiaEnabled;
                    });
                }
                catch (Exception ex)
                {
                    // Show error message on the main thread
                    controlWDDMnvidia.Invoke((MethodInvoker)delegate
                    {
                        messagesBoxImplementation.ShowMessage($"Error checking WDDM driver status: {ex.Message}", "Error", MessageBoxButtons.OK);
                    });
                }
            });

            // Start the thread
            checkThread.Start();
        }

        private static bool CheckUACRegistryKey()
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows NT\Terminal Services"))
                {
                    if (key != null)
                    {
                        object enablecontrolWDDMnvidia = key.GetValue("fEnableWddmDriver");
                        if (enablecontrolWDDMnvidia != null && enablecontrolWDDMnvidia is int)
                        {
                            return (int)enablecontrolWDDMnvidia == 1; // fEnableWddmDriver enabled if value is 1
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the error
                DependencyContainer.MessagesBoxImplementation.ShowMessage($"Error reading UAC registry key: {ex.Message}", "Error", MessageBoxButtons.OK);
            }

            return false; // Default to false if there's an issue
        }
    }
}
