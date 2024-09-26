using Console2Desk.ToggleSwitchDev;
using Microsoft.Win32;

namespace Console2Desk.SettingsButton
{
    internal class GPUAMDSDILowLatencyCheck
    {
        public static void CheckLowLatency(ControlToggles controlAMDgpuSDI, MessagesBoxImplementation messagesBoxImplementation)
        {
            try
            {
                // Percorso della chiave di registro per il PCI
                string pciPath = @"SYSTEM\ControlSet001\Enum\PCI";
                bool foundMSISupported = false;

                using (RegistryKey pciKey = Registry.LocalMachine.OpenSubKey(pciPath))
                {
                    if (pciKey == null)
                    {
                        throw new Exception("Model of GPU not supporte, please contact the Developper to add your GPU model.");
                    }

                    // Itera su ogni sottocartella del dispositivo PCI
                    foreach (string deviceKeyName in pciKey.GetSubKeyNames())
                    {
                        using (RegistryKey deviceKey = pciKey.OpenSubKey(deviceKeyName))
                        {
                            if (deviceKey == null) continue;

                            foreach (string subKeyName in deviceKey.GetSubKeyNames())
                            {
                                using (RegistryKey subKey = deviceKey.OpenSubKey(subKeyName))
                                {
                                    if (subKey == null) continue;

                                    // Cerca la voce DeviceDesc
                                    string deviceDesc = (string)subKey.GetValue("DeviceDesc");

                                    if (!string.IsNullOrEmpty(deviceDesc) && deviceDesc.Contains("AMD Radeon"))
                                    {
                                        // Controlla lo stato della chiave MSISupported
                                        using (RegistryKey messageSignaledInterruptPropertiesKey = subKey.OpenSubKey(@"Device Parameters\Interrupt Management\MessageSignaledInterruptProperties"))
                                        {
                                            if (messageSignaledInterruptPropertiesKey != null)
                                            {
                                                object msiSupportedValue = messageSignaledInterruptPropertiesKey.GetValue("MSISupported");

                                                if (msiSupportedValue != null && (int)msiSupportedValue == 1)
                                                {
                                                    // La chiave MSISupported è impostata su 1
                                                    foundMSISupported = true;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            if (foundMSISupported)
                            {
                                break; // Uscita dal ciclo se la chiave è stata trovata e impostata
                            }
                        }
                    }
                }

                // Aggiorna lo stato del toggle switch nel thread principale
                controlAMDgpuSDI.Invoke((MethodInvoker)delegate
                {
                    controlAMDgpuSDI.IsOn = foundMSISupported;
                });
            }
            catch (Exception ex)
            {
                // Mostra un messaggio di errore nel thread principale
                controlAMDgpuSDI.Invoke((MethodInvoker)delegate
                {
                    //MessageBox.Show($"Errore durante il controllo dello stato della latenza GPU: {ex.Message}", "Errore", MessageBoxButtons.OK);
                });
            }
        }
    }
}
