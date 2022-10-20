using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace LestalyTest.Extensions;

[TestClass()]
public class StringParseExtensionsTests
{
    [TestMethod()]
    public void ParseUInt8()
    {
        "0".ParseUInt8().Should().Be(0);
        "100".ParseUInt8().Should().Be(100);
        "255".ParseUInt8().Should().Be(255);
        new Action(() => "256".ParseUInt8()).Should().Throw<Exception>();

        "0".ParseUInt8(NumberStyles.HexNumber).Should().Be(0);
        "80".ParseUInt8(NumberStyles.HexNumber).Should().Be(0x80);
        "FF".ParseUInt8(NumberStyles.HexNumber).Should().Be(0xFF);
        new Action(() => "100".ParseUInt8(NumberStyles.HexNumber)).Should().Throw<Exception>();
    }

    [TestMethod()]
    public void ParseUInt16()
    {
        "0".ParseUInt16().Should().Be(0);
        "100".ParseUInt16().Should().Be(100);
        "65535".ParseUInt16().Should().Be(65535);
        new Action(() => "65536".ParseUInt16()).Should().Throw<Exception>();

        "0".ParseUInt16(NumberStyles.HexNumber).Should().Be(0);
        "8000".ParseUInt16(NumberStyles.HexNumber).Should().Be(0x8000);
        "FFFF".ParseUInt16(NumberStyles.HexNumber).Should().Be(0xFFFF);
        new Action(() => "10000".ParseUInt16(NumberStyles.HexNumber)).Should().Throw<Exception>();
    }

    [TestMethod()]
    public void ParseUInt32()
    {
        "0".ParseUInt32().Should().Be(0);
        "1000000000".ParseUInt32().Should().Be(1000000000);
        "4294967295".ParseUInt32().Should().Be(4294967295);
        new Action(() => "4294967296".ParseUInt32()).Should().Throw<Exception>();

        "0".ParseUInt32(NumberStyles.HexNumber).Should().Be(0);
        "80000000".ParseUInt32(NumberStyles.HexNumber).Should().Be(0x80000000);
        "FFFFFFFF".ParseUInt32(NumberStyles.HexNumber).Should().Be(0xFFFFFFFF);
        new Action(() => "100000000".ParseUInt32(NumberStyles.HexNumber)).Should().Throw<Exception>();
    }

    [TestMethod()]
    public void ParseUInt64()
    {
        "0".ParseUInt64().Should().Be(0);
        "10000000000000000000".ParseUInt64().Should().Be(10000000000000000000);
        "18446744073709551615".ParseUInt64().Should().Be(18446744073709551615);
        new Action(() => "18446744073709551616".ParseUInt64()).Should().Throw<Exception>();

        "0".ParseUInt64(NumberStyles.HexNumber).Should().Be(0);
        "8000000000000000".ParseUInt64(NumberStyles.HexNumber).Should().Be(0x8000000000000000);
        "FFFFFFFFFFFFFFFF".ParseUInt64(NumberStyles.HexNumber).Should().Be(0xFFFFFFFFFFFFFFFF);
        new Action(() => "10000000000000000".ParseUInt64(NumberStyles.HexNumber)).Should().Throw<Exception>();
    }

    [TestMethod()]
    public void ParseInt8()
    {
        "0".ParseInt8().Should().Be(0);
        "100".ParseInt8().Should().Be(100);
        "127".ParseInt8().Should().Be(127);
        "-128".ParseInt8().Should().Be(-128);
        new Action(() => "128".ParseInt8()).Should().Throw<Exception>();

        "0".ParseInt8(NumberStyles.HexNumber).Should().Be(0);
        "7F".ParseInt8(NumberStyles.HexNumber).Should().Be(0x7F);
        "80".ParseInt8(NumberStyles.HexNumber).Should().Be(-128);
        new Action(() => "100".ParseInt8(NumberStyles.HexNumber)).Should().Throw<Exception>();
    }

