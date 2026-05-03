using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Lestaly;

/// <summary>
/// 正規表現オブジェクトの拡張メソッド
/// </summary>
public static class RegexExtensions
{
    /// <summary>Regex に対する拡張メソッド</summary>
    /// <param name="self">正規表現オブジェクト</param>
    extension(Regex self)
    {
        /// <summary>文字列への正規表現とマッチング結果を加工して文字列を返却する。</summary>
        /// <param name="text">マッチング対象文字列</param>
        /// <param name="selector">マッチ結果を文字列に変換するセレクタ</param>
        /// <param name="alt">マッチしなかった場合の代替文字列</param>
        /// <returns>マッチした場合はセレクタの結果。マッチしない場合は alt の値。</returns>
        [return: NotNullIfNotNull(nameof(alt))]
        public string? MatchSelect(string text, Func<Match, string> selector, string? alt = default)
        {
            var match = self.Match(text);
            if (match.Success) return selector(match);
            return alt;
        }
    }
}
