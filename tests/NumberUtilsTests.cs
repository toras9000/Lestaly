using System.Globalization;

namespace LestalyTest;

[TestClass()]
public class NumberUtilsTests
{
    [TestMethod()]
    public void ToHumanize_Signed_Bin()
    {
        var si = false;
        NumberUtils.ToHumanize(0, si).Should().Be("0");
        NumberUtils.ToHumanize(1, si).Should().Be("1");
        NumberUtils.ToHumanize(999, si).Should().Be("999");
        NumberUtils.ToHumanize(1000, si).Should().Be("1000");
        NumberUtils.ToHumanize(1023, si).Should().Be("1023");
        NumberUtils.ToHumanize(1024, si).Should().Be("1.00k");
        NumberUtils.ToHumanize(1999, si).Should().Be("1.95k");
        NumberUtils.ToHumanize(2000, si).Should().Be("1.95k");
        NumberUtils.ToHumanize(2047, si).Should().Be("1.99k");
        NumberUtils.ToHumanize(2048, si).Should().Be("2.00k");
        NumberUtils.ToHumanize(2099, si).Should().Be("2.04k");
        NumberUtils.ToHumanize(2100, si).Should().Be("2.05k");
        NumberUtils.ToHumanize(2150, si).Should().Be("2.09k");
        NumberUtils.ToHumanize(2151, si).Should().Be("2.10k");
        NumberUtils.ToHumanize(9999, si).Should().Be("9.76k");
        NumberUtils.ToHumanize(10000, si).Should().Be("9.76k");
        NumberUtils.ToHumanize(10239, si).Should().Be("9.99k");
        NumberUtils.ToHumanize(10240, si).Should().Be("10.0k");
        NumberUtils.ToHumanize(10999, si).Should().Be("10.7k");
        NumberUtils.ToHumanize(11000, si).Should().Be("10.7k");
        NumberUtils.ToHumanize(11263, si).Should().Be("10.9k");
        NumberUtils.ToHumanize(11264, si).Should().Be("11.0k");
        NumberUtils.ToHumanize(999999, si).Should().Be("976k");
        NumberUtils.ToHumanize(1000000, si).Should().Be("976k");
        NumberUtils.ToHumanize(1048575, si).Should().Be("1023k");
        NumberUtils.ToHumanize(1048576, si).Should().Be("1.00M");
        NumberUtils.ToHumanize(999999999, si).Should().Be("953M");
        NumberUtils.ToHumanize(1000000000, si).Should().Be("953M");
        NumberUtils.ToHumanize(1073741823, si).Should().Be("1023M");
        NumberUtils.ToHumanize(1073741824, si).Should().Be("1.00G");
        NumberUtils.ToHumanize(2147483647, si).Should().Be("1.99G");
        NumberUtils.ToHumanize(2147483648, si).Should().Be("2.00G");
        NumberUtils.ToHumanize(9223372036854775807, si).Should().Be("7.99E");

        NumberUtils.ToHumanize(-1, si).Should().Be("-1");
        NumberUtils.ToHumanize(-999, si).Should().Be("-999");
        NumberUtils.ToHumanize(-1000, si).Should().Be("-1000");
        NumberUtils.ToHumanize(-1023, si).Should().Be("-1023");
        NumberUtils.ToHumanize(-1024, si).Should().Be("-1.00k");
        NumberUtils.ToHumanize(-1999, si).Should().Be("-1.95k");
        NumberUtils.ToHumanize(-2000, si).Should().Be("-1.95k");
        NumberUtils.ToHumanize(-2047, si).Should().Be("-1.99k");
        NumberUtils.ToHumanize(-2048, si).Should().Be("-2.00k");
        NumberUtils.ToHumanize(-2099, si).Should().Be("-2.04k");
        NumberUtils.ToHumanize(-2100, si).Should().Be("-2.05k");
        NumberUtils.ToHumanize(-2150, si).Should().Be("-2.09k");
        NumberUtils.ToHumanize(-2151, si).Should().Be("-2.10k");
        NumberUtils.ToHumanize(-9999, si).Should().Be("-9.76k");
        NumberUtils.ToHumanize(-10000, si).Should().Be("-9.76k");
        NumberUtils.ToHumanize(-10239, si).Should().Be("-9.99k");
        NumberUtils.ToHumanize(-10240, si).Should().Be("-10.0k");
        NumberUtils.ToHumanize(-10999, si).Should().Be("-10.7k");
        NumberUtils.ToHumanize(-11000, si).Should().Be("-10.7k");
        NumberUtils.ToHumanize(-11263, si).Should().Be("-10.9k");
        NumberUtils.ToHumanize(-11264, si).Should().Be("-11.0k");
        NumberUtils.ToHumanize(-999999, si).Should().Be("-976k");
        NumberUtils.ToHumanize(-1000000, si).Should().Be("-976k");
        NumberUtils.ToHumanize(-1048575, si).Should().Be("-1023k");
        NumberUtils.ToHumanize(-1048576, si).Should().Be("-1.00M");
        NumberUtils.ToHumanize(-999999999, si).Should().Be("-953M");
        NumberUtils.ToHumanize(-1000000000, si).Should().Be("-953M");
        NumberUtils.ToHumanize(-1073741823, si).Should().Be("-1023M");
        NumberUtils.ToHumanize(-1073741824, si).Should().Be("-1.00G");
        NumberUtils.ToHumanize(-2147483647, si).Should().Be("-1.99G");
        NumberUtils.ToHumanize(-2147483648, si).Should().Be("-2.00G");
        NumberUtils.ToHumanize(-9223372036854775807, si).Should().Be("-7.99E");
        NumberUtils.ToHumanize(-9223372036854775808, si).Should().Be("-8.00E");
    }

