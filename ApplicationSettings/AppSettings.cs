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
        public virtual string FullPath
        {
            get
            {
                return this.Configuration.FilePath;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the configuration file exists.
        /// </summary>
        public virtual bool FileExists
        {
            get
            {
                return this.Configuration.HasFile;
            }
        }

        /// <summary>
        /// Gets or sets Configuration.
        /// </summary>
        protected Configuration Configuration { get; set; }

        /// <summary>
        /// Gets mandatory configuration value.
        /// </summary>
        /// <param name="settingName">
        /// The setting name.
        /// </param>
        /// <returns>
        /// Value of the setting.
        /// </returns>
        public string GetValue(string settingName)
        {
            if (!this.HasAppSetting(settingName))
            {
                throw new AppSettingException("The setting with name {0} does not exist in the configuration".FormatWith(settingName));
            }

            return this.GetAppSettingValue(settingName);
        }

        /// <summary>
        /// Gets mandatory configuration value.
        /// </summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="settingName">Name of the setting.</param>
        /// <returns>Value of the setting</returns>
        public T GetValue<T>(string settingName)
        {
            var value = this.GetValue(settingName);
            return this.ConvertValue<T>(settingName, value);
        }

        /// <summary>
        /// Gets optional value. If the setting does not exist <paramref name="defaultValue"/> is returned.
        /// </summary>
        /// <param name="settingName">Name of the setting.</param>
        /// <param name="defaultValue">Default value</param>
        /// <returns>Value of the setting or default value.</returns>
        public string GetOptionalValue(string settingName, string defaultValue)
        {
            if (!this.HasAppSetting(settingName))
            {
                return defaultValue;
            }

            return this.GetAppSettingValue(settingName);
        }

        /// <summary>
        /// Gets optional value. If the setting does not exist <paramref name="defaultValue"/> is returned.
        /// </summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="settingName">Name of the setting</param>
        /// <param name="defaultValue">Default value</param>
        /// <returns>Value of the setting or default value</returns>
        public T GetOptionalValue<T>(string settingName, T defaultValue)
        {
            if (!this.HasAppSetting(settingName))
            {
                return defaultValue;
            }

            var value = this.GetAppSettingValue(settingName);
            return this.ConvertValue<T>(settingName, value);
        }

        /// <summary>
        /// Converts the value to specific type.
        /// </summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="settingName">Name of the setting</param>
        /// <param name="value">Value to be converted</param>
        /// <returns>Converted value</returns>
        protected virtual T ConvertValue<T>(string settingName, string value)
        {
            try
            {
                return TypeConverter.Convert<T>(value);
            }
            catch (Exception exp)
            {
                var msg = "Failed to convert setting {0} (value: {1}) into {2}."
                    .FormatWith(settingName, value, typeof(T).FullName);
                throw new AppSettingException(msg, exp);
            }            
        }

        /// <summary>
        /// Gets the value of the appsetting.
        /// </summary>
        /// <param name="settingName">Name of the setting.</param>
        /// <returns>Value of the setting.</returns>
        protected virtual string GetAppSettingValue(string settingName)
        {
            return this.Configuration.AppSettings.Settings[settingName].Value;
        }

        /// <summary>
        /// Checks if the setting exists.
        /// </summary>
        /// <param name="settingName">Name of the setting</param>
        /// <returns>True if setting exists.</returns>
        protected virtual bool HasAppSetting(string settingName)
        {
            return this.Configuration.AppSettings.Settings.AllKeys.Contains(settingName);
        }

        private void ValidateFilePath(string fileName)
        {
            string fullPath;

            try
            {
                fullPath = System.IO.Path.GetFullPath(fileName);
            }
            catch (Exception exp)
            {
                throw new AppSettingException("The path to configuration file is invalid", exp);
            }

            if (!System.IO.File.Exists(fullPath))
            {
                throw new AppSettingException("The file {0} does not exist".FormatWith(fullPath));
            }
        }
    }
}
