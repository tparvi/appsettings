using System;
using System.Linq;

namespace AttributeUsageExample
{
    using System.Reflection;

    using ApplicationSettings;

    class Program
    {
        public static AppSettings settings;

        public static void Main(string[] args)
        {
            try
            {
                // Open the default app.config file which in this case is SimpleExample.exe.config
                // and it is located in the same folder as the SimpleExample.exe.
                settings = AppSettings.CreateForAssembly(Assembly.GetEntryAssembly(), FileOption.FileMustExist);

                // Write all the settings into our own object
                var mySettings = new MyApplicationSettings();
                settings.WriteInto(mySettings);

                mySettings.StringValue = new string(mySettings.StringValue.Reverse().ToArray());

                // Read everything back and save
                settings.ReadFrom(mySettings);
                settings.Save();
            }
            catch (Exception exp)
            {
                Console.Error.WriteLine(exp.Message);
                throw;
            }
        }
    }

    public class MyApplicationSettings
    {
        /// <summary>
        /// Basic case. Setting named StringValue is uesd for
        /// this property and setting must exist.
        /// </summary>
        public string StringValue { get; set; }

        /// <summary>
        /// Setting named DoubleValue is used for this property.
        /// </summary>
        [SettingProperty(SettingName = "DoubleValue")]
        public double Double { get; private set; }

        /// <summary>
        /// Uses fi-FI culture to convert the value.
        /// </summary>
        [SettingProperty(SettingName = "DoubleWithFinnishLocale", CultureName = "fi-FI")]
        public double LocalizedValue { get; private set; }

        /// <summary>
        /// This is just a property which we don't care when reading or
        /// writing settings.
        /// </summary>
        [IgnoreProperty]
        public string ValueWeDontCareAbout { get; private set; }

        /// <summary>
        /// This value might or might not exist. If it does not exist
        /// then default vlaue abc is used when reading configuration settings.
        /// </summary>
        [SettingProperty(IsOptional = true, DefaultValue = "A")]
        public string OptionalSetting { get; private set; }
    }
}
