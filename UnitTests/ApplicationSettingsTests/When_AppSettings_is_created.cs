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

        [Test]
        public void Then_if_filepath_is_uri_exception_is_thrown()
        {
            Assert.Throws<AppSettingException>(() => { new AppSettings(@"file:////foo.txt"); });
        }

        [Test]
        public void Then_relative_path_should_be_converted_to_aboslute_path()
        {   
            // Using only file name so that the path is relative         
            var settings = new AppSettings(SimpleConfigFile);

            // This is the absolute path to the file
            var absoluteFileName = TestHelpers.GetFullPathToConfigurationFile(SimpleConfigFile);
            Assert.AreEqual(absoluteFileName, settings.FullPath);
            Assert.IsTrue(settings.FileExists);                        
        }

        [Test]
        public void With_non_existing_file_should_succeed()
        {
            var fileName = TestHelpers.GetFullPathToConfigurationFile(NonExistingConfigFile);
            
            var settings = new AppSettings(fileName);
            
            Assert.AreEqual(fileName, settings.FullPath);
            Assert.IsFalse(settings.FileExists);                        
        }
    }
}
