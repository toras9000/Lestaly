using System.Globalization;
using FluentAssertions;

namespace LestalyTest.Extensions;

[TestClass()]
public class StringParseExtensionsTests
{
    #region Parse
    [TestMethod()]
    public void ParseUInt8()
    {
        // string
        "0".ParseUInt8().Should().Be(0);
        "100".ParseUInt8().Should().Be(100);
        "255".ParseUInt8().Should().Be(255);
        new Action(() => "256".ParseUInt8()).Should().Throw<Exception>();

        "0".ParseUInt8(NumberStyles.HexNumber).Should().Be(0);
        "80".ParseUInt8(NumberStyles.HexNumber).Should().Be(0x80);
        "FF".ParseUInt8(NumberStyles.HexNumber).Should().Be(0xFF);
        new Action(() => "100".ParseUInt8(NumberStyles.HexNumber)).Should().Throw<Exception>();

        // ReadOnlySpan
        "0".AsSpan().ParseUInt8().Should().Be(0);
        "100".AsSpan().ParseUInt8().Should().Be(100);
        "255".AsSpan().ParseUInt8().Should().Be(255);
        new Action(() => "256".AsSpan().ParseUInt8()).Should().Throw<Exception>();

        "0".AsSpan().ParseUInt8(NumberStyles.HexNumber).Should().Be(0);
        "80".AsSpan().ParseUInt8(NumberStyles.HexNumber).Should().Be(0x80);
        "FF".AsSpan().ParseUInt8(NumberStyles.HexNumber).Should().Be(0xFF);
        new Action(() => "100".AsSpan().ParseUInt8(NumberStyles.HexNumber)).Should().Throw<Exception>();

        // Span
        "0".ToArray().AsSpan().ParseUInt8().Should().Be(0);
        "100".ToArray().AsSpan().ParseUInt8().Should().Be(100);
        "255".ToArray().AsSpan().ParseUInt8().Should().Be(255);
        new Action(() => "256".ToArray().AsSpan().ParseUInt8()).Should().Throw<Exception>();

        "0".ToArray().AsSpan().ParseUInt8(NumberStyles.HexNumber).Should().Be(0);
        "80".ToArray().AsSpan().ParseUInt8(NumberStyles.HexNumber).Should().Be(0x80);
        "FF".ToArray().AsSpan().ParseUInt8(NumberStyles.HexNumber).Should().Be(0xFF);
        new Action(() => "100".ToArray().AsSpan().ParseUInt8(NumberStyles.HexNumber)).Should().Throw<Exception>();
    }

    [TestMethod()]
    public void ParseUInt16()
    {
        // string
        "0".ParseUInt16().Should().Be(0);
        "100".ParseUInt16().Should().Be(100);
        "65535".ParseUInt16().Should().Be(65535);
        new Action(() => "65536".ParseUInt16()).Should().Throw<Exception>();

        "0".ParseUInt16(NumberStyles.HexNumber).Should().Be(0);
        "8000".ParseUInt16(NumberStyles.HexNumber).Should().Be(0x8000);
        "FFFF".ParseUInt16(NumberStyles.HexNumber).Should().Be(0xFFFF);
        new Action(() => "10000".ParseUInt16(NumberStyles.HexNumber)).Should().Throw<Exception>();

        // ReadOnlySpan
        "0".AsSpan().ParseUInt16().Should().Be(0);
        "100".AsSpan().ParseUInt16().Should().Be(100);
        "65535".AsSpan().ParseUInt16().Should().Be(65535);
        new Action(() => "65536".AsSpan().ParseUInt16()).Should().Throw<Exception>();

        "0".AsSpan().ParseUInt16(NumberStyles.HexNumber).Should().Be(0);
        "8000".AsSpan().ParseUInt16(NumberStyles.HexNumber).Should().Be(0x8000);
        "FFFF".AsSpan().ParseUInt16(NumberStyles.HexNumber).Should().Be(0xFFFF);
        new Action(() => "10000".AsSpan().ParseUInt16(NumberStyles.HexNumber)).Should().Throw<Exception>();

        // Span
        "0".ToArray().AsSpan().ParseUInt16().Should().Be(0);
        "100".ToArray().AsSpan().ParseUInt16().Should().Be(100);
        "65535".ToArray().AsSpan().ParseUInt16().Should().Be(65535);
        new Action(() => "65536".ToArray().AsSpan().ParseUInt16()).Should().Throw<Exception>();

        "0".ToArray().AsSpan().ParseUInt16(NumberStyles.HexNumber).Should().Be(0);
        "8000".ToArray().AsSpan().ParseUInt16(NumberStyles.HexNumber).Should().Be(0x8000);
        "FFFF".ToArray().AsSpan().ParseUInt16(NumberStyles.HexNumber).Should().Be(0xFFFF);
        new Action(() => "10000".ToArray().AsSpan().ParseUInt16(NumberStyles.HexNumber)).Should().Throw<Exception>();
    }

    [TestMethod()]
    public void ParseUInt32()
    {
        // string
        "0".ParseUInt32().Should().Be(0);
        "1000000000".ParseUInt32().Should().Be(1000000000);
        "4294967295".ParseUInt32().Should().Be(4294967295);
        new Action(() => "4294967296".ParseUInt32()).Should().Throw<Exception>();

        "0".ParseUInt32(NumberStyles.HexNumber).Should().Be(0);
        "80000000".ParseUInt32(NumberStyles.HexNumber).Should().Be(0x80000000);
        "FFFFFFFF".ParseUInt32(NumberStyles.HexNumber).Should().Be(0xFFFFFFFF);
        new Action(() => "100000000".ParseUInt32(NumberStyles.HexNumber)).Should().Throw<Exception>();

        // ReadOnlySpan
        "0".AsSpan().ParseUInt32().Should().Be(0);
        "1000000000".AsSpan().ParseUInt32().Should().Be(1000000000);
        "4294967295".AsSpan().ParseUInt32().Should().Be(4294967295);
        new Action(() => "4294967296".AsSpan().ParseUInt32()).Should().Throw<Exception>();

        "0".AsSpan().ParseUInt32(NumberStyles.HexNumber).Should().Be(0);
        "80000000".AsSpan().ParseUInt32(NumberStyles.HexNumber).Should().Be(0x80000000);
        "FFFFFFFF".AsSpan().ParseUInt32(NumberStyles.HexNumber).Should().Be(0xFFFFFFFF);
        new Action(() => "100000000".AsSpan().ParseUInt32(NumberStyles.HexNumber)).Should().Throw<Exception>();

        // Span
        "0".ToArray().AsSpan().ParseUInt32().Should().Be(0);
        "1000000000".ToArray().AsSpan().ParseUInt32().Should().Be(1000000000);
        "4294967295".ToArray().AsSpan().ParseUInt32().Should().Be(4294967295);
        new Action(() => "4294967296".ToArray().AsSpan().ParseUInt32()).Should().Throw<Exception>();

        "0".ToArray().AsSpan().ParseUInt32(NumberStyles.HexNumber).Should().Be(0);
        "80000000".ToArray().AsSpan().ParseUInt32(NumberStyles.HexNumber).Should().Be(0x80000000);
        "FFFFFFFF".ToArray().AsSpan().ParseUInt32(NumberStyles.HexNumber).Should().Be(0xFFFFFFFF);
        new Action(() => "100000000".ToArray().AsSpan().ParseUInt32(NumberStyles.HexNumber)).Should().Throw<Exception>();
    }

    [TestMethod()]
    public void ParseUInt64()
    {
        // string
        "0".ParseUInt64().Should().Be(0);
        "10000000000000000000".ParseUInt64().Should().Be(10000000000000000000);
        "18446744073709551615".ParseUInt64().Should().Be(18446744073709551615);
        new Action(() => "18446744073709551616".ParseUInt64()).Should().Throw<Exception>();

        "0".ParseUInt64(NumberStyles.HexNumber).Should().Be(0);
        "8000000000000000".ParseUInt64(NumberStyles.HexNumber).Should().Be(0x8000000000000000);
        "FFFFFFFFFFFFFFFF".ParseUInt64(NumberStyles.HexNumber).Should().Be(0xFFFFFFFFFFFFFFFF);
        new Action(() => "10000000000000000".ParseUInt64(NumberStyles.HexNumber)).Should().Throw<Exception>();

        // ReadOnlySpan
        "0".AsSpan().ParseUInt64().Should().Be(0);
        "10000000000000000000".AsSpan().ParseUInt64().Should().Be(10000000000000000000);
        "18446744073709551615".AsSpan().ParseUInt64().Should().Be(18446744073709551615);
        new Action(() => "18446744073709551616".AsSpan().ParseUInt64()).Should().Throw<Exception>();

        "0".AsSpan().ParseUInt64(NumberStyles.HexNumber).Should().Be(0);
        "8000000000000000".AsSpan().ParseUInt64(NumberStyles.HexNumber).Should().Be(0x8000000000000000);
        "FFFFFFFFFFFFFFFF".AsSpan().ParseUInt64(NumberStyles.HexNumber).Should().Be(0xFFFFFFFFFFFFFFFF);
        new Action(() => "10000000000000000".AsSpan().ParseUInt64(NumberStyles.HexNumber)).Should().Throw<Exception>();

        // Span
        "0".ToArray().AsSpan().ParseUInt64().Should().Be(0);
        "10000000000000000000".ToArray().AsSpan().ParseUInt64().Should().Be(10000000000000000000);
        "18446744073709551615".ToArray().AsSpan().ParseUInt64().Should().Be(18446744073709551615);
        new Action(() => "18446744073709551616".ToArray().AsSpan().ParseUInt64()).Should().Throw<Exception>();

        "0".ToArray().AsSpan().ParseUInt64(NumberStyles.HexNumber).Should().Be(0);
        "8000000000000000".ToArray().AsSpan().ParseUInt64(NumberStyles.HexNumber).Should().Be(0x8000000000000000);
        "FFFFFFFFFFFFFFFF".ToArray().AsSpan().ParseUInt64(NumberStyles.HexNumber).Should().Be(0xFFFFFFFFFFFFFFFF);
        new Action(() => "10000000000000000".ToArray().AsSpan().ParseUInt64(NumberStyles.HexNumber)).Should().Throw<Exception>();
    }

