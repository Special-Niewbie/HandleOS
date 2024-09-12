using System.Runtime.InteropServices;

namespace inputs2desk.system.Internal.Win32;

[StructLayout(LayoutKind.Sequential)]
internal struct KEYBDINPUT
{
    internal VirtualKeyShort wVk;
    internal ScanCodeShort wScan;
    internal KEYEVENTF dwFlags;
    internal int time;
    internal nuint dwExtraInfo;
}
