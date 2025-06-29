using System.Globalization;

namespace Lestaly;

/// <summary>
/// 数値を処理するユーティリティ
/// </summary>
public static class NumberUtils
{
    /// <summary>数値を補助単位付き表記で文字列化する。</summary>
    /// <param name="value">対象の値</param>
    /// <param name="si">true を指定すると 1000 の累乗で補助単位を付与する。falseの場合は 1024 の累乗となる。</param>
    /// <param name="numInfo">
    /// 文字列化する際の数値書式情報。省略時は現在のカルチャに依存する。
    /// 書式情報では NumberDecimalSeparator と NegativeSign のみを利用する。
    /// NumberNegativePattern は考慮しない。パターンにかかわらず、常に負値は "-1.23k" のような書式で文字列化する。
    /// </param>
    /// <param name="formatter">文字列構築デリゲート</param>
    /// <returns>構築された文字列</returns>
    public static string ToHumanize(long value, bool si = false, NumberFormatInfo? numInfo = null, Func<string, char?, string>? formatter = null)
    {
        // 補助単位。論理的に Exa までしか利用されない。
        var supp = (stackalloc char?[] { null, 'k', 'M', 'G', 'T', 'P', 'E', 'Z', 'Y', 'R', 'Q', });

        // 数値書式情報
        numInfo ??= CultureInfo.CurrentCulture.NumberFormat;

        // 負の最大値に対する処理を特殊化
        // 自前の負号処理をするため後続処理で絶対値を使うので、負の最大値を同じ処理で扱えない。
        if (value == long.MinValue)
        {
            if (formatter != null) return formatter(si ? $"{numInfo.NegativeSign}9{numInfo.NumberDecimalSeparator}22" : $"{numInfo.NegativeSign}8{numInfo.NumberDecimalSeparator}00", 'E');
            return si ? $"{numInfo.NegativeSign}9{numInfo.NumberDecimalSeparator}22E" : $"{numInfo.NegativeSign}8{numInfo.NumberDecimalSeparator}00E";
        }

        // 補助単位表現する値を算出
        var dist = si ? 1000L : 1024L;
        var sign = value < 0 ? -1 : 1;
        var quot = Math.Abs(value);
        var remain = 0L;
        while (dist <= quot && !supp.IsEmpty)
        {
            (quot, remain) = Math.DivRem(quot, dist);
            supp = supp[1..];
        }

        // 補助単位文字
        var sup = supp[0];

        // 符号文字
        var sigmark = sign < 0 ? numInfo.NegativeSign : "";

        // 補助単位が付く場合、累乗値の大きさによって小数部を追加する
        if (sup != null)
        {
            if (quot < 10)
            {
                if (formatter != null) return formatter($"{sigmark}{quot}{numInfo.NumberDecimalSeparator}{100 * remain / dist:D2}", sup);
                return $"{sigmark}{quot}{numInfo.NumberDecimalSeparator}{100 * remain / dist:D2}{sup}";

            }
            else if (quot < 100)
            {
                if (formatter != null) return formatter($"{sigmark}{quot}{numInfo.NumberDecimalSeparator}{10 * remain / dist:D1}", sup);
                return $"{sigmark}{quot}{numInfo.NumberDecimalSeparator}{10 * remain / dist:D1}{sup}";
            }
        }

        // 小数部不要ならばそのまま
        if (formatter != null) return formatter($"{sigmark}{quot}", sup);
        return $"{sigmark}{quot}{sup}";
    }

    /// <summary>数値を補助単位付き表記で文字列化する。</summary>
    /// <param name="value">対象の値</param>
    /// <param name="si">true を指定すると 1000 の累乗で補助単位を付与する。falseの場合は 1024 の累乗となる。</param>
    /// <param name="numInfo">
    /// 文字列化する際の数値書式情報。省略時は現在のカルチャに依存する。
    /// 書式情報では NumberDecimalSeparator のみを利用する。
    /// </param>
    /// <param name="formatter">文字列構築デリゲート</param>
    /// <returns>構築された文字列</returns>
    public static string ToHumanize(ulong value, bool si = false, NumberFormatInfo? numInfo = null, Func<string, char?, string>? formatter = null)
    {
        // 補助単位。論理的に Exa までしか利用されない。
        var supp = (stackalloc char?[] { null, 'k', 'M', 'G', 'T', 'P', 'E', 'Z', 'Y', 'R', 'Q', });

        // 数値書式情報
        numInfo ??= CultureInfo.CurrentCulture.NumberFormat;

        // 補助単位表現する値を算出
        var dist = si ? 1000UL : 1024UL;
        var quot = value;
        var remain = 0UL;
        while (dist <= quot && !supp.IsEmpty)
        {
            (quot, remain) = Math.DivRem(quot, dist);
            supp = supp[1..];
        }

        // 補助単位が付く場合、累乗値の大きさによって小数部を追加する
        var sup = supp[0];
        if (sup != null)
        {
            if (quot < 10)
            {
                if (formatter != null) return formatter($"{quot}{numInfo.NumberDecimalSeparator}{100 * remain / dist:D2}", sup);
                return $"{quot}{numInfo.NumberDecimalSeparator}{100 * remain / dist:D2}{sup}";
            }
            else if (quot < 100)
            {
                if (formatter != null) return formatter($"{quot}{numInfo.NumberDecimalSeparator}{10 * remain / dist:D1}", sup);
                return $"{quot}{numInfo.NumberDecimalSeparator}{10 * remain / dist:D1}{sup}";
            }
        }

        // 小数部不要ならばそのまま
        if (formatter != null) return formatter(quot.ToString(), sup);
        return $"{quot}{sup}";
    }
}