    [TestMethod()]
    public void ParseInt8()
    {
        // string
        "0".ParseInt8().Should().Be(0);
        "100".ParseInt8().Should().Be(100);
        "127".ParseInt8().Should().Be(127);
        "-128".ParseInt8().Should().Be(-128);
        new Action(() => "128".ParseInt8()).Should().Throw<Exception>();

        "0".ParseInt8(NumberStyles.HexNumber).Should().Be(0);
        "7F".ParseInt8(NumberStyles.HexNumber).Should().Be(0x7F);
        "80".ParseInt8(NumberStyles.HexNumber).Should().Be(-128);
        new Action(() => "100".ParseInt8(NumberStyles.HexNumber)).Should().Throw<Exception>();

        // ReadOnlySpan
        "0".AsSpan().ParseInt8().Should().Be(0);
        "100".AsSpan().ParseInt8().Should().Be(100);
        "127".AsSpan().ParseInt8().Should().Be(127);
        "-128".AsSpan().ParseInt8().Should().Be(-128);
        new Action(() => "128".AsSpan().ParseInt8()).Should().Throw<Exception>();

        "0".AsSpan().ParseInt8(NumberStyles.HexNumber).Should().Be(0);
        "7F".AsSpan().ParseInt8(NumberStyles.HexNumber).Should().Be(0x7F);
        "80".AsSpan().ParseInt8(NumberStyles.HexNumber).Should().Be(-128);
        new Action(() => "100".AsSpan().ParseInt8(NumberStyles.HexNumber)).Should().Throw<Exception>();

        // Span
        "0".ToArray().AsSpan().ParseInt8().Should().Be(0);
        "100".ToArray().AsSpan().ParseInt8().Should().Be(100);
        "127".ToArray().AsSpan().ParseInt8().Should().Be(127);
        "-128".ToArray().AsSpan().ParseInt8().Should().Be(-128);
        new Action(() => "128".ToArray().AsSpan().ParseInt8()).Should().Throw<Exception>();

        "0".ToArray().AsSpan().ParseInt8(NumberStyles.HexNumber).Should().Be(0);
        "7F".ToArray().AsSpan().ParseInt8(NumberStyles.HexNumber).Should().Be(0x7F);
        "80".ToArray().AsSpan().ParseInt8(NumberStyles.HexNumber).Should().Be(-128);
        new Action(() => "100".ToArray().AsSpan().ParseInt8(NumberStyles.HexNumber)).Should().Throw<Exception>();
    }

    [TestMethod()]
    public void ParseInt16()
    {
        // string
        "0".ParseInt16().Should().Be(0);
        "10000".ParseInt16().Should().Be(10000);
        "32767".ParseInt16().Should().Be(32767);
        new Action(() => "32768".ParseInt16()).Should().Throw<Exception>();

        "0".ParseInt16(NumberStyles.HexNumber).Should().Be(0);
        "7FFF".ParseInt16(NumberStyles.HexNumber).Should().Be(0x7FFF);
        "8000".ParseInt16(NumberStyles.HexNumber).Should().Be(-32768);
        new Action(() => "10000".ParseInt16(NumberStyles.HexNumber)).Should().Throw<Exception>();

        // ReadOnlySpan
        "0".AsSpan().ParseInt16().Should().Be(0);
        "10000".AsSpan().ParseInt16().Should().Be(10000);
        "32767".AsSpan().ParseInt16().Should().Be(32767);
        new Action(() => "32768".AsSpan().ParseInt16()).Should().Throw<Exception>();

        "0".AsSpan().ParseInt16(NumberStyles.HexNumber).Should().Be(0);
        "7FFF".AsSpan().ParseInt16(NumberStyles.HexNumber).Should().Be(0x7FFF);
        "8000".AsSpan().ParseInt16(NumberStyles.HexNumber).Should().Be(-32768);
        new Action(() => "10000".AsSpan().ParseInt16(NumberStyles.HexNumber)).Should().Throw<Exception>();

        // Span
        "0".ToArray().AsSpan().ParseInt16().Should().Be(0);
        "10000".ToArray().AsSpan().ParseInt16().Should().Be(10000);
        "32767".ToArray().AsSpan().ParseInt16().Should().Be(32767);
        new Action(() => "32768".ToArray().AsSpan().ParseInt16()).Should().Throw<Exception>();

        "0".ToArray().AsSpan().ParseInt16(NumberStyles.HexNumber).Should().Be(0);
        "7FFF".ToArray().AsSpan().ParseInt16(NumberStyles.HexNumber).Should().Be(0x7FFF);
        "8000".ToArray().AsSpan().ParseInt16(NumberStyles.HexNumber).Should().Be(-32768);
        new Action(() => "10000".ToArray().AsSpan().ParseInt16(NumberStyles.HexNumber)).Should().Throw<Exception>();
    }

    [TestMethod()]
    public void ParseInt32()
    {
        // string
        "0".ParseInt32().Should().Be(0);
        "1000000000".ParseInt32().Should().Be(1000000000);
        "2147483647".ParseInt32().Should().Be(2147483647);
        new Action(() => "2147483648".ParseInt32()).Should().Throw<Exception>();

        "0".ParseInt32(NumberStyles.HexNumber).Should().Be(0);
        "7FFFFFFF".ParseInt32(NumberStyles.HexNumber).Should().Be(0x7FFFFFFF);
        "80000000".ParseInt32(NumberStyles.HexNumber).Should().Be(-2147483648);
        new Action(() => "100000000".ParseInt32(NumberStyles.HexNumber)).Should().Throw<Exception>();

        // ReadOnlySpan
        "0".AsSpan().ParseInt32().Should().Be(0);
        "1000000000".AsSpan().ParseInt32().Should().Be(1000000000);
        "2147483647".AsSpan().ParseInt32().Should().Be(2147483647);
        new Action(() => "2147483648".AsSpan().ParseInt32()).Should().Throw<Exception>();

        "0".AsSpan().ParseInt32(NumberStyles.HexNumber).Should().Be(0);
        "7FFFFFFF".AsSpan().ParseInt32(NumberStyles.HexNumber).Should().Be(0x7FFFFFFF);
        "80000000".AsSpan().ParseInt32(NumberStyles.HexNumber).Should().Be(-2147483648);
        new Action(() => "100000000".AsSpan().ParseInt32(NumberStyles.HexNumber)).Should().Throw<Exception>();

        // Span
        "0".ToArray().AsSpan().ParseInt32().Should().Be(0);
        "1000000000".ToArray().AsSpan().ParseInt32().Should().Be(1000000000);
        "2147483647".ToArray().AsSpan().ParseInt32().Should().Be(2147483647);
        new Action(() => "2147483648".ToArray().AsSpan().ParseInt32()).Should().Throw<Exception>();

        "0".ToArray().AsSpan().ParseInt32(NumberStyles.HexNumber).Should().Be(0);
        "7FFFFFFF".ToArray().AsSpan().ParseInt32(NumberStyles.HexNumber).Should().Be(0x7FFFFFFF);
        "80000000".ToArray().AsSpan().ParseInt32(NumberStyles.HexNumber).Should().Be(-2147483648);
        new Action(() => "100000000".ToArray().AsSpan().ParseInt32(NumberStyles.HexNumber)).Should().Throw<Exception>();
    }

    [TestMethod()]
    public void ParseInt64()
    {
        // string
        "0".ParseInt64().Should().Be(0);
        "1000000000000000000".ParseInt64().Should().Be(1000000000000000000);
        "9223372036854775807".ParseInt64().Should().Be(9223372036854775807);
        new Action(() => "9223372036854775808".ParseInt64()).Should().Throw<Exception>();

        "0".ParseInt64(NumberStyles.HexNumber).Should().Be(0);
        "7FFFFFFFFFFFFFFF".ParseInt64(NumberStyles.HexNumber).Should().Be(0x7FFFFFFFFFFFFFFF);
        "8000000000000000".ParseInt64(NumberStyles.HexNumber).Should().Be(-9223372036854775808);
        new Action(() => "10000000000000000".ParseInt64(NumberStyles.HexNumber)).Should().Throw<Exception>();

        // ReadOnlySpan
        "0".AsSpan().ParseInt64().Should().Be(0);
        "1000000000000000000".AsSpan().ParseInt64().Should().Be(1000000000000000000);
        "9223372036854775807".AsSpan().ParseInt64().Should().Be(9223372036854775807);
        new Action(() => "9223372036854775808".AsSpan().ParseInt64()).Should().Throw<Exception>();

        "0".AsSpan().ParseInt64(NumberStyles.HexNumber).Should().Be(0);
        "7FFFFFFFFFFFFFFF".AsSpan().ParseInt64(NumberStyles.HexNumber).Should().Be(0x7FFFFFFFFFFFFFFF);
        "8000000000000000".AsSpan().ParseInt64(NumberStyles.HexNumber).Should().Be(-9223372036854775808);
        new Action(() => "10000000000000000".AsSpan().ParseInt64(NumberStyles.HexNumber)).Should().Throw<Exception>();

        // Span
        "0".ToArray().AsSpan().ParseInt64().Should().Be(0);
        "1000000000000000000".ToArray().AsSpan().ParseInt64().Should().Be(1000000000000000000);
        "9223372036854775807".ToArray().AsSpan().ParseInt64().Should().Be(9223372036854775807);
        new Action(() => "9223372036854775808".ToArray().AsSpan().ParseInt64()).Should().Throw<Exception>();

        "0".ToArray().AsSpan().ParseInt64(NumberStyles.HexNumber).Should().Be(0);
        "7FFFFFFFFFFFFFFF".ToArray().AsSpan().ParseInt64(NumberStyles.HexNumber).Should().Be(0x7FFFFFFFFFFFFFFF);
        "8000000000000000".ToArray().AsSpan().ParseInt64(NumberStyles.HexNumber).Should().Be(-9223372036854775808);
        new Action(() => "10000000000000000".ToArray().AsSpan().ParseInt64(NumberStyles.HexNumber)).Should().Throw<Exception>();
    }

    [TestMethod()]
    public void ParseFloat()
    {
        // string
        "0".ParseFloat().Should().Be(0);
        "100".ParseFloat().Should().Be(100f);
        "-100".ParseFloat().Should().Be(-100f);
        new Action(() => "asd".ParseFloat()).Should().Throw<Exception>();

        "100".ParseFloat(NumberStyles.Float).Should().Be(100f);

        // ReadOnlySpan
        "0".AsSpan().ParseFloat().Should().Be(0);
        "100".AsSpan().ParseFloat().Should().Be(100f);
        "-100".AsSpan().ParseFloat().Should().Be(-100f);
        new Action(() => "asd".AsSpan().ParseFloat()).Should().Throw<Exception>();

        "100".AsSpan().ParseFloat(NumberStyles.Float).Should().Be(100f);

        // Span
        "0".ToArray().AsSpan().ParseFloat().Should().Be(0);
        "100".ToArray().AsSpan().ParseFloat().Should().Be(100f);
        "-100".ToArray().AsSpan().ParseFloat().Should().Be(-100f);
        new Action(() => "asd".ToArray().AsSpan().ParseFloat()).Should().Throw<Exception>();

        "100".ToArray().AsSpan().ParseFloat(NumberStyles.Float).Should().Be(100f);
    }

