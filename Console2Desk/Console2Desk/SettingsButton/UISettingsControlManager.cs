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

using Console2Desk.ToggleSwitchDev;

namespace Console2Desk.SettingsButton
{
    internal class UISettingsControlManager
    {
        public static void HideButtons(Special_Niewbie_Button buttonOpenFileExplorer, Special_Niewbie_Button buttonRestorePauseUpgrade,
            Special_Niewbie_Button touchScreenEnDbButton, Special_Niewbie_Button buttonXinputTest, Special_Niewbie_Button buttonChangeConsoleSettings,
            Special_Niewbie_Button msStoreButton, ControlToggles controlAMDnoShutter, ControlToggles controlMeltdownSpectreProtectionOnOff, Label labelAMDnoShutter,
            Label labelMeltdown_Spectre, Label label2, PictureBox pictureBoxAMDnoShutter, PictureBox pictureBoxMeltdown_Spectre, Panel panel4Toggle,
            ControlToggles controlSystemDevices, Label labelSystemDevices, PictureBox pictureBoxSystemDevices, ControlToggles controlCoreIsolation_Exploid,
            Label labelCoreIsolation_CFG, PictureBox pictureBoxCoreIsolation_CFG, ControlToggles controlBCDMemoryUsage,
            Label labelBCDMemoryUsage, PictureBox pictureBCDMemoryUsage, ControlToggles controlReduceWindowsLatency,
            PictureBox pictureBoxReduceWindowsLatency, Label labelReduceWindowsLatency, Label labelReduceNetworkLatency, PictureBox pictureBoxReduceNetworkLatency,
            ControlToggles controlReduceNetworkLatency, Special_Niewbie_Button special_Niewbie_ButtonHOB, PictureBox pictureBoxUAC, 
            Label labelUAC, ControlToggles uacToggleSwitch, Special_Niewbie_Button special_Niewbie_ButtonRestoreBoost)
        {
            buttonOpenFileExplorer.Visible = false;
            buttonRestorePauseUpgrade.Visible = false;
            touchScreenEnDbButton.Visible = false;
            buttonXinputTest.Visible = false;
            buttonChangeConsoleSettings.Visible = false;
            msStoreButton.Visible = false;
            controlAMDnoShutter.Visible = false;
            controlMeltdownSpectreProtectionOnOff.Visible = false;
            labelAMDnoShutter.Visible = false;
            labelMeltdown_Spectre.Visible = false;
            label2.Visible = false;
            pictureBoxAMDnoShutter.Visible = false;
            pictureBoxMeltdown_Spectre.Visible = false;
            panel4Toggle.Visible = false;
            controlSystemDevices.Visible = false;
            labelSystemDevices.Visible = false;
            pictureBoxSystemDevices.Visible = false;
            controlCoreIsolation_Exploid.Visible = false;
            labelCoreIsolation_CFG.Visible = false;
            pictureBoxCoreIsolation_CFG.Visible = false;
            controlBCDMemoryUsage.Visible = false;
            labelBCDMemoryUsage.Visible = false;
            pictureBCDMemoryUsage.Visible = false;
            controlReduceWindowsLatency.Visible = false;
            pictureBoxReduceWindowsLatency.Visible = false;
            labelReduceWindowsLatency.Visible = false;
            labelReduceNetworkLatency.Visible = false;
            pictureBoxReduceNetworkLatency.Visible = false;
            controlReduceNetworkLatency.Visible = false;
            special_Niewbie_ButtonHOB.Visible = false;
            pictureBoxUAC.Visible = false;
            labelUAC.Visible = false;
            uacToggleSwitch.Visible = false;
            special_Niewbie_ButtonRestoreBoost.Visible = false;

        }
        

        public static void EnableDesktopButton(Special_Niewbie_Button desktopButton1, Special_Niewbie_Button consoleButton1,
            PictureBox pictureBoxResetTouchKeyboard, PictureBox pictureBoxAMDadrenaline, PictureBox pictureBoxAMDadrenalinePress, PictureBox pictureBoxRealTime_ON,
            PictureBox pictureBox4, PictureBox pictureBoxRealTime_OFF, PictureBox pictureBoxWifi, Special_Niewbie_Button buttonMiniConsoleWindowNew)
        {
            desktopButton1.Enabled = true;
            desktopButton1.BackColor = Color.FromArgb(50, 50, 50);
            desktopButton1.BorderColor = Color.FromArgb(227, 227, 227);

            consoleButton1.Enabled = true;
            consoleButton1.BackColor = Color.FromArgb(50, 50, 50);
            consoleButton1.BorderColor = Color.FromArgb(227, 227, 227);

            pictureBoxResetTouchKeyboard.Enabled = true;
            pictureBoxAMDadrenaline.Enabled = true;
            pictureBoxAMDadrenalinePress.Enabled = true;
            pictureBoxRealTime_ON.Enabled = true;
            pictureBox4.Enabled = true;
            pictureBoxRealTime_OFF.Enabled = true;
            pictureBoxWifi.Enabled = true;
            buttonMiniConsoleWindowNew.Enabled = true;
        }
    }
}
