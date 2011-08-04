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
        /// Gets the only ConnectionString from the configuration. If there are
        /// multiple connection strings or none then exception is thrown.
        /// </summary>
        public virtual string ConnectionString
        {
            get
            {
                return this.GetOnlyExistingConnectionString();
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
            return this.ConvertValue<T>(settingName, value, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Gets mandatory configuration value.
        /// </summary>
        /// <param name="settingName">
        /// The setting name.
        /// </param>
        /// <param name="conversionFunc">
        /// The conversion function used to convert the value.
        /// </param>
        /// <typeparam name="T">
        /// Type of the value.
        /// </typeparam>
        /// <returns>
        /// Value of the setting.
        /// </returns>
        public T GetValue<T>(string settingName, Func<string, string, T> conversionFunc)
        {
            var value = this.GetValue(settingName);
            return conversionFunc(settingName, value);
        }

        /// <summary>
        /// Gets mandatory configuration value.
        /// </summary>
        /// <param name="settingName">
        /// The setting name.
        /// </param>
        /// <param name="formatProvider">
        /// An object that supplies culture-specific formatting information.
        /// </param>
        /// <typeparam name="T">
        /// Type of the value.
        /// </typeparam>
        /// <returns>
        /// Value of the setting.
        /// </returns>
        public T GetValue<T>(string settingName, IFormatProvider formatProvider)
        {
            var value = this.GetValue(settingName);
            return this.ConvertValue<T>(settingName, value, formatProvider);
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
            return this.ConvertValue<T>(settingName, value, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Gets optional value. If the setting does not exist <paramref name="defaultValue"/> is returned.
        /// </summary>
        /// <param name="settingName">
        /// The setting name.
        /// </param>
        /// <param name="defaultValue">
        /// The default value.
        /// </param>
        /// <param name="formatProvider">
        /// An object that supplies culture-specific formatting information.
        /// </param>
        /// <typeparam name="T">
        /// Type of the value.
        /// </typeparam>
        /// <returns>
        /// Value of the setting or default value.
        /// </returns>
        public T GetOptionalValue<T>(string settingName, T defaultValue, IFormatProvider formatProvider)
        {
            if (!this.HasAppSetting(settingName))
            {
                return defaultValue;
            }

            var value = this.GetAppSettingValue(settingName);
            return this.ConvertValue<T>(settingName, value, formatProvider);
        }


        /// <summary>
        /// Gets optional value. If the setting does not exist <paramref name="defaultValue"/> is returned.
        /// If setting exists <paramref name="conversionFunc"/> is used to convert the value.
        /// </summary>
        /// <param name="settingName">
        /// The setting name.
        /// </param>
        /// <param name="defaultValue">
        /// The default value.
        /// </param>
        /// <param name="conversionFunc">
        /// The conversion functio.
        /// </param>
        /// <typeparam name="T">
        /// Type of the value.
        /// </typeparam>
        /// <returns>
        /// Default value or value returned by the <paramref name="conversionFunc"/>.
        /// </returns>
        public T GetOptionalValue<T>(string settingName, T defaultValue, Func<string, string, T> conversionFunc)
        {
            if (!this.HasAppSetting(settingName))
            {
                return defaultValue;
            }

            var value = this.GetAppSettingValue(settingName);
            return conversionFunc(settingName, value);            
        }

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <param name="connectionStringName">
        /// The connection string name.
        /// </param>
        /// <returns>
        /// The connection string.
        /// </returns>
        public string GetConnectionString(string connectionStringName)
        {
            var connectionString = this.GetConnectionStringByName(connectionStringName);
            if (null != connectionString)
            {
                return connectionString.ConnectionString;
            }
            
            throw new AppSettingException("There is no connection string by the name {0}".FormatWith(connectionStringName));
        }

        /// <summary>
        /// Gets the connection string from configuration.
        /// </summary>
        /// <param name="connectionStringName">
        /// The connection string name.
        /// </param>
        /// <returns>
        /// Connection string or null.
        /// </returns>
        protected ConnectionStringSettings GetConnectionStringByName(string connectionStringName)
        {
            return this.Configuration.ConnectionStrings.ConnectionStrings[connectionStringName];
        }

        /// <summary>
        /// Converts the value to specific type.
        /// </summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="settingName">Name of the setting</param>
        /// <param name="value">Value to be converted</param>
        /// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
        /// <returns>Converted value</returns>
        protected virtual T ConvertValue<T>(string settingName, string value, IFormatProvider formatProvider)
        {
            try
            {
                return TypeConverter.Convert<T>(value, formatProvider);
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
        /// Checks if the app setting exists.
        /// </summary>
        /// <param name="settingName">Name of the setting</param>
        /// <returns>True if setting exists.</returns>
        protected virtual bool HasAppSetting(string settingName)
        {
            return this.Configuration.AppSettings.Settings.AllKeys.Contains(settingName);
        }

        /// <summary>
        /// Checsk that there is only single connection string and returns it.
        /// </summary>
        /// <returns>
        /// The only connection string that has been configured.
        /// </returns>
        protected virtual string GetOnlyExistingConnectionString()
        {
            this.ValidateThatOnlySingleConnectionStringExists();
            return this.Configuration.ConnectionStrings.ConnectionStrings[0].ConnectionString;            
        }

        /// <summary>
        /// Validates the there is only single connection string.
        /// </summary>
        /// <exception cref="AppSettingException">
        /// If there are no connection strings or multiple connection strings.
        /// </exception>
        protected virtual void ValidateThatOnlySingleConnectionStringExists()
        {
            if (this.Configuration.ConnectionStrings.ConnectionStrings.Count > 1)
            {
                var msg = "Cannot return connection strings because there are multiple connection strings." + Environment.NewLine;
                msg += "Found following connection strings from the configuration:" + Environment.NewLine;

                foreach (var cs in this.Configuration.ConnectionStrings.ConnectionStrings)
                {
                    msg += cs + Environment.NewLine;
                }

                msg += "If your app.config contains only single connection string make sure you add <clear/>" + Environment.NewLine;
                msg += "right below <connectionStrings> to remove machine level connection strings which are automatically" + Environment.NewLine;
                msg += "added to your configuration from machine.config.";

                throw new AppSettingException(msg);
            }

            if (1 != this.Configuration.ConnectionStrings.ConnectionStrings.Count)
            {
                var msg = "There should be single connection string but the are actually {0} connection strings."
                    .FormatWith(this.Configuration.ConnectionStrings.ConnectionStrings.Count);
                throw new AppSettingException(msg);
            }
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
