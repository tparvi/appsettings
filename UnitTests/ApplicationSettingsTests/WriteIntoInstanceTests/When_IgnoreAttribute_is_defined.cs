namespace ApplicationSettingsTests.WriteIntoInstanceTests
{
    using ApplicationSettings;

    using ApplicationSettingsTests.Configurations;

    using NUnit.Framework;

    [TestFixture]
    public class When_IgnoreAttribute_is_defined : TestBase
    {
        [Test]
        public void Then_property_is_ignored()
        {
            var settings = new AppSettings(SimpleConfig.AbsolutePathToConfigFile);
            var mySettings = new SettingsWithIgnoredProperty(100);

            settings.WriteInto(mySettings);

            Assert.AreEqual(100, mySettings.IntValue);
        }
    }

    public class SettingsWithIgnoredProperty
    {
        public SettingsWithIgnoredProperty(int intValue)
        {
            this.IntValue = intValue;
        }

        [IgnoreProperty]
        public int IntValue { get; private set; }
    }
}
