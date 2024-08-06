/*
    For safety concerns, we must have this code to ensure that the `Console Softwares` 
    does not open with elevated privileges for applications like Playnite or Steam. 
    Otherwise, it could be risky for the end user to run any game or external program 
    with high privileges.
*/
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace Console2Desk.TouchButtons
{
    public static class ProcessExtensions
    {

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool CreateProcessWithTokenW(
        IntPtr hToken,
        int dwLogonFlags,
        string lpApplicationName,
        string lpCommandLine,
        int dwCreationFlags,
        IntPtr lpEnvironment,
        string lpCurrentDirectory,
        [In] ref STARTUPINFO lpStartupInfo,
        out PROCESS_INFORMATION lpProcessInformation);

        [StructLayout(LayoutKind.Sequential)]
        private struct PROCESS_INFORMATION
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public int dwProcessId;
            public int dwThreadId;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct STARTUPINFO
        {
            public int cb;
            public string lpReserved;
            public string lpDesktop;
            public string lpTitle;
            public int dwX;
            public int dwY;
            public int dwXSize;
            public int dwYSize;
            public int dwXCountChars;
            public int dwYCountChars;
            public int dwFillAttribute;
            public int dwFlags;
            public short wShowWindow;
            public short cbReserved2;
            public IntPtr lpReserved2;
            public IntPtr hStdInput;
            public IntPtr hStdOutput;
            public IntPtr hStdError;
        }

        public static void StartProcessAsUser(string path)
        {
            var hToken = WindowsIdentity.GetCurrent().Token;
            var si = new STARTUPINFO();
            var pi = new PROCESS_INFORMATION();

            if (!CreateProcessWithTokenW(hToken, 0, null, path, 0, IntPtr.Zero, null, ref si, out pi))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            CloseHandle(pi.hProcess);
            CloseHandle(pi.hThread);
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr hObject);
    }
}
