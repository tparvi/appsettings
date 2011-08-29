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

        [Test]
        public void And_writing_for_it_is_enabled_then_property_is_written_into()
        {
            var settings = new AppSettings(SimpleConfig.AbsolutePathToConfigFile);
            var mySettings = new SettingsWithIgnoredProperty(100);

            settings.WriteInto(mySettings);

            Assert.AreEqual(1.1d, mySettings.DoubleValue);
            
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

        [IgnoreProperty(EnableWriting = true)]
        public double DoubleValue { get; set; }
    }
}
