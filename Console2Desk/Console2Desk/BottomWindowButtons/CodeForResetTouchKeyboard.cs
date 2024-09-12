using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Console2Desk.BottomWindowButtons
{
    internal class CodeForResetTouchKeyboard
    {
        // Importa la funzione per eseguire un comando
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool CreateProcess(string lpApplicationName,
            string lpCommandLine, IntPtr lpProcessAttributes,
            IntPtr lpThreadAttributes, bool bInheritHandles,
            uint dwCreationFlags, IntPtr lpEnvironment,
            string lpCurrentDirectory, [In] ref STARTUPINFO lpStartupInfo,
            out PROCESS_INFORMATION lpProcessInformation);

        [StructLayout(LayoutKind.Sequential)]
        public struct STARTUPINFO
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
            public byte lpReserved2;
            public IntPtr hStdInput;
            public IntPtr hStdOutput;
            public IntPtr hStdError;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PROCESS_INFORMATION
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public uint dwProcessId;
            public uint dwThreadId;
        }

        private const uint CREATE_NEW_CONSOLE = 0x00000010;

        public static async Task ResetTouchKeyboardAsync(PictureBox pictureBoxResetTouchKeyboard)
        {
            ShareDisableTouchscreenClass.DisableTouchscreen(pictureBoxResetTouchKeyboard);
            Task.Delay(1500).Wait();

            await Task.Run(() =>
            {
                // Chiudi TabTip.exe se è in esecuzione
                foreach (var process in Process.GetProcessesByName("TabTip"))
                {
                    process.Kill();
                }

                // Pausa di 2 secondo
                Task.Delay(1500).Wait();
               
                // Riavvia TabTip.exe
                STARTUPINFO si = new STARTUPINFO();
                si.cb = Marshal.SizeOf(si);
                PROCESS_INFORMATION pi;
                CreateProcess(null, @"C:\Program Files\Common Files\microsoft shared\ink\TabTip.exe", IntPtr.Zero, IntPtr.Zero, false, CREATE_NEW_CONSOLE, IntPtr.Zero, null, ref si, out pi);
                
                // reactivate the Touch screen Panel
                Task.Delay(1500).Wait();
                ShareDisableTouchscreenClass.DisableTouchscreen(pictureBoxResetTouchKeyboard);
            });
        }
    }
}
