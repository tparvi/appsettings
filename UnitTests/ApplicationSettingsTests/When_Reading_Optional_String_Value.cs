using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationSettingsTests
{
    using System.Reflection;

    using ApplicationSettings;

    using ApplicationSettingsTests.Configurations;

    using NUnit.Framework;

    [TestFixture]
    public class When_Reading_Optional_String_Value : TestBase
    {
        [Test]
        public void And_setting_exists_then_its_value_is_returned()
        {
            //var settings = new AppSettings(SimpleConfig.AbsolutePathToSimpleConfigFile);
            Assert.Inconclusive("Not implemented");
        }

    }
}