    [TestMethod()]
    public void ParseInt16()
    {
        "0".ParseInt16().Should().Be(0);
        "10000".ParseInt16().Should().Be(10000);
        "32767".ParseInt16().Should().Be(32767);
        new Action(() => "32768".ParseInt16()).Should().Throw<Exception>();

        "0".ParseInt16(NumberStyles.HexNumber).Should().Be(0);
        "7FFF".ParseInt16(NumberStyles.HexNumber).Should().Be(0x7FFF);
        "8000".ParseInt16(NumberStyles.HexNumber).Should().Be(-32768);
        new Action(() => "10000".ParseInt16(NumberStyles.HexNumber)).Should().Throw<Exception>();
    }

    [TestMethod()]
    public void ParseInt32()
    {
        "0".ParseInt32().Should().Be(0);
        "1000000000".ParseInt32().Should().Be(1000000000);
        "2147483647".ParseInt32().Should().Be(2147483647);
        new Action(() => "2147483648".ParseInt32()).Should().Throw<Exception>();

        "0".ParseInt32(NumberStyles.HexNumber).Should().Be(0);
        "7FFFFFFF".ParseInt32(NumberStyles.HexNumber).Should().Be(0x7FFFFFFF);
        "80000000".ParseInt32(NumberStyles.HexNumber).Should().Be(-2147483648);
        new Action(() => "100000000".ParseInt32(NumberStyles.HexNumber)).Should().Throw<Exception>();
    }

    [TestMethod()]
    public void ParseInt64()
    {
        "0".ParseInt64().Should().Be(0);
        "1000000000000000000".ParseInt64().Should().Be(1000000000000000000);
        "9223372036854775807".ParseInt64().Should().Be(9223372036854775807);
        new Action(() => "9223372036854775808".ParseInt64()).Should().Throw<Exception>();

        "0".ParseInt64(NumberStyles.HexNumber).Should().Be(0);
        "7FFFFFFFFFFFFFFF".ParseInt64(NumberStyles.HexNumber).Should().Be(0x7FFFFFFFFFFFFFFF);
        "8000000000000000".ParseInt64(NumberStyles.HexNumber).Should().Be(-9223372036854775808);
        new Action(() => "10000000000000000".ParseInt64(NumberStyles.HexNumber)).Should().Throw<Exception>();
    }

    [TestMethod()]
    public void ParseFloat()
    {
        "0".ParseFloat().Should().Be(0);
        "100".ParseFloat().Should().Be(100f);
        "-100".ParseFloat().Should().Be(-100f);
        new Action(() => "asd".ParseFloat()).Should().Throw<Exception>();

        "100".ParseFloat(NumberStyles.Float).Should().Be(100f);
    }

    [TestMethod()]
    public void ParseDouble()
    {
        "0".ParseDouble().Should().Be(0);
        "100".ParseDouble().Should().Be(100d);
        "-100".ParseDouble().Should().Be(-100d);
        new Action(() => "asd".ParseDouble()).Should().Throw<Exception>();

        "100".ParseDouble(NumberStyles.Float).Should().Be(100d);
    }

    [TestMethod()]
    public void ParseDecimal()
    {
        "0".ParseDecimal().Should().Be(0);
        "100".ParseDecimal().Should().Be(100m);
        "-100".ParseDecimal().Should().Be(-100m);
        new Action(() => "asd".ParseDecimal()).Should().Throw<Exception>();

        "100".ParseDecimal(NumberStyles.Number).Should().Be(100m);
    }

    [TestMethod()]
    public void ParseDateTime()
    {
        "2022/12/31".ParseDateTime().Should().Be(new DateTime(2022, 12, 31));
        new Action(() => "asd".ParseDateTime()).Should().Throw<Exception>();

        "11:12:13".ParseDateTime(DateTimeStyles.NoCurrentDateDefault).Should().Be(new DateTime(1, 1, 1, 11, 12, 13));
    }

