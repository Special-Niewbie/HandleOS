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

using Console2Desk.ToggleSwitchDev;
using System.Diagnostics;

namespace Console2Desk.FormWifi
{
    internal class CodeForWiFiOnOffCheck
    {
        public static bool IsWiFiOn(ControlToggles controlWiFi, MessagesBoxImplementation messagesBoxImplementation)
        {
            try
            {
                // Create process to run netsh command
                Process process = new Process();
                process.StartInfo.FileName = "netsh";
                process.StartInfo.Arguments = "wlan show interfaces";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;

                // Start the process
                process.Start();

                // Read the command output
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                // Check if the output contains WiFi status information
                if (output.Contains("WiFi"))
                {
                    // Look for "Radio status" in the output
                    StringReader reader = new StringReader(output);
                    string line;
                    bool softwareOn = false;
                    bool radioStatusChecked = false;

                    while ((line = reader.ReadLine()) != null)
                    {
                        // Check first "Radio status"
                        if (line.Trim().StartsWith("Radio status"))
                        {
                            // Check if the next value is "Software On" or "Software Off"
                            string softwareStatus = reader.ReadLine()?.Trim();
                            if (softwareStatus != null && softwareStatus.Contains("Software On"))
                            {
                                softwareOn = true;
                            }
                            else if (softwareStatus != null && softwareStatus.Contains("Software Off"))
                            {
                                softwareOn = false;
                            }

                            radioStatusChecked = true; // Indicates that we have checked the WiFi status
                            break;
                        }
                    }

                    // If we didn't find "Radio status", we search for "State"
                    if (!radioStatusChecked)
                    {
                        reader = new StringReader(output); // Reset the reader to search for the state
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.Trim().StartsWith("State") && line.Contains("connected"))
                            {
                                softwareOn = true; // We consider WiFi to be on if it is connected
                                break;
                            }
                        }
                    }

                    // Set the toggle state
                    controlWiFi.IsOn = softwareOn;
                    return softwareOn;
                }
                else
                {
                    messagesBoxImplementation.ShowMessage("No WiFi interfaces found.", "info", MessageBoxButtons.OK);
                    return false;
                }
            }
            catch (Exception ex)
            {
                messagesBoxImplementation.ShowMessage("Error checking WiFi state: " + ex.Message, "error", MessageBoxButtons.OK);
                return false;
            }
        }
    }
}
