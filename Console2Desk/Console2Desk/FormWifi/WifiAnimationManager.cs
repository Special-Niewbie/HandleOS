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

using Microsoft.Win32;
using System.Diagnostics;

namespace Console2Desk.FormWifi
{
    internal class WifiAnimationManager
    {
        private readonly System.Windows.Forms.Timer gifTimer = new System.Windows.Forms.Timer();
        private readonly PictureBox pictureBoxWifi;
        private readonly int repeatCount = 2;
        private int currentRepeat = 0;

        public WifiAnimationManager(PictureBox pictureBox)
        {
            pictureBoxWifi = pictureBox;
            SetupTimer();
        }

        private void SetupTimer()
        {
            gifTimer.Interval = 1120;
            gifTimer.Tick += gifTimer_Tick;
        }

        private void gifTimer_Tick(object sender, EventArgs e)
        {
            currentRepeat++;
            if (currentRepeat >= repeatCount * 2)
            {
                gifTimer.Stop();
                pictureBoxWifi.Image = Properties.Resources.Wifi_icon;
            }
        }

        public void StartAnimation()
        {
            pictureBoxWifi.Image = Properties.Resources.Wifi;
            currentRepeat = 0;
            gifTimer.Start();
        }

        public void StopAnimation()
        {
            gifTimer.Stop();
            pictureBoxWifi.Image = Properties.Resources.Wifi_icon;
        }
    }

    public class WifiMenuManager
    {
        private readonly MessagesBoxImplementation _messagesBoxImplementation;

        public WifiMenuManager(MessagesBoxImplementation messagesBox)
        {
            _messagesBoxImplementation = messagesBox;
        }

        public SystemMode GetSystemMode()
        {
            return IsDesktopMode() ? SystemMode.Desktop : SystemMode.Console;
        }

        private bool IsDesktopMode()
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon"))
                {
                    if (key != null)
                    {
                        string shellValue = key.GetValue("Shell") as string;
                        return string.Equals(shellValue, "explorer.exe", StringComparison.OrdinalIgnoreCase);
                    }
                }
            }
            catch (Exception ex)
            {
                _messagesBoxImplementation.ShowMessage(
                    $"Error reading registry key: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK);
            }
            return false;
        }

        public bool ShowWifiMenu()
        {
            Process.Start("explorer.exe", "ms-availablenetworks:");
            return true;
        }
    }
}
