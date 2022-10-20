
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
    /// <returns>パース結果値</returns>
    public static Byte ParseUInt8(this string self, IFormatProvider? provider = default)
    {
        return Byte.Parse(self, provider);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static Byte ParseUInt8(this string self, NumberStyles style, IFormatProvider? provider = default)
    {
        return Byte.Parse(self, style, provider);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static UInt16 ParseUInt16(this string self, IFormatProvider? provider = default)
    {
        return UInt16.Parse(self, provider);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static UInt16 ParseUInt16(this string self, NumberStyles style, IFormatProvider? provider = default)
    {
        return UInt16.Parse(self, style, provider);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static UInt32 ParseUInt32(this string self, IFormatProvider? provider = default)
    {
        return UInt32.Parse(self, provider);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static UInt32 ParseUInt32(this string self, NumberStyles style, IFormatProvider? provider = default)
    {
        return UInt32.Parse(self, style, provider);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static UInt64 ParseUInt64(this string self, IFormatProvider? provider = default)
    {
        return UInt64.Parse(self, provider);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static UInt64 ParseUInt64(this string self, NumberStyles style, IFormatProvider? provider = default)
    {
        return UInt64.Parse(self, style, provider);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static SByte ParseInt8(this string self, IFormatProvider? provider = default)
    {
        return SByte.Parse(self, provider);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static SByte ParseInt8(this string self, NumberStyles style, IFormatProvider? provider = default)
    {
        return SByte.Parse(self, style, provider);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static Int16 ParseInt16(this string self, IFormatProvider? provider = default)
    {
        return Int16.Parse(self, provider);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static Int16 ParseInt16(this string self, NumberStyles style, IFormatProvider? provider = default)
    {
        return Int16.Parse(self, style, provider);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static Int32 ParseInt32(this string self, IFormatProvider? provider = default)
    {
        return Int32.Parse(self, provider);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static Int32 ParseInt32(this string self, NumberStyles style, IFormatProvider? provider = default)
    {
        return Int32.Parse(self, style, provider);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static Int64 ParseInt64(this string self, IFormatProvider? provider = default)
    {
        return Int64.Parse(self, provider);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static Int64 ParseInt64(this string self, NumberStyles style, IFormatProvider? provider = default)
    {
        return Int64.Parse(self, style, provider);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static Single ParseFloat(this string self, IFormatProvider? provider = default)
    {
        return Single.Parse(self, provider);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static Single ParseFloat(this string self, NumberStyles style, IFormatProvider? provider = default)
    {
        return Single.Parse(self, style, provider);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static Double ParseDouble(this string self, IFormatProvider? provider = default)
    {
        return Double.Parse(self, provider);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static Double ParseDouble(this string self, NumberStyles style, IFormatProvider? provider = default)
    {
        return Double.Parse(self, style, provider);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static Decimal ParseDecimal(this string self, IFormatProvider? provider = default)
    {
        return Decimal.Parse(self, provider);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static Decimal ParseDecimal(this string self, NumberStyles style, IFormatProvider? provider = default)
    {
        return Decimal.Parse(self, style, provider);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static DateTime ParseDateTime(this string self, IFormatProvider? provider = default)
    {
        return DateTime.Parse(self, provider);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static DateTime ParseDateTime(this string self, DateTimeStyles style, IFormatProvider? provider = default)
    {
        return DateTime.Parse(self, provider, style);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="format">パース書式指定子</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static DateTime ParseDateTimeExact(this string self, string format, IFormatProvider? provider = default)
    {
        return DateTime.ParseExact(self, format, provider);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="format">パース書式指定子</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static DateTime ParseDateTimeExact(this string self, string format, DateTimeStyles style, IFormatProvider? provider = default)
    {
        return DateTime.ParseExact(self, format, provider, style);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>パース結果値</returns>
    public static Byte ParseInvariantUInt8(this string self)
    {
        return Byte.Parse(self, CultureInfo.InvariantCulture);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static Byte ParseInvariantUInt8(this string self, NumberStyles style)
    {
        return Byte.Parse(self, style, CultureInfo.InvariantCulture);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>パース結果値</returns>
    public static UInt16 ParseInvariantUInt16(this string self)
    {
        return UInt16.Parse(self);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static UInt16 ParseInvariantUInt16(this string self, NumberStyles style)
    {
        return UInt16.Parse(self, style, CultureInfo.InvariantCulture);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>パース結果値</returns>
    public static UInt32 ParseInvariantUInt32(this string self)
    {
        return UInt32.Parse(self, CultureInfo.InvariantCulture);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static UInt32 ParseInvariantUInt32(this string self, NumberStyles style)
    {
        return UInt32.Parse(self, style, CultureInfo.InvariantCulture);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>パース結果値</returns>
    public static UInt64 ParseInvariantUInt64(this string self)
    {
        return UInt64.Parse(self, CultureInfo.InvariantCulture);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static UInt64 ParseInvariantUInt64(this string self, NumberStyles style)
    {
        return UInt64.Parse(self, style, CultureInfo.InvariantCulture);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>パース結果値</returns>
    public static SByte ParseInvariantInt8(this string self)
    {
        return SByte.Parse(self, CultureInfo.InvariantCulture);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static SByte ParseInvariantInt8(this string self, NumberStyles style)
    {
        return SByte.Parse(self, style, CultureInfo.InvariantCulture);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>パース結果値</returns>
    public static Int16 ParseInvariantInt16(this string self)
    {
        return Int16.Parse(self, CultureInfo.InvariantCulture);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static Int16 ParseInvariantInt16(this string self, NumberStyles style)
    {
        return Int16.Parse(self, style, CultureInfo.InvariantCulture);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>パース結果値</returns>
    public static Int32 ParseInvariantInt32(this string self)
    {
        return Int32.Parse(self, CultureInfo.InvariantCulture);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static Int32 ParseInvariantInt32(this string self, NumberStyles style)
    {
        return Int32.Parse(self, style, CultureInfo.InvariantCulture);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>パース結果値</returns>
    public static Int64 ParseInvariantInt64(this string self)
    {
        return Int64.Parse(self, CultureInfo.InvariantCulture);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static Int64 ParseInvariantInt64(this string self, NumberStyles style)
    {
        return Int64.Parse(self, style, CultureInfo.InvariantCulture);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>パース結果値</returns>
    public static Single ParseInvariantFloat(this string self)
    {
        return Single.Parse(self, CultureInfo.InvariantCulture);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static Single ParseInvariantFloat(this string self, NumberStyles style)
    {
        return Single.Parse(self, style, CultureInfo.InvariantCulture);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>パース結果値</returns>
    public static Double ParseInvariantDouble(this string self)
    {
        return Double.Parse(self, CultureInfo.InvariantCulture);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static Double ParseInvariantDouble(this string self, NumberStyles style)
    {
        return Double.Parse(self, style, CultureInfo.InvariantCulture);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>パース結果値</returns>
    public static Decimal ParseInvariantDecimal(this string self)
    {
        return Decimal.Parse(self, CultureInfo.InvariantCulture);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static Decimal ParseInvariantDecimal(this string self, NumberStyles style)
    {
        return Decimal.Parse(self, style, CultureInfo.InvariantCulture);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>パース結果値</returns>
    public static DateTime ParseInvariantDateTime(this string self)
    {
        return DateTime.Parse(self, CultureInfo.InvariantCulture);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static DateTime ParseInvariantDateTime(this string self, DateTimeStyles style)
    {
        return DateTime.Parse(self, CultureInfo.InvariantCulture, style);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="format">パース書式指定子</param>
    /// <returns>パース結果値</returns>
    public static DateTime ParseInvariantDateTimeExact(this string self, string format)
    {
        return DateTime.ParseExact(self, format, CultureInfo.InvariantCulture);
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="format">パース書式指定子</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static DateTime ParseInvariantDateTimeExact(this string self, string format, DateTimeStyles style)
    {
        return DateTime.ParseExact(self, format, CultureInfo.InvariantCulture, style);
    }

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static Byte? TryParseUInt8(this string self, IFormatProvider? provider = default)
    {
        return Byte.TryParse(self, NumberStyles.Integer, provider, out var result) ? result : null;
    }

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値またはnull</returns>
    public static Byte? TryParseUInt8(this string self, NumberStyles style, IFormatProvider? provider = default)
    {
        return Byte.TryParse(self, style, provider, out var result) ? result : null;
    }

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt16? TryParseUInt16(this string self, IFormatProvider? provider = default)
    {
        return UInt16.TryParse(self, NumberStyles.Integer, provider, out var result) ? result : null;
    }

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt16? TryParseUInt16(this string self, NumberStyles style, IFormatProvider? provider = default)
    {
        return UInt16.TryParse(self, style, provider, out var result) ? result : null;
    }

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt32? TryParseUInt32(this string self, IFormatProvider? provider = default)
    {
        return UInt32.TryParse(self, NumberStyles.Integer, provider, out var result) ? result : null;
    }

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt32? TryParseUInt32(this string self, NumberStyles style, IFormatProvider? provider = default)
    {
        return UInt32.TryParse(self, style, provider, out var result) ? result : null;
    }

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt64? TryParseUInt64(this string self, IFormatProvider? provider = default)
    {
        return UInt64.TryParse(self, NumberStyles.Integer, provider, out var result) ? result : null;
    }

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt64? TryParseUInt64(this string self, NumberStyles style, IFormatProvider? provider = default)
    {
        return UInt64.TryParse(self, style, provider, out var result) ? result : null;
    }

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static SByte? TryParseInt8(this string self, IFormatProvider? provider = default)
    {
        return SByte.TryParse(self, NumberStyles.Integer, provider, out var result) ? result : null;
    }

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値またはnull</returns>
    public static SByte? TryParseInt8(this string self, NumberStyles style, IFormatProvider? provider = default)
    {
        return SByte.TryParse(self, style, provider, out var result) ? result : null;
    }

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static Int16? TryParseInt16(this string self, IFormatProvider? provider = default)
    {
        return Int16.TryParse(self, NumberStyles.Integer, provider, out var result) ? result : null;
    }

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値またはnull</returns>
    public static Int16? TryParseInt16(this string self, NumberStyles style, IFormatProvider? provider = default)
    {
        return Int16.TryParse(self, style, provider, out var result) ? result : null;
    }

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static Int32? TryParseInt32(this string self, IFormatProvider? provider = default)
    {
        return Int32.TryParse(self, NumberStyles.Integer, provider, out var result) ? result : null;
    }

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値またはnull</returns>
    public static Int32? TryParseInt32(this string self, NumberStyles style, IFormatProvider? provider = default)
    {
        return Int32.TryParse(self, style, provider, out var result) ? result : null;
    }

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static Int64? TryParseInt64(this string self, IFormatProvider? provider = default)
    {
        return Int64.TryParse(self, NumberStyles.Integer, provider, out var result) ? result : null;
    }

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値またはnull</returns>
    public static Int64? TryParseInt64(this string self, NumberStyles style, IFormatProvider? provider = default)
    {
        return Int64.TryParse(self, style, provider, out var result) ? result : null;
    }

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static Single? TryParseFloat(this string self, IFormatProvider? provider = default)
    {
        return Single.TryParse(self, NumberStyles.Float, provider, out var result) ? result : null;
    }

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値またはnull</returns>
    public static Single? TryParseFloat(this string self, NumberStyles style, IFormatProvider? provider = default)
    {
        return Single.TryParse(self, style, provider, out var result) ? result : null;
    }

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static Double? TryParseDouble(this string self, IFormatProvider? provider = default)
    {
        return Double.TryParse(self, NumberStyles.Float, provider, out var result) ? result : null;
    }

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値またはnull</returns>
    public static Double? TryParseDouble(this string self, NumberStyles style, IFormatProvider? provider = default)
    {
        return Double.TryParse(self, style, provider, out var result) ? result : null;
    }

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static Decimal? TryParseDecimal(this string self, IFormatProvider? provider = default)
    {
        return Decimal.TryParse(self, NumberStyles.Number, provider, out var result) ? result : null;
    }

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値またはnull</returns>
    public static Decimal? TryParseDecimal(this string self, NumberStyles style, IFormatProvider? provider = default)
    {
        return Decimal.TryParse(self, style, provider, out var result) ? result : null;
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static DateTime? TryParseDateTime(this string self, IFormatProvider? provider = default)
    {
        return DateTime.TryParse(self, provider, DateTimeStyles.None, out var result) ? result : null;
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static DateTime? TryParseDateTime(this string self, DateTimeStyles style, IFormatProvider? provider = default)
    {
        return DateTime.TryParse(self, provider, style, out var result) ? result : null;
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="format">パース書式指定子</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static DateTime? TryParseDateTimeExact(this string self, string format, IFormatProvider? provider = default)
    {
        return DateTime.TryParseExact(self, format, provider, DateTimeStyles.None, out var result) ? result : null;
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="format">パース書式指定子</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static DateTime? TryParseDateTimeExact(this string self, string format, DateTimeStyles style, IFormatProvider? provider = default)
    {
        return DateTime.TryParseExact(self, format, provider, style, out var result) ? result : null;
    }

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>パース結果値またはnull</returns>
    public static Byte? TryParseInvariantUInt8(this string self)
    {
        return Byte.TryParse(self, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result) ? result : null;
    }

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値またはnull</returns>
    public static Byte? TryParseInvariantUInt8(this string self, NumberStyles style)
    {
        return Byte.TryParse(self, style, CultureInfo.InvariantCulture, out var result) ? result : null;
    }

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt16? TryParseInvariantUInt16(this string self)
    {
        return UInt16.TryParse(self, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result) ? result : null;
    }

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt16? TryParseInvariantUInt16(this string self, NumberStyles style)
    {
        return UInt16.TryParse(self, style, CultureInfo.InvariantCulture, out var result) ? result : null;
    }

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt32? TryParseInvariantUInt32(this string self)
    {
        return UInt32.TryParse(self, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result) ? result : null;
    }

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt32? TryParseInvariantUInt32(this string self, NumberStyles style)
    {
        return UInt32.TryParse(self, style, CultureInfo.InvariantCulture, out var result) ? result : null;
    }

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt64? TryParseInvariantUInt64(this string self)
    {
        return UInt64.TryParse(self, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result) ? result : null;
    }

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt64? TryParseInvariantUInt64(this string self, NumberStyles style)
    {
        return UInt64.TryParse(self, style, CultureInfo.InvariantCulture, out var result) ? result : null;
    }

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>パース結果値またはnull</returns>
    public static SByte? TryParseInvariantInt8(this string self)
    {
        return SByte.TryParse(self, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result) ? result : null;
    }

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値またはnull</returns>
    public static SByte? TryParseInvariantInt8(this string self, NumberStyles style)
    {
        return SByte.TryParse(self, style, CultureInfo.InvariantCulture, out var result) ? result : null;
    }

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>パース結果値またはnull</returns>
    public static Int16? TryParseInvariantInt16(this string self)
    {
        return Int16.TryParse(self, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result) ? result : null;
    }

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値またはnull</returns>
    public static Int16? TryParseInvariantInt16(this string self, NumberStyles style)
    {
        return Int16.TryParse(self, style, CultureInfo.InvariantCulture, out var result) ? result : null;
    }

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>パース結果値またはnull</returns>
    public static Int32? TryParseInvariantInt32(this string self)
    {
        return Int32.TryParse(self, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result) ? result : null;
    }

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値またはnull</returns>
    public static Int32? TryParseInvariantInt32(this string self, NumberStyles style)
    {
        return Int32.TryParse(self, style, CultureInfo.InvariantCulture, out var result) ? result : null;
    }

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>パース結果値またはnull</returns>
    public static Int64? TryParseInvariantInt64(this string self)
    {
        return Int64.TryParse(self, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result) ? result : null;
    }

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値またはnull</returns>
    public static Int64? TryParseInvariantInt64(this string self, NumberStyles style)
    {
        return Int64.TryParse(self, style, CultureInfo.InvariantCulture, out var result) ? result : null;
    }

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>パース結果値またはnull</returns>
    public static Single? TryParseInvariantFloat(this string self)
    {
        return Single.TryParse(self, NumberStyles.Float, CultureInfo.InvariantCulture, out var result) ? result : null;
    }

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値またはnull</returns>
    public static Single? TryParseInvariantFloat(this string self, NumberStyles style)
    {
        return Single.TryParse(self, style, CultureInfo.InvariantCulture, out var result) ? result : null;
    }

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>パース結果値またはnull</returns>
    public static Double? TryParseInvariantDouble(this string self)
    {
        return Double.TryParse(self, NumberStyles.Float, CultureInfo.InvariantCulture, out var result) ? result : null;
    }

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値またはnull</returns>
    public static Double? TryParseInvariantDouble(this string self, NumberStyles style)
    {
        return Double.TryParse(self, style, CultureInfo.InvariantCulture, out var result) ? result : null;
    }

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>パース結果値またはnull</returns>
    public static Decimal? TryParseInvariantDecimal(this string self)
    {
        return Decimal.TryParse(self, NumberStyles.Number, CultureInfo.InvariantCulture, out var result) ? result : null;
    }

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値またはnull</returns>
    public static Decimal? TryParseInvariantDecimal(this string self, NumberStyles style)
    {
        return Decimal.TryParse(self, style, CultureInfo.InvariantCulture, out var result) ? result : null;
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>パース結果値</returns>
    public static DateTime? TryParseInvariantDateTime(this string self)
    {
        return DateTime.TryParse(self, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result) ? result : null;
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static DateTime? TryParseInvariantDateTime(this string self, DateTimeStyles style)
    {
        return DateTime.TryParse(self, CultureInfo.InvariantCulture, style, out var result) ? result : null;
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="format">パース書式指定子</param>
    /// <returns>パース結果値</returns>
    public static DateTime? TryParseInvariantDateTimeExact(this string self, string format)
    {
        return DateTime.TryParseExact(self, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result) ? result : null;
    }

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="format">パース書式指定子</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static DateTime? TryParseInvariantDateTimeExact(this string self, string format, DateTimeStyles style)
    {
        return DateTime.TryParseExact(self, format, CultureInfo.InvariantCulture, style, out var result) ? result : null;
    }



}
