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

using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Console2Desk.SettingsButton
{
    internal class UISettingsControlManager
    {
        public static void HideButtons(Special_Niewbie_Button buttonOpenFileExplorer, Special_Niewbie_Button buttonRestorePauseUpgrade, Special_Niewbie_Button touchScreenEnDbButton, Special_Niewbie_Button buttonXinputTest, Special_Niewbie_Button buttonChangeConsoleSettings, Special_Niewbie_Button msStoreButton)
        {
            buttonOpenFileExplorer.Visible = false;
            buttonRestorePauseUpgrade.Visible = false;
            touchScreenEnDbButton.Visible = false;
            buttonXinputTest.Visible = false;
            buttonChangeConsoleSettings.Visible = false;
            msStoreButton.Visible = false;
        }

        public static void EnableDesktopButton(Special_Niewbie_Button desktopButton1, Special_Niewbie_Button consoleButton1)
        {
            desktopButton1.Enabled = true;
            desktopButton1.BackColor = Color.FromArgb(50, 50, 50);
            desktopButton1.BorderColor = Color.FromArgb(227, 227, 227);

            consoleButton1.Enabled = true;
            consoleButton1.BackColor = Color.FromArgb(50, 50, 50);
            consoleButton1.BorderColor = Color.FromArgb(227, 227, 227);
        }
    }
}
