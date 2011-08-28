namespace ApplicationSettingsTests.WriteIntoInstanceTests
{
    using ApplicationSettings;

    using ApplicationSettingsTests.Configurations;

    using NUnit.Framework;

    [TestFixture]
    public class When_both_IgnoreAndSetting_are_defined : TestBase
    {
        [Test]
        public void Then_property_is_ignored()
        {
            var settings = new AppSettings(SimpleConfig.AbsolutePathToConfigFile);
            var mySettings = new Settings(100);

            settings.WriteInto(mySettings);

            Assert.AreEqual(100, mySettings.IntValue);
        }
    }

    public class Settings
    {
        public Settings(int intValue)
        {
            this.IntValue = intValue;
        }

        // Since both attributes are defined we use only IgnoredProperty

        [IgnoreProperty]
        [SettingProperty(SettingName = "IntValue")]
        public int IntValue { get; private set; }
    }
}
