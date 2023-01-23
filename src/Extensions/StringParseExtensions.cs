using System.Globalization;

namespace Lestaly;

/// <summary>
/// 文字列のパース関連の拡張メソッド
/// </summary>
public static class StringParseExtensions
{
    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static Byte ParseUInt8(this string self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => Byte.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static Byte ParseUInt8(this Span<char> self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => Byte.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static Byte ParseUInt8(this ReadOnlySpan<char> self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => Byte.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static UInt16 ParseUInt16(this string self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => UInt16.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static UInt16 ParseUInt16(this Span<char> self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => UInt16.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static UInt16 ParseUInt16(this ReadOnlySpan<char> self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => UInt16.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static UInt32 ParseUInt32(this string self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => UInt32.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static UInt32 ParseUInt32(this Span<char> self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => UInt32.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static UInt32 ParseUInt32(this ReadOnlySpan<char> self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => UInt32.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static UInt64 ParseUInt64(this string self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => UInt64.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static UInt64 ParseUInt64(this Span<char> self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => UInt64.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static UInt64 ParseUInt64(this ReadOnlySpan<char> self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => UInt64.Parse(self, style, provider ?? CultureInfo.InvariantCulture);


    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static SByte ParseInt8(this string self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => SByte.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static SByte ParseInt8(this Span<char> self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => SByte.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static SByte ParseInt8(this ReadOnlySpan<char> self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => SByte.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static Int16 ParseInt16(this string self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => Int16.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static Int16 ParseInt16(this Span<char> self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => Int16.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static Int16 ParseInt16(this ReadOnlySpan<char> self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => Int16.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static Int32 ParseInt32(this string self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => Int32.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static Int32 ParseInt32(this Span<char> self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => Int32.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static Int32 ParseInt32(this ReadOnlySpan<char> self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => Int32.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static Int64 ParseInt64(this string self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => Int64.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static Int64 ParseInt64(this Span<char> self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => Int64.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static Int64 ParseInt64(this ReadOnlySpan<char> self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => Int64.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static Single ParseFloat(this string self, NumberStyles style = NumberStyles.AllowThousands | NumberStyles.Float, IFormatProvider? provider = default)
        => Single.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static Single ParseFloat(this Span<char> self, NumberStyles style = NumberStyles.AllowThousands | NumberStyles.Float, IFormatProvider? provider = default)
        => Single.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static Single ParseFloat(this ReadOnlySpan<char> self, NumberStyles style = NumberStyles.AllowThousands | NumberStyles.Float, IFormatProvider? provider = default)
        => Single.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static Double ParseDouble(this string self, NumberStyles style = NumberStyles.AllowThousands | NumberStyles.Float, IFormatProvider? provider = default)
        => Double.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static Double ParseDouble(this Span<char> self, NumberStyles style = NumberStyles.AllowThousands | NumberStyles.Float, IFormatProvider? provider = default)
        => Double.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static Double ParseDouble(this ReadOnlySpan<char> self, NumberStyles style = NumberStyles.AllowThousands | NumberStyles.Float, IFormatProvider? provider = default)
        => Double.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static Decimal ParseDecimal(this string self, NumberStyles style = NumberStyles.Number, IFormatProvider? provider = default)
        => Decimal.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static Decimal ParseDecimal(this Span<char> self, NumberStyles style = NumberStyles.Number, IFormatProvider? provider = default)
        => Decimal.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static Decimal ParseDecimal(this ReadOnlySpan<char> self, NumberStyles style = NumberStyles.Number, IFormatProvider? provider = default)
        => Decimal.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static DateTime ParseDateTime(this string self, DateTimeStyles style = DateTimeStyles.None, IFormatProvider? provider = default)
        => DateTime.Parse(self, provider ?? CultureInfo.InvariantCulture, style);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static DateTime ParseDateTime(this Span<char> self, DateTimeStyles style = DateTimeStyles.None, IFormatProvider? provider = default)
        => DateTime.Parse(self, provider ?? CultureInfo.InvariantCulture, style);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static DateTime ParseDateTime(this ReadOnlySpan<char> self, DateTimeStyles style = DateTimeStyles.None, IFormatProvider? provider = default)
        => DateTime.Parse(self, provider ?? CultureInfo.InvariantCulture, style);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="format">パース書式指定子</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static DateTime ParseDateTimeExact(this string self, string format, DateTimeStyles style = DateTimeStyles.None, IFormatProvider? provider = default)
        => DateTime.ParseExact(self, format, provider ?? CultureInfo.InvariantCulture, style);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="format">パース書式指定子</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static DateTime ParseDateTimeExact(this Span<char> self, string format, DateTimeStyles style = DateTimeStyles.None, IFormatProvider? provider = default)
        => DateTime.ParseExact(self, format, provider ?? CultureInfo.InvariantCulture, style);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="format">パース書式指定子</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static DateTime ParseDateTimeExact(this ReadOnlySpan<char> self, string format, DateTimeStyles style = DateTimeStyles.None, IFormatProvider? provider = default)
        => DateTime.ParseExact(self, format, provider ?? CultureInfo.InvariantCulture, style);


    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値またはnull</returns>
    public static Byte? TryParseUInt8(this string self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => Byte.TryParse(self, style, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt16? TryParseUInt16(this string self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => UInt16.TryParse(self, style, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt32? TryParseUInt32(this string self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => UInt32.TryParse(self, style, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt64? TryParseUInt64(this string self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => UInt64.TryParse(self, style, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値またはnull</returns>
    public static SByte? TryParseInt8(this string self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => SByte.TryParse(self, style, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値またはnull</returns>
    public static Int16? TryParseInt16(this string self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => Int16.TryParse(self, style, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値またはnull</returns>
    public static Int32? TryParseInt32(this string self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => Int32.TryParse(self, style, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値またはnull</returns>
    public static Int64? TryParseInt64(this string self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => Int64.TryParse(self, style, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値またはnull</returns>
    public static Single? TryParseFloat(this string self, NumberStyles style = NumberStyles.AllowThousands | NumberStyles.Float, IFormatProvider? provider = default)
        => Single.TryParse(self, style, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値またはnull</returns>
    public static Double? TryParseDouble(this string self, NumberStyles style = NumberStyles.AllowThousands | NumberStyles.Float, IFormatProvider? provider = default)
        => Double.TryParse(self, style, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値またはnull</returns>
    public static Decimal? TryParseDecimal(this string self, NumberStyles style = NumberStyles.Number, IFormatProvider? provider = default)
        => Decimal.TryParse(self, style, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static DateTime? TryParseDateTime(this string self, DateTimeStyles style = DateTimeStyles.None, IFormatProvider? provider = default)
        => DateTime.TryParse(self, provider ?? CultureInfo.InvariantCulture, style, out var result) ? result : null;

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="format">パース書式指定子</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static DateTime? TryParseDateTimeExact(this string self, string format, DateTimeStyles style = DateTimeStyles.None, IFormatProvider? provider = default)
        => DateTime.TryParseExact(self, format, provider ?? CultureInfo.InvariantCulture, style, out var result) ? result : null;

}
