namespace ApplicationSettingsTests.UpdateTests
{
    using System.Globalization;

    using ApplicationSettings;

    using NUnit.Framework;

    [TestFixture]
    public class When_updating_generic_value : TestBase
    {
        [Test]
        public void And_setting_exist_Then_existing_setting_should_be_replaced()
        {
            var settings = new AppSettings("NonExistingFile", FileOption.None);
            var settingName = "setting";

            // Original value
            settings.SetValue<int>(settingName, 1);
            Assert.AreEqual(1, settings.GetValue<int>(settingName));

            // Save with different value
            settings.SetValue<int>(settingName, 2);
            Assert.AreEqual(2, settings.GetValue<int>(settingName));            
        }

        [Test]
        public void And_setting_does_not_exist_Then_setting_should_be_stored()
        {            
            var settings = new AppSettings("NonExistingFile", FileOption.None);

            settings.SetValue<int>("setting", 1);

            Assert.AreEqual(1, settings.GetValue<int>("setting"));
        }

        [Test]
        public void And_custom_IFromatProvider_is_specified_it_should_be_used()
        {
            var settings = new AppSettings("NonExistingFile", FileOption.None);
            var formatProvider = CultureInfo.GetCultureInfo("fi-FI");

            settings.SetValue<double>("setting", 1.1, formatProvider);

            Assert.AreEqual(1.1d, settings.GetValue<double>("setting", formatProvider));

            // Since the value was stored using fi-FI locale then string value
            // should have value of 1,1 (comma instead of dot)
            Assert.AreEqual("1,1", settings.GetValue("setting"));
        }

        [Test]
        public void And_custom_conversion_function_is_specified_it_should_be_used()
        {
            var settings = new AppSettings("NonExistingFile", FileOption.None);

            var functionCalled = false;
            settings.SetValue<int>("setting", 1, (settingName, rawValue) =>
                {
                    functionCalled = true;
                    return rawValue.ToString();
                });

            Assert.IsTrue(functionCalled);
        }

        [Test]
        public void And_custom_conversion_function_is_specified_value_it_returns_should_be_used()
        {
            var settings = new AppSettings("NonExistingFile", FileOption.None);

            settings.SetValue<int>("setting", 1, (settingName, rawValue) => "a");

            Assert.AreEqual("a", settings.GetValue("setting"));
        }

        [Test]
        public void And_value_is_nullable_Then_update_should_succeed()
        {
            var settings = new AppSettings("NonExistingFile", FileOption.None);

            settings.SetValue<int?>("setting", null);

            Assert.AreEqual(null, settings.GetValue<int?>("setting"));
        }
    }
}
