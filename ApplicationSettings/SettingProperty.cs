namespace ApplicationSettings
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Describes the setting related properties.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SettingProperty : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingProperty"/> class.
        /// </summary>
        public SettingProperty()
        {
            this.IsOptional = false;
        }

        /// <summary>
        /// Gets or sets CultureName which is used when converting
        /// value. Maps to <see cref="CultureInfo.Name"/>. If this is
        /// null then <see cref="CultureInfo.InvariantCulture"/> is used.
        /// </summary>
        public string CultureName { get; set; }

        /// <summary>
        /// Gets or sets SettingName which is used when reading or writing
        /// setting. If it is null then name of the property is used.
        /// </summary>
        public string SettingName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the setting is optional.
        /// If the setting is not found then <see cref="DefaultValue"/> is
        /// used. Used only when <see cref="AppSettings.WriteInto"/> is used.
        /// </summary>
        public bool IsOptional { get; set; }

        /// <summary>
        /// Gets or sets DefaultValue which is used when <see cref="IsOptional"/>
        /// is <true/> and setting does not exist. Used only when 
        /// <see cref="AppSettings.WriteInto"/> is used.
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether setting is actually
        /// connection string and should be read and written as such.
        /// </summary>
        public bool IsConnectionString { get; set; }
    }
}