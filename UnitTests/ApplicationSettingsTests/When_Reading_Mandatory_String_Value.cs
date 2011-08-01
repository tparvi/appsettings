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
    public class When_Reading_Mandatory_String_Value : TestBase
    {
        [Test]
        public void Then_value_should_be_returned()
        {
            var settings = new AppSettings(SimpleConfig.AbsolutePathToSimpleConfigFile);

            var value = settings.GetValue(SimpleConfig.NonEmptyStringValue);

            Assert.AreEqual("abc", value);
        }

        [Test]
        public void And_value_is_empty_Then_value_should_be_returned()
        {
            var settings = new AppSettings(SimpleConfig.AbsolutePathToSimpleConfigFile);

            var value = settings.GetValue(SimpleConfig.EmptyStringValue);

            Assert.AreEqual(string.Empty, value);
        }

        [Test]
        public void And_setting_does_not_exist_exception_is_thrown()
        {
            var settings = new AppSettings(SimpleConfig.AbsolutePathToSimpleConfigFile);

            Assert.Throws<AppSettingException>(() => settings.GetValue("NonExistingSetting"));
        }

        [Test]
        public void And_AppSetting_section_does_not_exist()
        {
            Assert.Inconclusive("Not implemented");
        }
    }
}