    [TestMethod()]
    public void ToHumanize_Signed_Si()
    {
        var si = true;
        NumberUtils.ToHumanize(0, si).Should().Be("0");
        NumberUtils.ToHumanize(1, si).Should().Be("1");
        NumberUtils.ToHumanize(999, si).Should().Be("999");
        NumberUtils.ToHumanize(1000, si).Should().Be("1.00k");
        NumberUtils.ToHumanize(1023, si).Should().Be("1.02k");
        NumberUtils.ToHumanize(1024, si).Should().Be("1.02k");
        NumberUtils.ToHumanize(1999, si).Should().Be("1.99k");
        NumberUtils.ToHumanize(2000, si).Should().Be("2.00k");
        NumberUtils.ToHumanize(2047, si).Should().Be("2.04k");
        NumberUtils.ToHumanize(2048, si).Should().Be("2.04k");
        NumberUtils.ToHumanize(2099, si).Should().Be("2.09k");
        NumberUtils.ToHumanize(2100, si).Should().Be("2.10k");
        NumberUtils.ToHumanize(2150, si).Should().Be("2.15k");
        NumberUtils.ToHumanize(2151, si).Should().Be("2.15k");
        NumberUtils.ToHumanize(9999, si).Should().Be("9.99k");
        NumberUtils.ToHumanize(10000, si).Should().Be("10.0k");
        NumberUtils.ToHumanize(10239, si).Should().Be("10.2k");
        NumberUtils.ToHumanize(10240, si).Should().Be("10.2k");
        NumberUtils.ToHumanize(10999, si).Should().Be("10.9k");
        NumberUtils.ToHumanize(11000, si).Should().Be("11.0k");
        NumberUtils.ToHumanize(11263, si).Should().Be("11.2k");
        NumberUtils.ToHumanize(11264, si).Should().Be("11.2k");
        NumberUtils.ToHumanize(999999, si).Should().Be("999k");
        NumberUtils.ToHumanize(1000000, si).Should().Be("1.00M");
        NumberUtils.ToHumanize(1048575, si).Should().Be("1.04M");
        NumberUtils.ToHumanize(1048576, si).Should().Be("1.04M");
        NumberUtils.ToHumanize(999999999, si).Should().Be("999M");
        NumberUtils.ToHumanize(1000000000, si).Should().Be("1.00G");
        NumberUtils.ToHumanize(1073741823, si).Should().Be("1.07G");
        NumberUtils.ToHumanize(1073741824, si).Should().Be("1.07G");
        NumberUtils.ToHumanize(2147483647, si).Should().Be("2.14G");
        NumberUtils.ToHumanize(2147483648, si).Should().Be("2.14G");
        NumberUtils.ToHumanize(9223372036854775807, si).Should().Be("9.22E");

        NumberUtils.ToHumanize(-1, si).Should().Be("-1");
        NumberUtils.ToHumanize(-999, si).Should().Be("-999");
        NumberUtils.ToHumanize(-1000, si).Should().Be("-1.00k");
        NumberUtils.ToHumanize(-1023, si).Should().Be("-1.02k");
        NumberUtils.ToHumanize(-1024, si).Should().Be("-1.02k");
        NumberUtils.ToHumanize(-1999, si).Should().Be("-1.99k");
        NumberUtils.ToHumanize(-2000, si).Should().Be("-2.00k");
        NumberUtils.ToHumanize(-2047, si).Should().Be("-2.04k");
        NumberUtils.ToHumanize(-2048, si).Should().Be("-2.04k");
        NumberUtils.ToHumanize(-2099, si).Should().Be("-2.09k");
        NumberUtils.ToHumanize(-2100, si).Should().Be("-2.10k");
        NumberUtils.ToHumanize(-2150, si).Should().Be("-2.15k");
        NumberUtils.ToHumanize(-2151, si).Should().Be("-2.15k");
        NumberUtils.ToHumanize(-9999, si).Should().Be("-9.99k");
        NumberUtils.ToHumanize(-10000, si).Should().Be("-10.0k");
        NumberUtils.ToHumanize(-10239, si).Should().Be("-10.2k");
        NumberUtils.ToHumanize(-10240, si).Should().Be("-10.2k");
        NumberUtils.ToHumanize(-10999, si).Should().Be("-10.9k");
        NumberUtils.ToHumanize(-11000, si).Should().Be("-11.0k");
        NumberUtils.ToHumanize(-11263, si).Should().Be("-11.2k");
        NumberUtils.ToHumanize(-11264, si).Should().Be("-11.2k");
        NumberUtils.ToHumanize(-999999, si).Should().Be("-999k");
        NumberUtils.ToHumanize(-1000000, si).Should().Be("-1.00M");
        NumberUtils.ToHumanize(-1048575, si).Should().Be("-1.04M");
        NumberUtils.ToHumanize(-1048576, si).Should().Be("-1.04M");
        NumberUtils.ToHumanize(-999999999, si).Should().Be("-999M");
        NumberUtils.ToHumanize(-1000000000, si).Should().Be("-1.00G");
        NumberUtils.ToHumanize(-1073741823, si).Should().Be("-1.07G");
        NumberUtils.ToHumanize(-1073741824, si).Should().Be("-1.07G");
        NumberUtils.ToHumanize(-2147483647, si).Should().Be("-2.14G");
        NumberUtils.ToHumanize(-2147483648, si).Should().Be("-2.14G");
        NumberUtils.ToHumanize(-9223372036854775807, si).Should().Be("-9.22E");
        NumberUtils.ToHumanize(-9223372036854775808, si).Should().Be("-9.22E");

    }

