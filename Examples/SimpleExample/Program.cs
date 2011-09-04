using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleExample
{
    using System.Globalization;
    using System.Reflection;

    using ApplicationSettings;

    public enum ProjectStatus
    {
        Unknown,
        Started,
        Finished
    }

    public class Program
    {
        public static AppSettings settings;

        public static void Main(string[] args)
        {            
            try
            {
                // Open the default app.config file which in this case is SimpleExample.exe.config
                // and it is located in the same folder as the SimpleExample.exe.
                settings = AppSettings.CreateForAssembly(Assembly.GetEntryAssembly(), FileOption.FileMustExist);

                // If you want to know the full path to the configuration file, use the FullPath property
                Console.WriteLine("Full path to configuration file is: {0}", settings.FullPath);

                ReadStringValues();
                ReadStandardDataTypes();
                ReadEnumerations();
                ReadNullableValues();
                ReadUsingCustomFormatProvider();
                ReadOptionalValues();
                ReadUsingCustomConversionFunction();
                ReadConnectionString();
                ReadingCustomConfigurationFile();

                UpdatingValues();
                CreatingAppSettings();
            }
            catch (Exception exp)
            {
                Console.Error.WriteLine(exp.Message);
                throw;
            }
        }

        public static void ReadStringValues()
        {
            // String values can be read without having to specify the type. This avoids the
            // unnecessary type conversion.
            var str = settings.GetValue("StringValue");
            Console.WriteLine("StringValue is: {0}", str);

            // Of course nothing forbids you of using the type when reading string values
            settings.GetValue<string>("StringValue");
        }

        public static void ReadStandardDataTypes()
        {
            // Standard .NET data types can be read type safely
            var doubleValue = settings.GetValue<double>("DoubleValue");
            Console.WriteLine("DoubleValue is {0}", doubleValue);
        }

        public static void ReadEnumerations()
        {
            // You can read enumerations which are stored as strings
            var projectStatus = settings.GetValue<ProjectStatus>("EnumString");
            Console.WriteLine("Enum saved as string has value: {0}", projectStatus);

            // You can also read enumerations which are stored as integers
            projectStatus = settings.GetValue<ProjectStatus>("EnumNumeric");
            Console.WriteLine("Enum saved as integer has value: {0}", projectStatus);
        }

        public static void ReadNullableValues()
        {
            // Nullable values are also supported. When you are reading Nullable value
            // empty string in the app.config is considered as a null value
            var nullableIntValue = settings.GetValue<int?>("EmptyIntValue");
            Console.WriteLine("EmptyIntValue HasValue returns: {0}", nullableIntValue.HasValue);

            // If the value is not empty you can access it using the .Value property
            var doubleValue = settings.GetValue<double?>("DoubleValue");
            Console.WriteLine("DoubleValue is {0}", doubleValue.Value);            
        }

        public static void ReadUsingCustomFormatProvider()
        {
            // Sometimes you might have values which have been saved using
            // different culture. E.g. double value might have been saved
            // using comma as decimal separator. In those cases you can
            // use custom format provider
            var doubleValue = settings.GetValue<double>("DoubleWithFinnishLocale", CultureInfo.GetCultureInfo("fi-FI"));
            Console.WriteLine("Double with finnish locale is: {0}", doubleValue);
        }

        public static void ReadOptionalValues()
        {
            // If the setting is optional then you can specify
            // default value which is returned in case setting does not exist
            var value = settings.GetOptionalValue("NonExistingSetting", "DefaultValue");
            Console.WriteLine("Value for NonExistingSetting is: {0}", value);

            // Of course you can use default values type safely
            var intValue = settings.GetOptionalValue<int>("NonExistingIntValue", 123);
            Console.WriteLine("Value for NonExistingIntValue is: {0}", intValue);
        }

        public static void ReadUsingCustomConversionFunction()
        {
            // Sometimes you want to specify custom converson function for your value
            var value = settings.GetValue<double>("DoubleValue", (setting, rawValue) 
                => double.Parse(rawValue, CultureInfo.InvariantCulture) * 10);
            Console.WriteLine("DoubleValue using custom conversion function is: {0}", value);
        }

        public static void ReadConnectionString()
        {
            var cs = settings.GetConnectionString("MyDatabase");
        }

        public static void ReadingCustomConfigurationFile()
        {
            // If your .config fle is named something else you can
            // give relative or absolute path to that file
            var custom = new AppSettings(@"Custom.config", FileOption.FileMustExist);
            Console.WriteLine("Value in Custom.config is {0}", custom.GetValue("Custom"));
        }

        public static void UpdatingValues()
        {
            Console.WriteLine("Updating values");

            // You can update the setting value simply using SetValue
            settings.SetValue("StringValue", "new value");
            settings.SetValue<double>("DoubleValue", 1.2d);
            settings.SetConnectionString("MyDatabase", "Data Source=localhost;Initial Catalog=MyDb;User Id=username;Password=newpassword;");

            Console.WriteLine("Saving settings");
            settings.Save();
        }

        public static void CreatingAppSettings()
        {
            // Creating for the entry point of your program. Entry
            // point can be foo.exe (foo.exe.config) or foo.dll (foo.dll.config)
            var entryPoint = AppSettings.CreateForAssembly(Assembly.GetEntryAssembly(), FileOption.None);
            
            // If you call CreateForCallingAssembly from foo.dll then
            // the name of the configuration file should be foo.dll.config
            AppSettings.CreateForCallingAssembly(FileOption.None);

            // If your settings are stored under AppData folder you can create
            // AppSettings for the current user.
            var currentUserOfApp = AppSettings.CreateForCurrentUser(Assembly.GetEntryAssembly(), "MyApplication", FileOption.None);
            Console.WriteLine("Configuration for current user points to {0}", currentUserOfApp.FullPath);
        }
    }
}
