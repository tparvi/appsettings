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
    public class When_Reading_User_Given_Configuration_File : TestBase
    {
        [Test]
        public void Then_reading_absolute_path_should_succeed()
        {
            var fileName = SimpleConfig.AbsolutePathToConfigFile;

            var settings = new AppSettings(fileName);

            Assert.AreEqual(fileName, settings.FullPath);
            Assert.IsTrue(settings.FileExists);            
        }

        [Test]
        public void Then_relative_path_should_be_converted_to_aboslute_path()
        {   
            // Using only file name so that the path is relative         
            var settings = new AppSettings(TestHelpers.GetRelativePathToConfigurationFile(SimpleConfig.ConfigFile));

            // This is the absolute path to the file
            Assert.AreEqual(SimpleConfig.AbsolutePathToConfigFile, settings.FullPath);
            Assert.IsTrue(settings.FileExists);                        
        }

        [Test]
        public void Then_if_filepath_is_uri_exception_is_thrown()
        {
            Assert.Throws<AppSettingException>(() => { new AppSettings(@"file:////foo.txt"); });
        }


        [Test]
        public void And_file_does_not_exist_then_exception_is_thrown()
        {
            var fileName = TestHelpers.GetFullPathToConfigurationFile(NonExistingConfigFile);

            Assert.Throws<AppSettingException>(() => { new AppSettings(fileName); });
        }
    }
}
