namespace ApplicationSettingsTests.WriteIntoInstanceTests
{
    using ApplicationSettings;

    using ApplicationSettingsTests.Configurations;

    using NUnit.Framework;

    [TestFixture]
    public class When_SettingAttribute_is_defined : TestBase
    {
        [Test]
        public void Then_SettingName_is_used()
        {
            var settings = new AppSettings(SimpleConfig.AbsolutePathToConfigFile);

            var mySettings = new SettingsWithSettingsAttribute();
            settings.WriteInto(mySettings);

            Assert.AreEqual(1, mySettings.Value);
        }

        [Test]
        public void And_SettigName_is_null_Then_property_name_is_used()
        {
            var settings = new AppSettings(SimpleConfig.AbsolutePathToConfigFile);

            var mySettings = new SettingsWithNullSettingName();
            settings.WriteInto(mySettings);

            Assert.AreEqual(1, mySettings.IntValue);
        }

        [Test]
        public void Then_CultureName_is_used()
        {
            var settings = new AppSettings(SimpleConfig.AbsolutePathToConfigFile);

            var mySettings = new SettingsWithCultureName();
            settings.WriteInto(mySettings);

            Assert.AreEqual(1.1d, mySettings.FinnishLocaleDoubleValue);
        }

        [Test]
        public void And_CultureName_is_null_Then_InvariantCulture_is_used()
        {
            var settings = new AppSettings(SimpleConfig.AbsolutePathToConfigFile);

            var mySettings = new SettingsWithNullCultureName();
            settings.WriteInto(mySettings);

            // The setting contains value of 1,1 which is valid when using
            // Finnish locale. Since the CultureName is null we are using
            // InvariantCulture which will turn 1,1 into 11.0
            Assert.AreEqual(11.0d, mySettings.FinnishLocaleDoubleValue);
        }

    }

    public class SettingsWithNullCultureName
    {
        [SettingProperty(SettingName = "DoubleWithFinnishLocale")]
        public double FinnishLocaleDoubleValue { get; set; }
    }


    public class SettingsWithCultureName
    {
        [SettingProperty(SettingName = "DoubleWithFinnishLocale", CultureName = "fi-FI")]
        public double FinnishLocaleDoubleValue { get; set; }
    }


    public class SettingsWithNullSettingName
    {
        [SettingProperty]
        public int IntValue { get; set; }
    }


    public class SettingsWithSettingsAttribute
    {
        [SettingProperty(SettingName = "IntValue")]
        public int Value { get; set; }
    }
}
