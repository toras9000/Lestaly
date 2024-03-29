﻿using System.Globalization;
#if NET7_0_OR_GREATER
using System.Numerics;
#endif

namespace Lestaly;

/// <summary>
/// 文字列のパース関連の拡張メソッド
/// </summary>
public static class StringParseExtensions
{
    #region Parse
    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static Byte ParseUInt8(this string self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => Byte.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static Byte ParseUInt8(this Span<char> self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => Byte.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static Byte ParseUInt8(this ReadOnlySpan<char> self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => Byte.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static UInt16 ParseUInt16(this string self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => UInt16.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static UInt16 ParseUInt16(this Span<char> self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => UInt16.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static UInt16 ParseUInt16(this ReadOnlySpan<char> self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => UInt16.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static UInt32 ParseUInt32(this string self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => UInt32.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static UInt32 ParseUInt32(this Span<char> self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => UInt32.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static UInt32 ParseUInt32(this ReadOnlySpan<char> self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => UInt32.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static UInt64 ParseUInt64(this string self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => UInt64.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static UInt64 ParseUInt64(this Span<char> self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => UInt64.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static UInt64 ParseUInt64(this ReadOnlySpan<char> self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => UInt64.Parse(self, style, provider ?? CultureInfo.InvariantCulture);


    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static SByte ParseInt8(this string self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => SByte.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static SByte ParseInt8(this Span<char> self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => SByte.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static SByte ParseInt8(this ReadOnlySpan<char> self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => SByte.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static Int16 ParseInt16(this string self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => Int16.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
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
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static Int32 ParseInt32(this string self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => Int32.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static Int32 ParseInt32(this Span<char> self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => Int32.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static Int32 ParseInt32(this ReadOnlySpan<char> self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => Int32.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static Int64 ParseInt64(this string self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => Int64.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static Int64 ParseInt64(this Span<char> self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => Int64.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static Int64 ParseInt64(this ReadOnlySpan<char> self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => Int64.Parse(self, style, provider ?? CultureInfo.InvariantCulture);


    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static Single ParseFloat(this string self, NumberStyles style = NumberStyles.AllowThousands | NumberStyles.Float, IFormatProvider? provider = default)
        => Single.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static Single ParseFloat(this Span<char> self, NumberStyles style = NumberStyles.AllowThousands | NumberStyles.Float, IFormatProvider? provider = default)
        => Single.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static Single ParseFloat(this ReadOnlySpan<char> self, NumberStyles style = NumberStyles.AllowThousands | NumberStyles.Float, IFormatProvider? provider = default)
        => Single.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static Double ParseDouble(this string self, NumberStyles style = NumberStyles.AllowThousands | NumberStyles.Float, IFormatProvider? provider = default)
        => Double.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static Double ParseDouble(this Span<char> self, NumberStyles style = NumberStyles.AllowThousands | NumberStyles.Float, IFormatProvider? provider = default)
        => Double.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static Double ParseDouble(this ReadOnlySpan<char> self, NumberStyles style = NumberStyles.AllowThousands | NumberStyles.Float, IFormatProvider? provider = default)
        => Double.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static Decimal ParseDecimal(this string self, NumberStyles style = NumberStyles.Number, IFormatProvider? provider = default)
        => Decimal.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static Decimal ParseDecimal(this Span<char> self, NumberStyles style = NumberStyles.Number, IFormatProvider? provider = default)
        => Decimal.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static Decimal ParseDecimal(this ReadOnlySpan<char> self, NumberStyles style = NumberStyles.Number, IFormatProvider? provider = default)
        => Decimal.Parse(self, style, provider ?? CultureInfo.InvariantCulture);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static DateTime ParseDateTime(this string self, DateTimeStyles style = DateTimeStyles.None, IFormatProvider? provider = default)
        => DateTime.Parse(self, provider ?? CultureInfo.InvariantCulture, style);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static DateTime ParseDateTime(this Span<char> self, DateTimeStyles style = DateTimeStyles.None, IFormatProvider? provider = default)
        => DateTime.Parse(self, provider ?? CultureInfo.InvariantCulture, style);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static DateTime ParseDateTime(this ReadOnlySpan<char> self, DateTimeStyles style = DateTimeStyles.None, IFormatProvider? provider = default)
        => DateTime.Parse(self, provider ?? CultureInfo.InvariantCulture, style);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="format">パース書式指定子</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static DateTime ParseDateTimeExact(this string self, string format, DateTimeStyles style = DateTimeStyles.None, IFormatProvider? provider = default)
        => DateTime.ParseExact(self, format, provider ?? CultureInfo.InvariantCulture, style);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="format">パース書式指定子</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static DateTime ParseDateTimeExact(this Span<char> self, string format, DateTimeStyles style = DateTimeStyles.None, IFormatProvider? provider = default)
        => DateTime.ParseExact(self, format, provider ?? CultureInfo.InvariantCulture, style);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="format">パース書式指定子</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static DateTime ParseDateTimeExact(this ReadOnlySpan<char> self, string format, DateTimeStyles style = DateTimeStyles.None, IFormatProvider? provider = default)
        => DateTime.ParseExact(self, format, provider ?? CultureInfo.InvariantCulture, style);

#if NET7_0_OR_GREATER
    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static TResult ParseNumber<TResult>(this string self, NumberStyles? style = default, IFormatProvider? provider = default) where TResult : struct, INumber<TResult>
        => self.AsSpan().ParseNumber<TResult>(style, provider);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static TResult ParseNumber<TResult>(this Span<char> self, NumberStyles? style = default, IFormatProvider? provider = default) where TResult : struct, INumber<TResult>
        => ((ReadOnlySpan<char>)self).ParseNumber<TResult>(style, provider);

    /// <summary>文字列をパースする。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <param name="style">パース書式</param>
    /// <returns>パース結果値</returns>
    public static TResult ParseNumber<TResult>(this ReadOnlySpan<char> self, NumberStyles? style = default, IFormatProvider? provider = default) where TResult : struct, INumber<TResult>
        => style.HasValue ? TResult.Parse(self, style.Value, provider ?? CultureInfo.InvariantCulture)
                          : TResult.Parse(self, provider ?? CultureInfo.InvariantCulture);
#endif
    #endregion

    #region TryParse
    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static Byte? TryParseUInt8(this string self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => Byte.TryParse(self, style, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static Byte? TryParseUInt8(this Span<char> self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => Byte.TryParse(self, style, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static Byte? TryParseUInt8(this ReadOnlySpan<char> self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => Byte.TryParse(self, style, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt16? TryParseUInt16(this string self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => UInt16.TryParse(self, style, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt16? TryParseUInt16(this Span<char> self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => UInt16.TryParse(self, style, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt16? TryParseUInt16(this ReadOnlySpan<char> self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => UInt16.TryParse(self, style, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt32? TryParseUInt32(this string self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => UInt32.TryParse(self, style, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt32? TryParseUInt32(this Span<char> self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => UInt32.TryParse(self, style, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt32? TryParseUInt32(this ReadOnlySpan<char> self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => UInt32.TryParse(self, style, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt64? TryParseUInt64(this string self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => UInt64.TryParse(self, style, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt64? TryParseUInt64(this Span<char> self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => UInt64.TryParse(self, style, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt64? TryParseUInt64(this ReadOnlySpan<char> self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => UInt64.TryParse(self, style, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;


    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static SByte? TryParseInt8(this string self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => SByte.TryParse(self, style, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static SByte? TryParseInt8(this Span<char> self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => SByte.TryParse(self, style, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static SByte? TryParseInt8(this ReadOnlySpan<char> self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => SByte.TryParse(self, style, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static Int16? TryParseInt16(this string self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => Int16.TryParse(self, style, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static Int16? TryParseInt16(this Span<char> self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => Int16.TryParse(self, style, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static Int16? TryParseInt16(this ReadOnlySpan<char> self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => Int16.TryParse(self, style, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static Int32? TryParseInt32(this string self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => Int32.TryParse(self, style, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static Int32? TryParseInt32(this Span<char> self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => Int32.TryParse(self, style, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static Int32? TryParseInt32(this ReadOnlySpan<char> self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => Int32.TryParse(self, style, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static Int64? TryParseInt64(this string self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => Int64.TryParse(self, style, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static Int64? TryParseInt64(this Span<char> self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => Int64.TryParse(self, style, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static Int64? TryParseInt64(this ReadOnlySpan<char> self, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default)
        => Int64.TryParse(self, style, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;


    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static Single? TryParseFloat(this string self, NumberStyles style = NumberStyles.AllowThousands | NumberStyles.Float, IFormatProvider? provider = default)
        => Single.TryParse(self, style, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static Single? TryParseFloat(this Span<char> self, NumberStyles style = NumberStyles.AllowThousands | NumberStyles.Float, IFormatProvider? provider = default)
        => Single.TryParse(self, style, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static Single? TryParseFloat(this ReadOnlySpan<char> self, NumberStyles style = NumberStyles.AllowThousands | NumberStyles.Float, IFormatProvider? provider = default)
        => Single.TryParse(self, style, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static Double? TryParseDouble(this string self, NumberStyles style = NumberStyles.AllowThousands | NumberStyles.Float, IFormatProvider? provider = default)
        => Double.TryParse(self, style, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static Double? TryParseDouble(this Span<char> self, NumberStyles style = NumberStyles.AllowThousands | NumberStyles.Float, IFormatProvider? provider = default)
        => Double.TryParse(self, style, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static Double? TryParseDouble(this ReadOnlySpan<char> self, NumberStyles style = NumberStyles.AllowThousands | NumberStyles.Float, IFormatProvider? provider = default)
        => Double.TryParse(self, style, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static Decimal? TryParseDecimal(this string self, NumberStyles style = NumberStyles.Number, IFormatProvider? provider = default)
        => Decimal.TryParse(self, style, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static Decimal? TryParseDecimal(this Span<char> self, NumberStyles style = NumberStyles.Number, IFormatProvider? provider = default)
        => Decimal.TryParse(self, style, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static Decimal? TryParseDecimal(this ReadOnlySpan<char> self, NumberStyles style = NumberStyles.Number, IFormatProvider? provider = default)
        => Decimal.TryParse(self, style, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static DateTime? TryParseDateTime(this string self, DateTimeStyles style = DateTimeStyles.None, IFormatProvider? provider = default)
        => DateTime.TryParse(self, provider ?? CultureInfo.InvariantCulture, style, out var result) ? result : null;

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static DateTime? TryParseDateTime(this Span<char> self, DateTimeStyles style = DateTimeStyles.None, IFormatProvider? provider = default)
        => DateTime.TryParse(self, provider ?? CultureInfo.InvariantCulture, style, out var result) ? result : null;

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static DateTime? TryParseDateTime(this ReadOnlySpan<char> self, DateTimeStyles style = DateTimeStyles.None, IFormatProvider? provider = default)
        => DateTime.TryParse(self, provider ?? CultureInfo.InvariantCulture, style, out var result) ? result : null;

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="format">パース書式指定子</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static DateTime? TryParseDateTimeExact(this string self, string format, DateTimeStyles style = DateTimeStyles.None, IFormatProvider? provider = default)
        => DateTime.TryParseExact(self, format, provider ?? CultureInfo.InvariantCulture, style, out var result) ? result : null;

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="format">パース書式指定子</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static DateTime? TryParseDateTimeExact(this Span<char> self, string format, DateTimeStyles style = DateTimeStyles.None, IFormatProvider? provider = default)
        => DateTime.TryParseExact(self, format, provider ?? CultureInfo.InvariantCulture, style, out var result) ? result : null;

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="format">パース書式指定子</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値</returns>
    public static DateTime? TryParseDateTimeExact(this ReadOnlySpan<char> self, string format, DateTimeStyles style = DateTimeStyles.None, IFormatProvider? provider = default)
        => DateTime.TryParseExact(self, format, provider ?? CultureInfo.InvariantCulture, style, out var result) ? result : null;

#if NET7_0_OR_GREATER
    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static TResult? TryParseNumber<TResult>(this string self, NumberStyles? style = default, IFormatProvider? provider = default) where TResult : struct, INumber<TResult>
        => self.AsSpan().TryParseNumber<TResult>(style, provider);

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static TResult? TryParseNumber<TResult>(this Span<char> self, NumberStyles? style = default, IFormatProvider? provider = default) where TResult : struct, INumber<TResult>
        => ((ReadOnlySpan<char>)self).TryParseNumber<TResult>(style, provider);

    /// <summary>文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="style">パース書式</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static TResult? TryParseNumber<TResult>(this ReadOnlySpan<char> self, NumberStyles? style = default, IFormatProvider? provider = default) where TResult : struct, INumber<TResult>
        => style.HasValue ? TResult.TryParse(self, style.Value, provider ?? CultureInfo.InvariantCulture, out var result1) ? result1 : null
                          : TResult.TryParse(self, provider ?? CultureInfo.InvariantCulture, out var result2) ? result2 : null;
#endif
    #endregion

    #region TryParseHex
    /// <summary>16進数として文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static Byte? TryParseHexNumber8(this string self, IFormatProvider? provider = default)
        => Byte.TryParse(self, NumberStyles.HexNumber, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>16進数として文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static Byte? TryParseHexNumber8(this Span<char> self, IFormatProvider? provider = default)
        => Byte.TryParse(self, NumberStyles.HexNumber, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>16進数として文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static Byte? TryParseHexNumber8(this ReadOnlySpan<char> self, IFormatProvider? provider = default)
        => Byte.TryParse(self, NumberStyles.HexNumber, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>16進数として文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt16? TryParseHexNumber16(this string self, IFormatProvider? provider = default)
        => UInt16.TryParse(self, NumberStyles.HexNumber, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>16進数として文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt16? TryParseHexNumber16(this Span<char> self, IFormatProvider? provider = default)
        => UInt16.TryParse(self, NumberStyles.HexNumber, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>16進数として文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt16? TryParseHexNumber16(this ReadOnlySpan<char> self, IFormatProvider? provider = default)
        => UInt16.TryParse(self, NumberStyles.HexNumber, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>16進数として文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt32? TryParseHexNumber32(this string self, IFormatProvider? provider = default)
        => UInt32.TryParse(self, NumberStyles.HexNumber, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>16進数として文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt32? TryParseHexNumber32(this Span<char> self, IFormatProvider? provider = default)
        => UInt32.TryParse(self, NumberStyles.HexNumber, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>16進数として文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt32? TryParseHexNumber32(this ReadOnlySpan<char> self, IFormatProvider? provider = default)
        => UInt32.TryParse(self, NumberStyles.HexNumber, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>16進数として文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt64? TryParseHexNumber64(this string self, IFormatProvider? provider = default)
        => UInt64.TryParse(self, NumberStyles.HexNumber, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>16進数として文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt64? TryParseHexNumber64(this Span<char> self, IFormatProvider? provider = default)
        => UInt64.TryParse(self, NumberStyles.HexNumber, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>16進数として文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt64? TryParseHexNumber64(this ReadOnlySpan<char> self, IFormatProvider? provider = default)
        => UInt64.TryParse(self, NumberStyles.HexNumber, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

#if NET7_0_OR_GREATER
    /// <summary>16進数として文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static TResult? TryParseHexNumber<TResult>(this string self, IFormatProvider? provider = default) where TResult : struct, INumber<TResult>
        => self.AsSpan().TryParseHexNumber<TResult>(provider);

    /// <summary>16進数として文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static TResult? TryParseHexNumber<TResult>(this Span<char> self, IFormatProvider? provider = default) where TResult : struct, INumber<TResult>
        => ((ReadOnlySpan<char>)self).TryParseHexNumber<TResult>(provider);

    /// <summary>16進数として文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static TResult? TryParseHexNumber<TResult>(this ReadOnlySpan<char> self, IFormatProvider? provider = default) where TResult : struct, INumber<TResult>
        => TResult.TryParse(self, NumberStyles.HexNumber, provider ?? CultureInfo.InvariantCulture, out var result) ? result : null;

    /// <summary>修飾文字列などを含めて16進数として文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static TResult? TryParseHex<TResult>(this string self, IFormatProvider? provider = default) where TResult : struct, INumber<TResult>
        => self.AsSpan().TryParseHex<TResult>(provider);

    /// <summary>修飾文字列などを含めて16進数として文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static TResult? TryParseHex<TResult>(this Span<char> self, IFormatProvider? provider = default) where TResult : struct, INumber<TResult>
        => ((ReadOnlySpan<char>)self).TryParseHex<TResult>(provider);

    /// <summary>修飾文字列などを含めて16進数として文字列のパースを試みる。</summary>
    /// <remarks>
    /// 修飾文字列が付与されていればそれを除去した部分文字列を、無ければ文字列全体を2進数としてパースする。
    /// 修飾文字列の大文字・小文字は問わず、プレフィクスとして 0x, &amp;h, # を、サフィックスとして h を認識する。
    /// </remarks>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static TResult? TryParseHex<TResult>(this ReadOnlySpan<char> self, IFormatProvider? provider = default) where TResult : struct, INumber<TResult>
    {
        const string HexPrefix1 = "0x";
        if (self.StartsWith(HexPrefix1, StringComparison.OrdinalIgnoreCase)) return self[HexPrefix1.Length..].TryParseHexNumber<TResult>(provider);

        const string HexPrefix2 = "&H";
        if (self.StartsWith(HexPrefix2, StringComparison.OrdinalIgnoreCase)) return self[HexPrefix2.Length..].TryParseHexNumber<TResult>(provider);

        const string HexPrefix3 = "#";
        if (self.StartsWith(HexPrefix3, StringComparison.OrdinalIgnoreCase)) return self[HexPrefix3.Length..].TryParseHexNumber<TResult>(provider);

        const string HexSuffix1 = "H";
        if (self.EndsWith(HexSuffix1, StringComparison.OrdinalIgnoreCase)) return self[..^HexSuffix1.Length].TryParseHexNumber<TResult>(provider);

        return self.TryParseHexNumber<TResult>(provider);
    }
#endif
    #endregion

    #region TryParseBin
    /// <summary>2進数として文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="trim">前後空白をトリムするか否か</param>
    /// <param name="snake">アンダーバーによる区切りを許容するか否か</param>
    /// <returns>パース結果値またはnull</returns>
    public static Byte? TryParseBinNumber8(this string self, bool trim = true, bool snake = true)
        => self.AsSpan().TryParseBinNumber8(trim, snake);

    /// <summary>2進数として文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="trim">前後空白をトリムするか否か</param>
    /// <param name="snake">アンダーバーによる区切りを許容するか否か</param>
    /// <returns>パース結果値またはnull</returns>
    public static Byte? TryParseBinNumber8(this Span<char> self, bool trim = true, bool snake = true)
        => ((ReadOnlySpan<char>)self).TryParseBinNumber8(trim, snake);

    /// <summary>2進数として文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="trim">前後空白をトリムするか否か</param>
    /// <param name="snake">アンダーバーによる区切りを許容するか否か</param>
    /// <returns>パース結果値またはnull</returns>
    public static Byte? TryParseBinNumber8(this ReadOnlySpan<char> self, bool trim = true, bool snake = true)
        => (Byte?)TryParseBinNumber64Core(self, 8, trim, snake);

    /// <summary>2進数として文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="trim">前後空白をトリムするか否か</param>
    /// <param name="snake">アンダーバーによる区切りを許容するか否か</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt16? TryParseBinNumber16(this string self, bool trim = true, bool snake = true)
        => self.AsSpan().TryParseBinNumber16(trim, snake);

    /// <summary>2進数として文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="trim">前後空白をトリムするか否か</param>
    /// <param name="snake">アンダーバーによる区切りを許容するか否か</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt16? TryParseBinNumber16(this Span<char> self, bool trim = true, bool snake = true)
        => ((ReadOnlySpan<char>)self).TryParseBinNumber16(trim, snake);

    /// <summary>2進数として文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="trim">前後空白をトリムするか否か</param>
    /// <param name="snake">アンダーバーによる区切りを許容するか否か</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt16? TryParseBinNumber16(this ReadOnlySpan<char> self, bool trim = true, bool snake = true)
        => (UInt16?)TryParseBinNumber64Core(self, 16, trim, snake);

    /// <summary>2進数として文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="trim">前後空白をトリムするか否か</param>
    /// <param name="snake">アンダーバーによる区切りを許容するか否か</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt32? TryParseBinNumber32(this string self, bool trim = true, bool snake = true)
        => self.AsSpan().TryParseBinNumber32(trim, snake);

    /// <summary>2進数として文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="trim">前後空白をトリムするか否か</param>
    /// <param name="snake">アンダーバーによる区切りを許容するか否か</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt32? TryParseBinNumber32(this Span<char> self, bool trim = true, bool snake = true)
        => ((ReadOnlySpan<char>)self).TryParseBinNumber32(trim, snake);

    /// <summary>2進数として文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="trim">前後空白をトリムするか否か</param>
    /// <param name="snake">アンダーバーによる区切りを許容するか否か</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt32? TryParseBinNumber32(this ReadOnlySpan<char> self, bool trim = true, bool snake = true)
    => (UInt32?)TryParseBinNumber64Core(self, 32, trim, snake);

    /// <summary>2進数として文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="trim">前後空白をトリムするか否か</param>
    /// <param name="snake">アンダーバーによる区切りを許容するか否か</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt64? TryParseBinNumber64(this string self, bool trim = true, bool snake = true)
        => self.AsSpan().TryParseBinNumber64(trim, snake);

    /// <summary>2進数として文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="trim">前後空白をトリムするか否か</param>
    /// <param name="snake">アンダーバーによる区切りを許容するか否か</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt64? TryParseBinNumber64(this Span<char> self, bool trim = true, bool snake = true)
        => ((ReadOnlySpan<char>)self).TryParseBinNumber64(trim, snake);

    /// <summary>2進数として文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="trim">前後空白をトリムするか否か</param>
    /// <param name="snake">アンダーバーによる区切りを許容するか否か</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt64? TryParseBinNumber64(this ReadOnlySpan<char> self, bool trim = true, bool snake = true)
    => TryParseBinNumber64Core(self, 64, trim, snake);

    /// <summary>2進数として文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="width">ビット幅</param>
    /// <param name="trim">前後空白をトリムするか否か</param>
    /// <param name="snake">アンダーバーによる区切りを許容するか否か</param>
    /// <returns>パース結果値またはnull</returns>
    private static UInt64? TryParseBinNumber64Core(ReadOnlySpan<char> self, int width, bool trim = true, bool snake = true)
    {
        // パラメータ指定によってパース対象をトリムするかを決定する
        var body = trim ? self.Trim() : self;

        // 空文字列の場合はパース不可
        if (body.Length <= 0) return null;

        // 末尾からビットとして解釈
        var value = default(UInt64);
        var bit = 0;
        var idx = body.Length - 1;
        while (0 <= idx)
        {
            switch (body[idx])
            {
            case '0':
                if (width <= bit) return null;
                bit++;
                break;

            case '1':
                if (width <= bit) return null;
                value |= (UInt64)(0x01ul << bit);
                bit++;
                break;

            case '_':
                if (!snake) return null;
                break;

            default:
                return null;
            }

            idx--;
        }

        return value;
    }

#if NET7_0_OR_GREATER
    /// <summary>2進数として文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="trim">前後空白をトリムするか否か</param>
    /// <param name="snake">アンダーバーによる区切りを許容するか否か</param>
    /// <returns>パース結果値またはnull</returns>
    public static TResult? TryParseBinNumber<TResult>(this string self, bool trim = true, bool snake = true) where TResult : struct, IBinaryInteger<TResult>
        => self.AsSpan().TryParseBinNumber<TResult>(trim, snake);

    /// <summary>2進数として文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="trim">前後空白をトリムするか否か</param>
    /// <param name="snake">アンダーバーによる区切りを許容するか否か</param>
    /// <returns>パース結果値またはnull</returns>
    public static TResult? TryParseBinNumber<TResult>(this Span<char> self, bool trim = true, bool snake = true) where TResult : struct, IBinaryInteger<TResult>
        => ((ReadOnlySpan<char>)self).TryParseBinNumber<TResult>(trim, snake);

    /// <summary>2進数として文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="trim">前後空白をトリムするか否か</param>
    /// <param name="snake">アンダーバーによる区切りを許容するか否か</param>
    /// <returns>パース結果値またはnull</returns>
    public static TResult? TryParseBinNumber<TResult>(this ReadOnlySpan<char> self, bool trim = true, bool snake = true) where TResult : struct, IBinaryInteger<TResult>
    {
        // パラメータ指定によってパース対象をトリムするかを決定する
        var body = trim ? self.Trim() : self;

        // 空文字列の場合はパース不可
        if (body.Length <= 0) return null;

        // 末尾からビットとして解釈
        var width = TResult.Zero.GetByteCount() * 8;
        var value = default(TResult);
        var bit = 0;
        var idx = body.Length - 1;
        while (0 <= idx)
        {
            switch (body[idx])
            {
            case '0':
                if (width <= bit) return null;
                bit++;
                break;

            case '1':
                if (width <= bit) return null;
                value |= (TResult.One << bit);
                bit++;
                break;

            case '_':
                if (!snake) return null;
                break;

            default:
                return null;
            }

            idx--;
        }

        return value;
    }

    /// <summary>修飾文字列などを含めて2進数として文字列のパースを試みる。</summary>
    /// <remarks>
    /// 修飾文字列が付与されていればそれを除去した部分文字列を、無ければ文字列全体を2進数としてパースする。
    /// 修飾文字列の大文字・小文字は問わず、プレフィクスとして 0b, &amp;b を、サフィックスとして b を認識する。
    /// </remarks>
    /// <param name="self">対象文字列</param>
    /// <param name="trim">前後空白をトリムするか否か</param>
    /// <param name="snake">アンダーバーによる区切りを許容するか否か</param>
    /// <returns>パース結果値またはnull</returns>
    public static TResult? TryParseBin<TResult>(this string self, bool trim = true, bool snake = true) where TResult : struct, IBinaryInteger<TResult>
        => self.AsSpan().TryParseBin<TResult>(trim, snake);

    /// <summary>修飾文字列などを含めて2進数として文字列のパースを試みる。</summary>
    /// <remarks>
    /// 修飾文字列が付与されていればそれを除去した部分文字列を、無ければ文字列全体を2進数としてパースする。
    /// 修飾文字列の大文字・小文字は問わず、プレフィクスとして 0b, &amp;b を、サフィックスとして b を認識する。
    /// </remarks>
    /// <param name="self">対象文字列</param>
    /// <param name="trim">前後空白をトリムするか否か</param>
    /// <param name="snake">アンダーバーによる区切りを許容するか否か</param>
    /// <returns>パース結果値またはnull</returns>
    public static TResult? TryParseBin<TResult>(this Span<char> self, bool trim = true, bool snake = true) where TResult : struct, IBinaryInteger<TResult>
        => ((ReadOnlySpan<char>)self).TryParseBin<TResult>(trim, snake);

    /// <summary>修飾文字列などを含めて2進数として文字列のパースを試みる。</summary>
    /// <remarks>
    /// 修飾文字列が付与されていればそれを除去した部分文字列を、無ければ文字列全体を2進数としてパースする。
    /// 修飾文字列の大文字・小文字は問わず、プレフィクスとして 0b, &amp;b を、サフィックスとして b を認識する。
    /// </remarks>
    /// <param name="self">対象文字列</param>
    /// <param name="trim">前後空白をトリムするか否か</param>
    /// <param name="snake">アンダーバーによる区切りを許容するか否か</param>
    /// <returns>パース結果値またはnull</returns>
    public static TResult? TryParseBin<TResult>(this ReadOnlySpan<char> self, bool trim = true, bool snake = true) where TResult : struct, IBinaryInteger<TResult>
    {
        const string BinPrefix1 = "0b";
        if (self.StartsWith(BinPrefix1, StringComparison.OrdinalIgnoreCase)) return self[BinPrefix1.Length..].TryParseBinNumber<TResult>(trim, snake);

        const string BinPrefix2 = "&B";
        if (self.StartsWith(BinPrefix2, StringComparison.OrdinalIgnoreCase)) return self[BinPrefix2.Length..].TryParseBinNumber<TResult>(trim, snake);

        const string BinSuffix1 = "B";
        if (self.EndsWith(BinSuffix1, StringComparison.OrdinalIgnoreCase)) return self[..^BinSuffix1.Length].TryParseBinNumber<TResult>(trim, snake);

        return self.TryParseBinNumber<TResult>(trim, snake);
    }
#endif
    #endregion

    #region TryParseWithPrefix
    /// <summary>プレフィックス解釈を伴う文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static Byte? TryParseWithPrefix8(this string self, IFormatProvider? provider = default)
        => self.AsSpan().TryParseWithPrefix8(provider);

    /// <summary>プレフィックス解釈を伴う文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static Byte? TryParseWithPrefix8(this Span<char> self, IFormatProvider? provider = default)
        => ((ReadOnlySpan<char>)self).TryParseWithPrefix8(provider);

    /// <summary>プレフィックス解釈を伴う文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static Byte? TryParseWithPrefix8(this ReadOnlySpan<char> self, IFormatProvider? provider = default)
    {
        const string HexPrefix1 = "0x";
        if (self.StartsWith(HexPrefix1, StringComparison.OrdinalIgnoreCase)) return self[HexPrefix1.Length..].TryParseHexNumber8(provider);

        const string HexPrefix2 = "&H";
        if (self.StartsWith(HexPrefix2, StringComparison.OrdinalIgnoreCase)) return self[HexPrefix2.Length..].TryParseHexNumber8(provider);

        const string HexPrefix3 = "#";
        if (self.StartsWith(HexPrefix3, StringComparison.OrdinalIgnoreCase)) return self[HexPrefix3.Length..].TryParseHexNumber8(provider);

        const string BinPrefix1 = "0b";
        if (self.StartsWith(BinPrefix1, StringComparison.OrdinalIgnoreCase)) return self[BinPrefix1.Length..].TryParseBinNumber8(trim: true, snake: true);

        const string BinPrefix2 = "&B";
        if (self.StartsWith(BinPrefix2, StringComparison.OrdinalIgnoreCase)) return self[BinPrefix2.Length..].TryParseBinNumber8(trim: true, snake: true);

        return self.TryParseUInt8(provider: provider);
    }

    /// <summary>プレフィックス解釈を伴う文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt16? TryParseWithPrefix16(this string self, IFormatProvider? provider = default)
        => self.AsSpan().TryParseWithPrefix16(provider);

    /// <summary>プレフィックス解釈を伴う文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt16? TryParseWithPrefix16(this Span<char> self, IFormatProvider? provider = default)
        => ((ReadOnlySpan<char>)self).TryParseWithPrefix16(provider);

    /// <summary>プレフィックス解釈を伴う文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt16? TryParseWithPrefix16(this ReadOnlySpan<char> self, IFormatProvider? provider = default)
    {
        const string HexPrefix1 = "0x";
        if (self.StartsWith(HexPrefix1, StringComparison.OrdinalIgnoreCase)) return self[HexPrefix1.Length..].TryParseHexNumber16(provider);

        const string HexPrefix2 = "&H";
        if (self.StartsWith(HexPrefix2, StringComparison.OrdinalIgnoreCase)) return self[HexPrefix2.Length..].TryParseHexNumber16(provider);

        const string HexPrefix3 = "#";
        if (self.StartsWith(HexPrefix3, StringComparison.OrdinalIgnoreCase)) return self[HexPrefix3.Length..].TryParseHexNumber16(provider);

        const string BinPrefix1 = "0b";
        if (self.StartsWith(BinPrefix1, StringComparison.OrdinalIgnoreCase)) return self[BinPrefix1.Length..].TryParseBinNumber16(trim: true, snake: true);

        const string BinPrefix2 = "&B";
        if (self.StartsWith(BinPrefix2, StringComparison.OrdinalIgnoreCase)) return self[BinPrefix2.Length..].TryParseBinNumber16(trim: true, snake: true);

        return self.TryParseUInt16(provider: provider);
    }

    /// <summary>プレフィックス解釈を伴う文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt32? TryParseWithPrefix32(this string self, IFormatProvider? provider = default)
        => self.AsSpan().TryParseWithPrefix32(provider);

    /// <summary>プレフィックス解釈を伴う文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt32? TryParseWithPrefix32(this Span<char> self, IFormatProvider? provider = default)
        => ((ReadOnlySpan<char>)self).TryParseWithPrefix32(provider);

    /// <summary>プレフィックス解釈を伴う文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt32? TryParseWithPrefix32(this ReadOnlySpan<char> self, IFormatProvider? provider = default)
    {
        const string HexPrefix1 = "0x";
        if (self.StartsWith(HexPrefix1, StringComparison.OrdinalIgnoreCase)) return self[HexPrefix1.Length..].TryParseHexNumber32(provider);

        const string HexPrefix2 = "&H";
        if (self.StartsWith(HexPrefix2, StringComparison.OrdinalIgnoreCase)) return self[HexPrefix2.Length..].TryParseHexNumber32(provider);

        const string HexPrefix3 = "#";
        if (self.StartsWith(HexPrefix3, StringComparison.OrdinalIgnoreCase)) return self[HexPrefix3.Length..].TryParseHexNumber32(provider);

        const string BinPrefix1 = "0b";
        if (self.StartsWith(BinPrefix1, StringComparison.OrdinalIgnoreCase)) return self[BinPrefix1.Length..].TryParseBinNumber32(trim: true, snake: true);

        const string BinPrefix2 = "&B";
        if (self.StartsWith(BinPrefix2, StringComparison.OrdinalIgnoreCase)) return self[BinPrefix2.Length..].TryParseBinNumber32(trim: true, snake: true);

        return self.TryParseUInt32(provider: provider);
    }

    /// <summary>プレフィックス解釈を伴う文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt64? TryParseWithPrefix64(this string self, IFormatProvider? provider = default)
        => self.AsSpan().TryParseWithPrefix64(provider);

    /// <summary>プレフィックス解釈を伴う文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt64? TryParseWithPrefix64(this Span<char> self, IFormatProvider? provider = default)
        => ((ReadOnlySpan<char>)self).TryParseWithPrefix64(provider);

    /// <summary>プレフィックス解釈を伴う文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static UInt64? TryParseWithPrefix64(this ReadOnlySpan<char> self, IFormatProvider? provider = default)
    {
        const string HexPrefix1 = "0x";
        if (self.StartsWith(HexPrefix1, StringComparison.OrdinalIgnoreCase)) return self[HexPrefix1.Length..].TryParseHexNumber64(provider);

        const string HexPrefix2 = "&H";
        if (self.StartsWith(HexPrefix2, StringComparison.OrdinalIgnoreCase)) return self[HexPrefix2.Length..].TryParseHexNumber64(provider);

        const string HexPrefix3 = "#";
        if (self.StartsWith(HexPrefix3, StringComparison.OrdinalIgnoreCase)) return self[HexPrefix3.Length..].TryParseHexNumber64(provider);

        const string BinPrefix1 = "0b";
        if (self.StartsWith(BinPrefix1, StringComparison.OrdinalIgnoreCase)) return self[BinPrefix1.Length..].TryParseBinNumber64(trim: true, snake: true);

        const string BinPrefix2 = "&B";
        if (self.StartsWith(BinPrefix2, StringComparison.OrdinalIgnoreCase)) return self[BinPrefix2.Length..].TryParseBinNumber64(trim: true, snake: true);

        return self.TryParseUInt64(provider: provider);
    }

#if NET7_0_OR_GREATER
    /// <summary>プレフィックス解釈を伴う文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static TResult? TryParseNumberWithPrefix<TResult>(this string self, IFormatProvider? provider = default) where TResult : struct, IBinaryInteger<TResult>
        => self.AsSpan().TryParseNumberWithPrefix<TResult>(provider);

    /// <summary>プレフィックス解釈を伴う文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static TResult? TryParseNumberWithPrefix<TResult>(this Span<char> self, IFormatProvider? provider = default) where TResult : struct, IBinaryInteger<TResult>
        => ((ReadOnlySpan<char>)self).TryParseNumberWithPrefix<TResult>(provider);

    /// <summary>プレフィックス解釈を伴う文字列のパースを試みる。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="provider">カルチャ固有の書式情報プロバイダ</param>
    /// <returns>パース結果値またはnull</returns>
    public static TResult? TryParseNumberWithPrefix<TResult>(this ReadOnlySpan<char> self, IFormatProvider? provider = default) where TResult : struct, IBinaryInteger<TResult>
    {
        const string HexPrefix1 = "0x";
        if (self.StartsWith(HexPrefix1, StringComparison.OrdinalIgnoreCase)) return self[HexPrefix1.Length..].TryParseHexNumber<TResult>(provider);

        const string HexPrefix2 = "&H";
        if (self.StartsWith(HexPrefix2, StringComparison.OrdinalIgnoreCase)) return self[HexPrefix2.Length..].TryParseHexNumber<TResult>(provider);

        const string HexPrefix3 = "#";
        if (self.StartsWith(HexPrefix3, StringComparison.OrdinalIgnoreCase)) return self[HexPrefix3.Length..].TryParseHexNumber<TResult>(provider);

        const string BinPrefix1 = "0b";
        if (self.StartsWith(BinPrefix1, StringComparison.OrdinalIgnoreCase)) return self[BinPrefix1.Length..].TryParseBinNumber<TResult>(trim: true, snake: true);

        const string BinPrefix2 = "&B";
        if (self.StartsWith(BinPrefix2, StringComparison.OrdinalIgnoreCase)) return self[BinPrefix2.Length..].TryParseBinNumber<TResult>(trim: true, snake: true);

        return self.TryParseNumber<TResult>(provider: provider);
    }
#endif 
    #endregion
}
