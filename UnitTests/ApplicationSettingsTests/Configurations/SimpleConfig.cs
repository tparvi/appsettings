namespace ApplicationSettingsTests.Configurations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class SimpleConfig
    {
        public static readonly string ConfigFile = "simple.config";
        public static readonly string NonEmptyStringValue = "NonEmptyStringValue";
        public static readonly string EmptyStringValue = "EmptyStringValue";
        public static readonly string NullStringValue = "NullStringValue";

        public static readonly string IntValue = "IntValue";
        public static readonly string EmptyIntValue = "EmptyIntValue";
        public static readonly string DoubleValue = "DoubleValue";
        public static readonly string DecimalValue = "DecimalValue";
        public static readonly string DoubleWithFinnishLocale = "DoubleWithFinnishLocale";

        public static readonly string ConnectionString = "Data Source=localhost;Initial Catalog=MyDb;User Id=username;Password=password;";

        public static readonly string ConnectionStringName = "MyDatabase";

        public static string AbsolutePathToConfigFile
        {
            get
            {
                return TestHelpers.GetFullPathToConfigurationFile(ConfigFile);
            }
        }
    }
}
