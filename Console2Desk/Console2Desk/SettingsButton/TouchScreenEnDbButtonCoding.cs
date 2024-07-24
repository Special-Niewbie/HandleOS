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

using System.Management;
using System.Globalization;

namespace Console2Desk.SettingsButton
{
    internal class TouchScreenEnDbButtonCoding
    {
        private string GetLocalizedTouchScreenName()
        {
            // Get the current operating system language
            CultureInfo currentCulture = CultureInfo.CurrentCulture;

            // The disabled message below checks whether the returned language is correct - FOR Developers ######
            //MessageBox.Show($"OS language: {currentCulture.Name}");

            // Define a translation mapping
            Dictionary<string, string> translationMap = new Dictionary<string, string>()
            {
                { "en-EN", "HID-compliant touch screen" }, // English
                { "it-IT", "Touch screen compatibile HID" }, // Italian
                { "es-ES", "Pantalla táctil compatible con HID" }, // Spanish
                { "fr-FR", "Écran tactile compatible HID" }, // French
                { "de-DE", "HID-kompatibler Touchscreen" }, // German
                { "zh-CN", "HID兼容触摸屏" }, // Chinese (Simplified)
                { "ru-RU", "Сенсорный экран, совместимый с HID" }, // Russian
                { "he-IL", "מסך מגע תואם HID" }, // Hebrew
                { "ar-SA", "شاشة لمس متوافقة مع HID" } // Arabic
                // Add other languages if necessary...
            };

            // Get the device name corresponding to the current language
            string localizedTouchScreenName;
            if (translationMap.TryGetValue(currentCulture.Name, out localizedTouchScreenName))
            {
                // Below mesasage - FOR Developers ######
                //MessageBox.Show($"Name of the touchscreen device in the language {currentCulture.Name}: {localizedTouchScreenName}");
                return localizedTouchScreenName;
            }
            else
            {
                // If the operating system language is not supported, return the default English name
                MessageBox.Show("I'm sorry, but the language of your operating system is not supported. " +
                    "Please visit the developer's GitHub page to request support for your language: https://github.com/Special-Niewbie. " +
                    "\n\n Tring to using and checking the default English name drivers.");
                return "HID-compliant touch screen";
            }
        }

        public void code4touchScreenEnDbButton(Special_Niewbie_Button touchScreenEnDbButton)
        {
            string searchString = GetLocalizedTouchScreenName();

            // "using" to ensure resource release
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Description LIKE '%" + searchString + "%'"))
            {
                ManagementObjectCollection devices = searcher.Get();

                if (devices != null && devices.Count > 0)
                {
                    bool foundTouchscreen = false;

                    foreach (ManagementObject device in devices)
                    {
                        if (IsTouchScreen(device))
                        {
                            bool isTouchScreenEnabled = IsDeviceEnabled(device);

                            // Checking for nullity of the device object
                            if (device != null)
                            {
                                if (isTouchScreenEnabled)
                                {
                                    DisableDevice(device);
                                    touchScreenEnDbButton.Image = Properties.Resources.touchscreen_Disable48;
                                    MessageBox.Show("Touchscreen device DISABLED.");
                                }
                                else
                                {
                                    EnableDevice(device);
                                    touchScreenEnDbButton.Image = Properties.Resources.touchscreen_Enabled48;
                                    MessageBox.Show("Touchscreen device ENABLED.");
                                }

                                foundTouchscreen = true;
                            }
                            else
                            {
                                MessageBox.Show("Error: Null touchscreen device object.");
                            }
                        }
                    }

                    if (!foundTouchscreen)
                    {
                        MessageBox.Show("No touchscreen devices found.");
                    }
                }
                else
                {
                    MessageBox.Show("No device with the description '" + searchString + "' found.");
                }
            }
        }

        private bool IsTouchScreen(ManagementObject device)
        {
            // Checks if the description contains the string "HID-compliant touch screen" (or in another language)
            string description = device["Description"].ToString();
            return description.Contains(GetLocalizedTouchScreenName());
        }

        private bool IsDeviceEnabled(ManagementObject device)
        {
            // Check if the device is enabled
            object isEnabledObj = device["Status"];
            if (isEnabledObj != null && isEnabledObj.ToString() == "OK")
            {
                return true;
            }
            return false;
        }

        private void EnableDevice(ManagementObject device)
        {
            try
            {
                device.InvokeMethod("Enable", null);
            }
            catch (ManagementException ex)
            {
                MessageBox.Show($"Error when enabling the device: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Ignore this generic exception don't do nothing here
                //MessageBox.Show($"Generic error when enabling the device: {ex.Message}");
            }
        }

        private void DisableDevice(ManagementObject device)
        {
            try
            {
                device.InvokeMethod("Disable", null);
            }
            catch (ManagementException ex)
            {
                MessageBox.Show($"Error when disabling the device: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Ignore this generic exception don't do nothing here
                //MessageBox.Show($"Generic error when diabling the device: {ex.Message}");

            }
        }
    }
}
