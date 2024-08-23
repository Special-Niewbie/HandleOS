using System.Diagnostics;

namespace Console2Desk.FuturesButtons
{
    internal class RAMChecker
    {
        private PictureBox pictureBoxCheckVRAM;
        private PictureBox pictureBoxVramPlus;

        public RAMChecker(PictureBox pictureBoxCheckVRAM, PictureBox pictureBoxVramPlus)
        {
            this.pictureBoxCheckVRAM = pictureBoxCheckVRAM;
            this.pictureBoxVramPlus = pictureBoxVramPlus;
        }

        public async Task CheckVirtualMemorySettingsAsync(MessagesBoxImplementation messagesBoxImplementations)
        {
            try
            {
                // Get the total physical memory using PowerShell
                string output = await ExecutePowerShellCommandAsync("Get-WmiObject -Class Win32_ComputerSystem | Select-Object -ExpandProperty TotalPhysicalMemory");
                string ramNumericString = new string(output.Where(char.IsDigit).ToArray());
                ulong RAM = ulong.Parse(ramNumericString);

                // Calculate initial and maximum size in MB
                ulong initialSize = RAM * 3 / 2 / 1024 / 1024;
                ulong maximumSize = RAM * 3 / 1024 / 1024;

                // Get the current paging file settings
                string regOutput = await ExecuteCmdAsync("reg query \"HKLM\\SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Memory Management\" /v PagingFiles");
                string[] regLines = regOutput.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string line in regLines)
                {
                    if (line.Contains("PagingFiles"))
                    {
                        string[] regTokens = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        if (regTokens.Length >= 5)
                        {
                            ulong currentInitialSize = ulong.Parse(regTokens[3]);
                            ulong currentMaximumSize = ulong.Parse(regTokens[4]);

                            // Set icons based on VRAM settings
                            if (currentInitialSize == 16 && currentMaximumSize == 8192)
                            {
                                pictureBoxCheckVRAM.Visible = true;
                                pictureBoxCheckVRAM.Image = Properties.Resources.green_check_mark_icon_56x56;
                            }
                            else if (currentInitialSize == initialSize && currentMaximumSize == maximumSize)
                            {
                                pictureBoxVramPlus.Visible = true;
                                pictureBoxVramPlus.Image = Properties.Resources.vramPlus;
                            }
                            else
                            {
                                pictureBoxCheckVRAM.Visible = true;
                                pictureBoxCheckVRAM.Image = Properties.Resources.WarningInfo;
                            }
                            return;
                        }
                    }
                }

                // Handle cases where the paging file settings could not be found
                pictureBoxCheckVRAM.Visible = true;
                pictureBoxCheckVRAM.Image = Properties.Resources.WarningInfo;
            }
            catch (Exception ex)
            {
                messagesBoxImplementations.ShowMessage($"An error occurred while checking virtual memory settings: {ex.Message}", "Error", MessageBoxButtons.OK);
            }
        }

        private async Task<string> ExecuteCmdAsync(string command)
        {
            try
            {
                return await Task.Run(() =>
                {
                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = $"/c {command}",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                    using (Process process = Process.Start(psi))
                    {
                        using (StreamReader reader = process.StandardOutput)
                        {
                            return reader.ReadToEnd();
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                DependencyContainer.MessagesBoxImplementation.ShowMessage($"An error occurred while executing command '{command}': {ex.Message}", "Error", MessageBoxButtons.OK);
                return string.Empty;
            }
        }

        private async Task<string> ExecutePowerShellCommandAsync(string command)
        {
            try
            {
                return await Task.Run(() =>
                {
                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = "powershell.exe",
                        Arguments = $"-Command \"{command}\"",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                    using (Process process = Process.Start(psi))
                    {
                        using (StreamReader reader = process.StandardOutput)
                        {
                            return reader.ReadToEnd();
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                DependencyContainer.MessagesBoxImplementation.ShowMessage($"An error occurred while executing PowerShell command '{command}': {ex.Message}", "Error", MessageBoxButtons.OK);
                return string.Empty;
            }
        }
    }
}
