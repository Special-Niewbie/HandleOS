using Console2Desk.ToggleSwitchDev;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console2Desk.SettingsButton
{
    internal class CodeForMSGamebarMessagesCheck
    {
        public static void CheckGamebarMessagesState(ControlToggles controlGameBarMessages, MessagesBoxImplementation messagesBoxImplementation)
        {
            // Esegui il controllo in un thread separato
            Thread checkThread = new Thread(() =>
            {
                try
                {
                    // Path delle chiavi di registro
                    string msGamebarPath = @"ms-gamebar";
                    string msGamebarServicesPath = @"ms-gamebarservices";

                    bool isGamebarMessagesDisabled = false;

                    // Verifica se le chiavi esistono e se il valore è impostato correttamente per l'abilitazione
                    using (RegistryKey gamebarKey = Registry.ClassesRoot.OpenSubKey(msGamebarPath))
                    using (RegistryKey gamebarServicesKey = Registry.ClassesRoot.OpenSubKey(msGamebarServicesPath))
                    {
                        if (gamebarKey != null && gamebarServicesKey != null)
                        {
                            string gamebarValue = (string)gamebarKey.GetValue("", null);
                            string gamebarServicesValue = (string)gamebarServicesKey.GetValue("", null);

                            string gamebarProtocol = (string)gamebarKey.GetValue("URL Protocol", null);
                            string gamebarNoOpenWith = (string)gamebarKey.GetValue("NoOpenWith", null);

                            string gamebarServicesProtocol = (string)gamebarServicesKey.GetValue("URL Protocol", null);
                            string gamebarServicesNoOpenWith = (string)gamebarServicesKey.GetValue("NoOpenWith", null);

                            // Verifica se i valori sono impostati correttamente per considerare lo stato come OFF
                            isGamebarMessagesDisabled = gamebarValue == "URL:ms-gamebar" &&
                                                        gamebarProtocol == " " &&
                                                        gamebarNoOpenWith == " " &&
                                                        gamebarServicesValue == "URL:ms-gamebarservices" &&
                                                        gamebarServicesProtocol == " " &&
                                                        gamebarServicesNoOpenWith == " ";
                        }
                    }

                    // Aggiorna lo stato del toggle switch nel thread principale
                    controlGameBarMessages.Invoke((MethodInvoker)delegate
                    {
                        // Il toggle sarà ON se i messaggi della Game Bar sono abilitati, OFF se disabilitati
                        controlGameBarMessages.IsOn = !isGamebarMessagesDisabled;
                    });
                }
                catch (Exception ex)
                {
                    // Mostra un messaggio di errore nel thread principale
                    controlGameBarMessages.Invoke((MethodInvoker)delegate
                    {
                        messagesBoxImplementation.ShowMessage($"An error occurred while checking the Game Bar messages state: {ex.Message}", "Error", MessageBoxButtons.OK);
                    });

                    // Se si verifica un errore, impostare il toggle su ON come fallback
                    controlGameBarMessages.Invoke((MethodInvoker)delegate
                    {
                        controlGameBarMessages.IsOn = true;
                    });
                }
            });

            // Avvia il thread
            checkThread.Start();
        }
    }
}


