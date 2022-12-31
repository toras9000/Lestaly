using System.Diagnostics.CodeAnalysis;
using System.Text;

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
    public static string FirstLine(this string? self)
        => CometFlavor.Extensions.Text.StringExtensions.FirstLine(self) ?? "";

    /// <summary>
    /// 文字列の最後の行を取得する。
    /// </summary>
    /// <param name="self">対象文字列</param>
    /// <returns>最後の行文字列</returns>
    public static string LastLine(this string? self)
        => CometFlavor.Extensions.Text.StringExtensions.LastLine(self) ?? "";

    /// <summary>特定文字の前部分文字列を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="marker">検索文字</param>
    /// <param name="defaultEmpty">検索文字列が見つからない場合に空を返すか否か</param>
    /// <returns>処理結果文字列</returns>
    public static string BeforeAt(this string? self, char marker, bool defaultEmpty = false)
        => CometFlavor.Extensions.Text.StringExtensions.BeforeAt(self, marker, defaultEmpty) ?? "";

    /// <summary>特定文字列の前部分文字列を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="marker">検索文字列</param>
    /// <param name="defaultEmpty">検索文字列が見つからない場合に空を返すか否か</param>
    /// <returns>処理結果文字列</returns>
    public static string BeforeAt(this string? self, string marker, bool defaultEmpty = false)
        => CometFlavor.Extensions.Text.StringExtensions.BeforeAt(self, marker, defaultEmpty) ?? "";

    /// <summary>特定文字の後部分文字列を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="marker">検索文字</param>
    /// <param name="defaultEmpty">検索文字列が見つからない場合に空を返すか否か</param>
    /// <returns>処理結果文字列</returns>
    public static string AfterAt(this string? self, char marker, bool defaultEmpty = false)
        => CometFlavor.Extensions.Text.StringExtensions.AfterAt(self, marker, defaultEmpty) ?? "";

    /// <summary>特定文字列の後部分文字列を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="marker">検索文字列</param>
    /// <param name="defaultEmpty">検索文字列が見つからない場合に空を返すか否か</param>
    /// <returns>処理結果文字列</returns>
    public static string AfterAt(this string? self, string marker, bool defaultEmpty = false)
        => CometFlavor.Extensions.Text.StringExtensions.AfterAt(self, marker, defaultEmpty) ?? "";

    /// <summary>文字列を連結する。</summary>
    /// <param name="self">文字列のシーケンス</param>
    /// <param name="separator">連結する文字間に差し込む文字列</param>
    /// <returns>連結された文字列</returns>
    public static string JoinString(this IEnumerable<string?> self, string? separator = null)
        => string.Join(separator, self);

    /// <summary>文字列のシーケンスからnull/空を取り除く。</summary>
    /// <param name="self">文字列のシーケンス</param>
    /// <returns>null/空以外のシーケンス</returns>
    public static IEnumerable<string> DropEmpty(this IEnumerable<string?> self)
        => self.Where((string? s) => !string.IsNullOrEmpty(s))!;

    /// <summary>文字列のシーケンスからnull/空白文字列を取り除く。</summary>
    /// <param name="self">文字列のシーケンス</param>
    /// <returns>null/空白文字列以外のシーケンス</returns>
    public static IEnumerable<string> DropWhite(this IEnumerable<string?> self)
        => self.Where((string? s) => !string.IsNullOrWhiteSpace(s))!;

    /// <summary>指定の文字列がnull/空でない場合に装飾を付与する。</summary>
    /// <param name="self">元になる文字列</param>
    /// <param name="format">文字列を装飾する書式。埋め込み位置0のプレースホルダ({{0}})が含まれる必要がある。</param>
    /// <returns>装飾された文字列。元がnullまたは空の場合はそのまま返却。</returns>
    public static string? Decorate(this string? self, string format)
        => string.IsNullOrEmpty(self) ? self : string.Format(format, self);

    /// <summary>指定の文字列がnull/空でない場合に装飾を付与する。</summary>
    /// <param name="self">元になる文字列</param>
    /// <param name="decorator">文字列を装飾するデリゲート</param>
    /// <returns>装飾された文字列。元がnullまたは空の場合はそのまま返却。</returns>
    public static string? Decorate(this string? self, Func<string, string> decorator)
        => string.IsNullOrEmpty(self) ? self : decorator?.Invoke(self) ?? self;

    /// <summary>指定の文字列がnull/空でない場合にプレフィックスを付与する。</summary>
    /// <param name="self">元になる文字列</param>
    /// <param name="prefix">先頭に付与する文字列</param>
    /// <returns>装飾された文字列。元がnullまたは空の場合はそのまま返却。</returns>
    public static string? DecoratePrefix(this string? self, string prefix)
        => string.IsNullOrEmpty(self) ? self : prefix + self;

    /// <summary>指定の文字列がnull/空でない場合にサフィックスを付与する。</summary>
    /// <param name="self">元になる文字列</param>
    /// <param name="suffix">末尾に付与する文字列</param>
    /// <returns>装飾された文字列。元がnullまたは空の場合はそのまま返却。</returns>
    public static string? DecorateSuffix(this string? self, string suffix)
        => string.IsNullOrEmpty(self) ? self : self + suffix;

    /// <summary>対象と続く文字列の両方の文字列がnull/空でない場合に結合した文字列を作る</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="sequel">続く文字列</param>
    /// <returns>結合した文字列。いずれかがnull/空であれば空文字列を返却する。</returns>
    public static string TieIn(this string? self, string? sequel)
    {
        if (string.IsNullOrEmpty(self) || string.IsNullOrEmpty(sequel))
        {
            return "";
        }

        return self + sequel;
    }

    /// <summary>
    /// 文字列をクォートする。
    /// </summary>
    /// <param name="text">対象文字列。nullの場合は空文字列と同じ扱いとする。</param>
    /// <param name="quote">クォートキャラクタ</param>
    /// <param name="escape">対象文字列中のクォートキャラクタをエスケープするキャラクタ</param>
    /// <returns>クォートされたキャラクタ</returns>
    public static string Quote(this string? text, char quote = '"', char? escape = null)
        => CometFlavor.Extensions.Text.StringExtensions.Quote(text, quote, escape);

    /// <summary>文字列をアンクォートする。</summary>
    /// <param name="text">対象文字列。</param>
    /// <param name="quotes">クォートキャラクタ候補。空の場合はダブル/シングルクォートキャラクタを候補とする。</param>
    /// <param name="escape">クォートキャラクタをエスケープしているキャラクタ。指定がない場合はクォートキャラクタ2つで</param>
    /// <returns>アンクォートされた文字列</returns>
    public static string? Unquote(this string text, ReadOnlySpan<char> quotes = default, char? escape = null)
        => CometFlavor.Extensions.Text.StringExtensions.Unquote(text, quotes, escape);

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
    public static string CutLeftElements(this string? self, int count)
        => CometFlavor.Extensions.Text.StringExtensions.CutLeftElements(self, count) ?? "";

    /// <summary>
    /// 文字列の末尾にある指定された長さの文字要素を切り出す。
    /// </summary>
    /// <param name="self">元になる文字列。nullまたは空の場合は元のインスタンスをそのまま返却する。</param>
    /// <param name="count">切り出す文字要素の長さ。</param>
    /// <returns>切り出された文字列</returns>
    public static string CutRightElements(this string? self, int count)
        => CometFlavor.Extensions.Text.StringExtensions.CutRightElements(self, count) ?? "";

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

    /// <summary>文字列がnullや空であるかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>nullや空ならば true</returns>
    public static bool IsEmpty([NotNullWhen(false)] this string? self)
        => string.IsNullOrEmpty(self);

    /// <summary>文字列がnullや空以外であるかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>nullや空以外であれば true</returns>
    public static bool IsNotEmpty([NotNullWhen(true)] this string? self)
        => !string.IsNullOrEmpty(self);

    /// <summary>文字列がnullや空白文字であるかを判定する</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>nullや空白文字ならば true</returns>
    public static bool IsWhite([NotNullWhen(false)] this string? self)
        => string.IsNullOrWhiteSpace(self);

    /// <summary>文字列がnullや空白文字以外であるかを判定する</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>nullや空白文字以外ならば true</returns>
    public static bool IsNotWhite([NotNullWhen(true)] this string? self)
        => !string.IsNullOrWhiteSpace(self);

    /// <summary>文字列がnullや空であれば代替文字列を返却する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="alt">代替文字列</param>
    /// <returns>nullや空ならば代替文字列、それ以外ならば元の文字列</returns>
    public static string WhenEmpty(this string? self, string alt)
        => !string.IsNullOrEmpty(self) ? self : alt;

    /// <summary>文字列がnullや空であれば代替文字列を返却する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="alt">代替文字列取得デリゲート</param>
    /// <returns>nullや空ならば代替文字列、それ以外ならば元の文字列</returns>
    public static string WhenEmpty(this string? self, Func<string> alt)
        => !string.IsNullOrEmpty(self) ? self : alt();

    /// <summary>文字列がnullや空白文字であれば代替文字列を返却する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="alt">代替文字列</param>
    /// <returns>nullや空白文字ならば代替文字列、それ以外ならば元の文字列</returns>
    public static string WhenWhite(this string? self, string alt)
        => !string.IsNullOrWhiteSpace(self) ? self : alt;

    /// <summary>文字列がnullや空白文字であれば代替文字列を返却する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="alt">代替文字列取得デリゲート</param>
    /// <returns>nullや空白文字ならば代替文字列、それ以外ならば元の文字列</returns>
    public static string WhenWhite(this string? self, Func<string> alt)
        => !string.IsNullOrWhiteSpace(self) ? self : alt();

    /// <summary>文字列がnullや空であれば例外を送出する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="generator">例外オブジェクト生成デリゲート</param>
    /// <returns>対象文字列</returns>
    public static string ThrowIfEmpty(this string? self, Func<Exception>? generator = null)
        => string.IsNullOrEmpty(self) ? throw generator?.Invoke() ?? new InvalidDataException() : self;

    /// <summary>文字列がnullや空白文字であれば例外を送出する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="generator">例外オブジェクト生成デリゲート</param>
    /// <returns>対象文字列</returns>
    public static string ThrowIfWhite(this string? self, Func<Exception>? generator = null)
        => string.IsNullOrWhiteSpace(self) ? throw generator?.Invoke() ?? new InvalidDataException() : self;

    /// <summary>文字列がnullや空であればキャンセル例外を送出する。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>対象文字列</returns>
    public static string CancelIfEmpty(this string? self)
    => self.ThrowIfEmpty(() => new OperationCanceledException());

    /// <summary>文字列がnullや空白文字であればキャンセル例外を送出する。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>対象文字列</returns>
    public static string CancelIfWhite(this string? self)
    => self.ThrowIfWhite(() => new OperationCanceledException());

}
