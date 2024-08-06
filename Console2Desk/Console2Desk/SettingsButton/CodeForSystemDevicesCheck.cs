using Console2Desk.ToggleSwitchDev;
using System.Management;


namespace Console2Desk.SettingsButton
{
    internal class CodeForSystemDevicesCheck
    {
        public static void CheckSystemDevices(ControlToggles systemDevicesToggleSwitch, MessagesBoxImplementation messagesBoxImplementation)
        {
            // Run the check in a separate thread
            Thread checkThread = new Thread(() =>
            {
                try
                {
                    // List of device names to check
                    string[] deviceNames = {
                        "High precision event timer",
                        "Microsoft Virtua Drive Enumerator",
                        "NDIS Virtual Network Adapter Enumerator",
                        "Remote Desktop Device Redirector Bus"
                    };

                    bool anyDeviceFound = false;
                    bool anyDeviceEnabled = false;

                    foreach (string deviceName in deviceNames)
                    {
                        if (IsDevicePresent(deviceName))
                        {
                            anyDeviceFound = true;

                            if (IsDeviceEnabled(deviceName))
                            {
                                anyDeviceEnabled = true;
                                break; // Stop checking if at least one device is enabled
                            }
                        }
                    }

                    // Update the toggle switch state on the main thread
                    systemDevicesToggleSwitch.Invoke((MethodInvoker)delegate
                    {
                        // Set the toggle switch state based on whether any device is enabled
                        systemDevicesToggleSwitch.IsOn = anyDeviceEnabled;
                    });

                    // Notify the user if no devices were found
                    if (!anyDeviceFound)
                    {
                        //MessageBox.Show("The specific System devices are not present to check.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    // Show error message on the main thread
                    systemDevicesToggleSwitch.Invoke((MethodInvoker)delegate
                    {
                        messagesBoxImplementation.ShowMessage($"An error occurred while checking System devices: {ex.Message}", "Error", MessageBoxButtons.OK);
                    });
                }
            });

            // Start the thread
            checkThread.Start();
        }

        private static bool IsDevicePresent(string deviceName)
        {
            try
            {
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Description LIKE '%" + deviceName + "%'"))
                {
                    ManagementObjectCollection devices = searcher.Get();
                    return devices.Count > 0;
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error checking device presence: {ex.Message}");
                return false;
            }
        }

        private static bool IsDeviceEnabled(string deviceName)
        {
            try
            {
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Description LIKE '%" + deviceName + "%'"))
                {
                    ManagementObjectCollection devices = searcher.Get();
                    foreach (ManagementObject device in devices)
                    {
                        object status = device["Status"];
                        if (status != null && status.ToString() == "OK")
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error checking device status: {ex.Message}");
            }

            return false;
        }
    }
}