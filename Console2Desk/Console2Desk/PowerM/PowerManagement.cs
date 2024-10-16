using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Console2Desk.PowerM
{
    public class PowerManagement
    {
        // Import delle API di Windows necessarie
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("PowrProf.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern bool SetSuspendState(bool hibernate, bool forceCritical, bool disableWakeEvent);

        [DllImport("user32.dll")]
        private static extern int PostMessage(int hWnd, int hMsg, int wParam, int lParam);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr hWnd);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool ReleaseDC(IntPtr hWnd, IntPtr hDc);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(int hWnd, int wMsg, int wParam, int lParam);

        private const int HWND_BROADCAST = 0xffff;
        private const int WM_SYSCOMMAND = 0x0112;
        private const int SC_MONITORPOWER = 0xF170;
        private const int MONITOR_OFF = 2;
        private const int MONITOR_ON = -1;

        const uint SE_PRIVILEGE_ENABLED = 0x00000002;
        const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";
        const uint TOKEN_ADJUST_PRIVILEGES = 0x0020;
        const uint TOKEN_QUERY = 0x0008;

        // Per il riavvio e lo spegnimento
        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool AdjustTokenPrivileges(IntPtr TokenHandle, bool DisableAllPrivileges, ref TOKEN_PRIVILEGES NewState, int BufferLength, IntPtr PreviousState, IntPtr ReturnLength);

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool OpenProcessToken(IntPtr ProcessHandle, uint DesiredAccess, out IntPtr TokenHandle);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool LookupPrivilegeValue(string lpSystemName, string lpName, ref LUID lpLuid);

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct TOKEN_PRIVILEGES
        {
            public int PrivilegeCount;
            public LUID Luid;
            public uint LowPart;
            public int HighPart;
            public int Attributes;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct LUID
        {
            public int LowPart;
            public int HighPart;
        }

        // Metodo per spegnere lo schermo
        public static void TurnOffScreen()
        {
            try
            {                
                SendMessage(HWND_BROADCAST, WM_SYSCOMMAND, SC_MONITORPOWER, MONITOR_OFF);
            }
            catch
            {
                try
                {
                    // Se il primo metodo fallisce, proviamo il secondo
                    IntPtr hWnd = (IntPtr)HWND_BROADCAST;
                    SendMessage(hWnd, (uint)WM_SYSCOMMAND, (IntPtr)SC_MONITORPOWER, (IntPtr)MONITOR_OFF);
                }
                catch (Exception ex)
                {
                    DependencyContainer.MessagesBoxImplementation.ShowMessage($"Screen off error: {ex.Message}", "Error", MessageBoxButtons.OK);
                }
            }
        }

        /*public static void SetSleepMode()
        {
            try
            {
                // Primo tentativo: modalità sospensione normale
                bool result = Application.SetSuspendState(
                    PowerState.Suspend,  // Stato: Sospensione
                    force: true,        // Non forzare (permette alle applicazioni di salvare il loro stato)
                    disableWakeEvent: false  // Permette il risveglio del sistema
                );
            }
            catch (Exception ex)
            {
                DependencyContainer.MessagesBoxImplementation.ShowMessage($"Error activating sleep mode: {ex.Message}",
                              "Error",
                              MessageBoxButtons.OK);
            }
        }*/

        // Metodo per riavviare il PC
        public static void RestartComputer()
        {
            try
            {
                SetPrivileges();
                Process.Start("shutdown.exe", "/r /t 0");
            }
            catch (Exception ex)
            {
                DependencyContainer.MessagesBoxImplementation.ShowMessage($"System reboot error: {ex.Message}", "Error", MessageBoxButtons.OK);
            }
        }

        // Metodo per spegnere il PC
        public static void ShutdownComputer()
        {
            try
            {
                SetPrivileges();
                Process.Start("shutdown.exe", "/s /t 0");
            }
            catch (Exception ex)
            {
                DependencyContainer.MessagesBoxImplementation.ShowMessage($"System shutdown error: {ex.Message}", "Error", MessageBoxButtons.OK);
            }
        }

        // Metodo privato per impostare i privilegi necessari
        private static void SetPrivileges()
        {
            const uint TOKEN_ADJUST_PRIVILEGES = 0x0020;
            const uint TOKEN_QUERY = 0x0008;
            const int SE_PRIVILEGE_ENABLED = 0x00000002;
            const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";

            var tokenPrivileges = new TOKEN_PRIVILEGES();
            var luid = new LUID();

            if (!OpenProcessToken(Process.GetCurrentProcess().Handle, TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, out IntPtr tokenHandle))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            if (!LookupPrivilegeValue(null, SE_SHUTDOWN_NAME, ref luid))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            tokenPrivileges.PrivilegeCount = 1;
            tokenPrivileges.Luid = luid;
            tokenPrivileges.Attributes = SE_PRIVILEGE_ENABLED;

            if (!AdjustTokenPrivileges(tokenHandle, false, ref tokenPrivileges, 0, IntPtr.Zero, IntPtr.Zero))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }
    }
}
