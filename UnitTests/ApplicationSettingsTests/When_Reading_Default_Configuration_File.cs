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
    public class When_Reading_Default_Configuration_File : TestBase
    {
        [Test]
        public void Then_reading_should_succeed()
        {
            // Since we are inside .dll the configuration file name
            // is ApplicationSettingsTests.dll.config

            var settings = new AppSettings();

            Assert.IsTrue(settings.FileExists);            
        }
    }
}