    [TestMethod()]
    public void ToHumanize_Signed_Format()
    {
        var ni = new NumberFormatInfo();
        ni.NumberDecimalSeparator = "/";
        ni.NegativeSign = "!";

        static string formatter(string num, char? sup) => $"<{num}>[{sup}]";

        NumberUtils.ToHumanize(0, si: false, ni, formatter).Should().Be("<0>[]");
        NumberUtils.ToHumanize(0, si: true, ni, formatter).Should().Be("<0>[]");
        NumberUtils.ToHumanize(12, si: false, ni, formatter).Should().Be("<12>[]");
        NumberUtils.ToHumanize(12, si: true, ni, formatter).Should().Be("<12>[]");
        NumberUtils.ToHumanize(345, si: false, ni, formatter).Should().Be("<345>[]");
        NumberUtils.ToHumanize(345, si: true, ni, formatter).Should().Be("<345>[]");
        NumberUtils.ToHumanize(2048, si: false, ni, formatter).Should().Be("<2/00>[k]");
        NumberUtils.ToHumanize(2048, si: true, ni, formatter).Should().Be("<2/04>[k]");
        NumberUtils.ToHumanize(30000, si: false, ni, formatter).Should().Be("<29/2>[k]");
        NumberUtils.ToHumanize(30000, si: true, ni, formatter).Should().Be("<30/0>[k]");
        NumberUtils.ToHumanize(400000, si: false, ni, formatter).Should().Be("<390>[k]");
        NumberUtils.ToHumanize(400000, si: true, ni, formatter).Should().Be("<400>[k]");
        NumberUtils.ToHumanize(9223372036854775807, si: false, ni, formatter).Should().Be("<7/99>[E]");
        NumberUtils.ToHumanize(9223372036854775807, si: true, ni, formatter).Should().Be("<9/22>[E]");

        NumberUtils.ToHumanize(-12, si: false, ni, formatter).Should().Be("<!12>[]");
        NumberUtils.ToHumanize(-12, si: true, ni, formatter).Should().Be("<!12>[]");
        NumberUtils.ToHumanize(-345, si: false, ni, formatter).Should().Be("<!345>[]");
        NumberUtils.ToHumanize(-345, si: true, ni, formatter).Should().Be("<!345>[]");
        NumberUtils.ToHumanize(-2048, si: false, ni, formatter).Should().Be("<!2/00>[k]");
        NumberUtils.ToHumanize(-2048, si: true, ni, formatter).Should().Be("<!2/04>[k]");
        NumberUtils.ToHumanize(-30000, si: false, ni, formatter).Should().Be("<!29/2>[k]");
        NumberUtils.ToHumanize(-30000, si: true, ni, formatter).Should().Be("<!30/0>[k]");
        NumberUtils.ToHumanize(-400000, si: false, ni, formatter).Should().Be("<!390>[k]");
        NumberUtils.ToHumanize(-400000, si: true, ni, formatter).Should().Be("<!400>[k]");
        NumberUtils.ToHumanize(-9223372036854775808, si: false, ni, formatter).Should().Be("<!8/00>[E]");
        NumberUtils.ToHumanize(-9223372036854775808, si: true, ni, formatter).Should().Be("<!9/22>[E]");

    }

