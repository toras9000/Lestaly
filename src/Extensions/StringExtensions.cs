using System.Globalization;
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
    public static string? FirstLine(this string? self)
    {
        if (string.IsNullOrEmpty(self)) return self;

        // 改行位置を検索
        var breakIdx = self.IndexOfAny(LineBreakChars);
        if (breakIdx < 0)
        {
            // 改行がない場合はそのままを返却
            return self;
        }

        // 改行前までを切り出し
        return self.Substring(0, breakIdx);
    }

    /// <summary>
    /// 文字列の最後の行を取得する。
    /// </summary>
    /// <param name="self">対象文字列</param>
    /// <returns>最後の行文字列</returns>
    public static string? LastLine(this string? self)
    {
        if (string.IsNullOrEmpty(self)) return self;

        // 最終改行位置を検索
        var breakIdx = self.LastIndexOfAny(LineBreakChars);
        if (breakIdx < 0)
        {
            // 改行がない場合はそのままを返却
            return self;
        }

        // 最終改行の後ろを返却
        return self.Substring(breakIdx + 1);

    }

    /// <summary>
    /// 文字列を連結する。
    /// </summary>
    /// <param name="self">文字列のシーケンス</param>
    /// <param name="separator">連結する文字間に差し込む文字列</param>
    /// <returns></returns>
    public static string JoinString(this IEnumerable<string?> self, string? separator = null)
    {
        return string.Join(separator, self);
    }

    /// <summary>
    /// 文字列を装飾する。
    /// 元の文字列が null または 空の場合はなにもしない。
    /// </summary>
    /// <param name="self">元になる文字列</param>
    /// <param name="format">文字列を装飾する書式。埋め込み位置0のプレースホルダ({{0}})が含まれる必要がある。</param>
    /// <returns>装飾された文字列。元がnullまたは空の場合はそのまま返却。</returns>
    public static string? Decorate(this string? self, string format)
    {
        if (string.IsNullOrEmpty(self)) return self;
        return string.Format(format, self);
    }

    /// <summary>
    /// 文字列を装飾する。
    /// 元の文字列が null または 空の場合はなにもしない。
    /// </summary>
    /// <param name="self">元になる文字列</param>
    /// <param name="decorator">文字列を装飾するデリゲート</param>
    /// <returns>装飾された文字列。元がnullまたは空の場合はそのまま返却。</returns>
    public static string? Decorate(this string? self, Func<string, string> decorator)
    {
        if (string.IsNullOrEmpty(self)) return self;
        if (decorator == null) return self;
        return decorator(self);
    }

    /// <summary>
    /// 文字列のテキスト要素を列挙する。
    /// </summary>
    /// <param name="self">対象文字列</param>
    /// <returns>テキスト要素シーケンス</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IEnumerable<string> AsTextElements(this string self)
    {
        // パラメータチェック
        if (self == null) throw new ArgumentNullException(nameof(self));

        // テキスト要素を列挙
        var elementer = StringInfo.GetTextElementEnumerator(self);
        while (elementer.MoveNext())
        {
            yield return (string)elementer.Current;
        }
    }

    /// <summary>
    /// 文字列のテキスト要素数を取得する。
    /// </summary>
    /// <param name="self">対象文字列</param>
    /// <returns>テキスト要素数</returns>
    public static int TextElementCount(this string self)
    {
        // パラメータチェック
        if (self == null) throw new ArgumentNullException(nameof(self));

        return StringInfo.ParseCombiningCharacters(self).Length;
    }

    /// <summary>
    /// 文字列の先頭から指定された長さの文字要素を切り出す。
    /// </summary>
    /// <param name="self">元になる文字列。nullまたは空の場合は元のインスタンスをそのまま返却する。</param>
    /// <param name="count">切り出す文字要素の長さ。</param>
    /// <returns>切り出された文字列</returns>
    public static string? CutLeftElements(this string? self, int count)
    {
        // パラメータチェック
        if (count < 0) throw new ArgumentException(nameof(count));
        if (string.IsNullOrEmpty(self)) return self;
        if (count == 0) return string.Empty;

        // 切り出し文字列の構築用
        var builder = new StringBuilder(capacity: count);

        // 文字要素を列挙しながら指定要素数まで蓄積
        var taked = 0;
        var elementer = StringInfo.GetTextElementEnumerator(self);
        while (taked < count && elementer.MoveNext())
        {
            builder.Append(elementer.Current);
            taked++;
        }

        // 切り出した先頭文字列を返却
        return builder.ToString();
    }

    /// <summary>
    /// 文字列の末尾にある指定された長さの文字要素を切り出す。
    /// </summary>
    /// <param name="self">元になる文字列。nullまたは空の場合は元のインスタンスをそのまま返却する。</param>
    /// <param name="count">切り出す文字要素の長さ。</param>
    /// <returns>切り出された文字列</returns>
    public static string? CutRightElements(this string? self, int count)
    {
        // パラメータチェック
        if (count < 0) throw new ArgumentException(nameof(count));
        if (string.IsNullOrEmpty(self)) return self;
        if (count == 0) return string.Empty;

        // 最後の文字を蓄積するバッファ
        var buffer = new Queue<object>(capacity: count);

        // 文字要素を列挙しながら指定数までの最後の要素を蓄積
        var elementer = StringInfo.GetTextElementEnumerator(self);
        while (elementer.MoveNext())
        {
            // 既に切り出し要素数蓄積済みならば1つ除去
            if (count <= buffer.Count)
            {
                buffer.Dequeue();
            }

            // 後方の要素を蓄積
            buffer.Enqueue(elementer.Current);
        }

        // 蓄積された末尾文字列を返却
        return string.Concat(buffer);
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
        if (self == null) throw new ArgumentNullException(nameof(self));
        if (length < 0) throw new ArgumentException(nameof(length));

        // マーカーが指定の長さを超えている場合は矛盾するのでパラメータ指定が正しくない。
        // 省略されずマーカーが使用されない場合もあり得るが、パラメータ length と marker の関係性が正しくないのであれば揺れなく異常検出できようにしている。
        var markerLen = marker?.Length ?? 0;
        if (length < markerLen) throw new ArgumentException();

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
        if (self == null) throw new ArgumentNullException(nameof(self));
        if (count < 0) throw new ArgumentException(nameof(count));

        // マーカーが指定の長さを超えている場合は矛盾するのでパラメータ指定が正しくない。
        // 省略されずマーカーが使用されない場合もあり得るが、パラメータ width と marker の関係性が正しくないのであれば揺れなく異常検出できようにしている。
        var markerCount = marker?.TextElementCount() ?? 0;
        if (count < markerCount) throw new ArgumentException(nameof(marker));

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

    /// <summary>改行キャラクタ配列</summary>
    private static readonly char[] LineBreakChars = new[] { '\r', '\n', };
}
