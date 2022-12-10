using System.Globalization;

namespace Lestaly;

/// <summary>
/// 数値に対する拡張メソッド
/// </summary>
public static class NumberExtensions
{
    /// <summary>数値を補助単位付き表記で文字列化する。</summary>
    /// <param name="self">対象の値</param>
    /// <param name="si">true を指定すると 1000 の累乗で補助単位を付与する。falseの場合は 1024 の累乗となる。</param>
    /// <param name="numInfo">文字列化する際の数値書式情報。省略時は現在のカルチャに依存する。</param>
    /// <returns>構築された文字列</returns>
    public static string ToHumanize(this int self, bool si = false, NumberFormatInfo? numInfo = null)
        => NumberUtils.ToHumanize((long)self, si, numInfo);

    /// <summary>数値を補助単位付き表記で文字列化する。</summary>
    /// <param name="self">対象の値</param>
    /// <param name="si">true を指定すると 1000 の累乗で補助単位を付与する。falseの場合は 1024 の累乗となる。</param>
    /// <param name="numInfo">文字列化する際の数値書式情報。省略時は現在のカルチャに依存する。</param>
    /// <returns>構築された文字列</returns>
    public static string ToHumanize(this long self, bool si = false, NumberFormatInfo? numInfo = null)
        => NumberUtils.ToHumanize(self, si, numInfo);

    /// <summary>数値を補助単位付き表記で文字列化する。</summary>
    /// <param name="self">対象の値</param>
    /// <param name="si">true を指定すると 1000 の累乗で補助単位を付与する。falseの場合は 1024 の累乗となる。</param>
    /// <param name="numInfo">文字列化する際の数値書式情報。省略時は現在のカルチャに依存する。</param>
    /// <returns>構築された文字列</returns>
    public static string ToHumanize(this uint self, bool si = false, NumberFormatInfo? numInfo = null)
        => NumberUtils.ToHumanize((ulong)self, si, numInfo);

    /// <summary>数値を補助単位付き表記で文字列化する。</summary>
    /// <param name="self">対象の値</param>
    /// <param name="si">true を指定すると 1000 の累乗で補助単位を付与する。falseの場合は 1024 の累乗となる。</param>
    /// <param name="numInfo">文字列化する際の数値書式情報。省略時は現在のカルチャに依存する。</param>
    /// <returns>構築された文字列</returns>
    public static string ToHumanize(this ulong self, bool si = false, NumberFormatInfo? numInfo = null)
        => NumberUtils.ToHumanize(self, si, numInfo);
}
