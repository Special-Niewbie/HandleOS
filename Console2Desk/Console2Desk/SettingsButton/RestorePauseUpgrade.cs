/*
Console2Desk

Copyright (C) 2023 Special-Niewbie Softwares

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation version 3.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
*/

using Microsoft.Win32;

namespace Console2Desk.SettingsButton
{
    internal class RestorePauseUpgrade
    {
        public static void Execute(Special_Niewbie_Button Button, MessagesBoxImplementation messagesBoxImplementation)
        {
            // Ask the user for confirmation before proceeding
            DialogResult result = messagesBoxImplementation.ShowMessage("Are you sure you want to restore the Windows Upgrade pause settings until the year 3000?", "Confirm Restore", MessageBoxButtons.YesNo);

            // Check the user's response
            if (result == DialogResult.Yes)
            {
                // Code to restore settings in the registry
                try
                {
                    // Create or open the desired registry key
                    using (RegistryKey key = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\WindowsUpdate\UX"))
                    {
                        // Set registry key values
                        key.SetValue("IsConvergedUpdateStackEnabled", 1, RegistryValueKind.DWord);
                    }

                    using (RegistryKey key = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\WindowsUpdate\UX\Settings"))
                    {
                        // Set registry key values
                        key.SetValue("FlightSettingsMaxPauseDays", 3650, RegistryValueKind.DWord);
                        key.SetValue("PauseFeatureUpdatesStartTime", "2023-08-05T23:00:00Z", RegistryValueKind.String);
                        key.SetValue("PauseFeatureUpdatesEndTime", "3000-07-11T22:00:00Z", RegistryValueKind.String);
                        key.SetValue("PauseQualityUpdatesStartTime", "2023-08-05T23:00:00Z", RegistryValueKind.String);
                        key.SetValue("PauseQualityUpdatesEndTime", "3000-07-11T22:00:00Z", RegistryValueKind.String);
                        key.SetValue("PauseUpdatesStartTime", "2023-08-05T23:00:00Z", RegistryValueKind.String);
                        key.SetValue("PauseUpdatesExpiryTime", "3000-07-11T22:00:00Z", RegistryValueKind.String);
                    }

                    using (RegistryKey key = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\WindowsUpdate\UX\StateVariables"))
                    {
                        // Set registry key values
                        key.SetValue("SmartSchedulerPredictedStartTimePoint", new byte[] { 0xa0, 0x62, 0x1f, 0x6c, 0x8e, 0x01, 0x00, 0x00 }, RegistryValueKind.Binary);
                        key.SetValue("SmartSchedulerPredictedEndTimePoint", new byte[] { 0x20, 0x2e, 0xc4, 0x6c, 0x8e, 0x01, 0x00, 0x00 }, RegistryValueKind.Binary);
                        key.SetValue("SmartSchedulerPredictedConfidence", 50, RegistryValueKind.DWord);
                    }

                    using (RegistryKey key = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\WindowsUpdate\UpdatePolicy"))
                    {
                        // Set registry key values
                        key.SetValue("DeferQualityUpdates", 0, RegistryValueKind.DWord);
                        key.SetValue("DeferFeatureUpdates", 0, RegistryValueKind.DWord);
                        key.SetValue("BranchReadinessLevel", "CB", RegistryValueKind.String);
                        key.SetValue("IsDeferralIsActive", 1, RegistryValueKind.DWord);
                        key.SetValue("QualityUpdatesPaused", 1, RegistryValueKind.DWord);
                        key.SetValue("QualityUpdatePausePeriodInDays", 3650, RegistryValueKind.DWord);
                        key.SetValue("FeatureUpdatesPaused", 1, RegistryValueKind.DWord);
                        key.SetValue("FeatureUpdatePausePeriodInDays", 3650, RegistryValueKind.DWord);
                        key.SetValue("PauseFeatureUpdatesStartTime", "2023-08-05T23:00:00Z", RegistryValueKind.String);
                        key.SetValue("PauseFeatureUpdatesEndTime", "3000-07-11T22:00:00Z", RegistryValueKind.String);
                        key.SetValue("PauseQualityUpdatesStartTime", "2023-08-05T23:00:00Z", RegistryValueKind.String);
                        key.SetValue("PauseQualityUpdatesEndTime", "3000-07-11T22:00:00Z", RegistryValueKind.String);
                        key.SetValue("TemporaryEnterpriseFeatureControlState", 2, RegistryValueKind.DWord);
                    }

                    using (RegistryKey key = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\WindowsUpdate\UpdatePolicy\Settings"))
                    {
                        // Set registry key values
                        key.SetValue("PausedFeatureStatus", 2, RegistryValueKind.DWord);
                        key.SetValue("PausedQualityStatus", 2, RegistryValueKind.DWord);
                        key.SetValue("PausedQualityDate", "2023-08-05 23:00:00", RegistryValueKind.String);
                        key.SetValue("PausedFeatureDate", "2023-08-05 23:00:00", RegistryValueKind.String);
                    }

                    messagesBoxImplementation.ShowMessage("Windows Upgrade, pause settings successfully modified.", "Success", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    //Console.WriteLine("Error editing registry: " + ex.Message);
                }
            }
            else
            {
                //Console.WriteLine("Operation canceled by user.");
            }
        }
    }
}
