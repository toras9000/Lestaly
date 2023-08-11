using System.Diagnostics.CodeAnalysis;

namespace Lestaly;

/// <summary>
/// string に対する拡張メソッド
/// </summary>
public static class StringExtensions
{
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
        => string.IsNullOrEmpty(self) ? alt : self;

    /// <summary>文字列がnullや空であれば代替文字列を返却する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="alt">代替文字列取得デリゲート</param>
    /// <returns>nullや空ならば代替文字列、それ以外ならば元の文字列</returns>
    public static string WhenEmpty(this string? self, Func<string> alt)
        => string.IsNullOrEmpty(self) ? alt() : self;

    /// <summary>文字列がnullや空白文字であれば代替文字列を返却する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="alt">代替文字列</param>
    /// <returns>nullや空白文字ならば代替文字列、それ以外ならば元の文字列</returns>
    public static string WhenWhite(this string? self, string alt)
        => string.IsNullOrWhiteSpace(self) ? alt : self;

    /// <summary>文字列がnullや空白文字であれば代替文字列を返却する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="alt">代替文字列取得デリゲート</param>
    /// <returns>nullや空白文字ならば代替文字列、それ以外ならば元の文字列</returns>
    public static string WhenWhite(this string? self, Func<string> alt)
        => string.IsNullOrWhiteSpace(self) ? alt() : self;

    /// <summary>文字列が空であれば null に替える。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>空でない文字列、または null</returns>
    public static string? OmitEmpty(this string? self)
    {
        if (self == null) return null;
        if (self.Length == 0) return null;
        return self;
    }

    /// <summary>文字列の空白をトリムし、空であれば null に替える</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>空白がトリムされた文字列、または null</returns>
    public static string? OmitWhite(this string? self)
    {
        if (self == null) return null;
        var trimmed = self.AsSpan().Trim();
        if (trimmed.Length == 0) return null;
        return trimmed.ToString();
    }

    /// <summary>文字列がnullや空でなければ変換する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="converter">変換処理</param>
    /// <returns>変換された値。もしくはデフォルト値</returns>
    public static TResult? NotEmptyTo<TResult>(this string? self, Func<string, TResult> converter)
    {
        if (string.IsNullOrEmpty(self)) return default;
        return converter(self);
    }

    /// <summary>文字列がnullや空白でなければ変換する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="converter">変換処理</param>
    /// <returns>変換された値。もしくはデフォルト値</returns>
    public static TResult? NotWhiteTo<TResult>(this string? self, Func<string, TResult> converter)
    {
        if (string.IsNullOrWhiteSpace(self)) return default;
        return converter(self);
    }

    /// <summary>文字列が指定のテキストのいずれかで始まるかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="values">始まるかを判定する文字列。nullや空の要素は無視される。</param>
    /// <param name="ignoreCase">大文字/小文字の違いを無視するか否か(序数ベース)</param>
    /// <returns>いずれかで始まっているか否か</returns>
    public static bool StartsWithAny(this string self, string[] values, bool ignoreCase)
        => self == null ? false : self.AsSpan().StartsWithAny(values, ignoreCase);

    /// <summary>文字列が指定のテキストのいずれかで始まるかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="values">始まるかを判定する文字列。nullや空の要素は無視される。</param>
    /// <param name="comparison">文字列比較方法</param>
    /// <returns>いずれかで始まっているか否か</returns>
    public static bool StartsWithAny(this string self, string[] values, StringComparison comparison = StringComparison.Ordinal)
        => self == null ? false : self.AsSpan().StartsWithAny(values, comparison);

    /// <summary>文字列が指定のテキストのいずれかで始まるかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="values">始まるかを判定する文字列。nullや空の要素は無視される。</param>
    /// <param name="ignoreCase">大文字/小文字の違いを無視するか否か(序数ベース)</param>
    /// <returns>いずれかで始まっているか否か</returns>
    public static bool StartsWithAny(this ReadOnlySpan<char> self, string[] values, bool ignoreCase)
        => self.StartsWithAny(values, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);

    /// <summary>文字列が指定のテキストのいずれかで始まるかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="values">始まるかを判定する文字列。nullや空の要素は無視される。</param>
    /// <param name="comparison">文字列比較方法</param>
    /// <returns>いずれかで始まっているか否か</returns>
    public static bool StartsWithAny(this ReadOnlySpan<char> self, string[] values, StringComparison comparison = StringComparison.Ordinal)
    {
        for (var i = 0; i < values.Length; i++)
        {
            var value = values[i];
            if (string.IsNullOrEmpty(value)) continue;
            if (self.StartsWith(value, comparison)) return true;
        }
        return false;
    }

