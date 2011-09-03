namespace ApplicationSettingsTests.ReadFromInstanceTests
{
    using System.Globalization;

    using ApplicationSettings;

    using ApplicationSettingsTests.Configurations;

    using NUnit.Framework;

    [TestFixture]
    public class When_SettingAttribute_is_defined : TestBase
    {
        [Test]
        public void Then_SettingName_is_used()
        {
            var settings = new AppSettings("filename", FileOption.None);
            var mySettings = new SettingsWithSettingsAttribute() { Value = 1 };

            settings.ReadFrom(mySettings);

            Assert.AreEqual(1, settings.GetValue<int>("SomeIntValue"));
        }

        [Test]
        public void And_SettigName_is_null_Then_property_name_is_used()
        {
            var settings = new AppSettings("filename", FileOption.None);
            var mySettings = new SettingsWithNullSettingName() { IntValue = 1 };

            settings.ReadFrom(mySettings);

            Assert.AreEqual(1, settings.GetValue<int>("IntValue"));
        }

        [Test]
        public void Then_CultureName_is_used()
        {
            var settings = new AppSettings("filename", FileOption.None);
            var mySettings = new SettingsWithCultureName() { FinnishLocaleDoubleValue = 1.1d };

            settings.ReadFrom(mySettings);

            Assert.AreEqual(1.1d, settings.GetValue<double>("DoubleWithFinnishLocale", CultureInfo.GetCultureInfo("fi-FI")));
            Assert.AreEqual("1,1", settings.GetValue("DoubleWithFinnishLocale"));
        }

        [Test]
        public void And_CultureName_is_null_Then_InvariantCulture_is_used()
        {
            var settings = new AppSettings("filename", FileOption.None);
            var mySettings = new SettingsWithNullCultureName() { FinnishLocaleDoubleValue = 1.1d };

            settings.ReadFrom(mySettings);
            
            // Since we are not using specific locale the InvariantCulture writes
            // the value normally 1.1. If we would have specified fi-FI locale the
            // value would have been 1,1
            Assert.AreEqual("1.1", settings.GetValue("DoubleWithFinnishLocale"));
            Assert.AreEqual(1.1d, settings.GetValue<double>("DoubleWithFinnishLocale"));
        }

        [Test]
        public void And_IsConnectionString_is_true_then_setting_is_saved_as_connection_string()
        {
            var settings = new AppSettings("filename", FileOption.None);
            var mySettings = new SettingsWithConnectionString()
                {
                    DatabaseConnectionString = @"Data Source=localhost;Initial Catalog=MyDb;User Id=username;Password=password;"
                };

            settings.ReadFrom(mySettings);

            var value = settings.GetConnectionString("MyDb");
            Assert.AreEqual(value, mySettings.DatabaseConnectionString);
        }
    }

    public class SettingsWithConnectionString
    {
        [SettingProperty(SettingName = "MyDb", IsConnectionString = true)]
        public string DatabaseConnectionString { get; set; }
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
        [SettingProperty(SettingName = "SomeIntValue")]
        public int Value { get; set; }
    }
}
