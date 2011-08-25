namespace ApplicationSettingsTests
{
    using ApplicationSettings;

    using ApplicationSettingsTests.Configurations;

    using NUnit.Framework;

    [TestFixture]
    public class When_Getting_Mandatory_String_Value : TestBase
    {
        [Test]
        public void Then_value_should_be_returned()
        {
            var settings = new AppSettings(SimpleConfig.AbsolutePathToConfigFile);

            var value = settings.GetValue(SimpleConfig.NonEmptyStringValue);

            Assert.AreEqual("abc", value);
        }

        [Test]
        public void And_value_is_null_Then_empty_value_should_be_returned()
        {
            // The functionality is the same as using standard Configuration/ConfigurationManager.
            // If the value="" part is missing then empty string is returned. It would make
            // sense that it would return null but that is not the case

            var settings = new AppSettings(SimpleConfig.AbsolutePathToConfigFile);

            var value = settings.GetValue(SimpleConfig.NullStringValue);

            Assert.AreEqual(string.Empty, value);
        }


        [Test]
        public void And_value_is_empty_Then_value_should_be_returned()
        {
            var settings = new AppSettings(SimpleConfig.AbsolutePathToConfigFile);

            var value = settings.GetValue(SimpleConfig.EmptyStringValue);

            Assert.AreEqual(string.Empty, value);
        }

        [Test]
        public void And_setting_does_not_exist_exception_is_thrown()
        {
            var settings = new AppSettings(SimpleConfig.AbsolutePathToConfigFile);

            Assert.Throws<AppSettingException>(() => settings.GetValue("NonExistingSetting"));
        }

        [Test]
        public void And_AppSetting_section_does_not_exist_exception_is_thrown()
        {
            var settings = new AppSettings(NoAppSettingsConfig.AbsolutePathToConfigFile);

            Assert.Throws<AppSettingException>(() => settings.GetValue("NonExistingSetting"));
        }

        [Test]
        public void And_file_does_not_exist_Then_exception_is_thrown()
        {
            var settings = new AppSettings("NonExistingConfig.config", FileOption.None);

            Assert.Throws<AppSettingException>(() => settings.GetValue("NonExistingSetting"));
        }
    }
}
