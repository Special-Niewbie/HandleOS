using Console2Desk.ToggleSwitchDev;
using System.Diagnostics;
using System.Management;

namespace Console2Desk.SettingsButton
{
    internal class CodeForSystemDevices
    {
        public static async Task ToggleSystemDevicesAsync(ControlToggles systemDevicesToggleSwitch, MessagesBoxImplementation messagesBoxImplementation)
        {
            try
            {
                // Get the state of the toggle switch
                bool isOn = systemDevicesToggleSwitch.IsOn;

                // List of device names to manage
                string[] deviceNames = {
                    "High precision event timer",
                    "Microsoft Virtual Drive Enumerator",
                    "NDIS Virtual Network Adapter Enumerator",
                    "Remote Desktop Device Redirector Bus"
                };

                bool anyDeviceManaged = false;

                // Show a message indicating that the operation might take some time
                systemDevicesToggleSwitch.Invoke((MethodInvoker)delegate
                {
                    messagesBoxImplementation.ShowMessage(
                        "Changing system devices might take some time. Please wait the next message appearing and do not close the application." +
                        "Disabling these components may affect virtualization functionality, timing precision, " +
                        "with Remote Desktop and virtual drives. If you use Remote Desktop or virtualization services, it is advised to re-enable " +
                        "these components to ensure proper operation.",
                        "Processing",
                        MessageBoxButtons.OK);

                    // Change cursor to wait cursor
                    Cursor.Current = Cursors.WaitCursor;
                });

                // Check if each device is present and enable/disable accordingly
                foreach (string deviceName in deviceNames)
                {
                    try
                    {
                        if (await IsDevicePresentAsync(deviceName))
                        {
                            if (isOn)
                            {
                                await EnableDeviceAsync(deviceName);
                            }
                            else
                            {
                                await DisableDeviceAsync(deviceName);
                            }
                            anyDeviceManaged = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log errors for each device
                        //Console.WriteLine($"Error managing device '{deviceName}': {ex.Message}");
                    }
                }

                // Reset cursor to default
                systemDevicesToggleSwitch.Invoke((MethodInvoker)delegate
                {
                    Cursor.Current = Cursors.Default;
                });

                // Check if any devices were found and managed
                if (!anyDeviceManaged)
                {
                    systemDevicesToggleSwitch.Invoke((MethodInvoker)delegate
                    {
                        messagesBoxImplementation.ShowMessage("The specific System devices are not present to enable/disable.", "Warning", MessageBoxButtons.OK);
                    });
                }
                
                // Ask the user if they want to restart the computer
                systemDevicesToggleSwitch.Invoke((MethodInvoker)delegate
                {
                    Cursor.Current = Cursors.WaitCursor;
                    Thread.Sleep(6000); // Wait for 6 seconds after each device action
                    Cursor.Current = Cursors.Default;
                    DialogResult result = messagesBoxImplementation.ShowMessage(
                        "Changes have been applied, do you want to restart your computer now to ensure all changes are fully applied?",
                        "Restart Recommended",
                        MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        RestartComputer();
                    }
                });
            }
            catch (Exception ex)
            {
                // Show error message on the main thread
                systemDevicesToggleSwitch.Invoke((MethodInvoker)delegate
                {
                    Cursor.Current = Cursors.Default;
                    messagesBoxImplementation.ShowMessage($"An error occurred while toggling System devices: {ex.Message}", "Error", MessageBoxButtons.OK);
                });
            }
        }

        private static async Task<bool> IsDevicePresentAsync(string deviceName)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (ManagementObjectSearcher searcher = new ManagementObjectSearcher($"SELECT * FROM Win32_PnPEntity WHERE Name LIKE '%{deviceName}%'"))
                    {
                        ManagementObjectCollection devices = searcher.Get();
                        return devices.Count > 0;
                    }
                }
                catch (Exception ex)
                {
                    //Console.WriteLine($"Error checking device presence for '{deviceName}': {ex.Message}");
                    return false;
                }
            });
        }

        private static async Task EnableDeviceAsync(string deviceName)
        {
            await ManageDeviceAsync(deviceName, "Enable");
        }

        private static async Task DisableDeviceAsync(string deviceName)
        {
            await ManageDeviceAsync(deviceName, "Disable");
        }

        private static async Task ManageDeviceAsync(string deviceName, string action)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (ManagementObjectSearcher searcher = new ManagementObjectSearcher($"SELECT * FROM Win32_PnPEntity WHERE Name LIKE '%{deviceName}%'"))
                    {
                        ManagementObjectCollection devices = searcher.Get();
                        if (devices == null || devices.Count == 0)
                        {
                            //Console.WriteLine($"No devices found for {action.ToLower()}ing: {deviceName}");
                            return;
                        }

                        foreach (ManagementObject device in devices)
                        {
                            if (device == null)
                            {
                                //Console.WriteLine($"Device is null for: {deviceName}");
                                continue;
                            }

                            try
                            {
                                //Console.WriteLine($"{action}ing device: {deviceName}");
                                device.InvokeMethod(action, null);
                                Thread.Sleep(2000); // Wait for 10 seconds after each device action
                            }
                            catch (Exception ex)
                            {
                                //Console.WriteLine($"Error {action.ToLower()}ing device '{deviceName}': {ex.Message}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    //Console.WriteLine($"Error {action.ToLower()}ing the device '{deviceName}': {ex.Message}");
                }
            });
        }

        private static void RestartComputer()
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "shutdown",
                    Arguments = "/r /t 0",
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                DependencyContainer.MessagesBoxImplementation.ShowMessage($"Error restarting computer:" + ex.Message, "Error", MessageBoxButtons.OK);
            }
        }
    }
}