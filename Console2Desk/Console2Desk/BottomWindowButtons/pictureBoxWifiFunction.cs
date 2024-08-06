using Microsoft.Win32;
using System.Diagnostics;

namespace Console2Desk.BottomWindowButtons
{
    internal class pictureBoxWifiFunction
    {

        private static System.Windows.Forms.Timer gifTimer = new System.Windows.Forms.Timer(); // Timer statement
        private static PictureBox pictureBoxWifi; // Reference to pictureBoxWifi

        // Number of times the GIF animation will repeat
        private static int repeatCount = 2;
        // Counter to track how many times the animation has been repeated
        private static int currentRepeat = 0;

        private static void SetupTimer()
        {
            gifTimer.Interval = 1120;
            gifTimer.Tick += gifTimer_Tick;
        }
        private static void gifTimer_Tick(object sender, EventArgs e)
        {
            // Increase the repetition counter
            currentRepeat++;

            // Check if the number of repetitions has reached the limit
            if (currentRepeat >= repeatCount * 2)
            {
                // Stop the GIF animation
                gifTimer.Stop();
                // View the last image of the animation
                pictureBoxWifi.Image = Properties.Resources.Wifi_icon;
            }
        }
        private static bool IsDesktopMode()
        {
            try
            {
                // Open the registry key to read the Shell value
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon"))
                {
                    if (key != null)
                    {
                        // Get the shell key value
                        string shellValue = key.GetValue("Shell") as string;

                        // Check if the value is explorer.exe
                        return string.Equals(shellValue, "explorer.exe", StringComparison.OrdinalIgnoreCase);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions when reading the registry key
                DependencyContainer.MessagesBoxImplementation.ShowMessage($"Error reading registry key: {ex.Message}", "Error", MessageBoxButtons.OK);
            }

            // Return false if an error occurs or if the Shell key value is not explorer.exe
            return false;
        }
        public static void CodeForpictureBoxWifi(Form1 form, PictureBox wifiPictureBox, MessagesBoxImplementation messagesBoxImplementation)
        {
            // We make sure the timer is set up
            SetupTimer();

            // Assign the reference to pictureBoxWifi
            pictureBoxWifi = wifiPictureBox;
            // Check if the system is in Desktop mode
            if (!IsDesktopMode())
            {
                messagesBoxImplementation.ShowMessage("This feature is only available in Desktop Mode. Please switch to Desktop Mode to select the WiFi network.", "Error", MessageBoxButtons.OK);
                return;
            }
            else
            {
                // Start the GIF animation
                pictureBoxWifi.Image = Properties.Resources.Wifi;
                // Resets the repetition counter
                currentRepeat = 0;
                // Start the timer to control the GIF animation
                gifTimer.Start();

                // Start the explorer.exe process to show the WiFi networks drop-down menu
                Process.Start("explorer.exe", "ms-availablenetworks:");
            }
        }
    }
}