    [TestMethod()]
    public void ParseDateTimeExact()
    {
        "1234_05_06".ParseDateTimeExact("yyyy_M_d").Should().Be(new DateTime(1234, 5, 6));

        new Action(() => "zz".ParseDateTimeExact("yyyy_M_d")).Should().Throw<Exception>();
    }

    [TestMethod()]
    public void TryParseUInt8()
    {
        "0".TryParseUInt8().Should().Be(0);
        "100".TryParseUInt8().Should().Be(100);
        "255".TryParseUInt8().Should().Be(255);
        "256".TryParseUInt8().Should().BeNull();

        "0".TryParseUInt8(NumberStyles.HexNumber).Should().Be(0);
        "80".TryParseUInt8(NumberStyles.HexNumber).Should().Be(0x80);
        "FF".TryParseUInt8(NumberStyles.HexNumber).Should().Be(0xFF);
        "100".TryParseUInt8(NumberStyles.HexNumber).Should().BeNull();
    }

    [TestMethod()]
    public void TryParseUInt16()
    {
        "0".TryParseUInt16().Should().Be(0);
        "100".TryParseUInt16().Should().Be(100);
        "65535".TryParseUInt16().Should().Be(65535);
        "65536".TryParseUInt16().Should().BeNull();

        "0".TryParseUInt16(NumberStyles.HexNumber).Should().Be(0);
        "8000".ParseUInt16(NumberStyles.HexNumber).Should().Be(0x8000);
        "FFFF".TryParseUInt16(NumberStyles.HexNumber).Should().Be(0xFFFF);
        "10000".TryParseUInt16(NumberStyles.HexNumber).Should().BeNull();
    }

    [TestMethod()]
    public void TryParseUInt32()
    {
        "0".TryParseUInt32().Should().Be(0);
        "1000000000".TryParseUInt32().Should().Be(1000000000);
        "4294967295".TryParseUInt32().Should().Be(4294967295);
        "4294967296".TryParseUInt32().Should().BeNull();

        "0".TryParseUInt32(NumberStyles.HexNumber).Should().Be(0);
        "80000000".TryParseUInt32(NumberStyles.HexNumber).Should().Be(0x80000000);
        "FFFFFFFF".TryParseUInt32(NumberStyles.HexNumber).Should().Be(0xFFFFFFFF);
        "100000000".TryParseUInt32(NumberStyles.HexNumber).Should().BeNull();
    }

    [TestMethod()]
    public void TryParseUInt64()
    {
        "0".TryParseUInt64().Should().Be(0);
        "10000000000000000000".TryParseUInt64().Should().Be(10000000000000000000);
        "18446744073709551615".TryParseUInt64().Should().Be(18446744073709551615);
        "18446744073709551616".TryParseUInt64().Should().BeNull();

        "0".TryParseUInt64(NumberStyles.HexNumber).Should().Be(0);
        "8000000000000000".TryParseUInt64(NumberStyles.HexNumber).Should().Be(0x8000000000000000);
        "FFFFFFFFFFFFFFFF".TryParseUInt64(NumberStyles.HexNumber).Should().Be(0xFFFFFFFFFFFFFFFF);
        "10000000000000000".TryParseUInt64(NumberStyles.HexNumber).Should().BeNull();
    }

    [TestMethod()]
    public void TryParseInt8()
    {
        "0".TryParseInt8().Should().Be(0);
        "100".TryParseInt8().Should().Be(100);
        "127".TryParseInt8().Should().Be(127);
        "-128".TryParseInt8().Should().Be(-128);
        "128".TryParseInt8().Should().BeNull();

        "0".TryParseInt8(NumberStyles.HexNumber).Should().Be(0);
        "7F".TryParseInt8(NumberStyles.HexNumber).Should().Be(0x7F);
        "80".TryParseInt8(NumberStyles.HexNumber).Should().Be(-128);
        "100".TryParseInt8(NumberStyles.HexNumber).Should().BeNull();
    }

