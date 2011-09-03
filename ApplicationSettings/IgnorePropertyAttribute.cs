namespace ApplicationSettings
{
    using System;

    /// <summary>
    /// Describes that property should be ignored.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes", Justification = "Ok")]
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class IgnorePropertyAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets a value indicating whether property
        /// should be written into when settings are loaded.
        /// </summary>
        public bool EnableWriting { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether property
        /// should be read from when saving settings.
        /// </summary>
        public bool EnableReading { get; set; }
    }
}
