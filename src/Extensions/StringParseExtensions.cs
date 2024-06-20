using System.Globalization;
using System.Numerics;

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
    #endregion

    #region TryParse
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
    #endregion

    #region TryParseHex
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
    #endregion

    #region TryParseBin
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
    #endregion

    #region TryParseWithPrefix
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
    #endregion
}
