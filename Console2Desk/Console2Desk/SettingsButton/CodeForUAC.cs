using Console2Desk.ToggleSwitchDev;
using Microsoft.Win32;

namespace Console2Desk.SettingsButton
{
    internal class CodeForUAC
    {
        public static void ToggleUAC(ControlToggles uacToggleSwitch, MessagesBoxImplementation messagesBoxImplementation)
        {
            Thread toggleThread = new Thread(() =>
            {
                try
                {
                    bool isOn = uacToggleSwitch.IsOn;

                    if (isOn)
                    {
                        // Enable UAC by setting the registry key to 1
                        SetUACRegistryKey(1);
                    }
                    else
                    {
                        // Disable UAC by setting the registry key to 0
                        SetUACRegistryKey(0);
                    }

                    // Update the toggle switch state on the main thread
                    uacToggleSwitch.Invoke((MethodInvoker)delegate
                    {
                        uacToggleSwitch.IsOn = !isOn;
                    });

                }
                catch (Exception ex)
                {
                    // Show error message on the main thread
                    uacToggleSwitch.Invoke((MethodInvoker)delegate
                    {
                        // messagesBoxImplementation.ShowMessage($"An error occurred while toggling UAC: {ex.Message}", "Error", MessageBoxButtons.OK);
                    });
                }
            });

            // Start the thread
            toggleThread.Start();
        }

        private static void SetUACRegistryKey(int value)
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System", writable: true))
                {
                    if (key != null)
                    {
                        key.SetValue("EnableLUA", value, RegistryValueKind.DWord);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the error
                DependencyContainer.MessagesBoxImplementation.ShowMessage($"Error setting UAC registry key: {ex.Message}", "Error", MessageBoxButtons.OK);
            }
        }
    }
}
