namespace ApplicationSettingsTests.CreationTests
{
    using ApplicationSettings;

    using ApplicationSettingsTests.Configurations;

    using NUnit.Framework;

    [TestFixture]
    public class When_using_filename_and_option : TestBase
    {
        [Test]
        public void And_Option_is_FileMustExis_and_file_does_not_exist_exception_is_thrown()
        {
            Assert.Throws<AppSettingException>(() =>
                {
                    new AppSettings("NonExistingFile.config", FileOption.FileMustExist);
                });
        }

        [Test]
        public void And_Option_is_None_and_file_does_not_exist_FileExists_returns_false()
        {
            var settings = new AppSettings("NonExistingFile.config", FileOption.None);

            Assert.IsFalse(settings.FileExists);
        }
    }
}