    [TestMethod()]
    public void TryParseInt16()
    {
        "0".TryParseInt16().Should().Be(0);
        "10000".TryParseInt16().Should().Be(10000);
        "32767".TryParseInt16().Should().Be(32767);
        "32768".TryParseInt16().Should().BeNull();

        "0".TryParseInt16(NumberStyles.HexNumber).Should().Be(0);
        "7FFF".TryParseInt16(NumberStyles.HexNumber).Should().Be(0x7FFF);
        "8000".TryParseInt16(NumberStyles.HexNumber).Should().Be(-32768);
        "10000".TryParseInt16(NumberStyles.HexNumber).Should().BeNull();
    }

    [TestMethod()]
    public void TryParseInt32()
    {
        "0".TryParseInt32().Should().Be(0);
        "1000000000".TryParseInt32().Should().Be(1000000000);
        "2147483647".TryParseInt32().Should().Be(2147483647);
        "2147483648".TryParseInt32().Should().BeNull();

        "0".TryParseInt32(NumberStyles.HexNumber).Should().Be(0);
        "7FFFFFFF".TryParseInt32(NumberStyles.HexNumber).Should().Be(0x7FFFFFFF);
        "80000000".TryParseInt32(NumberStyles.HexNumber).Should().Be(-2147483648);
        "100000000".TryParseInt32(NumberStyles.HexNumber).Should().BeNull();
    }

    [TestMethod()]
    public void TryParseInt64()
    {
        "0".TryParseInt64().Should().Be(0);
        "1000000000000000000".TryParseInt64().Should().Be(1000000000000000000);
        "9223372036854775807".TryParseInt64().Should().Be(9223372036854775807);
        "9223372036854775808".TryParseInt64().Should().BeNull();

        "0".TryParseInt64(NumberStyles.HexNumber).Should().Be(0);
        "7FFFFFFFFFFFFFFF".TryParseInt64(NumberStyles.HexNumber).Should().Be(0x7FFFFFFFFFFFFFFF);
        "8000000000000000".TryParseInt64(NumberStyles.HexNumber).Should().Be(-9223372036854775808);
        "10000000000000000".TryParseInt64(NumberStyles.HexNumber).Should().BeNull();
    }

    [TestMethod()]
    public void TryParseFloat()
    {
        "0".TryParseFloat().Should().Be(0);
        "100".TryParseFloat().Should().Be(100f);
        "-100".TryParseFloat().Should().Be(-100f);
        "asd".TryParseFloat().Should().BeNull();

        "100".TryParseFloat(NumberStyles.Float).Should().Be(100f);
    }

    [TestMethod()]
    public void TryParseDouble()
    {
        "0".TryParseDouble().Should().Be(0);
        "100".TryParseDouble().Should().Be(100d);
        "-100".TryParseDouble().Should().Be(-100d);
        "asd".TryParseDouble().Should().BeNull();

        "100".TryParseDouble(NumberStyles.Float).Should().Be(100d);
    }

    [TestMethod()]
    public void TryParseDecimal()
    {
        "0".TryParseDecimal().Should().Be(0);
        "100".TryParseDecimal().Should().Be(100m);
        "-100".TryParseDecimal().Should().Be(-100m);
        "asd".TryParseDecimal().Should().BeNull();

        "100".TryParseDecimal(NumberStyles.Number).Should().Be(100m);
    }

    [TestMethod()]
    public void TryParseDateTime()
    {
        "2022/12/31".TryParseDateTime().Should().Be(new DateTime(2022, 12, 31));
        "asd".TryParseDateTime().Should().BeNull();

        "11:12:13".TryParseDateTime(DateTimeStyles.NoCurrentDateDefault).Should().Be(new DateTime(1, 1, 1, 11, 12, 13));
    }

    [TestMethod()]
    public void TryParseDateTimeExact()
    {
        "1234_05_06".TryParseDateTimeExact("yyyy_M_d").Should().Be(new DateTime(1234, 5, 6));

        "zz".TryParseDateTimeExact("yyyy_M_d").Should().BeNull();
    }


}
