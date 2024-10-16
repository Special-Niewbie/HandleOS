using Console2Desk.ToggleSwitchDev;
using Microsoft.Win32;

namespace Console2Desk.SettingsButton
{
    internal class HdwAcceleratedGPUScheduling
    {
        public static void ToggleHwAcceleratedGPUScheduling(ControlToggles controlHWAccelleration, MessagesBoxImplementation messagesBoxImplementation)
        {
            // Esegui l'azione di toggle in un thread separato
            Thread toggleThread = new Thread(() =>
            {
                try
                {
                    // Controlla lo stato attuale del toggle switch
                    bool isOn = controlHWAccelleration.IsOn;

                    string keyPath = @"SYSTEM\CurrentControlSet\Control\GraphicsDrivers";
                    string valueName = "HwSchMode";

                    using (RegistryKey key = Registry.LocalMachine.OpenSubKey(keyPath, true))
                    {
                        if (key == null)
                        {
                            throw new Exception("Unable to access the registry key.");
                        }

                        object currentValue = key.GetValue(valueName);

                        if (currentValue == null || (int)currentValue == 1)
                        {
                            // Se il valore non esiste o è 1, lo impostiamo a 2 (ON)
                            key.SetValue(valueName, 2, RegistryValueKind.DWord);
                            isOn = true;
                        }
                        else if ((int)currentValue == 2)
                        {
                            // Se il valore è 2, lo impostiamo a 1 (OFF)
                            key.SetValue(valueName, 1, RegistryValueKind.DWord);
                            isOn = false;
                        }
                    }

                    // Aggiorna l'UI nel thread principale
                    controlHWAccelleration.Invoke((MethodInvoker)delegate
                    {
                        controlHWAccelleration.IsOn = isOn;
                        /*string message = isOn
                            ? "Hardware Accelerated GPU Scheduling has been enabled successfully."
                            : "Hardware Accelerated GPU Scheduling has been disabled successfully.";
                        messagesBoxImplementation.ShowMessage(message, "Information", MessageBoxButtons.OK);*/
                    });
                }
                catch (Exception ex)
                {
                    // Mostra un messaggio di errore nel thread principale
                    controlHWAccelleration.Invoke((MethodInvoker)delegate
                    {
                        controlHWAccelleration.IsOn = false;
                        messagesBoxImplementation.ShowMessage($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK);
                    });
                }
            });

            // Avvia il thread
            toggleThread.Start();
        }
    }
}
