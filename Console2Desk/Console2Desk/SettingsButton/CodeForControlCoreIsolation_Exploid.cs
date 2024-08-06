using Console2Desk.ToggleSwitchDev;
using Microsoft.Win32;
using System.Diagnostics;


namespace Console2Desk.SettingsButton
{
    internal class CodeForControlCoreIsolation_Exploid
    {
        public static void ToggleCoreIsolationAndExploitProtection(ControlToggles protectionToggleSwitch, MessagesBoxImplementation messagesBoxImplementation)
        {
            // Run the toggle action in a separate thread
            Thread toggleThread = new Thread(() =>
            {
                try
                {
                    bool isOn = protectionToggleSwitch.IsOn;

                    // Core Isolation settings
                    using (RegistryKey coreIsolationKey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\DeviceGuard\Scenarios\HypervisorEnforcedCodeIntegrity", writable: true))
                    {
                        if (coreIsolationKey == null)
                        {
                            throw new Exception("Unable to access the Core Isolation registry key.");
                        }
                        coreIsolationKey.SetValue("Enabled", isOn ? 1 : 0, RegistryValueKind.DWord);
                    }

                    // Control Flow Guard settings
                    string cfgCommand = isOn
                        ? "Set-ProcessMitigation -System -Enable CFG"
                        : "Set-ProcessMitigation -System -Disable CFG";
                    ExecutePowerShellScript(cfgCommand);

                    // Update the toggle switch state on the main thread
                    protectionToggleSwitch.Invoke((MethodInvoker)delegate
                    {
                        protectionToggleSwitch.IsOn = !isOn;
                    });

                    // Show restart message
                    DialogResult result = messagesBoxImplementation.ShowMessage(
                        "The changes will take effect after a system restart. Do you want to restart now?",
                        "Restart Required",
                        MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        // Restart the system
                        Process.Start("shutdown.exe", "/r /t 0");
                    }

                    // Show detailed explanation message if turning off
                    if (!isOn)
                    {
                        messagesBoxImplementation.ShowMessage(
                            "- What is Core isolation Memory integrity?\n\n" +
                            "Core isolation provides added protection against malware and other attacks by isolating computer processes from your operating system and device. Select Core isolation details to enable, disable, and change the settings for core isolation features.\n" +
                            "Memory integrity is a feature of core isolation. By turning on the Memory integrity setting, you can help prevent malicious code from accessing high-security processes in the event of an attack.\n\n" +
                            "- What is Control Flow Guard?\n\n" +
                            "Control Flow Guard (CFG) is a highly-optimized platform security feature that was created to combat memory corruption vulnerabilities. By placing tight restrictions on where an application can execute code from, it makes it much harder for exploits to execute arbitrary code through vulnerabilities such as buffer overflows. CFG extends previous exploit mitigation technologies such as /GS, DEP, and ASLR.\n\n" +
                            "- Prevent memory corruption and ransomware attacks.\n" +
                            "- Restrict the capabilities of the server to whatever is needed at a particular point in time to reduce attack surface.\n" +
                            "- Make it harder to exploit arbitrary code through vulnerabilities such as buffer overflows.",
                            "Explanation",
                            MessageBoxButtons.OK);
                    }
                }
                catch (Exception ex)
                {
                    // Show error message on the main thread
                    protectionToggleSwitch.Invoke((MethodInvoker)delegate
                    {
                        messagesBoxImplementation.ShowMessage($"An error occurred while toggling Core Isolation and Exploit Protection: {ex.Message}", "Error", MessageBoxButtons.OK);
                    });
                }
            });

            // Start the thread
            toggleThread.Start();
        }

        private static void ExecutePowerShellScript(string script)
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
                    process.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error executing PowerShell script: {ex.Message}");
            }
        }
    }
}
