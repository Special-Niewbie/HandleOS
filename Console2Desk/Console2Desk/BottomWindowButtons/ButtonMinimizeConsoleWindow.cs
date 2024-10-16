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


namespace Console2Desk.BottomWindowButtons
{
    internal class ButtonMinimizeConsoleWindow
    {
        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(nint hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern bool IsIconic(nint hWnd);

        private const int SW_MINIMIZE = 6;
        private const int SW_RESTORE = 9;
        public static void CodeForbuttonMiniConsoleWindow(Form1 form, MessagesBoxImplementation messagesBoxImplementation)
        {
            string settingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Console2Desk", "Settings.ini");

            if (!File.Exists(settingsPath))
            {
                messagesBoxImplementation.ShowMessage("Settings.ini not found. Please configure the settings first.", "Error", MessageBoxButtons.OK);
                return;
            }

            string[] settings = File.ReadAllLines(settingsPath);
            if (settings.Length == 0)
            {
                messagesBoxImplementation.ShowMessage("Settings.ini is empty. Please configure the settings first.", "Error", MessageBoxButtons.OK);
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
                messagesBoxImplementation.ShowMessage("Invalid settings in Settings.ini. Please configure the settings correctly.", "Error", MessageBoxButtons.OK);
                return;
            }

            bool foundProcess = false;
            /// Below I have tried some methods, but it seems that none of the methods succeed in running the apps without administrator privileges.
            // Check the "Shell" registry key if it is not "explorer.exe"
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon"))
            {
                if (key != null)
                {
                    object shellValue = key.GetValue("Shell");
                    if (shellValue != null)
                    {
                        string shellCommand = shellValue.ToString();
                        //messagesBoxImplementation.ShowMessage($"Shell command from registry: {shellCommand}", "Debug", MessageBoxButtons.OK);

                        // Logica per estrarre il percorso eseguibile e gli argomenti
                        string executablePath;
                        string arguments;

                        if (shellCommand.StartsWith("\""))
                        {
                            int endQuote = shellCommand.IndexOf("\"", 1);
                            if (endQuote != -1)
                            {
                                executablePath = shellCommand.Substring(1, endQuote - 1);
                                arguments = shellCommand.Substring(endQuote + 1).Trim();
                            }
                            else
                            {
                                executablePath = shellCommand;
                                arguments = "";
                            }
                        }
                        else
                        {
                            // Se non ci sono virgolette, assumiamo che tutto sia il percorso dell'eseguibile
                            executablePath = shellCommand;
                            arguments = "";
                        }

                        // messagesBoxImplementation.ShowMessage($"Executable path: {executablePath}\nArguments: {arguments}", "Debug", MessageBoxButtons.OK);

                        // Verifica se l'applicazione è già in esecuzione
                        string processName = Path.GetFileNameWithoutExtension(executablePath);
                        Process[] runningProcesses = Process.GetProcessesByName(processName);

                        if (runningProcesses.Length == 0)
                        {
                            // Avvia l'applicazione personalizzata se non è in esecuzione
                            try
                            {
                                if (!File.Exists(executablePath))
                                {
                                    messagesBoxImplementation.ShowMessage($"Executable file not found: {executablePath}", "Error", MessageBoxButtons.OK);
                                    return;
                                }

                                ProcessStartInfo startInfo = new ProcessStartInfo
                                {
                                    FileName = executablePath,
                                    Arguments = arguments,
                                    UseShellExecute = true,
                                    WorkingDirectory = Path.GetDirectoryName(executablePath),
                                    Verb = "runas"  // Prova ad eseguire con privilegi elevati
                                };

                                //messagesBoxImplementation.ShowMessage($"Attempting to start process:\nFileName: {startInfo.FileName}\nArguments: {startInfo.Arguments}\nWorking Directory: {startInfo.WorkingDirectory}", "Debug", MessageBoxButtons.OK);

                                Process.Start(startInfo);
                                //messagesBoxImplementation.ShowMessage($"The application {executablePath} has been started.", "Info", MessageBoxButtons.OK);
                            }
                            catch (Exception ex)
                            {
                                messagesBoxImplementation.ShowMessage($"Error starting application: {ex.Message}\nStack Trace: {ex.StackTrace}", "Error", MessageBoxButtons.OK);
                            }
                        }
                        else
                        {
                            //For Debug messagesBoxImplementation.ShowMessage("L'applicazione è già in esecuzione.", "Info", MessageBoxButtons.OK);
                            // L'applicazione è già in esecuzione, gestisci la minimizzazione/massimizzazione
                        }
                    }
                    else
                    {
                        //For Debug  messagesBoxImplementation.ShowMessage("Il valore della chiave Shell non è stato trovato.", "Error", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    //For Debug  messagesBoxImplementation.ShowMessage("La chiave Winlogon non è stata trovata.", "Error", MessageBoxButtons.OK);
                }
            }

            // If the app is "explorer.exe", we continue with the management of active processes
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
                messagesBoxImplementation.ShowMessage($"No Steam or {selectedApp} application is currently running.", "Error", MessageBoxButtons.OK);
            }
        }
    }
}