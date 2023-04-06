using System.Globalization;
using System.Numerics;
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

    #region ParseNumber(.NET7)
    [TestMethod()]
    public void ParseNumber()
    {
        // string
        $"{Byte.MinValue}".ParseNumber<Byte>().Should().Be(Byte.MinValue);
        $"{UInt16.MinValue}".ParseNumber<UInt16>().Should().Be(UInt16.MinValue);
        $"{UInt32.MinValue}".ParseNumber<UInt32>().Should().Be(UInt32.MinValue);
        $"{UInt64.MinValue}".ParseNumber<UInt64>().Should().Be(UInt64.MinValue);
        $"{UInt128.MinValue}".ParseNumber<UInt128>().Should().Be(UInt128.MinValue);
        $"{SByte.MinValue}".ParseNumber<SByte>().Should().Be(SByte.MinValue);
        $"{Int16.MinValue}".ParseNumber<Int16>().Should().Be(Int16.MinValue);
        $"{Int32.MinValue}".ParseNumber<Int32>().Should().Be(Int32.MinValue);
        $"{Int64.MinValue}".ParseNumber<Int64>().Should().Be(Int64.MinValue);
        $"{Int128.MinValue}".ParseNumber<Int128>().Should().Be(Int128.MinValue);
        $"{Byte.MaxValue}".ParseNumber<Byte>().Should().Be(Byte.MaxValue);
        $"{UInt16.MaxValue}".ParseNumber<UInt16>().Should().Be(UInt16.MaxValue);
        $"{UInt32.MaxValue}".ParseNumber<UInt32>().Should().Be(UInt32.MaxValue);
        $"{UInt64.MaxValue}".ParseNumber<UInt64>().Should().Be(UInt64.MaxValue);
        $"{UInt128.MaxValue}".ParseNumber<UInt128>().Should().Be(UInt128.MaxValue);
        $"{SByte.MaxValue}".ParseNumber<SByte>().Should().Be(SByte.MaxValue);
        $"{Int16.MaxValue}".ParseNumber<Int16>().Should().Be(Int16.MaxValue);
        $"{Int32.MaxValue}".ParseNumber<Int32>().Should().Be(Int32.MaxValue);
        $"{Int64.MaxValue}".ParseNumber<Int64>().Should().Be(Int64.MaxValue);
        $"{Int128.MaxValue}".ParseNumber<Int128>().Should().Be(Int128.MaxValue);
        new Action(() => "-129".ParseNumber<SByte>()).Should().Throw<Exception>();
        new Action(() => "-32769".ParseNumber<Int16>()).Should().Throw<Exception>();
        new Action(() => "-2147483649".ParseNumber<Int32>()).Should().Throw<Exception>();
        new Action(() => "-9223372036854775809".ParseNumber<Int64>()).Should().Throw<Exception>();
        new Action(() => "-170141183460469231731687303715884105729".ParseNumber<UInt128>()).Should().Throw<Exception>();
        new Action(() => "256".ParseNumber<Byte>()).Should().Throw<Exception>();
        new Action(() => "65536".ParseNumber<UInt16>()).Should().Throw<Exception>();
        new Action(() => "4294967296".ParseNumber<UInt32>()).Should().Throw<Exception>();
        new Action(() => "18446744073709552000".ParseNumber<UInt64>()).Should().Throw<Exception>();
        new Action(() => "340282366920938463463374607431768211456".ParseNumber<UInt128>()).Should().Throw<Exception>();

        // ReadOnlySpan
        $"{Byte.MinValue}".AsSpan().ParseNumber<Byte>().Should().Be(Byte.MinValue);
        $"{UInt16.MinValue}".AsSpan().ParseNumber<UInt16>().Should().Be(UInt16.MinValue);
        $"{UInt32.MinValue}".AsSpan().ParseNumber<UInt32>().Should().Be(UInt32.MinValue);
        $"{UInt64.MinValue}".AsSpan().ParseNumber<UInt64>().Should().Be(UInt64.MinValue);
        $"{UInt128.MinValue}".AsSpan().ParseNumber<UInt128>().Should().Be(UInt128.MinValue);
        $"{SByte.MinValue}".AsSpan().ParseNumber<SByte>().Should().Be(SByte.MinValue);
        $"{Int16.MinValue}".AsSpan().ParseNumber<Int16>().Should().Be(Int16.MinValue);
        $"{Int32.MinValue}".AsSpan().ParseNumber<Int32>().Should().Be(Int32.MinValue);
        $"{Int64.MinValue}".AsSpan().ParseNumber<Int64>().Should().Be(Int64.MinValue);
        $"{Int128.MinValue}".AsSpan().ParseNumber<Int128>().Should().Be(Int128.MinValue);
        $"{Byte.MaxValue}".AsSpan().ParseNumber<Byte>().Should().Be(Byte.MaxValue);
        $"{UInt16.MaxValue}".AsSpan().ParseNumber<UInt16>().Should().Be(UInt16.MaxValue);
        $"{UInt32.MaxValue}".AsSpan().ParseNumber<UInt32>().Should().Be(UInt32.MaxValue);
        $"{UInt64.MaxValue}".AsSpan().ParseNumber<UInt64>().Should().Be(UInt64.MaxValue);
        $"{UInt128.MaxValue}".AsSpan().ParseNumber<UInt128>().Should().Be(UInt128.MaxValue);
        $"{SByte.MaxValue}".AsSpan().ParseNumber<SByte>().Should().Be(SByte.MaxValue);
        $"{Int16.MaxValue}".AsSpan().ParseNumber<Int16>().Should().Be(Int16.MaxValue);
        $"{Int32.MaxValue}".AsSpan().ParseNumber<Int32>().Should().Be(Int32.MaxValue);
        $"{Int64.MaxValue}".AsSpan().ParseNumber<Int64>().Should().Be(Int64.MaxValue);
        $"{Int128.MaxValue}".AsSpan().ParseNumber<Int128>().Should().Be(Int128.MaxValue);
        new Action(() => "-129".AsSpan().ParseNumber<SByte>()).Should().Throw<Exception>();
        new Action(() => "-32769".AsSpan().ParseNumber<Int16>()).Should().Throw<Exception>();
        new Action(() => "-2147483649".AsSpan().ParseNumber<Int32>()).Should().Throw<Exception>();
        new Action(() => "-9223372036854775809".AsSpan().ParseNumber<Int64>()).Should().Throw<Exception>();
        new Action(() => "-170141183460469231731687303715884105729".AsSpan().ParseNumber<UInt128>()).Should().Throw<Exception>();
        new Action(() => "256".AsSpan().ParseNumber<Byte>()).Should().Throw<Exception>();
        new Action(() => "65536".AsSpan().ParseNumber<UInt16>()).Should().Throw<Exception>();
        new Action(() => "4294967296".AsSpan().ParseNumber<UInt32>()).Should().Throw<Exception>();
        new Action(() => "18446744073709552000".AsSpan().ParseNumber<UInt64>()).Should().Throw<Exception>();
        new Action(() => "340282366920938463463374607431768211456".AsSpan().ParseNumber<UInt128>()).Should().Throw<Exception>();

        // Span
        $"{Byte.MinValue}".ToArray().AsSpan().ParseNumber<Byte>().Should().Be(Byte.MinValue);
        $"{UInt16.MinValue}".ToArray().AsSpan().ParseNumber<UInt16>().Should().Be(UInt16.MinValue);
        $"{UInt32.MinValue}".ToArray().AsSpan().ParseNumber<UInt32>().Should().Be(UInt32.MinValue);
        $"{UInt64.MinValue}".ToArray().AsSpan().ParseNumber<UInt64>().Should().Be(UInt64.MinValue);
        $"{UInt128.MinValue}".ToArray().AsSpan().ParseNumber<UInt128>().Should().Be(UInt128.MinValue);
        $"{SByte.MinValue}".ToArray().AsSpan().ParseNumber<SByte>().Should().Be(SByte.MinValue);
        $"{Int16.MinValue}".ToArray().AsSpan().ParseNumber<Int16>().Should().Be(Int16.MinValue);
        $"{Int32.MinValue}".ToArray().AsSpan().ParseNumber<Int32>().Should().Be(Int32.MinValue);
        $"{Int64.MinValue}".ToArray().AsSpan().ParseNumber<Int64>().Should().Be(Int64.MinValue);
        $"{Int128.MinValue}".ToArray().AsSpan().ParseNumber<Int128>().Should().Be(Int128.MinValue);
        $"{Byte.MaxValue}".ToArray().AsSpan().ParseNumber<Byte>().Should().Be(Byte.MaxValue);
        $"{UInt16.MaxValue}".ToArray().AsSpan().ParseNumber<UInt16>().Should().Be(UInt16.MaxValue);
        $"{UInt32.MaxValue}".ToArray().AsSpan().ParseNumber<UInt32>().Should().Be(UInt32.MaxValue);
        $"{UInt64.MaxValue}".ToArray().AsSpan().ParseNumber<UInt64>().Should().Be(UInt64.MaxValue);
        $"{UInt128.MaxValue}".ToArray().AsSpan().ParseNumber<UInt128>().Should().Be(UInt128.MaxValue);
        $"{SByte.MaxValue}".ToArray().AsSpan().ParseNumber<SByte>().Should().Be(SByte.MaxValue);
        $"{Int16.MaxValue}".ToArray().AsSpan().ParseNumber<Int16>().Should().Be(Int16.MaxValue);
        $"{Int32.MaxValue}".ToArray().AsSpan().ParseNumber<Int32>().Should().Be(Int32.MaxValue);
        $"{Int64.MaxValue}".ToArray().AsSpan().ParseNumber<Int64>().Should().Be(Int64.MaxValue);
        $"{Int128.MaxValue}".ToArray().AsSpan().ParseNumber<Int128>().Should().Be(Int128.MaxValue);
        new Action(() => "-129".ToArray().AsSpan().ParseNumber<SByte>()).Should().Throw<Exception>();
        new Action(() => "-32769".ToArray().AsSpan().ParseNumber<Int16>()).Should().Throw<Exception>();
        new Action(() => "-2147483649".ToArray().AsSpan().ParseNumber<Int32>()).Should().Throw<Exception>();
        new Action(() => "-9223372036854775809".ToArray().AsSpan().ParseNumber<Int64>()).Should().Throw<Exception>();
        new Action(() => "-170141183460469231731687303715884105729".ToArray().AsSpan().ParseNumber<UInt128>()).Should().Throw<Exception>();
        new Action(() => "256".ToArray().AsSpan().ParseNumber<Byte>()).Should().Throw<Exception>();
        new Action(() => "65536".ToArray().AsSpan().ParseNumber<UInt16>()).Should().Throw<Exception>();
        new Action(() => "4294967296".ToArray().AsSpan().ParseNumber<UInt32>()).Should().Throw<Exception>();
        new Action(() => "18446744073709552000".ToArray().AsSpan().ParseNumber<UInt64>()).Should().Throw<Exception>();
        new Action(() => "340282366920938463463374607431768211456".ToArray().AsSpan().ParseNumber<UInt128>()).Should().Throw<Exception>();
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

    #region TryParseNumber(.NET7)
    [TestMethod()]
    public void TryParseNumber()
    {
        // string
        $"{Byte.MinValue}".TryParseNumber<Byte>().Should().Be(Byte.MinValue);
        $"{UInt16.MinValue}".TryParseNumber<UInt16>().Should().Be(UInt16.MinValue);
        $"{UInt32.MinValue}".TryParseNumber<UInt32>().Should().Be(UInt32.MinValue);
        $"{UInt64.MinValue}".TryParseNumber<UInt64>().Should().Be(UInt64.MinValue);
        $"{UInt128.MinValue}".TryParseNumber<UInt128>().Should().Be(UInt128.MinValue);
        $"{SByte.MinValue}".TryParseNumber<SByte>().Should().Be(SByte.MinValue);
        $"{Int16.MinValue}".TryParseNumber<Int16>().Should().Be(Int16.MinValue);
        $"{Int32.MinValue}".TryParseNumber<Int32>().Should().Be(Int32.MinValue);
        $"{Int64.MinValue}".TryParseNumber<Int64>().Should().Be(Int64.MinValue);
        $"{Int128.MinValue}".TryParseNumber<Int128>().Should().Be(Int128.MinValue);
        $"{Byte.MaxValue}".TryParseNumber<Byte>().Should().Be(Byte.MaxValue);
        $"{UInt16.MaxValue}".TryParseNumber<UInt16>().Should().Be(UInt16.MaxValue);
        $"{UInt32.MaxValue}".TryParseNumber<UInt32>().Should().Be(UInt32.MaxValue);
        $"{UInt64.MaxValue}".TryParseNumber<UInt64>().Should().Be(UInt64.MaxValue);
        $"{UInt128.MaxValue}".TryParseNumber<UInt128>().Should().Be(UInt128.MaxValue);
        $"{SByte.MaxValue}".TryParseNumber<SByte>().Should().Be(SByte.MaxValue);
        $"{Int16.MaxValue}".TryParseNumber<Int16>().Should().Be(Int16.MaxValue);
        $"{Int32.MaxValue}".TryParseNumber<Int32>().Should().Be(Int32.MaxValue);
        $"{Int64.MaxValue}".TryParseNumber<Int64>().Should().Be(Int64.MaxValue);
        $"{Int128.MaxValue}".TryParseNumber<Int128>().Should().Be(Int128.MaxValue);
        "-129".TryParseNumber<SByte>().Should().BeNull();
        "-32769".TryParseNumber<Int16>().Should().BeNull();
        "-2147483649".TryParseNumber<Int32>().Should().BeNull();
        "-9223372036854775809".TryParseNumber<Int64>().Should().BeNull();
        "-170141183460469231731687303715884105729".TryParseNumber<UInt128>().Should().BeNull();
        "256".TryParseNumber<Byte>().Should().BeNull();
        "65536".TryParseNumber<UInt16>().Should().BeNull();
        "4294967296".TryParseNumber<UInt32>().Should().BeNull();
        "18446744073709552000".TryParseNumber<UInt64>().Should().BeNull();
        "340282366920938463463374607431768211456".TryParseNumber<UInt128>().Should().BeNull();

        // ReadOnlySpan
        $"{Byte.MinValue}".AsSpan().TryParseNumber<Byte>().Should().Be(Byte.MinValue);
        $"{UInt16.MinValue}".AsSpan().TryParseNumber<UInt16>().Should().Be(UInt16.MinValue);
        $"{UInt32.MinValue}".AsSpan().TryParseNumber<UInt32>().Should().Be(UInt32.MinValue);
        $"{UInt64.MinValue}".AsSpan().TryParseNumber<UInt64>().Should().Be(UInt64.MinValue);
        $"{UInt128.MinValue}".AsSpan().TryParseNumber<UInt128>().Should().Be(UInt128.MinValue);
        $"{SByte.MinValue}".AsSpan().TryParseNumber<SByte>().Should().Be(SByte.MinValue);
        $"{Int16.MinValue}".AsSpan().TryParseNumber<Int16>().Should().Be(Int16.MinValue);
        $"{Int32.MinValue}".AsSpan().TryParseNumber<Int32>().Should().Be(Int32.MinValue);
        $"{Int64.MinValue}".AsSpan().TryParseNumber<Int64>().Should().Be(Int64.MinValue);
        $"{Int128.MinValue}".AsSpan().TryParseNumber<Int128>().Should().Be(Int128.MinValue);
        $"{Byte.MaxValue}".AsSpan().TryParseNumber<Byte>().Should().Be(Byte.MaxValue);
        $"{UInt16.MaxValue}".AsSpan().TryParseNumber<UInt16>().Should().Be(UInt16.MaxValue);
        $"{UInt32.MaxValue}".AsSpan().TryParseNumber<UInt32>().Should().Be(UInt32.MaxValue);
        $"{UInt64.MaxValue}".AsSpan().TryParseNumber<UInt64>().Should().Be(UInt64.MaxValue);
        $"{UInt128.MaxValue}".AsSpan().TryParseNumber<UInt128>().Should().Be(UInt128.MaxValue);
        $"{SByte.MaxValue}".AsSpan().TryParseNumber<SByte>().Should().Be(SByte.MaxValue);
        $"{Int16.MaxValue}".AsSpan().TryParseNumber<Int16>().Should().Be(Int16.MaxValue);
        $"{Int32.MaxValue}".AsSpan().TryParseNumber<Int32>().Should().Be(Int32.MaxValue);
        $"{Int64.MaxValue}".AsSpan().TryParseNumber<Int64>().Should().Be(Int64.MaxValue);
        $"{Int128.MaxValue}".AsSpan().TryParseNumber<Int128>().Should().Be(Int128.MaxValue);
        "-129".AsSpan().TryParseNumber<SByte>().Should().BeNull();
        "-32769".AsSpan().TryParseNumber<Int16>().Should().BeNull();
        "-2147483649".AsSpan().TryParseNumber<Int32>().Should().BeNull();
        "-9223372036854775809".AsSpan().TryParseNumber<Int64>().Should().BeNull();
        "-170141183460469231731687303715884105729".AsSpan().TryParseNumber<UInt128>().Should().BeNull();
        "256".AsSpan().TryParseNumber<Byte>().Should().BeNull();
        "65536".AsSpan().TryParseNumber<UInt16>().Should().BeNull();
        "4294967296".AsSpan().TryParseNumber<UInt32>().Should().BeNull();
        "18446744073709552000".AsSpan().TryParseNumber<UInt64>().Should().BeNull();
        "340282366920938463463374607431768211456".AsSpan().TryParseNumber<UInt128>().Should().BeNull();

        // Span
        $"{Byte.MinValue}".ToArray().AsSpan().TryParseNumber<Byte>().Should().Be(Byte.MinValue);
        $"{UInt16.MinValue}".ToArray().AsSpan().TryParseNumber<UInt16>().Should().Be(UInt16.MinValue);
        $"{UInt32.MinValue}".ToArray().AsSpan().TryParseNumber<UInt32>().Should().Be(UInt32.MinValue);
        $"{UInt64.MinValue}".ToArray().AsSpan().TryParseNumber<UInt64>().Should().Be(UInt64.MinValue);
        $"{UInt128.MinValue}".ToArray().AsSpan().TryParseNumber<UInt128>().Should().Be(UInt128.MinValue);
        $"{SByte.MinValue}".ToArray().AsSpan().TryParseNumber<SByte>().Should().Be(SByte.MinValue);
        $"{Int16.MinValue}".ToArray().AsSpan().TryParseNumber<Int16>().Should().Be(Int16.MinValue);
        $"{Int32.MinValue}".ToArray().AsSpan().TryParseNumber<Int32>().Should().Be(Int32.MinValue);
        $"{Int64.MinValue}".ToArray().AsSpan().TryParseNumber<Int64>().Should().Be(Int64.MinValue);
        $"{Int128.MinValue}".ToArray().AsSpan().TryParseNumber<Int128>().Should().Be(Int128.MinValue);
        $"{Byte.MaxValue}".ToArray().AsSpan().TryParseNumber<Byte>().Should().Be(Byte.MaxValue);
        $"{UInt16.MaxValue}".ToArray().AsSpan().TryParseNumber<UInt16>().Should().Be(UInt16.MaxValue);
        $"{UInt32.MaxValue}".ToArray().AsSpan().TryParseNumber<UInt32>().Should().Be(UInt32.MaxValue);
        $"{UInt64.MaxValue}".ToArray().AsSpan().TryParseNumber<UInt64>().Should().Be(UInt64.MaxValue);
        $"{UInt128.MaxValue}".ToArray().AsSpan().TryParseNumber<UInt128>().Should().Be(UInt128.MaxValue);
        $"{SByte.MaxValue}".ToArray().AsSpan().TryParseNumber<SByte>().Should().Be(SByte.MaxValue);
        $"{Int16.MaxValue}".ToArray().AsSpan().TryParseNumber<Int16>().Should().Be(Int16.MaxValue);
        $"{Int32.MaxValue}".ToArray().AsSpan().TryParseNumber<Int32>().Should().Be(Int32.MaxValue);
        $"{Int64.MaxValue}".ToArray().AsSpan().TryParseNumber<Int64>().Should().Be(Int64.MaxValue);
        $"{Int128.MaxValue}".ToArray().AsSpan().TryParseNumber<Int128>().Should().Be(Int128.MaxValue);
        "-129".ToArray().AsSpan().TryParseNumber<SByte>().Should().BeNull();
        "-32769".ToArray().AsSpan().TryParseNumber<Int16>().Should().BeNull();
        "-2147483649".ToArray().AsSpan().TryParseNumber<Int32>().Should().BeNull();
        "-9223372036854775809".ToArray().AsSpan().TryParseNumber<Int64>().Should().BeNull();
        "-170141183460469231731687303715884105729".ToArray().AsSpan().TryParseNumber<UInt128>().Should().BeNull();
        "256".ToArray().AsSpan().TryParseNumber<Byte>().Should().BeNull();
        "65536".ToArray().AsSpan().TryParseNumber<UInt16>().Should().BeNull();
        "4294967296".ToArray().AsSpan().TryParseNumber<UInt32>().Should().BeNull();
        "18446744073709552000".ToArray().AsSpan().TryParseNumber<UInt64>().Should().BeNull();
        "340282366920938463463374607431768211456".ToArray().AsSpan().TryParseNumber<UInt128>().Should().BeNull();
    }
    #endregion

    #region TryParseHexNumber
    [TestMethod()]
    public void TryParseHexNumber8()
    {
        // string
        "0".TryParseHexNumber8().Should().Be(0);
        "80".TryParseHexNumber8().Should().Be(0x80);
        "FF".TryParseHexNumber8().Should().Be(0xFF);
        "100".TryParseHexNumber8().Should().BeNull();

        // ReadOnlySpan
        "0".AsSpan().TryParseHexNumber8().Should().Be(0);
        "80".AsSpan().TryParseHexNumber8().Should().Be(0x80);
        "FF".AsSpan().TryParseHexNumber8().Should().Be(0xFF);
        "100".AsSpan().TryParseHexNumber8().Should().BeNull();

        // Span
        "0".ToArray().AsSpan().TryParseHexNumber8().Should().Be(0);
        "80".ToArray().AsSpan().TryParseHexNumber8().Should().Be(0x80);
        "FF".ToArray().AsSpan().TryParseHexNumber8().Should().Be(0xFF);
        "100".ToArray().AsSpan().TryParseHexNumber8().Should().BeNull();

    }

    [TestMethod()]
    public void TryParseHexNumber16()
    {
        // string
        "0".TryParseHexNumber16().Should().Be(0);
        "8000".TryParseHexNumber16().Should().Be(0x8000);
        "FFFF".TryParseHexNumber16().Should().Be(0xFFFF);
        "10000".TryParseHexNumber16().Should().BeNull();

        // ReadOnlySpan
        "0".AsSpan().TryParseHexNumber16().Should().Be(0);
        "8000".AsSpan().TryParseHexNumber16().Should().Be(0x8000);
        "FFFF".AsSpan().TryParseHexNumber16().Should().Be(0xFFFF);
        "10000".AsSpan().TryParseHexNumber16().Should().BeNull();

        // Span
        "0".ToArray().AsSpan().TryParseHexNumber16().Should().Be(0);
        "8000".ToArray().AsSpan().TryParseHexNumber16().Should().Be(0x8000);
        "FFFF".ToArray().AsSpan().TryParseHexNumber16().Should().Be(0xFFFF);
        "10000".ToArray().AsSpan().TryParseHexNumber16().Should().BeNull();
    }

    [TestMethod()]
    public void TryParseHexNumber32()
    {
        // string
        "0".TryParseHexNumber32().Should().Be(0);
        "80000000".TryParseHexNumber32().Should().Be(0x80000000);
        "FFFFFFFF".TryParseHexNumber32().Should().Be(0xFFFFFFFF);
        "100000000".TryParseHexNumber32().Should().BeNull();

        // ReadOnlySpan
        "0".AsSpan().TryParseHexNumber32().Should().Be(0);
        "80000000".AsSpan().TryParseHexNumber32().Should().Be(0x80000000);
        "FFFFFFFF".AsSpan().TryParseHexNumber32().Should().Be(0xFFFFFFFF);
        "100000000".AsSpan().TryParseHexNumber32().Should().BeNull();

        // Span
        "0".ToArray().AsSpan().TryParseHexNumber32().Should().Be(0);
        "80000000".ToArray().AsSpan().TryParseHexNumber32().Should().Be(0x80000000);
        "FFFFFFFF".ToArray().AsSpan().TryParseHexNumber32().Should().Be(0xFFFFFFFF);
        "100000000".ToArray().AsSpan().TryParseHexNumber32().Should().BeNull();
    }

    [TestMethod()]
    public void TryParseHexNumber64()
    {
        // string
        "0".TryParseHexNumber64().Should().Be(0);
        "8000000000000000".TryParseHexNumber64().Should().Be(0x8000000000000000);
        "FFFFFFFFFFFFFFFF".TryParseHexNumber64().Should().Be(0xFFFFFFFFFFFFFFFF);
        "10000000000000000".TryParseHexNumber64().Should().BeNull();

        // ReadOnlySpan
        "0".AsSpan().TryParseHexNumber64().Should().Be(0);
        "8000000000000000".AsSpan().TryParseHexNumber64().Should().Be(0x8000000000000000);
        "FFFFFFFFFFFFFFFF".AsSpan().TryParseHexNumber64().Should().Be(0xFFFFFFFFFFFFFFFF);
        "10000000000000000".AsSpan().TryParseHexNumber64().Should().BeNull();

        // Span
        "0".ToArray().AsSpan().TryParseHexNumber64().Should().Be(0);
        "8000000000000000".ToArray().AsSpan().TryParseHexNumber64().Should().Be(0x8000000000000000);
        "FFFFFFFFFFFFFFFF".ToArray().AsSpan().TryParseHexNumber64().Should().Be(0xFFFFFFFFFFFFFFFF);
        "10000000000000000".ToArray().AsSpan().TryParseHexNumber64().Should().BeNull();
    }
    #endregion

    #region TryParseHexNumber(.NET7)
    [TestMethod()]
    public void TryParseHexNumber()
    {
        // string
        $"{Byte.MinValue:X}".TryParseHexNumber<Byte>().Should().Be(Byte.MinValue);
        $"{UInt16.MinValue:X}".TryParseHexNumber<UInt16>().Should().Be(UInt16.MinValue);
        $"{UInt32.MinValue:X}".TryParseHexNumber<UInt32>().Should().Be(UInt32.MinValue);
        $"{UInt64.MinValue:X}".TryParseHexNumber<UInt64>().Should().Be(UInt64.MinValue);
        $"{UInt128.MinValue:X}".TryParseHexNumber<UInt128>().Should().Be(UInt128.MinValue);
        $"{SByte.MinValue:X}".TryParseHexNumber<SByte>().Should().Be(SByte.MinValue);
        $"{Int16.MinValue:X}".TryParseHexNumber<Int16>().Should().Be(Int16.MinValue);
        $"{Int32.MinValue:X}".TryParseHexNumber<Int32>().Should().Be(Int32.MinValue);
        $"{Int64.MinValue:X}".TryParseHexNumber<Int64>().Should().Be(Int64.MinValue);
        $"{Int128.MinValue:X}".TryParseHexNumber<Int128>().Should().Be(Int128.MinValue);
        $"{Byte.MaxValue:X}".TryParseHexNumber<Byte>().Should().Be(Byte.MaxValue);
        $"{UInt16.MaxValue:X}".TryParseHexNumber<UInt16>().Should().Be(UInt16.MaxValue);
        $"{UInt32.MaxValue:X}".TryParseHexNumber<UInt32>().Should().Be(UInt32.MaxValue);
        $"{UInt64.MaxValue:X}".TryParseHexNumber<UInt64>().Should().Be(UInt64.MaxValue);
        $"{UInt128.MaxValue:X}".TryParseHexNumber<UInt128>().Should().Be(UInt128.MaxValue);
        $"{SByte.MaxValue:X}".TryParseHexNumber<SByte>().Should().Be(SByte.MaxValue);
        $"{Int16.MaxValue:X}".TryParseHexNumber<Int16>().Should().Be(Int16.MaxValue);
        $"{Int32.MaxValue:X}".TryParseHexNumber<Int32>().Should().Be(Int32.MaxValue);
        $"{Int64.MaxValue:X}".TryParseHexNumber<Int64>().Should().Be(Int64.MaxValue);
        $"{Int128.MaxValue:X}".TryParseHexNumber<Int128>().Should().Be(Int128.MaxValue);
        $"1{0:X2}".TryParseHexNumber<SByte>().Should().BeNull();
        $"1{0:X4}".TryParseHexNumber<Int16>().Should().BeNull();
        $"1{0:X8}".TryParseHexNumber<Int32>().Should().BeNull();
        $"1{0:X16}".TryParseHexNumber<Int64>().Should().BeNull();
        $"1{0:X32}".TryParseHexNumber<Int128>().Should().BeNull();
        $"1{0:X2}".TryParseHexNumber<Byte>().Should().BeNull();
        $"1{0:X4}".TryParseHexNumber<UInt16>().Should().BeNull();
        $"1{0:X8}".TryParseHexNumber<UInt32>().Should().BeNull();
        $"1{0:X16}".TryParseHexNumber<UInt64>().Should().BeNull();
        $"1{0:X32}".TryParseHexNumber<UInt128>().Should().BeNull();

        // ReadOnlySpan
        $"{Byte.MinValue:X}".AsSpan().TryParseHexNumber<Byte>().Should().Be(Byte.MinValue);
        $"{UInt16.MinValue:X}".AsSpan().TryParseHexNumber<UInt16>().Should().Be(UInt16.MinValue);
        $"{UInt32.MinValue:X}".AsSpan().TryParseHexNumber<UInt32>().Should().Be(UInt32.MinValue);
        $"{UInt64.MinValue:X}".AsSpan().TryParseHexNumber<UInt64>().Should().Be(UInt64.MinValue);
        $"{UInt128.MinValue:X}".AsSpan().TryParseHexNumber<UInt128>().Should().Be(UInt128.MinValue);
        $"{SByte.MinValue:X}".AsSpan().TryParseHexNumber<SByte>().Should().Be(SByte.MinValue);
        $"{Int16.MinValue:X}".AsSpan().TryParseHexNumber<Int16>().Should().Be(Int16.MinValue);
        $"{Int32.MinValue:X}".AsSpan().TryParseHexNumber<Int32>().Should().Be(Int32.MinValue);
        $"{Int64.MinValue:X}".AsSpan().TryParseHexNumber<Int64>().Should().Be(Int64.MinValue);
        $"{Int128.MinValue:X}".AsSpan().TryParseHexNumber<Int128>().Should().Be(Int128.MinValue);
        $"{Byte.MaxValue:X}".AsSpan().TryParseHexNumber<Byte>().Should().Be(Byte.MaxValue);
        $"{UInt16.MaxValue:X}".AsSpan().TryParseHexNumber<UInt16>().Should().Be(UInt16.MaxValue);
        $"{UInt32.MaxValue:X}".AsSpan().TryParseHexNumber<UInt32>().Should().Be(UInt32.MaxValue);
        $"{UInt64.MaxValue:X}".AsSpan().TryParseHexNumber<UInt64>().Should().Be(UInt64.MaxValue);
        $"{UInt128.MaxValue:X}".AsSpan().TryParseHexNumber<UInt128>().Should().Be(UInt128.MaxValue);
        $"{SByte.MaxValue:X}".AsSpan().TryParseHexNumber<SByte>().Should().Be(SByte.MaxValue);
        $"{Int16.MaxValue:X}".AsSpan().TryParseHexNumber<Int16>().Should().Be(Int16.MaxValue);
        $"{Int32.MaxValue:X}".AsSpan().TryParseHexNumber<Int32>().Should().Be(Int32.MaxValue);
        $"{Int64.MaxValue:X}".AsSpan().TryParseHexNumber<Int64>().Should().Be(Int64.MaxValue);
        $"{Int128.MaxValue:X}".AsSpan().TryParseHexNumber<Int128>().Should().Be(Int128.MaxValue);
        $"1{0:X2}".AsSpan().TryParseHexNumber<SByte>().Should().BeNull();
        $"1{0:X4}".AsSpan().TryParseHexNumber<Int16>().Should().BeNull();
        $"1{0:X8}".AsSpan().TryParseHexNumber<Int32>().Should().BeNull();
        $"1{0:X16}".AsSpan().TryParseHexNumber<Int64>().Should().BeNull();
        $"1{0:X32}".AsSpan().TryParseHexNumber<Int128>().Should().BeNull();
        $"1{0:X2}".AsSpan().TryParseHexNumber<Byte>().Should().BeNull();
        $"1{0:X4}".AsSpan().TryParseHexNumber<UInt16>().Should().BeNull();
        $"1{0:X8}".AsSpan().TryParseHexNumber<UInt32>().Should().BeNull();
        $"1{0:X16}".AsSpan().TryParseHexNumber<UInt64>().Should().BeNull();
        $"1{0:X32}".AsSpan().TryParseHexNumber<UInt128>().Should().BeNull();

        // Span
        $"{Byte.MinValue:X}".ToArray().AsSpan().TryParseHexNumber<Byte>().Should().Be(Byte.MinValue);
        $"{UInt16.MinValue:X}".ToArray().AsSpan().TryParseHexNumber<UInt16>().Should().Be(UInt16.MinValue);
        $"{UInt32.MinValue:X}".ToArray().AsSpan().TryParseHexNumber<UInt32>().Should().Be(UInt32.MinValue);
        $"{UInt64.MinValue:X}".ToArray().AsSpan().TryParseHexNumber<UInt64>().Should().Be(UInt64.MinValue);
        $"{UInt128.MinValue:X}".ToArray().AsSpan().TryParseHexNumber<UInt128>().Should().Be(UInt128.MinValue);
        $"{SByte.MinValue:X}".ToArray().AsSpan().TryParseHexNumber<SByte>().Should().Be(SByte.MinValue);
        $"{Int16.MinValue:X}".ToArray().AsSpan().TryParseHexNumber<Int16>().Should().Be(Int16.MinValue);
        $"{Int32.MinValue:X}".ToArray().AsSpan().TryParseHexNumber<Int32>().Should().Be(Int32.MinValue);
        $"{Int64.MinValue:X}".ToArray().AsSpan().TryParseHexNumber<Int64>().Should().Be(Int64.MinValue);
        $"{Int128.MinValue:X}".ToArray().AsSpan().TryParseHexNumber<Int128>().Should().Be(Int128.MinValue);
        $"{Byte.MaxValue:X}".ToArray().AsSpan().TryParseHexNumber<Byte>().Should().Be(Byte.MaxValue);
        $"{UInt16.MaxValue:X}".ToArray().AsSpan().TryParseHexNumber<UInt16>().Should().Be(UInt16.MaxValue);
        $"{UInt32.MaxValue:X}".ToArray().AsSpan().TryParseHexNumber<UInt32>().Should().Be(UInt32.MaxValue);
        $"{UInt64.MaxValue:X}".ToArray().AsSpan().TryParseHexNumber<UInt64>().Should().Be(UInt64.MaxValue);
        $"{UInt128.MaxValue:X}".ToArray().AsSpan().TryParseHexNumber<UInt128>().Should().Be(UInt128.MaxValue);
        $"{SByte.MaxValue:X}".ToArray().AsSpan().TryParseHexNumber<SByte>().Should().Be(SByte.MaxValue);
        $"{Int16.MaxValue:X}".ToArray().AsSpan().TryParseHexNumber<Int16>().Should().Be(Int16.MaxValue);
        $"{Int32.MaxValue:X}".ToArray().AsSpan().TryParseHexNumber<Int32>().Should().Be(Int32.MaxValue);
        $"{Int64.MaxValue:X}".ToArray().AsSpan().TryParseHexNumber<Int64>().Should().Be(Int64.MaxValue);
        $"{Int128.MaxValue:X}".ToArray().AsSpan().TryParseHexNumber<Int128>().Should().Be(Int128.MaxValue);
        $"1{0:X2}".ToArray().AsSpan().TryParseHexNumber<SByte>().Should().BeNull();
        $"1{0:X4}".ToArray().AsSpan().TryParseHexNumber<Int16>().Should().BeNull();
        $"1{0:X8}".ToArray().AsSpan().TryParseHexNumber<Int32>().Should().BeNull();
        $"1{0:X16}".ToArray().AsSpan().TryParseHexNumber<Int64>().Should().BeNull();
        $"1{0:X32}".ToArray().AsSpan().TryParseHexNumber<Int128>().Should().BeNull();
        $"1{0:X2}".ToArray().AsSpan().TryParseHexNumber<Byte>().Should().BeNull();
        $"1{0:X4}".ToArray().AsSpan().TryParseHexNumber<UInt16>().Should().BeNull();
        $"1{0:X8}".ToArray().AsSpan().TryParseHexNumber<UInt32>().Should().BeNull();
        $"1{0:X16}".ToArray().AsSpan().TryParseHexNumber<UInt64>().Should().BeNull();
        $"1{0:X32}".ToArray().AsSpan().TryParseHexNumber<UInt128>().Should().BeNull();
    }
    #endregion

    #region TryParseHex(.NET7)
    [TestMethod()]
    public void TryParseHex()
    {
        // string
        "123".TryParseHex<ushort>().Should().Be(0x123);
        "0x123".TryParseHex<ushort>().Should().Be(0x123);
        "0X123".TryParseHex<ushort>().Should().Be(0x123);
        "&h123".TryParseHex<ushort>().Should().Be(0x123);
        "&H123".TryParseHex<ushort>().Should().Be(0x123);
        "#123".TryParseHex<ushort>().Should().Be(0x123);
        "123h".TryParseHex<ushort>().Should().Be(0x123);
        "123H".TryParseHex<ushort>().Should().Be(0x123);
        "X123".TryParseHex<ushort>().Should().BeNull();

        // ReadOnlySpan
        "123".AsSpan().TryParseHex<ushort>().Should().Be(0x123);
        "0x123".AsSpan().TryParseHex<ushort>().Should().Be(0x123);
        "0X123".AsSpan().TryParseHex<ushort>().Should().Be(0x123);
        "&h123".AsSpan().TryParseHex<ushort>().Should().Be(0x123);
        "&H123".AsSpan().TryParseHex<ushort>().Should().Be(0x123);
        "#123".AsSpan().TryParseHex<ushort>().Should().Be(0x123);
        "123h".AsSpan().TryParseHex<ushort>().Should().Be(0x123);
        "123H".AsSpan().TryParseHex<ushort>().Should().Be(0x123);
        "X123".AsSpan().TryParseHex<ushort>().Should().BeNull();

        // Span
        "123".ToArray().AsSpan().TryParseHex<ushort>().Should().Be(0x123);
        "0x123".ToArray().AsSpan().TryParseHex<ushort>().Should().Be(0x123);
        "0X123".ToArray().AsSpan().TryParseHex<ushort>().Should().Be(0x123);
        "&h123".ToArray().AsSpan().TryParseHex<ushort>().Should().Be(0x123);
        "&H123".ToArray().AsSpan().TryParseHex<ushort>().Should().Be(0x123);
        "#123".ToArray().AsSpan().TryParseHex<ushort>().Should().Be(0x123);
        "123h".ToArray().AsSpan().TryParseHex<ushort>().Should().Be(0x123);
        "123H".ToArray().AsSpan().TryParseHex<ushort>().Should().Be(0x123);
        "X123".ToArray().AsSpan().TryParseHex<ushort>().Should().BeNull();
    }
    #endregion

    #region TryParseBinNumber
    [TestMethod()]
    public void TryParseBinNumber8()
    {
        // string
        "0".TryParseBinNumber8(trim: true, snake: true).Should().Be(0);
        "1".TryParseBinNumber8(trim: true, snake: true).Should().Be(1);
        "10".TryParseBinNumber8(trim: true, snake: true).Should().Be(2);
        "11111111".TryParseBinNumber8(trim: true, snake: true).Should().Be(0xFF);
        "100000000".TryParseBinNumber8(trim: true, snake: true).Should().BeNull();

        // ReadOnlySpan
        "0".AsSpan().TryParseBinNumber8(trim: true, snake: true).Should().Be(0);
        "1".AsSpan().TryParseBinNumber8(trim: true, snake: true).Should().Be(1);
        "10".AsSpan().TryParseBinNumber8(trim: true, snake: true).Should().Be(2);
        "11111111".AsSpan().TryParseBinNumber8(trim: true, snake: true).Should().Be(0xFF);
        "100000000".AsSpan().TryParseBinNumber8(trim: true, snake: true).Should().BeNull();

        // Span
        "0".ToArray().AsSpan().TryParseBinNumber8(trim: true, snake: true).Should().Be(0);
        "1".ToArray().AsSpan().TryParseBinNumber8(trim: true, snake: true).Should().Be(1);
        "10".ToArray().AsSpan().TryParseBinNumber8(trim: true, snake: true).Should().Be(2);
        "11111111".ToArray().AsSpan().TryParseBinNumber8(trim: true, snake: true).Should().Be(0xFF);
        "100000000".ToArray().AsSpan().TryParseBinNumber8(trim: true, snake: true).Should().BeNull();

        // parameters
        "   1   ".AsSpan().TryParseBinNumber8(trim: true, snake: true).Should().Be(1);
        "   1   ".AsSpan().TryParseBinNumber8(trim: false, snake: true).Should().BeNull();
        "1111_1111".AsSpan().TryParseBinNumber8(trim: true, snake: true).Should().Be(0xFF);
        "1111_1111".AsSpan().TryParseBinNumber8(trim: true, snake: false).Should().BeNull();
    }

    [TestMethod()]
    public void TryParseBinNumber16()
    {
        // string
        "0".TryParseBinNumber16(trim: true, snake: true).Should().Be(0);
        "1".TryParseBinNumber16(trim: true, snake: true).Should().Be(1);
        "10".TryParseBinNumber16(trim: true, snake: true).Should().Be(2);
        "1111111111111111".TryParseBinNumber16(trim: true, snake: true).Should().Be(0xFFFF);
        "10000000000000000".TryParseBinNumber16(trim: true, snake: true).Should().BeNull();

        // ReadOnlySpan
        "0".AsSpan().TryParseBinNumber16(trim: true, snake: true).Should().Be(0);
        "1".AsSpan().TryParseBinNumber16(trim: true, snake: true).Should().Be(1);
        "10".AsSpan().TryParseBinNumber16(trim: true, snake: true).Should().Be(2);
        "1111111111111111".AsSpan().TryParseBinNumber16(trim: true, snake: true).Should().Be(0xFFFF);
        "10000000000000000".AsSpan().TryParseBinNumber16(trim: true, snake: true).Should().BeNull();

        // Span
        "0".ToArray().AsSpan().TryParseBinNumber16(trim: true, snake: true).Should().Be(0);
        "1".ToArray().AsSpan().TryParseBinNumber16(trim: true, snake: true).Should().Be(1);
        "10".ToArray().AsSpan().TryParseBinNumber16(trim: true, snake: true).Should().Be(2);
        "1111111111111111".ToArray().AsSpan().TryParseBinNumber16(trim: true, snake: true).Should().Be(0xFFFF);
        "10000000000000000".ToArray().AsSpan().TryParseBinNumber16(trim: true, snake: true).Should().BeNull();

        // parameters
        "   1   ".AsSpan().TryParseBinNumber16(trim: true, snake: true).Should().Be(1);
        "   1   ".AsSpan().TryParseBinNumber16(trim: false, snake: true).Should().BeNull();
        "11111111_11111111".AsSpan().TryParseBinNumber16(trim: true, snake: true).Should().Be(0xFFFF);
        "11111111_11111111".AsSpan().TryParseBinNumber16(trim: true, snake: false).Should().BeNull();
    }

    [TestMethod()]
    public void TryParseBinNumber32()
    {
        // string
        "0".TryParseBinNumber32(trim: true, snake: true).Should().Be(0);
        "1".TryParseBinNumber32(trim: true, snake: true).Should().Be(1);
        "10".TryParseBinNumber32(trim: true, snake: true).Should().Be(2);
        "11111111111111111111111111111111".TryParseBinNumber32(trim: true, snake: true).Should().Be(0xFFFFFFFF);
        "100000000000000000000000000000000".TryParseBinNumber32(trim: true, snake: true).Should().BeNull();

        // ReadOnlySpan
        "0".AsSpan().TryParseBinNumber32(trim: true, snake: true).Should().Be(0);
        "1".AsSpan().TryParseBinNumber32(trim: true, snake: true).Should().Be(1);
        "10".AsSpan().TryParseBinNumber32(trim: true, snake: true).Should().Be(2);
        "11111111111111111111111111111111".AsSpan().TryParseBinNumber32(trim: true, snake: true).Should().Be(0xFFFFFFFF);
        "100000000000000000000000000000000".AsSpan().TryParseBinNumber32(trim: true, snake: true).Should().BeNull();

        // Span
        "0".ToArray().AsSpan().TryParseBinNumber32(trim: true, snake: true).Should().Be(0);
        "1".ToArray().AsSpan().TryParseBinNumber32(trim: true, snake: true).Should().Be(1);
        "10".ToArray().AsSpan().TryParseBinNumber32(trim: true, snake: true).Should().Be(2);
        "11111111111111111111111111111111".ToArray().AsSpan().TryParseBinNumber32(trim: true, snake: true).Should().Be(0xFFFFFFFF);
        "100000000000000000000000000000000".ToArray().AsSpan().TryParseBinNumber32(trim: true, snake: true).Should().BeNull();

        // parameters
        "   1   ".AsSpan().TryParseBinNumber32(trim: true, snake: true).Should().Be(1);
        "   1   ".AsSpan().TryParseBinNumber32(trim: false, snake: true).Should().BeNull();
        "11111111_11111111_11111111_11111111".AsSpan().TryParseBinNumber32(trim: true, snake: true).Should().Be(0xFFFFFFFF);
        "11111111_11111111_11111111_11111111".AsSpan().TryParseBinNumber32(trim: true, snake: false).Should().BeNull();
    }

    [TestMethod()]
    public void TryParseBinNumber64()
    {
        // string
        "0".TryParseBinNumber64(trim: true, snake: true).Should().Be(0);
        "1".TryParseBinNumber64(trim: true, snake: true).Should().Be(1);
        "10".TryParseBinNumber64(trim: true, snake: true).Should().Be(2);
        "1111111111111111111111111111111111111111111111111111111111111111".TryParseBinNumber64(trim: true, snake: true).Should().Be(0xFFFFFFFFFFFFFFFF);
        "10000000000000000000000000000000000000000000000000000000000000000".TryParseBinNumber64(trim: true, snake: true).Should().BeNull();

        // ReadOnlySpan
        "0".AsSpan().TryParseBinNumber64(trim: true, snake: true).Should().Be(0);
        "1".AsSpan().TryParseBinNumber64(trim: true, snake: true).Should().Be(1);
        "10".AsSpan().TryParseBinNumber64(trim: true, snake: true).Should().Be(2);
        "1111111111111111111111111111111111111111111111111111111111111111".AsSpan().TryParseBinNumber64(trim: true, snake: true).Should().Be(0xFFFFFFFFFFFFFFFF);
        "10000000000000000000000000000000000000000000000000000000000000000".AsSpan().TryParseBinNumber64(trim: true, snake: true).Should().BeNull();

        // Span
        "0".ToArray().AsSpan().TryParseBinNumber64(trim: true, snake: true).Should().Be(0);
        "1".ToArray().AsSpan().TryParseBinNumber64(trim: true, snake: true).Should().Be(1);
        "10".ToArray().AsSpan().TryParseBinNumber64(trim: true, snake: true).Should().Be(2);
        "1111111111111111111111111111111111111111111111111111111111111111".ToArray().AsSpan().TryParseBinNumber64(trim: true, snake: true).Should().Be(0xFFFFFFFFFFFFFFFF);
        "10000000000000000000000000000000000000000000000000000000000000000".ToArray().AsSpan().TryParseBinNumber64(trim: true, snake: true).Should().BeNull();

        // parameters
        "   1   ".AsSpan().TryParseBinNumber64(trim: true, snake: true).Should().Be(1);
        "   1   ".AsSpan().TryParseBinNumber64(trim: false, snake: true).Should().BeNull();
        "11111111_11111111_11111111_11111111_11111111_11111111_11111111_11111111".AsSpan().TryParseBinNumber64(trim: true, snake: true).Should().Be(0xFFFFFFFFFFFFFFFF);
        "11111111_11111111_11111111_11111111_11111111_11111111_11111111_11111111".AsSpan().TryParseBinNumber64(trim: true, snake: false).Should().BeNull();
    }
    #endregion

    #region TryParseBinNumber(.NET7)
    [TestMethod()]
    public void TryParseBinNumber()
    {
        // string
        $"{new string('0', 8)}".TryParseBinNumber<Byte>().Should().Be(0);
        $"{new string('0', 16)}".TryParseBinNumber<UInt16>().Should().Be(0);
        $"{new string('0', 32)}".TryParseBinNumber<UInt32>().Should().Be(0);
        $"{new string('0', 64)}".TryParseBinNumber<UInt64>().Should().Be(0);
        $"{new string('0', 128)}".TryParseBinNumber<UInt128>().Should().Be(UInt128.Zero);
        $"{new string('0', 8)}".TryParseBinNumber<SByte>().Should().Be(0);
        $"{new string('0', 16)}".TryParseBinNumber<Int16>().Should().Be(0);
        $"{new string('0', 32)}".TryParseBinNumber<Int32>().Should().Be(0);
        $"{new string('0', 64)}".TryParseBinNumber<Int64>().Should().Be(0);
        $"{new string('0', 128)}".TryParseBinNumber<Int128>().Should().Be(Int128.Zero);
        $"{new string('1', 8)}".TryParseBinNumber<Byte>().Should().Be(Byte.MaxValue);
        $"{new string('1', 16)}".TryParseBinNumber<UInt16>().Should().Be(UInt16.MaxValue);
        $"{new string('1', 32)}".TryParseBinNumber<UInt32>().Should().Be(UInt32.MaxValue);
        $"{new string('1', 64)}".TryParseBinNumber<UInt64>().Should().Be(UInt64.MaxValue);
        $"{new string('1', 128)}".TryParseBinNumber<UInt128>().Should().Be(UInt128.MaxValue);
        $"{new string('1', 8)}".TryParseBinNumber<SByte>().Should().Be(unchecked((SByte)Byte.MaxValue));
        $"{new string('1', 16)}".TryParseBinNumber<Int16>().Should().Be(unchecked((Int16)UInt16.MaxValue));
        $"{new string('1', 32)}".TryParseBinNumber<Int32>().Should().Be(unchecked((Int32)UInt32.MaxValue));
        $"{new string('1', 64)}".TryParseBinNumber<Int64>().Should().Be(unchecked((Int64)UInt64.MaxValue));
        $"{new string('1', 128)}".TryParseBinNumber<Int128>().Should().Be(unchecked((Int128)UInt128.MaxValue));
        $"1{new string('0', 8)}".TryParseBinNumber<Byte>().Should().BeNull();
        $"1{new string('0', 16)}".TryParseBinNumber<UInt16>().Should().BeNull();
        $"1{new string('0', 32)}".TryParseBinNumber<UInt32>().Should().BeNull();
        $"1{new string('0', 64)}".TryParseBinNumber<UInt64>().Should().BeNull();
        $"1{new string('0', 128)}".TryParseBinNumber<UInt128>().Should().BeNull();
        $"1{new string('0', 8)}".TryParseBinNumber<SByte>().Should().BeNull();
        $"1{new string('0', 16)}".TryParseBinNumber<Int16>().Should().BeNull();
        $"1{new string('0', 32)}".TryParseBinNumber<Int32>().Should().BeNull();
        $"1{new string('0', 64)}".TryParseBinNumber<Int64>().Should().BeNull();
        $"1{new string('0', 128)}".TryParseBinNumber<Int128>().Should().BeNull();

        // ReadOnlySpan
        $"{new string('0', 8)}".AsSpan().TryParseBinNumber<Byte>().Should().Be(0);
        $"{new string('0', 16)}".AsSpan().TryParseBinNumber<UInt16>().Should().Be(0);
        $"{new string('0', 32)}".AsSpan().TryParseBinNumber<UInt32>().Should().Be(0);
        $"{new string('0', 64)}".AsSpan().TryParseBinNumber<UInt64>().Should().Be(0);
        $"{new string('0', 128)}".AsSpan().TryParseBinNumber<UInt128>().Should().Be(UInt128.Zero);
        $"{new string('0', 8)}".AsSpan().TryParseBinNumber<SByte>().Should().Be(0);
        $"{new string('0', 16)}".AsSpan().TryParseBinNumber<Int16>().Should().Be(0);
        $"{new string('0', 32)}".AsSpan().TryParseBinNumber<Int32>().Should().Be(0);
        $"{new string('0', 64)}".AsSpan().TryParseBinNumber<Int64>().Should().Be(0);
        $"{new string('0', 128)}".AsSpan().TryParseBinNumber<Int128>().Should().Be(Int128.Zero);
        $"{new string('1', 8)}".AsSpan().TryParseBinNumber<Byte>().Should().Be(Byte.MaxValue);
        $"{new string('1', 16)}".AsSpan().TryParseBinNumber<UInt16>().Should().Be(UInt16.MaxValue);
        $"{new string('1', 32)}".AsSpan().TryParseBinNumber<UInt32>().Should().Be(UInt32.MaxValue);
        $"{new string('1', 64)}".AsSpan().TryParseBinNumber<UInt64>().Should().Be(UInt64.MaxValue);
        $"{new string('1', 128)}".AsSpan().TryParseBinNumber<UInt128>().Should().Be(UInt128.MaxValue);
        $"{new string('1', 8)}".AsSpan().TryParseBinNumber<SByte>().Should().Be(unchecked((SByte)Byte.MaxValue));
        $"{new string('1', 16)}".AsSpan().TryParseBinNumber<Int16>().Should().Be(unchecked((Int16)UInt16.MaxValue));
        $"{new string('1', 32)}".AsSpan().TryParseBinNumber<Int32>().Should().Be(unchecked((Int32)UInt32.MaxValue));
        $"{new string('1', 64)}".AsSpan().TryParseBinNumber<Int64>().Should().Be(unchecked((Int64)UInt64.MaxValue));
        $"{new string('1', 128)}".AsSpan().TryParseBinNumber<Int128>().Should().Be(unchecked((Int128)UInt128.MaxValue));
        $"1{new string('0', 8)}".AsSpan().TryParseBinNumber<Byte>().Should().BeNull();
        $"1{new string('0', 16)}".AsSpan().TryParseBinNumber<UInt16>().Should().BeNull();
        $"1{new string('0', 32)}".AsSpan().TryParseBinNumber<UInt32>().Should().BeNull();
        $"1{new string('0', 64)}".AsSpan().TryParseBinNumber<UInt64>().Should().BeNull();
        $"1{new string('0', 128)}".AsSpan().TryParseBinNumber<UInt128>().Should().BeNull();
        $"1{new string('0', 8)}".AsSpan().TryParseBinNumber<SByte>().Should().BeNull();
        $"1{new string('0', 16)}".AsSpan().TryParseBinNumber<Int16>().Should().BeNull();
        $"1{new string('0', 32)}".AsSpan().TryParseBinNumber<Int32>().Should().BeNull();
        $"1{new string('0', 64)}".AsSpan().TryParseBinNumber<Int64>().Should().BeNull();
        $"1{new string('0', 128)}".AsSpan().TryParseBinNumber<Int128>().Should().BeNull();

        // Span
        $"{new string('0', 8)}".ToArray().AsSpan().TryParseBinNumber<Byte>().Should().Be(0);
        $"{new string('0', 16)}".ToArray().AsSpan().TryParseBinNumber<UInt16>().Should().Be(0);
        $"{new string('0', 32)}".ToArray().AsSpan().TryParseBinNumber<UInt32>().Should().Be(0);
        $"{new string('0', 64)}".ToArray().AsSpan().TryParseBinNumber<UInt64>().Should().Be(0);
        $"{new string('0', 128)}".ToArray().AsSpan().TryParseBinNumber<UInt128>().Should().Be(UInt128.Zero);
        $"{new string('0', 8)}".ToArray().AsSpan().TryParseBinNumber<SByte>().Should().Be(0);
        $"{new string('0', 16)}".ToArray().AsSpan().TryParseBinNumber<Int16>().Should().Be(0);
        $"{new string('0', 32)}".ToArray().AsSpan().TryParseBinNumber<Int32>().Should().Be(0);
        $"{new string('0', 64)}".ToArray().AsSpan().TryParseBinNumber<Int64>().Should().Be(0);
        $"{new string('0', 128)}".ToArray().AsSpan().TryParseBinNumber<Int128>().Should().Be(Int128.Zero);
        $"{new string('1', 8)}".ToArray().AsSpan().TryParseBinNumber<Byte>().Should().Be(Byte.MaxValue);
        $"{new string('1', 16)}".ToArray().AsSpan().TryParseBinNumber<UInt16>().Should().Be(UInt16.MaxValue);
        $"{new string('1', 32)}".ToArray().AsSpan().TryParseBinNumber<UInt32>().Should().Be(UInt32.MaxValue);
        $"{new string('1', 64)}".ToArray().AsSpan().TryParseBinNumber<UInt64>().Should().Be(UInt64.MaxValue);
        $"{new string('1', 128)}".ToArray().AsSpan().TryParseBinNumber<UInt128>().Should().Be(UInt128.MaxValue);
        $"{new string('1', 8)}".ToArray().AsSpan().TryParseBinNumber<SByte>().Should().Be(unchecked((SByte)Byte.MaxValue));
        $"{new string('1', 16)}".ToArray().AsSpan().TryParseBinNumber<Int16>().Should().Be(unchecked((Int16)UInt16.MaxValue));
        $"{new string('1', 32)}".ToArray().AsSpan().TryParseBinNumber<Int32>().Should().Be(unchecked((Int32)UInt32.MaxValue));
        $"{new string('1', 64)}".ToArray().AsSpan().TryParseBinNumber<Int64>().Should().Be(unchecked((Int64)UInt64.MaxValue));
        $"{new string('1', 128)}".ToArray().AsSpan().TryParseBinNumber<Int128>().Should().Be(unchecked((Int128)UInt128.MaxValue));
        $"1{new string('0', 8)}".ToArray().AsSpan().TryParseBinNumber<Byte>().Should().BeNull();
        $"1{new string('0', 16)}".ToArray().AsSpan().TryParseBinNumber<UInt16>().Should().BeNull();
        $"1{new string('0', 32)}".ToArray().AsSpan().TryParseBinNumber<UInt32>().Should().BeNull();
        $"1{new string('0', 64)}".ToArray().AsSpan().TryParseBinNumber<UInt64>().Should().BeNull();
        $"1{new string('0', 128)}".ToArray().AsSpan().TryParseBinNumber<UInt128>().Should().BeNull();
        $"1{new string('0', 8)}".ToArray().AsSpan().TryParseBinNumber<SByte>().Should().BeNull();
        $"1{new string('0', 16)}".ToArray().AsSpan().TryParseBinNumber<Int16>().Should().BeNull();
        $"1{new string('0', 32)}".ToArray().AsSpan().TryParseBinNumber<Int32>().Should().BeNull();
        $"1{new string('0', 64)}".ToArray().AsSpan().TryParseBinNumber<Int64>().Should().BeNull();
        $"1{new string('0', 128)}".ToArray().AsSpan().TryParseBinNumber<Int128>().Should().BeNull();
    }
    #endregion

    #region TryParseBin(.NET7)
    [TestMethod()]
    public void TryParseBin()
    {
        // string
        "1_0100".TryParseBin<ushort>(snake: true).Should().Be(0x14);
        "0b1_0100".TryParseBin<ushort>().Should().Be(0x14);
        "0B1_0100".TryParseBin<ushort>().Should().Be(0x14);
        "&b1_0100".TryParseBin<ushort>().Should().Be(0x14);
        "&B1_0100".TryParseBin<ushort>().Should().Be(0x14);
        "1_0100b".TryParseBin<ushort>().Should().Be(0x14);
        "1_0100B".TryParseBin<ushort>().Should().Be(0x14);
        "B1_0100".TryParseBin<ushort>().Should().BeNull();

        // ReadOnlySpan
        "1_0100".AsSpan().TryParseBin<ushort>().Should().Be(0x14);
        "0b1_0100".AsSpan().TryParseBin<ushort>().Should().Be(0x14);
        "&b1_0100".AsSpan().TryParseBin<ushort>().Should().Be(0x14);
        "&B1_0100".AsSpan().TryParseBin<ushort>().Should().Be(0x14);
        "1_0100b".AsSpan().TryParseBin<ushort>().Should().Be(0x14);
        "1_0100B".AsSpan().TryParseBin<ushort>().Should().Be(0x14);
        "B1_0100".AsSpan().TryParseBin<ushort>().Should().BeNull();

        // Span
        "1_0100".ToArray().AsSpan().TryParseBin<ushort>().Should().Be(0x14);
        "0b1_0100".ToArray().AsSpan().TryParseBin<ushort>().Should().Be(0x14);
        "&b1_0100".ToArray().AsSpan().TryParseBin<ushort>().Should().Be(0x14);
        "&B1_0100".ToArray().AsSpan().TryParseBin<ushort>().Should().Be(0x14);
        "1_0100b".ToArray().AsSpan().TryParseBin<ushort>().Should().Be(0x14);
        "1_0100B".ToArray().AsSpan().TryParseBin<ushort>().Should().Be(0x14);
        "B1_0100".ToArray().AsSpan().TryParseBin<ushort>().Should().BeNull();
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

    #region TryParseNumberWithPrefix(.NET7)
    [TestMethod()]
    public void TryParseNumberWithPrefix()
    {
        "12345678".TryParseNumberWithPrefix<UInt32>().Should().Be(12345678);
        "0x12345678".TryParseNumberWithPrefix<UInt32>().Should().Be(0x12345678);
        "0X12345678".TryParseNumberWithPrefix<UInt32>().Should().Be(0x12345678);
        "&h12345678".TryParseNumberWithPrefix<UInt32>().Should().Be(0x12345678);
        "&H12345678".TryParseNumberWithPrefix<UInt32>().Should().Be(0x12345678);
        "#12345678".TryParseNumberWithPrefix<UInt32>().Should().Be(0x12345678);

        "0x12".TryParseNumberWithPrefix<Byte>().Should().Be(0x12);
        "0x12".TryParseNumberWithPrefix<UInt16>().Should().Be(0x12);
        "0x12".TryParseNumberWithPrefix<UInt32>().Should().Be(0x12);
        "0x12".TryParseNumberWithPrefix<UInt64>().Should().Be(0x12);
        "0x12".TryParseNumberWithPrefix<UInt128>().Should().Be((UInt128)0x12);

        "xyz".TryParseNumberWithPrefix<Byte>().Should().BeNull();
        "xyz".TryParseNumberWithPrefix<UInt16>().Should().BeNull();
        "xyz".TryParseNumberWithPrefix<UInt32>().Should().BeNull();
        "xyz".TryParseNumberWithPrefix<UInt64>().Should().BeNull();
        "xyz".TryParseNumberWithPrefix<UInt128>().Should().BeNull();
    }
    #endregion

}
