using System.Diagnostics;

namespace Console2Desk.FuturesButtons
{
    internal class Code_for_IncreaseRAM_System
    {
        // Function to execute cmd.exe commands and return output
        private string ExecuteCmd(string command)
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
        }

        // Method to increase system RAM
        public void IncreaseRAM(MessagesBoxImplementation messagesBoxImplementation)
        {
            try
            {
                // Show warning message to the user
                DialogResult dialogResult = messagesBoxImplementation.ShowMessage(
                    "If you are using HandleOS with at least 16GB of RAM, this feature is not necessary. This feature is useful for HandleOS or Windows OS installations with limited RAM between 1GB to 4GB to rebalance system RAM usage. If you are in this scenario, press YES. Otherwise, press NO.",
                    "RAM Virtual Increase Warning",
                    MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.No)
                {
                    // User chose not to proceed
                    messagesBoxImplementation.ShowMessage("Operation cancelled.", "Cancelled", MessageBoxButtons.OK);
                    return;
                }

                // Get total physical memory using SystemInfo
                string output = ExecuteCmd("Systeminfo | find \"Total Physical Memory\"");

                // Parsing the output to get the total RAM
                string[] tokens = output.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (tokens.Length >= 5)
                {
                    string ramString = tokens[3] + tokens[4]; // Merge tokens to get RAM without spaces and commas
                    ulong RAM = ulong.Parse(ramString);

                    // InitialSize and maximumSize calculation
                    ulong initialSize = RAM * 3 / 2;
                    ulong maximumSize = RAM * 3;

                    // Sets the paging file size in the registry
                    string regCommand = $"Reg add \"HKLM\\SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Memory Management\" /v \"PagingFiles\" /t REG_MULTI_SZ /d \"c:\\pagefile.sys {initialSize} {maximumSize}\" /f";
                    ExecuteCmd(regCommand);

                    // Show a congratulatory message
                    double RAM_GB = RAM / 1024.0;
                    double maximumSize_GB = maximumSize / 1024.0;

                    messagesBoxImplementation.ShowMessage($"Congratulations! RAM available in the system has been increased from {RAM_GB:N2} GB to {maximumSize_GB:N2} GB.", "RAM Increased Successfully", MessageBoxButtons.OK);

                    // Ask the user if they want to restart the system
                    DialogResult restartDialogResult = messagesBoxImplementation.ShowMessage("Do you want to restart the system to apply the changes?", "Restart Required", MessageBoxButtons.YesNo);
                    if (restartDialogResult == DialogResult.Yes)
                    {
                        // Riavvia il sistema
                        ExecuteCmd("Shutdown -r -t 5 -c \"REBOOTING SYSTEM\"");
                    }
                }
                else
                {
                    messagesBoxImplementation.ShowMessage("Unable to retrieve installed RAM amount.", "Error", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                messagesBoxImplementation.ShowMessage($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK);
            }
        }
    }
}
