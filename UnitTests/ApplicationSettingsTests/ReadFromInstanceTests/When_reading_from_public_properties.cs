namespace ApplicationSettingsTests.ReadFromInstanceTests
{
    using System.Configuration;

    using ApplicationSettings;

    using ApplicationSettingsTests.Configurations;

    using NUnit.Framework;

    [TestFixture]
    public class When_reading_from_public_properties : TestBase
    {
        [Test]
        public void Then_all_public_properties_should_be_get()
        {
            var nonEmptyString = "a";
            var anyInt = 1;

            var settings = new AppSettings("filename", FileOption.None);
            
            var mySettings = new SettingsWithPublicGetters(nonEmptyString, anyInt, null);
            mySettings.DoubleValue = 1.1d;

            settings.ReadFrom(mySettings);

            Assert.AreEqual(nonEmptyString, settings.GetValue("NonEmptyStringValue"));
            Assert.AreEqual(anyInt, settings.GetValue<int>("IntValue"));
            Assert.AreEqual(null, settings.GetValue<int?>("EmptyIntValue"));
            Assert.AreEqual(1.1d, settings.GetValue<double>("DoubleValue"));
        }

        [Test]
        public void Then_write_only_properties_should_be_skipped()
        {
            var settings = new AppSettings("filename", FileOption.None);
            var mySettings = new SettingsWithWriteOnlyProperty();
            mySettings.NonEmptyStringValue = "abc";

            settings.ReadFrom(mySettings);

            Assert.AreEqual("abc", settings.GetValue("NonEmptyStringValue"));
            Assert.IsFalse(settings.HasAppSetting("DoubleValue"));
        }

        [Test]
        public void Then_protected_getters_should_be_used()
        {
            var settings = new AppSettings("filename", FileOption.None);
            var mySettings = new SettingsWithProtectedGetters("a");

            settings.ReadFrom(mySettings);

            Assert.AreEqual("a", settings.GetValue("NonEmptyStringValue"));
        }

        [Test]
        public void Then_private_getters_should_be_used()
        {
            var settings = new AppSettings("filename", FileOption.None);
            var mySettings = new SettingsWithPrivateGetters("a");

            settings.ReadFrom(mySettings);

            Assert.AreEqual("a", settings.GetValue("NonEmptyStringValue"));
        }

        [Test]
        public void Then_existing_setting_is_overwritten()
        {
            // Initialize with value "a"
            var settings = new AppSettings("filename", FileOption.None);
            settings.SetValue("NonEmptyStringValue", "a");

            // Overwrite a with b
            var mySettings = new SettingsWithPublicGetters("b", 1, null);

            settings.ReadFrom(mySettings);

            Assert.AreEqual("b", settings.GetValue("NonEmptyStringValue"));            
        }

        [Test]
        public void Then_properties_in_AppSettings_class_should_be_skipped()
        {
            var settings = new CustomizedAppSettings("filename", FileOption.None);
            settings.Numeric = 10;

            settings.SaveAllIntoSettings();

            Assert.AreEqual(1, settings.Config.AppSettings.Settings.Count);            
        }
    }

    public class CustomizedAppSettings : AppSettings
    {
        public CustomizedAppSettings(string fileName, FileOption fileOption)
            : base(fileName, fileOption)
        {
        }

        public void SaveAllIntoSettings()
        {
            this.ReadFrom(this);
        }

        public int Numeric { get; set; }

        [IgnoreProperty]
        public Configuration Config
        {
            get
            {
                return this.Configuration;
            }
        }
    }

    public class SettingsWithPrivateGetters
    {
        private string value;

        public SettingsWithPrivateGetters(string value)
        {
            this.value = value;
        }

        public string NonEmptyStringValue
        {
            private get
            {
                return this.value;
            }

            set
            {
                this.value = value;
            }
        }
    }

    public class SettingsWithProtectedGetters
    {
        private string value;

        public SettingsWithProtectedGetters(string value)
        {
            this.value = value;
        }

        public string NonEmptyStringValue
        {
            protected get
            {
                return this.value;
            }

            set
            {
                this.value = value;
            }
        }
    }


    public class SettingsWithWriteOnlyProperty
    {
        private double doubleValue = double.MinValue;

        public string NonEmptyStringValue { get; set; }

        public double DoubleValue
        {
            set
            {
                this.doubleValue = value;
            }
        }
    }

    public class SettingsWithPublicGetters
    {
        public SettingsWithPublicGetters(string nonEmptyStringValue, int intValue, int? emptyIntValue)
        {
            this.NonEmptyStringValue = nonEmptyStringValue;
            this.IntValue = intValue;
            this.EmptyIntValue = emptyIntValue;
        }

        public string NonEmptyStringValue { get; set; }
        public int IntValue { get; set; }
        public int? EmptyIntValue { get; set; }
        public double DoubleValue { get; set; }
    }
}
