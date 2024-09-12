using System.Runtime.InteropServices;

namespace inputs2desk.system.Internal.Win32;

/// <summary>
/// Used by
/// <see cref="NativeMethods.SendInput(uint, inputs2desk.system.Internal.Win32.INPUT[], int)"/>
/// to store information for synthesizing input events such as keystrokes,
/// mouse movement, and mouse clicks.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct INPUT
{

    /// <summary>
    /// The type of the input event.
    /// </summary>
    public InputType type;

    public InputUnion U;

    public static int Size => Marshal.SizeOf<INPUT>();

}
