using Console2Desk.ToggleSwitchDev;
using Microsoft.Win32;
using System.Diagnostics;


namespace Console2Desk.SettingsButton
{
    internal class CodeForReduceWindowsLatency
    {
        public static void ToggleReduceWindowsLatency(ControlToggles latencyToggleSwitch, MessagesBoxImplementation messagesBoxImplementation)
        {
            // Run the toggle action in a separate thread
            Thread toggleThread = new Thread(() =>
            {
                try
                {
                    bool isOn = latencyToggleSwitch.IsOn;

                    // Registry paths
                    string powerRegistryPath = @"SYSTEM\CurrentControlSet\Control\Power";
                    string graphicsDriversRegistryPath = @"SYSTEM\CurrentControlSet\Control\GraphicsDrivers\Power";

                    if (isOn)
                    {
                        // Add keys and values for latency reduction
                        AddOrUpdateRegistryKey(powerRegistryPath, "ExitLatency", 1);
                        AddOrUpdateRegistryKey(powerRegistryPath, "ExitLatencyCheckEnabled", 1);
                        AddOrUpdateRegistryKey(powerRegistryPath, "Latency", 1);
                        AddOrUpdateRegistryKey(powerRegistryPath, "LatencyToleranceDefault", 1);
                        AddOrUpdateRegistryKey(powerRegistryPath, "LatencyToleranceFSVP", 1);
                        AddOrUpdateRegistryKey(powerRegistryPath, "LatencyTolerancePerfOverride", 1);
                        AddOrUpdateRegistryKey(powerRegistryPath, "LatencyToleranceScreenOffIR", 1);
                        AddOrUpdateRegistryKey(powerRegistryPath, "LatencyToleranceVSyncEnabled", 1);
                        AddOrUpdateRegistryKey(powerRegistryPath, "RtlCapabilityCheckLatency", 1);

                        AddOrUpdateRegistryKey(graphicsDriversRegistryPath, "DefaultD3TransitionLatencyActivelyUsed", 1);
                        AddOrUpdateRegistryKey(graphicsDriversRegistryPath, "DefaultD3TransitionLatencyIdleLongTime", 1);
                        AddOrUpdateRegistryKey(graphicsDriversRegistryPath, "DefaultD3TransitionLatencyIdleMonitorOff", 1);
                        AddOrUpdateRegistryKey(graphicsDriversRegistryPath, "DefaultD3TransitionLatencyIdleNoContext", 1);
                        AddOrUpdateRegistryKey(graphicsDriversRegistryPath, "DefaultD3TransitionLatencyIdleShortTime", 1);
                        AddOrUpdateRegistryKey(graphicsDriversRegistryPath, "DefaultD3TransitionLatencyIdleVeryLongTime", 1);
                        AddOrUpdateRegistryKey(graphicsDriversRegistryPath, "DefaultLatencyToleranceIdle0", 1);
                        AddOrUpdateRegistryKey(graphicsDriversRegistryPath, "DefaultLatencyToleranceIdle0MonitorOff", 1);
                        AddOrUpdateRegistryKey(graphicsDriversRegistryPath, "DefaultLatencyToleranceIdle1", 1);
                        AddOrUpdateRegistryKey(graphicsDriversRegistryPath, "DefaultLatencyToleranceIdle1MonitorOff", 1);
                        AddOrUpdateRegistryKey(graphicsDriversRegistryPath, "DefaultLatencyToleranceMemory", 1);
                        AddOrUpdateRegistryKey(graphicsDriversRegistryPath, "DefaultLatencyToleranceNoContext", 1);
                        AddOrUpdateRegistryKey(graphicsDriversRegistryPath, "DefaultLatencyToleranceNoContextMonitorOff", 1);
                        AddOrUpdateRegistryKey(graphicsDriversRegistryPath, "DefaultLatencyToleranceOther", 1);
                        AddOrUpdateRegistryKey(graphicsDriversRegistryPath, "DefaultLatencyToleranceTimerPeriod", 1);
                        AddOrUpdateRegistryKey(graphicsDriversRegistryPath, "DefaultMemoryRefreshLatencyToleranceActivelyUsed", 1);
                        AddOrUpdateRegistryKey(graphicsDriversRegistryPath, "DefaultMemoryRefreshLatencyToleranceMonitorOff", 1);
                        AddOrUpdateRegistryKey(graphicsDriversRegistryPath, "DefaultMemoryRefreshLatencyToleranceNoContext", 1);
                        AddOrUpdateRegistryKey(graphicsDriversRegistryPath, "Latency", 1);
                        AddOrUpdateRegistryKey(graphicsDriversRegistryPath, "MaxIAverageGraphicsLatencyInOneBucket", 1);
                        AddOrUpdateRegistryKey(graphicsDriversRegistryPath, "MiracastPerfTrackGraphicsLatency", 1);
                        AddOrUpdateRegistryKey(graphicsDriversRegistryPath, "MonitorLatencyTolerance", 1);
                        AddOrUpdateRegistryKey(graphicsDriversRegistryPath, "MonitorRefreshLatencyTolerance", 1);
                        AddOrUpdateRegistryKey(graphicsDriversRegistryPath, "TransitionLatency", 1);
                    }
                    else
                    {
                        // Remove keys for latency reduction
                        RemoveRegistryKey(powerRegistryPath, "ExitLatency");
                        RemoveRegistryKey(powerRegistryPath, "ExitLatencyCheckEnabled");
                        RemoveRegistryKey(powerRegistryPath, "Latency");
                        RemoveRegistryKey(powerRegistryPath, "LatencyToleranceDefault");
                        RemoveRegistryKey(powerRegistryPath, "LatencyToleranceFSVP");
                        RemoveRegistryKey(powerRegistryPath, "LatencyTolerancePerfOverride");
                        RemoveRegistryKey(powerRegistryPath, "LatencyToleranceScreenOffIR");
                        RemoveRegistryKey(powerRegistryPath, "LatencyToleranceVSyncEnabled");
                        RemoveRegistryKey(powerRegistryPath, "RtlCapabilityCheckLatency");

                        RemoveRegistryKey(graphicsDriversRegistryPath, "DefaultD3TransitionLatencyActivelyUsed");
                        RemoveRegistryKey(graphicsDriversRegistryPath, "DefaultD3TransitionLatencyIdleLongTime");
                        RemoveRegistryKey(graphicsDriversRegistryPath, "DefaultD3TransitionLatencyIdleMonitorOff");
                        RemoveRegistryKey(graphicsDriversRegistryPath, "DefaultD3TransitionLatencyIdleNoContext");
                        RemoveRegistryKey(graphicsDriversRegistryPath, "DefaultD3TransitionLatencyIdleShortTime");
                        RemoveRegistryKey(graphicsDriversRegistryPath, "DefaultD3TransitionLatencyIdleVeryLongTime");
                        RemoveRegistryKey(graphicsDriversRegistryPath, "DefaultLatencyToleranceIdle0");
                        RemoveRegistryKey(graphicsDriversRegistryPath, "DefaultLatencyToleranceIdle0MonitorOff");
                        RemoveRegistryKey(graphicsDriversRegistryPath, "DefaultLatencyToleranceIdle1");
                        RemoveRegistryKey(graphicsDriversRegistryPath, "DefaultLatencyToleranceIdle1MonitorOff");
                        RemoveRegistryKey(graphicsDriversRegistryPath, "DefaultLatencyToleranceMemory");
                        RemoveRegistryKey(graphicsDriversRegistryPath, "DefaultLatencyToleranceNoContext");
                        RemoveRegistryKey(graphicsDriversRegistryPath, "DefaultLatencyToleranceNoContextMonitorOff");
                        RemoveRegistryKey(graphicsDriversRegistryPath, "DefaultLatencyToleranceOther");
                        RemoveRegistryKey(graphicsDriversRegistryPath, "DefaultLatencyToleranceTimerPeriod");
                        RemoveRegistryKey(graphicsDriversRegistryPath, "DefaultMemoryRefreshLatencyToleranceActivelyUsed");
                        RemoveRegistryKey(graphicsDriversRegistryPath, "DefaultMemoryRefreshLatencyToleranceMonitorOff");
                        RemoveRegistryKey(graphicsDriversRegistryPath, "DefaultMemoryRefreshLatencyToleranceNoContext");
                        RemoveRegistryKey(graphicsDriversRegistryPath, "Latency");
                        RemoveRegistryKey(graphicsDriversRegistryPath, "MaxIAverageGraphicsLatencyInOneBucket");
                        RemoveRegistryKey(graphicsDriversRegistryPath, "MiracastPerfTrackGraphicsLatency");
                        RemoveRegistryKey(graphicsDriversRegistryPath, "MonitorLatencyTolerance");
                        RemoveRegistryKey(graphicsDriversRegistryPath, "MonitorRefreshLatencyTolerance");
                        RemoveRegistryKey(graphicsDriversRegistryPath, "TransitionLatency");
                    }

                    // Update the toggle switch state on the main thread
                    latencyToggleSwitch.Invoke((MethodInvoker)delegate
                    {
                        latencyToggleSwitch.IsOn = !isOn;
                    });

                    if (isOn || !isOn)  // Adjust the conditions based on specific needs
                    {
                        var result = messagesBoxImplementation.ShowMessage(
                            "Changes will take effect after a system restart. Do you want to restart now?",
                            "Restart Required",
                            MessageBoxButtons.YesNo);

                        if (result == DialogResult.Yes)
                        {
                            // Restart the system
                            Process.Start("shutdown.exe", "/r /t 0");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Show error message on the main thread
                    latencyToggleSwitch.Invoke((MethodInvoker)delegate
                    {
                        messagesBoxImplementation.ShowMessage($"An error occurred while toggling Windows Latency Reduction: {ex.Message}", "Error", MessageBoxButtons.OK);
                    });
                }
            });

            // Start the thread
            toggleThread.Start();
        }

        private static void AddOrUpdateRegistryKey(string registryPath, string keyName, object value)
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(registryPath, writable: true))
                {
                    if (key == null)
                    {
                        using (RegistryKey newKey = Registry.LocalMachine.CreateSubKey(registryPath))
                        {
                            newKey.SetValue(keyName, value);
                        }
                    }
                    else
                    {
                        key.SetValue(keyName, value);
                    }
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error adding/updating registry key {keyName}: {ex.Message}");
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
    }
}