    /// <summary>文字列が指定のテキストのいずれかで終わるかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="values">終わるかを判定する文字列。nullや空の要素は無視される。</param>
    /// <param name="ignoreCase">大文字/小文字の違いを無視するか否か(序数ベース)</param>
    /// <returns>いずれかで終わっているか否か</returns>
    public static bool EndsWithAny(this string self, string[] values, bool ignoreCase)
        => self == null ? false : self.AsSpan().EndsWithAny(values, ignoreCase);

    /// <summary>文字列が指定のテキストのいずれかで終わるかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="values">終わるかを判定する文字列。nullや空の要素は無視される。</param>
    /// <param name="comparison">文字列比較方法</param>
    /// <returns>いずれかで終わっているか否か</returns>
    public static bool EndsWithAny(this string self, string[] values, StringComparison comparison = StringComparison.Ordinal)
        => self == null ? false : self.AsSpan().EndsWithAny(values, comparison);

    /// <summary>文字列が指定のテキストのいずれかで終わるかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="values">終わるかを判定する文字列。nullや空の要素は無視される。</param>
    /// <param name="ignoreCase">大文字/小文字の違いを無視するか否か(序数ベース)</param>
    /// <returns>いずれかで終わっているか否か</returns>
    public static bool EndsWithAny(this ReadOnlySpan<char> self, string[] values, bool ignoreCase)
        => self.EndsWithAny(values, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);

    /// <summary>文字列が指定のテキストのいずれかで終わるかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="values">終わるかを判定する文字列。nullや空の要素は無視される。</param>
    /// <param name="comparison">文字列比較方法</param>
    /// <returns>いずれかで終わっているか否か</returns>
    public static bool EndsWithAny(this ReadOnlySpan<char> self, string[] values, StringComparison comparison = StringComparison.Ordinal)
    {
        for (var i = 0; i < values.Length; i++)
        {
            var value = values[i];
            if (string.IsNullOrEmpty(value)) continue;
            if (self.EndsWith(value, comparison)) return true;
        }
        return false;
    }

    /// <summary>文字列が指定の文字列で始まるよう必要であれば付与する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="value">必要であれば付与する文字列。</param>
    /// <param name="comparison">文字列比較方法</param>
    /// <returns>すでに指定文字列で始まっていれば元の文字列、そうでなければ指定文字列を先頭に付与した文字列。</returns>
    public static string EnsureStarts(this string? self, string value, StringComparison comparison = StringComparison.Ordinal)
        => self?.StartsWith(value, comparison) == true ? self : value + self;

    /// <summary>文字列が指定の文字列で始まるよう必要であれば付与する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="value">必要であれば付与する文字列。</param>
    /// <param name="ignoreCase">大文字/小文字の違いを無視するか否か(序数ベース)</param>
    /// <returns>すでに指定文字列で始まっていれば元の文字列、そうでなければ指定文字列を先頭に付与した文字列。</returns>
    public static string EnsureStarts(this string? self, string value, bool ignoreCase)
        => self?.StartsWith(value, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) == true ? self : value + self;

    /// <summary>文字列が指定の文字で終わるよう必要であれば付与する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="value">必要であれば付与する文字列。</param>
    /// <param name="comparison">文字列比較方法</param>
    /// <returns>すでに指定文字列で終わっていれば元の文字列、そうでなければ指定文字を末尾に付与した文字列。</returns>
    public static string EnsureEnds(this string? self, string value, StringComparison comparison = StringComparison.Ordinal)
        => self?.EndsWith(value, comparison) == true ? self : self + value;

    /// <summary>文字列が指定の文字で終わるよう必要であれば付与する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="value">必要であれば付与する文字列。</param>
    /// <param name="ignoreCase">大文字/小文字の違いを無視するか否か(序数ベース)</param>
    /// <returns>すでに指定文字列で終わっていれば元の文字列、そうでなければ指定文字を末尾に付与した文字列。</returns>
    public static string EnsureEnds(this string? self, string value, bool ignoreCase)
        => self?.EndsWith(value, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) == true ? self : self + value;

    /// <summary>
    /// 文字列の最初の行を取得する。
    /// </summary>
    /// <param name="self">対象文字列</param>
    /// <returns>最初の行文字列</returns>
    [return: NotNullIfNotNull(nameof(self))]
    public static string? FirstLine(this string? self)
        => CometFlavor.Extensions.Text.StringExtensions.FirstLine(self);

