using Console2Desk.ToggleSwitchDev;
using Microsoft.Win32;
using System.Diagnostics;

namespace Console2Desk.SettingsButton
{
    internal class CodeForControlCoreIsolation_ExploidCheck
    {
        public static void CheckCoreIsolationAndExploitProtection(ControlToggles protectionToggleSwitch, MessagesBoxImplementation messagesBoxImplementation)
        {
            // Run the check in a separate thread
            Thread checkThread = new Thread(() =>
            {
                try
                {
                    // Check Core Isolation value
                    int coreIsolationValue = GetCoreIsolationValue();
                    bool isCoreIsolationOn = coreIsolationValue == 1;

                    // Check Control Flow Guard value
                    bool isControlFlowGuardOn = GetControlFlowGuardValue();

                    // Update the toggle switch on the main thread
                    protectionToggleSwitch.Invoke((MethodInvoker)delegate
                    {
                        protectionToggleSwitch.IsOn = isCoreIsolationOn && isControlFlowGuardOn;
                    });
                }
                catch (Exception ex)
                {
                    // Show error message on the main thread
                    protectionToggleSwitch.Invoke((MethodInvoker)delegate
                    {
                        messagesBoxImplementation.ShowMessage($"An error occurred while checking Core Isolation and Exploit Protection: {ex.Message}", "Error", MessageBoxButtons.OK);
                    });
                }
            });

            // Start the thread
            checkThread.Start();
        }

        private static int GetCoreIsolationValue()
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\DeviceGuard\Scenarios\HypervisorEnforcedCodeIntegrity"))
                {
                    if (key != null)
                    {
                        object value = key.GetValue("Enabled");
                        if (value != null && value is int)
                        {
                            return (int)value;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error accessing Core Isolation registry key: {ex.Message}");
            }

            // Default to 0 if the key or value is not found
            return 0;
        }

        private static bool GetControlFlowGuardValue()
        {
            try
            {
                // Execute PowerShell command to get Control Flow Guard status
                string command = "Get-ProcessMitigation -System";
                string output = ExecutePowerShellScript(command);

                // Parse the output to determine if CFG is enabled
                return output.Contains("Enable                             : ON");
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error retrieving Control Flow Guard value: {ex.Message}");
                return false;
            }
        }

        private static string ExecutePowerShellScript(string script)
        {
            try
            {
                using (var process = new Process())
                {
                    process.StartInfo.FileName = "powershell.exe";
                    process.StartInfo.Arguments = $"-NoProfile -ExecutionPolicy Bypass -Command \"{script}\"";
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;
                    process.Start();
                    string output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();
                    return output;
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error executing PowerShell script: {ex.Message}");
                return string.Empty;
            }
        }
    }
}
