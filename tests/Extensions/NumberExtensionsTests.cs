using FluentAssertions;

namespace LestalyTest.Extensions;

[TestClass()]
public class NumberExtensionsTests
{
    [TestMethod()]
    public void ToHumanize_Int32()
    {
        ((int)999999).ToHumanize(si: false).Should().Be("976k");
        ((int)999999).ToHumanize(si: true).Should().Be("999k");
        ((int)12345678).ToHumanize(si: false).Should().Be("11.7M");
        ((int)12345678).ToHumanize(si: true).Should().Be("12.3M");
        ((int)-2147483648).ToHumanize(si: false).Should().Be("-2.00G");
        ((int)-2147483648).ToHumanize(si: true).Should().Be("-2.14G");
    }

    [TestMethod()]
    public void ToHumanize_UInt32()
    {
        ((uint)999999u).ToHumanize(si: false).Should().Be("976k");
        ((uint)999999u).ToHumanize(si: true).Should().Be("999k");
        ((uint)12345678u).ToHumanize(si: false).Should().Be("11.7M");
        ((uint)12345678u).ToHumanize(si: true).Should().Be("12.3M");
        ((uint)2147483647u).ToHumanize(si: false).Should().Be("1.99G");
        ((uint)2147483647u).ToHumanize(si: true).Should().Be("2.14G");
    }

    [TestMethod()]
    public void ToHumanize_Int64()
    {
        ((long)999999).ToHumanize(si: false).Should().Be("976k");
        ((long)999999).ToHumanize(si: true).Should().Be("999k");
        ((long)12345678).ToHumanize(si: false).Should().Be("11.7M");
        ((long)12345678).ToHumanize(si: true).Should().Be("12.3M");
        ((long)-9223372036854775808).ToHumanize(si: false).Should().Be("-8.00E");
        ((long)-9223372036854775808).ToHumanize(si: true).Should().Be("-9.22E");
    }

    [TestMethod()]
    public void ToHumanize_UInt64()
    {
        ((ulong)999999u).ToHumanize(si: false).Should().Be("976k");
        ((ulong)999999u).ToHumanize(si: true).Should().Be("999k");
        ((ulong)12345678u).ToHumanize(si: false).Should().Be("11.7M");
        ((ulong)12345678u).ToHumanize(si: true).Should().Be("12.3M");
        ((ulong)18446744073709551615u).ToHumanize(si: false).Should().Be("15.9E");
        ((ulong)18446744073709551615u).ToHumanize(si: true).Should().Be("18.4E");
    }
}
