namespace ApplicationSettingsTests.SaveTests
{
    using System;

    using ApplicationSettings;

    using ApplicationSettingsTests.Configurations;

    using NUnit.Framework;

    [TestFixture]
    public class When_saving_settings : TestBase
    {
        [Test]
        public void And_settings_file_does_not_exist_it_should_be_created()
        {
            var fileName = Guid.NewGuid() + ".config";
            var fullPathToConfigurationFile = TestHelpers.GetFullPathToConfigurationFile(fileName);

            try
            {
                Assert.IsFalse(System.IO.File.Exists(fullPathToConfigurationFile));

                var settings = new AppSettings(fullPathToConfigurationFile, FileOption.None);

                settings.SetValue("string", "a");
                settings.SetValue<int>("int", 1);
                settings.SetValue<int?>("nullableint", null);

                settings.Save();

                Assert.IsTrue(System.IO.File.Exists(fullPathToConfigurationFile));
                Assert.IsTrue(settings.FileExists);
            }
            finally 
            {
                TestHelpers.DeleteIfExists(fullPathToConfigurationFile);
            }
        }

        [Test]
        public void Then_values_should_be_saved_using_invariant_culture()
        {
            var originalFile = SimpleConfig.AbsolutePathToConfigFile;
            var tempFile = TestHelpers.CreateCopyOfFile(originalFile);

            try
            {
                var settings = new AppSettings(tempFile, FileOption.FileMustExist);
                settings.SetValue<double>("OtherDouble", 1.1);

                settings.Save();

                var otherSettings = new AppSettings(tempFile, FileOption.FileMustExist);
                var value = otherSettings.GetValue<double>("OtherDouble");

                Assert.AreEqual(1.1d, value);
            }
            finally
            {
                TestHelpers.DeleteIfExists(tempFile);
            }            
        }
    }
}
