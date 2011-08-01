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
            var path = GetAbsolutePathToTestAssembly();
            var configurationPath = System.IO.Path.Combine(path, "Configurations");
            
            return System.IO.Path.Combine(configurationPath, fileName);
        }

        public static string GetRelativePathToConfigurationFile(string fileName)
        {            
            return System.IO.Path.Combine("Configurations", fileName);
        }

        private static string GetAbsolutePathToTestAssembly()
        {
            var assembly = Assembly.GetAssembly(typeof(TestHelpers));
            var uri = new Uri(assembly.CodeBase);
            return System.IO.Path.GetDirectoryName(uri.AbsolutePath);            
        }
    }
}
