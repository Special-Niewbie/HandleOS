using System.Runtime.InteropServices;

namespace inputs2desk.system.Internal.Win32;

/// <summary>
/// Contains information about a simulated mouse event.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct MOUSEINPUT
{

    /// <summary>
    /// The absolute position of the mouse, or the amount of motion since the last mouse
    /// event was generated, depending on the value of the dwFlags member. Absolute data
    /// is specified as the x coordinate of the mouse; relative data is specified as the
    /// number of pixels moved.
    /// </summary>
    public int dx;

    /// <summary>
    /// The absolute position of the mouse, or the amount of motion since the last mouse
    /// event was generated, depending on the value of the dwFlags member. Absolute data
    /// is specified as the y coordinate of the mouse; relative data is specified as the
    /// number of pixels moved.
    /// </summary>
    public int dy;

    public uint mouseData;

    /// <summary>
    /// A set of bit flags that specify various aspects of mouse motion and button clicks.
    /// </summary>
    public MOUSEEVENTF dwFlags;

    /// <summary>
    /// The time stamp for the event, in milliseconds. If this parameter is 0, the
    /// system will provide its own time stamp.
    /// </summary>
    public uint time;

    public nuint dwExtraInfo;

}