using System.Runtime.InteropServices;
using System.Security;

namespace inputs2desk.system.Internal.Win32;

[SuppressUnmanagedCodeSecurity]
internal static partial class NativeMethods
{


    private const string Dll_User32 = "user32.dll";


    /// <summary>
    /// Synthesizes keystrokes, mouse motions, and button clicks.
    /// </summary>
    /// <param name="nInputs">The number of structures in the
    /// <paramref name="pInputs"/> array.</param>
    /// <param name="pInputs">An array of <see cref="INPUT"/> structures. Each
    /// structure represents an event to be inserted into the keyboard or mouse
    /// input stream.</param>
    /// <param name="cbSize">The size, in bytes, of an <see cref="INPUT"/> structure.
    /// If <paramref name="cbSize"/> is not the size of an <see cref="INPUT"/>
    /// structure, the function fails.</param>
    /// <returns>The function returns the number of events that it successfully
    /// inserted into the keyboard or mouse input stream. If the function returns
    /// zero, the input was already blocked by another thread. To get extended error
    /// information, call GetLastError.</returns>
    [DllImport(Dll_User32, SetLastError = true)]
    public static extern uint SendInput(uint nInputs, [In] INPUT[] pInputs, int cbSize);


    /// <summary>
    /// Synthesizes keystrokes, mouse motions, and button clicks.
    /// </summary>
    /// <param name="pInputs">An array of <see cref="INPUT"/> structures. Each
    /// structure represents an event to be inserted into the keyboard or mouse
    /// input stream.</param>
    /// <returns>The function returns the number of events that it successfully
    /// inserted into the keyboard or mouse input stream. If the function returns
    /// zero, the input was already blocked by another thread. To get extended error
    /// information, call GetLastError.</returns>
    public static uint SendInput(params INPUT[] pInputs)
    {
        return SendInput((uint)pInputs.Length, pInputs, INPUT.Size);
    }


}
