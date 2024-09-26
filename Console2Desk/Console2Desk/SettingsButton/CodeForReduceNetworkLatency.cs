using Console2Desk.ToggleSwitchDev;
using Microsoft.Win32;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Console2Desk.SettingsButton
{
    internal class CodeForReduceNetworkLatency
    {
        public static void ToggleReduceNetworkLatency(ControlToggles networkLatencyToggleSwitch, MessagesBoxImplementation messagesBoxImplementation)
        {
            // Run the toggle action in a separate thread
            Thread toggleThread = new Thread(() =>
            {
                try
                {
                    bool isOn = networkLatencyToggleSwitch.IsOn;

                    if (isOn)
                    {
                        // Add registry keys to reduce network latency
                        AddTcpipRegistryKeys();
                        CreateOrUpdateRegistryKey(@"SOFTWARE\Microsoft\MSMQ\Parameters", "TCPNoDelay", 1);
                    }
                    else
                    {
                        // Remove registry keys to revert changes
                        RemoveTcpipRegistryKeys();
                        RemoveRegistryKey(@"SOFTWARE\Microsoft\MSMQ\Parameters", "TCPNoDelay");
                    }

                    // Update the toggle switch state on the main thread
                    networkLatencyToggleSwitch.Invoke((MethodInvoker)delegate
                    {
                        networkLatencyToggleSwitch.IsOn = !isOn;
                    });
                }
                catch (Exception ex)
                {
                    // Show error message on the main thread
                    networkLatencyToggleSwitch.Invoke((MethodInvoker)delegate
                    {
                        messagesBoxImplementation.ShowMessage($"An error occurred while toggling network latency settings: {ex.Message}", "Error", MessageBoxButtons.OK);
                    });
                }
            });

            // Start the thread
            toggleThread.Start();
        }

        private static void CreateOrUpdateRegistryKey(string registryPath, string keyName, object value)
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.CreateSubKey(registryPath, RegistryKeyPermissionCheck.ReadWriteSubTree))
                {
                    if (key != null)
                    {
                        key.SetValue(keyName, value, RegistryValueKind.DWord);
                    }
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error setting registry key {keyName}: {ex.Message}");
            }
        }

        private static void RemoveRegistryKey(string registryPath, string keyName)
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(registryPath, writable: true))
                {
                    if (key != null)
                    {
                        key.DeleteValue(keyName, throwOnMissingValue: false);
                    }
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error removing registry key {keyName}: {ex.Message}");
            }
        }

        private static void AddTcpipRegistryKeys()
        {
            try
            {
                List<string> ipAddresses = GetLocalIPAddresses();
                if (ipAddresses.Count == 0)
                {
                    //Console.WriteLine("No IP addresses found.");
                    return;
                }

                foreach (string ipAddress in ipAddresses)
                {
                    string interfaceKeyPath = FindInterfaceKeyPath(ipAddress);
                    if (interfaceKeyPath != null)
                    {
                        using (RegistryKey interfaceKey = Registry.LocalMachine.OpenSubKey(interfaceKeyPath, writable: true))
                        {
                            if (interfaceKey != null)
                            {
                                interfaceKey.SetValue("TCPNoDelay", 1, RegistryValueKind.DWord);
                                interfaceKey.SetValue("TcpAckFrequency", 1, RegistryValueKind.DWord);
                                interfaceKey.SetValue("TcpDelAckTicks", 0, RegistryValueKind.DWord);
                            }
                            else
                            {
                                //Console.WriteLine($"Registry key not found: {interfaceKeyPath}");
                            }
                        }
                    }
                    else
                    {
                        //Console.WriteLine($"Interface key path not found for IP address: {ipAddress}");
                    }
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error adding TCP/IP registry keys: {ex.Message}");
            }
        }

        private static void RemoveTcpipRegistryKeys()
        {
            try
            {
                List<string> ipAddresses = GetLocalIPAddresses();
                if (ipAddresses.Count == 0)
                {
                    Console.WriteLine("No IP addresses found.");
                    return;
                }

                foreach (string ipAddress in ipAddresses)
                {
                    string interfaceKeyPath = FindInterfaceKeyPath(ipAddress);
                    if (interfaceKeyPath != null)
                    {
                        using (RegistryKey interfaceKey = Registry.LocalMachine.OpenSubKey(interfaceKeyPath, writable: true))
                        {
                            if (interfaceKey != null)
                            {
                                interfaceKey.DeleteValue("TCPNoDelay", throwOnMissingValue: false);
                                interfaceKey.DeleteValue("TcpAckFrequency", throwOnMissingValue: false);
                                interfaceKey.DeleteValue("TcpDelAckTicks", throwOnMissingValue: false);
                            }
                            else
                            {
                                //Console.WriteLine($"Registry key not found: {interfaceKeyPath}");
                            }
                        }
                    }
                    else
                    {
                        //Console.WriteLine($"Interface key path not found for IP address: {ipAddress}");
                    }
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error removing TCP/IP registry keys: {ex.Message}");
            }
        }

        private static List<string> GetLocalIPAddresses()
        {
            List<string> ipAddresses = new List<string>();
            try
            {
                foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (ni.OperationalStatus == OperationalStatus.Up)
                    {
                        foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                        {
                            if (ip.Address.AddressFamily == AddressFamily.InterNetwork) // IPv4
                            {
                                ipAddresses.Add(ip.Address.ToString());
                                //Console.WriteLine($"Interface: {ni.Name}, IP: {ip.Address}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error getting local IP addresses: {ex.Message}");
            }
            return ipAddresses;
        }

        private static string FindInterfaceKeyPath(string ipAddress)
        {
            try
            {
                // Lista dei percorsi del registry
                string[] registryPaths =
                {
                    @"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\Interfaces",
                    @"SYSTEM\ControlSet001\Services\Tcpip\Parameters\Interfaces",
                    @"SYSTEM\ControlSet002\Services\Tcpip\Parameters\Interfaces"
                };

                foreach (string basePath in registryPaths)
                {
                    using (RegistryKey parametersKey = Registry.LocalMachine.OpenSubKey(basePath))
                    {
                        if (parametersKey != null)
                        {
                            foreach (string subKeyName in parametersKey.GetSubKeyNames())
                            {
                                using (RegistryKey interfaceKey = parametersKey.OpenSubKey(subKeyName))
                                {
                                    if (interfaceKey != null)
                                    {
                                        string dhcpIPAddress = interfaceKey.GetValue("DhcpIPAddress") as string;
                                        if (dhcpIPAddress == ipAddress)
                                        {
                                            return basePath + "\\" + subKeyName;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error finding interface key path: {ex.Message}");
            }

            return null;
        }
    }
}

