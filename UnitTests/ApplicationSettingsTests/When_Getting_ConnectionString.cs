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
    public class When_Getting_ConnectionString : TestBase
    {
        [Test]
        public void Then_connection_string_should_be_returned()
        {
            var settings = new AppSettings(SimpleConfig.AbsolutePathToSimpleConfigFile);

            var value = settings.ConnectionString;

            Assert.AreEqual(SimpleConfig.ConnectionString, value);
        }

        [Test]
        public void And_multiple_connection_string_exist_then_exception_is_thrown()
        {
            var settings = new AppSettings(MultipleConnectionStringsConfig.AbsolutePathToConfigFile);

            Assert.Throws<AppSettingException>(() => { var cs = settings.ConnectionString; });
        }

        [Test]
        public void And_there_are_no_connection_strings_exception_is_thrown()
        {
            var settings = new AppSettings(EmptyConnectionStrings.AbsolutePathToConfigFile);

            Assert.Throws<AppSettingException>(() => { var cs = settings.ConnectionString; });
        }
    }
}
