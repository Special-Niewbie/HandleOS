using Console2Desk.ToggleSwitchDev;
using Microsoft.Win32;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Console2Desk.SettingsButton
{
    internal class CodeForReduceNetworkLatencyCheck
    {
        public static void CheckReduceNetworkLatency(ControlToggles networkLatencyToggleSwitch, MessagesBoxImplementation messagesBoxImplementation)
        {
            // Run the check in a separate thread
            Thread checkThread = new Thread(() =>
            {
                try
                {
                    bool isEnabled = IsReduceNetworkLatencyEnabled();

                    // Update the toggle switch on the main thread
                    networkLatencyToggleSwitch.Invoke((MethodInvoker)delegate
                    {
                        networkLatencyToggleSwitch.IsOn = isEnabled;
                    });
                }
                catch (Exception ex)
                {
                    // Show error message on the main thread
                    networkLatencyToggleSwitch.Invoke((MethodInvoker)delegate
                    {
                        messagesBoxImplementation.ShowMessage($"An error occurred while checking network latency settings: {ex.Message}", "Error", MessageBoxButtons.OK);
                    });
                }
            });

            // Start the thread
            checkThread.Start();
        }

        private static bool IsReduceNetworkLatencyEnabled()
        {
            try
            {
                // Verifica la chiave per MSMQ
                bool msmqEnabled = CheckRegistryKey(@"SOFTWARE\Microsoft\MSMQ\Parameters", "TCPNoDelay", 1);

                // Recupera gli indirizzi IP locali
                List<string> ipAddresses = GetLocalIPAddresses();
                if (ipAddresses.Count == 0)
                {
                    //Console.WriteLine("No IP addresses found.");
                    return false;
                }

                // Verifica le chiavi TCP/IP per tutte le interfacce di rete corrispondenti agli indirizzi IP
                bool tcpipEnabled = false;
                foreach (string ipAddress in ipAddresses)
                {
                    string interfaceKeyPath = FindInterfaceKeyPath(ipAddress);
                    if (interfaceKeyPath != null)
                    {
                        tcpipEnabled = CheckRegistryKey(interfaceKeyPath, "TCPNoDelay", 1) &&
                                       CheckRegistryKey(interfaceKeyPath, "TcpAckFrequency", 1) &&
                                       CheckRegistryKey(interfaceKeyPath, "TcpDelAckTicks", 0);

                        if (tcpipEnabled)
                        {
                            break; // Se troviamo un'interfaccia abilitata, possiamo interrompere il ciclo
                        }
                    }
                }

                return msmqEnabled && tcpipEnabled;
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error checking registry keys: {ex.Message}");
                return false;
            }
        }

        private static bool CheckRegistryKey(string registryPath, string keyName, object expectedValue)
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(registryPath))
                {
                    if (key != null)
                    {
                        object value = key.GetValue(keyName);
                        return value != null && value.ToString() == expectedValue.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error checking registry key {keyName}: {ex.Message}");
            }
            return false;
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

