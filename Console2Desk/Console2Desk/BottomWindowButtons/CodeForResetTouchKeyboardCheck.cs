using Microsoft.Win32;

namespace Console2Desk.BottomWindowButtons
{
    internal static class CodeForResetTouchKeyboardCheck
    {
        private const string RegistryPath = @"Software\Microsoft\TabletTip\1.7";
        private const string ValueName = "TipbandDesiredVisibility";
        const string valueName2 = "TouchKeyboardTapInvoke";

        public static void CheckTouchKeyboardState(PictureBox pictureBoxResetTouchKeyboard, MessagesBoxImplementation messagesBoxImplementation)
        {
            try
            {
                using (var key = Registry.CurrentUser.OpenSubKey(RegistryPath))
                {
                    // Se la chiave non esiste o il valore è 2, mostra l'immagine di abilitazione
                    if (key == null || (int?)key.GetValue(ValueName, valueName2) == 2)
                    {
                        pictureBoxResetTouchKeyboard.Image = Properties.Resources.HandTouchKeyboard;
                    }
                    // Se il valore è 0, mostra l'immagine di disabilitazione
                    else if ((int)key.GetValue(ValueName, valueName2) == 0)
                    {
                        pictureBoxResetTouchKeyboard.Image = Properties.Resources.HandTouchKeyboard_Disabled;
                    }
                }
            }
            catch (Exception ex)
            {
                // messagesBoxImplementation.ShowMessage($"An error occurred while checking the registry: {ex.Message}", "Error", MessageBoxButtons.OK);
                return;
            }
        }
    }
}
