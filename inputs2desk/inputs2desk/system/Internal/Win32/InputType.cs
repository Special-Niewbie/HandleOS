namespace inputs2desk.system.Internal.Win32;

internal enum InputType : uint
{
    /// <summary>
    /// The event is a mouse event. Use the <see cref="InputUnion.mi"/>
    /// structure of the union.
    /// </summary>
    INPUT_MOUSE = 0,
    /// <summary>
    /// The event is a keyboard event. Use the <see cref="InputUnion.ki"/>
    /// structure of the union.
    /// </summary>
    INPUT_KEYBOARD = 1,
    /// <summary>
    /// The event is a hardware event. Use the <see cref="InputUnion.hi"/>
    /// structure of the union.
    /// </summary>
    INPUT_HARDWARE = 2,
}
