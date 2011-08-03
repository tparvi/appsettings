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
    public class When_Getting_Mandatory_Value : TestBase
    {
        [Test]
        public void And_value_is_int_Then_value_should_be_returned()
        {
            var settings = new AppSettings(SimpleConfig.AbsolutePathToSimpleConfigFile);

            var value = settings.GetValue<int>(SimpleConfig.IntValue);

            Assert.AreEqual(1, value);
        }

        [Test]
        public void And_value_is_double_Then_value_should_be_returned()
        {
            var settings = new AppSettings(SimpleConfig.AbsolutePathToSimpleConfigFile);

            var value = settings.GetValue<double>(SimpleConfig.DoubleValue);

            Assert.AreEqual(1.1, value);
        }

        [Test]
        public void And_value_is_decimal_Then_value_should_be_returned()
        {
            var settings = new AppSettings(SimpleConfig.AbsolutePathToSimpleConfigFile);

            var value = settings.GetValue<decimal>(SimpleConfig.DecimalValue);

            Assert.AreEqual(1.1, value);
        }

        [Test]
        public void And_custom_conversion_function_is_specified_it_should_be_used()
        {
            var settings = new AppSettings(SimpleConfig.AbsolutePathToSimpleConfigFile);
            var functionCalled = false;

            settings.GetValue<decimal>(SimpleConfig.DecimalValue, (setting, settingValue) => { functionCalled = true; return 0.0m; });

            Assert.IsTrue(functionCalled);
        }

        [Test]
        public void And_custom_conversion_function_is_specified_its_value_should_be_returned()
        {
            var settings = new AppSettings(SimpleConfig.AbsolutePathToSimpleConfigFile);
            var expectedValue = 100.123m;

            var value = settings.GetValue<decimal>(SimpleConfig.DecimalValue, (setting, settingValue) => expectedValue);

            Assert.AreEqual(expectedValue, value);
        }

        [Test]
        public void And_setting_does_not_exist_exception_is_thrown()
        {
            var settings = new AppSettings(SimpleConfig.AbsolutePathToSimpleConfigFile);

            Assert.Throws<AppSettingException>(() => settings.GetValue<int>("NonExistingSetting"));
        }

        [Test]
        public void And_value_cannot_be_converted_exception_is_thrown()
        {
            var settings = new AppSettings(SimpleConfig.AbsolutePathToSimpleConfigFile);

            Assert.Throws<AppSettingException>(() => settings.GetValue<decimal>(SimpleConfig.NonEmptyStringValue));
        }
    }
}
