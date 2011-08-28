namespace ApplicationSettings.Internal
{
    using System.Globalization;

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
    }
}