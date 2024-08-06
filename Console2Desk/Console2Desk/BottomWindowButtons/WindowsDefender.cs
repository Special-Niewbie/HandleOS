/*
Console2Desk

Copyright (C) 2023 Special-Niewbie Softwares

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation version 3.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
*/

using System.Diagnostics;

namespace Console2Desk.BottomWindowButtons
{
    internal class WindowsDefender
    {
        public static void DisableRealTimeProtection(MessagesBoxImplementation messagesBoxImplementation)
        {
            try
            {
                // Create a new PowerShell process with administrator privileges
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "powershell.exe";
                psi.Arguments = "-ExecutionPolicy Bypass -NoProfile -Command \"& { Set-MpPreference -DisableRealtimeMonitoring $true }\"";
                psi.WindowStyle = ProcessWindowStyle.Hidden;
                psi.CreateNoWindow = true; // Don't show the PowerShell console window
                psi.UseShellExecute = false; // Do not use shell launcher to run

                Process process = new Process();
                process.StartInfo = psi;
                process.Start();
                process.WaitForExit(); // Attendere il completamento del processo
            }
            catch (Exception ex)
            {
                messagesBoxImplementation.ShowMessage($"An error occurred while disabling Real-Time Protection: {ex.Message}", "Error", MessageBoxButtons.OK);
                Cursor.Current = Cursors.Default;
            }
        }

        public static void EnableRealTimeProtection(MessagesBoxImplementation messagesBoxImplementation)
        {
            try
            {
                // Create a new PowerShell process with administrator privileges
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "powershell.exe";
                psi.Arguments = "-ExecutionPolicy Bypass -NoProfile -Command \"& { Set-MpPreference -DisableRealtimeMonitoring $false }\"";
                psi.WindowStyle = ProcessWindowStyle.Hidden;
                psi.CreateNoWindow = true; // Don't show the PowerShell console window
                psi.UseShellExecute = false; // Do not use shell launcher to run

                Process process = new Process();
                process.StartInfo = psi;
                process.Start();
                process.WaitForExit(); // Wait for the process to complete
            }
            catch (Exception ex)
            {
                messagesBoxImplementation.ShowMessage($"An error occurred while enabling Real-Time Protection: {ex.Message}", "Error", MessageBoxButtons.OK);
                Cursor.Current = Cursors.Default;
            }
        }

        public static void OpenWindowsDefender(MessagesBoxImplementation messagesBoxImplementation)
        {
            try
            {
                // Get the path to the Windows Defender executable
                string defenderPath = Environment.ExpandEnvironmentVariables(@"%ProgramFiles%\Windows Defender\MSASCui.exe");

                // Check if the executable exists
                if (File.Exists(defenderPath))
                {
                    // Launch the Windows Defender executable
                    Process.Start(defenderPath);
                }
                else
                {
                    // If the executable does not exist, open the Windows Defender threat settings
                    Process.Start("explorer.exe", "windowsdefender://Threatsettings");
                }
            }
            catch (Exception ex)
            {
                messagesBoxImplementation.ShowMessage($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK);
            }
        }
    }
}
