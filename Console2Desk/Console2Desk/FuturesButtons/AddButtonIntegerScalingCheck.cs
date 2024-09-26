using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console2Desk.FuturesButtons
{
    internal class AddButtonIntegerScalingCheck
    {
        public static void CodeForaddButtonIntegerScalingCheck(PictureBox pictureBox3, MessagesBoxImplementation messagesBoxImplementation)
        {
            try
            {
                string[] subKeyNames;
                // Ottieni tutti i nomi delle sottochiavi della chiave principale
                using (RegistryKey mainKey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}"))
                {
                    if (mainKey == null)
                    {
                        //messagesBoxImplementation.ShowMessage("Unable to access the registry key.", "Error", MessageBoxButtons.OK);
                        return;
                    }
                    subKeyNames = mainKey.GetSubKeyNames();
                }

                string subKeyName = null;
                // Trova la sottochiave corretta relativa alla AMD Radeon Graphics
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
                    catch (UnauthorizedAccessException)
                    {
                        // Do nothing
                    }
                    catch (Exception ex)
                    {
                        //messagesBoxImplementation.ShowMessage($"Error checking subkeys: {ex.Message}", "Error", MessageBoxButtons.OK);
                        // For Debug
                    }
                }

                if (subKeyName == null)
                {
                    //messagesBoxImplementation.ShowMessage("It looks like you don't have an AMD GPU in your system.", "Error", MessageBoxButtons.OK);
                    return;
                }

                string[] registryKeys = new string[] {
                    $@"SYSTEM\CurrentControlSet\Control\Class\{{4d36e968-e325-11ce-bfc1-08002be10318}}\{subKeyName}"
                };

                // Per ciascun percorso della chiave di registro
                foreach (string keyPath in registryKeys)
                {
                    try
                    {
                        // Apre la chiave di registro
                        using (RegistryKey key = Registry.LocalMachine.OpenSubKey(keyPath, false))
                        {
                            if (key != null)
                            {
                                // Controlla se esiste il valore della chiave
                                object currentValue = key.GetValue("DalEmbeddedIntegerScalingSupport");

                                // Se il valore è 1, rende visibile il pictureBox
                                if (currentValue != null && (int)currentValue == 1)
                                {
                                    pictureBox3.Visible = true;
                                }
                                else
                                {
                                    // Se il valore è diverso da 1 o non esiste, nasconde il pictureBox
                                    pictureBox3.Visible = false;
                                }
                            }
                        }
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        //messagesBoxImplementation.ShowMessage($"Access denied to registry key: {ex.Message}", "Error", MessageBoxButtons.OK);
                    }
                    catch (Exception ex)
                    {
                        messagesBoxImplementation.ShowMessage($"Error accessing registry key: {ex.Message}", "Error", MessageBoxButtons.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                messagesBoxImplementation.ShowMessage($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK);
            }
        }
    }
}
