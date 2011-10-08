namespace ApplicationSettingsTests.SaveTests
{
    using System;

    using ApplicationSettings;

    using ApplicationSettingsTests.Configurations;

    using NUnit.Framework;

    [TestFixture]
    public class Read_existing_settings_modify_save_and_read_again : TestBase
    {
        [Test]
        public void Should_succeed()
        {
            var originalFile = SimpleConfig.AbsolutePathToConfigFile;
            var tempFile = TestHelpers.CreateCopyOfFile(originalFile);

            try
            {
                var settings = new AppSettings(tempFile, FileOption.FileMustExist);

                // Read existing settings
                settings.GetValue(SimpleConfig.NonEmptyStringValue);
                settings.GetValue<int>(SimpleConfig.IntValue);
                settings.GetValue<int?>(SimpleConfig.EmptyIntValue);
                settings.GetValue<double>(SimpleConfig.DoubleValue);

                // Modify settings
                settings.SetValue(SimpleConfig.NonEmptyStringValue, "nonEmptyValue");
                settings.SetValue<int>(SimpleConfig.IntValue, int.MinValue);
                settings.SetValue<int?>(SimpleConfig.EmptyIntValue, 1);
                settings.SetConnectionString("MyDatabase", "db");

                settings.Save();

                var otherSettings = new AppSettings(tempFile, FileOption.FileMustExist);
                var nonEmptyString = otherSettings.GetValue(SimpleConfig.NonEmptyStringValue);
                var intValue = otherSettings.GetValue<int>(SimpleConfig.IntValue);
                var emptyIntValue = otherSettings.GetValue<int?>(SimpleConfig.EmptyIntValue);
                var doubleValue = otherSettings.GetValue<double>(SimpleConfig.DoubleValue);

                Assert.AreEqual("nonEmptyValue", nonEmptyString);
                Assert.AreEqual(int.MinValue, intValue);
                Assert.AreEqual(1, emptyIntValue);
                Assert.AreEqual(1.1d, doubleValue);
            }
            finally 
            {
                TestHelpers.DeleteIfExists(tempFile);
            }
        }
    }
}