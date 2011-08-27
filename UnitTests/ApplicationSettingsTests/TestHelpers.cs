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

        public static void DeleteIfExists(string fullPathToFile)
        {
            if (System.IO.File.Exists(fullPathToFile))
            {
                System.IO.File.Delete(fullPathToFile);
            }
        }

        public static string CreateCopyOfFile(string source)
        {
            var directory = System.IO.Path.GetDirectoryName(source);
            var fileName = Guid.NewGuid() + ".config";
            var target = System.IO.Path.Combine(directory, fileName);
            System.IO.File.Copy(source, target);
            return target;
        }

        private static string GetAbsolutePathToTestAssembly()
        {
            var assembly = Assembly.GetAssembly(typeof(TestHelpers));
            var uri = new Uri(assembly.CodeBase);
            return System.IO.Path.GetDirectoryName(uri.AbsolutePath);            
        }
    }
}
