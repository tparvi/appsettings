namespace ApplicationSettings
{
    using System;
    using System.Configuration;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;

    using ApplicationSettings.Internal;

    /// <summary>
    /// Option used when accessing the physical file.
    /// </summary>
    public enum FileOption
    {
        /// <summary>
        /// None. The existance of the file is not confirmed
        /// prior of using it.
        /// </summary>
        None,

        /// <summary>
        /// The file must exist prior of using it.
        /// </summary>
        FileMustExist
    }

    /// <summary>
    /// Application settings.
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Extension used for configuration files.
        /// </summary>
        public const string Extension = @".config";

        /// <summary>
        /// Initializes a new instance of the <see cref="AppSettings"/> class.
        /// </summary>
        /// <param name="fileName">
        /// Absolute or relative path to the configuraton file. The file
        /// must exist.
        /// </param>
        public AppSettings(string fileName)
        {
            this.Configuration = OpenConfigurationFile(fileName, FileOption.FileMustExist);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppSettings"/> class.
        /// </summary>
        /// <param name="fileOption">
        /// The file Option.
        /// </param>
        public AppSettings(FileOption fileOption)
        {
            this.Configuration = OpenConfigurationFile(fileOption);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppSettings"/> class.
        /// </summary>
        /// <param name="fileName">
        /// Absolute or relative path to the configuraton file.
        /// </param>
        /// <param name="fileOption">
        /// The file option.
        /// </param>
        public AppSettings(string fileName, FileOption fileOption)
        {
            this.Configuration = OpenConfigurationFile(fileName, fileOption);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppSettings"/> class.
        /// </summary>
        protected AppSettings()
        {     
            // This constructor is reserved for derived classes
            // and static methods
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
        /// Creates <see cref="AppSettings"/> for the calling assembly by using it's
        /// location and name to generate the full path to configuration file.        
        /// </summary>
        /// <param name="fileOption">
        /// The file option.
        /// </param>
        /// <returns>
        /// Created <see cref="AppSettings"/> object.
        /// </returns>
        public static AppSettings CreateForCallingAssembly(FileOption fileOption)
        {
            return CreateForAssembly(Assembly.GetCallingAssembly(), fileOption);
        }

        /// <summary>
        /// Creates <see cref="AppSettings"/> for the <paramref name="assembly"/> by using it's
        /// location and name to generate the full path to configuration file.        
        /// </summary>
        /// <param name="assembly">
        /// The assembly.
        /// </param>
        /// <param name="fileOption">
        /// The file option.
        /// </param>
        /// <returns>
        /// Created <see cref="AppSettings"/> object.
        /// </returns>
        public static AppSettings CreateForAssembly(Assembly assembly, FileOption fileOption)
        {
            if (null == assembly)
            {
                throw new ArgumentNullException("assembly", "Cannot create AppSettings. Assembly is null.");
            }

            var fullPath = GetPathToConfigurationFile(assembly);

            var appSettings = new AppSettings
            {
                Configuration = OpenConfigurationFile(fullPath, fileOption)
            };

            return appSettings;
        }

        /// <summary>
        /// Creates <see cref="AppSettings"/> for the <paramref name="assembly"/> by using it's
        /// name for to generate the configuration file name.
        /// </summary>
        /// <param name="assembly">
        /// The assembly.
        /// </param>
        /// <param name="pathUnderAppData">
        /// The path under app data. This is appended to the <see cref="Environment.SpecialFolder.ApplicationData"/>
        /// to get the path to the folder where configuration file is.
        /// </param>
        /// <param name="fileOption">
        /// The file option.
        /// </param>
        /// <returns>
        /// Created <see cref="AppSettings"/> object.
        /// </returns>
        public static AppSettings CreateForCurrentUser(Assembly assembly, string pathUnderAppData, FileOption fileOption)
        {
            if (null == assembly)
            {
                throw new ArgumentNullException("assembly", "Cannot create AppSettings. Assembly is null.");
            }

            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var path = System.IO.Path.Combine(appData, pathUnderAppData);

            var fileName = assembly.GetConfigurationFileName(Extension);
            var fulPath = System.IO.Path.Combine(path, fileName);

            return new AppSettings(fulPath, fileOption);
        }

        /// <summary>
        /// Saves the settings into physical file.
        /// </summary>
        public virtual void Save()
        {
            this.Configuration.Save();
        }

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
        /// Sets the value for setting. If the setting exists then it's value
        /// is replaced with <paramref name="value"/>. If the setting does not
        /// exist then new setting is created usng <paramref name="settingName"/>
        /// as key.
        /// </summary>
        /// <param name="settingName">
        /// The setting name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        public void SetValue(string settingName, string value)
        {
            this.UpdateValue(settingName, value);
        }

        /// <summary>
        /// Sets the value for setting. If the setting exists then it's value
        /// is replaced with <paramref name="value"/>. If the setting does not
        /// exist then new setting is created usng <paramref name="settingName"/>
        /// as key.
        /// </summary>
        /// <param name="settingName">
        /// The setting name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <typeparam name="T">
        /// Type of the setting.
        /// </typeparam>
        public void SetValue<T>(string settingName, T value)
        {
            this.SetValue<T>(settingName, value, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Sets the value for setting. If the setting exists then it's value
        /// is replaced with <paramref name="value"/>. If the setting does not
        /// exist then new setting is created usng <paramref name="settingName"/>
        /// as key.
        /// </summary>
        /// <param name="settingName">
        /// The setting name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="formatProvider">
        /// The format provider.
        /// </param>
        /// <typeparam name="T">
        /// Type of the setting.
        /// </typeparam>
        public void SetValue<T>(string settingName, T value, IFormatProvider formatProvider)
        {
            var tempValue = Convert.ToString(value, formatProvider);
            this.UpdateValue(settingName, tempValue);
        }

        /// <summary>
        /// Sets the value for setting. If the setting exists then it's value
        /// is replaced with <paramref name="value"/>. If the setting does not
        /// exist then new setting is created usng <paramref name="settingName"/>
        /// as key.
        /// </summary>
        /// <param name="settingName">
        /// The setting name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="conversionFunc">
        /// The conversion function used to convert <paramref name="value"/> into string.
        /// </param>
        /// <typeparam name="T">
        /// Type of the setting.
        /// </typeparam>
        public void SetValue<T>(string settingName, T value, Func<T, string, string> conversionFunc)
        {
            var tempValue = conversionFunc(value, settingName);
            this.UpdateValue(settingName, tempValue);
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
        /// Sets the value for connection string. If it exists then it's value
        /// is replaced with <paramref name="value"/>. If the connection string does not
        /// exist then new setting is created usng <paramref name="connectionStringName"/>
        /// as key.
        /// </summary>
        /// <param name="connectionStringName">
        /// The connection strng name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        public virtual void SetConnectionString(string connectionStringName, string value)
        {
            var connectionString = this.GetConnectionStringByName(connectionStringName);
            if (null == connectionString)
            {
                this.Configuration.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings(connectionStringName, value));
            }
            else
            {
                connectionString.ConnectionString = value;
            }
        }

        /// <summary>
        /// Writes configuration setting values into public properties of
        /// <paramref name="instance"/> object. Properties can have private
        /// or protected setters.
        /// </summary>
        /// <param name="instance">
        /// The instance.
        /// </param>
        public virtual void WriteInto(object instance)
        {
            if (null == instance)
            {
                throw new ArgumentNullException("instance", "Cannot write configuration values into null object.");
            }

            var properties = instance.GetType().GetProperties();

            foreach (var propertyInfo in properties)
            {
                if (!PropertyHelper.CanWriteInto(propertyInfo))
                {
                    continue;
                }

                var settingName = PropertyHelper.GetSettingName(propertyInfo);

                string rawValue;

                if (PropertyHelper.IsConnectionString(propertyInfo))
                {                    
                    rawValue = this.GetConnectionStringValueForProperty(propertyInfo, settingName);                    
                }
                else
                {
                    rawValue = this.GetSettingsValueForProperty(propertyInfo, settingName);
                }

                var formatProvider = PropertyHelper.GetFormatProvider(propertyInfo);
                var type = propertyInfo.PropertyType;
                var value = this.ConvertValue(type, settingName, rawValue, formatProvider);
                propertyInfo.SetValue(instance, value, null);
            }
        }

        /// <summary>
        /// Reads public properties of <paramref name="instance"/> and
        /// writes them into settings. Existing settings are overwritten.
        /// </summary>
        /// <param name="instance">
        /// The instance.
        /// </param>
        public virtual void ReadFrom(object instance)
        {
            if (null == instance)
            {
                throw new ArgumentNullException("instance", "Cannot read configuration values from null object.");
            }

            var properties = instance.GetType().GetProperties();

            foreach (var propertyInfo in properties)
            {
                var settingName = PropertyHelper.GetSettingName(propertyInfo);

                if (!PropertyHelper.CanReadFrom(propertyInfo))
                {
                    continue;
                }

                var formatProvider = PropertyHelper.GetFormatProvider(propertyInfo);
                var rawValue = PropertyHelper.GetPropertyValue(instance, propertyInfo, formatProvider);

                if (PropertyHelper.IsConnectionString(propertyInfo))
                {
                    this.SetConnectionString(settingName, rawValue);
                }
                else
                {
                    this.SetValue(settingName, rawValue);
                }
            }            
        }

        /// <summary>
        /// Checks if the app setting exists.
        /// </summary>
        /// <param name="settingName">Name of the setting</param>
        /// <returns>True if setting exists.</returns>
        public bool HasAppSetting(string settingName)
        {
            if (null == this.Configuration)
            {
                throw new InvalidOperationException("The Configuration has not been created yet (Configuration is null)");
            }

            return this.Configuration.AppSettings.Settings.AllKeys.Contains(settingName);
        }

        /// <summary>
        /// Checks if the connection string exists.
        /// </summary>
        /// <param name="connectionStringName">
        /// The connection string name.
        /// </param>
        /// <returns>
        /// True if the connection string exists.
        /// </returns>
        public bool HasConnectionString(string connectionStringName)
        {
            return null != this.GetConnectionStringByName(connectionStringName);
        }

        /// <summary>
        /// Gets the full path to the configuration file by generating the
        /// configuration file name from the <paramref name="assembly"/>
        /// and by appending the <see cref="Extension"/> to the file name.
        /// </summary>
        /// <param name="assembly">
        /// The assembly.
        /// </param>
        /// <returns>
        /// Full path to configuration file.
        /// </returns>
        protected static string GetPathToConfigurationFile(Assembly assembly)
        {
            var location = assembly.GetDirectoryName();
            var fileName = assembly.GetConfigurationFileName(Extension);
            return System.IO.Path.Combine(location, fileName);
        }

        /// <summary>
        /// Validates the file path and that the file exists.
        /// </summary>
        /// <param name="fileName">
        /// Relative or absolute path to file.
        /// </param>
        protected static void ValidateFilePath(string fileName)
        {
            string fullPath;

            try
            {
                fullPath = System.IO.Path.GetFullPath(fileName);
            }
            catch (Exception exp)
            {
                throw new AppSettingException("The path to configuration file is invalid. See inner exception for details.", exp);
            }

            if (!System.IO.File.Exists(fullPath))
            {
                throw new AppSettingException("The file {0} does not exist".FormatWith(fullPath));
            }
        }

        /// <summary>
        /// Opens the configuration file.
        /// </summary>
        /// <param name="fileName">
        /// Absolute or relative path to existing file.
        /// </param>
        /// <param name="fileOption">
        /// The file Option.
        /// </param>
        /// <returns>
        /// Configuration object.
        /// </returns>
        protected static Configuration OpenConfigurationFile(string fileName, FileOption fileOption)
        {
            if (FileOption.FileMustExist == fileOption)
            {
                ValidateFilePath(fileName);    
            }
            
            var fileMap = new ExeConfigurationFileMap { ExeConfigFilename = fileName };
            return ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
        }

        /// <summary>
        /// Opens the configuration file for the executable.
        /// </summary>
        /// <param name="fileOption">
        /// The file Option.
        /// </param>
        /// <returns>
        /// Configuration object.
        /// </returns>
        protected static Configuration OpenConfigurationFile(FileOption fileOption)
        {
            var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (FileOption.FileMustExist == fileOption)
            {
                ValidateFilePath(configuration.FilePath);    
            }
            
            return configuration;
        }

        /// <summary>
        /// Gets the value for the property. The value is mandatory except if property
        /// is associated with <see cref="SettingProperty"/> attribute in which case
        /// it can be optional.
        /// </summary>
        /// <param name="propertyInfo">
        /// The property info.
        /// </param>
        /// <param name="settingName">
        /// The setting name.
        /// </param>
        /// <returns>
        /// Value for the property.
        /// </returns>
        protected virtual string GetSettingsValueForProperty(PropertyInfo propertyInfo, string settingName)
        {
            var attribute = propertyInfo.GetCustomAttribute<SettingProperty>();
            if (null != attribute && attribute.IsOptional)
            {
                return this.GetOptionalValue(settingName, attribute.DefaultValue);                
            }

            return this.GetValue(settingName);
        }

        /// <summary>
        /// Gets the connection string for the property. The value is mandatory except if property
        /// is associated with <see cref="SettingProperty"/> attribute in which case
        /// it can be optional.
        /// </summary>
        /// <param name="propertyInfo">
        /// The property info.
        /// </param>
        /// <param name="connectionStringName">
        /// The connection string name.
        /// </param>
        /// <returns>
        /// Value for the connection string.
        /// </returns>
        protected virtual string GetConnectionStringValueForProperty(PropertyInfo propertyInfo, string connectionStringName)
        {
            var attribute = propertyInfo.GetCustomAttribute<SettingProperty>();
            if (null != attribute && attribute.IsOptional)
            {
                if (!this.HasConnectionString(connectionStringName))
                {
                    return attribute.DefaultValue;
                }
            }

            return this.GetConnectionString(connectionStringName);            
        }

        /// <summary>
        /// Updates the setting with new value. If the setting does not exist
        /// new setting is created.
        /// </summary>
        /// <param name="settingName">
        /// The setting name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        protected virtual void UpdateValue(string settingName, string value)
        {
            var element = this.Configuration.AppSettings.Settings[settingName];
            if (null != element)
            {
                element.Value = value;
            }
            else
            {
                this.Configuration.AppSettings.Settings.Add(settingName, value);
            }
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
        protected virtual ConnectionStringSettings GetConnectionStringByName(string connectionStringName)
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
            return (T)this.ConvertValue(typeof(T), settingName, value, formatProvider);
        }

        /// <summary>
        /// Converts the value to specific type.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="settingName">
        /// The setting name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="formatProvider">
        /// The format provider.
        /// </param>
        /// <returns>
        /// Converted value.
        /// </returns>
        protected virtual object ConvertValue(Type type, string settingName, string value, IFormatProvider formatProvider)
        {
            try
            {
                return TypeConverter.Convert(type, value, formatProvider);
            }
            catch (Exception exp)
            {
                var msg = "Failed to convert setting {0} (value: {1}) into {2}. See inner exception for details."
                    .FormatWith(settingName, value, type.FullName);
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

                msg += "If your config contains only single connection string make sure you add <clear/>" + Environment.NewLine;
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
    }
}
