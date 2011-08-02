namespace ApplicationSettingsTests.Configurations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class MultipleConnectionStringsConfig
    {
        public static readonly string ConfigurationFile = "MultipleConnectionStrings.config";

        public static string AbsolutePathToConfigFile
        {
            get
            {
                return TestHelpers.GetFullPathToConfigurationFile(ConfigurationFile);
            }
        }
    }
}
