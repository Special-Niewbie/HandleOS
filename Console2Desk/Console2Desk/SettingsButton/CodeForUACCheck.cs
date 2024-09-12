using Console2Desk.ToggleSwitchDev;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console2Desk.SettingsButton
{
    internal class CodeForUACCheck
    {
        public static void CheckUACStatus(ControlToggles uacToggleSwitch, MessagesBoxImplementation messagesBoxImplementation)
        {
            Thread checkThread = new Thread(() =>
            {
                try
                {
                    bool isUACEnabled = CheckUACRegistryKey();

                    // Update the toggle switch state on the main thread
                    uacToggleSwitch.Invoke((MethodInvoker)delegate
                    {
                        uacToggleSwitch.IsOn = isUACEnabled;
                    });
                }
                catch (Exception ex)
                {
                    // Show error message on the main thread
                    uacToggleSwitch.Invoke((MethodInvoker)delegate
                    {
                        messagesBoxImplementation.ShowMessage($"Error checking UAC status: {ex.Message}", "Error", MessageBoxButtons.OK);
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
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System"))
                {
                    if (key != null)
                    {
                        object enableLUA = key.GetValue("EnableLUA");
                        if (enableLUA != null && enableLUA is int)
                        {
                            return (int)enableLUA == 1; // UAC enabled if value is 1
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
