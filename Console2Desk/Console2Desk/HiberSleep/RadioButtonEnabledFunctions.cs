using Microsoft.Win32;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Console2Desk.HiberSleep
{
    internal class RadioButtonEnabledFunctions
    {
        private const string HostGuardianFeature = "HostGuardian";
        private const string BackupFileExtension = ".pow";

        public bool ProcessHibernationSetup()
        {
            var guardianStatus = GetHostGuardianStatus();
            if (guardianStatus.IsEnabled)
            {
                return HandleEnabledHostGuardian();
            }

            if (IsRunningOnExternalDrive())
            {
                ShowExternalDriveWarning();
                return false;
            }

            BackupPowerPlans();
            EnableHibernateSettings();
            EnableHibernation();
            ShowSuccessAndPromptGuardianReactivation();

            return true;
        }

        private (bool IsEnabled, string State) GetHostGuardianStatus()
        {
            ProcessStartInfo processInfo = new ProcessStartInfo("powershell.exe")
            {
                Arguments = $"-Command \"(Get-WindowsOptionalFeature -Online -FeatureName '{HostGuardianFeature}').State\"",
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true
            };

            using (Process process = Process.Start(processInfo))
            {
                string output = process.StandardOutput.ReadToEnd().Trim();
                return (output.Equals("Enabled", StringComparison.OrdinalIgnoreCase), output);
            }
        }

        private bool HandleEnabledHostGuardian()
        {
            var result = DependencyContainer.MessagesBoxImplementation.ShowMessage(
                "Guarded Host is currently enabled. It needs to be disabled before proceeding with Hibernation/Sleep setup.\n\n" +
                "Please follow these steps:\n" +
                "1. Click OK and on Window Pop-up `Turn Windows features on or off`.\n" +
                "2. Disable 'Guarded Host'.\n" +
                "3. Restart your computer.\n" +
                "4. Run this application again to enable Hibernation/Sleep.",
                "Guarded Host Check",
                MessageBoxButtons.OKCancel);

            if (result == DialogResult.OK)
            {
                Process.Start("optionalfeatures");
                
                return false;
            }
            else
            {
                return false;
            }
        }

        private bool IsRunningOnExternalDrive()
        {
            try
            {
                using (var key = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control"))
                {
                    if (key == null) return false;

                    var value = key.GetValue("PortableOperatingSystem");
                    return value != null && Convert.ToInt32(value) == 1;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void ShowExternalDriveWarning()
        {
            DependencyContainer.MessagesBoxImplementation.ShowMessage(
                "Your Windows installation is detected on an external drive (USB/External SSD). " +
                "Microsoft Windows does not support Hibernation/Sleep mode when running from external drives " +
                "due to system limitations and boot compatibility issues.",
                "External Drive Detected",
                MessageBoxButtons.OK);
        }

        private void BackupPowerPlans()
        {
            string backupDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Console2Desk",
                "BackupPowerPlan"
            );

            if (!Directory.Exists(backupDir) ||
                !Directory.GetFiles(backupDir, $"*{BackupFileExtension}").Any())
            {
                Directory.CreateDirectory(backupDir);
                List<string> powerPlanGuids = GetPowerPlanGuids();
                foreach (string guid in powerPlanGuids)
                {
                    string backupFilePath = Path.Combine(backupDir, $"PowerPlan_{guid}{BackupFileExtension}");
                    ExecuteCommand($"powercfg /export \"{backupFilePath}\" {guid}");
                }

                File.WriteAllText(
                    Path.Combine(backupDir, "backup_log.txt"),
                    $"Backup completed on {DateTime.Now}\nPower plans saved: {powerPlanGuids.Count}"
                );
            }
        }

        private List<string> GetPowerPlanGuids()
        {
            List<string> guids = new List<string>();
            ProcessStartInfo processInfo = new ProcessStartInfo("powercfg", "/list")
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            using (Process process = Process.Start(processInfo))
            {
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                var guidRegex = new Regex(@"([a-fA-F0-9]{8}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{12})");
                var matches = guidRegex.Matches(output);

                foreach (Match match in matches)
                {
                    guids.Add(match.Groups[1].Value);
                }

                if (guids.Count == 0)
                {
                    throw new Exception("No power plans found");
                }
            }

            return guids;
        }

        private void EnableHibernateSettings()
        {
            SetRegistryValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\Power", "HiberBootEnabled", 1);
            SetRegistryValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Power", "HibernateEnabled", 1);
            SetRegistryValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\FlyoutMenuSettings", "ShowHibernateOption", 1);
            SetRegistryValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\FlyoutMenuSettings", "ShowSleepOption", 1);
        }

        private void EnableHibernation()
        {
            ExecuteCommand("powercfg /hibernate on");
        }

        private void ShowSuccessAndPromptGuardianReactivation()
        {
            DependencyContainer.MessagesBoxImplementation.ShowMessage(
                "Hibernation/Sleep has been successfully enabled.",
                "Setup Complete",
                MessageBoxButtons.OK);

            Process.Start("optionalfeatures");
            
        }

        private void ExecuteCommand(string command)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo("cmd.exe", "/c " + command)
            {
                CreateNoWindow = true,
                UseShellExecute = true, // Necessario per l'elevazione
                Verb = "runas", // Richiede privilegi di amministratore
                RedirectStandardOutput = false, // Non puoi redirigere l'output con UseShellExecute = true
                RedirectStandardError = false
            };

            try
            {
                using (Process process = Process.Start(processInfo))
                {
                    process.WaitForExit();
                    if (process.ExitCode != 0)
                    {
                        throw new Exception($"Command '{command}' failed with exit code {process.ExitCode}");
                    }
                }
            }
            catch (Win32Exception ex) when (ex.NativeErrorCode == 1223)
            {
                // L'utente ha annullato il prompt UAC
                throw new Exception("The operation was canceled by the user.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error executing command '{command}': {ex.Message}", ex);
            }
        }

        private void SetRegistryValue(string keyPath, string valueName, int value)
        {
            try
            {
                Registry.SetValue(keyPath, valueName, value, RegistryValueKind.DWord);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error modifying registry: {ex.Message}");
            }
        }
    }
}