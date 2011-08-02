namespace ApplicationSettingsTests.Configurations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class EmptyConnectionStrings
    {
        public static readonly string ConfigurationFile = "EmptyConnectionStrings.config";

        public static string AbsolutePathToConfigFile
        {
            get
            {
                return TestHelpers.GetFullPathToConfigurationFile(ConfigurationFile);
            }
        }
    }
}
