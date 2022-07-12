namespace Lestaly;

/// <summary>
/// string に対する拡張メソッド
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// 文字列の最初の行を取得する。
    /// </summary>
    /// <param name="self">対象文字列</param>
    /// <returns>最初の行文字列</returns>
    public static string? FirstLine(this string? self)
        => CometFlavor.Extensions.Text.StringExtensions.FirstLine(self);

    /// <summary>
    /// 文字列の最後の行を取得する。
    /// </summary>
    /// <param name="self">対象文字列</param>
    /// <returns>最後の行文字列</returns>
    public static string? LastLine(this string? self)
        => CometFlavor.Extensions.Text.StringExtensions.LastLine(self);

    /// <summary>
    /// 文字列を連結する。
    /// </summary>
    /// <param name="self">文字列のシーケンス</param>
    /// <param name="separator">連結する文字間に差し込む文字列</param>
    /// <returns></returns>
    public static string JoinString(this IEnumerable<string?> self, string? separator = null)
        => CometFlavor.Extensions.Text.StringExtensions.JoinString(self, separator);

    /// <summary>
    /// 文字列を装飾する。
    /// 元の文字列が null または 空の場合はなにもしない。
    /// </summary>
    /// <param name="self">元になる文字列</param>
    /// <param name="format">文字列を装飾する書式。埋め込み位置0のプレースホルダ({{0}})が含まれる必要がある。</param>
    /// <returns>装飾された文字列。元がnullまたは空の場合はそのまま返却。</returns>
    public static string? Decorate(this string? self, string format)
        => CometFlavor.Extensions.Text.StringExtensions.Decorate(self, format);

    /// <summary>
    /// 文字列を装飾する。
    /// 元の文字列が null または 空の場合はなにもしない。
    /// </summary>
    /// <param name="self">元になる文字列</param>
    /// <param name="decorator">文字列を装飾するデリゲート</param>
    /// <returns>装飾された文字列。元がnullまたは空の場合はそのまま返却。</returns>
    public static string? Decorate(this string? self, Func<string, string> decorator)
        => CometFlavor.Extensions.Text.StringExtensions.Decorate(self, decorator);

    /// <summary>
    /// 文字列のテキスト要素を列挙する。
    /// </summary>
    /// <param name="self">対象文字列</param>
    /// <returns>テキスト要素シーケンス</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IEnumerable<string> AsTextElements(this string self)
        => CometFlavor.Extensions.Text.StringExtensions.AsTextElements(self);

    /// <summary>
    /// 文字列のテキスト要素数を取得する。
    /// </summary>
    /// <param name="self">対象文字列</param>
    /// <returns>テキスト要素数</returns>
    public static int TextElementCount(this string self)
        => CometFlavor.Extensions.Text.StringExtensions.TextElementCount(self);

    /// <summary>
    /// 文字列の先頭から指定された長さの文字要素を切り出す。
    /// </summary>
    /// <param name="self">元になる文字列。nullまたは空の場合は元のインスタンスをそのまま返却する。</param>
    /// <param name="count">切り出す文字要素の長さ。</param>
    /// <returns>切り出された文字列</returns>
    public static string? CutLeftElements(this string? self, int count)
        => CometFlavor.Extensions.Text.StringExtensions.CutLeftElements(self, count);

    /// <summary>
    /// 文字列の末尾にある指定された長さの文字要素を切り出す。
    /// </summary>
    /// <param name="self">元になる文字列。nullまたは空の場合は元のインスタンスをそのまま返却する。</param>
    /// <param name="count">切り出す文字要素の長さ。</param>
    /// <returns>切り出された文字列</returns>
    public static string? CutRightElements(this string? self, int count)
        => CometFlavor.Extensions.Text.StringExtensions.CutRightElements(self, count);

    /// <summary>
    /// 文字列を指定の長さに省略する。
    /// このメソッドでは string の Length 基準での長さ制限となる。
    /// </summary>
    /// <param name="self">元の文字列</param>
    /// <param name="length">制限する文字列の長さ</param>
    /// <param name="marker">省略時に付与するマーカ文字列</param>
    /// <returns>必要に応じて省略した文字列。</returns>
    public static string EllipsisByLength(this string self, int length, string? marker = null)
        => CometFlavor.Extensions.Text.StringExtensions.EllipsisByLength(self, length, marker);

    /// <summary>
    /// 文字列を指定の長さに省略する。
    /// このメソッドでは string の 文字要素 基準での長さ制限となる。
    /// </summary>
    /// <param name="self">元の文字列</param>
    /// <param name="count">制限する文字列の文字要素数</param>
    /// <param name="marker">省略時に付与するマーカ文字列</param>
    /// <returns>必要に応じて省略した文字列。</returns>
    public static string EllipsisByElements(this string self, int count, string? marker = null)
        => CometFlavor.Extensions.Text.StringExtensions.EllipsisByElements(self, count, marker);

}
