namespace inputs2desk.system.Internal.Win32;

/// <summary>
/// A set of bit flags that specify various aspects of mouse motion and
/// button clicks.
/// </summary>
[Flags]
internal enum MOUSEEVENTF : uint
{
    /// <summary>
    /// Movement occurred.
    /// </summary>
    MOVE = 0x0001,
    /// <summary>
    /// The left button was pressed.
    /// </summary>
    LEFTDOWN = 0x0002,
    /// <summary>
    /// The left button was released.
    /// </summary>
    LEFTUP = 0x0004,
    /// <summary>
    /// The right button was pressed.
    /// </summary>
    RIGHTDOWN = 0x0008,
    /// <summary>
    /// The right button was released.
    /// </summary>
    RIGHTUP = 0x0010,
    /// <summary>
    /// The middle button was pressed.
    /// </summary>
    MIDDLEDOWN = 0x0020,
    /// <summary>
    /// The middle button was released.
    /// </summary>
    MIDDLEUP = 0x0040,
    /// <summary>
    /// An X button was pressed.
    /// </summary>
    XDOWN = 0x0080,
    /// <summary>
    /// An X button was released.
    /// </summary>
    XUP = 0x0100,
    /// <summary>
    /// The wheel was moved, if the mouse has a wheel. The amount of movement is specified
    /// in <see cref="MOUSEINPUT.mouseData"/>.
    /// </summary>
    WHEEL = 0x0800,
    /// <summary>
    /// The wheel was moved horizontally, if the mouse has a wheel. The amount of movement
    /// is specified in <see cref="MOUSEINPUT.mouseData"/>.
    /// </summary>
    HWHEEL = 0x01000,
    /// <summary>
    /// The <c>WM_MOUSEMOVE</c> messages will not be coalesced. The default behavior is to
    /// coalesce <c>WM_MOUSEMOVE</c> messages.
    /// </summary>
    MOVE_NOCOALESCE = 0x2000,
    /// <summary>
    /// Maps coordinates to the entire desktop. Must be used with <see cref="ABSOLUTE"/>.
    /// </summary>
    VIRTUALDESK = 0x4000,
    /// <summary>
    /// The <see cref="MOUSEINPUT.dx"/> and <see cref="MOUSEINPUT.dy"/> members contain
    /// normalized absolute coordinates. If the flag is not set, <c>dx</c> and <c>dy</c>
    /// contain relative data (the change in position since the last reported position).
    /// This flag can be set, or not set, regardless of what kind of mouse or other pointing
    /// device, if any, is connected to the system.
    /// </summary>
    ABSOLUTE = 0x8000,
}