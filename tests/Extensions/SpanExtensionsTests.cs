using FluentAssertions;

namespace LestalyTest.Extensions;

[TestClass()]
public class SpanExtensionsTests
{
    [TestMethod()]
    public void AsReadOnlySpan()
    {
        var data = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x9A, 0xBC, 0xDE, 0xF0, };

        data.AsReadOnlySpan().ToArray().Should().Equal(data);
        data.AsReadOnlySpan(0).ToArray().Should().Equal(data);
        data.AsReadOnlySpan(1).ToArray().Should().Equal(data.Skip(1));
        data.AsReadOnlySpan(data.Length).ToArray().Should().BeEmpty();
        data.AsReadOnlySpan(0, 3).ToArray().Should().Equal(data.Take(3));
        data.AsReadOnlySpan(1, 3).ToArray().Should().Equal(data.Skip(1).Take(3));
        data.AsReadOnlySpan(7, 1).ToArray().Should().Equal(data.Skip(7).Take(1));
    }

    [TestMethod()]
    public void AtLittleEndian()
    {
        var data = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x9A, 0xBC, 0xDE, 0xF0, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, };

        data.AsReadOnlySpan(0, 1).AtLittleEndian<byte>().Should().Be(0x12);
        data.AsReadOnlySpan(1, 1).AtLittleEndian<byte>().Should().Be(0x34);
        data.AsReadOnlySpan(0, 2).AtLittleEndian<ushort>().Should().Be(0x3412);
        data.AsReadOnlySpan(1, 2).AtLittleEndian<ushort>().Should().Be(0x5634);
        data.AsReadOnlySpan(0, 2).AtLittleEndian<short>().Should().Be(0x3412);
        data.AsReadOnlySpan(1, 2).AtLittleEndian<short>().Should().Be(0x5634);
        data.AsReadOnlySpan(0, 4).AtLittleEndian<uint>().Should().Be(0x78563412);
        data.AsReadOnlySpan(1, 4).AtLittleEndian<uint>().Should().Be(0x9A785634);

        data.AsReadOnlySpan(0, 3).AtLittleEndian<uint>().Should().Be(0x563412);
        data.AsReadOnlySpan(1, 3).AtLittleEndian<uint>().Should().Be(0x785634);

        data.AsSpan(0, 4).AtLittleEndian<uint>().Should().Be(0x78563412);
        data[0..4].AtLittleEndian<uint>().Should().Be(0x78563412);
    }

    [TestMethod()]
    public void AtBigEndian()
    {
        var data = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x9A, 0xBC, 0xDE, 0xF0, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, };

        data.AsReadOnlySpan(0, 1).AtBigEndian<byte>().Should().Be(0x12);
        data.AsReadOnlySpan(1, 1).AtBigEndian<byte>().Should().Be(0x34);
        data.AsReadOnlySpan(0, 2).AtBigEndian<ushort>().Should().Be(0x1234);
        data.AsReadOnlySpan(1, 2).AtBigEndian<ushort>().Should().Be(0x3456);
        data.AsReadOnlySpan(0, 2).AtBigEndian<short>().Should().Be(0x1234);
        data.AsReadOnlySpan(1, 2).AtBigEndian<short>().Should().Be(0x3456);
        data.AsReadOnlySpan(0, 4).AtBigEndian<uint>().Should().Be(0x12345678);
        data.AsReadOnlySpan(1, 4).AtBigEndian<uint>().Should().Be(0x3456789A);

        data.AsReadOnlySpan(0, 3).AtBigEndian<uint>().Should().Be(0x123456);
        data.AsReadOnlySpan(1, 3).AtBigEndian<uint>().Should().Be(0x345678);

        data.AsSpan(0, 4).AtBigEndian<uint>().Should().Be(0x12345678);
        data[0..4].AtBigEndian<uint>().Should().Be(0x12345678);
    }

    [TestMethod()]
    public void AtEndian()
    {
        var data = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x9A, 0xBC, 0xDE, 0xF0, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, };

        data.AsReadOnlySpan(0, 1).AtEndian<byte>(little: true).Should().Be(0x12);
        data.AsReadOnlySpan(1, 1).AtEndian<byte>(little: true).Should().Be(0x34);
        data.AsReadOnlySpan(0, 2).AtEndian<ushort>(little: true).Should().Be(0x3412);
        data.AsReadOnlySpan(1, 2).AtEndian<ushort>(little: true).Should().Be(0x5634);
        data.AsReadOnlySpan(0, 2).AtEndian<short>(little: true).Should().Be(0x3412);
        data.AsReadOnlySpan(1, 2).AtEndian<short>(little: true).Should().Be(0x5634);
        data.AsReadOnlySpan(0, 4).AtEndian<uint>(little: true).Should().Be(0x78563412);
        data.AsReadOnlySpan(1, 4).AtEndian<uint>(little: true).Should().Be(0x9A785634);

        data.AsReadOnlySpan(0, 1).AtEndian<byte>(little: false).Should().Be(0x12);
        data.AsReadOnlySpan(1, 1).AtEndian<byte>(little: false).Should().Be(0x34);
        data.AsReadOnlySpan(0, 2).AtEndian<ushort>(little: false).Should().Be(0x1234);
        data.AsReadOnlySpan(1, 2).AtEndian<ushort>(little: false).Should().Be(0x3456);
        data.AsReadOnlySpan(0, 2).AtEndian<short>(little: false).Should().Be(0x1234);
        data.AsReadOnlySpan(1, 2).AtEndian<short>(little: false).Should().Be(0x3456);
        data.AsReadOnlySpan(0, 4).AtEndian<uint>(little: false).Should().Be(0x12345678);
        data.AsReadOnlySpan(1, 4).AtEndian<uint>(little: false).Should().Be(0x3456789A);

        data.AsSpan(0, 4).AtEndian<uint>(little: true).Should().Be(0x78563412);
        data[0..4].AtEndian<uint>(little: true).Should().Be(0x78563412);

    }

}
