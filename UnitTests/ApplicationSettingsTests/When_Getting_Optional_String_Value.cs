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
    public class When_Getting_Optional_String_Value : TestBase
    {
        [Test]
        public void And_setting_exists_then_its_value_is_returned()
        {
            var settings = new AppSettings(SimpleConfig.AbsolutePathToConfigFile);

            var value = settings.GetOptionalValue(SimpleConfig.NonEmptyStringValue, null);

            Assert.AreEqual("abc", value);
        }

        [Test]
        public void And_setting_does_not_exist_then_default_value_is_returned()
        {
            var settings = new AppSettings(SimpleConfig.AbsolutePathToConfigFile);

            var defaultValue = "default value";
            var value = settings.GetOptionalValue("NonExistingParameter", defaultValue);

            Assert.AreEqual(defaultValue, value);
        }

        [Test]
        public void And_AppSetting_section_does_not_exist_default_value_is_returned()
        {
            var settings = new AppSettings(NoAppSettingsConfig.AbsolutePathToConfigFile);

            var defaultValue = "default value";
            var value = settings.GetOptionalValue("NonExistingParameter", defaultValue);

            Assert.AreEqual(defaultValue, value);
        }
    }
}
