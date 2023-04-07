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

    [TestMethod()]
    public void ToBinaryString_Value()
    {
        ((sbyte)1).ToBinaryString("0b_", 8).Should().Be("0b_00000001");
        ((short)1).ToBinaryString("0b_", 8).Should().Be("0b_00000000_00000001");
        ((int)1).ToBinaryString("0b_", 8).Should().Be("0b_00000000_00000000_00000000_00000001");
        ((long)1).ToBinaryString("0b_", 8).Should().Be("0b_00000000_00000000_00000000_00000000_00000000_00000000_00000000_00000001");
        ((byte)1).ToBinaryString("0b_", 8).Should().Be("0b_00000001");
        ((ushort)1).ToBinaryString("0b_", 8).Should().Be("0b_00000000_00000001");
        ((uint)1).ToBinaryString("0b_", 8).Should().Be("0b_00000000_00000000_00000000_00000001");
        ((ulong)1).ToBinaryString("0b_", 8).Should().Be("0b_00000000_00000000_00000000_00000000_00000000_00000000_00000000_00000001");

        ((sbyte)5).ToBinaryString("0b_", 8).Should().Be("0b_00000101");
        ((short)5).ToBinaryString("0b_", 8).Should().Be("0b_00000000_00000101");
        ((int)5).ToBinaryString("0b_", 8).Should().Be("0b_00000000_00000000_00000000_00000101");
        ((long)5).ToBinaryString("0b_", 8).Should().Be("0b_00000000_00000000_00000000_00000000_00000000_00000000_00000000_00000101");
        ((byte)5).ToBinaryString("0b_", 8).Should().Be("0b_00000101");
        ((ushort)5).ToBinaryString("0b_", 8).Should().Be("0b_00000000_00000101");
        ((uint)5).ToBinaryString("0b_", 8).Should().Be("0b_00000000_00000000_00000000_00000101");
        ((ulong)5).ToBinaryString("0b_", 8).Should().Be("0b_00000000_00000000_00000000_00000000_00000000_00000000_00000000_00000101");

        ((sbyte)-1).ToBinaryString("0b_", 8).Should().Be("0b_11111111");
        ((short)-1).ToBinaryString("0b_", 8).Should().Be("0b_11111111_11111111");
        ((int)-1).ToBinaryString("0b_", 8).Should().Be("0b_11111111_11111111_11111111_11111111");
        ((long)-1).ToBinaryString("0b_", 8).Should().Be("0b_11111111_11111111_11111111_11111111_11111111_11111111_11111111_11111111");
        unchecked((byte)-1).ToBinaryString("0b_", 8).Should().Be("0b_11111111");
        unchecked((ushort)-1).ToBinaryString("0b_", 8).Should().Be("0b_11111111_11111111");
        unchecked((uint)-1).ToBinaryString("0b_", 8).Should().Be("0b_11111111_11111111_11111111_11111111");
        unchecked((ulong)-1).ToBinaryString("0b_", 8).Should().Be("0b_11111111_11111111_11111111_11111111_11111111_11111111_11111111_11111111");

        unchecked((sbyte)0x80).ToBinaryString("0b_", 8).Should().Be("0b_10000000");
        unchecked((short)0x8000).ToBinaryString("0b_", 8).Should().Be("0b_10000000_00000000");
        unchecked((int)0x80000000).ToBinaryString("0b_", 8).Should().Be("0b_10000000_00000000_00000000_00000000");
        unchecked((long)0x8000000000000000).ToBinaryString("0b_", 8).Should().Be("0b_10000000_00000000_00000000_00000000_00000000_00000000_00000000_00000000");
        unchecked((byte)0x80).ToBinaryString("0b_", 8).Should().Be("0b_10000000");
        unchecked((ushort)0x8000).ToBinaryString("0b_", 8).Should().Be("0b_10000000_00000000");
        unchecked((uint)0x80000000).ToBinaryString("0b_", 8).Should().Be("0b_10000000_00000000_00000000_00000000");
        unchecked((ulong)0x8000000000000000).ToBinaryString("0b_", 8).Should().Be("0b_10000000_00000000_00000000_00000000_00000000_00000000_00000000_00000000");
    }

    [TestMethod()]
    public void ToBinaryString_Separate()
    {
        ((sbyte)0).ToBinaryString("0b_", 8).Should().Be("0b_00000000");
        ((short)0).ToBinaryString("0b_", 8).Should().Be("0b_00000000_00000000");
        ((int)0).ToBinaryString("0b_", 8).Should().Be("0b_00000000_00000000_00000000_00000000");
        ((long)0).ToBinaryString("0b_", 8).Should().Be("0b_00000000_00000000_00000000_00000000_00000000_00000000_00000000_00000000");
        ((byte)0).ToBinaryString("0b_", 8).Should().Be("0b_00000000");
        ((ushort)0).ToBinaryString("0b_", 8).Should().Be("0b_00000000_00000000");
        ((uint)0).ToBinaryString("0b_", 8).Should().Be("0b_00000000_00000000_00000000_00000000");
        ((ulong)0).ToBinaryString("0b_", 8).Should().Be("0b_00000000_00000000_00000000_00000000_00000000_00000000_00000000_00000000");

        ((sbyte)0).ToBinaryString("0b_", 32).Should().Be("0b_00000000");
        ((short)0).ToBinaryString("0b_", 32).Should().Be("0b_0000000000000000");
        ((int)0).ToBinaryString("0b_", 32).Should().Be("0b_00000000000000000000000000000000");
        ((long)0).ToBinaryString("0b_", 32).Should().Be("0b_00000000000000000000000000000000_00000000000000000000000000000000");
        ((byte)0).ToBinaryString("0b_", 32).Should().Be("0b_00000000");
        ((ushort)0).ToBinaryString("0b_", 32).Should().Be("0b_0000000000000000");
        ((uint)0).ToBinaryString("0b_", 32).Should().Be("0b_00000000000000000000000000000000");
        ((ulong)0).ToBinaryString("0b_", 32).Should().Be("0b_00000000000000000000000000000000_00000000000000000000000000000000");

        ((sbyte)0).ToBinaryString("0b_", 1).Should().Be("0b_0_0_0_0_0_0_0_0");
        ((short)0).ToBinaryString("0b_", 1).Should().Be("0b_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0");
        ((int)0).ToBinaryString("0b_", 1).Should().Be("0b_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0");
        ((long)0).ToBinaryString("0b_", 1).Should().Be("0b_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0");
        ((byte)0).ToBinaryString("0b_", 1).Should().Be("0b_0_0_0_0_0_0_0_0");
        ((ushort)0).ToBinaryString("0b_", 1).Should().Be("0b_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0");
        ((uint)0).ToBinaryString("0b_", 1).Should().Be("0b_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0");
        ((ulong)0).ToBinaryString("0b_", 1).Should().Be("0b_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0");

        ((sbyte)0).ToBinaryString("0b_", 0).Should().Be("0b_00000000");
        ((short)0).ToBinaryString("0b_", 0).Should().Be("0b_0000000000000000");
        ((int)0).ToBinaryString("0b_", 0).Should().Be("0b_00000000000000000000000000000000");
        ((long)0).ToBinaryString("0b_", 0).Should().Be("0b_0000000000000000000000000000000000000000000000000000000000000000");
        ((byte)0).ToBinaryString("0b_", 0).Should().Be("0b_00000000");
        ((ushort)0).ToBinaryString("0b_", 0).Should().Be("0b_0000000000000000");
        ((uint)0).ToBinaryString("0b_", 0).Should().Be("0b_00000000000000000000000000000000");
        ((ulong)0).ToBinaryString("0b_", 0).Should().Be("0b_0000000000000000000000000000000000000000000000000000000000000000");
    }

    [TestMethod()]
    public void ToBinaryString_Prefix()
    {
        ((byte)0).ToBinaryString("asd", 0).Should().Be("asd00000000");
        ((byte)0).ToBinaryString("", 0).Should().Be("00000000");
        ((byte)0).ToBinaryString(null!, 0).Should().Be("00000000");
    }

    [TestMethod()]
    public void ToHexString_Value()
    {
        ((sbyte)1).ToHexString("0x", 8).Should().Be("0x01");
        ((short)1).ToHexString("0x", 8).Should().Be("0x0001");
        ((int)1).ToHexString("0x", 8).Should().Be("0x00000001");
        ((long)1).ToHexString("0x", 8).Should().Be("0x00000000_00000001");
        ((byte)1).ToHexString("0x", 8).Should().Be("0x01");
        ((ushort)1).ToHexString("0x", 8).Should().Be("0x0001");
        ((uint)1).ToHexString("0x", 8).Should().Be("0x00000001");
        ((ulong)1).ToHexString("0x", 8).Should().Be("0x00000000_00000001");

        ((sbyte)46).ToHexString("0x", 8).Should().Be("0x2E");
        ((short)46).ToHexString("0x", 8).Should().Be("0x002E");
        ((int)46).ToHexString("0x", 8).Should().Be("0x0000002E");
        ((long)46).ToHexString("0x", 8).Should().Be("0x00000000_0000002E");
        ((byte)46).ToHexString("0x", 8).Should().Be("0x2E");
        ((ushort)46).ToHexString("0x", 8).Should().Be("0x002E");
        ((uint)46).ToHexString("0x", 8).Should().Be("0x0000002E");
        ((ulong)46).ToHexString("0x", 8).Should().Be("0x00000000_0000002E");

        ((sbyte)-1).ToHexString("0x", 8).Should().Be("0xFF");
        ((short)-1).ToHexString("0x", 8).Should().Be("0xFFFF");
        ((int)-1).ToHexString("0x", 8).Should().Be("0xFFFFFFFF");
        ((long)-1).ToHexString("0x", 8).Should().Be("0xFFFFFFFF_FFFFFFFF");
        unchecked((byte)-1).ToHexString("0x", 8).Should().Be("0xFF");
        unchecked((ushort)-1).ToHexString("0x", 8).Should().Be("0xFFFF");
        unchecked((uint)-1).ToHexString("0x", 8).Should().Be("0xFFFFFFFF");
        unchecked((ulong)-1).ToHexString("0x", 8).Should().Be("0xFFFFFFFF_FFFFFFFF");

        unchecked((sbyte)0x80).ToHexString("0x", 8).Should().Be("0x80");
        unchecked((short)0x8000).ToHexString("0x", 8).Should().Be("0x8000");
        unchecked((int)0x80000000).ToHexString("0x", 8).Should().Be("0x80000000");
        unchecked((long)0x8000000000000000).ToHexString("0x", 8).Should().Be("0x80000000_00000000");
        unchecked((byte)0x80).ToHexString("0x", 8).Should().Be("0x80");
        unchecked((ushort)0x8000).ToHexString("0x", 8).Should().Be("0x8000");
        unchecked((uint)0x80000000).ToHexString("0x", 8).Should().Be("0x80000000");
        unchecked((ulong)0x8000000000000000).ToHexString("0x", 8).Should().Be("0x80000000_00000000");
    }

    [TestMethod()]
    public void ToHexString_Separate()
    {
        ((sbyte)0).ToHexString("0x", 8).Should().Be("0x00");
        ((short)0).ToHexString("0x", 8).Should().Be("0x0000");
        ((int)0).ToHexString("0x", 8).Should().Be("0x00000000");
        ((long)0).ToHexString("0x", 8).Should().Be("0x00000000_00000000");
        ((byte)0).ToHexString("0x", 8).Should().Be("0x00");
        ((ushort)0).ToHexString("0x", 8).Should().Be("0x0000");
        ((uint)0).ToHexString("0x", 8).Should().Be("0x00000000");
        ((ulong)0).ToHexString("0x", 8).Should().Be("0x00000000_00000000");

        ((sbyte)0).ToHexString("0x", 3).Should().Be("0x00");
        ((short)0).ToHexString("0x", 3).Should().Be("0x0_000");
        ((int)0).ToHexString("0x", 3).Should().Be("0x00_000_000");
        ((long)0).ToHexString("0x", 3).Should().Be("0x0_000_000_000_000_000");
        ((byte)0).ToHexString("0x", 3).Should().Be("0x00");
        ((ushort)0).ToHexString("0x", 3).Should().Be("0x0_000");
        ((uint)0).ToHexString("0x", 3).Should().Be("0x00_000_000");
        ((ulong)0).ToHexString("0x", 3).Should().Be("0x0_000_000_000_000_000");

        ((sbyte)0).ToHexString("0x", 1).Should().Be("0x0_0");
        ((short)0).ToHexString("0x", 1).Should().Be("0x0_0_0_0");
        ((int)0).ToHexString("0x", 1).Should().Be("0x0_0_0_0_0_0_0_0");
        ((long)0).ToHexString("0x", 1).Should().Be("0x0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0");
        ((byte)0).ToHexString("0x", 1).Should().Be("0x0_0");
        ((ushort)0).ToHexString("0x", 1).Should().Be("0x0_0_0_0");
        ((uint)0).ToHexString("0x", 1).Should().Be("0x0_0_0_0_0_0_0_0");
        ((ulong)0).ToHexString("0x", 1).Should().Be("0x0_0_0_0_0_0_0_0_0_0_0_0_0_0_0_0");

        ((sbyte)0).ToHexString("0x", 0).Should().Be("0x00");
        ((short)0).ToHexString("0x", 0).Should().Be("0x0000");
        ((int)0).ToHexString("0x", 0).Should().Be("0x00000000");
        ((long)0).ToHexString("0x", 0).Should().Be("0x0000000000000000");
        ((byte)0).ToHexString("0x", 0).Should().Be("0x00");
        ((ushort)0).ToHexString("0x", 0).Should().Be("0x0000");
        ((uint)0).ToHexString("0x", 0).Should().Be("0x00000000");
        ((ulong)0).ToHexString("0x", 0).Should().Be("0x0000000000000000");
    }

    [TestMethod()]
    public void ToHexString_Prefix()
    {
        ((byte)0).ToHexString("asd", 0).Should().Be("asd00");
        ((byte)0).ToHexString("", 0).Should().Be("00");
        ((byte)0).ToHexString(null!, 0).Should().Be("00");
    }
}
