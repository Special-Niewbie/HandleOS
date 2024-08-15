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

using Microsoft.Win32;
using System.Diagnostics;

namespace Console2Desk.TouchButtons
{
    public class ConsoleButton
    {
        public static void CodeForconsoleButton1(MessagesBoxImplementation messagesBoxImplementation, Special_Niewbie_Button Button, string fullscreenAppPath, string defaultFullscreenSteamAppPath)
        {
            string settingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Console2Desk", "Settings.ini");
            string selectedPath = string.Empty;

            if (!File.Exists(settingsPath))
            {
                DialogResult result = messagesBoxImplementation.ShowMessage("Settings.ini not found. Do you want to apply default settings with Playnite?", "Settings Not Found", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    // Apply default settings with Playnite
                    selectedPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Playnite", "Playnite.FullscreenApp.exe --hidesplashscreen");
                    Directory.CreateDirectory(Path.GetDirectoryName(settingsPath));
                    File.WriteAllText(settingsPath, "Playnite\n" + selectedPath);
                }
                else
                {
                    // Open the ConsoleSettingsButton form to configure settings
                    using (ConsoleSettingsButton settingsForm = new ConsoleSettingsButton())
                    {
                        settingsForm.ShowDialog();
                    }

                    // Reload the settings if the file was created
                    if (File.Exists(settingsPath))
                    {
                        string[] settings = File.ReadAllLines(settingsPath);
                        string selectedApp = settings[0];
                        selectedPath = settings.Length > 1 ? settings[1] : string.Empty;

                        if (string.IsNullOrEmpty(selectedPath))
                        {
                            messagesBoxImplementation.ShowMessage("Invalid settings. Please configure the settings first.", "Error", MessageBoxButtons.OK);
                            return;
                        }
                    }
                    else
                    {
                        messagesBoxImplementation.ShowMessage("Settings not configured. Please configure the settings first.", "Error", MessageBoxButtons.OK);
                        return;
                    }
                }
            }
            else
            {
                // Read the settings from the file
                string[] settings = File.ReadAllLines(settingsPath);
                string selectedApp = settings[0];

                if (selectedApp == "Playnite")
                {
                    selectedPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Playnite", "Playnite.FullscreenApp.exe --hidesplashscreen");
                }
                else if (selectedApp == "Steam")
                {
                    selectedPath = @"C:\Program Files (x86)\Steam\steam.exe";
                }
                else if (selectedApp == "Custom" && settings.Length > 1)
                {
                    selectedPath = settings[1];
                }
                else
                {
                    messagesBoxImplementation.ShowMessage("Invalid settings. Please configure the settings first.", "Error", MessageBoxButtons.OK);
                    return;
                }
            }

            try
            {
                // Open the registry key for modification
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true))
                {
                    if (key != null)
                    {
                        // Set the value of the "Shell" key to the path of the selected application
                        key.SetValue("Shell", selectedPath);
                        AutoClosingMessageBox.Show("Console Mode applied successfully.", "Success", 500); // It closes automatically after 1 second
                    }
                    else
                    {
                        messagesBoxImplementation.ShowMessage("Registry key not found.", "Error", MessageBoxButtons.OK);
                        return; // Exit the method if the registry key was not found
                    }
                }
            }
            catch (Exception ex)
            {
                messagesBoxImplementation.ShowMessage("Error modifying registry key: " + ex.Message, "Error", MessageBoxButtons.OK);
                return; // Exit the method on error
            }

            // Kill the processes associated with explorer.exe
            try
            {
                Process[] processes = Process.GetProcessesByName("explorer");
                foreach (Process process in processes)
                {
                    process.Kill();
                }
            }
            catch (Exception ex)
            {
                messagesBoxImplementation.ShowMessage("Error killing explorer.exe process: " + ex.Message, "Error", MessageBoxButtons.OK);
                return; // Exit the method on error
            }

            // Check if the process is already running before starting it
            if (!IsProcessRunning(selectedPath))
            {
                // Start the selected application with normal user privileges
                try
                {
                    ProcessExtensions.StartProcessAsUser(selectedPath);
                }
                catch (Exception ex)
                {
                    // MessageBox.Show($"Error starting application: {selectedPath}\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Exit the method on error and to avoid doubling the MessageBox during the explorer.exe killing process
                }
            }
            else
            {
                // MessageBox.Show("The application is already running.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return; // To avoid doubling the MessageBox during the explorer.exe killing process
            }
        }

        private static bool IsProcessRunning(string processPath)
        {
            string processName = Path.GetFileNameWithoutExtension(processPath);
            return Process.GetProcessesByName(processName).Any();
        }
    }
}
