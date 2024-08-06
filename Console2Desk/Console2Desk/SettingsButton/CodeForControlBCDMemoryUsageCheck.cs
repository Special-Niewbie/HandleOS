using Console2Desk.ToggleSwitchDev;
using System.Diagnostics;


namespace Console2Desk.SettingsButton
{
    internal class CodeForControlBCDMemoryUsageCheck
    {
        public static void CheckBCDMemoryUsage(ControlToggles memoryToggleSwitch, MessagesBoxImplementation messagesBoxImplementation)
        {
            try
            {
                string bcdEditOutput = ExecuteCommand("bcdedit");

                bool isFirstMegabytePolicySet = bcdEditOutput.Contains("firstmegabytepolicy");
                bool isAvoidLowMemorySet = bcdEditOutput.Contains("avoidlowmemory");
                bool isNoLowMemSet = bcdEditOutput.Contains("nolowmem");

                bool isOn = isFirstMegabytePolicySet && isAvoidLowMemorySet && isNoLowMemSet;

                // Update the toggle switch state on the main thread
                memoryToggleSwitch.Invoke((MethodInvoker)delegate
                {
                    memoryToggleSwitch.IsOn = isOn;
                });
            }
            catch (Exception ex)
            {
                // Show error message on the main thread
                memoryToggleSwitch.Invoke((MethodInvoker)delegate
                {
                    messagesBoxImplementation.ShowMessage($"An error occurred while checking BCD Memory Usage: {ex.Message}", "Error", MessageBoxButtons.OK);
                });
            }
        }

        private static string ExecuteCommand(string command)
        {
            try
            {
                using (var process = new Process())
                {
                    process.StartInfo.FileName = "cmd.exe";
                    process.StartInfo.Arguments = $"/C {command}";
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;
                    process.Start();
                    string output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();
                    return output;
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error executing command: {ex.Message}");
                return string.Empty;
            }
        }
    }
}