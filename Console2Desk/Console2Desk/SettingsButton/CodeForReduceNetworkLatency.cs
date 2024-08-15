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
                        //messagesBoxImplementation.ShowMessage($"An error occurred while toggling network latency settings: {ex.Message}", "Error", MessageBoxButtons.OK);
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
                //DependencyContainer.MessagesBoxImplementation.ShowMessage($"Error setting registry key {keyName}: {ex.Message}", "Error", MessageBoxButtons.OK);
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
                DependencyContainer.MessagesBoxImplementation.ShowMessage($"Error removing registry key {keyName}: {ex.Message}", "Error", MessageBoxButtons.OK);
            }
        }

        private static void AddTcpipRegistryKeys()
        {
            try
            {
                string ipAddress = GetLocalIPAddress();
                if (string.IsNullOrEmpty(ipAddress))
                {
                    Console.WriteLine("No IP address found.");
                    return;
                }

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
                            //DependencyContainer.MessagesBoxImplementation.ShowMessage($"Registry key not found: {interfaceKeyPath}", "Error", MessageBoxButtons.OK);
                        }
                    }
                }
                else
                {
                    //DependencyContainer.MessagesBoxImplementation.ShowMessage("Interface key path not found for IP address.", "Error", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                //DependencyContainer.MessagesBoxImplementation.ShowMessage($"Error adding TCP/IP registry keys: {ex.Message}", "Error", MessageBoxButtons.OK);
            }
        }

        private static void RemoveTcpipRegistryKeys()
        {
            try
            {
                string ipAddress = GetLocalIPAddress();
                if (string.IsNullOrEmpty(ipAddress))
                {
                    DependencyContainer.MessagesBoxImplementation.ShowMessage("No IP address found.", "Error", MessageBoxButtons.OK);
                    return;
                }

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
                            //DependencyContainer.MessagesBoxImplementation.ShowMessage($"Registry key not found: {interfaceKeyPath}", "Error", MessageBoxButtons.OK);
                        }
                    }
                }
                else
                {
                    //DependencyContainer.MessagesBoxImplementation.ShowMessage("Interface key path not found for IP address.", "Error", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                 //DependencyContainer.MessagesBoxImplementation.ShowMessage($"Error removing TCP/IP registry keys: {ex.Message}", "Error", MessageBoxButtons.OK);
            }
        }

        private static string GetLocalIPAddress()
        {
            try
            {
                foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
                {
                    foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork) // IPv4
                        {
                            return ip.Address.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DependencyContainer.MessagesBoxImplementation.ShowMessage($"Error getting local IP address: {ex.Message}", "Error", MessageBoxButtons.OK);
            }
            return null;
        }

        private static string FindInterfaceKeyPath(string ipAddress)
        {
            try
            {
                using (RegistryKey parametersKey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\Interfaces"))
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
                                        return @"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\Interfaces\" + subKeyName;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //DependencyContainer.MessagesBoxImplementation.ShowMessage($"Error finding interface key path: {ex.Message}", "Error", MessageBoxButtons.OK);
            }
            return null;
        }
    }
}
