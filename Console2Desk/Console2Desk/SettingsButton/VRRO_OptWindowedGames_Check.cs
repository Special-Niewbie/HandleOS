using Console2Desk.ToggleSwitchDev;
using Microsoft.Win32;

namespace Console2Desk.SettingsButton
{
    internal class VRRO_OptWindowedGames_Check
    {
        private const string RequiredValue = "VRROptimizeEnable=1;SwapEffectUpgradeEnable=1;";
        private const string RegistryKeyPath = @"Software\Microsoft\DirectX\UserGpuPreferences";
        private const string RegistryValueName = "DirectXUserGlobalSettings";

        public static void CheckVRRO_OptWindowedGamesStatus(ControlToggles controlVRRO_OptWindowedGames, MessagesBoxImplementation messagesBoxImplementation)
        {
            Thread checkThread = new Thread(() =>
            {
                try
                {
                    // Ensure the registry key exists and contains the correct value if necessary
                    bool isKeyUpdated = EnsureVRRO_OptWindowedGamesRegistryKeyExists();

                    // Check the current state of the registry key after ensuring its existence
                    bool isVRRO_OptEnabled = CheckVRRO_OptWindowedGamesRegistryKey();

                    // Update the toggle switch state on the main thread based on the result
                    controlVRRO_OptWindowedGames.Invoke((MethodInvoker)delegate
                    {
                        controlVRRO_OptWindowedGames.IsOn = isVRRO_OptEnabled || isKeyUpdated;
                    });
                }
                catch (Exception ex)
                {
                    // Show error message on the main thread
                    controlVRRO_OptWindowedGames.Invoke((MethodInvoker)delegate
                    {
                        messagesBoxImplementation.ShowMessage($"Error checking VRR Optimization status: {ex.Message}", "Error", MessageBoxButtons.OK);
                    });
                }
            });

            // Start the thread
            checkThread.Start();
        }

        // Ensure the registry key exists and has the correct value
        private static bool EnsureVRRO_OptWindowedGamesRegistryKeyExists()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryKeyPath, true))
                {
                    if (key != null)
                    {
                        object existingValue = key.GetValue(RegistryValueName);

                        // If the value is missing or invalid, set both values to the required string
                        if (existingValue == null || !HasBothSettings(existingValue.ToString()))
                        {
                            key.SetValue(RegistryValueName, RequiredValue, RegistryValueKind.String);
                            return true; // Indicates that the key has been updated
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the error if registry modification fails
                DependencyContainer.MessagesBoxImplementation.ShowMessage($"Error creating or updating registry key: {ex.Message}", "Error", MessageBoxButtons.OK);
            }

            return false; // No update was made
        }

        // Check if the registry value contains the correct settings
        private static bool CheckVRRO_OptWindowedGamesRegistryKey()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryKeyPath))
                {
                    if (key != null)
                    {
                        object controlVRRO_OptWindowedGames = key.GetValue(RegistryValueName);
                        if (controlVRRO_OptWindowedGames != null)
                        {
                            // Validate the current value
                            return IsValueEnabled(controlVRRO_OptWindowedGames.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the error
                DependencyContainer.MessagesBoxImplementation.ShowMessage($"Error reading VRR Optimization registry key: {ex.Message}", "Error", MessageBoxButtons.OK);
            }

            return false; // Default to false if there's an issue
        }

        // Check if both settings are present
        private static bool HasBothSettings(string value)
        {
            return value.Contains("VRROptimizeEnable") && value.Contains("SwapEffectUpgradeEnable");
        }

        // Helper method to check the registry value's state
        private static bool IsValueEnabled(string value)
        {
            // Extract the values of VRROptimizeEnable and SwapEffectUpgradeEnable
            bool vrrOptimizeEnable = value.Contains("VRROptimizeEnable=1");
            bool swapEffectUpgradeEnable = value.Contains("SwapEffectUpgradeEnable=1");

            bool vrrOptimizeDisable = value.Contains("VRROptimizeEnable=0");
            bool swapEffectUpgradeDisable = value.Contains("SwapEffectUpgradeEnable=0");

            // Case 1: Both values are set to 1
            if (vrrOptimizeEnable && swapEffectUpgradeEnable)
            {
                return true;
            }
            // Case 2 and 3: One or both values are 0
            if ((vrrOptimizeEnable && swapEffectUpgradeDisable) || (vrrOptimizeDisable && swapEffectUpgradeEnable) || (vrrOptimizeDisable && swapEffectUpgradeDisable))
            {
                return false;
            }

            return false; // Default to false for any other cases
        }
    }
}
