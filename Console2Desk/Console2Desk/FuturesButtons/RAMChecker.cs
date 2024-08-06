using System.Diagnostics;

namespace Console2Desk.FuturesButtons
{
    internal class RAMChecker
    {
        private PictureBox pictureBoxCheckVRAM;

        public RAMChecker(PictureBox pictureBoxCheckVRAM)
        {
            this.pictureBoxCheckVRAM = pictureBoxCheckVRAM;
        }

        public async Task CheckVirtualMemorySettingsAsync(MessagesBoxImplementation messagesBoxImplementation)
        {
            try
            {
                string output = await ExecuteCmdAsync("Systeminfo | find \"Total Physical Memory\"");
                // MessageBox.Show($"Output: {output}", "Debug Info");

                string[] tokens = output.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (tokens.Length >= 5)
                {
                    string ramString = tokens[3] + tokens[4];
                    // MessageBox.Show($"RAM String: {ramString}", "Debug Info");

                    // Removing any non-numeric characters and converting to ulong
                    string ramNumericString = new string(ramString.Where(char.IsDigit).ToArray());
                    ulong RAM = ulong.Parse(ramNumericString);

                    // Calculating initial and maximum size in MB
                    ulong initialSize = RAM * 3 / 2;
                    ulong maximumSize = RAM * 3;

                    // MessageBox.Show($"Initial Size: {initialSize} MB, Maximum Size: {maximumSize} MB", "Debug Info");

                    string regOutput = await ExecuteCmdAsync("reg query \"HKLM\\SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Memory Management\" /v PagingFiles");
                    // MessageBox.Show($"Reg Output: {regOutput}", "Debug Info");

                    // Parsing the reg output to get the values
                    string[] regLines = regOutput.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string line in regLines)
                    {
                        if (line.Contains("PagingFiles"))
                        {
                            string[] regTokens = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                            if (regTokens.Length >= 5)
                            {
                                string pagingFilePath = regTokens[2]; // c:\pagefile.sys
                                ulong currentInitialSize = ulong.Parse(regTokens[3]);
                                ulong currentMaximumSize = ulong.Parse(regTokens[4]);

                                // MessageBox.Show($"Current Initial Size: {currentInitialSize} MB, Current Maximum Size: {currentMaximumSize} MB", "Debug Info");

                                // Comparing the actual sizes
                                bool initialSizeCorrect = currentInitialSize == initialSize;
                                bool maximumSizeCorrect = currentMaximumSize == maximumSize;

                                if (currentInitialSize == 16 && currentMaximumSize == 8192)
                                {
                                    pictureBoxCheckVRAM.Visible = true;
                                    pictureBoxCheckVRAM.Image = Properties.Resources.green_check_mark_icon_56x56;
                                }
                                else
                                {
                                    pictureBoxCheckVRAM.Visible = true;
                                    pictureBoxCheckVRAM.Image = Properties.Resources.WarningInfo;
                                }
                                return; // Exit the method after processing PagingFiles value
                            }
                            else
                            {
                                //MessageBox.Show($"Unexpected format in regTokens: {string.Join(", ", regTokens)}", "Debug Info");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                messagesBoxImplementation.ShowMessage($"An error occurred while checking virtual memory settings: {ex.Message}", "Error", MessageBoxButtons.OK);
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
    }
}
