using inputs2desk.system.Internal.Win32;

namespace inputs2desk.system.Input;

/// <summary>
/// Provides static members for simulating mouse input.
/// </summary>
public static class Mouse
{


    private static INPUT CreateButtonPress(MouseButton mouseButton)
    {
        return new()
        {
            type = InputType.INPUT_MOUSE,
            U = new()
            {
                mi = new()
                {
                    dwFlags = mouseButton switch
                    {
                        MouseButton.Left => MOUSEEVENTF.LEFTDOWN,
                        MouseButton.Right => MOUSEEVENTF.RIGHTDOWN,
                        MouseButton.Middle => MOUSEEVENTF.MIDDLEDOWN,
                        MouseButton.X1 or MouseButton.X2 => MOUSEEVENTF.XDOWN,
                        _ => throw new NotSupportedException()
                    },
                    mouseData = mouseButton == MouseButton.X1 ? 0x1u : mouseButton == MouseButton.X2 ? 0x2u : 0u,
                }
            }
        };
    }


    private static INPUT CreateButtonRelease(MouseButton mouseButton)
    {
        return new()
        {
            type = InputType.INPUT_MOUSE,
            U = new()
            {
                mi = new()
                {
                    dwFlags = mouseButton switch
                    {
                        MouseButton.Left => MOUSEEVENTF.LEFTUP,
                        MouseButton.Right => MOUSEEVENTF.RIGHTUP,
                        MouseButton.Middle => MOUSEEVENTF.MIDDLEUP,
                        MouseButton.X1 or MouseButton.X2 => MOUSEEVENTF.XUP,
                        _ => throw new NotSupportedException()
                    },
                    mouseData = mouseButton == MouseButton.X1 ? 0x1u : mouseButton == MouseButton.X2 ? 0x2u : 0u,
                }
            }
        };
    }


    public static void SetPosition(double x, double y)
    {
        if (double.IsNaN(x) || double.IsInfinity(x))
            throw new ArgumentException($"'{x}' is not a valid value for '{nameof(x)}' parameter.", nameof(x));
        if (double.IsNaN(y) || double.IsInfinity(y))
            throw new ArgumentException($"'{y}' is not a valid value for '{nameof(y)}' parameter.", nameof(y));

        INPUT input = new()
        {
            type = InputType.INPUT_MOUSE,
            U = new()
            {
                mi = new()
                {
                    dx = (int)Math.Round(x * 65536),
                    dy = (int)Math.Round(y * 65536),
                    dwFlags = MOUSEEVENTF.MOVE | MOUSEEVENTF.ABSOLUTE | MOUSEEVENTF.VIRTUALDESK,
                }
            }
        };
        NativeMethods.SendInput(input);
    }


    public static void MoveBy(double x, double y)
    {
        if (double.IsNaN(x) || double.IsInfinity(x))
            throw new ArgumentException($"'{x}' is not a valid value for '{nameof(x)}' parameter.", nameof(x));
        if (double.IsNaN(y) || double.IsInfinity(y))
            throw new ArgumentException($"'{y}' is not a valid value for '{nameof(y)}' parameter.", nameof(y));

        if (x == 0 && y == 0)
            return;

        INPUT input = new()
        {
            type = InputType.INPUT_MOUSE,
            U = new()
            {
                mi = new()
                {
                    dx = (int)Math.Round(x * 65536),
                    dy = (int)Math.Round(y * 65536),
                    dwFlags = MOUSEEVENTF.MOVE,
                }
            }
        };
        NativeMethods.SendInput(input);
    }


    public static void PressButton(MouseButton mouseButton)
    {
        if (!Enum.IsDefined(mouseButton))
            throw new ArgumentException(
                $"'{mouseButton}' is not a defined constant of a '{nameof(MouseButton)}' enumeration.",
                nameof(mouseButton));

        NativeMethods.SendInput(CreateButtonPress(mouseButton));
    }


    public static void ReleaseButton(MouseButton mouseButton)
    {
        if (!Enum.IsDefined(mouseButton))
            throw new ArgumentException(
                $"'{mouseButton}' is not a defined constant of a '{nameof(MouseButton)}' enumeration.",
                nameof(mouseButton));

        NativeMethods.SendInput(CreateButtonRelease(mouseButton));
    }


    public static void TapButton(MouseButton mouseButton)
    {
        if (!Enum.IsDefined(mouseButton))
            throw new ArgumentException(
                $"'{mouseButton}' is not a defined constant of a '{nameof(MouseButton)}' enumeration.",
                nameof(mouseButton));

        NativeMethods.SendInput(CreateButtonPress(mouseButton), CreateButtonRelease(mouseButton));
    }


}
