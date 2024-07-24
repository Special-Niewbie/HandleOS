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

namespace Console2Desk.TouchButtons
{
    internal class DesktopButton
    {
        public static void CodeFordesktopButton1(Special_Niewbie_Button Button, string explorerPath)
        {
            try
            {
                // Open the registry key for modification
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true))
                {
                    // Check if the key exists before modifying it
                    if (key != null)
                    {
                        // Set the value of the "Shell" key to explorer.exe
                        key.SetValue("Shell", explorerPath);
                        AutoClosingMessageBox.Show("Desktop Mode applied successfully.", "Success", 500); // It closes automatically after 1 second
                    }
                    else
                    {
                        MessageBox.Show("Registry key not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Exit method if registry key not found
                    }
                }

                // Restart explorer.exe
                Process.Start("explorer.exe");

                // After modifying the registry key, perform the restart action
                using (RegistryKey key01menu = Registry.ClassesRoot.CreateSubKey(@"DesktopBackground\Shell\Restart Explorer\shell\01menu\command", true))
                using (RegistryKey key02menu = Registry.ClassesRoot.CreateSubKey(@"DesktopBackground\Shell\Restart Explorer\shell\02menu\command", true))
                {
                    // Check if the keys exist before modifying them
                    if (key01menu != null && key02menu != null)
                    {
                        // Set the value of the "command" keys to the command for restarting explorer.exe
                        key01menu.SetValue("", @"cmd.exe /c taskkill /f /im explorer.exe");
                        key02menu.SetValue("", @"cmd.exe /c taskkill /f /im explorer.exe");
                    }
                    else
                    {
                        MessageBox.Show("One or more registry keys not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error modifying registry keys or restarting explorer.exe: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
