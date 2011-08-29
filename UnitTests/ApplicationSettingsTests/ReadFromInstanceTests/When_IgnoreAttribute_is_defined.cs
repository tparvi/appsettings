namespace ApplicationSettingsTests.ReadFromInstanceTests
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
            var settings = new AppSettings("filename",FileOption.None);
            var mySettings = new SettingsWithIgnoredProperty(100);

            settings.ReadFrom(mySettings);

            Assert.IsFalse(settings.HasAppSetting("IntValue"));
        }

        [Test]
        public void And_reading_from_it_is_enabled_then_property_is_read_from()
        {
            var settings = new AppSettings("filename", FileOption.None);
            var mySettings = new SettingsWithIgnoredProperty(100);
            mySettings.DoubleValue = 1.1d;

            settings.ReadFrom(mySettings);

            Assert.AreEqual(1.1d, settings.GetValue<double>("DoubleValue"));            
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

        [IgnoreProperty(EnableReading = true)]
        public double DoubleValue { get; set; }
    }
}