    [TestMethod()]
    public void ToHumanize_Unsigned_Bin()
    {
        var si = false;
        NumberUtils.ToHumanize(0u, si).Should().Be("0");
        NumberUtils.ToHumanize(1u, si).Should().Be("1");
        NumberUtils.ToHumanize(999u, si).Should().Be("999");
        NumberUtils.ToHumanize(1000u, si).Should().Be("1000");
        NumberUtils.ToHumanize(1023u, si).Should().Be("1023");
        NumberUtils.ToHumanize(1024u, si).Should().Be("1.00k");
        NumberUtils.ToHumanize(1999u, si).Should().Be("1.95k");
        NumberUtils.ToHumanize(2000u, si).Should().Be("1.95k");
        NumberUtils.ToHumanize(2047u, si).Should().Be("1.99k");
        NumberUtils.ToHumanize(2048u, si).Should().Be("2.00k");
        NumberUtils.ToHumanize(2099u, si).Should().Be("2.04k");
        NumberUtils.ToHumanize(2100u, si).Should().Be("2.05k");
        NumberUtils.ToHumanize(2150u, si).Should().Be("2.09k");
        NumberUtils.ToHumanize(2151u, si).Should().Be("2.10k");
        NumberUtils.ToHumanize(9999u, si).Should().Be("9.76k");
        NumberUtils.ToHumanize(10000u, si).Should().Be("9.76k");
        NumberUtils.ToHumanize(10239u, si).Should().Be("9.99k");
        NumberUtils.ToHumanize(10240u, si).Should().Be("10.0k");
        NumberUtils.ToHumanize(10999u, si).Should().Be("10.7k");
        NumberUtils.ToHumanize(11000u, si).Should().Be("10.7k");
        NumberUtils.ToHumanize(11263u, si).Should().Be("10.9k");
        NumberUtils.ToHumanize(11264u, si).Should().Be("11.0k");
        NumberUtils.ToHumanize(999999u, si).Should().Be("976k");
        NumberUtils.ToHumanize(1000000u, si).Should().Be("976k");
        NumberUtils.ToHumanize(1048575u, si).Should().Be("1023k");
        NumberUtils.ToHumanize(1048576u, si).Should().Be("1.00M");
        NumberUtils.ToHumanize(999999999u, si).Should().Be("953M");
        NumberUtils.ToHumanize(1000000000u, si).Should().Be("953M");
        NumberUtils.ToHumanize(1073741823u, si).Should().Be("1023M");
        NumberUtils.ToHumanize(1073741824u, si).Should().Be("1.00G");
        NumberUtils.ToHumanize(2147483647u, si).Should().Be("1.99G");
        NumberUtils.ToHumanize(2147483648u, si).Should().Be("2.00G");
        NumberUtils.ToHumanize(9223372036854775807u, si).Should().Be("7.99E");
        NumberUtils.ToHumanize(9223372036854775808u, si).Should().Be("8.00E");
        NumberUtils.ToHumanize(18446744073709551615u, si).Should().Be("15.9E");

    }