    [TestMethod()]
    public void ParseDouble()
    {
        // string
        "0".ParseDouble().Should().Be(0);
        "100".ParseDouble().Should().Be(100d);
        "-100".ParseDouble().Should().Be(-100d);
        new Action(() => "asd".ParseDouble()).Should().Throw<Exception>();

        "100".ParseDouble(NumberStyles.Float).Should().Be(100d);

        // ReadOnlySpan
        "0".AsSpan().ParseDouble().Should().Be(0);
        "100".AsSpan().ParseDouble().Should().Be(100d);
        "-100".AsSpan().ParseDouble().Should().Be(-100d);
        new Action(() => "asd".AsSpan().ParseDouble()).Should().Throw<Exception>();

        "100".AsSpan().ParseDouble(NumberStyles.Float).Should().Be(100d);

        // Span
        "0".ToArray().AsSpan().ParseDouble().Should().Be(0);
        "100".ToArray().AsSpan().ParseDouble().Should().Be(100d);
        "-100".ToArray().AsSpan().ParseDouble().Should().Be(-100d);
        new Action(() => "asd".ToArray().AsSpan().ParseDouble()).Should().Throw<Exception>();

        "100".ToArray().AsSpan().ParseDouble(NumberStyles.Float).Should().Be(100d);
    }

    [TestMethod()]
    public void ParseDecimal()
    {
        // string
        "0".ParseDecimal().Should().Be(0);
        "100".ParseDecimal().Should().Be(100m);
        "-100".ParseDecimal().Should().Be(-100m);
        new Action(() => "asd".ParseDecimal()).Should().Throw<Exception>();

        "100".ParseDecimal(NumberStyles.Number).Should().Be(100m);

        // ReadOnlySpan
        "0".AsSpan().ParseDecimal().Should().Be(0);
        "100".AsSpan().ParseDecimal().Should().Be(100m);
        "-100".AsSpan().ParseDecimal().Should().Be(-100m);
        new Action(() => "asd".AsSpan().ParseDecimal()).Should().Throw<Exception>();

        "100".AsSpan().ParseDecimal(NumberStyles.Number).Should().Be(100m);

        // Span
        "0".ToArray().AsSpan().ParseDecimal().Should().Be(0);
        "100".ToArray().AsSpan().ParseDecimal().Should().Be(100m);
        "-100".ToArray().AsSpan().ParseDecimal().Should().Be(-100m);
        new Action(() => "asd".ToArray().AsSpan().ParseDecimal()).Should().Throw<Exception>();

        "100".ToArray().AsSpan().ParseDecimal(NumberStyles.Number).Should().Be(100m);
    }

    [TestMethod()]
    public void ParseDateTime()
    {
        // string
        "2022/12/31".ParseDateTime().Should().Be(new DateTime(2022, 12, 31));
        new Action(() => "asd".ParseDateTime()).Should().Throw<Exception>();

        "11:12:13".ParseDateTime(DateTimeStyles.NoCurrentDateDefault).Should().Be(new DateTime(1, 1, 1, 11, 12, 13));

        // ReadOnlySpan
        "2022/12/31".AsSpan().ParseDateTime().Should().Be(new DateTime(2022, 12, 31));
        new Action(() => "asd".AsSpan().ParseDateTime()).Should().Throw<Exception>();

        "11:12:13".AsSpan().ParseDateTime(DateTimeStyles.NoCurrentDateDefault).Should().Be(new DateTime(1, 1, 1, 11, 12, 13));

        // Span
        "2022/12/31".ToArray().AsSpan().ParseDateTime().Should().Be(new DateTime(2022, 12, 31));
        new Action(() => "asd".ToArray().AsSpan().ParseDateTime()).Should().Throw<Exception>();

        "11:12:13".ToArray().AsSpan().ParseDateTime(DateTimeStyles.NoCurrentDateDefault).Should().Be(new DateTime(1, 1, 1, 11, 12, 13));
    }

    [TestMethod()]
    public void ParseDateTimeExact()
    {
        // string
        "1234_05_06".ParseDateTimeExact("yyyy_M_d").Should().Be(new DateTime(1234, 5, 6));

        new Action(() => "zz".ParseDateTimeExact("yyyy_M_d")).Should().Throw<Exception>();

        // ReadOnlySpan
        "1234_05_06".AsSpan().ParseDateTimeExact("yyyy_M_d").Should().Be(new DateTime(1234, 5, 6));

        new Action(() => "zz".AsSpan().ParseDateTimeExact("yyyy_M_d")).Should().Throw<Exception>();

        // Span
        "1234_05_06".ToArray().AsSpan().ParseDateTimeExact("yyyy_M_d").Should().Be(new DateTime(1234, 5, 6));

        new Action(() => "zz".ToArray().AsSpan().ParseDateTimeExact("yyyy_M_d")).Should().Throw<Exception>();
    }
    #endregion

    #region TryParse
    [TestMethod()]
    public void TryParseUInt8()
    {
        // string
        "0".TryParseUInt8().Should().Be(0);
        "100".TryParseUInt8().Should().Be(100);
        "255".TryParseUInt8().Should().Be(255);
        "256".TryParseUInt8().Should().BeNull();

        "0".TryParseUInt8(NumberStyles.HexNumber).Should().Be(0);
        "80".TryParseUInt8(NumberStyles.HexNumber).Should().Be(0x80);
        "FF".TryParseUInt8(NumberStyles.HexNumber).Should().Be(0xFF);
        "100".TryParseUInt8(NumberStyles.HexNumber).Should().BeNull();

        // ReadOnlySpan
        "0".AsSpan().TryParseUInt8().Should().Be(0);
        "100".AsSpan().TryParseUInt8().Should().Be(100);
        "255".AsSpan().TryParseUInt8().Should().Be(255);
        "256".AsSpan().TryParseUInt8().Should().BeNull();

        "0".AsSpan().TryParseUInt8(NumberStyles.HexNumber).Should().Be(0);
        "80".AsSpan().TryParseUInt8(NumberStyles.HexNumber).Should().Be(0x80);
        "FF".AsSpan().TryParseUInt8(NumberStyles.HexNumber).Should().Be(0xFF);
        "100".AsSpan().TryParseUInt8(NumberStyles.HexNumber).Should().BeNull();

        // Span
        "0".ToArray().AsSpan().TryParseUInt8().Should().Be(0);
        "100".ToArray().AsSpan().TryParseUInt8().Should().Be(100);
        "255".ToArray().AsSpan().TryParseUInt8().Should().Be(255);
        "256".ToArray().AsSpan().TryParseUInt8().Should().BeNull();

        "0".ToArray().AsSpan().TryParseUInt8(NumberStyles.HexNumber).Should().Be(0);
        "80".ToArray().AsSpan().TryParseUInt8(NumberStyles.HexNumber).Should().Be(0x80);
        "FF".ToArray().AsSpan().TryParseUInt8(NumberStyles.HexNumber).Should().Be(0xFF);
        "100".ToArray().AsSpan().TryParseUInt8(NumberStyles.HexNumber).Should().BeNull();

    }

    [TestMethod()]
    public void TryParseUInt16()
    {
        // string
        "0".TryParseUInt16().Should().Be(0);
        "100".TryParseUInt16().Should().Be(100);
        "65535".TryParseUInt16().Should().Be(65535);
        "65536".TryParseUInt16().Should().BeNull();

        "0".TryParseUInt16(NumberStyles.HexNumber).Should().Be(0);
        "8000".ParseUInt16(NumberStyles.HexNumber).Should().Be(0x8000);
        "FFFF".TryParseUInt16(NumberStyles.HexNumber).Should().Be(0xFFFF);
        "10000".TryParseUInt16(NumberStyles.HexNumber).Should().BeNull();

        // ReadOnlySpan
        "0".AsSpan().TryParseUInt16().Should().Be(0);
        "100".AsSpan().TryParseUInt16().Should().Be(100);
        "65535".AsSpan().TryParseUInt16().Should().Be(65535);
        "65536".AsSpan().TryParseUInt16().Should().BeNull();

        "0".AsSpan().TryParseUInt16(NumberStyles.HexNumber).Should().Be(0);
        "8000".AsSpan().TryParseUInt16(NumberStyles.HexNumber).Should().Be(0x8000);
        "FFFF".AsSpan().TryParseUInt16(NumberStyles.HexNumber).Should().Be(0xFFFF);
        "10000".AsSpan().TryParseUInt16(NumberStyles.HexNumber).Should().BeNull();

        // Span
        "0".ToArray().AsSpan().TryParseUInt16().Should().Be(0);
        "100".ToArray().AsSpan().TryParseUInt16().Should().Be(100);
        "65535".ToArray().AsSpan().TryParseUInt16().Should().Be(65535);
        "65536".ToArray().AsSpan().TryParseUInt16().Should().BeNull();

        "0".ToArray().AsSpan().TryParseUInt16(NumberStyles.HexNumber).Should().Be(0);
        "8000".ToArray().AsSpan().TryParseUInt16(NumberStyles.HexNumber).Should().Be(0x8000);
        "FFFF".ToArray().AsSpan().TryParseUInt16(NumberStyles.HexNumber).Should().Be(0xFFFF);
        "10000".ToArray().AsSpan().TryParseUInt16(NumberStyles.HexNumber).Should().BeNull();
    }

    [TestMethod()]
    public void TryParseUInt32()
    {
        // string
        "0".TryParseUInt32().Should().Be(0);
        "1000000000".TryParseUInt32().Should().Be(1000000000);
        "4294967295".TryParseUInt32().Should().Be(4294967295);
        "4294967296".TryParseUInt32().Should().BeNull();

        "0".TryParseUInt32(NumberStyles.HexNumber).Should().Be(0);
        "80000000".TryParseUInt32(NumberStyles.HexNumber).Should().Be(0x80000000);
        "FFFFFFFF".TryParseUInt32(NumberStyles.HexNumber).Should().Be(0xFFFFFFFF);
        "100000000".TryParseUInt32(NumberStyles.HexNumber).Should().BeNull();

        // ReadOnlySpan
        "0".AsSpan().TryParseUInt32().Should().Be(0);
        "1000000000".AsSpan().TryParseUInt32().Should().Be(1000000000);
        "4294967295".AsSpan().TryParseUInt32().Should().Be(4294967295);
        "4294967296".AsSpan().TryParseUInt32().Should().BeNull();

        "0".AsSpan().TryParseUInt32(NumberStyles.HexNumber).Should().Be(0);
        "80000000".AsSpan().TryParseUInt32(NumberStyles.HexNumber).Should().Be(0x80000000);
        "FFFFFFFF".AsSpan().TryParseUInt32(NumberStyles.HexNumber).Should().Be(0xFFFFFFFF);
        "100000000".AsSpan().TryParseUInt32(NumberStyles.HexNumber).Should().BeNull();

        // Span
        "0".ToArray().AsSpan().TryParseUInt32().Should().Be(0);
        "1000000000".ToArray().AsSpan().TryParseUInt32().Should().Be(1000000000);
        "4294967295".ToArray().AsSpan().TryParseUInt32().Should().Be(4294967295);
        "4294967296".ToArray().AsSpan().TryParseUInt32().Should().BeNull();

        "0".ToArray().AsSpan().TryParseUInt32(NumberStyles.HexNumber).Should().Be(0);
        "80000000".ToArray().AsSpan().TryParseUInt32(NumberStyles.HexNumber).Should().Be(0x80000000);
        "FFFFFFFF".ToArray().AsSpan().TryParseUInt32(NumberStyles.HexNumber).Should().Be(0xFFFFFFFF);
        "100000000".ToArray().AsSpan().TryParseUInt32(NumberStyles.HexNumber).Should().BeNull();
    }

