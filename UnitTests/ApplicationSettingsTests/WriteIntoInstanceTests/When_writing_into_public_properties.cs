namespace ApplicationSettingsTests.WriteIntoInstanceTests
{
    using ApplicationSettings;

    using ApplicationSettingsTests.Configurations;

    using NUnit.Framework;

    [TestFixture]
    public class When_writing_into_public_properties : TestBase
    {
        [Test]
        public void Then_all_public_properties_should_be_set()
        {
            var settings = new AppSettings(SimpleConfig.AbsolutePathToConfigFile);
            var mySettings = new SettingsWithPublicSetters(null, int.MinValue, 123);

            settings.WriteInto(mySettings);

            Assert.AreEqual("abc", mySettings.NonEmptyStringValue);
            Assert.AreEqual(1, mySettings.IntValue);
            Assert.IsNull(mySettings.EmptyIntValue);
            Assert.AreEqual(1.1d, mySettings.DoubleValue);
        }

        [Test]
        public void Then_read_only_properties_should_be_skipped()
        {
            var settings = new AppSettings(SimpleConfig.AbsolutePathToConfigFile);
            var mySettings = new SettingsWithReadOnlyProperty();

            settings.WriteInto(mySettings);

            Assert.AreEqual(double.MinValue, mySettings.DoubleValue);
            Assert.AreEqual("abc", mySettings.NonEmptyStringValue);
        }

        [Test]
        public void Then_protected_setters_should_be_used()
        {
            var settings = new AppSettings(SimpleConfig.AbsolutePathToConfigFile);
            var mySettings = new SettingsWithProtectedSetters();

            settings.WriteInto(mySettings);

            Assert.AreEqual("abc", mySettings.NonEmptyStringValue);
        }

        [Test]
        public void Then_private_setters_should_be_used()
        {
            var settings = new AppSettings(SimpleConfig.AbsolutePathToConfigFile);
            var mySettings = new SettingsWithPrivateSetters();

            settings.WriteInto(mySettings);

            Assert.AreEqual("abc", mySettings.NonEmptyStringValue);
        }

        [Test]
        public void Then_if_conversion_fails_exception_is_thrown()
        {
            var settings = new AppSettings(SimpleConfig.AbsolutePathToConfigFile);
            var mySettings = new SettingsWithInvalidType();

            Assert.Throws<AppSettingException>(() => settings.WriteInto(mySettings));
        }
    }

    public class SettingsWithInvalidType
    {
        /// <summary>
        /// The type should be string since value is stored as string
        /// in the configuration.
        /// </summary>
        public double NonEmptyStringValue { get; private set; }
    }

    public class SettingsWithPrivateSetters
    {
        public string NonEmptyStringValue { get; private set; }
    }

    public class SettingsWithProtectedSetters
    {
        public string NonEmptyStringValue { get; protected set; }
    }


    public class SettingsWithReadOnlyProperty
    {
        private double doubleValue = double.MinValue;

        public string NonEmptyStringValue { get; set; }

        public double DoubleValue
        {
            get
            {
                return this.doubleValue;
            }
        }
    }

    public class SettingsWithPublicSetters
    {
        public SettingsWithPublicSetters(string nonEmptyStringValue, int intValue, int? emptyIntValue)
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
