namespace LestalyTest.Extensions;

[TestClass()]
public class MemoryExtensionsTests
{
    [TestMethod()]
    public void AsReadOnly()
    {
        var array = Array.Empty<byte>();

        array.AsSpan().OverloadTest().Should().Be("Span");
        array.AsSpan().AsReadOnly().OverloadTest().Should().Be("ReadOnlySpan");

        array.AsMemory().OverloadTest().Should().Be("Memory");
        array.AsMemory().AsReadOnly().OverloadTest().Should().Be("ReadOnlyMemory");
    }

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
        data.AsReadOnlySpan(2..4).ToArray().Should().Equal(data.Skip(2).Take(2));
    }

    [TestMethod()]
    public void AsReadOnlyMemory()
    {
        var data = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x9A, 0xBC, 0xDE, 0xF0, };

        data.AsReadOnlyMemory().ToArray().Should().Equal(data);
        data.AsReadOnlyMemory(0).ToArray().Should().Equal(data);
        data.AsReadOnlyMemory(1).ToArray().Should().Equal(data.Skip(1));
        data.AsReadOnlyMemory(data.Length).ToArray().Should().BeEmpty();
        data.AsReadOnlyMemory(0, 3).ToArray().Should().Equal(data.Take(3));
        data.AsReadOnlyMemory(1, 3).ToArray().Should().Equal(data.Skip(1).Take(3));
        data.AsReadOnlyMemory(7, 1).ToArray().Should().Equal(data.Skip(7).Take(1));
        data.AsReadOnlyMemory(2..4).ToArray().Should().Equal(data.Skip(2).Take(2));
    }

    [TestMethod()]
    public void AsLittleEndian_BinaryInteger()
    {
        var data = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x9A, 0xBC, 0xDE, 0xF0, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, };

        data.AsReadOnlySpan(0).AsLittleEndian<byte>().Should().Be(0x12);
        data.AsReadOnlySpan(1).AsLittleEndian<byte>().Should().Be(0x34);
        data.AsReadOnlySpan(0).AsLittleEndian<ushort>().Should().Be(0x3412);
        data.AsReadOnlySpan(1).AsLittleEndian<ushort>().Should().Be(0x5634);
        data.AsReadOnlySpan(0).AsLittleEndian<short>().Should().Be(0x3412);
        data.AsReadOnlySpan(1).AsLittleEndian<short>().Should().Be(0x5634);
        data.AsReadOnlySpan(0).AsLittleEndian<uint>().Should().Be(0x78563412);
        data.AsReadOnlySpan(1).AsLittleEndian<uint>().Should().Be(0x9A785634);

        data.AsSpan(0).AsLittleEndian<uint>().Should().Be(0x78563412);
        data.AsLittleEndian<uint>().Should().Be(0x78563412);
    }

    [TestMethod()]
    public void AsBigEndian_BinaryInteger()
    {
        var data = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x9A, 0xBC, 0xDE, 0xF0, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, };

        data.AsReadOnlySpan(0).AsBigEndian<byte>().Should().Be(0x12);
        data.AsReadOnlySpan(1).AsBigEndian<byte>().Should().Be(0x34);
        data.AsReadOnlySpan(0).AsBigEndian<ushort>().Should().Be(0x1234);
        data.AsReadOnlySpan(1).AsBigEndian<ushort>().Should().Be(0x3456);
        data.AsReadOnlySpan(0).AsBigEndian<short>().Should().Be(0x1234);
        data.AsReadOnlySpan(1).AsBigEndian<short>().Should().Be(0x3456);
        data.AsReadOnlySpan(0).AsBigEndian<uint>().Should().Be(0x12345678);
        data.AsReadOnlySpan(1).AsBigEndian<uint>().Should().Be(0x3456789A);

        data.AsSpan(0).AsBigEndian<uint>().Should().Be(0x12345678);
        data.AsBigEndian<uint>().Should().Be(0x12345678);
    }

    [TestMethod()]
    public void AsEndian_BinaryInteger()
    {
        var data = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x9A, 0xBC, 0xDE, 0xF0, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, };

        data.AsReadOnlySpan(0).AsEndian<byte>(little: true).Should().Be(0x12);
        data.AsReadOnlySpan(1).AsEndian<byte>(little: true).Should().Be(0x34);
        data.AsReadOnlySpan(0).AsEndian<ushort>(little: true).Should().Be(0x3412);
        data.AsReadOnlySpan(1).AsEndian<ushort>(little: true).Should().Be(0x5634);
        data.AsReadOnlySpan(0).AsEndian<short>(little: true).Should().Be(0x3412);
        data.AsReadOnlySpan(1).AsEndian<short>(little: true).Should().Be(0x5634);
        data.AsReadOnlySpan(0).AsEndian<uint>(little: true).Should().Be(0x78563412);
        data.AsReadOnlySpan(1).AsEndian<uint>(little: true).Should().Be(0x9A785634);

        data.AsReadOnlySpan(0).AsEndian<byte>(little: false).Should().Be(0x12);
        data.AsReadOnlySpan(1).AsEndian<byte>(little: false).Should().Be(0x34);
        data.AsReadOnlySpan(0).AsEndian<ushort>(little: false).Should().Be(0x1234);
        data.AsReadOnlySpan(1).AsEndian<ushort>(little: false).Should().Be(0x3456);
        data.AsReadOnlySpan(0).AsEndian<short>(little: false).Should().Be(0x1234);
        data.AsReadOnlySpan(1).AsEndian<short>(little: false).Should().Be(0x3456);
        data.AsReadOnlySpan(0).AsEndian<uint>(little: false).Should().Be(0x12345678);
        data.AsReadOnlySpan(1).AsEndian<uint>(little: false).Should().Be(0x3456789A);

        data.AsSpan(0).AsEndian<uint>(little: true).Should().Be(0x78563412);
        data.AsEndian<uint>(little: true).Should().Be(0x78563412);
    }

    [TestMethod()]
    public void WriteByLittleEndian()
    {
        var buffer = new byte[32];

        buffer.AsSpan().WriteByLittleEndian<byte>(0x12).WriteByLittleEndian<byte>(0x34);
        buffer.AsSpan(0, 2).ToArray().Should().Equal(0x12, 0x34);

        buffer.AsSpan().WriteByLittleEndian<ushort>(0x1234).WriteByLittleEndian<ushort>(0x5678);
        buffer.AsSpan(0, 4).ToArray().Should().Equal(0x34, 0x12, 0x78, 0x56);
    }

    [TestMethod()]
    public void WriteByBigEndian()
    {
        var buffer = new byte[32];

        buffer.AsSpan().WriteByBigEndian<byte>(0x12).WriteByBigEndian<byte>(0x34);
        buffer.AsSpan(0, 2).ToArray().Should().Equal(0x12, 0x34);

        buffer.AsSpan().WriteByBigEndian<ushort>(0x1234).WriteByBigEndian<ushort>(0x5678);
        buffer.AsSpan(0, 4).ToArray().Should().Equal(0x12, 0x34, 0x56, 0x78);
    }

    [TestMethod()]
    public void WriteByEndian()
    {
        var buffer = new byte[32];

        buffer.AsSpan().WriteByEndian<ushort>(little: true, 0x1234).WriteByEndian<ushort>(little: false, 0x5678);
        buffer.AsSpan(0, 4).ToArray().Should().Equal(0x34, 0x12, 0x56, 0x78);
    }

    [TestMethod()]
    public void CopyFrom()
    {
        var buffer = new int[8];
        buffer.FillBy(0xC5);

        var span = buffer.AsSpan();
        span.CopyFrom([0x12, 0x34, 0x56]);

        buffer.Should().Equal([0x12, 0x34, 0x56, 0xC5, 0xC5, 0xC5, 0xC5, 0xC5]);
    }

    [TestMethod()]
    public void CopyAdvanceFrom()
    {
        var buffer = new int[8];
        buffer.FillBy(0xC5);

        var span = buffer.AsSpan();
        span.CopyAdvanceFrom([0x12, 0x34, 0x56]).CopyAdvanceFrom([0x23, 0x45]);

        buffer.Should().Equal([0x12, 0x34, 0x56, 0x23, 0x45, 0xC5, 0xC5, 0xC5]);
    }

    [TestMethod()]
    public void Cap()
    {
        var source = "abcdef";

        source.AsSpan().Cap(10).ToString().Should().Be("abcdef");
        source.AsSpan().Cap(5).ToString().Should().Be("abcde");
        source.AsSpan().Cap(1).ToString().Should().Be("a");
        source.AsSpan().Cap(0).ToString().Should().BeEmpty();
        source.AsSpan().Cap(-1).ToString().Should().BeEmpty();
    }

