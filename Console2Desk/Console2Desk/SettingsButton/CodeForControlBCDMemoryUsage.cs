﻿using Console2Desk.ToggleSwitchDev;
using System.Diagnostics;

namespace Console2Desk.SettingsButton
{
    internal class CodeForControlBCDMemoryUsage
    {
        private static string backupDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Console2Desk", "BackupBCD");

        public static void ToggleBCDMemoryUsage(ControlToggles memoryToggleSwitch, MessagesBoxImplementation messagesBoxImplementation)
        {
            // Run the toggle action in a separate thread
            Thread toggleThread = new Thread(() =>
            {
                try
                {
                    bool isOn = memoryToggleSwitch.IsOn;

                    // Check Secure Boot status
                    bool secureBootEnabled = IsSecureBootEnabled();

                    if (secureBootEnabled)
                    {
                        // Notify the user that Secure Boot needs to be disabled
                        memoryToggleSwitch.Invoke((MethodInvoker)delegate
                        {
                            messagesBoxImplementation.ShowMessage(
                                "Secure Boot is enabled. Please disable Secure Boot to apply changes.",
                                "Secure Boot Enabled",
                                MessageBoxButtons.OK);
                        });
                        return; // Exit if Secure Boot is enabled
                    }

                    // Backup BCD if turning ON and backup does not already exist
                    if (isOn && !Directory.Exists(backupDirectory))
                    {
                        Directory.CreateDirectory(backupDirectory);

                        // Backup BCD
                        ExecuteCommand("bcdedit /enum all > \"" + Path.Combine(backupDirectory, "bcdoutput.txt") + "\"");
                        ExecuteCommand("bcdedit /export \"" + Path.Combine(backupDirectory, "bcdbackup.bin") + "\"");
                    }

                    // Apply BCD settings
                    if (isOn)
                    {
                        ExecuteCommand("bcdedit /set firstmegabytepolicy UseAll");
                        ExecuteCommand("bcdedit /set avoidlowmemory 0x8000000");
                        ExecuteCommand("bcdedit /set nolowmem Yes");
                    }
                    else
                    {
                        ExecuteCommand("bcdedit /deletevalue firstmegabytepolicy");
                        ExecuteCommand("bcdedit /deletevalue avoidlowmemory");
                        ExecuteCommand("bcdedit /deletevalue nolowmem");
                    }

                    // Update the toggle switch state on the main thread
                    memoryToggleSwitch.Invoke((MethodInvoker)delegate
                    {
                        memoryToggleSwitch.IsOn = !isOn;
                    });

                    // Show warning message for restart if needed
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
                    memoryToggleSwitch.Invoke((MethodInvoker)delegate
                    {
                        messagesBoxImplementation.ShowMessage($"An error occurred while toggling BCD Memory Usage: {ex.Message}", "Error", MessageBoxButtons.OK);
                    });
                }
            });

            // Start the thread
            toggleThread.Start();
        }

        private static bool IsSecureBootEnabled()
        {
            try
            {
                using (var process = new Process())
                {
                    process.StartInfo.FileName = "powershell";
                    process.StartInfo.Arguments = "-Command \"Confirm-SecureBootUEFI\"";
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;
                    process.Start();

                    string output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();

                    // Check if output contains "True"
                    return output.Trim().Equals("True", StringComparison.OrdinalIgnoreCase);
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error checking Secure Boot status: {ex.Message}");
                return false;
            }
        }

        private static void ExecuteCommand(string command)
        {
            try
            {
                using (var process = new Process())
                {
                    process.StartInfo.FileName = "cmd.exe";
                    process.StartInfo.Arguments = $"/C {command}";
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;
                    process.Start();
                    process.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error executing command: {ex.Message}");
            }
        }
    }
}