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
        public void And_setting_does_not_exist_exception_is_thrown()
        {
            var settings = new AppSettings(SimpleConfig.AbsolutePathToSimpleConfigFile);

            Assert.Throws<AppSettingException>(() => settings.GetValue<int>("NonExistingSetting"));
        }
    }
}
