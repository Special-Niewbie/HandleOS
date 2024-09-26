using Microsoft.Win32;
using Console2Desk.ToggleSwitchDev;
using System.Diagnostics;

namespace Console2Desk.SettingsButton
{
    internal class CodeForMSGamebarMessages
    {
        public static void ToggleGamebarMessages(ControlToggles controlGameBarMessages, MessagesBoxImplementation messagesBoxImplementation)
        {
            // Esegui l'azione di toggle in un thread separato
            Thread toggleThread = new Thread(() =>
            {
                try
                {
                    // Controlla lo stato attuale del toggle switch
                    bool isOn = controlGameBarMessages.IsOn;

                    // Percorsi delle chiavi di registro
                    string msGamebarPath = @"ms-gamebar";
                    string msGamebarServicesPath = @"ms-gamebarservices";

                    if (isOn)
                    {
                        // Configurazione per abilitare i messaggi della Game Bar
                        Registry.SetValue(@"HKEY_CLASSES_ROOT\" + msGamebarPath, "", "URL:ms-gamebar");
                        using (RegistryKey gamebarKey = Registry.ClassesRoot.OpenSubKey(msGamebarPath, true))
                        {
                            if (gamebarKey != null)
                            {
                                gamebarKey.DeleteValue("NoOpenWith", false);
                            }
                        }
                        Registry.SetValue(@"HKEY_CLASSES_ROOT\" + msGamebarPath, "URL Protocol", "");

                        Registry.SetValue(@"HKEY_CLASSES_ROOT\" + msGamebarServicesPath, "", "URL:ms-gamebarservices");
                        using (RegistryKey gamebarServicesKey = Registry.ClassesRoot.OpenSubKey(msGamebarServicesPath, true))
                        {
                            if (gamebarServicesKey != null)
                            {
                                gamebarServicesKey.DeleteValue("NoOpenWith", false);
                            }
                        }

                        using (RegistryKey gamebarShellKey = Registry.ClassesRoot.OpenSubKey(msGamebarPath + @"\shell", true))
                        {
                            if (gamebarShellKey != null)
                            {
                                Registry.ClassesRoot.DeleteSubKeyTree(msGamebarPath + @"\shell");
                            }
                        }

                        using (RegistryKey gamebarServicesShellKey = Registry.ClassesRoot.OpenSubKey(msGamebarServicesPath + @"\shell", true))
                        {
                            if (gamebarServicesShellKey != null)
                            {
                                Registry.ClassesRoot.DeleteSubKeyTree(msGamebarServicesPath + @"\shell");
                            }
                        }
                    }
                    else
                    {
                        // Configurazione per disabilitare i messaggi della Game Bar
                        Registry.SetValue(@"HKEY_CLASSES_ROOT\" + msGamebarPath, "", "URL:ms-gamebar");
                        Registry.SetValue(@"HKEY_CLASSES_ROOT\" + msGamebarPath, "URL Protocol", " ");
                        Registry.SetValue(@"HKEY_CLASSES_ROOT\" + msGamebarPath, "NoOpenWith", " ");
                        Registry.SetValue(@"HKEY_CLASSES_ROOT\" + msGamebarPath + @"\shell\open\command", "", $"\"{Environment.GetEnvironmentVariable("SystemRoot")}\\System32\\systray.exe\"");

                        Registry.SetValue(@"HKEY_CLASSES_ROOT\" + msGamebarServicesPath, "", "URL:ms-gamebarservices");
                        Registry.SetValue(@"HKEY_CLASSES_ROOT\" + msGamebarServicesPath, "URL Protocol", " ");
                        Registry.SetValue(@"HKEY_CLASSES_ROOT\" + msGamebarServicesPath, "NoOpenWith", " ");
                        Registry.SetValue(@"HKEY_CLASSES_ROOT\" + msGamebarServicesPath + @"\shell\open\command", "", $"\"{Environment.GetEnvironmentVariable("SystemRoot")}\\System32\\systray.exe\"");

                        // Mostra un messaggio se il toggle è stato disabilitato
                        controlGameBarMessages.Invoke((MethodInvoker)delegate
                        {
                            controlGameBarMessages.IsOn = false;
                            messagesBoxImplementation.ShowMessage(
                                "Game Bar messages have been disabled. To re-enable them, change the state of the toggle switch.",
                                "Information",
                                MessageBoxButtons.OK);
                        });
                    }

                    // Aggiorna lo stato del toggle switch nel thread principale
                    controlGameBarMessages.Invoke((MethodInvoker)delegate
                    {
                        controlGameBarMessages.IsOn = !isOn;
                    });
                }
                catch (Exception ex)
                {
                    // Mostra un messaggio di errore nel thread principale
                    controlGameBarMessages.Invoke((MethodInvoker)delegate
                    {
                        messagesBoxImplementation.ShowMessage($"An error occurred while changing the Game Bar message status: {ex.Message}", "Error", MessageBoxButtons.OK);
                    });
                }
            });

            // Avvia il thread
            toggleThread.Start();
        }
    }
}
