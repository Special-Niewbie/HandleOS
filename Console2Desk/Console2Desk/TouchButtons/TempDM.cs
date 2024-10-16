using Microsoft.Win32;
using System.Diagnostics;
using System.Runtime.InteropServices;


namespace Console2Desk.TouchButtons
{
    class TempDM
    {
        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        private static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        private const uint WM_CLOSE = 0x0010;
        private const string ExplorerClassName = "CabinetWClass"; // File Explorer Window Class

        // Variable to store the original Shell key
        private static string originalShellValue = string.Empty;

        public static void CodeForTouchTempDM(MessagesBoxImplementation messagesBoxImplementation, Special_Niewbie_Button buttonTempDM, string explorerPath)
        {
            try
            {
                // Check if explorer.exe (the desktop) is running
                bool isExplorerRunning = IsExplorerRunning();

                if (!isExplorerRunning)
                {
                    SaveCurrentShellValue();

                    SetShellToExplorer();

                    RestartExplorerAsShell();

                    // wait 2 seconds
                    Task.Delay(2000).ContinueWith(t =>
                    {
                        // Close any File Explorer windows
                        CloseNewExplorerWindows();
                        Task.Delay(2000);
                        // Restore the original Shell key value
                        RestoreOriginalShellValue();
                    });
                }
                else
                {
                    messagesBoxImplementation.ShowMessage("Explorer.exe is already running and the desktop is active.", "Info", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                messagesBoxImplementation.ShowMessage("Error restarting explorer.exe: " + ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private static bool IsExplorerRunning()
        {
            // Find the desktop window (class "Progman") to check if explorer.exe is active
            IntPtr hWnd = FindWindow("Progman", null);
            return hWnd != IntPtr.Zero;
        }

        private static void SaveCurrentShellValue()
        {
            try
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", false);

                if (key != null)
                {
                    // Store the current value of the Shell key
                    object shellValue = key.GetValue("Shell");
                    if (shellValue != null)
                    {
                        originalShellValue = shellValue.ToString();
                    }

                    key.Close();
                }
            }
            catch (Exception ex)
            {
                //throw new Exception("Impossibile leggere la chiave di registro Shell: " + ex.Message);
            }
        }

        private static void SetShellToExplorer()
        {
            try
            {
                // Edit registry key to set explorer.exe as shell
                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true);

                if (key != null)
                {
                    key.SetValue("Shell", "explorer.exe", RegistryValueKind.String);
                    key.Close();
                }
            }
            catch (Exception ex)
            {
                //throw new Exception("Impossibile modificare la chiave di registro per explorer.exe: " + ex.Message);
            }
        }

        private static void RestoreOriginalShellValue()
        {
            try
            {
                // Restore the original value of the Shell key
                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true);

                if (key != null)
                {
                    if (!string.IsNullOrEmpty(originalShellValue))
                    {
                        // Restore the key to its original value
                        key.SetValue("Shell", originalShellValue, RegistryValueKind.String);
                    }
                    else
                    {
                        // If there was no previous value, removes the Shell key
                        key.DeleteValue("Shell", false);
                    }

                    key.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to restore original Shell registry key: " + ex.Message);
            }
        }

        private static void RestartExplorerAsShell()
        {
            // Close explorer.exe before restarting it
            var explorerProcesses = Process.GetProcessesByName("explorer");
            foreach (var process in explorerProcesses)
            {
                process.Kill();
            }

            // Launch explorer.exe as system shell
            Process.Start("explorer.exe");
        }

        private static void CloseNewExplorerWindows()
        {
            // Find and close all File Explorer windows
            IntPtr hwnd = FindWindow(ExplorerClassName, null);

            while (hwnd != IntPtr.Zero)
            {
                PostMessage(hwnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                hwnd = FindWindow(ExplorerClassName, null);
            }
        }
    }
}