    [TestMethod()]
    public void TryParseUInt64()
    {
        // string
        "0".TryParseUInt64().Should().Be(0);
        "10000000000000000000".TryParseUInt64().Should().Be(10000000000000000000);
        "18446744073709551615".TryParseUInt64().Should().Be(18446744073709551615);
        "18446744073709551616".TryParseUInt64().Should().BeNull();

        "0".TryParseUInt64(NumberStyles.HexNumber).Should().Be(0);
        "8000000000000000".TryParseUInt64(NumberStyles.HexNumber).Should().Be(0x8000000000000000);
        "FFFFFFFFFFFFFFFF".TryParseUInt64(NumberStyles.HexNumber).Should().Be(0xFFFFFFFFFFFFFFFF);
        "10000000000000000".TryParseUInt64(NumberStyles.HexNumber).Should().BeNull();

        // ReadOnlySpan
        "0".AsSpan().TryParseUInt64().Should().Be(0);
        "10000000000000000000".AsSpan().TryParseUInt64().Should().Be(10000000000000000000);
        "18446744073709551615".AsSpan().TryParseUInt64().Should().Be(18446744073709551615);
        "18446744073709551616".AsSpan().TryParseUInt64().Should().BeNull();

        "0".AsSpan().TryParseUInt64(NumberStyles.HexNumber).Should().Be(0);
        "8000000000000000".AsSpan().TryParseUInt64(NumberStyles.HexNumber).Should().Be(0x8000000000000000);
        "FFFFFFFFFFFFFFFF".AsSpan().TryParseUInt64(NumberStyles.HexNumber).Should().Be(0xFFFFFFFFFFFFFFFF);
        "10000000000000000".AsSpan().TryParseUInt64(NumberStyles.HexNumber).Should().BeNull();

        // Span
        "0".ToArray().AsSpan().TryParseUInt64().Should().Be(0);
        "10000000000000000000".ToArray().AsSpan().TryParseUInt64().Should().Be(10000000000000000000);
        "18446744073709551615".ToArray().AsSpan().TryParseUInt64().Should().Be(18446744073709551615);
        "18446744073709551616".ToArray().AsSpan().TryParseUInt64().Should().BeNull();

        "0".ToArray().AsSpan().TryParseUInt64(NumberStyles.HexNumber).Should().Be(0);
        "8000000000000000".ToArray().AsSpan().TryParseUInt64(NumberStyles.HexNumber).Should().Be(0x8000000000000000);
        "FFFFFFFFFFFFFFFF".ToArray().AsSpan().TryParseUInt64(NumberStyles.HexNumber).Should().Be(0xFFFFFFFFFFFFFFFF);
        "10000000000000000".ToArray().AsSpan().TryParseUInt64(NumberStyles.HexNumber).Should().BeNull();
    }

    [TestMethod()]
    public void TryParseInt8()
    {
        // string
        "0".TryParseInt8().Should().Be(0);
        "100".TryParseInt8().Should().Be(100);
        "127".TryParseInt8().Should().Be(127);
        "-128".TryParseInt8().Should().Be(-128);
        "128".TryParseInt8().Should().BeNull();

        "0".TryParseInt8(NumberStyles.HexNumber).Should().Be(0);
        "7F".TryParseInt8(NumberStyles.HexNumber).Should().Be(0x7F);
        "80".TryParseInt8(NumberStyles.HexNumber).Should().Be(-128);
        "100".TryParseInt8(NumberStyles.HexNumber).Should().BeNull();

        // ReadOnlySpan
        "0".AsSpan().TryParseInt8().Should().Be(0);
        "100".AsSpan().TryParseInt8().Should().Be(100);
        "127".AsSpan().TryParseInt8().Should().Be(127);
        "-128".AsSpan().TryParseInt8().Should().Be(-128);
        "128".AsSpan().TryParseInt8().Should().BeNull();

        "0".AsSpan().TryParseInt8(NumberStyles.HexNumber).Should().Be(0);
        "7F".AsSpan().TryParseInt8(NumberStyles.HexNumber).Should().Be(0x7F);
        "80".AsSpan().TryParseInt8(NumberStyles.HexNumber).Should().Be(-128);
        "100".AsSpan().TryParseInt8(NumberStyles.HexNumber).Should().BeNull();

        // Span
        "0".ToArray().AsSpan().TryParseInt8().Should().Be(0);
        "100".ToArray().AsSpan().TryParseInt8().Should().Be(100);
        "127".ToArray().AsSpan().TryParseInt8().Should().Be(127);
        "-128".ToArray().AsSpan().TryParseInt8().Should().Be(-128);
        "128".ToArray().AsSpan().TryParseInt8().Should().BeNull();

        "0".ToArray().AsSpan().TryParseInt8(NumberStyles.HexNumber).Should().Be(0);
        "7F".ToArray().AsSpan().TryParseInt8(NumberStyles.HexNumber).Should().Be(0x7F);
        "80".ToArray().AsSpan().TryParseInt8(NumberStyles.HexNumber).Should().Be(-128);
        "100".ToArray().AsSpan().TryParseInt8(NumberStyles.HexNumber).Should().BeNull();
    }

    [TestMethod()]
    public void TryParseInt16()
    {
        // string
        "0".TryParseInt16().Should().Be(0);
        "10000".TryParseInt16().Should().Be(10000);
        "32767".TryParseInt16().Should().Be(32767);
        "32768".TryParseInt16().Should().BeNull();

        "0".TryParseInt16(NumberStyles.HexNumber).Should().Be(0);
        "7FFF".TryParseInt16(NumberStyles.HexNumber).Should().Be(0x7FFF);
        "8000".TryParseInt16(NumberStyles.HexNumber).Should().Be(-32768);
        "10000".TryParseInt16(NumberStyles.HexNumber).Should().BeNull();

        // ReadOnlySpan
        "0".AsSpan().TryParseInt16().Should().Be(0);
        "10000".AsSpan().TryParseInt16().Should().Be(10000);
        "32767".AsSpan().TryParseInt16().Should().Be(32767);
        "32768".AsSpan().TryParseInt16().Should().BeNull();

        "0".AsSpan().TryParseInt16(NumberStyles.HexNumber).Should().Be(0);
        "7FFF".AsSpan().TryParseInt16(NumberStyles.HexNumber).Should().Be(0x7FFF);
        "8000".AsSpan().TryParseInt16(NumberStyles.HexNumber).Should().Be(-32768);
        "10000".AsSpan().TryParseInt16(NumberStyles.HexNumber).Should().BeNull();

        // Span
        "0".ToArray().AsSpan().TryParseInt16().Should().Be(0);
        "10000".ToArray().AsSpan().TryParseInt16().Should().Be(10000);
        "32767".ToArray().AsSpan().TryParseInt16().Should().Be(32767);
        "32768".ToArray().AsSpan().TryParseInt16().Should().BeNull();

        "0".ToArray().AsSpan().TryParseInt16(NumberStyles.HexNumber).Should().Be(0);
        "7FFF".ToArray().AsSpan().TryParseInt16(NumberStyles.HexNumber).Should().Be(0x7FFF);
        "8000".ToArray().AsSpan().TryParseInt16(NumberStyles.HexNumber).Should().Be(-32768);
        "10000".ToArray().AsSpan().TryParseInt16(NumberStyles.HexNumber).Should().BeNull();
    }

    [TestMethod()]
    public void TryParseInt32()
    {
        // string
        "0".TryParseInt32().Should().Be(0);
        "1000000000".TryParseInt32().Should().Be(1000000000);
        "2147483647".TryParseInt32().Should().Be(2147483647);
        "2147483648".TryParseInt32().Should().BeNull();

        "0".TryParseInt32(NumberStyles.HexNumber).Should().Be(0);
        "7FFFFFFF".TryParseInt32(NumberStyles.HexNumber).Should().Be(0x7FFFFFFF);
        "80000000".TryParseInt32(NumberStyles.HexNumber).Should().Be(-2147483648);
        "100000000".TryParseInt32(NumberStyles.HexNumber).Should().BeNull();

        // ReadOnlySpan
        "0".AsSpan().TryParseInt32().Should().Be(0);
        "1000000000".AsSpan().TryParseInt32().Should().Be(1000000000);
        "2147483647".AsSpan().TryParseInt32().Should().Be(2147483647);
        "2147483648".AsSpan().TryParseInt32().Should().BeNull();

        "0".AsSpan().TryParseInt32(NumberStyles.HexNumber).Should().Be(0);
        "7FFFFFFF".AsSpan().TryParseInt32(NumberStyles.HexNumber).Should().Be(0x7FFFFFFF);
        "80000000".AsSpan().TryParseInt32(NumberStyles.HexNumber).Should().Be(-2147483648);
        "100000000".AsSpan().TryParseInt32(NumberStyles.HexNumber).Should().BeNull();

        // Span
        "0".ToArray().AsSpan().TryParseInt32().Should().Be(0);
        "1000000000".ToArray().AsSpan().TryParseInt32().Should().Be(1000000000);
        "2147483647".ToArray().AsSpan().TryParseInt32().Should().Be(2147483647);
        "2147483648".ToArray().AsSpan().TryParseInt32().Should().BeNull();

        "0".ToArray().AsSpan().TryParseInt32(NumberStyles.HexNumber).Should().Be(0);
        "7FFFFFFF".ToArray().AsSpan().TryParseInt32(NumberStyles.HexNumber).Should().Be(0x7FFFFFFF);
        "80000000".ToArray().AsSpan().TryParseInt32(NumberStyles.HexNumber).Should().Be(-2147483648);
        "100000000".ToArray().AsSpan().TryParseInt32(NumberStyles.HexNumber).Should().BeNull();
    }

