using System.Text.RegularExpressions;

namespace Lestaly.Extensions;

/// <summary>
/// 文字列と正規表現関連の拡張メソッド
/// </summary>
public static class StringRegexExtensions
{
    /// <summary>文字列が正規表現にマッチするかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="pattern">正規表現パターン</param>
    /// <returns>マッチするか否か</returns>
    public static bool IsMatch(this string self, string pattern)
    {
        return Regex.IsMatch(self, pattern);
    }

    /// <summary>文字列が正規表現にマッチするかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="pattern">正規表現パターン</param>
    /// <param name="options">正規表現オプション</param>
    /// <returns>マッチするか否か</returns>
    public static bool IsMatch(this string self, string pattern, RegexOptions options)
    {
        return Regex.IsMatch(self, pattern, options);
    }

    /// <summary>文字列に正規表現をマッチさせた結果を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="pattern">正規表現パターン</param>
    /// <returns>マッチ結果</returns>
    public static Match Match(this string self, string pattern)
    {
        return Regex.Match(self, pattern);
    }

    /// <summary>文字列に正規表現をマッチさせた結果を取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="pattern">正規表現パターン</param>
    /// <param name="options">正規表現オプション</param>
    /// <returns>マッチ結果</returns>
    public static Match Match(this string self, string pattern, RegexOptions options)
    {
        return Regex.Match(self, pattern, options);
    }

    /// <summary>文字列に正規表現をマッチさせた結果をすべて取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="pattern">正規表現パターン</param>
    /// <returns>マッチ結果コレクション</returns>
    public static MatchCollection Matches(this string self, string pattern)
    {
        return Regex.Matches(self, pattern);
    }

    /// <summary>文字列に正規表現をマッチさせた結果をすべて取得する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="pattern">正規表現パターン</param>
    /// <param name="options">正規表現オプション</param>
    /// <returns>マッチ結果コレクション</returns>
    public static MatchCollection Matches(this string self, string pattern, RegexOptions options)
    {
        return Regex.Matches(self, pattern, options);
    }

    /// <summary>正規表現にマッチした個所を文字列置き換えする</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="pattern">正規表現パターン</param>
    /// <param name="replacement">マッチ個所を置き換える文字列</param>
    /// <returns>置き換え結果文字列</returns>
    public static string MatchReplace(this string self, string pattern, string replacement)
    {
        return Regex.Replace(self, pattern, replacement);
    }

    /// <summary>正規表現にマッチした個所を文字列置き換えする</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="pattern">正規表現パターン</param>
    /// <param name="replacement">マッチ個所を置き換える文字列</param>
    /// <param name="options">正規表現オプション</param>
    /// <returns>置き換え結果文字列</returns>
    public static string MatchReplace(this string self, string pattern, string replacement, RegexOptions options)
    {
        return Regex.Replace(self, pattern, replacement, options);
    }

    /// <summary>正規表現にマッチした個所で文字列を分割する</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="pattern">正規表現パターン</param>
    /// <returns>分割された文字列の配列</returns>
    public static string[] MatchSplit(this string self, string pattern)
    {
        return Regex.Split(self, pattern);
    }

    /// <summary>正規表現にマッチした個所を文字列置き換えする</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="pattern">正規表現パターン</param>
    /// <param name="options">正規表現オプション</param>
    /// <returns>分割された文字列の配列</returns>
    public static string[] MatchSplit(this string self, string pattern, RegexOptions options)
    {
        return Regex.Split(self, pattern, options);
    }
}
