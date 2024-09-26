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

namespace Console2Desk.BottomWindowButtons
{
    internal class ShareDisableTouchscreenClass
    {
        public static void DisableTouchscreen(PictureBox pictureBoxResetTouchKeyboard)
        {
            const string registryPath = @"Software\Microsoft\TabletTip\1.7";
            const string valueName = "TipbandDesiredVisibility";
            const string valueName2 = "TouchKeyboardTapInvoke";

            try
            {
                // Apri la chiave di registro o creala se non esiste
                using (var key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(registryPath))
                {
                    if (key != null)
                    {
                        // Controlla il valore corrente
                        object value = key.GetValue(valueName, valueName2);
                        if (value == null || (int)value == 2 || (int)value == 1)
                        {
                            // Imposta il valore a 0 per disabilitare il touchscreen
                            key.SetValue(valueName, 0, Microsoft.Win32.RegistryValueKind.DWord);
                            key.SetValue(valueName2, 0, Microsoft.Win32.RegistryValueKind.DWord);

                            // Attendere un secondo e verificare il cambiamento
                            System.Threading.Thread.Sleep(1000);
                            if ((int)key.GetValue(valueName, valueName2) == 0)
                            {
                                pictureBoxResetTouchKeyboard.Image = Properties.Resources.HandTouchKeyboard_Disabled;
                            }
                        }
                        else if ((int)value == 0)
                        {
                            // Il touchscreen è già disabilitato, abilitalo di nuovo
                            key.SetValue(valueName, 2, Microsoft.Win32.RegistryValueKind.DWord);
                            key.SetValue(valueName2, 2, Microsoft.Win32.RegistryValueKind.DWord);

                            // Attendere un secondo e verificare il cambiamento
                            System.Threading.Thread.Sleep(1000);
                            if ((int)key.GetValue(valueName, valueName2) == 2)
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
