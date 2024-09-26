using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console2Desk.FuturesButtons
{
    internal class Button1610Stock
    {
        public static void CodeFor1610StockButton(Form1 form, MessagesBoxImplementation messagesBoxImplementation)
        {
            try
            {
                string[] subKeyNames;
                // Gets all the names of the subkeys in the main registry key
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
                // Gets all the names of the subkeys in the main registry key
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
                        // Log or handle the exception as needed
                    }
                    catch (Exception ex)
                    {
                        // Handle other potential exceptions here
                    }
                }

                if (subKeyName == null)
                {
                    messagesBoxImplementation.ShowMessage("Your AMD GPU drivers are missing. Please contact the developer to include support for your GPU/driver model.", "Error", MessageBoxButtons.OK);
                    return;
                }

                // Open or create registry key for editing
                try
                {
                    using (RegistryKey key = Registry.LocalMachine.CreateSubKey($@"SYSTEM\CurrentControlSet\Control\Class\{{4d36e968-e325-11ce-bfc1-08002be10318}}\{subKeyName}"))
                    {
                        if (key != null)
                        {
                            // Set the values of the required registry keys for 16:10 resolutions
                            key.SetValue("DALNonStandardModesBCD1", new byte[] {
                                // Example values for 2560x1600 resolution
                                0x19, 0x20, 0x10, 0x80, 0x00, 0x00, 0x00, 0x00,
                                0x16, 0x00, 0x09, 0x00, 0x00, 0x00, 0x00, 0x00,
                                0x14, 0x80, 0x07, 0x20, 0x00, 0x00, 0x00, 0x00
                            }, RegistryValueKind.Binary);

                            key.SetValue("DALRestrictedModesBCD1", new byte[] {
                                // Example values for 1920x1200 resolution
                                0x51, 0x20, 0x28, 0x80, 0x00, 0x00, 0x00, 0x00,
                                0x38, 0x40, 0x24, 0x00, 0x00, 0x00, 0x00, 0x00,
                                0x51, 0x20, 0x21, 0x60, 0x00, 0x00, 0x00, 0x00
                            }, RegistryValueKind.Binary);

                            key.SetValue("DALRestrictedModesBCD2", new byte[] {
                                // Example values for 1280x800 resolution
                                0x20, 0x48, 0x15, 0x36, 0x00, 0x00, 0x00, 0x00,
                                0x25, 0x60, 0x14, 0x40, 0x00, 0x00, 0x00, 0x00,
                                0x19, 0x20, 0x12, 0x00, 0x00, 0x00, 0x00, 0x00
                            }, RegistryValueKind.Binary);

                            key.SetValue("DALRestrictedModesBCD3", new byte[0], RegistryValueKind.Binary);
                            key.SetValue("DALRestrictedModesBCD4", new byte[0], RegistryValueKind.Binary);
                            key.SetValue("DALRestrictedModesBCD5", new byte[0], RegistryValueKind.Binary);

                            messagesBoxImplementation.ShowMessage("GPU Drivers modified successfully. You may need to restart your computer to apply these changes.", "Success", MessageBoxButtons.OK);
                            form.CheckRegistrySettings();
                        }
                        else
                        {
                            messagesBoxImplementation.ShowMessage("Your AMD GPU drivers are missing. Please contact the developer to include support for your GPU/driver model.", "Error", MessageBoxButtons.OK);
                        }
                    }
                }
                catch (UnauthorizedAccessException ex)
                {
                    // MessageBox.Show($"Access denied to registry key. Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    //messagesBoxImplementation.ShowMessage($"Error accessing or creating registry key. Error: {ex.Message}", "Error", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                messagesBoxImplementation.ShowMessage($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK);
            }
        }
    }
}
