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
    }
}