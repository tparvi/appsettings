namespace ApplicationSettingsTests.CreationTests
{
    using ApplicationSettings;

    using ApplicationSettingsTests.Configurations;

    using NUnit.Framework;

    [TestFixture]
    public class When_creating_for_calling_assembly : TestBase
    {
        [Test]
        public void Then_file_name_should_point_to_calling_assembly()
        {
            var appSettings = AppSettings.CreateForCallingAssembly(FileOption.None);

            var fileName = System.IO.Path.GetFileName(appSettings.FullPath);

            Assert.AreEqual("ApplicationSettingsTests.dll.config", fileName);
        }
    }
}
