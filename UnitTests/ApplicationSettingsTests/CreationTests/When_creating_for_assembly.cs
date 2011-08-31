namespace ApplicationSettingsTests.CreationTests
{
    using System.Reflection;

    using ApplicationSettings;

    using ApplicationSettingsTests.Configurations;

    using NUnit.Framework;

    [TestFixture]
    public class When_creating_for_assembly : TestBase
    {
        [Test]
        public void Then_file_name_should_point_to_assembly()
        {
            var appSettings = AppSettings.CreateForAssembly(Assembly.GetAssembly(typeof(AppSettings)), FileOption.None);

            var fileName = System.IO.Path.GetFileName(appSettings.FullPath);

            Assert.AreEqual("ApplicationSettings.dll.config", fileName);
        }
    }
}
