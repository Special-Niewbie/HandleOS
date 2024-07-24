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


using System.Diagnostics;
using System.Runtime.InteropServices;


namespace Console2Desk.FuturesButtons
{
    internal class ButtonMinimizeConsoleWindow
    {
        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern bool IsIconic(IntPtr hWnd);

        private const int SW_MINIMIZE = 6;
        private const int SW_RESTORE = 9;
        public static void CodeForbuttonMiniConsoleWindow(Form1 form)
        {
            string settingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Console2Desk", "Settings.ini");

            if (!File.Exists(settingsPath))
            {
                MessageBox.Show("Settings.ini not found. Please configure the settings first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string[] settings = File.ReadAllLines(settingsPath);
            if (settings.Length == 0)
            {
                MessageBox.Show("Settings.ini is empty. Please configure the settings first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string selectedApp = settings[0];
            string customAppPath = settings.Length > 1 ? settings[1] : string.Empty;

            string[] processNames = Array.Empty<string>();

            if (selectedApp == "Playnite")
            {
                processNames = new[] { "Playnite.FullscreenApp", "Playnite.DesktopApp" };
            }
            else if (selectedApp == "Steam")
            {
                processNames = new[] { "steam", "steamwebhelper" };
            }
            else if (selectedApp == "Custom" && !string.IsNullOrEmpty(customAppPath))
            {
                processNames = new[] { Path.GetFileNameWithoutExtension(customAppPath) };
            }
            else
            {
                MessageBox.Show("Invalid settings in Settings.ini. Please configure the settings correctly.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool foundProcess = false;

            foreach (string processName in processNames)
            {
                Process[] processes = Process.GetProcessesByName(processName);

                if (processes.Length > 0)
                {
                    foundProcess = true;
                    foreach (Process process in processes)
                    {
                        if (IsIconic(process.MainWindowHandle))
                        {
                            ShowWindowAsync(process.MainWindowHandle, SW_RESTORE);
                        }
                        else
                        {
                            ShowWindowAsync(process.MainWindowHandle, SW_MINIMIZE);
                        }
                    }
                }
            }

            if (!foundProcess)
            {
                MessageBox.Show($"No {selectedApp} application is currently running.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
