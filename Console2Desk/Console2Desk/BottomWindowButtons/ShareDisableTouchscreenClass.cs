using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console2Desk.BottomWindowButtons
{
    internal class ShareDisableTouchscreenClass
    {
        public static void DisableTouchscreen(PictureBox pictureBoxResetTouchKeyboard)
        {
            const string registryPath = @"Software\Microsoft\TabletTip\1.7";
            const string valueName = "TipbandDesiredVisibility";

            try
            {
                // Apri la chiave di registro o creala se non esiste
                using (var key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(registryPath))
                {
                    if (key != null)
                    {
                        // Controlla il valore corrente
                        object value = key.GetValue(valueName);
                        if (value == null || (int)value == 2)
                        {
                            // Imposta il valore a 0 per disabilitare il touchscreen
                            key.SetValue(valueName, 0, Microsoft.Win32.RegistryValueKind.DWord);

                            // Attendere un secondo e verificare il cambiamento
                            System.Threading.Thread.Sleep(1000);
                            if ((int)key.GetValue(valueName) == 0)
                            {
                                pictureBoxResetTouchKeyboard.Image = Properties.Resources.HandTouchKeyboard_Disabled;
                            }
                        }
                        else if ((int)value == 0)
                        {
                            // Il touchscreen è già disabilitato, abilitalo di nuovo
                            key.SetValue(valueName, 2, Microsoft.Win32.RegistryValueKind.DWord);

                            // Attendere un secondo e verificare il cambiamento
                            System.Threading.Thread.Sleep(1000);
                            if ((int)key.GetValue(valueName) == 2)
                            {
                                pictureBoxResetTouchKeyboard.Image = Properties.Resources.HandTouchKeyboard; // Cambia l'immagine per lo stato abilitato
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DependencyContainer.MessagesBoxImplementation.ShowMessage($"An error occurred while modifying the registry: {ex.Message}", "Error", MessageBoxButtons.OK);
            }
        }
    }
}
