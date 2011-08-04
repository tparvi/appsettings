using System;

namespace ApplicationSettingsTests
{
    using System.Globalization;

    using ApplicationSettings;

    using NUnit.Framework;

    public enum SimpleEnum
    {
        Starting,
        Started,
        Shutdown
    }

    [TestFixture]
    public class When_Converting_Enum : TestBase
    {
        [Test]
        public void Then_should_convert_string_to_enum()
        {
            var value = TypeConverter.Convert<SimpleEnum>("Starting");

            Assert.AreEqual(SimpleEnum.Starting, value);
        }

        [Test]
        public void Then_should_convert_case_insensitive_string_to_enum()
        {
            var value = TypeConverter.Convert<SimpleEnum>("starting");

            Assert.AreEqual(SimpleEnum.Starting, value);
        }

        [Test]
        public void Then_should_convert_numeric_string_to_enum()
        {
            var value = TypeConverter.Convert<SimpleEnum>("1");

            Assert.AreEqual(SimpleEnum.Started, value);
        }

        [Test]
        public void Then_should_convert_empty_string_to_nullable_enum_without_value()
        {
            var value = TypeConverter.Convert<SimpleEnum?>("");

            Assert.IsFalse(value.HasValue);
        }

        [Test]
        public void Then_should_convert_string_to_nullable_enum()
        {
            var value = TypeConverter.Convert<SimpleEnum?>("Starting");

            Assert.AreEqual(SimpleEnum.Starting, value);
        }
    }
}
