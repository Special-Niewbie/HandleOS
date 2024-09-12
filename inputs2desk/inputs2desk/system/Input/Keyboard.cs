using inputs2desk.system.Internal.Win32;

namespace inputs2desk.system.Input;

/// <summary>
/// Provides static members for simulating keyboard input.
/// </summary>
public static class Keyboard
{


    private static INPUT CreateKeyPress(KeyboardVirtualKey key)
    {
        return new()
        {
            type = InputType.INPUT_KEYBOARD,
            U = new()
            {
                ki = new()
                {
                    wVk = (VirtualKeyShort)key,
                }
            }
        };
    }


    private static INPUT CreateKeyRelease(KeyboardVirtualKey key)
    {
        return new()
        {
            type = InputType.INPUT_KEYBOARD,
            U = new()
            {
                ki = new()
                {
                    dwFlags = KEYEVENTF.KEYUP,
                    wVk = (VirtualKeyShort)key,
                }
            }
        };
    }


    public static void PressKey(KeyboardVirtualKey key)
    {
        NativeMethods.SendInput(CreateKeyPress(key));
    }


    public static void ReleaseKey(KeyboardVirtualKey key)
    {
        NativeMethods.SendInput(CreateKeyRelease(key));
    }


    public static void TapKey(KeyboardVirtualKey key)
    {
        NativeMethods.SendInput(CreateKeyPress(key), CreateKeyRelease(key));
    }


}
