using System;

namespace ApplicationSettingsTests
{
    using System.Globalization;

    using ApplicationSettings;

    using NUnit.Framework;

    [TestFixture]
    public class When_Converting_Basic_Type : TestBase
    {
        [Test]
        public void Then_should_convert_string_to_int()
        {
            var value = TypeConverter.Convert<int>("123");

            Assert.AreEqual(123, value);
        }

        [Test]
        public void Then_should_convert_string_to_sbyte()
        {
            var value = TypeConverter.Convert<sbyte>("127");

            Assert.AreEqual(127, value);
        }

        [Test]
        public void Then_should_convert_string_to_byte()
        {
            var value = TypeConverter.Convert<byte>("255");

            Assert.AreEqual(255, value);
        }

        [Test]
        public void Then_should_convert_string_to_char()
        {
            var value = TypeConverter.Convert<char>("c");

            Assert.AreEqual('c', value);
        }

        [Test]
        public void Then_should_convert_string_to_short()
        {
            var value = TypeConverter.Convert<short>("32000");

            Assert.AreEqual(32000, value);
        }

        [Test]
        public void Then_should_convert_string_to_ushort()
        {
            var value = TypeConverter.Convert<ushort>("65000");

            Assert.AreEqual(65000, value);
        }

        [Test]
        public void Then_should_convert_string_to_uint()
        {
            var value = TypeConverter.Convert<uint>("4000000000");

            Assert.AreEqual(4000000000, value);
        }

        [Test]
        public void Then_should_convert_string_to_long()
        {
            var value = TypeConverter.Convert<long>("9223372036854775807"); // max value

            Assert.AreEqual(9223372036854775807, value);
        }

        [Test]
        public void Then_should_convert_string_to_ulong()
        {
            var value = TypeConverter.Convert<ulong>("18446744073709551615"); // max value

            Assert.AreEqual(18446744073709551615, value);
        }

        [Test]
        public void Then_should_convert_string_to_decimal()
        {
            var value = TypeConverter.Convert<decimal>("1.2");

            Assert.AreEqual(1.2, value);
        }

        [Test]
        public void Then_should_convert_string_to_double()
        {
            var value = TypeConverter.Convert<double>("1.123");

            Assert.AreEqual(1.123, value);
        }

        [Test]
        public void Then_should_convert_string_to_float()
        {
            var value = TypeConverter.Convert<float>("4.5");

            Assert.AreEqual(4.5, value);
        }

        [Test]
        public void Then_should_convert_string_to_bool()
        {
            var value = TypeConverter.Convert<bool>("true");

            Assert.AreEqual(true, value);
        }

        [Test]
        public void Then_should_convert_string_to_datetime()
        {
            var expectedValue = new DateTime(2011, 8, 1, 16, 00, 00);

            var value = TypeConverter.Convert<DateTime>(expectedValue.ToString(CultureInfo.InvariantCulture));

            Assert.AreEqual(expectedValue, value);            
        }
    }
}
