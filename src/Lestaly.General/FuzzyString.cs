using System.Buffers;
using System.Globalization;
using System.Text;

namespace Lestaly;

/// <summary>簡易的な曖昧文字列処理</summary>
public static class FuzzyString
{
    /// <summary>文字列から空白や制御文字を除去し正規化した文字列を作成する</summary>
    /// <param name="text">ソース文字列</param>
    /// <param name="ignores">追加の無視キャラクタ</param>
    /// <returns>正規化された文字列</returns>
    public static string Prune(ReadOnlySpan<char> text, SearchValues<char>? ignores = default)
    {
        // 正規化文字列構築バッファ
        var builder = new StringBuilder(text.Length);

        // ソース文字列を書記素毎に処理する。空白はスキップする。
        var scan = text.TrimStart();
        while (!scan.IsEmpty)
        {
            // 書記素長を取得
            var elemLen = StringInfo.GetNextTextElementLength(scan);
            if (elemLen == 1)
            {// 1キャラクタの書記素
                // 無視文字に該当するかを判定
                var ch = scan[0];
                var ignore = char.IsWhiteSpace(ch)
                          || char.IsControl(ch)
                          || ignores?.Contains(ch) == true
                          || char.GetUnicodeCategory(ch) is UnicodeCategory.Format;
                if (!ignore)
                {
                    // 無視キャラクタでない場合は正規化バッファに追加
                    builder.AppendNormalized([ch]);
                }
            }
            else
            {// 複数キャラクタの書記素
                // 書記素を正規化
                var elem = scan[..elemLen];
                builder.AppendNormalized(elem);
            }

            // 処理した書記素をスキップ。続く空白もスキップ。
            scan = scan[elemLen..].TrimStart();
        }

        return builder.ToString();
    }

    /// <summary>空白や制御文字を除去し正規化した比較を行う</summary>
    /// <param name="text1">文字列1</param>
    /// <param name="text2">文字列2</param>
    /// <param name="ignores">追加の無視キャラクタ</param>
    /// <param name="comparison">文字列比較タイプ</param>
    /// <returns>正規化結果が一致するかどうか</returns>
    public static bool Equivalent(ReadOnlySpan<char> text1, ReadOnlySpan<char> text2, SearchValues<char>? ignores = default, StringComparison comparison = StringComparison.Ordinal)
    {
        // できれば正規化文字列を作らずに比較したかったが、とりあえず機能実現を優先して正規化文字列同士で比較
        var norm1 = Prune(text1, ignores);
        var norm2 = Prune(text2, ignores);
        return norm1.Equals(norm2, comparison);
    }
}