    [TestMethod()]
    public void TryParseInt64()
    {
        // string
        "0".TryParseInt64().Should().Be(0);
        "1000000000000000000".TryParseInt64().Should().Be(1000000000000000000);
        "9223372036854775807".TryParseInt64().Should().Be(9223372036854775807);
        "9223372036854775808".TryParseInt64().Should().BeNull();

        "0".TryParseInt64(NumberStyles.HexNumber).Should().Be(0);
        "7FFFFFFFFFFFFFFF".TryParseInt64(NumberStyles.HexNumber).Should().Be(0x7FFFFFFFFFFFFFFF);
        "8000000000000000".TryParseInt64(NumberStyles.HexNumber).Should().Be(-9223372036854775808);
        "10000000000000000".TryParseInt64(NumberStyles.HexNumber).Should().BeNull();

        // ReadOnlySpan
        "0".AsSpan().TryParseInt64().Should().Be(0);
        "1000000000000000000".AsSpan().TryParseInt64().Should().Be(1000000000000000000);
        "9223372036854775807".AsSpan().TryParseInt64().Should().Be(9223372036854775807);
        "9223372036854775808".AsSpan().TryParseInt64().Should().BeNull();

        "0".AsSpan().TryParseInt64(NumberStyles.HexNumber).Should().Be(0);
        "7FFFFFFFFFFFFFFF".AsSpan().TryParseInt64(NumberStyles.HexNumber).Should().Be(0x7FFFFFFFFFFFFFFF);
        "8000000000000000".AsSpan().TryParseInt64(NumberStyles.HexNumber).Should().Be(-9223372036854775808);
        "10000000000000000".AsSpan().TryParseInt64(NumberStyles.HexNumber).Should().BeNull();

        // Span
        "0".ToArray().AsSpan().TryParseInt64().Should().Be(0);
        "1000000000000000000".ToArray().AsSpan().TryParseInt64().Should().Be(1000000000000000000);
        "9223372036854775807".ToArray().AsSpan().TryParseInt64().Should().Be(9223372036854775807);
        "9223372036854775808".ToArray().AsSpan().TryParseInt64().Should().BeNull();

        "0".ToArray().AsSpan().TryParseInt64(NumberStyles.HexNumber).Should().Be(0);
        "7FFFFFFFFFFFFFFF".ToArray().AsSpan().TryParseInt64(NumberStyles.HexNumber).Should().Be(0x7FFFFFFFFFFFFFFF);
        "8000000000000000".ToArray().AsSpan().TryParseInt64(NumberStyles.HexNumber).Should().Be(-9223372036854775808);
        "10000000000000000".ToArray().AsSpan().TryParseInt64(NumberStyles.HexNumber).Should().BeNull();
    }

    [TestMethod()]
    public void TryParseFloat()
    {
        // string
        "0".TryParseFloat().Should().Be(0);
        "100".TryParseFloat().Should().Be(100f);
        "-100".TryParseFloat().Should().Be(-100f);
        "asd".TryParseFloat().Should().BeNull();

        "100".TryParseFloat(NumberStyles.Float).Should().Be(100f);

        // ReadOnlySpan
        "0".AsSpan().TryParseFloat().Should().Be(0);
        "100".AsSpan().TryParseFloat().Should().Be(100f);
        "-100".AsSpan().TryParseFloat().Should().Be(-100f);
        "asd".AsSpan().TryParseFloat().Should().BeNull();

        "100".AsSpan().TryParseFloat(NumberStyles.Float).Should().Be(100f);

        // Span
        "0".ToArray().AsSpan().TryParseFloat().Should().Be(0);
        "100".ToArray().AsSpan().TryParseFloat().Should().Be(100f);
        "-100".ToArray().AsSpan().TryParseFloat().Should().Be(-100f);
        "asd".ToArray().AsSpan().TryParseFloat().Should().BeNull();

        "100".ToArray().AsSpan().TryParseFloat(NumberStyles.Float).Should().Be(100f);
    }

    [TestMethod()]
    public void TryParseDouble()
    {
        // string
        "0".TryParseDouble().Should().Be(0);
        "100".TryParseDouble().Should().Be(100d);
        "-100".TryParseDouble().Should().Be(-100d);
        "asd".TryParseDouble().Should().BeNull();

        "100".TryParseDouble(NumberStyles.Float).Should().Be(100d);

        // ReadOnlySpan
        "0".AsSpan().TryParseDouble().Should().Be(0);
        "100".AsSpan().TryParseDouble().Should().Be(100d);
        "-100".AsSpan().TryParseDouble().Should().Be(-100d);
        "asd".AsSpan().TryParseDouble().Should().BeNull();

        "100".AsSpan().TryParseDouble(NumberStyles.Float).Should().Be(100d);

        // Span
        "0".ToArray().AsSpan().TryParseDouble().Should().Be(0);
        "100".ToArray().AsSpan().TryParseDouble().Should().Be(100d);
        "-100".ToArray().AsSpan().TryParseDouble().Should().Be(-100d);
        "asd".ToArray().AsSpan().TryParseDouble().Should().BeNull();

        "100".ToArray().AsSpan().TryParseDouble(NumberStyles.Float).Should().Be(100d);
    }

    [TestMethod()]
    public void TryParseDecimal()
    {
        // string
        "0".TryParseDecimal().Should().Be(0);
        "100".TryParseDecimal().Should().Be(100m);
        "-100".TryParseDecimal().Should().Be(-100m);
        "asd".TryParseDecimal().Should().BeNull();

        "100".TryParseDecimal(NumberStyles.Number).Should().Be(100m);

        // ReadOnlySpan
        "0".AsSpan().TryParseDecimal().Should().Be(0);
        "100".AsSpan().TryParseDecimal().Should().Be(100m);
        "-100".AsSpan().TryParseDecimal().Should().Be(-100m);
        "asd".AsSpan().TryParseDecimal().Should().BeNull();

        "100".AsSpan().TryParseDecimal(NumberStyles.Number).Should().Be(100m);

        // Span
        "0".ToArray().AsSpan().TryParseDecimal().Should().Be(0);
        "100".ToArray().AsSpan().TryParseDecimal().Should().Be(100m);
        "-100".ToArray().AsSpan().TryParseDecimal().Should().Be(-100m);
        "asd".ToArray().AsSpan().TryParseDecimal().Should().BeNull();

        "100".ToArray().AsSpan().TryParseDecimal(NumberStyles.Number).Should().Be(100m);
    }

    [TestMethod()]
    public void TryParseDateTime()
    {
        // string
        "2022/12/31".TryParseDateTime().Should().Be(new DateTime(2022, 12, 31));
        "asd".TryParseDateTime().Should().BeNull();

        "11:12:13".TryParseDateTime(DateTimeStyles.NoCurrentDateDefault).Should().Be(new DateTime(1, 1, 1, 11, 12, 13));

        // ReadOnlySpan
        "2022/12/31".AsSpan().TryParseDateTime().Should().Be(new DateTime(2022, 12, 31));
        "asd".AsSpan().TryParseDateTime().Should().BeNull();

        "11:12:13".AsSpan().TryParseDateTime(DateTimeStyles.NoCurrentDateDefault).Should().Be(new DateTime(1, 1, 1, 11, 12, 13));

        // Span
        "2022/12/31".ToArray().AsSpan().TryParseDateTime().Should().Be(new DateTime(2022, 12, 31));
        "asd".ToArray().AsSpan().TryParseDateTime().Should().BeNull();

        "11:12:13".ToArray().AsSpan().TryParseDateTime(DateTimeStyles.NoCurrentDateDefault).Should().Be(new DateTime(1, 1, 1, 11, 12, 13));
    }

    [TestMethod()]
    public void TryParseDateTimeExact()
    {
        // string
        "1234_05_06".TryParseDateTimeExact("yyyy_M_d").Should().Be(new DateTime(1234, 5, 6));

        "zz".TryParseDateTimeExact("yyyy_M_d").Should().BeNull();

        // ReadOnlySpan
        "1234_05_06".AsSpan().TryParseDateTimeExact("yyyy_M_d").Should().Be(new DateTime(1234, 5, 6));

        "zz".AsSpan().TryParseDateTimeExact("yyyy_M_d").Should().BeNull();

        // Span
        "1234_05_06".ToArray().AsSpan().TryParseDateTimeExact("yyyy_M_d").Should().Be(new DateTime(1234, 5, 6));

        "zz".ToArray().AsSpan().TryParseDateTimeExact("yyyy_M_d").Should().BeNull();

    }
    #endregion

    #region TryParseHex
    [TestMethod()]
    public void TryParseHex8()
    {
        // string
        "0".TryParseHex8().Should().Be(0);
        "80".TryParseHex8().Should().Be(0x80);
        "FF".TryParseHex8().Should().Be(0xFF);
        "100".TryParseHex8().Should().BeNull();

        // ReadOnlySpan
        "0".AsSpan().TryParseHex8().Should().Be(0);
        "80".AsSpan().TryParseHex8().Should().Be(0x80);
        "FF".AsSpan().TryParseHex8().Should().Be(0xFF);
        "100".AsSpan().TryParseHex8().Should().BeNull();

        // Span
        "0".ToArray().AsSpan().TryParseHex8().Should().Be(0);
        "80".ToArray().AsSpan().TryParseHex8().Should().Be(0x80);
        "FF".ToArray().AsSpan().TryParseHex8().Should().Be(0xFF);
        "100".ToArray().AsSpan().TryParseHex8().Should().BeNull();

    }

    [TestMethod()]
    public void TryParseHex16()
    {
        // string
        "0".TryParseHex16().Should().Be(0);
        "8000".TryParseHex16().Should().Be(0x8000);
        "FFFF".TryParseHex16().Should().Be(0xFFFF);
        "10000".TryParseHex16().Should().BeNull();

        // ReadOnlySpan
        "0".AsSpan().TryParseHex16().Should().Be(0);
        "8000".AsSpan().TryParseHex16().Should().Be(0x8000);
        "FFFF".AsSpan().TryParseHex16().Should().Be(0xFFFF);
        "10000".AsSpan().TryParseHex16().Should().BeNull();

        // Span
        "0".ToArray().AsSpan().TryParseHex16().Should().Be(0);
        "8000".ToArray().AsSpan().TryParseHex16().Should().Be(0x8000);
        "FFFF".ToArray().AsSpan().TryParseHex16().Should().Be(0xFFFF);
        "10000".ToArray().AsSpan().TryParseHex16().Should().BeNull();
    }

    [TestMethod()]
    public void TryParseHex32()
    {
        // string
        "0".TryParseHex32().Should().Be(0);
        "80000000".TryParseHex32().Should().Be(0x80000000);
        "FFFFFFFF".TryParseHex32().Should().Be(0xFFFFFFFF);
        "100000000".TryParseHex32().Should().BeNull();

        // ReadOnlySpan
        "0".AsSpan().TryParseHex32().Should().Be(0);
        "80000000".AsSpan().TryParseHex32().Should().Be(0x80000000);
        "FFFFFFFF".AsSpan().TryParseHex32().Should().Be(0xFFFFFFFF);
        "100000000".AsSpan().TryParseHex32().Should().BeNull();

        // Span
        "0".ToArray().AsSpan().TryParseHex32().Should().Be(0);
        "80000000".ToArray().AsSpan().TryParseHex32().Should().Be(0x80000000);
        "FFFFFFFF".ToArray().AsSpan().TryParseHex32().Should().Be(0xFFFFFFFF);
        "100000000".ToArray().AsSpan().TryParseHex32().Should().BeNull();
    }

