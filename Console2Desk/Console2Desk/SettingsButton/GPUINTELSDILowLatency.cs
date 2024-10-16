using Console2Desk.ToggleSwitchDev;
using Microsoft.Win32;

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
                    bool foundIntelGPU = false;

                    // Percorso della chiave di registro per il PCI
                    string pciPath = @"SYSTEM\ControlSet001\Enum\PCI";
                    using (RegistryKey pciKey = Registry.LocalMachine.OpenSubKey(pciPath))
                    {
                        if (pciKey == null)
                        {
                            throw new Exception("Unable to access PCI registry key.");
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
                                            foundIntelGPU = true;

                                            // Se si tratta di una GPU Intel Iris Xe
                                            if (isOn)
                                            {
                                                EnableLowLatency(subKey);
                                            }
                                            else
                                            {
                                                DisableLowLatency(subKey);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    // Aggiorna l'UI nel thread principale
                    controliNTELgpuSDI.Invoke((MethodInvoker)delegate
                    {
                        if (!foundIntelGPU)
                        {
                            controliNTELgpuSDI.IsOn = false;
                            messagesBoxImplementation.ShowMessage(
                                "Model of GPU not supported, please contact the Developer to add your GPU model.",
                                "Information",
                                MessageBoxButtons.OK);
                        }
                        else
                        {
                            controliNTELgpuSDI.IsOn = isOn;
                            string message = isOn
                                ? "GPU low latency mode has been enabled successfully."
                                : "GPU low latency mode has been disabled successfully.";
                            messagesBoxImplementation.ShowMessage(message, "Information", MessageBoxButtons.OK);
                        }
                    });
                }
                catch (Exception ex)
                {
                    // Mostra un messaggio di errore nel thread principale
                    controliNTELgpuSDI.Invoke((MethodInvoker)delegate
                    {
                        controliNTELgpuSDI.IsOn = false;
                        messagesBoxImplementation.ShowMessage($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK);
                    });
                }
            });

            // Avvia il thread
            toggleThread.Start();
        }

        private static void EnableLowLatency(RegistryKey subKey)
        {
            using (RegistryKey messageSignaledInterruptPropertiesKey = subKey.OpenSubKey(@"Device Parameters\Interrupt Management\MessageSignaledInterruptProperties", true))
            {
                if (messageSignaledInterruptPropertiesKey != null)
                {
                    messageSignaledInterruptPropertiesKey.SetValue("MSISupported", 1, RegistryValueKind.DWord);
                }
            }

            using (RegistryKey affinityPolicyKey = subKey.OpenSubKey(@"Device Parameters\Interrupt Management\Affinity Policy", true))
            {
                if (affinityPolicyKey != null)
                {
                    affinityPolicyKey.SetValue("DevicePriority", 2, RegistryValueKind.DWord);
                }
            }
        }

        private static void DisableLowLatency(RegistryKey subKey)
        {
            using (RegistryKey messageSignaledInterruptPropertiesKey = subKey.OpenSubKey(@"Device Parameters\Interrupt Management\MessageSignaledInterruptProperties", true))
            {
                if (messageSignaledInterruptPropertiesKey != null)
                {
                    messageSignaledInterruptPropertiesKey.SetValue("MSISupported", 0, RegistryValueKind.DWord);
                }
            }

            using (RegistryKey affinityPolicyKey = subKey.OpenSubKey(@"Device Parameters\Interrupt Management\Affinity Policy", true))
            {
                if (affinityPolicyKey != null)
                {
                    affinityPolicyKey.DeleteValue("DevicePriority", false);
                }
            }
        }
    }
}
