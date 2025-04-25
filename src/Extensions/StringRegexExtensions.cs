using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Lestaly;

/// <summary>
/// 文字列と正規表現関連の拡張メソッド
/// </summary>
public static class StringRegexExtensions
{
    /// <summary>文字列が正規表現にマッチするかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="pattern">正規表現パターン</param>
    /// <returns>マッチするか否か</returns>
    public static bool IsMatch(this string self, [StringSyntax(StringSyntaxAttribute.Regex)] string pattern)
    {
        return Regex.IsMatch(self, pattern);
    }

    /// <summary>文字列が正規表現にマッチするかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="pattern">正規表現パターン</param>
    /// <param name="options">正規表現オプション</param>
    /// <returns>マッチするか否か</returns>
    public static bool IsMatch(this string self, [StringSyntax(StringSyntaxAttribute.Regex, nameof(options))] string pattern, RegexOptions options)
    {
        return Regex.IsMatch(self, pattern, options);
    }

    /// <summary>文字列に正規表現をマッチさせた結果を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="pattern">正規表現パターン</param>
    /// <returns>マッチ結果</returns>
    public static Match Match(this string self, [StringSyntax(StringSyntaxAttribute.Regex)] string pattern)
    {
        return Regex.Match(self, pattern);
    }

    /// <summary>文字列に正規表現をマッチさせた結果を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="pattern">正規表現パターン</param>
    /// <param name="options">正規表現オプション</param>
    /// <returns>マッチ結果</returns>
    public static Match Match(this string self, [StringSyntax(StringSyntaxAttribute.Regex, nameof(options))] string pattern, RegexOptions options)
    {
        return Regex.Match(self, pattern, options);
    }

    /// <summary>文字列に正規表現をマッチさせた結果をすべて取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="pattern">正規表現パターン</param>
    /// <returns>マッチ結果コレクション</returns>
    public static MatchCollection Matches(this string self, [StringSyntax(StringSyntaxAttribute.Regex)] string pattern)
    {
        return Regex.Matches(self, pattern);
    }

    /// <summary>文字列に正規表現をマッチさせた結果をすべて取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="pattern">正規表現パターン</param>
    /// <param name="options">正規表現オプション</param>
    /// <returns>マッチ結果コレクション</returns>
    public static MatchCollection Matches(this string self, [StringSyntax(StringSyntaxAttribute.Regex, nameof(options))] string pattern, RegexOptions options)
    {
        return Regex.Matches(self, pattern, options);
    }

    /// <summary>正規表現にマッチした個所を文字列置き換えする</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="pattern">正規表現パターン</param>
    /// <param name="replacement">マッチ個所を置き換える文字列</param>
    /// <returns>置き換え結果文字列</returns>
    public static string MatchReplace(this string self, [StringSyntax(StringSyntaxAttribute.Regex)] string pattern, string replacement)
    {
        return Regex.Replace(self, pattern, replacement);
    }

    /// <summary>正規表現にマッチした個所を文字列置き換えする</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="pattern">正規表現パターン</param>
    /// <param name="replacement">マッチ個所を置き換える文字列</param>
    /// <param name="options">正規表現オプション</param>
    /// <returns>置き換え結果文字列</returns>
    public static string MatchReplace(this string self, [StringSyntax(StringSyntaxAttribute.Regex, nameof(options))] string pattern, string replacement, RegexOptions options)
    {
        return Regex.Replace(self, pattern, replacement, options);
    }

    /// <summary>正規表現にマッチした個所で文字列を分割する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="pattern">正規表現パターン</param>
    /// <returns>分割された文字列の配列</returns>
    public static string[] MatchSplit(this string self, [StringSyntax(StringSyntaxAttribute.Regex)] string pattern)
    {
        return Regex.Split(self, pattern);
    }

    /// <summary>正規表現にマッチした個所を文字列置き換えする</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="pattern">正規表現パターン</param>
    /// <param name="options">正規表現オプション</param>
    /// <returns>分割された文字列の配列</returns>
    public static string[] MatchSplit(this string self, [StringSyntax(StringSyntaxAttribute.Regex, nameof(options))] string pattern, RegexOptions options)
    {
        return Regex.Split(self, pattern, options);
    }

    /// <summary>正規表現にマッチした個所を文字列置き換えする</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="pattern">正規表現パターン</param>
    /// <param name="selector">マッチ結果を文字列に変換するセレクタ</param>
    /// <param name="alt">マッチしなかった場合の代替文字列</param>
    /// <returns>マッチした場合はセレクタの結果。マッチしない場合は null </returns>
    [return: NotNullIfNotNull(nameof(alt))]
    public static string? MatchSelect(this string self, [StringSyntax(StringSyntaxAttribute.Regex)] string pattern, Func<Match, string> selector, string? alt = default)
    {
        var match = Regex.Match(self, pattern);
        if (match.Success) return selector(match);
        return alt;
    }