    [TestMethod()]
    public void TryParseHex64()
    {
        // string
        "0".TryParseHex64().Should().Be(0);
        "8000000000000000".TryParseHex64().Should().Be(0x8000000000000000);
        "FFFFFFFFFFFFFFFF".TryParseHex64().Should().Be(0xFFFFFFFFFFFFFFFF);
        "10000000000000000".TryParseHex64().Should().BeNull();

        // ReadOnlySpan
        "0".AsSpan().TryParseHex64().Should().Be(0);
        "8000000000000000".AsSpan().TryParseHex64().Should().Be(0x8000000000000000);
        "FFFFFFFFFFFFFFFF".AsSpan().TryParseHex64().Should().Be(0xFFFFFFFFFFFFFFFF);
        "10000000000000000".AsSpan().TryParseHex64().Should().BeNull();

        // Span
        "0".ToArray().AsSpan().TryParseHex64().Should().Be(0);
        "8000000000000000".ToArray().AsSpan().TryParseHex64().Should().Be(0x8000000000000000);
        "FFFFFFFFFFFFFFFF".ToArray().AsSpan().TryParseHex64().Should().Be(0xFFFFFFFFFFFFFFFF);
        "10000000000000000".ToArray().AsSpan().TryParseHex64().Should().BeNull();
    }
    #endregion

    #region TryParseBin
    [TestMethod()]
    public void TryParseBin8()
    {
        // string
        "0".TryParseBin8(trim: true, snake: true).Should().Be(0);
        "1".TryParseBin8(trim: true, snake: true).Should().Be(1);
        "10".TryParseBin8(trim: true, snake: true).Should().Be(2);
        "11111111".TryParseBin8(trim: true, snake: true).Should().Be(0xFF);
        "100000000".TryParseBin8(trim: true, snake: true).Should().BeNull();

        // ReadOnlySpan
        "0".AsSpan().TryParseBin8(trim: true, snake: true).Should().Be(0);
        "1".AsSpan().TryParseBin8(trim: true, snake: true).Should().Be(1);
        "10".AsSpan().TryParseBin8(trim: true, snake: true).Should().Be(2);
        "11111111".AsSpan().TryParseBin8(trim: true, snake: true).Should().Be(0xFF);
        "100000000".AsSpan().TryParseBin8(trim: true, snake: true).Should().BeNull();

        // Span
        "0".ToArray().AsSpan().TryParseBin8(trim: true, snake: true).Should().Be(0);
        "1".ToArray().AsSpan().TryParseBin8(trim: true, snake: true).Should().Be(1);
        "10".ToArray().AsSpan().TryParseBin8(trim: true, snake: true).Should().Be(2);
        "11111111".ToArray().AsSpan().TryParseBin8(trim: true, snake: true).Should().Be(0xFF);
        "100000000".ToArray().AsSpan().TryParseBin8(trim: true, snake: true).Should().BeNull();

        // parameters
        "   1   ".AsSpan().TryParseBin8(trim: true, snake: true).Should().Be(1);
        "   1   ".AsSpan().TryParseBin8(trim: false, snake: true).Should().BeNull();
        "1111_1111".AsSpan().TryParseBin8(trim: true, snake: true).Should().Be(0xFF);
        "1111_1111".AsSpan().TryParseBin8(trim: true, snake: false).Should().BeNull();
    }

    [TestMethod()]
    public void TryParseBin16()
    {
        // string
        "0".TryParseBin16(trim: true, snake: true).Should().Be(0);
        "1".TryParseBin16(trim: true, snake: true).Should().Be(1);
        "10".TryParseBin16(trim: true, snake: true).Should().Be(2);
        "1111111111111111".TryParseBin16(trim: true, snake: true).Should().Be(0xFFFF);
        "10000000000000000".TryParseBin16(trim: true, snake: true).Should().BeNull();

        // ReadOnlySpan
        "0".AsSpan().TryParseBin16(trim: true, snake: true).Should().Be(0);
        "1".AsSpan().TryParseBin16(trim: true, snake: true).Should().Be(1);
        "10".AsSpan().TryParseBin16(trim: true, snake: true).Should().Be(2);
        "1111111111111111".AsSpan().TryParseBin16(trim: true, snake: true).Should().Be(0xFFFF);
        "10000000000000000".AsSpan().TryParseBin16(trim: true, snake: true).Should().BeNull();

        // Span
        "0".ToArray().AsSpan().TryParseBin16(trim: true, snake: true).Should().Be(0);
        "1".ToArray().AsSpan().TryParseBin16(trim: true, snake: true).Should().Be(1);
        "10".ToArray().AsSpan().TryParseBin16(trim: true, snake: true).Should().Be(2);
        "1111111111111111".ToArray().AsSpan().TryParseBin16(trim: true, snake: true).Should().Be(0xFFFF);
        "10000000000000000".ToArray().AsSpan().TryParseBin16(trim: true, snake: true).Should().BeNull();

        // parameters
        "   1   ".AsSpan().TryParseBin16(trim: true, snake: true).Should().Be(1);
        "   1   ".AsSpan().TryParseBin16(trim: false, snake: true).Should().BeNull();
        "11111111_11111111".AsSpan().TryParseBin16(trim: true, snake: true).Should().Be(0xFFFF);
        "11111111_11111111".AsSpan().TryParseBin16(trim: true, snake: false).Should().BeNull();
    }

    [TestMethod()]
    public void TryParseBin32()
    {
        // string
        "0".TryParseBin32(trim: true, snake: true).Should().Be(0);
        "1".TryParseBin32(trim: true, snake: true).Should().Be(1);
        "10".TryParseBin32(trim: true, snake: true).Should().Be(2);
        "11111111111111111111111111111111".TryParseBin32(trim: true, snake: true).Should().Be(0xFFFFFFFF);
        "100000000000000000000000000000000".TryParseBin32(trim: true, snake: true).Should().BeNull();

        // ReadOnlySpan
        "0".AsSpan().TryParseBin32(trim: true, snake: true).Should().Be(0);
        "1".AsSpan().TryParseBin32(trim: true, snake: true).Should().Be(1);
        "10".AsSpan().TryParseBin32(trim: true, snake: true).Should().Be(2);
        "11111111111111111111111111111111".AsSpan().TryParseBin32(trim: true, snake: true).Should().Be(0xFFFFFFFF);
        "100000000000000000000000000000000".AsSpan().TryParseBin32(trim: true, snake: true).Should().BeNull();

        // Span
        "0".ToArray().AsSpan().TryParseBin32(trim: true, snake: true).Should().Be(0);
        "1".ToArray().AsSpan().TryParseBin32(trim: true, snake: true).Should().Be(1);
        "10".ToArray().AsSpan().TryParseBin32(trim: true, snake: true).Should().Be(2);
        "11111111111111111111111111111111".ToArray().AsSpan().TryParseBin32(trim: true, snake: true).Should().Be(0xFFFFFFFF);
        "100000000000000000000000000000000".ToArray().AsSpan().TryParseBin32(trim: true, snake: true).Should().BeNull();

        // parameters
        "   1   ".AsSpan().TryParseBin32(trim: true, snake: true).Should().Be(1);
        "   1   ".AsSpan().TryParseBin32(trim: false, snake: true).Should().BeNull();
        "11111111_11111111_11111111_11111111".AsSpan().TryParseBin32(trim: true, snake: true).Should().Be(0xFFFFFFFF);
        "11111111_11111111_11111111_11111111".AsSpan().TryParseBin32(trim: true, snake: false).Should().BeNull();
    }

    [TestMethod()]
    public void TryParseBin64()
    {
        // string
        "0".TryParseBin64(trim: true, snake: true).Should().Be(0);
        "1".TryParseBin64(trim: true, snake: true).Should().Be(1);
        "10".TryParseBin64(trim: true, snake: true).Should().Be(2);
        "1111111111111111111111111111111111111111111111111111111111111111".TryParseBin64(trim: true, snake: true).Should().Be(0xFFFFFFFFFFFFFFFF);
        "10000000000000000000000000000000000000000000000000000000000000000".TryParseBin64(trim: true, snake: true).Should().BeNull();

        // ReadOnlySpan
        "0".AsSpan().TryParseBin64(trim: true, snake: true).Should().Be(0);
        "1".AsSpan().TryParseBin64(trim: true, snake: true).Should().Be(1);
        "10".AsSpan().TryParseBin64(trim: true, snake: true).Should().Be(2);
        "1111111111111111111111111111111111111111111111111111111111111111".AsSpan().TryParseBin64(trim: true, snake: true).Should().Be(0xFFFFFFFFFFFFFFFF);
        "10000000000000000000000000000000000000000000000000000000000000000".AsSpan().TryParseBin64(trim: true, snake: true).Should().BeNull();

        // Span
        "0".ToArray().AsSpan().TryParseBin64(trim: true, snake: true).Should().Be(0);
        "1".ToArray().AsSpan().TryParseBin64(trim: true, snake: true).Should().Be(1);
        "10".ToArray().AsSpan().TryParseBin64(trim: true, snake: true).Should().Be(2);
        "1111111111111111111111111111111111111111111111111111111111111111".ToArray().AsSpan().TryParseBin64(trim: true, snake: true).Should().Be(0xFFFFFFFFFFFFFFFF);
        "10000000000000000000000000000000000000000000000000000000000000000".ToArray().AsSpan().TryParseBin64(trim: true, snake: true).Should().BeNull();

        // parameters
        "   1   ".AsSpan().TryParseBin64(trim: true, snake: true).Should().Be(1);
        "   1   ".AsSpan().TryParseBin64(trim: false, snake: true).Should().BeNull();
        "11111111_11111111_11111111_11111111_11111111_11111111_11111111_11111111".AsSpan().TryParseBin64(trim: true, snake: true).Should().Be(0xFFFFFFFFFFFFFFFF);
        "11111111_11111111_11111111_11111111_11111111_11111111_11111111_11111111".AsSpan().TryParseBin64(trim: true, snake: false).Should().BeNull();
    }
    #endregion

