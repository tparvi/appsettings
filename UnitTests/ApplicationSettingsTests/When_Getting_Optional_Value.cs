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
    public class When_Getting_Optional_Value : TestBase
    {
        [Test]
        public void And_setting_exists_then_its_value_is_returned()
        {
            var settings = new AppSettings(SimpleConfig.AbsolutePathToSimpleConfigFile);

            var anyDefaultValue = 100;
            var value = settings.GetOptionalValue<int>(SimpleConfig.IntValue, anyDefaultValue);

            Assert.AreEqual(1, value);
        }

        [Test]
        public void And_setting_does_not_exist_then_default_value_is_returned()
        {
            var settings = new AppSettings(SimpleConfig.AbsolutePathToSimpleConfigFile);

            var defaultValue = 100;
            var value = settings.GetOptionalValue<int>("NonExistingSetting", defaultValue);

            Assert.AreEqual(defaultValue, value);

        }

        [Test]
        public void And_AppSetting_section_does_not_exist()
        {
            Assert.Inconclusive("Not implemented");
        }

        [Test]
        public void And_value_cannot_be_converted_exception_is_thrown()
        {
            var settings = new AppSettings(SimpleConfig.AbsolutePathToSimpleConfigFile);

            Assert.Throws<AppSettingException>(() => settings.GetOptionalValue<decimal>(SimpleConfig.NonEmptyStringValue, 1.1m));
        }
    }
}
