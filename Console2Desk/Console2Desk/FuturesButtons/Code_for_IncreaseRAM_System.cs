using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public void IncreaseRAM()
        {
            try
            {
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

                    MessageBox.Show($"Congratulations! RAM available in the system has been increased from {RAM_GB:N2} GB to {maximumSize_GB:N2} GB.", "RAM Increased Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Ask the user if they want to restart the system
                    DialogResult dialogResult = MessageBox.Show("Do you want to restart the system to apply the changes?", "Restart Required", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        // Riavvia il sistema
                        ExecuteCmd("Shutdown -r -t 5 -c \"REBOOTING SYSTEM\"");
                    }
                }
                else
                {
                    MessageBox.Show("Unable to retrieve installed RAM amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
