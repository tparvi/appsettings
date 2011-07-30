namespace ApplicationSettings
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Exception related to <see cref="AppSettings"/>.
    /// </summary>
    [Serializable]
    public class AppSettingException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppSettingException"/> class.
        /// </summary>
        public AppSettingException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppSettingException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public AppSettingException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppSettingException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        public AppSettingException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppSettingException"/> class.
        /// </summary>
        /// <param name="info">
        /// The info.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        protected AppSettingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
