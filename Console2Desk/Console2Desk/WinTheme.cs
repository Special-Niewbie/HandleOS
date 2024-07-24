using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Console2Desk
{
    public class WinTheme
    {
        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        [DllImport("dwmapi.dll")]
        private static extern int DwmIsCompositionEnabled(ref int pfEnabled);

        private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;
        private const int DWMWA_USE_IMMERSIVE_LIGHT_MODE = 19;
        private const int DWMWDDMVERSION = 0x7C0;

        private IntPtr handle;

        public WinTheme(IntPtr handle)
        {
            this.handle = handle;
        }

        public void ApplyTheme()
        {
            // Check whether the system supports dark or light theme
            int enabled = 0;
            DwmIsCompositionEnabled(ref enabled);

            // Apply the dark or light theme to the outer edge of the window
            int useDarkMode = 0;
            if (enabled > 0)
            {
                useDarkMode = 1;
            }
            else
            {
                // Fallback to a default theme in case the system doesn't support dark or light theme
                // Can customize this logic to suit your needs
                useDarkMode = 0;
            }

            // Sets the system theme attribute for the outer border of the window
            int result = DwmSetWindowAttribute(this.handle, DWMWA_USE_IMMERSIVE_DARK_MODE, ref useDarkMode, sizeof(int));
        }
    }
}
