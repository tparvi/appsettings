namespace ApplicationSettings
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Extension methods for strings.
    /// </summary>
    internal static class StringExtensions
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
    }
}
