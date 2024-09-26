using Console2Desk.ToggleSwitchDev;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console2Desk.SettingsButton
{
    internal class GPUINTELSDILowLatency
    {
        public static void ToggleLowLatency(ControlToggles controliNTELgpuSDI, MessagesBoxImplementation messagesBoxImplementation)
        {
            // Esegui l'azione di toggle in un thread separato
            Thread toggleThread = new Thread(() =>
            {
                try
                {
                    // Controlla lo stato attuale del toggle switch
                    bool isOn = controliNTELgpuSDI.IsOn;

                    // Percorso della chiave di registro per il PCI
                    string pciPath = @"SYSTEM\ControlSet001\Enum\PCI";
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

                                        if (!string.IsNullOrEmpty(deviceDesc) && deviceDesc.Contains("Intel(R) Iris(R) Xe"))
                                        {
                                            // Se si tratta di una GPU AMD Radeon
                                            if (isOn)
                                            {
                                                // Abilita la bassa latenza
                                                using (RegistryKey messageSignaledInterruptPropertiesKey = subKey.OpenSubKey(@"Device Parameters\Interrupt Management\MessageSignaledInterruptProperties", true))
                                                {
                                                    if (messageSignaledInterruptPropertiesKey != null)
                                                    {
                                                        messageSignaledInterruptPropertiesKey.SetValue("MSISupported", 1, RegistryValueKind.DWord);
                                                        //Console.WriteLine($"Applied MSISupported=1 for {deviceDesc}");
                                                    }
                                                }

                                                using (RegistryKey affinityPolicyKey = subKey.OpenSubKey(@"Device Parameters\Interrupt Management\Affinity Policy", true))
                                                {
                                                    if (affinityPolicyKey != null)
                                                    {
                                                        affinityPolicyKey.SetValue("DevicePriority", 2, RegistryValueKind.DWord);
                                                        //Console.WriteLine($"Applied DevicePriority=2 for {deviceDesc}");
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                // Disabilita la bassa latenza
                                                using (RegistryKey messageSignaledInterruptPropertiesKey = subKey.OpenSubKey(@"Device Parameters\Interrupt Management\MessageSignaledInterruptProperties", true))
                                                {
                                                    if (messageSignaledInterruptPropertiesKey != null)
                                                    {
                                                        messageSignaledInterruptPropertiesKey.SetValue("MSISupported", 0, RegistryValueKind.DWord);
                                                        //Console.WriteLine($"Set MSISupported=0 for {deviceDesc}");
                                                    }
                                                }

                                                using (RegistryKey affinityPolicyKey = subKey.OpenSubKey(@"Device Parameters\Interrupt Management\Affinity Policy", true))
                                                {
                                                    if (affinityPolicyKey != null)
                                                    {
                                                        affinityPolicyKey.DeleteValue("DevicePriority", false);
                                                        //Console.WriteLine($"Deleted DevicePriority for {deviceDesc}");
                                                    }
                                                }
                                                // Imposta il toggle su OFF dopo aver disabilitato la modalità bassa latenza
                                                controliNTELgpuSDI.Invoke((MethodInvoker)delegate
                                                {
                                                    controliNTELgpuSDI.IsOn = false;
                                                });
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    // Mostra un messaggio di conferma se il toggle è su OFF
                    if (!isOn)
                    {
                        controliNTELgpuSDI.Invoke((MethodInvoker)delegate
                        {
                            messagesBoxImplementation.ShowMessage(
                                "GPU low latency mode has been disabled successfully.",
                                "Information",
                                MessageBoxButtons.OK);
                        });
                    }

                }
                catch (Exception ex)
                {
                    // Mostra un messaggio di errore nel thread principale
                    controliNTELgpuSDI.Invoke((MethodInvoker)delegate
                    {
                        messagesBoxImplementation.ShowMessage($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK);
                    });
                }
            });

            // Avvia il thread
            toggleThread.Start();
        }
    }
}
