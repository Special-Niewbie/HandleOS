using Console2Desk.ToggleSwitchDev;
using System.Diagnostics;

namespace Console2Desk.FormWifi
{
    internal class CodeForWiFiOnOff
    {
        public static void ToggleWiFi(ControlToggles controlWiFi, MessagesBoxImplementation messagesBoxImplementation)
        {
            // Determine the executable to run based on the current state of the toggle
            string exePath = controlWiFi.IsOn
                ? @"C:\Program Files\Console2Desk\netadapter\WiFi_On.exe"  // Path for turning WiFi On
                : @"C:\Program Files\Console2Desk\netadapter\WiFi_Off.exe"; // Path for turning WiFi Off

            // Show a message box for debugging purposes
            //MessageBox.Show($"Attempting to run: {exePath}", "Debug Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Create a new thread to execute the command
            Thread toggleThread = new Thread(() =>
            {
                try
                {
                    // Start the appropriate executable directly
                    Process.Start(exePath);
                }
                catch (Exception ex)
                {
                    // Handle any errors that occur during execution
                    messagesBoxImplementation.ShowMessage($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK);
                }
                // No need to change the toggle state here, keep it the same
            });

            // Start the thread to execute the toggle command
            toggleThread.Start();
        }
    }
}
