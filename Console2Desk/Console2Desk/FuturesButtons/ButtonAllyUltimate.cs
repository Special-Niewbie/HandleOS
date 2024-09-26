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
    internal class ButtonAllyUltimate
    {
        public static void CodeForallyUltimateButton(Form1 form, MessagesBoxImplementation messagesBoxImplementation) 
        {
            try
            {
                // Percorso del file di backup
                string backupFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Console2Desk");
                string backupFilePath = Path.Combine(backupFolderPath, "StockResolutionsDevice_Backup.reg");

                // Crea la cartella di destinazione se non esiste
                if (!Directory.Exists(backupFolderPath))
                {
                    Directory.CreateDirectory(backupFolderPath);
                }

                // Verifica se il file di backup esiste già
                if (!File.Exists(backupFilePath))
                {
                    // Esegui il backup della chiave di registro
                    BackupRegistryKey(backupFilePath);
                }

                // Ottiene tutti i nomi delle sottochiavi nella chiave di registro principale
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
                // Cerca la sottochiave corretta in base alle condizioni specificate
                foreach (string name in subKeyNames)
                {
                    try
                    {
                        using (RegistryKey subKey = Registry.LocalMachine.OpenSubKey($@"SYSTEM\CurrentControlSet\Control\Class\{{4d36e968-e325-11ce-bfc1-08002be10318}}\{name}"))
                        {
                            if (subKey != null)
                            {
                                string driverDesc = subKey.GetValue("DriverDesc")?.ToString();
                                if (driverDesc == "AMD Radeon Graphics" || driverDesc == "AMD Radeon 780M Graphics")
                                {
                                    if (subKey.GetSubKeyNames().Length > 0)
                                    {
                                        subKeyName = name;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        // Debug
                        //Console.WriteLine($"Access denied to registry key: {name}. Error: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        // Debug
                        //Console.WriteLine($"Error accessing registry key: {name}. Error: {ex.Message}");
                    }
                }

                if (subKeyName == null)
                {
                    messagesBoxImplementation.ShowMessage("Your AMD GPU drivers are missing. Please contact the developer to include support for your GPU/driver model.", "Error", MessageBoxButtons.OK);
                    return;
                }

                // Apre o crea la chiave di registro per la modifica
                try
                {
                    using (RegistryKey key = Registry.LocalMachine.CreateSubKey($@"SYSTEM\CurrentControlSet\Control\Class\{{4d36e968-e325-11ce-bfc1-08002be10318}}\{subKeyName}"))
                    {
                        if (key != null)
                        {
                            // Imposta i valori delle chiavi di registro richieste
                            key.SetValue("DALNonStandardModesBCD1", new byte[] {
                                0x19, 0x20, 0x10, 0x80, 0x17, 0x60, 0x09, 0x90,
                                0x16, 0x00, 0x09, 0x00, 0x13, 0x66, 0x07, 0x68,
                                0x12, 0x80, 0x07, 0x20, 0x11, 0x20, 0x06, 0x30,
                                0x09, 0x60, 0x05, 0x40, 0x16, 0x00, 0x09, 0x00,
                                0x13, 0x66, 0x07, 0x68, 0x12, 0x80, 0x07, 0x20,
                                0x10, 0x24, 0x05, 0x76, 0x09, 0x60, 0x05, 0x40,
                                0x08, 0x54, 0x04, 0x80, 0x06, 0x40, 0x03, 0x60
                            }, RegistryValueKind.Binary);

                            key.SetValue("DALRestrictedModesBCD1", new byte[] {
                                0x51, 0x20, 0x28, 0x80, 0x38, 0x40, 0x24, 0x00,
                                0x51, 0x20, 0x21, 0x60, 0x38, 0x40, 0x21, 0x60,
                                0x32, 0x00, 0x18, 0x00, 0x30, 0x72, 0x17, 0x28,
                                0x38, 0x40, 0x16, 0x20, 0x25, 0x60, 0x16, 0x00
                            }, RegistryValueKind.Binary);

                            key.SetValue("DALRestrictedModesBCD2", new byte[] {
                                0x20, 0x48, 0x15, 0x36, 0x25, 0x60, 0x14, 0x40,
                                0x19, 0x20, 0x12, 0x00, 0x16, 0x00, 0x12, 0x00,
                                0x16, 0x80, 0x10, 0x50, 0x12, 0x80, 0x10, 0x24,
                                0x16, 0x00, 0x10, 0x00, 0x12, 0x80, 0x08, 0x00
                            }, RegistryValueKind.Binary);

                            key.SetValue("DALRestrictedModesBCD3", new byte[] {
                                0x10, 0x24, 0x07, 0x68, 0x11, 0x28, 0x06, 0x34,
                                0x08, 0x00, 0x06, 0x00, 0x06, 0x40, 0x04, 0x80,
                                0x08, 0x54, 0x04, 0x80
                            }, RegistryValueKind.Binary);

                            key.SetValue("DALRestrictedModesBCD4", new byte[] {
                                0x10, 0x24, 0x07, 0x68, 0x11, 0x28, 0x06, 0x34,
                                0x08, 0x00, 0x06, 0x00, 0x06, 0x40, 0x04, 0x80,
                                0x08, 0x54, 0x04, 0x80
                            }, RegistryValueKind.Binary);

                            key.SetValue("DALRestrictedModesBCD5", new byte[] {
                                0x11, 0x20, 0x06, 0x30, 0x10, 0x24, 0x07, 0x68,
                                0x09, 0x60, 0x05, 0x40, 0x08, 0x54, 0x04, 0x80,
                                0x03, 0x20, 0x02, 0x40
                            }, RegistryValueKind.Binary);

                            messagesBoxImplementation.ShowMessage("GPU Drivers modified successfully. You may need to restart your computer to apply these changes.", "Success", MessageBoxButtons.OK);
                            form.CheckRegistrySettings();
                        }
                        else
                        {
                            messagesBoxImplementation.ShowMessage("Your AMD GPU drivers are missing. Please contact the developer to include support for your GPU/driver model.", "Error", MessageBoxButtons.OK);
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
                    if (process.ExitCode == 0)
                    {
                        //DependencyContainer.MessagesBoxImplementation.ShowMessage($"Registry backup created successfully at {backupFilePath}.", "Backup Success", MessageBoxButtons.OK);
                    }
                    else
                    {
                        DependencyContainer.MessagesBoxImplementation.ShowMessage("Failed to create registry backup.", "Backup Error", MessageBoxButtons.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                DependencyContainer.MessagesBoxImplementation.ShowMessage($"Error creating registry backup: {ex.Message}", "Backup Error", MessageBoxButtons.OK);
            }
        }
    }
}
