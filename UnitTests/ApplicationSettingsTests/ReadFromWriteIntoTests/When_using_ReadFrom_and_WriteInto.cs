namespace ApplicationSettingsTests.ReadFromInstanceTests
{
    using System;
    using System.Globalization;

    using ApplicationSettings;

    using ApplicationSettingsTests.Configurations;

    using NUnit.Framework;

    [TestFixture]
    public class When_using_ReadFrom_and_WriteInto : TestBase
    {
        [Test]
        public void Then_read_and_write_should_succeed()
        {
            var fileName = Guid.NewGuid() + ".config";
            var fullPathToConfigurationFile = TestHelpers.GetFullPathToConfigurationFile(fileName);

            try
            {
                var settings = new AppSettings(fullPathToConfigurationFile, FileOption.None);
                var mySettings = new TempSettings()
                    {
                        NonEmptyStringValue = "aaa",
                        IntValue = 123,
                        DoubleValue = 123.12d,
                        LocalizedValue = 456.789
                    };

                settings.ReadFrom(mySettings);

                Assert.AreEqual("aaa", settings.GetValue("NonEmptyStringValue"));
                Assert.IsFalse(settings.HasAppSetting("IntValue"));
                Assert.AreEqual(null, settings.GetValue<int?>("EmptyIntValue"));
                Assert.AreEqual(123.12d, settings.GetValue<double>("DoubleValue"));
                Assert.AreEqual(456.789, settings.GetValue<double>("DoubleFinnishLocale", CultureInfo.GetCultureInfo("fi-FI")));
                Assert.AreEqual("456,789", settings.GetValue("DoubleFinnishLocale"));

                settings.Save();

                var otherSettings = new TempSettings();
                settings.WriteInto(otherSettings);

                Assert.AreEqual("aaa", otherSettings.NonEmptyStringValue);
                Assert.AreEqual(0, otherSettings.IntValue);
                Assert.AreEqual(null, otherSettings.NullableIntValue);
                Assert.AreEqual(123.12d, otherSettings.DoubleValue);
                Assert.AreEqual(456.789, otherSettings.LocalizedValue);
            }
            finally 
            {
                TestHelpers.DeleteIfExists(fullPathToConfigurationFile);
            }
        }
    }

    public class TempSettings
    {
        public string NonEmptyStringValue { get; set; }
        
        [IgnoreProperty]
        public int IntValue { get; set; }
        
        [SettingProperty(SettingName = "EmptyIntValue")]
        public int? NullableIntValue { get; set; }
        
        public double DoubleValue { get; set; }

        [SettingProperty(CultureName = "fi-FI", SettingName = "DoubleFinnishLocale")]
        public double LocalizedValue { get; set; }
    }
}
