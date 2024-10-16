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
                // Apri la chiave di registro o creala se non esiste
                using (var key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(registryPath))
                {
                    if (key != null)
                    {
                        object value = key.GetValue(valueName);
                        object value2 = key.GetValue(valueName2);
                        int intValue = 0;
                        int intValue2 = 0;
                        if (value != null && int.TryParse(value.ToString(), out intValue) &&
                    value2 != null && int.TryParse(value2.ToString(), out intValue2))
                        {
                            if (intValue == 2 || intValue == 1 || intValue2 == 2 || intValue2 == 1)
                            {
                                key.SetValue(valueName, 0, Microsoft.Win32.RegistryValueKind.DWord);
                                key.SetValue(valueName2, 0, Microsoft.Win32.RegistryValueKind.DWord);
                                // Set EnableDesktopModeAutoInvoke to 0 (Off)
                                key.SetValue(valueNameAutoInvoke, 0, Microsoft.Win32.RegistryValueKind.DWord);
                                System.Threading.Thread.Sleep(1000);
                                if ((int)key.GetValue(valueName, 0) == 0 && (int)key.GetValue(valueName2, 0) == 0)
                                {
                                    pictureBoxResetTouchKeyboard.Image = Properties.Resources.HandTouchKeyboard_Disabled;
                                }
                            }
                            else if (intValue == 0 && intValue2 == 0)
                            {
                                key.SetValue(valueName, 2, Microsoft.Win32.RegistryValueKind.DWord);
                                key.SetValue(valueName2, 2, Microsoft.Win32.RegistryValueKind.DWord);
                                // Delete EnableDesktopModeAutoInvoke key
                                key.DeleteValue(valueNameAutoInvoke, false);
                                System.Threading.Thread.Sleep(1000);
                                if ((int)key.GetValue(valueName, 2) == 2 && (int)key.GetValue(valueName2, 2) == 2)
                                {
                                    pictureBoxResetTouchKeyboard.Image = Properties.Resources.HandTouchKeyboard;
                                }
                            }
                        }
                        else
                        {
                            // Handle the case where the values are not integers
                            DependencyContainer.MessagesBoxImplementation.ShowMessage("Invalid registry value type.", "Error", MessageBoxButtons.OK);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //DependencyContainer.MessagesBoxImplementation.ShowMessage($"An error occurred while modifying the registry: {ex.Message}", "Error", MessageBoxButtons.OK);
            }
        }
    }
}
