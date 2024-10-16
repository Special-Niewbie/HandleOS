using Console2Desk.ToggleSwitchDev;
using Microsoft.Win32;

namespace Console2Desk.SettingsButton
{
    internal class VRRO_OptWindowedGames
    {
        private const string RegistryKeyPath = @"Software\Microsoft\DirectX\UserGpuPreferences";
        private const string RegistryValueName = "DirectXUserGlobalSettings";
        private const string EnabledValue = "VRROptimizeEnable=1;SwapEffectUpgradeEnable=1;";
        private const string DisabledValue = "VRROptimizeEnable=0;SwapEffectUpgradeEnable=0;";

        public static void ApplyVRRO_OptWindowedGamesToggle(ControlToggles controlVRRO_OptWindowedGames, MessagesBoxImplementation messagesBoxImplementation)
        {
            // Start a new thread to avoid blocking the UI
            Thread applyThread = new Thread(() =>
            {
                try
                {
                    if (controlVRRO_OptWindowedGames.IsOn)
                    {
                        // Toggle is ON: Apply the necessary settings
                        EnableVRRO_OptWindowedGames();
                    }
                    else
                    {
                        // Toggle is OFF: Disable both settings
                        DisableVRRO_OptWindowedGames();
                    }
                }
                catch (Exception ex)
                {
                    // Show error message in case of an exception
                    controlVRRO_OptWindowedGames.Invoke((MethodInvoker)delegate
                    {
                        messagesBoxImplementation.ShowMessage($"Error applying VRR Optimization setting: {ex.Message}", "Error", MessageBoxButtons.OK);
                    });
                }
            });

            // Start the thread
            applyThread.Start();
        }

        // Method to enable VRRO and SwapEffectUpgrade by writing 1 to both
        private static void EnableVRRO_OptWindowedGames()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryKeyPath, true))
                {
                    if (key != null)
                    {
                        object existingValue = key.GetValue(RegistryValueName);

                        // If the key is missing or any of the values are incorrect, write both to 1
                        if (existingValue == null || !IsEnabled(existingValue.ToString()))
                        {
                            key.SetValue(RegistryValueName, EnabledValue, RegistryValueKind.String);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any errors that occur while writing to the registry
                DependencyContainer.MessagesBoxImplementation.ShowMessage($"Error enabling VRR Optimization: {ex.Message}", "Error", MessageBoxButtons.OK);
            }
        }

        // Method to disable VRRO and SwapEffectUpgrade by writing 0 to both
        private static void DisableVRRO_OptWindowedGames()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryKeyPath, true))
                {
                    if (key != null)
                    {
                        // Write both settings to 0
                        key.SetValue(RegistryValueName, DisabledValue, RegistryValueKind.String);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any errors that occur while writing to the registry
                DependencyContainer.MessagesBoxImplementation.ShowMessage($"Error disabling VRR Optimization: {ex.Message}", "Error", MessageBoxButtons.OK);
            }
        }

        // Helper method to check if both values are set to 1 (enabled)
        private static bool IsEnabled(string value)
        {
            return value.Contains("VRROptimizeEnable=1") && value.Contains("SwapEffectUpgradeEnable=1");
        }
    }
}

