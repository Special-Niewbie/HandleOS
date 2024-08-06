using Console2Desk.ToggleSwitchDev;
using Microsoft.Win32;

namespace Console2Desk.SettingsButton
{
    internal class CodeForAMD_NoShutter
    {
        public static void ToggleEnableUlps(ControlToggles ulpsToggleSwitch, MessagesBoxImplementation messagesBoxImplementation)
        {
            // Run the toggle action in a separate thread
            Thread toggleThread = new Thread(() =>
            {
                try
                {
                    string subKeyName = FindSubKeyWithEnableUlps();

                    if (subKeyName == null)
                    {
                        throw new Exception("Unable to find a subkey containing 'EnableUlps'.");
                    }

                    // Get the current value of the "EnableUlps" registry key
                    int currentValue = GetEnableUlpsValue(subKeyName);

                    // Determine the new value based on the toggle switch state
                    // Inverted logic: On (true) sets to 0, Off (false) sets to 1
                    int newValue = ulpsToggleSwitch.IsOn ? 0 : 1;

                    // Update the registry key if the value has changed
                    if (currentValue != newValue)
                    {
                        SetEnableUlpsValue(subKeyName, newValue);
                    }

                    // Update the toggle switch state on the main thread
                    ulpsToggleSwitch.Invoke((MethodInvoker)delegate
                    {
                        ulpsToggleSwitch.IsOn = (newValue == 0);
                    });
                }
                catch (Exception ex)
                {
                    // Show error message on the main thread
                    ulpsToggleSwitch.Invoke((MethodInvoker)delegate
                    {
                        messagesBoxImplementation.ShowMessage($"An error occurred while toggling 'EnableUlps': {ex.Message}", "Error", MessageBoxButtons.OK);
                    });
                }
            });

            // Start the thread
            toggleThread.Start();
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

        private static int GetEnableUlpsValue(string subKeyName)
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey($@"SYSTEM\CurrentControlSet\Control\Class\{{4d36e968-e325-11ce-bfc1-08002be10318}}\{subKeyName}"))
                {
                    if (key != null)
                    {
                        object value = key.GetValue("EnableUlps");
                        if (value != null && int.TryParse(value.ToString(), out int intValue))
                        {
                            return intValue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions if needed
                //Console.WriteLine($"Error reading registry key value: {ex.Message}");
            }

            return -1; // Return a default value indicating an error
        }

        private static void SetEnableUlpsValue(string subKeyName, int value)
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey($@"SYSTEM\CurrentControlSet\Control\Class\{{4d36e968-e325-11ce-bfc1-08002be10318}}\{subKeyName}", writable: true))
                {
                    if (key != null)
                    {
                        key.SetValue("EnableUlps", value, RegistryValueKind.DWord);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions if needed
                //Console.WriteLine($"Error setting registry key value: {ex.Message}");
            }
        }
    }
}
