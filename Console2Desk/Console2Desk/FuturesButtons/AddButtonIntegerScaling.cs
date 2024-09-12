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
using System.Windows.Forms;

namespace Console2Desk.FuturesButtons
{
    internal class AddButtonIntegerScaling
    {
        public static void CodeForaddButtonIntegerScaling(PictureBox pictureBox3, MessagesBoxImplementation messagesBoxImplementation)
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
                            if (subKey != null && subKey.GetValue("DriverDesc")?.ToString() == "AMD Radeon Graphics")
                            {
                                if (subKey.GetSubKeyNames().Length > 0)
                                {
                                    subKeyName = name;
                                    break;
                                }
                            }
                        }
                    }
                    catch (UnauthorizedAccessException)
                    {
                        // Gestione dell'accesso non autorizzato
                    }
                    catch (Exception ex)
                    {
                        //messagesBoxImplementation.ShowMessage($"Error checking subkeys: {ex.Message}", "Error", MessageBoxButtons.OK);
                    }
                }

                if (subKeyName == null)
                {
                    messagesBoxImplementation.ShowMessage("It looks like you don't have an AMD GPU in your system.", "Error", MessageBoxButtons.OK);
                    return;
                }

                bool keyToggled = false;
                string[] registryKeys = new string[] {
                    $@"SYSTEM\CurrentControlSet\Control\Class\{{4d36e968-e325-11ce-bfc1-08002be10318}}\{subKeyName}"
                };

                // Per ciascun percorso della chiave di registro
                foreach (string keyPath in registryKeys)
                {
                    try
                    {
                        // Apre la chiave di registro con accesso in scrittura
                        using (RegistryKey key = Registry.LocalMachine.OpenSubKey(keyPath, true))
                        {
                            if (key != null)
                            {
                                object currentValue = key.GetValue("DalEmbeddedIntegerScalingSupport");
                                int newValue = 1; // Valore predefinito se non esiste o è diverso da 1

                                // Controlla il valore attuale e alterna tra 0 e 1
                                if (currentValue != null && (int)currentValue == 1)
                                {
                                    newValue = 0;
                                }

                                // Imposta il nuovo valore
                                key.SetValue("DalEmbeddedIntegerScalingSupport", newValue, RegistryValueKind.DWord);
                                keyToggled = true;

                                // Mostra un messaggio a seconda del nuovo valore impostato
                                string message = (newValue == 1)
                                    ? "Integer scaling enabled (value set to 1)."
                                    : "Integer scaling disabled (value set to 0).";
                                messagesBoxImplementation.ShowMessage(message, "Success", MessageBoxButtons.OK);
                            }
                        }
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        //messagesBoxImplementation.ShowMessage($"Access denied to registry key: {ex.Message}", "Error", MessageBoxButtons.OK);
                    }
                    catch (Exception ex)
                    {
                        //messagesBoxImplementation.ShowMessage($"Error accessing or modifying registry key: {ex.Message}", "Error", MessageBoxButtons.OK);
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
