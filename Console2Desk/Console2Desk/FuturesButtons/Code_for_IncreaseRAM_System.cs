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
                // Show the message box asking whether to apply VRAM+ or base VRAM settings
                DialogResult dialogResult = messagesBoxImplementation.ShowMessage(
                    "Do you want to apply VRAM+ settings for HandleOS?\n\n" +
                    "- Press YES to apply VRAM+ settings, which will increase the RAM to 3 times its current size, suitable for some AAA games.\n\n" +
                    "- Press NO to apply the basic VRAM settings, suitable for all scenarios.\n\n" +
                    "- Press CANCEL to do nothing.",
                    "Select VRAM Settings",
                    MessageBoxButtons.YesNoCancel);

                if (dialogResult == DialogResult.Cancel)
                {
                    // User chose to cancel the operation
                    messagesBoxImplementation.ShowMessage("Operation cancelled.", "Cancelled", MessageBoxButtons.OK);
                    return;
                }

                // Get total physical memory using PowerShell
                string output = ExecuteCmd("powershell -command \"(Get-WmiObject -Class Win32_ComputerSystem | Select-Object -ExpandProperty TotalPhysicalMemory) / 1MB\"");

                if (double.TryParse(output.Trim(), out double RAM))
                {
                    ulong initialSize, maximumSize;

                    if (dialogResult == DialogResult.Yes)
                    {
                        // Calculate initialSize and maximumSize for VRAM+
                        initialSize = (ulong)(RAM * 3 / 2);
                        maximumSize = (ulong)(RAM * 3);
                    }
                    else // DialogResult.No
                    {
                        // Use static values for VRAM base settings
                        initialSize = 16;
                        maximumSize = 8192;
                    }

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
                        // Restart the system
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
