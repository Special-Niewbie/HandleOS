using Console2Desk.ToggleSwitchDev;
using Microsoft.Win32;

namespace Console2Desk.SettingsButton
{
    internal class CodeForMeltdownSpectreProtectionCheck
    {
        public static void CheckMeltdownSpectreProtection(ControlToggles protectionToggleSwitch, MessagesBoxImplementation messagesBoxImplementation)
        {
            // Run the check in a separate thread
            Thread checkThread = new Thread(() =>
            {
                try
                {
                    // Find the value of the "FeatureSettings" registry key
                    int featureSettingsValue = GetFeatureSettingsValue();

                    // Update the toggle switch on the main thread
                    protectionToggleSwitch.Invoke((MethodInvoker)delegate
                    {
                        // Set the toggle switch state based on the value of "FeatureSettings"
                        protectionToggleSwitch.IsOn = featureSettingsValue == 0;
                    });
                }
                catch (Exception ex)
                {
                    // Show error message on the main thread
                    protectionToggleSwitch.Invoke((MethodInvoker)delegate
                    {
                        messagesBoxImplementation.ShowMessage($"An error occurred while checking Meltdown/Spectre protection: {ex.Message}", "Error", MessageBoxButtons.OK);
                    });
                }
            });

            // Start the thread
            checkThread.Start();
        }

        private static int GetFeatureSettingsValue()
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management"))
                {
                    if (key != null)
                    {
                        object value = key.GetValue("FeatureSettings");
                        if (value != null && value is int)
                        {
                            return (int)value;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions if needed
                //Console.WriteLine($"Error accessing registry key: {ex.Message}");
            }

            // Default to 1 if the key or value is not found
            return 1;
        }
    }
}
