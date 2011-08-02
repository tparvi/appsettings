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
    public class When_Getting_ConnectionString_By_Name : TestBase
    {
        [Test]
        public void Then_connection_string_should_be_returned()
        {
            var settings = new AppSettings(SimpleConfig.AbsolutePathToSimpleConfigFile);

            var value = settings.GetConnectionString(SimpleConfig.ConnectionStringName);

            Assert.AreEqual(SimpleConfig.ConnectionString, value);
        }

        [Test]
        public void And_connection_string_does_not_exist_exception_is_thrown()
        {
            var settings = new AppSettings(SimpleConfig.AbsolutePathToSimpleConfigFile);

            Assert.Throws<AppSettingException>(() => settings.GetConnectionString("NonExistingConnectionString"));
        }
    }
}