    /// <summary>正規表現にマッチした個所を文字列置き換えする</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="pattern">正規表現パターン</param>
    /// <param name="options">正規表現オプション</param>
    /// <param name="selector">マッチ結果を文字列に変換するセレクタ</param>
    /// <param name="alt">マッチしなかった場合の代替文字列</param>
    /// <returns>マッチした場合はセレクタの結果。マッチしない場合は null </returns>
    [return: NotNullIfNotNull(nameof(alt))]
    public static string? MatchSelect(this string self, [StringSyntax(StringSyntaxAttribute.Regex, nameof(options))] string pattern, RegexOptions options, Func<Match, string> selector, string? alt = default)
    {
        var match = Regex.Match(self, pattern, options);
        if (match.Success) return selector(match);
        return alt;
    }

    /// <summary>文字列が指定のパターンで始まっているかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="pattern">除去するパターン</param>
    /// <returns>指定したパターンで始まっているか否か。</returns>
    public static bool StartsWithPattern(this string? self, [StringSyntax(StringSyntaxAttribute.Regex)] string pattern)
        => self.AsSpan().StartsWithPattern(pattern, default);

    /// <summary>文字列が指定のパターンで始まっているかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="pattern">除去するパターン</param>
    /// <param name="options">マッチオプション</param>
    /// <returns>指定したパターンで始まっているか否か。</returns>
    public static bool StartsWithPattern(this string? self, [StringSyntax(StringSyntaxAttribute.Regex, nameof(options))] string pattern, RegexOptions options)
        => self.AsSpan().StartsWithPattern(pattern, options);

    /// <summary>文字列が指定のパターンで始まっているかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="pattern">除去するパターン</param>
    /// <returns>指定したパターンで始まっているか否か。</returns>
    public static bool StartsWithPattern(this ReadOnlySpan<char> self, [StringSyntax(StringSyntaxAttribute.Regex)] string pattern)
        => self.StartsWithPattern(pattern, default);

    /// <summary>文字列が指定のパターンで始まっているかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="pattern">除去するパターン</param>
    /// <param name="options">マッチオプション</param>
    /// <returns>指定したパターンで始まっているか否か。</returns>
    public static bool StartsWithPattern(this ReadOnlySpan<char> self, [StringSyntax(StringSyntaxAttribute.Regex, nameof(options))] string pattern, RegexOptions options)
    {
        var matches = Regex.EnumerateMatches(self, pattern, options);
        if (!matches.MoveNext()) return false;
        return (matches.Current.Index == 0);
    }

    /// <summary>文字列が指定のパターンで終わっているかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="pattern">除去するパターン</param>
    /// <returns>指定したパターンで終わっているか否か。</returns>
    public static bool EndsWithPattern(this string? self, [StringSyntax(StringSyntaxAttribute.Regex)] string pattern)
        => self.AsSpan().EndsWithPattern(pattern, default);

    /// <summary>文字列が指定のパターンで終わっているかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="pattern">除去するパターン</param>
    /// <param name="options">マッチオプション</param>
    /// <returns>指定したパターンで終わっているか否か。</returns>
    public static bool EndsWithPattern(this string? self, [StringSyntax(StringSyntaxAttribute.Regex, nameof(options))] string pattern, RegexOptions options)
        => self.AsSpan().EndsWithPattern(pattern, options);

    /// <summary>文字列が指定のパターンで終わっているかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="pattern">除去するパターン</param>
    /// <returns>指定したパターンで終わっているか否か。</returns>
    public static bool EndsWithPattern(this ReadOnlySpan<char> self, [StringSyntax(StringSyntaxAttribute.Regex)] string pattern)
        => self.EndsWithPattern(pattern, default);

    /// <summary>文字列が指定のパターンで終わっているかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="pattern">除去するパターン</param>
    /// <param name="options">マッチオプション</param>
    /// <returns>指定したパターンで終わっているか否か。</returns>
    public static bool EndsWithPattern(this ReadOnlySpan<char> self, [StringSyntax(StringSyntaxAttribute.Regex, nameof(options))] string pattern, RegexOptions options)
    {
        var matches = Regex.EnumerateMatches(self, pattern, options);
        if (!matches.MoveNext()) return false;

        var index = matches.Current.Index;
        var length = matches.Current.Length;
        while (matches.MoveNext())
        {
            index = matches.Current.Index;
            length = matches.Current.Length;
        }

        return ((index + length) == self.Length);
    }

