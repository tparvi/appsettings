using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationSettingsTests
{
    using System.Reflection;

    using ApplicationSettings;

    using NUnit.Framework;

    [TestFixture]
    public class When_AppSettings_is_created : TestBase
    {
        [Test]
        public void Then_it_should_read_configuration_file_given_to_it()
        {
            var fileName = TestHelpers.GetFullPathToConfigurationFile(SimpleConfigFile);
            var settings = new AppSettings(fileName);
            Assert.AreEqual(fileName, settings.FullPath);
            Assert.IsTrue(settings.FileExists);            
        }
    }
}
