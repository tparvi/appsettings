using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationSettingsTests
{
    using System.Globalization;
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
            var settings = new AppSettings(SimpleConfig.AbsolutePathToConfigFile);

            var anyDefaultValue = 100;
            var value = settings.GetOptionalValue<int>(SimpleConfig.IntValue, anyDefaultValue);

            Assert.AreEqual(1, value);
        }

        [Test]
        public void And_setting_does_not_exist_then_default_value_is_returned()
        {
            var settings = new AppSettings(SimpleConfig.AbsolutePathToConfigFile);

            var defaultValue = 100;
            var value = settings.GetOptionalValue<int>("NonExistingSetting", defaultValue);

            Assert.AreEqual(defaultValue, value);

        }

        [Test]
        public void And_custom_conversion_function_is_specified_it_should_be_used()
        {
            var settings = new AppSettings(SimpleConfig.AbsolutePathToConfigFile);
            var functionCalled = false;

            settings.GetOptionalValue<int>(SimpleConfig.IntValue, 100, (setting, settingValue) => { functionCalled = true; return 0; });

            Assert.IsTrue(functionCalled);
        }

        [Test]
        public void And_custom_conversion_function_is_specified_its_value_should_be_returned()
        {
            var settings = new AppSettings(SimpleConfig.AbsolutePathToConfigFile);
            var expectedValue = 100;

            var value = settings.GetOptionalValue<int>(SimpleConfig.IntValue, expectedValue, (setting, settingValue) => expectedValue);

            Assert.AreEqual(expectedValue, value);
        }

        [Test]
        public void And_CultureInfo_is_specified_it_should_be_used()
        {
            var settings = new AppSettings(SimpleConfig.AbsolutePathToConfigFile);
            var expectedValue = 1.1;
            var anyDefaultValue = 0.0;

            var value = settings.GetOptionalValue<double>(SimpleConfig.DoubleWithFinnishLocale, anyDefaultValue, CultureInfo.GetCultureInfo("fi-FI"));

            Assert.AreEqual(expectedValue, value);
        }

        [Test]
        public void And_AppSetting_section_does_not_exist_default_value_is_returned()
        {
            var settings = new AppSettings(NoAppSettingsConfig.AbsolutePathToConfigFile);

            var defaultValue = 100;
            var value = settings.GetOptionalValue<int>("NonExistingSetting", defaultValue);

            Assert.AreEqual(defaultValue, value);

        }

        [Test]
        public void And_value_cannot_be_converted_exception_is_thrown()
        {
            var settings = new AppSettings(SimpleConfig.AbsolutePathToConfigFile);

            Assert.Throws<AppSettingException>(() => settings.GetOptionalValue<decimal>(SimpleConfig.NonEmptyStringValue, 1.1m));
        }
    }
}
