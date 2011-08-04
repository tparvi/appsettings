using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationSettingsTests
{
    using System.Globalization;
    using System.Reflection;

    using ApplicationSettings;

    using ApplicationSettingsTests.Configurations;

    using NUnit.Framework;

    [TestFixture]
    public class When_Converting_Nullable_Type : TestBase
    {
        [Test]
        public void Then_should_convert_string_to_nullable_int()
        {
            var value = TypeConverter.Convert<int?>("1");
            
            Assert.AreEqual(1, value);
        }

        [Test]
        public void Then_should_convert_empty_string_to_nullable_int_with_null_value()
        {
            var value = TypeConverter.Convert<int?>(string.Empty);
            
            Assert.IsFalse(value.HasValue);
        }
    }
}
