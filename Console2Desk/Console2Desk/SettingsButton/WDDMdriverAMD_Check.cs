using Console2Desk.ToggleSwitchDev;
using Microsoft.Win32;

namespace Console2Desk.SettingsButton
{
    internal class WDDMdriverAMD_Check
    {
        public static void CheckWDDMdriverAMDStatus(ControlToggles controlWDDMamd, MessagesBoxImplementation messagesBoxImplementation)
        {
            Thread checkThread = new Thread(() =>
            {
                try
                {
                    bool iscontrolWDDMamdEnabled = CheckUACRegistryKey();

                    // Update the toggle switch state on the main thread
                    controlWDDMamd.Invoke((MethodInvoker)delegate
                    {
                        controlWDDMamd.IsOn = iscontrolWDDMamdEnabled;
                    });
                }
                catch (Exception ex)
                {
                    // Show error message on the main thread
                    controlWDDMamd.Invoke((MethodInvoker)delegate
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
                        object enablecontrolWDDMamd = key.GetValue("fEnableWddmDriver");
                        if (enablecontrolWDDMamd != null && enablecontrolWDDMamd is int)
                        {
                            return (int)enablecontrolWDDMamd == 1; // fEnableWddmDriver enabled if value is 1
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
