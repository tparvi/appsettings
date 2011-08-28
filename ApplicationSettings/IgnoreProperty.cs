namespace ApplicationSettings
{
    using System;

    /// <summary>
    /// Describes that property should be ignored.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class IgnoreProperty : Attribute
    {
    }
}
