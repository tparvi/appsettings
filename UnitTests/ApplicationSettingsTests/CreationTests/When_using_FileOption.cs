namespace ApplicationSettingsTests.CreationTests
{
    using System.IO;

    using ApplicationSettings;

    using ApplicationSettingsTests.Configurations;

    using NUnit.Framework;

    [TestFixture]
    public class When_using_FileOption : TestBase
    {
        [Test]
        public void And_option_is_None_default_context_specific_file_should_be_used()
        {
            var settings = new AppSettings(FileOption.None);

            var fileName = Path.GetFileName(settings.FullPath);

            // Since we are inside dll the context points to dll specific .config file
            Assert.AreEqual("ApplicationSettingsTests.dll.config", fileName);
            Assert.IsTrue(settings.FileExists);            
        }

        [Test]
        public void And_option_is_FileMustExist_creation_should_succed()
        {
            var settings = new AppSettings(FileOption.FileMustExist);

            var fileName = Path.GetFileName(settings.FullPath);

            // Since we are inside dll the context points to dll specific .config file
            Assert.AreEqual("ApplicationSettingsTests.dll.config", fileName);
            Assert.IsTrue(settings.FileExists);            
        }
    }
}
