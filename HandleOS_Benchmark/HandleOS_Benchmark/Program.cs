using System.Diagnostics;

namespace HandleOS_Benchmark
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            if (IsConsole2DeskRunning())
            {
                // If Console2Desk.exe is running, start the application normally
                Application.Run(new Form1());
            }
            else
            {
                // Otherwise, show an error message and close the application
                MessageBox.Show("Failed to start Console2Desk HandleOS Benchmark.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
        private static bool IsConsole2DeskRunning()
        {
            // Find the Console2Desk.exe process
            return Process.GetProcessesByName("Console2Desk").Any();
        }
    }
}