using System.Diagnostics;

namespace Console2Desk.SettingsButton
{
    internal class msStoreButtonCoding
    {
        public static void ExecuteMsStoreCommand(Button msStoreButton, MessagesBoxImplementation messagesBoxImplementation)
        {
            try
            {
                // Command to check if Microsoft.WindowsStore is installed
                string checkCommand = "Get-AppxPackage -AllUsers Microsoft.WindowsStore*";

                // Configure PowerShell process
                ProcessStartInfo checkProcessInfo = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = $"-Command \"{checkCommand}\"",
                    Verb = "runas",  // Run PowerShell with administrative privileges
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                };

                // Start the process and read the output
                using (Process checkProcess = Process.Start(checkProcessInfo))
                {
                    if (checkProcess == null)
                    {
                        messagesBoxImplementation.ShowMessage("Failed to start PowerShell process.", "Error", MessageBoxButtons.OK);
                        return;
                    }

                    string output = checkProcess.StandardOutput.ReadToEnd();
                    checkProcess.WaitForExit();

                    // Determine action based on whether Microsoft.WindowsStore is installed
                    bool isInstalled = IsMicrosoftStoreInstalled(output);
                    string actionMessage = isInstalled ? "uninstall" : "reinstall";
                    string confirmationMessage = $"Are you sure you want to {actionMessage} Microsoft Windows Store?";

                    // Ask for confirmation from the user
                    DialogResult confirmationResult = messagesBoxImplementation.ShowMessage(confirmationMessage, "Confirmation", MessageBoxButtons.YesNo);

                    if (confirmationResult == DialogResult.Yes)
                    {
                        // Perform uninstallation or reinstallation based on current state
                        if (isInstalled)
                        {
                            // Uninstall Microsoft.WindowsStore
                            string uninstallCommand = "Get-AppxPackage -AllUsers Microsoft.WindowsStore* | Remove-AppxPackage";

                            ExecutePowerShellCommand(uninstallCommand, "Microsoft Windows Store uninstalled successfully.", "Failed to uninstall Microsoft Windows Store.");
                        }
                        else
                        {
                            // Reinstall Microsoft.WindowsStore
                            string reinstallScript = @"
                                Get-AppxPackage -AllUsers Microsoft.WindowsStore* | Foreach {Add-AppxPackage -DisableDevelopmentMode -Register ""$($_.InstallLocation)\AppXManifest.xml""}
                            ";
                            string scriptPath = Path.Combine(Path.GetTempPath(), "ReinstallWindowsStore.ps1");
                            File.WriteAllText(scriptPath, reinstallScript);

                            string command = $"-ExecutionPolicy Bypass -File \"{scriptPath}\"";
                            ExecutePowerShellCommand(command, "Microsoft Windows Store reinstalled successfully.", "Failed to reinstall Microsoft Windows Store.");

                            File.Delete(scriptPath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static bool IsMicrosoftStoreInstalled(string output)
        {
            // Search for the last occurrence of "PackageUserInformation :"
            int lastIndex = output.LastIndexOf("PackageUserInformation :");

            if (lastIndex >= 0)
            {
                // Extract the substring starting from lastIndex
                string substring = output.Substring(lastIndex);

                // Check if the substring contains "Installed}" or "Staged}"
                return substring.Contains("Installed}");
            }

            // Default to false if the substring is not found
            return false;
        }

        private static void ExecutePowerShellCommand(string command, string successMessage, string failureMessage)
        {
            try
            {
                ProcessStartInfo processInfo = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = command,
                    Verb = "runas",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using (Process process = Process.Start(processInfo))
                {
                    if (process == null)
                    {
                        DependencyContainer.MessagesBoxImplementation.ShowMessage("Failed to start PowerShell process.", "Error", MessageBoxButtons.OK);
                        return;
                    }

                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    if (process.ExitCode == 0)
                    {
                        DependencyContainer.MessagesBoxImplementation.ShowMessage(successMessage, "Success", MessageBoxButtons.OK);
                    }
                    else
                    {
                        string errorMessage = $"{failureMessage}\n\nError details:\n{error}\n\nOutput:\n{output}";
                        DependencyContainer.MessagesBoxImplementation.ShowMessage(errorMessage, "Error", MessageBoxButtons.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                DependencyContainer.MessagesBoxImplementation.ShowMessage($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK);
            }
        }
    }
}
