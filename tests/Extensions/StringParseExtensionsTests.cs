using System.Globalization;

namespace LestalyTest.Extensions;

[TestClass()]
public class StringParseExtensionsTests
{
    #region Parse
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

    #region ParseNumber
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
        $"{Decimal.MinValue}".ParseNumber<Decimal>().Should().Be(Decimal.MinValue);
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
        $"{Decimal.MaxValue}".ParseNumber<Decimal>().Should().Be(Decimal.MaxValue);
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
        new Action(() => $"1{Decimal.MaxValue}".ParseNumber<Decimal>()).Should().Throw<Exception>();

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
        $"{Decimal.MinValue}".AsSpan().ParseNumber<Decimal>().Should().Be(Decimal.MinValue);
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
        $"{Decimal.MaxValue}".AsSpan().ParseNumber<Decimal>().Should().Be(Decimal.MaxValue);
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
        new Action(() => $"1{Decimal.MaxValue}".AsSpan().ParseNumber<Decimal>()).Should().Throw<Exception>();

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
        $"{Decimal.MinValue}".ToArray().AsSpan().ParseNumber<Decimal>().Should().Be(Decimal.MinValue);
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
        $"{Decimal.MaxValue}".ToArray().AsSpan().ParseNumber<Decimal>().Should().Be(Decimal.MaxValue);
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
        new Action(() => $"1{Decimal.MaxValue}".ToArray().AsSpan().ParseNumber<Decimal>()).Should().Throw<Exception>();
    }
    #endregion

    #region TryParse
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

    #region TryParseNumber
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
        $"{Decimal.MinValue}".TryParseNumber<Decimal>().Should().Be(Decimal.MinValue);
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
        $"{Decimal.MaxValue}".TryParseNumber<Decimal>().Should().Be(Decimal.MaxValue);
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
        $"1{Decimal.MinValue}".TryParseNumber<Decimal>().Should().BeNull();

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
        $"{Decimal.MinValue}".AsSpan().TryParseNumber<Decimal>().Should().Be(Decimal.MinValue);
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
        $"{Decimal.MaxValue}".AsSpan().TryParseNumber<Decimal>().Should().Be(Decimal.MaxValue);
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
        $"1{Decimal.MaxValue}".AsSpan().TryParseNumber<Decimal>().Should().BeNull();

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
        $"{Decimal.MinValue}".ToArray().AsSpan().TryParseNumber<Decimal>().Should().Be(Decimal.MinValue);
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
        $"{Decimal.MaxValue}".ToArray().AsSpan().TryParseNumber<Decimal>().Should().Be(Decimal.MaxValue);
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
        $"1{Decimal.MaxValue}".ToArray().AsSpan().TryParseNumber<Decimal>().Should().BeNull();
    }
    #endregion

    #region TryParseHexNumber
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

    #region TryParseHex
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

    #region TryParseHumanized
    [TestMethod()]
    public void TryParseHumanized()
    {
        // string
        "2K".TryParseHumanized(si: true).Should().Be(2 * 1000L);
        "2M".TryParseHumanized(si: true).Should().Be(2 * 1000L * 1000L);
        "2G".TryParseHumanized(si: true).Should().Be(2 * 1000L * 1000L * 1000L);
        "2T".TryParseHumanized(si: true).Should().Be(2 * 1000L * 1000L * 1000L * 1000L);
        "2P".TryParseHumanized(si: true).Should().Be(2 * 1000L * 1000L * 1000L * 1000L * 1000L);
        "2E".TryParseHumanized(si: true).Should().Be(2 * 1000L * 1000L * 1000L * 1000L * 1000L * 1000L);
        "123E".TryParseHumanized(si: true).Should().Be(null);

        "2K".TryParseHumanized(si: false).Should().Be(2 * 1024L);
        "2M".TryParseHumanized(si: false).Should().Be(2 * 1024L * 1024L);
        "2G".TryParseHumanized(si: false).Should().Be(2 * 1024L * 1024L * 1024L);
        "2T".TryParseHumanized(si: false).Should().Be(2 * 1024L * 1024L * 1024L * 1024L);
        "2P".TryParseHumanized(si: false).Should().Be(2 * 1024L * 1024L * 1024L * 1024L * 1024L);
        "2E".TryParseHumanized(si: false).Should().Be(2 * 1024L * 1024L * 1024L * 1024L * 1024L * 1024L);
        "123E".TryParseHumanized(si: false).Should().Be(null);

        // ReadOnlySpan
        "2K".AsSpan().TryParseHumanized(si: true).Should().Be(2 * 1000L);
        "2M".AsSpan().TryParseHumanized(si: true).Should().Be(2 * 1000L * 1000L);
        "2G".AsSpan().TryParseHumanized(si: true).Should().Be(2 * 1000L * 1000L * 1000L);
        "2T".AsSpan().TryParseHumanized(si: true).Should().Be(2 * 1000L * 1000L * 1000L * 1000L);
        "2P".AsSpan().TryParseHumanized(si: true).Should().Be(2 * 1000L * 1000L * 1000L * 1000L * 1000L);
        "2E".AsSpan().TryParseHumanized(si: true).Should().Be(2 * 1000L * 1000L * 1000L * 1000L * 1000L * 1000L);
        "123E".AsSpan().TryParseHumanized(si: true).Should().Be(null);

        "2K".AsSpan().TryParseHumanized(si: false).Should().Be(2 * 1024L);
        "2M".AsSpan().TryParseHumanized(si: false).Should().Be(2 * 1024L * 1024L);
        "2G".AsSpan().TryParseHumanized(si: false).Should().Be(2 * 1024L * 1024L * 1024L);
        "2T".AsSpan().TryParseHumanized(si: false).Should().Be(2 * 1024L * 1024L * 1024L * 1024L);
        "2P".AsSpan().TryParseHumanized(si: false).Should().Be(2 * 1024L * 1024L * 1024L * 1024L * 1024L);
        "2E".AsSpan().TryParseHumanized(si: false).Should().Be(2 * 1024L * 1024L * 1024L * 1024L * 1024L * 1024L);
        "123E".AsSpan().TryParseHumanized(si: false).Should().Be(null);
    }
    #endregion

    #region TryParseBinNumber
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

    #region TryParseBin
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

    #region TryParseNumberWithPrefix
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
