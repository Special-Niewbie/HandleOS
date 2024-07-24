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

using System;

namespace Console2Desk
{
    public class timerGif
    {
        public static void StartTimerForPictureBoxSettings(PictureBox pictureBoxSettings)
        {
            // Show the GIF image
            pictureBoxSettings.Image = Properties.Resources.Settings;

            // Start a timer to restore the static icon after a certain delay
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 1500; // Set the delay in milliseconds (example: 2000 = 2 seconds)
            timer.Tick += (s, _) =>
            {
                // Restore static icon
                pictureBoxSettings.Image = Properties.Resources.Settings_icon;

                // Stop the timer
                timer.Stop();

                // Release timer resources
                timer.Dispose();
            };
            timer.Start();

        }
    }
}
