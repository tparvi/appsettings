namespace ApplicationSettings
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    public interface IAppSettings
    {
        T GetMandatoryValue<T>(string settingName);

        T GetValue<T>(string settingName, T defaultValue);

        // write settings

        // connection strings

        // sections?
    }

    /// <summary>
    /// Application settings.
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppSettings"/> class.
        /// </summary>
        /// <param name="fileName">
        /// Absolute path to the configuraton file.
        /// </param>
        public AppSettings(string fileName)
        {
            this.ValidateFilePath(fileName);

            var fileMap = new ExeConfigurationFileMap();
            fileMap.ExeConfigFilename = fileName;
            this.Configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
        }

        /// <summary>
        /// Gets absolute path to the configuration file.
        /// </summary>
        public string FullPath
        {
            get
            {
                return this.Configuration.FilePath;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the configuration file exists.
        /// </summary>
        public bool FileExists
        {
            get
            {
                return this.Configuration.HasFile;
            }
        }

        /// <summary>
        /// Gets Configuration.
        /// </summary>
        protected Configuration Configuration { get; private set; }

        private void ValidateFilePath(string fileName)
        {
            try
            {
                System.IO.Path.GetFullPath(fileName);
            }
            catch (Exception exp)
            {
                throw new AppSettingException("The path to configuration file is invalid", exp);
            }
        }
    }
}
