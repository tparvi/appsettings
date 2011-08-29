namespace ApplicationSettings.Internal
{
    using System;
    using System.Globalization;
    using System.Reflection;

    /// <summary>
    /// Helper methods for accessing property information.
    /// </summary>
    internal static class PropertyHelper
    {
        /// <summary>
        /// Checks if property can be written into. If the property is read only or
        /// it is ignored then we cannot write into it.
        /// </summary>
        /// <param name="propertyInfo">
        /// The property info.
        /// </param>
        /// <returns>
        /// True if property can be written into.
        /// </returns>
        public static bool CanWriteInto(PropertyInfo propertyInfo)
        {
            if (!propertyInfo.CanWrite)
            {
                return false;
            }

            // If the property is ignored then we don't write into it.
            var ignoredAttribute = propertyInfo.GetCustomAttribute<IgnoreProperty>();
            if (null != ignoredAttribute)
            {
                return ignoredAttribute.EnableWriting;
            }

            return true;
        }

        public static bool CanReadFrom(PropertyInfo propertyInfo)
        {
            if (!propertyInfo.CanRead)
            {
                return false;
            }

            // If the property is ignored then we don't write into it.
            var ignoredAttribute = propertyInfo.GetCustomAttribute<IgnoreProperty>();
            if (null != ignoredAttribute)
            {
                return ignoredAttribute.EnableReading;
            }

            return true;
        }

        /// <summary>
        /// Gets the value of the property.
        /// </summary>
        /// <param name="instance">Object from which the property is returned.</param>
        /// <param name="propertyInfo">The property.</param>
        /// <param name="formatProvider">Format provider.</param>
        /// <returns>Value of the property.</returns>
        public static string GetPropertyValue(object instance, PropertyInfo propertyInfo, IFormatProvider formatProvider)
        {
            var tempValue = propertyInfo.GetValue(instance, null);
            if (null == tempValue)
            {
                return string.Empty;
            }

            var value = Convert.ToString(tempValue, formatProvider);
            return value;
        }

        /// <summary>
        /// Gtes the format provider for the property. If property contains
        /// <see cref="SettingProperty"/> then it's <see cref="SettingProperty.CultureName"/>
        /// is used. Otherwise <see cref="CultureInfo.InvariantCulture"/> is returned.
        /// </summary>
        /// <param name="propertyInfo">
        /// The property info.
        /// </param>
        /// <returns>
        /// Format provider.
        /// </returns>
        public static IFormatProvider GetFormatProvider(PropertyInfo propertyInfo)
        {
            var attribute = propertyInfo.GetCustomAttribute<SettingProperty>();
            if (null != attribute && null != attribute.CultureName)
            {
                return CultureInfo.GetCultureInfo(attribute.CultureName);
            }

            return CultureInfo.InvariantCulture;
        }

        /// <summary>
        /// Gets the name of the setting for the property. If property contains
        /// <see cref="SettingProperty"/> then it is used. Otherwise property's
        /// name is returned.
        /// </summary>
        /// <param name="propertyInfo">
        /// The property info.
        /// </param>
        /// <returns>
        /// Name of the setting for the property.
        /// </returns>
        public static string GetSettingName(PropertyInfo propertyInfo)
        {
            var attribute = propertyInfo.GetCustomAttribute<SettingProperty>();
            if (null != attribute && null != attribute.SettingName)
            {
                return attribute.SettingName;
            }

            return propertyInfo.Name;
        }
    }
}
