using Console2Desk.ToggleSwitchDev;
using Microsoft.Win32;

namespace Console2Desk.SettingsButton
{
    internal class RDPOptimization
    {
        // Percorsi del Registro di sistema
        private const string TerminalServicesKeyPath = @"SOFTWARE\Policies\Microsoft\Windows NT\Terminal Services";
        private const string TermDDKeyPath = @"SYSTEM\CurrentControlSet\Services\TermDD";
        private const string WinStationsKeyPath = @"SYSTEM\CurrentControlSet\Control\Terminal Server\WinStations";
        private const string LanmanWorkstationKeyPath = @"SYSTEM\CurrentControlSet\Services\LanmanWorkstation\Parameters";
        private const string InteractiveDelayKeyPath = @"SYSTEM\CurrentControlSet\Control\Terminal Server\WinStations\RDP-Tcp";

        // Valori del Registro di sistema
        private static readonly (string, object)[] TerminalServicesValues =
        {
            ("SelectTransport", 0),
            ("fEnableVirtualizedGraphics", 1),
            ("fEnableRemoteFXAdvancedRemoteApp", 1),
            ("MaxCompressionLevel", 2),
            ("VisualExperiencePolicy", 1),
            ("GraphicsProfile", 2),
            ("bEnumerateHWBeforeSW", 1),
            ("AVC444ModePreferred", 1),
            ("AVCHardwareEncodePreferred", 1),
            ("VGOptimization_CaptureFrameRate", 60),   // dword:0000003c (60 in esadecimale)
            ("VGOptimization_CompressionRatio", 2),
            ("ImageQuality", 3)
        };

        private static readonly (string, object)[] TermDDValues =
        {
            ("FlowControlDisable", 1),
            ("FlowControlDisplayBandwidth", 0x10),     // dword:0000010 (16 in esadecimale)
            ("FlowControlChannelBandwidth", 0x90),     // dword:0000090 (144 in esadecimale)
            ("FlowControlChargePostCompression", 0)
        };

        private static readonly (string, object)[] WinStationsValues =
        {
            ("DWMFRAMEINTERVAL", 0x0f)                 // dword:0000000f (15 in esadecimale)
        };

        private static readonly (string, object)[] LanmanWorkstationValues =
        {
            ("DisableBandwidthThrottling", 1),
            ("DisableLargeMtu", 0)
        };

        // Chiave InteractiveDelay (esclusa dall'eliminazione)
        private const string InteractiveDelayName = "InteractiveDelay";
        private const int InteractiveDelayOnValue = 0;
        private const int InteractiveDelayOffValue = 50;

        public static void ApplyRDPOptimizationToggle(ControlToggles controlRDPOptimization, MessagesBoxImplementation messagesBoxImplementation)
        {
            // Start a new thread to avoid blocking the UI
            Thread applyThread = new Thread(() =>
            {
                try
                {
                    if (controlRDPOptimization.IsOn)
                    {
                        // Toggle ON: Applica le ottimizzazioni
                        EnableRDPOptimization();
                    }
                    else
                    {
                        // Toggle OFF: Rimuovi le ottimizzazioni, eccetto InteractiveDelay
                        DisableRDPOptimization();
                    }
                }
                catch (Exception ex)
                {
                    // Mostra un messaggio di errore in caso di eccezione
                    controlRDPOptimization.Invoke((MethodInvoker)delegate
                    {
                        messagesBoxImplementation.ShowMessage($"Error applying RDP Optimization: {ex.Message}", "Error", MessageBoxButtons.OK);
                    });
                }
            });

            // Start the thread
            applyThread.Start();
        }

        // Metodo per abilitare le ottimizzazioni RDP
        private static void EnableRDPOptimization()
        {
            try
            {
                // Scrivi le chiavi sotto Terminal Services
                WriteRegistryValues(Registry.LocalMachine, TerminalServicesKeyPath, TerminalServicesValues);

                // Scrivi le chiavi sotto TermDD
                WriteRegistryValues(Registry.LocalMachine, TermDDKeyPath, TermDDValues);

                // Scrivi le chiavi sotto WinStations
                WriteRegistryValues(Registry.LocalMachine, WinStationsKeyPath, WinStationsValues);

                // Scrivi le chiavi sotto LanmanWorkstation
                WriteRegistryValues(Registry.LocalMachine, LanmanWorkstationKeyPath, LanmanWorkstationValues);

                // Imposta InteractiveDelay a 0
                WriteRegistryValue(Registry.LocalMachine, InteractiveDelayKeyPath, InteractiveDelayName, InteractiveDelayOnValue);
            }
            catch (Exception ex)
            {
                DependencyContainer.MessagesBoxImplementation.ShowMessage($"Error enabling RDP Optimization: {ex.Message}", "Error", MessageBoxButtons.OK);
            }
        }

        // Metodo per disabilitare le ottimizzazioni RDP
        private static void DisableRDPOptimization()
        {
            try
            {
                // Rimuovi le chiavi sotto Terminal Services
                DeleteRegistryValues(Registry.LocalMachine, TerminalServicesKeyPath, TerminalServicesValues);

                // Rimuovi le chiavi sotto TermDD
                DeleteRegistryValues(Registry.LocalMachine, TermDDKeyPath, TermDDValues);

                // Rimuovi le chiavi sotto WinStations
                DeleteRegistryValues(Registry.LocalMachine, WinStationsKeyPath, WinStationsValues);

                // Rimuovi le chiavi sotto LanmanWorkstation
                DeleteRegistryValues(Registry.LocalMachine, LanmanWorkstationKeyPath, LanmanWorkstationValues);

                // Riporta InteractiveDelay a 50
                WriteRegistryValue(Registry.LocalMachine, InteractiveDelayKeyPath, InteractiveDelayName, InteractiveDelayOffValue);
            }
            catch (Exception ex)
            {
                DependencyContainer.MessagesBoxImplementation.ShowMessage($"Error disabling RDP Optimization: {ex.Message}", "Error", MessageBoxButtons.OK);
            }
        }

        // Metodo per scrivere più valori in una chiave di registro
        private static void WriteRegistryValues(RegistryKey root, string keyPath, (string, object)[] values)
        {
            using (RegistryKey key = root.OpenSubKey(keyPath, true) ?? root.CreateSubKey(keyPath))
            {
                if (key != null)
                {
                    foreach (var (name, value) in values)
                    {
                        key.SetValue(name, value, RegistryValueKind.DWord);
                    }
                }
            }
        }

        // Metodo per eliminare più valori da una chiave di registro
        private static void DeleteRegistryValues(RegistryKey root, string keyPath, (string, object)[] values)
        {
            using (RegistryKey key = root.OpenSubKey(keyPath, true))
            {
                if (key != null)
                {
                    foreach (var (name, _) in values)
                    {
                        key.DeleteValue(name, false);
                    }
                }
            }
        }

        // Metodo per scrivere un singolo valore nel registro
        private static void WriteRegistryValue(RegistryKey root, string keyPath, string valueName, object value)
        {
            using (RegistryKey key = root.OpenSubKey(keyPath, true) ?? root.CreateSubKey(keyPath))
            {
                if (key != null)
                {
                    key.SetValue(valueName, value, RegistryValueKind.DWord);
                }
            }
        }
    }
}
