namespace ApplicationSettings.Internal
{
    using System;
    using System.Globalization;
    using System.Reflection;

    /// <summary>
    /// Extension methods.
    /// </summary>
    internal static class Extensions
    {
        /// <summary>
        /// Formats the string.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <returns>
        /// Formatted string.
        /// </returns>
        public static string FormatWith(this string value, params object[] args)
        {
            return string.Format(CultureInfo.InvariantCulture, value, args);
        }

        /// <summary>
        /// Gets the custom attribute.
        /// </summary>
        /// <typeparam name="T">Type of the attribute.</typeparam>
        /// <param name="propertyInfo">Property info.</param>
        /// <returns>Attribute or null</returns>
        public static T GetCustomAttribute<T>(this PropertyInfo propertyInfo) where T : class
        {
            return Attribute.GetCustomAttribute(propertyInfo, typeof(T)) as T;
        }
    }
}