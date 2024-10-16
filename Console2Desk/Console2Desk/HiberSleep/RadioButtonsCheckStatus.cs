using Microsoft.Win32;

namespace Console2Desk.HiberSleep
{
    public class RadioButtonsCheckStatus
    {
        public static bool IsHibernateEnabled()
        {
            try
            {
                object value = Registry.GetValue(
                    @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Power",
                    "HibernateEnabled",
                    null
                );

                if (value == null)
                {
                    throw new Exception("Unable to read hibernation state from system log");
                }

                return Convert.ToInt32(value) == 1;
            }
            catch (Exception)
            {
                return false; // If there is an error, we assume it is disabled
            }
        }
    }
}
