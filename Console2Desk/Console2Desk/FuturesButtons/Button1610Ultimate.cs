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

namespace Console2Desk.FuturesButtons
{
    internal class Button1610Ultimate
    {
        public static void CodeFor1610UltimateButton(Form1 form, MessagesBoxImplementation messagesBoxImplementation)
        {
            string backupFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "RegistryBackup_16_10.reg");

            // Create the backup before making changes
            BackupRegistryKey(backupFilePath);

            try
            {
                // Gets all the names of the subkeys in the main registry key
                string[] subKeyNames;
                using (RegistryKey mainKey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}"))
                {
                    if (mainKey == null)
                    {
                        messagesBoxImplementation.ShowMessage("Unable to access the registry key.", "Error", MessageBoxButtons.OK);
                        return;
                    }
                    subKeyNames = mainKey.GetSubKeyNames();
                }

                string subKeyName = null;
                // Search for the correct subkey based on the specified conditions
                foreach (string name in subKeyNames)
                {
                    try
                    {
                        using (RegistryKey subKey = Registry.LocalMachine.OpenSubKey($@"SYSTEM\CurrentControlSet\Control\Class\{{4d36e968-e325-11ce-bfc1-08002be10318}}\{name}"))
                        {
                            if (subKey != null && subKey.GetValue("DriverDesc")?.ToString() == "AMD Radeon Graphics")
                            {
                                // Check if the key has any subkeys (indicates it is the main display key)
                                if (subKey.GetSubKeyNames().Length > 0)
                                {
                                    subKeyName = name;
                                    break;
                                }
                            }
                        }
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        // Log or handle the exception as needed
                        // Console.WriteLine($"Access denied to registry key: {name}. Error: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        // Handle other potential exceptions here
                        // Console.WriteLine($"Error accessing registry key: {name}. Error: {ex.Message}");
                    }
                }

                if (subKeyName == null)
                {
                    messagesBoxImplementation.ShowMessage("Unable to find the appropriate subkey.", "Error", MessageBoxButtons.OK);
                    return;
                }

                // Open or create registry key for editing
                try
                {
                    using (RegistryKey key = Registry.LocalMachine.CreateSubKey($@"SYSTEM\CurrentControlSet\Control\Class\{{4d36e968-e325-11ce-bfc1-08002be10318}}\{subKeyName}"))
                    {
                        if (key != null)
                        {
                            // Set the values of the required registry keys
                            key.SetValue("DALNonStandardModesBCD1", new byte[] {
                                0x19, 0x20, 0x12, 0x00, 0x17, 0x60, 0x10, 0x80,
                                0x16, 0x00, 0x10, 0x00, 0x14, 0x40, 0x09, 0x00,
                                0x12, 0x80, 0x08, 0x00, 0x11, 0x20, 0x07, 0x20,
                                0x10, 0x24, 0x06, 0x40, 0x09, 0x60, 0x07, 0x20,
                                0x08, 0x00, 0x05, 0x40, 0x07, 0x20, 0x05, 0x40,
                                0x06, 0x40, 0x04, 0x80
                            }, RegistryValueKind.Binary);

                            key.SetValue("DALRestrictedModesBCD1", new byte[] {
                                0x25, 0x60, 0x16, 0x00, 0x19, 0x20, 0x12, 0x00,
                                0x17, 0x60, 0x10, 0x80, 0x16, 0x00, 0x10, 0x00,
                                0x14, 0x40, 0x09, 0x00, 0x12, 0x80, 0x08, 0x00,
                                0x11, 0x20, 0x07, 0x20, 0x10, 0x24, 0x06, 0x40
                            }, RegistryValueKind.Binary);

                            key.SetValue("DALRestrictedModesBCD2", new byte[] {
                                0x19, 0x20, 0x12, 0x00, 0x17, 0x60, 0x10, 0x80,
                                0x16, 0x00, 0x10, 0x00, 0x14, 0x40, 0x09, 0x00,
                                0x12, 0x80, 0x08, 0x00, 0x11, 0x20, 0x07, 0x20,
                                0x10, 0x24, 0x06, 0x40, 0x09, 0x60, 0x07, 0x20
                            }, RegistryValueKind.Binary);

                            key.SetValue("DALRestrictedModesBCD3", new byte[] {
                                0x12, 0x80, 0x08, 0x00, 0x11, 0x20, 0x07, 0x20,
                                0x10, 0x24, 0x06, 0x40, 0x09, 0x60, 0x07, 0x20,
                                0x08, 0x00, 0x05, 0x40, 0x07, 0x20, 0x05, 0x40,
                                0x06, 0x40, 0x04, 0x80
                            }, RegistryValueKind.Binary);

                            key.SetValue("DALRestrictedModesBCD4", new byte[] {
                                0x12, 0x80, 0x08, 0x00, 0x11, 0x20, 0x07, 0x20,
                                0x10, 0x24, 0x06, 0x40, 0x09, 0x60, 0x07, 0x20,
                                0x08, 0x00, 0x05, 0x40, 0x07, 0x20, 0x05, 0x40,
                                0x06, 0x40, 0x04, 0x80
                            }, RegistryValueKind.Binary);

                            key.SetValue("DALRestrictedModesBCD5", new byte[] {
                                0x11, 0x20, 0x07, 0x20, 0x10, 0x24, 0x06, 0x40,
                                0x09, 0x60, 0x07, 0x20, 0x08, 0x00, 0x05, 0x40,
                                0x07, 0x20, 0x05, 0x40, 0x06, 0x40, 0x04, 0x80
                            }, RegistryValueKind.Binary);

                            messagesBoxImplementation.ShowMessage("Registry keys modified successfully. You may need to restart your computer to apply these changes.", "Success", MessageBoxButtons.OK);
                            form.CheckRegistrySettings();
                        }
                        else
                        {
                            messagesBoxImplementation.ShowMessage("Unable to create or access the registry key.", "Error", MessageBoxButtons.OK);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // MessageBox.Show("Error modifying registry keys: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                messagesBoxImplementation.ShowMessage("Error accessing registry: " + ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private static void BackupRegistryKey(string backupFilePath)
        {
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo
                {
                    FileName = "reg",
                    Arguments = $"export \"HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Control\\Class\\{{4d36e968-e325-11ce-bfc1-08002be10318}}\" \"{backupFilePath}\" /y",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = Process.Start(processStartInfo))
                {
                    process.WaitForExit();
                    if (process.ExitCode != 0)
                    {
                        DependencyContainer.MessagesBoxImplementation.ShowMessage("Failed to backup registry key.", "Error", MessageBoxButtons.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                DependencyContainer.MessagesBoxImplementation.ShowMessage("Error backing up registry: " + ex.Message, "Error", MessageBoxButtons.OK);
            }
        }
    }
}
