namespace ApplicationSettingsTests.Configurations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class NoAppSettingsConfig
    {
        public static readonly string ConfigFile = "NoAppSettings.config";

        public static string AbsolutePathToConfigFile
        {
            get
            {
                return TestHelpers.GetFullPathToConfigurationFile(ConfigFile);
            }
        }
    }
}