    /// <summary>文字列が指定のパターンで始まっている場合に除去した文字列を取得する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="pattern">除去するパターン</param>
    /// <returns>指定したパターンで始まっていれば除去した文字列。そうでなければ元の文字列。</returns>
    public static ReadOnlySpan<char> TrimStartPattern(this string? self, [StringSyntax(StringSyntaxAttribute.Regex)] string pattern)
        => self.AsSpan().TrimStartPattern(pattern, default);

    /// <summary>文字列が指定のパターンで始まっている場合に除去した文字列を取得する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="pattern">除去するパターン</param>
    /// <param name="options">マッチオプション</param>
    /// <returns>指定したパターンで始まっていれば除去した文字列。そうでなければ元の文字列。</returns>
    public static ReadOnlySpan<char> TrimStartPattern(this string? self, [StringSyntax(StringSyntaxAttribute.Regex, nameof(options))] string pattern, RegexOptions options)
        => self.AsSpan().TrimStartPattern(pattern, options);

    /// <summary>文字列が指定のパターンで始まっている場合に除去した文字列を取得する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="pattern">除去するパターン</param>
    /// <returns>指定したパターンで始まっていれば除去した文字列。そうでなければ元の文字列。</returns>
    public static ReadOnlySpan<char> TrimStartPattern(this ReadOnlySpan<char> self, [StringSyntax(StringSyntaxAttribute.Regex)] string pattern)
        => self.TrimStartPattern(pattern, default);

    /// <summary>文字列が指定のパターンで始まっている場合に除去した文字列を取得する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="pattern">除去するパターン</param>
    /// <param name="options">マッチオプション</param>
    /// <returns>指定したパターンで始まっていれば除去した文字列。そうでなければ元の文字列。</returns>
    public static ReadOnlySpan<char> TrimStartPattern(this ReadOnlySpan<char> self, [StringSyntax(StringSyntaxAttribute.Regex, nameof(options))] string pattern, RegexOptions options)
    {
        var matches = Regex.EnumerateMatches(self, pattern, options);
        if (!matches.MoveNext()) return self;
        if (matches.Current.Index != 0) return self;
        return self[matches.Current.Length..];
    }

    /// <summary>文字列が指定のパターンで終わっている場合に除去した文字列を取得する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="pattern">除去するパターン</param>
    /// <returns>指定したパターンで始まっていれば除去した文字列。そうでなければ元の文字列。</returns>
    public static ReadOnlySpan<char> TrimEndPattern(this string? self, [StringSyntax(StringSyntaxAttribute.Regex)] string pattern)
        => self.AsSpan().TrimEndPattern(pattern, default);

    /// <summary>文字列が指定のパターンで終わっている場合に除去した文字列を取得する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="pattern">除去するパターン</param>
    /// <param name="options">マッチオプション</param>
    /// <returns>指定したパターンで始まっていれば除去した文字列。そうでなければ元の文字列。</returns>
    public static ReadOnlySpan<char> TrimEndPattern(this string? self, [StringSyntax(StringSyntaxAttribute.Regex, nameof(options))] string pattern, RegexOptions options)
        => self.AsSpan().TrimEndPattern(pattern, options);

    /// <summary>文字列が指定のパターンで終わっている場合に除去した文字列を取得する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="pattern">除去するパターン</param>
    /// <returns>指定したパターンで始まっていれば除去した文字列。そうでなければ元の文字列。</returns>
    public static ReadOnlySpan<char> TrimEndPattern(this ReadOnlySpan<char> self, [StringSyntax(StringSyntaxAttribute.Regex)] string pattern)
        => self.TrimEndPattern(pattern, default);

    /// <summary>文字列が指定のパターンで終わっている場合に除去した文字列を取得する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="pattern">除去するパターン</param>
    /// <param name="options">マッチオプション</param>
    /// <returns>指定したパターンで始まっていれば除去した文字列。そうでなければ元の文字列。</returns>
    public static ReadOnlySpan<char> TrimEndPattern(this ReadOnlySpan<char> self, [StringSyntax(StringSyntaxAttribute.Regex, nameof(options))] string pattern, RegexOptions options)
    {
        var matches = Regex.EnumerateMatches(self, pattern, options);
        if (!matches.MoveNext()) return self;

        var index = matches.Current.Index;
        var length = matches.Current.Length;
        while (matches.MoveNext())
        {
            index = matches.Current.Index;
            length = matches.Current.Length;
        }
        if ((index + length) != self.Length) return self;
        return self[..index];
    }
}
