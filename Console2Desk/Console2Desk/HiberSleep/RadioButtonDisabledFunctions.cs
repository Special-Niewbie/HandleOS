using Microsoft.Win32;
using System.ComponentModel;
using System.Diagnostics;

namespace Console2Desk.HiberSleep
{
    internal class RadioButtonDisabledFunctions
    {
        private const string HostGuardianFeature = "HostGuardian";
        private const string BackupFileExtension = ".pow";

        public bool ProcessHibernationRemoval()
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

            DisableHibernation();
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
                "Guarded Host is currently enabled. It needs to be disabled before proceeding with Hibernation/Sleep removal.\n\n" +
                "Please follow these steps:\n" +
                "1. Click OK and on Window Pop-up `Turn Windows features on or off`.\n" +
                "2. Disable 'Guarded Host'.\n" +
                "3. Restart your computer.\n" +
                "4. Run this application again to remove Hibernation/Sleep.",
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
                "No action is needed as Hibernation/Sleep mode is not supported on external drives.",
                "External Drive Detected",
                MessageBoxButtons.OK);
        }

        private void DisableHibernation()
        {
            try
            {
                // Disable hibernation through powercfg
                ExecuteCommand("powercfg /hibernate off");

                // Remove registry settings
                SetRegistryValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\Power", "HiberBootEnabled", 0);
                SetRegistryValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Power", "HibernateEnabled", 0);
                SetRegistryValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\FlyoutMenuSettings", "ShowHibernateOption", 0);
                SetRegistryValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\FlyoutMenuSettings", "ShowSleepOption", 0);

                // Delete hiberfil.sys if it exists
                string hiberFilePath = @"C:\hiberfil.sys";
                if (File.Exists(hiberFilePath))
                {
                    ExecuteCommand($"del /f /q \"{hiberFilePath}\"");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error disabling hibernation: {ex.Message}");
            }
        }

        private void ShowSuccessAndPromptGuardianReactivation()
        {
            DependencyContainer.MessagesBoxImplementation.ShowMessage(
                "Hibernation/Sleep has been successfully disabled.",
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
