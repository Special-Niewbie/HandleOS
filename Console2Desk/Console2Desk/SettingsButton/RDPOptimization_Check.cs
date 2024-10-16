using Console2Desk.ToggleSwitchDev;
using Microsoft.Win32;

namespace Console2Desk.SettingsButton
{
    internal class RDPOptimization_Check
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

        private const string InteractiveDelayName = "InteractiveDelay";
        private const int InteractiveDelayOnValue = 0;
        private const int InteractiveDelayOffValue = 50;

        public static void CheckRDPOptimizationStatus(ControlToggles controlRDPOptimization, MessagesBoxImplementation messagesBoxImplementation)
        {
            Thread checkThread = new Thread(() =>
            {
                try
                {
                    // Controlla lo stato delle chiavi e aggiorna il toggle
                    bool isRDPOptimizationEnabled = CheckRDPOptimizationRegistryKeys();

                    controlRDPOptimization.Invoke((MethodInvoker)delegate
                    {
                        controlRDPOptimization.IsOn = isRDPOptimizationEnabled;
                    });
                }
                catch (Exception ex)
                {
                    controlRDPOptimization.Invoke((MethodInvoker)delegate
                    {
                        messagesBoxImplementation.ShowMessage($"Error checking RDP Optimization status: {ex.Message}", "Error", MessageBoxButtons.OK);
                    });
                }
            });

            checkThread.Start();
        }

        // Funzione che controlla se tutte le chiavi sono impostate correttamente
        private static bool CheckRDPOptimizationRegistryKeys()
        {
            try
            {
                bool allValid = true;

                allValid &= CheckRegistryValues(Registry.LocalMachine, TerminalServicesKeyPath, TerminalServicesValues);
                allValid &= CheckRegistryValues(Registry.LocalMachine, TermDDKeyPath, TermDDValues);
                allValid &= CheckRegistryValues(Registry.LocalMachine, WinStationsKeyPath, WinStationsValues);
                allValid &= CheckRegistryValues(Registry.LocalMachine, LanmanWorkstationKeyPath, LanmanWorkstationValues);

                // Controlla InteractiveDelay
                allValid &= CheckRegistryValue(Registry.LocalMachine, InteractiveDelayKeyPath, InteractiveDelayName, InteractiveDelayOnValue);

                return allValid;
            }
            catch (Exception ex)
            {
                DependencyContainer.MessagesBoxImplementation.ShowMessage($"Error reading RDP Optimization registry keys: {ex.Message}", "Error", MessageBoxButtons.OK);
                return false;
            }
        }

        // Funzione helper per controllare i valori di registro
        private static bool CheckRegistryValues(RegistryKey root, string keyPath, (string, object)[] values)
        {
            using (RegistryKey key = root.OpenSubKey(keyPath))
            {
                if (key != null)
                {
                    foreach (var (name, value) in values)
                    {
                        object existingValue = key.GetValue(name);
                        if (existingValue == null || !existingValue.Equals(value))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private static bool CheckRegistryValue(RegistryKey root, string keyPath, string valueName, object value)
        {
            using (RegistryKey key = root.OpenSubKey(keyPath))
            {
                if (key != null)
                {
                    object existingValue = key.GetValue(valueName);
                    return existingValue != null && existingValue.Equals(value);
                }
            }
            return false;
        }
    }
}