    #region TryParseWithPrefix
    [TestMethod()]
    public void TryParseWithPrefix8()
    {
        // string
        "12".TryParseWithPrefix8().Should().Be(12);
        "0x12".TryParseWithPrefix8().Should().Be(0x12);
        "0X12".TryParseWithPrefix8().Should().Be(0x12);
        "&h12".TryParseWithPrefix8().Should().Be(0x12);
        "&H12".TryParseWithPrefix8().Should().Be(0x12);
        "#12".TryParseWithPrefix8().Should().Be(0x12);
        "0b0001_0010".TryParseWithPrefix8().Should().Be(0x12);
        "255".TryParseWithPrefix8().Should().Be(255);
        "0xFF".TryParseWithPrefix8().Should().Be(0xFF);
        "&HFF".TryParseWithPrefix8().Should().Be(0xFF);
        "#FF".TryParseWithPrefix8().Should().Be(0xFF);
        "0b1111_1111".TryParseWithPrefix8().Should().Be(0xFF);
        "256".TryParseWithPrefix8().Should().BeNull();
        "0x100".TryParseWithPrefix8().Should().BeNull();
        "&H100".TryParseWithPrefix8().Should().BeNull();
        "#100".TryParseWithPrefix8().Should().BeNull();
        "a".TryParseWithPrefix8().Should().BeNull();
        "0xG".TryParseWithPrefix8().Should().BeNull();

        // ReadOnlySpan
        "12".AsSpan().TryParseWithPrefix8().Should().Be(12);
        "0x12".AsSpan().TryParseWithPrefix8().Should().Be(0x12);
        "0X12".AsSpan().TryParseWithPrefix8().Should().Be(0x12);
        "&h12".AsSpan().TryParseWithPrefix8().Should().Be(0x12);
        "&H12".AsSpan().TryParseWithPrefix8().Should().Be(0x12);
        "#12".AsSpan().TryParseWithPrefix8().Should().Be(0x12);
        "0b0001_0010".AsSpan().TryParseWithPrefix8().Should().Be(0x12);
        "255".AsSpan().TryParseWithPrefix8().Should().Be(255);
        "0xFF".AsSpan().TryParseWithPrefix8().Should().Be(0xFF);
        "&HFF".AsSpan().TryParseWithPrefix8().Should().Be(0xFF);
        "#FF".AsSpan().TryParseWithPrefix8().Should().Be(0xFF);
        "0b1111_1111".AsSpan().TryParseWithPrefix8().Should().Be(0xFF);
        "256".AsSpan().TryParseWithPrefix8().Should().BeNull();
        "0x100".AsSpan().TryParseWithPrefix8().Should().BeNull();
        "&H100".AsSpan().TryParseWithPrefix8().Should().BeNull();
        "#100".AsSpan().TryParseWithPrefix8().Should().BeNull();
        "a".AsSpan().TryParseWithPrefix8().Should().BeNull();
        "0xG".AsSpan().TryParseWithPrefix8().Should().BeNull();

        // Span
        "12".ToArray().AsSpan().TryParseWithPrefix8().Should().Be(12);
        "0x12".ToArray().AsSpan().TryParseWithPrefix8().Should().Be(0x12);
        "0X12".ToArray().AsSpan().TryParseWithPrefix8().Should().Be(0x12);
        "&h12".ToArray().AsSpan().TryParseWithPrefix8().Should().Be(0x12);
        "&H12".ToArray().AsSpan().TryParseWithPrefix8().Should().Be(0x12);
        "#12".ToArray().AsSpan().TryParseWithPrefix8().Should().Be(0x12);
        "0b0001_0010".ToArray().AsSpan().TryParseWithPrefix8().Should().Be(0x12);
        "255".ToArray().AsSpan().TryParseWithPrefix8().Should().Be(255);
        "0xFF".ToArray().AsSpan().TryParseWithPrefix8().Should().Be(0xFF);
        "&HFF".ToArray().AsSpan().TryParseWithPrefix8().Should().Be(0xFF);
        "#FF".ToArray().AsSpan().TryParseWithPrefix8().Should().Be(0xFF);
        "0b1111_1111".ToArray().AsSpan().TryParseWithPrefix8().Should().Be(0xFF);
        "256".ToArray().AsSpan().TryParseWithPrefix8().Should().BeNull();
        "0x100".ToArray().AsSpan().TryParseWithPrefix8().Should().BeNull();
        "&H100".ToArray().AsSpan().TryParseWithPrefix8().Should().BeNull();
        "#100".ToArray().AsSpan().TryParseWithPrefix8().Should().BeNull();
        "0b1_0000_0000".ToArray().AsSpan().TryParseWithPrefix8().Should().BeNull();
        "a".ToArray().AsSpan().TryParseWithPrefix8().Should().BeNull();
        "0xG".ToArray().AsSpan().TryParseWithPrefix8().Should().BeNull();
    }

    [TestMethod()]
    public void TryParseWithPrefix16()
    {
        // string
        "1234".TryParseWithPrefix16().Should().Be(1234);
        "0x1234".TryParseWithPrefix16().Should().Be(0x1234);
        "0X1234".TryParseWithPrefix16().Should().Be(0x1234);
        "&h1234".TryParseWithPrefix16().Should().Be(0x1234);
        "&H1234".TryParseWithPrefix16().Should().Be(0x1234);
        "#1234".TryParseWithPrefix16().Should().Be(0x1234);
        "0b0001001000110100".TryParseWithPrefix16().Should().Be(0x1234);
        "65535".TryParseWithPrefix16().Should().Be(65535);
        "0xFFFF".TryParseWithPrefix16().Should().Be(0xFFFF);
        "&HFFFF".TryParseWithPrefix16().Should().Be(0xFFFF);
        "#FFFF".TryParseWithPrefix16().Should().Be(0xFFFF);
        "0b1111111111111111".TryParseWithPrefix16().Should().Be(0xFFFF);
        "65536".TryParseWithPrefix16().Should().BeNull();
        "0x10000".TryParseWithPrefix16().Should().BeNull();
        "&H10000".TryParseWithPrefix16().Should().BeNull();
        "#10000".TryParseWithPrefix16().Should().BeNull();
        "0b10000000000000000".TryParseWithPrefix16().Should().BeNull();
        "a".TryParseWithPrefix16().Should().BeNull();
        "0xG".TryParseWithPrefix16().Should().BeNull();

        // ReadOnlySpan
        "1234".AsSpan().TryParseWithPrefix16().Should().Be(1234);
        "0x1234".AsSpan().TryParseWithPrefix16().Should().Be(0x1234);
        "0X1234".AsSpan().TryParseWithPrefix16().Should().Be(0x1234);
        "&h1234".AsSpan().TryParseWithPrefix16().Should().Be(0x1234);
        "&H1234".AsSpan().TryParseWithPrefix16().Should().Be(0x1234);
        "#1234".AsSpan().TryParseWithPrefix16().Should().Be(0x1234);
        "0b0001001000110100".AsSpan().TryParseWithPrefix16().Should().Be(0x1234);
        "65535".AsSpan().TryParseWithPrefix16().Should().Be(65535);
        "0xFFFF".AsSpan().TryParseWithPrefix16().Should().Be(0xFFFF);
        "&HFFFF".AsSpan().TryParseWithPrefix16().Should().Be(0xFFFF);
        "#FFFF".AsSpan().TryParseWithPrefix16().Should().Be(0xFFFF);
        "0b1111111111111111".AsSpan().TryParseWithPrefix16().Should().Be(0xFFFF);
        "65536".AsSpan().TryParseWithPrefix16().Should().BeNull();
        "0x10000".AsSpan().TryParseWithPrefix16().Should().BeNull();
        "&H10000".AsSpan().TryParseWithPrefix16().Should().BeNull();
        "#10000".AsSpan().TryParseWithPrefix16().Should().BeNull();
        "0b10000000000000000".AsSpan().TryParseWithPrefix16().Should().BeNull();
        "a".AsSpan().TryParseWithPrefix16().Should().BeNull();
        "0xG".AsSpan().TryParseWithPrefix16().Should().BeNull();

        // Span
        "1234".ToArray().AsSpan().TryParseWithPrefix16().Should().Be(1234);
        "0x1234".ToArray().AsSpan().TryParseWithPrefix16().Should().Be(0x1234);
        "0X1234".ToArray().AsSpan().TryParseWithPrefix16().Should().Be(0x1234);
        "&h1234".ToArray().AsSpan().TryParseWithPrefix16().Should().Be(0x1234);
        "&H1234".ToArray().AsSpan().TryParseWithPrefix16().Should().Be(0x1234);
        "#1234".ToArray().AsSpan().TryParseWithPrefix16().Should().Be(0x1234);
        "0b0001001000110100".ToArray().AsSpan().TryParseWithPrefix16().Should().Be(0x1234);
        "65535".ToArray().AsSpan().TryParseWithPrefix16().Should().Be(65535);
        "0xFFFF".ToArray().AsSpan().TryParseWithPrefix16().Should().Be(0xFFFF);
        "&HFFFF".ToArray().AsSpan().TryParseWithPrefix16().Should().Be(0xFFFF);
        "#FFFF".ToArray().AsSpan().TryParseWithPrefix16().Should().Be(0xFFFF);
        "0b1111111111111111".ToArray().AsSpan().TryParseWithPrefix16().Should().Be(0xFFFF);
        "65536".ToArray().AsSpan().TryParseWithPrefix16().Should().BeNull();
        "0x10000".ToArray().AsSpan().TryParseWithPrefix16().Should().BeNull();
        "&H10000".ToArray().AsSpan().TryParseWithPrefix16().Should().BeNull();
        "#10000".ToArray().AsSpan().TryParseWithPrefix16().Should().BeNull();
        "0b10000000000000000".ToArray().AsSpan().TryParseWithPrefix16().Should().BeNull();
        "a".ToArray().AsSpan().TryParseWithPrefix16().Should().BeNull();
        "0xG".ToArray().AsSpan().TryParseWithPrefix16().Should().BeNull();
    }

