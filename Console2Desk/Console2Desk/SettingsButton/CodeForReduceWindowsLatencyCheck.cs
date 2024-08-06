using Console2Desk.ToggleSwitchDev;
using Microsoft.Win32;

namespace Console2Desk.SettingsButton
{
    internal class CodeForReduceWindowsLatencyCheck
    {
        public static void CheckReduceWindowsLatency(ControlToggles latencyToggleSwitch, MessagesBoxImplementation messagesBoxImplementation)
        {
            // Run the check in a separate thread
            Thread checkThread = new Thread(() =>
            {
                try
                {
                    // Determine if latency settings are enabled
                    bool isLatencyEnabled = IsReduceWindowsLatencyEnabled();

                    // Update the toggle switch on the main thread
                    latencyToggleSwitch.Invoke((MethodInvoker)delegate
                    {
                        // Set the toggle switch state based on the registry values
                        latencyToggleSwitch.IsOn = isLatencyEnabled;
                    });
                }
                catch (Exception ex)
                {
                    // Show error message on the main thread
                    latencyToggleSwitch.Invoke((MethodInvoker)delegate
                    {
                        messagesBoxImplementation.ShowMessage($"An error occurred while checking Windows latency settings: {ex.Message}", "Error", MessageBoxButtons.OK);
                    });
                }
            });

            // Start the thread
            checkThread.Start();
        }

        private static bool IsReduceWindowsLatencyEnabled()
        {
            try
            {
                // Registry paths
                string powerRegistryPath = @"SYSTEM\CurrentControlSet\Control\Power";
                string graphicsDriversRegistryPath = @"SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Power";

                // Check if the keys exist and have the value 1
                return CheckRegistryKey(powerRegistryPath, "ExitLatency", 1) &&
                       CheckRegistryKey(powerRegistryPath, "ExitLatencyCheckEnabled", 1) &&
                       CheckRegistryKey(powerRegistryPath, "Latency", 1) &&
                       CheckRegistryKey(powerRegistryPath, "LatencyToleranceDefault", 1) &&
                       CheckRegistryKey(powerRegistryPath, "LatencyToleranceFSVP", 1) &&
                       CheckRegistryKey(powerRegistryPath, "LatencyTolerancePerfOverride", 1) &&
                       CheckRegistryKey(powerRegistryPath, "LatencyToleranceScreenOffIR", 1) &&
                       CheckRegistryKey(powerRegistryPath, "LatencyToleranceVSyncEnabled", 1) &&
                       CheckRegistryKey(powerRegistryPath, "RtlCapabilityCheckLatency", 1) &&
                       CheckRegistryKey(graphicsDriversRegistryPath, "DefaultD3TransitionLatencyActivelyUsed", 1) &&
                       CheckRegistryKey(graphicsDriversRegistryPath, "DefaultD3TransitionLatencyIdleLongTime", 1) &&
                       CheckRegistryKey(graphicsDriversRegistryPath, "DefaultD3TransitionLatencyIdleMonitorOff", 1) &&
                       CheckRegistryKey(graphicsDriversRegistryPath, "DefaultD3TransitionLatencyIdleNoContext", 1) &&
                       CheckRegistryKey(graphicsDriversRegistryPath, "DefaultD3TransitionLatencyIdleShortTime", 1) &&
                       CheckRegistryKey(graphicsDriversRegistryPath, "DefaultD3TransitionLatencyIdleVeryLongTime", 1) &&
                       CheckRegistryKey(graphicsDriversRegistryPath, "DefaultLatencyToleranceIdle0", 1) &&
                       CheckRegistryKey(graphicsDriversRegistryPath, "DefaultLatencyToleranceIdle0MonitorOff", 1) &&
                       CheckRegistryKey(graphicsDriversRegistryPath, "DefaultLatencyToleranceIdle1", 1) &&
                       CheckRegistryKey(graphicsDriversRegistryPath, "DefaultLatencyToleranceIdle1MonitorOff", 1) &&
                       CheckRegistryKey(graphicsDriversRegistryPath, "DefaultLatencyToleranceMemory", 1) &&
                       CheckRegistryKey(graphicsDriversRegistryPath, "DefaultLatencyToleranceNoContext", 1) &&
                       CheckRegistryKey(graphicsDriversRegistryPath, "DefaultLatencyToleranceNoContextMonitorOff", 1) &&
                       CheckRegistryKey(graphicsDriversRegistryPath, "DefaultLatencyToleranceOther", 1) &&
                       CheckRegistryKey(graphicsDriversRegistryPath, "DefaultLatencyToleranceTimerPeriod", 1) &&
                       CheckRegistryKey(graphicsDriversRegistryPath, "DefaultMemoryRefreshLatencyToleranceActivelyUsed", 1) &&
                       CheckRegistryKey(graphicsDriversRegistryPath, "DefaultMemoryRefreshLatencyToleranceMonitorOff", 1) &&
                       CheckRegistryKey(graphicsDriversRegistryPath, "DefaultMemoryRefreshLatencyToleranceNoContext", 1) &&
                       CheckRegistryKey(graphicsDriversRegistryPath, "Latency", 1) &&
                       CheckRegistryKey(graphicsDriversRegistryPath, "MaxIAverageGraphicsLatencyInOneBucket", 1) &&
                       CheckRegistryKey(graphicsDriversRegistryPath, "MiracastPerfTrackGraphicsLatency", 1) &&
                       CheckRegistryKey(graphicsDriversRegistryPath, "MonitorLatencyTolerance", 1) &&
                       CheckRegistryKey(graphicsDriversRegistryPath, "MonitorRefreshLatencyTolerance", 1) &&
                       CheckRegistryKey(graphicsDriversRegistryPath, "TransitionLatency", 1);
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
    }
}
