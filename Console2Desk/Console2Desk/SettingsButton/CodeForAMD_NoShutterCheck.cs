using Console2Desk.ToggleSwitchDev;
using Microsoft.Win32;

namespace Console2Desk.SettingsButton
{
    internal class CodeForAMD_NoShutterCheck
    {
        public static void CheckEnableUlps(ControlToggles ulpsToggleSwitch, MessagesBoxImplementation messagesBoxImplementation)
        {
            // Run the check in a separate thread
            Thread checkThread = new Thread(() =>
            {
                try
                {
                    // Find the subkey containing the "EnableUlps" value
                    string subKeyName = FindSubKeyWithEnableUlps();

                    if (subKeyName == null)
                    {
                        // throw new Exception("Unable to find a subkey containing 'EnableUlps'.");
                    }

                    // Check the value of the "EnableUlps" registry key
                    int enableUlpsValue = GetEnableUlpsValue(subKeyName);

                    // Update the toggle switch on the main thread
                    ulpsToggleSwitch.Invoke((MethodInvoker)delegate
                    {
                        // Set the toggle switch state based on the value of "EnableUlps"
                        // Inverted logic: 0 sets the toggle to On, 1 sets it to Off
                        ulpsToggleSwitch.IsOn = (enableUlpsValue == 0);
                    });
                }
                catch (Exception ex)
                {
                    // Show error message on the main thread
                    ulpsToggleSwitch.Invoke((MethodInvoker)delegate
                    {
                        messagesBoxImplementation.ShowMessage($"An error occurred while checking 'EnableUlps': {ex.Message}", "Error", MessageBoxButtons.OK);
                    });
                }
            });

            // Start the thread
            checkThread.Start();
        }

        private static string FindSubKeyWithEnableUlps()
        {
            try
            {
                using (RegistryKey mainKey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}"))
                {
                    if (mainKey == null)
                    {
                        throw new Exception("Unable to access the registry key.");
                    }

                    foreach (string subKeyName in mainKey.GetSubKeyNames())
                    {
                        using (RegistryKey subKey = mainKey.OpenSubKey(subKeyName))
                        {
                            if (subKey != null && subKey.GetValue("EnableUlps") != null)
                            {
                                return subKeyName;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions if needed
                //Console.WriteLine($"Error searching for registry key: {ex.Message}");
            }

            return null;
        }

        internal static int GetEnableUlpsValue(string subKeyName)
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey($@"SYSTEM\CurrentControlSet\Control\Class\{{4d36e968-e325-11ce-bfc1-08002be10318}}\{subKeyName}"))
                {
                    if (key != null)
                    {
                        object value = key.GetValue("EnableUlps");
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


