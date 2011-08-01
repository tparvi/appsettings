namespace ApplicationSettingsTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class TestBase
    {
        public static readonly string SimpleConfigFile = "simple.config";

        public static readonly string NonExistingConfigFile = "NonExistingConfig.config";

        public string AbsolutePathToSimpleConfigFile
        {
            get
            {
                return TestHelpers.GetFullPathToConfigurationFile(SimpleConfigFile);
            }
        }
    }
}