    /// <summary>
    /// 文字列の最後の行を取得する。
    /// </summary>
    /// <param name="self">対象文字列</param>
    /// <returns>最後の行文字列</returns>
    [return: NotNullIfNotNull(nameof(self))]
    public static string? LastLine(this string? self)
        => CometFlavor.Extensions.Text.StringExtensions.LastLine(self);

    /// <summary>文字列の行を列挙する。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>テキスト行シーケンス</returns>
    public static IEnumerable<string> AsTextLines(this string self)
        => CometFlavor.Extensions.Text.StringExtensions.AsTextLines(self);

    /// <summary>特定文字の前部分文字列を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="marker">検索文字</param>
    /// <param name="defaultEmpty">検索文字が見つからない場合に空を返すか否か</param>
    /// <returns>
    /// 検索文字が存在する場合はそれより前の文字列。
    /// 見つからない場合はパラメータ指定により文字列全体または空文字列。
    /// </returns>
    [return: NotNullIfNotNull(nameof(self))]
    public static string? BeforeAt(this string? self, char marker, bool defaultEmpty = false)
        => CometFlavor.Extensions.Text.StringExtensions.BeforeAt(self, marker, defaultEmpty);

    /// <summary>特定文字列の前部分文字列を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="marker">検索文字列</param>
    /// <param name="defaultEmpty">検索文字列が見つからない場合に空を返すか否か</param>
    /// <returns>
    /// 検索文字列が存在する場合はそれより前の文字列。
    /// 見つからない場合はパラメータ指定により文字列全体または空文字列。
    /// </returns>
    [return: NotNullIfNotNull(nameof(self))]
    public static string? BeforeAt(this string? self, string marker, bool defaultEmpty = false)
        => CometFlavor.Extensions.Text.StringExtensions.BeforeAt(self, marker, defaultEmpty);

    /// <summary>特定文字の後部分文字列を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="marker">検索文字</param>
    /// <param name="defaultEmpty">検索文字が見つからない場合に空を返すか否か</param>
    /// <returns>
    /// 検索文字が存在する場合はそれより後の文字列。
    /// 見つからない場合はパラメータ指定により文字列全体または空文字列。
    /// </returns>
    [return: NotNullIfNotNull(nameof(self))]
    public static string? AfterAt(this string? self, char marker, bool defaultEmpty = false)
        => CometFlavor.Extensions.Text.StringExtensions.AfterAt(self, marker, defaultEmpty);

    /// <summary>特定文字列の後部分文字列を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="marker">検索文字列</param>
    /// <param name="defaultEmpty">検索文字列が見つからない場合に空を返すか否か</param>
    /// <returns>
    /// 検索文字列が存在する場合はそれより後の文字列。
    /// 見つからない場合はパラメータ指定により文字列全体または空文字列。
    /// </returns>
    [return: NotNullIfNotNull(nameof(self))]
    public static string? AfterAt(this string? self, string marker, bool defaultEmpty = false)
        => CometFlavor.Extensions.Text.StringExtensions.AfterAt(self, marker, defaultEmpty);

    /// <summary>特定文字まで部分文字列を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="marker">検索文字</param>
    /// <param name="defaultEmpty">検索文字が見つからない場合に空を返すか否か</param>
    /// <returns>
    /// 検索文字が存在する場合は検索文字までの文字列。
    /// 見つからない場合はパラメータ指定により文字列全体または空文字列。
    /// </returns>
    [return: NotNullIfNotNull(nameof(self))]
    public static string? TakeTo(this string? self, char marker, bool defaultEmpty = false)
        => CometFlavor.Extensions.Text.StringExtensions.TakeTo(self, marker, defaultEmpty);

    /// <summary>特定文字列までの部分文字列を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="marker">検索文字列</param>
    /// <param name="defaultEmpty">検索文字列が見つからない場合に空を返すか否か</param>
    /// <returns>
    /// 検索文字列が存在する場合は検索文字列までの文字列。
    /// 見つからない場合はパラメータ指定により文字列全体または空文字列。
    /// </returns>
    [return: NotNullIfNotNull(nameof(self))]
    public static string? TakeTo(this string? self, string marker, bool defaultEmpty = false)
        => CometFlavor.Extensions.Text.StringExtensions.TakeTo(self, marker, defaultEmpty);

    /// <summary>特定文字からの部分文字列を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="marker">検索文字</param>
    /// <param name="defaultEmpty">検索文字が見つからない場合に空を返すか否か</param>
    /// <returns>
    /// 検索文字が存在する場合は検索文字からの文字列。
    /// 見つからない場合はパラメータ指定により文字列全体または空文字列。
    /// </returns>
    [return: NotNullIfNotNull(nameof(self))]
    public static string? TakeFrom(this string? self, char marker, bool defaultEmpty = false)
        => CometFlavor.Extensions.Text.StringExtensions.TakeFrom(self, marker, defaultEmpty);

