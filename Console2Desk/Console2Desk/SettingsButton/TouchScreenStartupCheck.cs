using System.Globalization;
using System.Management;

namespace Console2Desk.SettingsButton
{
    internal static class TouchScreenStartupCheck
    {
        public static void CheckTouchScreenStatus(Special_Niewbie_Button touchScreenEnDbButton, MessagesBoxImplementation messagesBoxImplementation)
        {
            string searchString = GetLocalizedTouchScreenName();

            // Use "using" to ensure resource release
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Description LIKE '%" + searchString + "%'"))
            {
                ManagementObjectCollection devices = searcher.Get();

                if (devices != null && devices.Count > 0)
                {
                    foreach (ManagementObject device in devices)
                    {
                        if (IsTouchScreen(device))
                        {
                            bool isTouchScreenEnabled = IsDeviceEnabled(device);

                            if (isTouchScreenEnabled)
                            {
                                // set icon for the touchscreen button
                                SetTouchScreenButtonIcon(touchScreenEnDbButton, true);
                            }
                            else
                            {
                                // set icon for the touchscreen button
                                SetTouchScreenButtonIcon(touchScreenEnDbButton, false);
                            }
                        }
                    }
                }
                else
                {
                    //MessageBox.Show("No device with the description '" + searchString + "' found.");
                    return;
                }
            }
        }

        private static string GetLocalizedTouchScreenName()
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
                //MessageBox.Show("I'm sorry, but the language of your operating system is not supported. " +
                    //"Please visit the developer's GitHub page to request support for your language: https://github.com/Special-Niewbie. " +
                    //"\n\n Tring to using and checking the default English name drivers.");
                return "HID-compliant touch screen";
            }
        }

        private static bool IsTouchScreen(ManagementObject device)
        {
            // Verifica se la descrizione contiene la stringa "HID-compliant touch screen"
            string description = device["Description"].ToString();
            return description.Contains(GetLocalizedTouchScreenName());
        }

        private static bool IsDeviceEnabled(ManagementObject device)
        {
            // Controlla se il dispositivo è abilitato
            object isEnabledObj = device["Status"];
            if (isEnabledObj != null && isEnabledObj.ToString() == "OK")
            {
                return true;
            }
            return false;
        }

        private static void SetTouchScreenButtonIcon(Special_Niewbie_Button touchScreenEnDbButton, bool isEnabled)
        {
            if (isEnabled)
            {
                // Imposta l'icona del pulsante per il touchscreen abilitato
                touchScreenEnDbButton.Image = Properties.Resources.touchscreen_Enabled48;
            }
            else
            {
                // Imposta l'icona del pulsante per il touchscreen disabilitato
                touchScreenEnDbButton.Image = Properties.Resources.touchscreen_Disable48;
            }
        }
    }
}
