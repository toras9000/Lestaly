using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

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
    [return: NotNullIfNotNull(nameof(alt))]
    public static string? WhenEmpty(this string? self, string? alt)
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
    [return: NotNullIfNotNull(nameof(alt))]
    public static string? WhenWhite(this string? self, string? alt)
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
    public static bool StartsWithAny(this string self, IEnumerable<string> values, bool ignoreCase)
        => self != null && self.AsSpan().StartsWithAny(values, ignoreCase);

    /// <summary>文字列が指定のテキストのいずれかで始まるかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="values">始まるかを判定する文字列。nullや空の要素は無視される。</param>
    /// <param name="comparison">文字列比較方法</param>
    /// <returns>いずれかで始まっているか否か</returns>
    public static bool StartsWithAny(this string self, IEnumerable<string> values, StringComparison comparison = StringComparison.Ordinal)
        => self != null && self.AsSpan().StartsWithAny(values, comparison);

    /// <summary>文字列が指定のテキストのいずれかで始まるかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="values">始まるかを判定する文字列。nullや空の要素は無視される。</param>
    /// <param name="ignoreCase">大文字/小文字の違いを無視するか否か(序数ベース)</param>
    /// <returns>いずれかで始まっているか否か</returns>
    public static bool StartsWithAny(this ReadOnlySpan<char> self, IEnumerable<string> values, bool ignoreCase)
        => self.StartsWithAny(values, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);

    /// <summary>文字列が指定のテキストのいずれかで始まるかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="values">始まるかを判定する文字列。nullや空の要素は無視される。</param>
    /// <param name="comparison">文字列比較方法</param>
    /// <returns>いずれかで始まっているか否か</returns>
    public static bool StartsWithAny(this ReadOnlySpan<char> self, IEnumerable<string> values, StringComparison comparison = StringComparison.Ordinal)
    {
        foreach (var value in values)
        {
            if (string.IsNullOrEmpty(value)) continue;
            if (self.StartsWith(value, comparison)) return true;
        }
        return false;
    }

    /// <summary>文字列が指定のテキストのいずれかで始まるかを判定する。(大文字/小文字区別なし)</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="values">始まるかを判定する文字列。nullや空の要素は無視される。</param>
    /// <returns>いずれかで始まっているか否か</returns>
    public static bool StartsWithAnyIgnoreCase(this string self, IEnumerable<string> values)
        => self != null && self.AsSpan().StartsWithAny(values, StringComparison.OrdinalIgnoreCase);

    /// <summary>文字列が指定のテキストのいずれかで始まるかを判定する。(大文字/小文字区別なし)</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="values">始まるかを判定する文字列。nullや空の要素は無視される。</param>
    /// <returns>いずれかで始まっているか否か</returns>
    public static bool StartsWithAnyIgnoreCase(this ReadOnlySpan<char> self, IEnumerable<string> values)
        => self.StartsWithAny(values, StringComparison.OrdinalIgnoreCase);

    /// <summary>文字列が指定のテキストのいずれかで終わるかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="values">終わるかを判定する文字列。nullや空の要素は無視される。</param>
    /// <param name="ignoreCase">大文字/小文字の違いを無視するか否か(序数ベース)</param>
    /// <returns>いずれかで終わっているか否か</returns>
    public static bool EndsWithAny(this string self, IEnumerable<string> values, bool ignoreCase)
        => self != null && self.AsSpan().EndsWithAny(values, ignoreCase);

    /// <summary>文字列が指定のテキストのいずれかで終わるかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="values">終わるかを判定する文字列。nullや空の要素は無視される。</param>
    /// <param name="comparison">文字列比較方法</param>
    /// <returns>いずれかで終わっているか否か</returns>
    public static bool EndsWithAny(this string self, IEnumerable<string> values, StringComparison comparison = StringComparison.Ordinal)
        => self != null && self.AsSpan().EndsWithAny(values, comparison);

    /// <summary>文字列が指定のテキストのいずれかで終わるかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="values">終わるかを判定する文字列。nullや空の要素は無視される。</param>
    /// <param name="ignoreCase">大文字/小文字の違いを無視するか否か(序数ベース)</param>
    /// <returns>いずれかで終わっているか否か</returns>
    public static bool EndsWithAny(this ReadOnlySpan<char> self, IEnumerable<string> values, bool ignoreCase)
        => self.EndsWithAny(values, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);

    /// <summary>文字列が指定のテキストのいずれかで終わるかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="values">終わるかを判定する文字列。nullや空の要素は無視される。</param>
    /// <param name="comparison">文字列比較方法</param>
    /// <returns>いずれかで終わっているか否か</returns>
    public static bool EndsWithAny(this ReadOnlySpan<char> self, IEnumerable<string> values, StringComparison comparison = StringComparison.Ordinal)
    {
        foreach (var value in values)
        {
            if (string.IsNullOrEmpty(value)) continue;
            if (self.EndsWith(value, comparison)) return true;
        }
        return false;
    }

    /// <summary>文字列が指定のテキストのいずれかで終わるかを判定する。(大文字/小文字区別なし)</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="values">終わるかを判定する文字列。nullや空の要素は無視される。</param>
    /// <returns>いずれかで終わっているか否か</returns>
    public static bool EndsWithAnyIgnoreCase(this string self, IEnumerable<string> values)
        => self != null && self.AsSpan().EndsWithAny(values, StringComparison.OrdinalIgnoreCase);

    /// <summary>文字列が指定のテキストのいずれかで終わるかを判定する。(大文字/小文字区別なし)</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="values">終わるかを判定する文字列。nullや空の要素は無視される。</param>
    /// <returns>いずれかで終わっているか否か</returns>
    public static bool EndsWithAnyIgnoreCase(this ReadOnlySpan<char> self, IEnumerable<string> values)
        => self.EndsWithAny(values, StringComparison.OrdinalIgnoreCase);

    /// <summary>文字列が指定のシーケンスのいずれかと一致するかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="values">比較する文字列のシーケンス。</param>
    /// <param name="comparison">文字列比較方法</param>
    /// <returns>いずれかと一致するか否か</returns>
    public static bool EqualsAny(this string? self, IEnumerable<string?> values, StringComparison comparison = StringComparison.Ordinal)
    {
        foreach (var value in values)
        {
            if (string.Equals(self, value, comparison)) return true;
        }
        return false;
    }

    /// <summary>文字列が指定のシーケンスのいずれかと一致するかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="values">比較する文字列のシーケンス。nullは無視される。</param>
    /// <param name="comparison">文字列比較方法</param>
    /// <returns>いずれかと一致するか否か</returns>
    public static bool EqualsAny(this ReadOnlySpan<char> self, IEnumerable<string> values, StringComparison comparison = StringComparison.Ordinal)
    {
        foreach (var value in values)
        {
            if (value == null) continue;
            if (self.Equals(value, comparison)) return true;
        }
        return false;
    }

    /// <summary>文字列が指定のシーケンスのいずれかと一致するかを判定する。(大文字/小文字区別なし)</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="values">比較する文字列のシーケンス。</param>
    /// <returns>いずれかと一致するか否か</returns>
    public static bool EqualsAnyIgnoreCase(this string? self, IEnumerable<string?> values)
        => self.EqualsAny(values, StringComparison.OrdinalIgnoreCase);

    /// <summary>文字列が指定のシーケンスのいずれかと一致するかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="values">比較する文字列のシーケンス。nullは無視される。</param>
    /// <returns>いずれかと一致するか否か</returns>
    public static bool EqualsAnyIgnoreCase(this ReadOnlySpan<char> self, IEnumerable<string> values)
        => self.EqualsAny(values, StringComparison.OrdinalIgnoreCase);

    /// <summary>文字列が指定のシーケンスのいずれかと一致するかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="values">比較する文字列のシーケンス。nullは無視される。</param>
    /// <returns>いずれかと一致するか否か</returns>
    public static bool RoughAny(this string? self, IEnumerable<string> values)
    {
        if (self == null) return values.Contains(null);

        var core = self.AsSpan().Trim();
        foreach (var value in values)
        {
            if (value == null) continue;
            if (core.Equals(value.AsSpan().Trim(), StringComparison.OrdinalIgnoreCase)) return true;
        }
        return false;
    }

    /// <summary>文字列が指定のシーケンスのいずれかと一致するかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="values">比較する文字列のシーケンス。nullは無視される。</param>
    /// <returns>いずれかと一致するか否か</returns>
    public static bool RoughAny(this ReadOnlySpan<char> self, IEnumerable<string> values)
    {
        var core = self.Trim();
        foreach (var value in values)
        {
            if (value == null) continue;
            if (core.Equals(value.AsSpan().Trim(), StringComparison.OrdinalIgnoreCase)) return true;
        }
        return false;
    }

    /// <summary>文字列が指定の文字列で始まっている場合に除去した文字列を取得する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="value">除去する文字列</param>
    /// <param name="comparison">文字列比較方法</param>
    /// <returns>指定した文字列で始まっていれば除去した文字列。そうでなければ元の文字列。</returns>
    public static ReadOnlySpan<char> TrimStartString(this string? self, ReadOnlySpan<char> value, StringComparison comparison = StringComparison.Ordinal)
        => self.AsSpan().StartsWith(value, comparison) == true ? self.AsSpan(value.Length..) : self;

    /// <summary>文字列が指定の文字列で始まっている場合に除去した文字列を取得する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="value">除去する文字列</param>
    /// <param name="ignoreCase">大文字/小文字の違いを無視するか否か(序数ベース)</param>
    /// <returns>指定した文字列で始まっていれば除去した文字列。そうでなければ元の文字列。</returns>
    public static ReadOnlySpan<char> TrimStartString(this string? self, ReadOnlySpan<char> value, bool ignoreCase)
        => self.AsSpan().StartsWith(value, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) == true ? self.AsSpan(value.Length..) : self;

    /// <summary>文字列が指定の文字列で始まっている場合に除去した文字列を取得する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="value">除去する文字列</param>
    /// <param name="comparison">文字列比較方法</param>
    /// <returns>指定した文字列で始まっていれば除去した文字列。そうでなければ元の文字列。</returns>
    public static ReadOnlySpan<char> TrimStartString(this ReadOnlySpan<char> self, ReadOnlySpan<char> value, StringComparison comparison = StringComparison.Ordinal)
        => self.StartsWith(value, comparison) == true ? self[value.Length..] : self;

    /// <summary>文字列が指定の文字列で始まっている場合に除去した文字列を取得する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="value">除去する文字列</param>
    /// <param name="ignoreCase">大文字/小文字の違いを無視するか否か(序数ベース)</param>
    /// <returns>指定した文字列で始まっていれば除去した文字列。そうでなければ元の文字列。</returns>
    public static ReadOnlySpan<char> TrimStartString(this ReadOnlySpan<char> self, ReadOnlySpan<char> value, bool ignoreCase)
        => self.StartsWith(value, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) == true ? self[value.Length..] : self;

    /// <summary>文字列が指定の文字列で終わっている場合に除去した文字列を取得する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="value">除去する文字列</param>
    /// <param name="comparison">文字列比較方法</param>
    /// <returns>指定した文字列で終わっていれば除去した文字列。そうでなければ元の文字列。</returns>
    public static ReadOnlySpan<char> TrimEndString(this string? self, ReadOnlySpan<char> value, StringComparison comparison = StringComparison.Ordinal)
        => self.AsSpan().EndsWith(value, comparison) == true ? self.AsSpan(0..^value.Length) : self;

    /// <summary>文字列が指定の文字列で終わっている場合に除去した文字列を取得する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="value">除去する文字列</param>
    /// <param name="ignoreCase">大文字/小文字の違いを無視するか否か(序数ベース)</param>
    /// <returns>指定した文字列で終わっていれば除去した文字列。そうでなければ元の文字列。</returns>
    public static ReadOnlySpan<char> TrimEndString(this string? self, string value, bool ignoreCase)
        => self.AsSpan().EndsWith(value, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) == true ? self.AsSpan(0..^value.Length) : self;

    /// <summary>文字列が指定の文字列で終わっている場合に除去した文字列を取得する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="value">除去する文字列</param>
    /// <param name="comparison">文字列比較方法</param>
    /// <returns>指定した文字列で終わっていれば除去した文字列。そうでなければ元の文字列。</returns>
    public static ReadOnlySpan<char> TrimEndString(this ReadOnlySpan<char> self, ReadOnlySpan<char> value, StringComparison comparison = StringComparison.Ordinal)
        => self.EndsWith(value, comparison) == true ? self[0..^value.Length] : self;

    /// <summary>文字列が指定の文字列で終わっている場合に除去した文字列を取得する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="value">除去する文字列</param>
    /// <param name="ignoreCase">大文字/小文字の違いを無視するか否か(序数ベース)</param>
    /// <returns>指定した文字列で終わっていれば除去した文字列。そうでなければ元の文字列。</returns>
    public static ReadOnlySpan<char> TrimEndString(this ReadOnlySpan<char> self, string value, bool ignoreCase)
        => self.EndsWith(value, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) == true ? self[0..^value.Length] : self;

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

    /// <summary>文字列を特定文字で分割する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="marker">分割文字</param>
    /// <param name="next">分割文字の後ろの文字列</param>
    /// <returns>分割文字の前の文字列</returns>
    public static ReadOnlySpan<char> SplitAt(this string? self, char marker, out ReadOnlySpan<char> next)
        => self.AsSpan().SplitAt(marker, out next);

    /// <summary>文字列を特定文字で分割する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="marker">分割文字</param>
    /// <param name="next">分割文字の後ろの文字列</param>
    /// <returns>分割文字の前の文字列</returns>
    public static ReadOnlySpan<char> SplitAt(this ReadOnlySpan<char> self, char marker, out ReadOnlySpan<char> next)
    {
        var idx = self.IndexOf(marker);
        if (idx < 0)
        {
            next = self[^0..];
            return self;
        }

        next = self[(idx + 1)..];
        return self[..idx];
    }

    /// <summary>文字列を特定文字列で分割する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="delimiter">分割文字列</param>
    /// <param name="next">分割文字列の後ろの文字列</param>
    /// <returns>分割文字列の前の文字列</returns>
    public static ReadOnlySpan<char> SplitAt(this string? self, ReadOnlySpan<char> delimiter, out ReadOnlySpan<char> next)
    => self.AsSpan().SplitAt(delimiter, out next);

    /// <summary>文字列を特定文字列で分割する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="delimiter">分割文字列</param>
    /// <param name="next">分割文字列の後ろの文字列</param>
    /// <returns>分割文字列の前の文字列</returns>
    public static ReadOnlySpan<char> SplitAt(this ReadOnlySpan<char> self, ReadOnlySpan<char> delimiter, out ReadOnlySpan<char> next)
    {
        var idx = self.IndexOf(delimiter);
        if (idx < 0)
        {
            next = self[^0..];
            return self;
        }

        next = self[(idx + delimiter.Length)..];
        return self[..idx];
    }

    /// <summary>文字列を特定文字列で分割する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="delimiters">分割文字のセット</param>
    /// <param name="next">分割文字列の後ろの文字列</param>
    /// <returns>分割文字列の前の文字列</returns>
    public static ReadOnlySpan<char> SplitAtAny(this string? self, ReadOnlySpan<char> delimiters, out ReadOnlySpan<char> next)
    => self.AsSpan().SplitAtAny(delimiters, out next);

    /// <summary>文字列を特定文字列で分割する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="delimiters">分割文字のセット</param>
    /// <param name="next">分割文字列の後ろの文字列</param>
    /// <returns>分割文字列の前の文字列</returns>
    public static ReadOnlySpan<char> SplitAtAny(this ReadOnlySpan<char> self, ReadOnlySpan<char> delimiters, out ReadOnlySpan<char> next)
    {
        var idx = self.IndexOfAny(delimiters);
        if (idx < 0)
        {
            next = self[^0..];
            return self;
        }

        next = self[(idx + 1)..];
        return self[..idx];
    }

    /// <summary>文字列の行を列挙する。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>テキスト行シーケンス</returns>
    public static IEnumerable<string> AsTextLines(this string self)
    {
        // パラメータチェック
        ArgumentNullException.ThrowIfNull(self);

        // テキスト行を列挙
        var idx = 0;
        while (idx < self.Length)
        {
            // 改行を検索
            var pos = self.IndexOfAny(LineBreakChars, idx);
            if (pos < 0) break;

            // 行を列挙
            yield return self[idx..pos];

            // 次の位置へ。CRLFの場合は1つの改行として扱う
            idx = pos + 1;
            if (idx < self.Length && self[idx - 1] == '\r' && self[idx] == '\n')
            {
                idx++;
            }
        }

        // 最期の部分を列挙
        yield return self[idx..];
    }

    /// <summary>文字列のシーケンスからnull/空を取り除く。</summary>
    /// <param name="self">文字列のシーケンス</param>
    /// <returns>null/空以外のシーケンス</returns>
    public static IEnumerable<string> DropEmpty(this IEnumerable<string?> self)
        => self.Where(s => !string.IsNullOrEmpty(s))!;

    /// <summary>文字列のシーケンスからnull/空白文字列を取り除く。</summary>
    /// <param name="self">文字列のシーケンス</param>
    /// <returns>null/空白文字列以外のシーケンス</returns>
    public static IEnumerable<string> DropWhite(this IEnumerable<string?> self)
        => self.Where(s => !string.IsNullOrWhiteSpace(s))!;

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

        ArgumentNullException.ThrowIfNull(mixer);
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
        ArgumentNullException.ThrowIfNull(mixer);
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

    /// <summary>文字列をクォートする。</summary>
    /// <param name="text">対象文字列。nullの場合は空文字列と同じ扱いとする。</param>
    /// <param name="quote">クォートキャラクタ</param>
    /// <param name="escape">対象文字列中のクォートキャラクタをエスケープするキャラクタ</param>
    /// <returns>クォートされたキャラクタ</returns>
    public static string Quote(this string? text, char quote = '"', char? escape = null)
    {
        if (text == null) return new string(quote, 2);
        return text.AsSpan().Quote(quote, escape);
    }

    /// <summary>文字列をクォートする。</summary>
    /// <param name="text">対象文字列。nullの場合は空文字列と同じ扱いとする。</param>
    /// <param name="quote">クォートキャラクタ</param>
    /// <param name="escape">対象文字列中のクォートキャラクタをエスケープするキャラクタ</param>
    /// <returns>クォートされたキャラクタ</returns>
    public static string Quote(this ReadOnlySpan<char> text, char quote = '"', char? escape = null)
    {
        if (text.IsEmpty) return new string(quote, 2);
        var esc = escape ?? quote;
        var buffer = new StringBuilder(text.Length + 10);
        buffer.Append(quote);
        foreach (var c in text)
        {
            if (c == quote) buffer.Append(esc);
            buffer.Append(c);
        }
        buffer.Append(quote);

        return buffer.ToString();
    }

    /// <summary>文字列をアンクォートする。</summary>
    /// <param name="self">対象文字列。</param>
    /// <param name="quotes">クォートキャラクタ候補。空の場合はダブル/シングルクォートキャラクタを候補とする。</param>
    /// <param name="escape">クォートキャラクタをエスケープしているキャラクタ。指定がない場合はクォートキャラクタ2つで</param>
    /// <returns>アンクォートされた文字列</returns>
    [return: NotNullIfNotNull(nameof(self))]
    public static string? Unquote(this string self, ReadOnlySpan<char> quotes = default, char? escape = null)
    {
        // nullやクォート分の幅がなければそのまま返す。
        if (self == null || self.Length < 2) return self;

        // クォートキャラクタ候補。指定があればそれを、無ければダブル・シングルクォートキャラクタを候補とする。
        var candidates = (quotes.Length == 0) ? ['"', '\'',] : quotes;

        // 最初の文字がクォート文字候補のいずれかであるかを判別
        // クォート文字で始まらない場合は元の文字列を返す
        if (candidates.IndexOf(self[0]) < 0) return self;

        // 両端がクォートキャラクタであるかを判定。
        var quoteChar = self[0];
        if (quoteChar != self[^1]) return self;

        // 前後クォートを除去した部分を取得
        var core = self.AsSpan(1, self.Length - 2);

        // クォートキャラクタのエスケープ表現
        var escaped = (stackalloc char[] { escape ?? quoteChar, quoteChar, });

        // エスケープ表見があるか検索
        var escIdx = core.IndexOf(escaped);
        if (escIdx < 0) return core.ToString();

        // エスケープ解除した文字列を作成
        var buffer = new StringBuilder(core.Length);
        var scan = core;
        do
        {
            // エスケープ文字位置前までとエスケープ解除文字を追加
            buffer.Append(scan[0..escIdx]);
            buffer.Append(quoteChar);

            // 次の位置からエスケープ表現を検索
            scan = scan[(escIdx + 2)..];
            escIdx = scan.IndexOf(escaped);
        }
        while (0 <= escIdx);

        // 残りの部分を追加
        buffer.Append(scan);

        return buffer.ToString();
    }

    /// <summary>文字列のテキスト要素を列挙する。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>テキスト要素シーケンス</returns>
    public static IEnumerable<string> AsTextElements(this string self)
    {
        // パラメータチェック
        ArgumentNullException.ThrowIfNull(self);

        // テキスト要素を列挙
        var elementer = StringInfo.GetTextElementEnumerator(self);
        while (elementer.MoveNext())
        {
            yield return (string)elementer.Current;
        }
    }

    /// <summary>文字列のテキスト要素数を取得する。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>テキスト要素数</returns>
    public static int TextElementCount(this string self)
        => self.AsSpan().TextElementCount();

    /// <summary>文字列のテキスト要素数を取得する。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>テキスト要素数</returns>
    public static int TextElementCount(this ReadOnlySpan<char> self)
    {
        var count = 0;
        var scan = self;
        while (!scan.IsEmpty)
        {
            scan = scan[StringInfo.GetNextTextElementLength(scan)..];
            count++;
        }

        return count;
    }

    /// <summary>文字列の先頭から指定された長さの文字要素を切り出す。</summary>
    /// <param name="self">元になる文字列</param>
    /// <param name="count">切り出す文字要素の長さ。</param>
    /// <returns>切り出された文字列</returns>
    public static ReadOnlySpan<char> CutLeftElements(this string? self, int count)
        => self.AsSpan().CutLeftElements(count);

    /// <summary>文字列の先頭から指定された長さの文字要素を切り出す。</summary>
    /// <param name="self">元になる文字列</param>
    /// <param name="count">切り出す文字要素の長さ。</param>
    /// <returns>切り出された文字列</returns>
    public static ReadOnlySpan<char> CutLeftElements(this ReadOnlySpan<char> self, int count)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(count, nameof(count));

        // 文字要素を列挙しながら指定要素数までカウント
        var taked = 0;
        var length = 0;
        var scan = self;
        while (taked < count && !scan.IsEmpty)
        {
            // 要素長を取得
            var elemLen = StringInfo.GetNextTextElementLength(scan);

            // 要素数分の長さをカウント
            length += elemLen;
            taked++;

            // 次の要素へ
            scan = scan[elemLen..];
        }

        // 切り出した文字列を返却
        return self[..length];
    }

    /// <summary>文字列の末尾にある指定された長さの文字要素を切り出す。</summary>
    /// <param name="self">元になる文字列</param>
    /// <param name="count">切り出す文字要素の長さ。</param>
    /// <returns>切り出された文字列</returns>
    public static ReadOnlySpan<char> CutRightElements(this string? self, int count)
        => self.AsSpan().CutRightElements(count);

    /// <summary>文字列の末尾にある指定された長さの文字要素を切り出す。</summary>
    /// <param name="self">元になる文字列</param>
    /// <param name="count">切り出す文字要素の長さ。</param>
    /// <returns>切り出された文字列</returns>
    public static ReadOnlySpan<char> CutRightElements(this ReadOnlySpan<char> self, int count)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(count, nameof(count));

        // 対象文字列が空の場合は結果も空
        if (count == 0) return self[^0..];

        // 最後の文字を蓄積するバッファ
        var lengths = new Queue<int>(capacity: count);

        var scan = self;
        while (!scan.IsEmpty)
        {
            // 要素長を取得
            var elemLen = StringInfo.GetNextTextElementLength(scan);

            // 取得要素数分の最終長さを管理
            if (count <= lengths.Count) lengths.Dequeue();
            lengths.Enqueue(elemLen);

            // 次の要素へ
            scan = scan[elemLen..];
        }

        // 切り出すべき末尾の長さ算出
        var rightLen = lengths.Sum();
        return self[^rightLen..];
    }

    /// <summary>
    /// 文字列を指定の長さに省略する。
    /// このメソッドでは string の Length 基準での長さ制限となる。
    /// </summary>
    /// <param name="self">元の文字列</param>
    /// <param name="length">制限する文字列の長さ</param>
    /// <param name="marker">省略時に付与するマーカ文字列</param>
    /// <returns>必要に応じて省略した文字列。</returns>
    public static string EllipsisByLength(this string self, int length, string? marker = null)
    {
        // パラメータチェック
        ArgumentNullException.ThrowIfNull(self);
        ArgumentOutOfRangeException.ThrowIfNegative(length);

        // マーカーが指定の長さを超えている場合は矛盾するのでパラメータ指定が正しくない。
        // 省略されずマーカーが使用されない場合もあり得るが、パラメータ length と marker の関係性が正しくないのであれば揺れなく異常検出できようにしている。
        var markerLen = marker?.Length ?? 0;
        if (length < markerLen) throw new ArgumentException($"{nameof(marker)} is grator than {nameof(length)}");

        // 元の文字列が指定の長さに収まる場合はそのまま返却
        if (self.Length <= length)
        {
            return self;
        }

        // 省略文字列として切り出す長さを算出。マーカを付与するのでその分を除いた長さ。
        var takeLen = length - markerLen;

        // 書記素クラスタを分割しないよう、テキスト要素を認識して切り出す。
        var builder = new StringBuilder();
        var elementer = StringInfo.GetTextElementEnumerator(self);
        while (0 < takeLen && elementer.MoveNext())
        {
            // テキスト要素を追加すると切り詰め長を超えてしまうようであればここで終わり
            var element = (string)elementer.Current;
            if (takeLen < element.Length)
            {
                break;
            }
            // 切り出し文字列に追加
            builder.Append(element);
            takeLen -= element.Length;
        }

        // マーカーを追加
        builder.Append(marker);

        // 省略した文字列を返却
        return builder.ToString();
    }

    /// <summary>
    /// 文字列を指定の長さに省略する。
    /// このメソッドでは string の 文字要素 基準での長さ制限となる。
    /// </summary>
    /// <param name="self">元の文字列</param>
    /// <param name="count">制限する文字列の文字要素数</param>
    /// <param name="marker">省略時に付与するマーカ文字列</param>
    /// <returns>必要に応じて省略した文字列。</returns>
    public static string EllipsisByElements(this string self, int count, string? marker = null)
    {
        // パラメータチェック
        ArgumentNullException.ThrowIfNull(self);
        ArgumentOutOfRangeException.ThrowIfNegative(count);

        // マーカーが指定の長さを超えている場合は矛盾するのでパラメータ指定が正しくない。
        // 省略されずマーカーが使用されない場合もあり得るが、パラメータ width と marker の関係性が正しくないのであれば揺れなく異常検出できようにしている。
        var markerCount = marker?.TextElementCount() ?? 0;
        if (count < markerCount) throw new ArgumentException($"{nameof(marker)} is grator than {nameof(count)}");

        // 明かな状況を処理
        if (string.IsNullOrEmpty(self)) return self;
        if (count == 0) return string.Empty;

        // 省略文字列として切り出す長さを算出。マーカを付与するのでその分を除いた長さ。
        var ellipsisCount = count - markerCount;

        // 書記素クラスタペアを分割しないよう、テキスト要素を認識して切り出す。
        var builder = new StringBuilder();
        var elementer = StringInfo.GetTextElementEnumerator(self);
        var sumCount = 0;       // 切り出し合計幅
        var ellipsedLen = 0;    // 省略が発生する場合に採用する文字列長さ
        while (elementer.MoveNext())
        {
            // 追加すると切り詰め長を超えないかをチェック
            if (count < (sumCount + 1))
            {
                // 超える場合は省略時の長さ＋マーカーの内容で切り出し終了
                builder.Length = ellipsedLen;
                builder.Append(marker);
                break;
            }
            else
            {
                // 切り出し文字列に追加
                builder.Append(elementer.Current);

                // 省略が発生した場合に使う文字列Lengthを把握する。
                // 合計幅が省略時の許容長を越えない限りはその長さを利用できる。
                sumCount++;
                if (sumCount <= ellipsisCount)
                {
                    ellipsedLen = builder.Length;
                }
            }
        }

        // 省略した文字列を返却
        return builder.ToString();
    }

    /// <summary>文字列がnullや空であれば例外を送出する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="generator">例外オブジェクト生成デリゲート</param>
    /// <returns>対象文字列</returns>
    public static string ThrowIfEmpty(this string? self, Func<Exception>? generator = null)
        => string.IsNullOrEmpty(self) ? throw generator?.Invoke() ?? new InvalidDataException() : self;

    /// <summary>文字列がnullや空であれば例外を送出する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="generator">例外オブジェクト生成デリゲート</param>
    /// <returns>対象文字列</returns>
    public static ReadOnlySpan<char> ThrowIfEmpty(this ReadOnlySpan<char> self, Func<Exception>? generator = null)
        => self.IsEmpty ? throw generator?.Invoke() ?? new InvalidDataException() : self;

    /// <summary>文字列がnullや空白文字であれば例外を送出する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="generator">例外オブジェクト生成デリゲート</param>
    /// <returns>対象文字列</returns>
    public static string ThrowIfWhite(this string? self, Func<Exception>? generator = null)
        => string.IsNullOrWhiteSpace(self) ? throw generator?.Invoke() ?? new InvalidDataException() : self;

    /// <summary>文字列がnullや空白文字であれば例外を送出する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="generator">例外オブジェクト生成デリゲート</param>
    /// <returns>対象文字列</returns>
    public static ReadOnlySpan<char> ThrowIfWhite(this ReadOnlySpan<char> self, Func<Exception>? generator = null)
        => self.IsEmpty ? throw generator?.Invoke() ?? new InvalidDataException() : self;

    /// <summary>文字列がnullや空であればキャンセル例外を送出する。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>対象文字列</returns>
    public static string CancelIfEmpty(this string? self)
        => self.ThrowIfEmpty(() => new OperationCanceledException());

    /// <summary>文字列がnullや空であればキャンセル例外を送出する。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>対象文字列</returns>
    public static ReadOnlySpan<char> CancelIfEmpty(this ReadOnlySpan<char> self)
        => self.ThrowIfEmpty(() => new OperationCanceledException());

    /// <summary>文字列がnullや空白文字であればキャンセル例外を送出する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="trim">前後空白をトリムするか否か</param>
    /// <param name="unquote">クォート解除を試みるか否か</param>
    /// <returns>対象文字列</returns>
    public static string CancelIfWhite(this string? self, bool trim = false, bool unquote = false)
    {
        var target = self;
        if (unquote) target = target?.Unquote();
        if (trim) target = target?.Trim();
        return target.ThrowIfWhite(() => new OperationCanceledException());
    }

    /// <summary>文字列がnullや空白文字であればキャンセル例外を送出する。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>対象文字列</returns>
    public static ReadOnlySpan<char> CancelIfWhite(this ReadOnlySpan<char> self)
        => self.ThrowIfWhite(() => new OperationCanceledException());


    /// <summary>改行キャラクタ配列</summary>
    private static readonly char[] LineBreakChars = new[] { '\r', '\n', };
}
