namespace ApplicationSettings
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Generic type converter.
    /// </summary>
    internal static class TypeConverter
    {
        /// <summary>
        /// Converts string to specific type.
        /// </summary>
        /// <typeparam name="T">Type to convert to</typeparam>
        /// <param name="value">Value to be converted</param>
        /// <returns>Converted value.</returns>
        public static T Convert<T>(string value)
        {
            var type = typeof(T);

            // In case the type is e.g. Nullable<int>
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {                
                if (string.Empty == value)
                {
                    return (T)(object)null;
                }

                var underlyingType = Nullable.GetUnderlyingType(type);

                if (underlyingType.IsEnum)
                {
                    return ConvertEnum<T>(underlyingType, value);
                }

                return (T)System.Convert.ChangeType(value, underlyingType, CultureInfo.InvariantCulture);
            }

            if (type.IsEnum)
            {
                return ConvertEnum<T>(type, value);
            }

            return (T)System.Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
        }

        private static T ConvertEnum<T>(Type type, string value)
        {
            return (T)Enum.Parse(type, value, true);
        }
    }
}