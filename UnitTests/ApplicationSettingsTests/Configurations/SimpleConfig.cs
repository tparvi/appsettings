namespace ApplicationSettingsTests.Configurations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class SimpleConfig
    {
        public static readonly string SimpleConfigFile = "simple.config";
        public static readonly string NonEmptyStringValue = "NonEmptyStringValue";
        public static readonly string EmptyStringValue = "EmptyStringValue";
        public static readonly string NullStringValue = "NullStringValue";

        public static readonly string IntValue = "IntValue";
        public static readonly string DoubleValue = "DoubleValue";
        public static readonly string DecimalValue = "DecimalValue";

        public static readonly string ConnectionString = "Data Source=localhost;Initial Catalog=MyDb;User Id=username;Password=password;";

        public static readonly string ConnectionStringName = "MyDatabase";

        public static string AbsolutePathToSimpleConfigFile
        {
            get
            {
                return TestHelpers.GetFullPathToConfigurationFile(SimpleConfigFile);
            }
        }
    }
}
