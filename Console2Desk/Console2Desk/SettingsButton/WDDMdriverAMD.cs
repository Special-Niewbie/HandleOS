using Console2Desk.ToggleSwitchDev;
using Microsoft.Win32;

namespace Console2Desk.SettingsButton
{
    internal class WDDMdriverAMD
    {
        public static void ToggleWDDMdriverAMD(ControlToggles controlWDDMamd, MessagesBoxImplementation messagesBoxImplementation)
        {
            // Esegui l'azione di toggle in un thread separato
            Thread toggleThread = new Thread(() =>
            {
                try
                {
                    // Controlla lo stato attuale del toggle switch
                    bool isOn = controlWDDMamd.IsOn;

                    string keyPath = @"SOFTWARE\Policies\Microsoft\Windows NT\Terminal Services";
                    string valueName = "fEnableWddmDriver";

                    using (RegistryKey key = Registry.LocalMachine.OpenSubKey(keyPath, true))
                    {
                        if (key == null)
                        {
                            throw new Exception("Unable to access the registry key.");
                        }

                        if (isOn)
                        {
                            // Se il toggle è su ON, impostiamo il valore a 1
                            key.SetValue(valueName, 1, RegistryValueKind.DWord);
                        }
                        else
                        {
                            // Se il toggle è su OFF, eliminiamo il valore della chiave
                            if (key.GetValue(valueName) != null)
                            {
                                key.DeleteValue(valueName);
                            }
                        }
                    }

                    // Aggiorna l'UI nel thread principale
                    controlWDDMamd.Invoke((MethodInvoker)delegate
                    {
                        controlWDDMamd.IsOn = isOn;
                    });
                }
                catch (Exception ex)
                {
                    // Mostra un messaggio di errore nel thread principale
                    controlWDDMamd.Invoke((MethodInvoker)delegate
                    {
                        controlWDDMamd.IsOn = false;
                        messagesBoxImplementation.ShowMessage($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK);
                    });
                }
            });

            // Avvia il thread
            toggleThread.Start();
        }
    }
}

