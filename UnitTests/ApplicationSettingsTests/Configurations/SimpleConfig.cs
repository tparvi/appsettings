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

        public static string AbsolutePathToSimpleConfigFile
        {
            get
            {
                return TestHelpers.GetFullPathToConfigurationFile(SimpleConfigFile);
            }
        }
    }
}
