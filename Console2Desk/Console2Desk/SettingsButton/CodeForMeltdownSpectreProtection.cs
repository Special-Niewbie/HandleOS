using Console2Desk.ToggleSwitchDev;
using Microsoft.Win32;

namespace Console2Desk.SettingsButton
{
    internal class CodeForMeltdownSpectreProtection
    {
        public static void ToggleMeltdownSpectreProtection(ControlToggles protectionToggleSwitch, MessagesBoxImplementation messagesBoxImplementation)
        {
            // Run the toggle action in a separate thread
            Thread toggleThread = new Thread(() =>
            {
                try
                {
                    // Check the current state of the toggle switch
                    bool isOn = protectionToggleSwitch.IsOn;

                    // Open the registry key for editing
                    using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management", writable: true))
                    {
                        if (key == null)
                        {
                            throw new Exception("Unable to access the registry key.");
                        }

                        // Update the "FeatureSettings" value based on the toggle switch state
                        key.SetValue("FeatureSettings", isOn ? 0 : 1, RegistryValueKind.DWord);

                        if (isOn)
                        {
                            // Remove the "FeatureSettingsOverride" and "FeatureSettingsOverrideMask" subkeys if they exist
                            key.DeleteSubKey("FeatureSettingsOverride", false);
                            key.DeleteSubKey("FeatureSettingsOverrideMask", false);

                            // Remove the "EnableCfg" value if it exists
                            key.DeleteValue("EnableCfg", false);
                        }
                        else
                        {
                            // Add the "FeatureSettingsOverride" and "FeatureSettingsOverrideMask" subkeys with value 3
                            using (RegistryKey overrideKey = key.CreateSubKey("FeatureSettingsOverride"))
                            {
                                overrideKey.SetValue("", 3, RegistryValueKind.DWord);
                            }
                            using (RegistryKey overrideMaskKey = key.CreateSubKey("FeatureSettingsOverrideMask"))
                            {
                                overrideMaskKey.SetValue("", 3, RegistryValueKind.DWord);
                            }

                            // Add the "EnableCfg" value with data 0
                            key.SetValue("EnableCfg", 0, RegistryValueKind.DWord);
                        }
                    }

                    // Update the toggle switch state on the main thread
                    protectionToggleSwitch.Invoke((MethodInvoker)delegate
                    {
                        protectionToggleSwitch.IsOn = !isOn;
                    });

                    // Show message box if turning off the toggle
                    if (!isOn)
                    {
                        protectionToggleSwitch.Invoke((MethodInvoker)delegate
                        {
                            messagesBoxImplementation.ShowMessage(
                                "In early 2018 the PC industry was rocked by the revelation that common processor design features, widely used to increase the performance of modern PCs, could be abused to create critical security vulnerabilities. The industry quickly responded, and is responding, to these Meltdown and Spectre threats by updating operating systems, motherboard BIOSes and CPU firmware.\n\n" +
                                "If this system's processor is naturally immune to Meltdown attacks but not from Spectre attacks. When enabled and active, protection from Spectre will come at some cost in system performance. If this system's performance is more important than security, the Spectre vulnerability protection can be disabled to obtain somewhat improved performance.",
                                "Warning",
                                MessageBoxButtons.OK);
                        });
                    }
                }
                catch (Exception ex)
                {
                    // Show error message on the main thread
                    protectionToggleSwitch.Invoke((MethodInvoker)delegate
                    {
                        messagesBoxImplementation.ShowMessage($"An error occurred while toggling Meltdown/Spectre protection: {ex.Message}", "Error", MessageBoxButtons.OK);
                    });
                }
            });

            // Start the thread
            toggleThread.Start();
        }
    }
}