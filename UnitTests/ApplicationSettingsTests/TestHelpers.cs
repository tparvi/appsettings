namespace ApplicationSettingsTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public static class TestHelpers
    {
        public static string GetFullPathToConfigurationFile(string fileName)
        {
            var assembly = Assembly.GetAssembly(typeof(TestHelpers));
            var uri = new Uri(assembly.CodeBase);
            var path = System.IO.Path.GetDirectoryName(uri.AbsolutePath);

            //var uri = new Uri(Assembly.GetExecutingAssembly().CodeBase);)
            return System.IO.Path.Combine(path, fileName);
            //System.IO.Path.fu
        }
    }
}
