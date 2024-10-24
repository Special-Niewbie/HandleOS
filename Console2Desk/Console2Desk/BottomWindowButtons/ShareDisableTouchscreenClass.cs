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
            const string valueNameAutoInvoke = "EnableDesktopModeAutoInvoke";

            try
            {
                using (var key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(registryPath))
                {
                    if (key != null)
                    {
                        // Read values ​​from the register, using 0 as the default value if they do not exist
                        int intValue = (int)(key.GetValue(valueName, 0) ?? 0);
                        int intValue2 = (int)(key.GetValue(valueName2, 0) ?? 0);

                        /*MessageBox.Show($"Values ​​read from register (with default):\n" +
                                      $"{valueName}: {intValue}\n" +
                                      $"{valueName2}: {intValue2}",
                                      "Debug Info");*/

                        if (intValue == 2 || intValue == 1 || intValue2 == 2 || intValue2 == 1)
                        {
                            // Disabilita
                            key.SetValue(valueName, 0, Microsoft.Win32.RegistryValueKind.DWord);
                            key.SetValue(valueName2, 0, Microsoft.Win32.RegistryValueKind.DWord);
                            key.SetValue(valueNameAutoInvoke, 0, Microsoft.Win32.RegistryValueKind.DWord);

                            System.Threading.Thread.Sleep(1000);

                            if ((int)key.GetValue(valueName, 0) == 0 && (int)key.GetValue(valueName2, 0) == 0)
                            {
                                pictureBoxResetTouchKeyboard.Image = Properties.Resources.HandTouchKeyboard_Disabled;
                                //MessageBox.Show("Touchscreen successfully disabled", "Info");
                            }
                        }
                        else if (intValue == 0 && intValue2 == 0)
                        {
                            // Abilita
                            key.SetValue(valueName, 2, Microsoft.Win32.RegistryValueKind.DWord);
                            key.SetValue(valueName2, 2, Microsoft.Win32.RegistryValueKind.DWord);
                            key.DeleteValue(valueNameAutoInvoke, false);

                            System.Threading.Thread.Sleep(1000);

                            if ((int)key.GetValue(valueName, 2) == 2 && (int)key.GetValue(valueName2, 2) == 2)
                            {
                                pictureBoxResetTouchKeyboard.Image = Properties.Resources.HandTouchKeyboard;
                                //MessageBox.Show("Touchscreen enabled successfully", "Info");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Error while editing registration:\n{ex.Message}", "Errore");
            }
        }
    }
}
