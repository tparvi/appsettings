namespace ApplicationSettingsTests.UpdateTests
{
    using ApplicationSettings;

    using NUnit.Framework;

    [TestFixture]
    public class When_updating_string_value : TestBase
    {
        [Test]
        public void And_setting_exists_Then_existing_setting_should_be_replaced()
        {
            var settings = new AppSettings("NonExistingFile", FileOption.None);
            var settingName = "setting";

            // Original value
            settings.SetValue(settingName, "a");
            Assert.AreEqual("a", settings.GetValue(settingName));

            // Save with different value
            settings.SetValue(settingName, "b");

            Assert.AreEqual("b", settings.GetValue(settingName));            
        }

        [Test]
        public void And_setting_does_not_exist_Then_setting_should_be_stored()
        {            
            var settings = new AppSettings("NonExistingFile", FileOption.None);

            settings.SetValue("setting", "a");

            Assert.AreEqual("a", settings.GetValue("setting"));
        }

        [Test]
        public void And_value_is_null_Then_update_should_succeed()
        {
            var settings = new AppSettings("NonExistingFile", FileOption.None);

            settings.SetValue("setting", null);

            Assert.AreEqual(null, settings.GetValue("setting"));
        }
    }
}
