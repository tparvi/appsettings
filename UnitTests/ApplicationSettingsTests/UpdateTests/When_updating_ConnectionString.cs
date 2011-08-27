namespace ApplicationSettingsTests.UpdateTests
{
    using ApplicationSettings;

    using NUnit.Framework;

    [TestFixture]
    public class When_updating_ConnectionString : TestBase
    {
        [Test]
        public void And_connection_string_exists_Then_existing_connection_string_should_be_replaced()
        {
            var settings = new AppSettings("NonExistingFile", FileOption.None);

            // Original value
            settings.SetConnectionString("cs", "a");
            Assert.AreEqual("a", settings.GetConnectionString("cs"));

            // Save with different value
            settings.SetConnectionString("cs", "b");

            Assert.AreEqual("b", settings.GetConnectionString("cs"));
        }

        [Test]
        public void And_connection_string_does_not_exist_Then_connection_string_should_be_stored()
        {            
            var settings = new AppSettings("NonExistingFile", FileOption.None);
            settings.SetConnectionString("cs", "a");

            Assert.AreEqual("a", settings.GetConnectionString("cs"));
        }
    }
}