#if NET10_0_OR_GREATER
    [TestMethod()]
    public void CountPattern()
    {
        var data = new byte[]
        {
            0x12, 0x34, 0x56, 0x78, 0x9A, 0xBC, 0xDE, 0xF0, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88,
            0x12, 0x34, 0x56, 0x78, 0x9A, 0xBC, 0xDE, 0xF0, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88,
            0x12, 0x34, 0x56, 0x78, 0x9A, 0xBC, 0xDE, 0xF0, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88,
        };

        data.AsSpan().CountPattern<byte>([0x12, 0x34, 0x56]).Should().Be(3);
        data.AsSpan().CountPattern<byte>([0x34, 0x56, 0x78]).Should().Be(3);
        data.AsSpan().CountPattern<byte>([0x66, 0x77, 0x88]).Should().Be(3);
        data.AsSpan().CountPattern<byte>([0x12, 0x34, 0x56, 0x78, 0x9A, 0xBC, 0xDE, 0xF0, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88]).Should().Be(3);
        data.AsSpan().CountPattern<byte>([0x12, 0x34, 0x56, 0x78, 0x9A, 0xBC, 0xDE, 0xF0, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x12]).Should().Be(1);
    }
#endif

    [TestMethod()]
    public void TrimStartPattern()
    {
        var source = new[]
        {
            "abc",
            "abcd",
            "bcd",
            "bcde",
            "cde",
            "cdef",
        };

        {
            source.TrimStartPattern(@"^a").ToArray().Should().Equal([
                "bcd",
                "bcde",
                "cde",
                "cdef",
            ]);
            source.TrimStartPattern(@"^b").ToArray().Should().Equal([
                "abc",
                "abcd",
                "bcd",
                "bcde",
                "cde",
                "cdef",
            ]);
        }
    }

    [TestMethod()]
    public void TrimEndPattern()
    {
        var source = new[]
        {
            "abc",
            "abcd",
            "bcd",
            "bcde",
            "cde",
            "cdef",
        };

        {
            source.TrimEndPattern(@"^c").ToArray().Should().Equal([
                "abc",
                "abcd",
                "bcd",
                "bcde",
            ]);
            source.TrimEndPattern(@"^b").ToArray().Should().Equal([
                "abc",
                "abcd",
                "bcd",
                "bcde",
                "cde",
                "cdef",
            ]);
        }
    }

}
