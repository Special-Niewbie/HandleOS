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

using System.Windows.Forms;

namespace Console2Desk
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Verifica se un'altra istanza del programma è già in esecuzione
            bool isAnotherInstanceRunning = false;
            var currentProcess = System.Diagnostics.Process.GetCurrentProcess();
            var processes = System.Diagnostics.Process.GetProcessesByName(currentProcess.ProcessName);
            if (processes.Length > 2)
            {
                foreach (var process in processes)
                {
                    if (process.Id != currentProcess.Id)
                    {
                        isAnotherInstanceRunning = true;
                        break;
                    }
                }
            }

            // If another instance is already running, exit
            if (isAnotherInstanceRunning)
            {
                DependencyContainer.MessagesBoxImplementation.ShowMessage("The program is running 2 times already. Please ensure that only one instance of the program is running at a time.", "Attention", MessageBoxButtons.OK);
                return;
            }
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            //if (Environment.OSVersion.Version.Major >= 6) SetProcessDPIAware();
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        //[System.Runtime.InteropServices.DllImport("user32.dll")]
        //private static extern bool SetProcessDPIAware();
    }
}