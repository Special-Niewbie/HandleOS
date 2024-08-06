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
using System.Runtime.InteropServices;

namespace Console2Desk.TouchButtons
{
    internal class DesktopButton
    {
        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        private static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        private const uint WM_CLOSE = 0x0010;
        private const string ExplorerClassName = "CabinetWClass"; // Classe della finestra di File Explorer

        public static void CodeFordesktopButton1(MessagesBoxImplementation messagesBoxImplementation, Special_Niewbie_Button Button, string explorerPath)
        {
            try
            {
                // Apri la chiave di registro per la modifica
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true))
                {
                    if (key != null)
                    {
                        key.SetValue("Shell", explorerPath);
                        AutoClosingMessageBox.Show("Desktop Mode applied successfully.", "Success", 500); // Si chiude automaticamente dopo 1 secondo
                    }
                    else
                    {
                        messagesBoxImplementation.ShowMessage("Registry key not found.", "Error", MessageBoxButtons.OK);
                        return; // Esci se la chiave di registro non è trovata
                    }
                }

                // Riavvia explorer.exe
                Process.Start("explorer.exe");

                // Modifica le chiavi di registro per il riavvio di explorer.exe
                using (RegistryKey key01menu = Registry.ClassesRoot.CreateSubKey(@"DesktopBackground\Shell\Restart Explorer\shell\01menu\command", true))
                using (RegistryKey key02menu = Registry.ClassesRoot.CreateSubKey(@"DesktopBackground\Shell\Restart Explorer\shell\02menu\command", true))
                {
                    if (key01menu != null && key02menu != null)
                    {
                        key01menu.SetValue("", @"cmd.exe /c taskkill /f /im explorer.exe");
                        key02menu.SetValue("", @"cmd.exe /c taskkill /f /im explorer.exe");
                    }
                    else
                    {
                        messagesBoxImplementation.ShowMessage("One or more registry keys not found.", "Error", MessageBoxButtons.OK);
                    }
                }

                // Dopo aver riavviato explorer.exe, aspetta 3 secondi e chiudi eventuali nuove finestre di File Explorer
                Task.Delay(1500).ContinueWith(t => CloseNewExplorerWindows());

            }
            catch (Exception ex)
            {
                messagesBoxImplementation.ShowMessage("Error modifying registry keys or restarting explorer.exe: " + ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private static void CloseNewExplorerWindows()
        {
            // Cerca e chiudi tutte le finestre di File Explorer
            IntPtr hwnd = FindWindow(ExplorerClassName, null);

            while (hwnd != IntPtr.Zero)
            {
                PostMessage(hwnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                hwnd = FindWindow(ExplorerClassName, null);
            }
        }
    }
}
