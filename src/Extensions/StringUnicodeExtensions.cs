using CometFlavor.Unicode.Extensions.Text;

namespace Lestaly;

/// <summary>
/// string に対する拡張メソッド
/// </summary>
public static class StringUnicodeExtensions
{
    /// <summary>文字列を指定の幅に省略する。</summary>
    /// <param name="self">元の文字列</param>
    /// <param name="width">制限する文字列の幅</param>
    /// <param name="marker">省略時に付与するマーカ文字列</param>
    /// <returns>必要に応じて省略された</returns>
    public static string? EllipsisByWidth(this string? self, int width, string? marker = "...")
        => CometFlavor.Unicode.Extensions.Text.StringExtensions.EllipsisByWidth(self ?? "", width, SimpleEqwMeasure);

    /// <summary>半角=1,全角=2に評価するシンプルな幅評価値</summary>
    private static readonly EawMeasure SimpleEqwMeasure = new EawMeasure(1, 2, 1);
}
