using System.Diagnostics;

namespace Console2Desk.SettingsButton
{
    internal class msStoreButtonStartupCheck
    {
        public static void CheckMsStoreInstallation(Button msStoreButton, MessagesBoxImplementation messagesBoxImplementation)
        {
            // Run the check in a separate thread
            Thread checkThread = new Thread(() =>
            {
                try
                {
                    // Check if Microsoft.WindowsStore is installed
                    string checkCommand = "Get-AppxPackage -AllUsers Microsoft.WindowsStore*";

                    // Run PowerShell as administrator
                    ProcessStartInfo checkProcessInfo = new ProcessStartInfo
                    {
                        FileName = "powershell.exe",
                        Arguments = $"-Command \"{checkCommand}\"",
                        Verb = "runas", // Run as administrator
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    };

                    // Start the process and read the output
                    using (Process checkProcess = Process.Start(checkProcessInfo))
                    {
                        string output = checkProcess.StandardOutput.ReadToEnd();
                        checkProcess.WaitForExit();

                        // Determine if Microsoft Windows Store is installed
                        bool isInstalled = IsMicrosoftStoreInstalled(output);

                        // Update the button text on the main thread
                        msStoreButton.Invoke((MethodInvoker)delegate
                        {
                            if (isInstalled)
                            {
                                msStoreButton.Text = "    Uninstall Official \n     MS Store\n     (press to uninstall it)";
                            }
                            else
                            {
                                msStoreButton.Text = "    Reinstall Official \n     MS Store\n     (press to reinstall it)";
                            }
                        });
                    }
                }
                catch (Exception ex)
                {
                    // Show error message on the main thread
                    msStoreButton.Invoke((MethodInvoker)delegate
                    {
                        messagesBoxImplementation.ShowMessage($"An error occurred while checking MS Store installation: {ex.Message}", "Error", MessageBoxButtons.OK);
                    });
                }
            });

            // Start the thread
            checkThread.Start();
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

    }
}