    [TestMethod()]
    public void TryParseWithPrefix32()
    {
        // string
        "12345678".TryParseWithPrefix32().Should().Be(12345678);
        "0x12345678".TryParseWithPrefix32().Should().Be(0x12345678);
        "0X12345678".TryParseWithPrefix32().Should().Be(0x12345678);
        "&h12345678".TryParseWithPrefix32().Should().Be(0x12345678);
        "&H12345678".TryParseWithPrefix32().Should().Be(0x12345678);
        "#12345678".TryParseWithPrefix32().Should().Be(0x12345678);
        "0b00010010001101000101011001111000".TryParseWithPrefix32().Should().Be(0x12345678);
        "4294967295".TryParseWithPrefix32().Should().Be(4294967295);
        "0xFFFFFFFF".TryParseWithPrefix32().Should().Be(0xFFFFFFFF);
        "&HFFFFFFFF".TryParseWithPrefix32().Should().Be(0xFFFFFFFF);
        "#FFFFFFFF".TryParseWithPrefix32().Should().Be(0xFFFFFFFF);
        "0b11111111111111111111111111111111".TryParseWithPrefix32().Should().Be(0xFFFFFFFF);
        "4294967296".TryParseWithPrefix32().Should().BeNull();
        "0x100000000".TryParseWithPrefix32().Should().BeNull();
        "&H100000000".TryParseWithPrefix32().Should().BeNull();
        "#100000000".TryParseWithPrefix32().Should().BeNull();
        "0b100000000000000000000000000000000".TryParseWithPrefix32().Should().BeNull();
        "a".TryParseWithPrefix32().Should().BeNull();
        "0xG".TryParseWithPrefix32().Should().BeNull();

        // ReadOnlySpan
        "12345678".AsSpan().TryParseWithPrefix32().Should().Be(12345678);
        "0x12345678".AsSpan().TryParseWithPrefix32().Should().Be(0x12345678);
        "0X12345678".AsSpan().TryParseWithPrefix32().Should().Be(0x12345678);
        "&h12345678".AsSpan().TryParseWithPrefix32().Should().Be(0x12345678);
        "&H12345678".AsSpan().TryParseWithPrefix32().Should().Be(0x12345678);
        "#12345678".AsSpan().TryParseWithPrefix32().Should().Be(0x12345678);
        "0b00010010001101000101011001111000".AsSpan().TryParseWithPrefix32().Should().Be(0x12345678);
        "4294967295".AsSpan().TryParseWithPrefix32().Should().Be(4294967295);
        "0xFFFFFFFF".AsSpan().TryParseWithPrefix32().Should().Be(0xFFFFFFFF);
        "&HFFFFFFFF".AsSpan().TryParseWithPrefix32().Should().Be(0xFFFFFFFF);
        "#FFFFFFFF".AsSpan().TryParseWithPrefix32().Should().Be(0xFFFFFFFF);
        "0b11111111111111111111111111111111".AsSpan().TryParseWithPrefix32().Should().Be(0xFFFFFFFF);
        "4294967296".AsSpan().TryParseWithPrefix32().Should().BeNull();
        "0x100000000".AsSpan().TryParseWithPrefix32().Should().BeNull();
        "&H100000000".AsSpan().TryParseWithPrefix32().Should().BeNull();
        "#100000000".AsSpan().TryParseWithPrefix32().Should().BeNull();
        "0b100000000000000000000000000000000".AsSpan().TryParseWithPrefix32().Should().BeNull();
        "a".AsSpan().TryParseWithPrefix32().Should().BeNull();
        "0xG".AsSpan().TryParseWithPrefix32().Should().BeNull();

        // Span
        "12345678".ToArray().AsSpan().TryParseWithPrefix32().Should().Be(12345678);
        "0x12345678".ToArray().AsSpan().TryParseWithPrefix32().Should().Be(0x12345678);
        "0X12345678".ToArray().AsSpan().TryParseWithPrefix32().Should().Be(0x12345678);
        "&h12345678".ToArray().AsSpan().TryParseWithPrefix32().Should().Be(0x12345678);
        "&H12345678".ToArray().AsSpan().TryParseWithPrefix32().Should().Be(0x12345678);
        "#12345678".ToArray().AsSpan().TryParseWithPrefix32().Should().Be(0x12345678);
        "0b00010010001101000101011001111000".ToArray().AsSpan().TryParseWithPrefix32().Should().Be(0x12345678);
        "4294967295".ToArray().AsSpan().TryParseWithPrefix32().Should().Be(4294967295);
        "0xFFFFFFFF".ToArray().AsSpan().TryParseWithPrefix32().Should().Be(0xFFFFFFFF);
        "&HFFFFFFFF".ToArray().AsSpan().TryParseWithPrefix32().Should().Be(0xFFFFFFFF);
        "#FFFFFFFF".ToArray().AsSpan().TryParseWithPrefix32().Should().Be(0xFFFFFFFF);
        "0b11111111111111111111111111111111".ToArray().AsSpan().TryParseWithPrefix32().Should().Be(0xFFFFFFFF);
        "4294967296".ToArray().AsSpan().TryParseWithPrefix32().Should().BeNull();
        "0x100000000".ToArray().AsSpan().TryParseWithPrefix32().Should().BeNull();
        "&H100000000".ToArray().AsSpan().TryParseWithPrefix32().Should().BeNull();
        "#100000000".ToArray().AsSpan().TryParseWithPrefix32().Should().BeNull();
        "0b100000000000000000000000000000000".ToArray().AsSpan().TryParseWithPrefix32().Should().BeNull();
        "a".ToArray().AsSpan().TryParseWithPrefix32().Should().BeNull();
        "0xG".ToArray().AsSpan().TryParseWithPrefix32().Should().BeNull();
    }

    [TestMethod()]
    public void TryParseWithPrefix64()
    {
        // string
        "1234567890123456".TryParseWithPrefix64().Should().Be(1234567890123456);
        "0x1234567890123456".TryParseWithPrefix64().Should().Be(0x1234567890123456);
        "0X1234567890123456".TryParseWithPrefix64().Should().Be(0x1234567890123456);
        "&h1234567890123456".TryParseWithPrefix64().Should().Be(0x1234567890123456);
        "&H1234567890123456".TryParseWithPrefix64().Should().Be(0x1234567890123456);
        "#1234567890123456".TryParseWithPrefix64().Should().Be(0x1234567890123456);
        "0b0001001000110100010101100111100010010000000100100011010001010110".TryParseWithPrefix64().Should().Be(0x1234567890123456);
        "18446744073709551615".TryParseWithPrefix64().Should().Be(18446744073709551615);
        "0xFFFFFFFFFFFFFFFF".TryParseWithPrefix64().Should().Be(0xFFFFFFFFFFFFFFFF);
        "&HFFFFFFFFFFFFFFFF".TryParseWithPrefix64().Should().Be(0xFFFFFFFFFFFFFFFF);
        "#FFFFFFFFFFFFFFFF".TryParseWithPrefix64().Should().Be(0xFFFFFFFFFFFFFFFF);
        "0b1111111111111111111111111111111111111111111111111111111111111111".TryParseWithPrefix64().Should().Be(0xFFFFFFFFFFFFFFFF);
        "18446744073709551616".TryParseWithPrefix64().Should().BeNull();
        "0x10000000000000000".TryParseWithPrefix64().Should().BeNull();
        "&H10000000000000000".TryParseWithPrefix64().Should().BeNull();
        "#10000000000000000".TryParseWithPrefix64().Should().BeNull();
        "0b10000000000000000000000000000000000000000000000000000000000000000".TryParseWithPrefix64().Should().BeNull();
        "a".TryParseWithPrefix64().Should().BeNull();
        "0xG".TryParseWithPrefix64().Should().BeNull();

        // ReadOnlySpan
        "1234567890123456".AsSpan().TryParseWithPrefix64().Should().Be(1234567890123456);
        "0x1234567890123456".AsSpan().TryParseWithPrefix64().Should().Be(0x1234567890123456);
        "0X1234567890123456".AsSpan().TryParseWithPrefix64().Should().Be(0x1234567890123456);
        "&h1234567890123456".AsSpan().TryParseWithPrefix64().Should().Be(0x1234567890123456);
        "&H1234567890123456".AsSpan().TryParseWithPrefix64().Should().Be(0x1234567890123456);
        "#1234567890123456".AsSpan().TryParseWithPrefix64().Should().Be(0x1234567890123456);
        "0b0001001000110100010101100111100010010000000100100011010001010110".AsSpan().TryParseWithPrefix64().Should().Be(0x1234567890123456);
        "18446744073709551615".AsSpan().TryParseWithPrefix64().Should().Be(18446744073709551615);
        "0xFFFFFFFFFFFFFFFF".AsSpan().TryParseWithPrefix64().Should().Be(0xFFFFFFFFFFFFFFFF);
        "&HFFFFFFFFFFFFFFFF".AsSpan().TryParseWithPrefix64().Should().Be(0xFFFFFFFFFFFFFFFF);
        "#FFFFFFFFFFFFFFFF".AsSpan().TryParseWithPrefix64().Should().Be(0xFFFFFFFFFFFFFFFF);
        "0b1111111111111111111111111111111111111111111111111111111111111111".AsSpan().TryParseWithPrefix64().Should().Be(0xFFFFFFFFFFFFFFFF);
        "18446744073709551616".AsSpan().TryParseWithPrefix64().Should().BeNull();
        "0x10000000000000000".AsSpan().TryParseWithPrefix64().Should().BeNull();
        "&H10000000000000000".AsSpan().TryParseWithPrefix64().Should().BeNull();
        "#10000000000000000".AsSpan().TryParseWithPrefix64().Should().BeNull();
        "0b10000000000000000000000000000000000000000000000000000000000000000".AsSpan().TryParseWithPrefix64().Should().BeNull();
        "a".AsSpan().TryParseWithPrefix64().Should().BeNull();
        "0xG".AsSpan().TryParseWithPrefix64().Should().BeNull();

        // Span
        "1234567890123456".ToArray().AsSpan().TryParseWithPrefix64().Should().Be(1234567890123456);
        "0x1234567890123456".ToArray().AsSpan().TryParseWithPrefix64().Should().Be(0x1234567890123456);
        "0X1234567890123456".ToArray().AsSpan().TryParseWithPrefix64().Should().Be(0x1234567890123456);
        "&h1234567890123456".ToArray().AsSpan().TryParseWithPrefix64().Should().Be(0x1234567890123456);
        "&H1234567890123456".ToArray().AsSpan().TryParseWithPrefix64().Should().Be(0x1234567890123456);
        "#1234567890123456".ToArray().AsSpan().TryParseWithPrefix64().Should().Be(0x1234567890123456);
        "0b0001001000110100010101100111100010010000000100100011010001010110".ToArray().AsSpan().TryParseWithPrefix64().Should().Be(0x1234567890123456);
        "18446744073709551615".ToArray().AsSpan().TryParseWithPrefix64().Should().Be(18446744073709551615);
        "0xFFFFFFFFFFFFFFFF".ToArray().AsSpan().TryParseWithPrefix64().Should().Be(0xFFFFFFFFFFFFFFFF);
        "&HFFFFFFFFFFFFFFFF".ToArray().AsSpan().TryParseWithPrefix64().Should().Be(0xFFFFFFFFFFFFFFFF);
        "#FFFFFFFFFFFFFFFF".ToArray().AsSpan().TryParseWithPrefix64().Should().Be(0xFFFFFFFFFFFFFFFF);
        "0b1111111111111111111111111111111111111111111111111111111111111111".ToArray().AsSpan().TryParseWithPrefix64().Should().Be(0xFFFFFFFFFFFFFFFF);
        "18446744073709551616".ToArray().AsSpan().TryParseWithPrefix64().Should().BeNull();
        "0x10000000000000000".ToArray().AsSpan().TryParseWithPrefix64().Should().BeNull();
        "&H10000000000000000".ToArray().AsSpan().TryParseWithPrefix64().Should().BeNull();
        "#10000000000000000".ToArray().AsSpan().TryParseWithPrefix64().Should().BeNull();
        "0b10000000000000000000000000000000000000000000000000000000000000000".ToArray().AsSpan().TryParseWithPrefix64().Should().BeNull();
        "a".ToArray().AsSpan().TryParseWithPrefix64().Should().BeNull();
        "0xG".ToArray().AsSpan().TryParseWithPrefix64().Should().BeNull();
    }
    #endregion

}
