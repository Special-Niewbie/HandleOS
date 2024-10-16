using Console2Desk.ToggleSwitchDev;
using Microsoft.Win32;

using Console2Desk.ToggleSwitchDev;
using Microsoft.Win32;

namespace Console2Desk.SettingsButton
{
    internal class HdwAcceleratedGPUSchedulingCheck
    {
        public static void CheckHdwAcceleratedGPUSchedulingStatus(ControlToggles controlHWAccelleration, MessagesBoxImplementation messagesBoxImplementation)
        {
            Thread checkThread = new Thread(() =>
            {
                try
                {
                    // Ensure the registry key exists and has the correct value
                    EnsureHdwAcceleratedGPUSchedulingRegistryKeyExists();

                    // Check the current state of the registry key
                    bool isHwSchModeEnabled = CheckHdwAcceleratedGPUSchedulingRegistryKey();

                    // Update the toggle switch state on the main thread
                    controlHWAccelleration.Invoke((MethodInvoker)delegate
                    {
                        controlHWAccelleration.IsOn = isHwSchModeEnabled;
                    });
                }
                catch (Exception ex)
                {
                    // Show error message on the main thread
                    controlHWAccelleration.Invoke((MethodInvoker)delegate
                    {
                        messagesBoxImplementation.ShowMessage($"Error checking Hardware Acceleration status: {ex.Message}", "Error", MessageBoxButtons.OK);
                    });
                }
            });

            // Start the thread
            checkThread.Start();
        }

        // Ensure the registry key exists and has the correct value
        private static void EnsureHdwAcceleratedGPUSchedulingRegistryKeyExists()
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\GraphicsDrivers", true))
                {
                    if (key != null)
                    {
                        object hwSchModeValue = key.GetValue("HwSchMode");

                        // If the key doesn't exist, create it and set the value to 2
                        if (hwSchModeValue == null)
                        {
                            key.SetValue("HwSchMode", 2, RegistryValueKind.DWord);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the error if registry modification fails
                DependencyContainer.MessagesBoxImplementation.ShowMessage($"Error creating or updating registry key: {ex.Message}", "Error", MessageBoxButtons.OK);
            }
        }

        // Check the current state of the registry key
        private static bool CheckHdwAcceleratedGPUSchedulingRegistryKey()
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\GraphicsDrivers"))
                {
                    if (key != null)
                    {
                        object controlHWAccelleration = key.GetValue("HwSchMode");
                        if (controlHWAccelleration != null && controlHWAccelleration is int)
                        {
                            return (int)controlHWAccelleration == 2; // Hardware Acceleration enabled if value is 2
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the error
                DependencyContainer.MessagesBoxImplementation.ShowMessage($"Error reading Hardware Acceleration registry key: {ex.Message}", "Error", MessageBoxButtons.OK);
            }

            return false; // Default to false if there's an issue
        }
    }
}

