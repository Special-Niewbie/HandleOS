using System;
using System.Windows.Forms;
using XInputium.XInput;
using inputs2desk.system.Input;
using XInputium.ModifierFunctions;

namespace inputs2desk
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }
       
    }
}   