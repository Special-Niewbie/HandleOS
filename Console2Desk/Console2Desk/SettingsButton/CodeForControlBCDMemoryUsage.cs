using Console2Desk.ToggleSwitchDev;
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

                    // Check if Paging File is enabled
                    bool isPagingFileEnabled = IsPagingFileEnabled();

                    if (isPagingFileEnabled)
                    {
                        // Notify the user that Paging File needs to be disabled
                        memoryToggleSwitch.Invoke((MethodInvoker)delegate
                        {
                            var result = messagesBoxImplementation.ShowMessage(
                                "Virtual RAM on disk enabled. Please disable Virtual RAM on Disk before proceeding. Would you like to disable it now?",
                                "Virtual RAM on Disk Enabled",
                                MessageBoxButtons.YesNo);

                            if (result == DialogResult.Yes)
                            {
                                // Disable Paging File
                                DisablePagingFile();
                                // Restart the system
                                Process.Start("shutdown.exe", "/r /t 0");
                            }
                        });
                        return; // Exit if Paging File is enabled
                    }

                    // Check Secure Boot status
                    bool secureBootEnabled = IsSecureBootEnabled();

                    if (secureBootEnabled)
                    {
                        // Notify the user that Secure Boot needs to be disabled
                        memoryToggleSwitch.Invoke((MethodInvoker)delegate
                        {
                            var result = messagesBoxImplementation.ShowMessage(
                                "Secure Boot is enabled. Please disable Secure Boot to apply changes.\n\n" +
                                "Before making changes to the BCD, it is recommended to read the guide on how to restore the BCD in case you encounter boot issues. If you are interested, press YES to open the guide (https://github.com/Special-Niewbie/HandleOS/blob/main/BCDFix.md) and learn the fix procedure. Otherwise, press NO to ignore this recommendation.\n\n" +
                                "Please be aware that using this procedure may affect online competitive games and could risk a ban. However, better to apply it with offline Games!",
                                "Secure Boot Enabled",
                                MessageBoxButtons.YesNo);

                            if (result == DialogResult.Yes)
                            {
                                // Open the guide URL in the default web browser
                                Process.Start(new ProcessStartInfo
                                {
                                    FileName = "https://github.com/Special-Niewbie/HandleOS/blob/main/BCDFix.md",
                                    UseShellExecute = true
                                });
                            }
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

        private static bool IsPagingFileEnabled()
        {
            try
            {
                string regOutput = ExecuteCmd("reg query \"HKLM\\SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Memory Management\" /v PagingFiles");

                if (string.IsNullOrEmpty(regOutput))
                {
                    return false; // Non c'è output, quindi la chiave potrebbe non esistere o non avere valore
                }

                // Cerchiamo la stringa "PagingFiles" e vediamo se c'è un valore associato
                string searchString = "PagingFiles    REG_MULTI_SZ    ";
                int index = regOutput.IndexOf(searchString);

                if (index != -1)
                {
                    // Cerchiamo se ci sono valori successivi alla stringa di ricerca
                    string valuePart = regOutput.Substring(index + searchString.Length).Trim();
                    return !string.IsNullOrEmpty(valuePart);
                }

                return false; // La stringa di ricerca non è stata trovata
            }
            catch (Exception ex)
            {
                // Log dell'errore o gestisci l'eccezione in modo appropriato
                return false;
            }
        }

        private static void DisablePagingFile()
        {
            ExecuteCmd("reg add \"HKLM\\SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Memory Management\" /v PagingFiles /t REG_MULTI_SZ /d \"\" /f");
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
            }
        }

        private static string ExecuteCmd(string command)
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

                    string output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();

                    return output;
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}