    [TestMethod()]
    public void ToHumanize_Unsigned_Si()
    {
        var si = true;
        NumberUtils.ToHumanize(0u, si).Should().Be("0");
        NumberUtils.ToHumanize(1u, si).Should().Be("1");
        NumberUtils.ToHumanize(999u, si).Should().Be("999");
        NumberUtils.ToHumanize(1000u, si).Should().Be("1.00k");
        NumberUtils.ToHumanize(1023u, si).Should().Be("1.02k");
        NumberUtils.ToHumanize(1024u, si).Should().Be("1.02k");
        NumberUtils.ToHumanize(1999u, si).Should().Be("1.99k");
        NumberUtils.ToHumanize(2000u, si).Should().Be("2.00k");
        NumberUtils.ToHumanize(2047u, si).Should().Be("2.04k");
        NumberUtils.ToHumanize(2048u, si).Should().Be("2.04k");
        NumberUtils.ToHumanize(2099u, si).Should().Be("2.09k");
        NumberUtils.ToHumanize(2100u, si).Should().Be("2.10k");
        NumberUtils.ToHumanize(2150u, si).Should().Be("2.15k");
        NumberUtils.ToHumanize(2151u, si).Should().Be("2.15k");
        NumberUtils.ToHumanize(9999u, si).Should().Be("9.99k");
        NumberUtils.ToHumanize(10000u, si).Should().Be("10.0k");
        NumberUtils.ToHumanize(10239u, si).Should().Be("10.2k");
        NumberUtils.ToHumanize(10240u, si).Should().Be("10.2k");
        NumberUtils.ToHumanize(10999u, si).Should().Be("10.9k");
        NumberUtils.ToHumanize(11000u, si).Should().Be("11.0k");
        NumberUtils.ToHumanize(11263u, si).Should().Be("11.2k");
        NumberUtils.ToHumanize(11264u, si).Should().Be("11.2k");
        NumberUtils.ToHumanize(999999u, si).Should().Be("999k");
        NumberUtils.ToHumanize(1000000u, si).Should().Be("1.00M");
        NumberUtils.ToHumanize(1048575u, si).Should().Be("1.04M");
        NumberUtils.ToHumanize(1048576u, si).Should().Be("1.04M");
        NumberUtils.ToHumanize(999999999u, si).Should().Be("999M");
        NumberUtils.ToHumanize(1000000000u, si).Should().Be("1.00G");
        NumberUtils.ToHumanize(1073741823u, si).Should().Be("1.07G");
        NumberUtils.ToHumanize(1073741824u, si).Should().Be("1.07G");
        NumberUtils.ToHumanize(2147483647u, si).Should().Be("2.14G");
        NumberUtils.ToHumanize(2147483648u, si).Should().Be("2.14G");
        NumberUtils.ToHumanize(9223372036854775807u, si).Should().Be("9.22E");
        NumberUtils.ToHumanize(9223372036854775808u, si).Should().Be("9.22E");
        NumberUtils.ToHumanize(18446744073709551615u, si).Should().Be("18.4E");


    }

    [TestMethod()]
    public void ToHumanize_Unsigned_Format()
    {
        var ni = new NumberFormatInfo();
        ni.NumberDecimalSeparator = "/";
        ni.NegativeSign = "!";

        static string formatter(string num, char? sup) => $"<{num}>[{sup}]";

        NumberUtils.ToHumanize(0u, si: false, ni, formatter).Should().Be("<0>[]");
        NumberUtils.ToHumanize(0u, si: true, ni, formatter).Should().Be("<0>[]");
        NumberUtils.ToHumanize(12u, si: false, ni, formatter).Should().Be("<12>[]");
        NumberUtils.ToHumanize(12u, si: true, ni, formatter).Should().Be("<12>[]");
        NumberUtils.ToHumanize(345u, si: false, ni, formatter).Should().Be("<345>[]");
        NumberUtils.ToHumanize(345u, si: true, ni, formatter).Should().Be("<345>[]");
        NumberUtils.ToHumanize(2048u, si: false, ni, formatter).Should().Be("<2/00>[k]");
        NumberUtils.ToHumanize(2048u, si: true, ni, formatter).Should().Be("<2/04>[k]");
        NumberUtils.ToHumanize(30000u, si: false, ni, formatter).Should().Be("<29/2>[k]");
        NumberUtils.ToHumanize(30000u, si: true, ni, formatter).Should().Be("<30/0>[k]");
        NumberUtils.ToHumanize(400000u, si: false, ni, formatter).Should().Be("<390>[k]");
        NumberUtils.ToHumanize(400000u, si: true, ni, formatter).Should().Be("<400>[k]");
        NumberUtils.ToHumanize(9223372036854775807u, si: false, ni, formatter).Should().Be("<7/99>[E]");
        NumberUtils.ToHumanize(9223372036854775807u, si: true, ni, formatter).Should().Be("<9/22>[E]");
        NumberUtils.ToHumanize(18446744073709551615u, si: false, ni, formatter).Should().Be("<15/9>[E]");
        NumberUtils.ToHumanize(18446744073709551615u, si: true, ni, formatter).Should().Be("<18/4>[E]");

    }
}