namespace inputs2desk.system.Internal.Win32;

[Flags]
internal enum KEYEVENTF : uint
{
    EXTENDEDKEY = 0x0001,
    KEYUP = 0x0002,
    SCANCODE = 0x0008,
    UNICODE = 0x0004
}