    /// <summary>特定文字列からの部分文字列を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="marker">検索文字列</param>
    /// <param name="defaultEmpty">検索文字列が見つからない場合に空を返すか否か</param>
    /// <returns>
    /// 検索文字列が存在する場合は検索文字列からの文字列。
    /// 見つからない場合はパラメータ指定により文字列全体または空文字列。
    /// </returns>
    [return: NotNullIfNotNull(nameof(self))]
    public static string? TakeFrom(this string? self, string marker, bool defaultEmpty = false)
        => CometFlavor.Extensions.Text.StringExtensions.TakeFrom(self, marker, defaultEmpty);

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

    /// <summary>文字列を連結する。</summary>
    /// <param name="self">文字列のシーケンス</param>
    /// <param name="separator">連結する文字間に差し込む文字列</param>
    /// <returns>連結された文字列</returns>
    public static string JoinString(this IEnumerable<string?> self, string? separator = null)
        => string.Join(separator, self);

    /// <summary>指定の文字列がnull/空でない場合に装飾を付与する。</summary>
    /// <param name="self">元になる文字列</param>
    /// <param name="format">文字列を装飾する書式。埋め込み位置0のプレースホルダ({{0}})が含まれる必要がある。</param>
    /// <returns>装飾された文字列。元がnullまたは空の場合はそのまま返却。</returns>
    [return: NotNullIfNotNull(nameof(self))]
    public static string? Decorate(this string? self, string format)
        => string.IsNullOrEmpty(self) ? self : string.Format(format, self);

    /// <summary>指定の文字列がnull/空でない場合に装飾を付与する。</summary>
    /// <param name="self">元になる文字列</param>
    /// <param name="decorator">文字列を装飾するデリゲート</param>
    /// <returns>装飾された文字列。元がnullまたは空の場合はそのまま返却。</returns>
    [return: NotNullIfNotNull(nameof(self))]
    public static string? Decorate(this string? self, Func<string, string> decorator)
        => string.IsNullOrEmpty(self) ? self : decorator == null ? self : decorator.Invoke(self);

    /// <summary>指定の文字列がnull/空でない場合にプレフィックスを付与する。</summary>
    /// <param name="self">元になる文字列</param>
    /// <param name="prefix">先頭に付与する文字列</param>
    /// <returns>装飾された文字列。元がnullまたは空の場合はそのまま返却。</returns>
    [return: NotNullIfNotNull(nameof(self))]
    public static string? DecoratePrefix(this string? self, string prefix)
        => string.IsNullOrEmpty(self) ? self : prefix + self;

    /// <summary>指定の文字列がnull/空でない場合にサフィックスを付与する。</summary>
    /// <param name="self">元になる文字列</param>
    /// <param name="suffix">末尾に付与する文字列</param>
    /// <returns>装飾された文字列。元がnullまたは空の場合はそのまま返却。</returns>
    [return: NotNullIfNotNull(nameof(self))]
    public static string? DecorateSuffix(this string? self, string suffix)
        => string.IsNullOrEmpty(self) ? self : self + suffix;

    /// <summary>2つの文字列の両方がnull/空でない場合に結合した文字列を返却する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="sequel">後続文字列</param>
    /// <returns>結合した文字列。いずれかがnull/空であれば空文字列を返却する。</returns>
    public static string TieIn(this string? self, string? sequel)
    {
        if (string.IsNullOrEmpty(self) || string.IsNullOrEmpty(sequel))
        {
            return "";
        }

        return self + sequel;
    }

    /// <summary>2つの文字列の両方がnull/空でない場合に合成した文字列を返却する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="sequel">後続文字列</param>
    /// <param name="mixer">文字列合成処理</param>
    /// <returns>合成した文字列。いずれかがnull/空であれば空文字列を返却する。</returns>
    public static string TieIn(this string? self, string? sequel, Func<string, string, string> mixer)
    {
        if (string.IsNullOrEmpty(self) || string.IsNullOrEmpty(sequel))
        {
            return "";
        }

        if (mixer == null) throw new ArgumentNullException(nameof(mixer));
        return mixer(self, sequel);
    }

    /// <summary>2つの文字列の両方がnull/空でない場合に合成した文字列を返却する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="sequel">後続文字列</param>
    /// <param name="joiner">結合キャラクタ</param>
    /// <returns>合成した文字列。いずれかがnull/空であれば空文字列を返却する。</returns>
    public static string TieIn(this string? self, string? sequel, char joiner)
    {
        if (string.IsNullOrEmpty(self) || string.IsNullOrEmpty(sequel))
        {
            return "";
        }

        return $"{self}{joiner}{sequel}";
    }

