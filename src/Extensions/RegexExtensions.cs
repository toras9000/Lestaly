using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Lestaly;

/// <summary>
/// 正規表現オブジェクトの拡張メソッド
/// </summary>
public static class RegexExtensions
{
    /// <summary>正規表現と文字列をマッチングし、にマッチするかを判定する。</summary>
    /// <param name="self">正規表現オブジェクト</param>
    /// <param name="text">マッチング対象文字列</param>
    /// <param name="selector">マッチ結果を文字列に変換するセレクタ</param>
    /// <param name="alt">マッチしなかった場合の代替文字列</param>
    /// <returns>マッチした場合はセレクタの結果。マッチしない場合は null </returns>
    [return: NotNullIfNotNull(nameof(alt))]
    public static string? MatchSelect(this Regex self, string text, Func<Match, string> selector, string? alt = default)
    {
        var match = self.Match(text);
        if (match.Success) return selector(match);
        return alt;
    }
}
