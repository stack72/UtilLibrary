using System;
using System.ComponentModel;

namespace Util.ConfigManager
{
    public interface IConfigManager
    {
        T GetAppSettingAs<T>(string propertyName) where T : struct;
    }

    public class ConfigManager: IConfigManager
    {
        public T GetAppSettingAs<T>(string propertyName) where T : struct
        {
            if (propertyName == null)
                throw new ArgumentNullException("propertyName");

            var value = System.Configuration.ConfigurationManager.AppSettings.Get(propertyName);

            T returnVal = default(T);
            if (String.IsNullOrEmpty(value))
                return returnVal;

            TryParse(value, out returnVal);
            return returnVal;
        }

        private static void TryParse<T>(string s, out T value)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            try
            {
                value = (T)converter.ConvertFromString(s);
                return;
            }
            catch
            {
                value = default(T);
                return;
            }
        }
    }
}
