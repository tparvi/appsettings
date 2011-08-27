namespace ApplicationSettingsTests.SaveTests
{
    using System;

    using ApplicationSettings;

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
    }
}
