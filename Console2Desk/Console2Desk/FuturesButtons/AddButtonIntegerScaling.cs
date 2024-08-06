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

namespace Console2Desk.FuturesButtons
{
    internal class AddButtonIntegerScaling
    {
        public static void CodeForaddButtonIntegerScaling(PictureBox pictureBox3, MessagesBoxImplementation messagesBoxImplementation)
        {
            try
            {
                bool keysAdded = false; // Flag to indicate whether keys have been added

                // Array containing the registry key paths to search for
                string[] registryKeys = {
            @"SYSTEM\ControlSet001\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000",
            @"SYSTEM\ControlSet001\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0001",
            @"SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000",
            @"SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0001"
        };

                // For each registry key path
                foreach (string keyPath in registryKeys)
                {
                    // Opens the registry key
                    using (RegistryKey key = Registry.LocalMachine.OpenSubKey(keyPath, true))
                    {
                        if (key != null)
                        {
                            // Adds the DalEmbeddedIntegerScalingSupport entry with value dword:00000001
                            key.SetValue("DalEmbeddedIntegerScalingSupport", 1, RegistryValueKind.DWord);
                            keysAdded = true; // Set the flag to true if at least one key has been added
                        }
                    }
                }

                if (keysAdded)
                {
                    // If keys have been added, it shows an informational message
                    messagesBoxImplementation.ShowMessage("Registry keys modified successfully. You may need to restart your computer to apply these changes. Please check AMD Adrenaline software for Integer Scaling support.", "Success", MessageBoxButtons.OK);
                    pictureBox3.Visible = true;
                }
                else
                {
                    // If no key was added, it shows an error message
                    messagesBoxImplementation.ShowMessage("Integer Scaling was not added because the predefined registry paths do not exist. Please contact the developer on GitHub for further assistance.", "Error", MessageBoxButtons.OK);
                    pictureBox3.Visible = false;
                }
            }
            catch (Exception ex)
            {
                // If an error occurs, displays a generic error message
                messagesBoxImplementation.ShowMessage("Error modifying registry keys: " + ex.Message, "Error", MessageBoxButtons.OK);
            }
        }
    }
}
