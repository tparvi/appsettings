﻿namespace ApplicationSettings
{
    using System;
    using System.Globalization;

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
            return Convert<T>(value, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts string to specific type.
        /// </summary>
        /// <typeparam name="T">Type to convert to.</typeparam>
        /// <param name="value">Value to be converted.</param>
        /// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
        /// <returns>Converted value.</returns>
        public static T Convert<T>(string value, IFormatProvider formatProvider)
        {
            var type = typeof(T);

            // In case the type is e.g. Nullable<int>
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                if (string.Empty == value)
                {
                    return (T)(object)null;
                }

                // Type is nullable and we have value is not empty.
                // Get the underlying type and continue the conversion.
                type = Nullable.GetUnderlyingType(type);
            }

            if (type.IsEnum)
            {
                return ConvertEnum<T>(type, value);
            }

            return (T)System.Convert.ChangeType(value, type, formatProvider);
        }

        private static T ConvertEnum<T>(Type type, string value)
        {
            return (T)Enum.Parse(type, value, true);
        }
    }
}