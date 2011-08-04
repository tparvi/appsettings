using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtensibilityExample
{
    using System.ComponentModel;

    using ApplicationSettings;

    public class Program
    {
        public static ExtendedAppSettings settings;

        public static void Main(string[] args)
        {
            settings = new ExtendedAppSettings();

            UpdateSettingValue();

            UseCustomConversionRoutine();
        }

        private static void UseCustomConversionRoutine()
        {
            var intValue = settings.GetValue<int>("IntValue");
            Console.WriteLine("IntValue is: {0}", intValue);

            var dollarStrValue = settings.GetValue("DollarValue");
            Console.WriteLine("Original DollarValue in config: {0}", dollarStrValue);

            var dollarValue = settings.GetValue<int>("DollarValue");
            Console.WriteLine("DollarValue after custom conversion is: {0}", dollarValue);
        }

        private static void UpdateSettingValue()
        {
            var strValue = settings.GetValue("StringValue");
            Console.WriteLine("Original StringValue: {0}", strValue);

            settings.UpdateSetting("StringValue", "B");
            settings.Save();

            strValue = settings.GetValue("StringValue");
            Console.WriteLine("Updated StringValue: {0}", strValue);
        }
    }

    public class ExtendedAppSettings : AppSettings
    {
        public void UpdateSetting(string settingName, string value)
        {
            // This method adds support for updating settings

            if (this.Configuration.AppSettings.Settings.AllKeys.Contains(settingName))
            {
                this.Configuration.AppSettings.Settings.Remove(settingName);    
            }
            
            this.Configuration.AppSettings.Settings.Add(settingName, value);
        }

        public void Save()
        {
            this.Configuration.Save();
        }

        protected override T ConvertValue<T>(string settingName, string value, IFormatProvider formatProvider)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (value.StartsWith("$"))
                {
                    // For dollar values remove the leading $ characters in order to convert them
                    var trimmedValue = value.TrimStart('$');
                    return base.ConvertValue<T>(settingName, trimmedValue, formatProvider);
                }
            }

            return base.ConvertValue<T>(settingName, value, formatProvider);
        }
    }
}