    /// <summary>2つの文字列の両方がnull/空でない場合に合成した文字列を返却する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="sequel">後続文字列</param>
    /// <param name="joiner">結合文字列</param>
    /// <returns>合成した文字列。いずれかがnull/空であれば空文字列を返却する。</returns>
    public static string TieIn(this string? self, string? sequel, string joiner)
    {
        if (string.IsNullOrEmpty(self) || string.IsNullOrEmpty(sequel))
        {
            return "";
        }

        return $"{self}{joiner}{sequel}";
    }

    /// <summary>2つの文字列の両方がnull/空でない場合に合成した文字列を、片方がnull/空でなければそれを返却する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="sequel">後続文字列</param>
    /// <returns>結合した文字列。いずれかがnull/空以外であればそれを、両方null/空であれば空文字列を返却する。</returns>
    public static string Mux(this string? self, string? sequel)
    {
        if (string.IsNullOrEmpty(self)) return sequel ?? "";
        if (string.IsNullOrEmpty(sequel)) return self;
        return self + sequel;
    }

    /// <summary>2つの文字列の両方がnull/空でない場合に合成した文字列を、片方がnull/空でなければそれを返却する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="sequel">後続文字列</param>
    /// <param name="mixer">文字列合成処理</param>
    /// <returns>合成した文字列。いずれかがnull/空以外であればそれを、両方null/空であれば空文字列を返却する。</returns>
    public static string Mux(this string? self, string? sequel, Func<string, string, string> mixer)
    {
        if (string.IsNullOrEmpty(self)) return sequel ?? "";
        if (string.IsNullOrEmpty(sequel)) return self;
        if (mixer == null) throw new ArgumentNullException(nameof(mixer));
        return mixer(self, sequel);
    }

    /// <summary>2つの文字列の両方がnull/空でない場合に合成した文字列を、片方がnull/空でなければそれを返却する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="sequel">後続文字列</param>
    /// <param name="joiner">結合キャラクタ</param>
    /// <returns>合成した文字列。いずれかがnull/空以外であればそれを、両方null/空であれば空文字列を返却する。</returns>
    public static string Mux(this string? self, string? sequel, char joiner)
    {
        if (string.IsNullOrEmpty(self)) return sequel ?? "";
        if (string.IsNullOrEmpty(sequel)) return self;
        return $"{self}{joiner}{sequel}";
    }

    /// <summary>2つの文字列の両方がnull/空でない場合に合成した文字列を、片方がnull/空でなければそれを返却する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="sequel">後続文字列</param>
    /// <param name="joiner">結合文字列</param>
    /// <returns>合成した文字列。いずれかがnull/空以外であればそれを、両方null/空であれば空文字列を返却する。</returns>
    public static string Mux(this string? self, string? sequel, string joiner)
    {
        if (string.IsNullOrEmpty(self)) return sequel ?? "";
        if (string.IsNullOrEmpty(sequel)) return self;
        return $"{self}{joiner}{sequel}";
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
    /// <param name="self">対象文字列。</param>
    /// <param name="quotes">クォートキャラクタ候補。空の場合はダブル/シングルクォートキャラクタを候補とする。</param>
    /// <param name="escape">クォートキャラクタをエスケープしているキャラクタ。指定がない場合はクォートキャラクタ2つで</param>
    /// <returns>アンクォートされた文字列</returns>
    [return: NotNullIfNotNull(nameof(self))]
    public static string? Unquote(this string self, ReadOnlySpan<char> quotes = default, char? escape = null)
        => CometFlavor.Extensions.Text.StringExtensions.Unquote(self, quotes, escape);

    /// <summary>
    /// 文字列のテキスト要素を列挙する。
    /// </summary>
    /// <param name="self">対象文字列</param>
    /// <returns>テキスト要素シーケンス</returns>
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
    [return: NotNullIfNotNull(nameof(self))]
    public static string? CutLeftElements(this string? self, int count)
        => CometFlavor.Extensions.Text.StringExtensions.CutLeftElements(self, count);

    /// <summary>
    /// 文字列の末尾にある指定された長さの文字要素を切り出す。
    /// </summary>
    /// <param name="self">元になる文字列。nullまたは空の場合は元のインスタンスをそのまま返却する。</param>
    /// <param name="count">切り出す文字要素の長さ。</param>
    /// <returns>切り出された文字列</returns>
    [return: NotNullIfNotNull(nameof(self))]
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
