using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleExample
{
    using System.Globalization;

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
                // and it is located in the same folder as the SimpleExample.exe
                settings = new AppSettings();

                ReadStringValues();
                ReadStandardDataTypes();
                ReadEnumerations();
                ReadNullableValues();
                ReadUsingCustomFormatProvider();
                ReadOptionalValues();
                ReadUsingCustomConversionFunction();
                ReadConnectionString();
            }
            catch (Exception exp)
            {
                Console.Error.WriteLine(exp.Message);
            }
        }

        public static void ReadStringValues()
        {
            // String values can be read without having to specify the type. This avoids the
            // unnecessary type conversion.
            var firstStringValue = settings.GetValue("FirstStringValue");
            Console.WriteLine("FirstStringValue is: {0}", firstStringValue);

            // Of course nothing forbids you of using the type when reading string values
            var secondStringValue = settings.GetValue<string>("SecondStringValue");
            Console.WriteLine("SecondStringValue is: {0}", secondStringValue);            
        }

        public static void ReadStandardDataTypes()
        {
            // Standard .NET data types can be read type safely
            var intValue = settings.GetValue<int>("IntValue");
            Console.WriteLine("IntValue is {0}", intValue);

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
            var intValue = settings.GetValue<int?>("IntValue");
            Console.WriteLine("IntValue is {0}", intValue.Value);            
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
            var value = settings.GetValue<int>("SecondIntValue", (setting, rawValue) => int.Parse(rawValue) * 10);

            Console.WriteLine("SecondIntValue using custom conversion function is: {0}", value);
        }

        public static void ReadConnectionString()
        {
            // If you have only single connection string you can access it by
            // using the ConnectionString property. Unfortunately .NET configuration
            // files inherit connection string from upper level configurations and
            // your machine.config is bound to have some. Fortunately you can add
            // the <clear/> tag to get rid of them. See the app.config file for example
            var cs = settings.ConnectionString;
            Console.WriteLine("ConnectionString is: {0}", cs);

            // Of course you can still access connection string by their name
            cs = settings.GetConnectionString("MyDatabase");
        }
    }
}
