using System.Text;

namespace Lestaly;

/// <summary>フィールドの整列種別</summary>
public enum FixedFieldAlign
{
    /// <summary>左寄せ</summary>
    Left,
    /// <summary>右寄せ</summary>
    Right,
}

/// <summary>固定フィールド文字列を構築する</summary>
/// <typeparam name="T">文字列化ソースデータ型</typeparam>
/// <remarks>
/// 特定のデータソースに対して、その要素型を元にした固定幅カラムの定義を行い、データを文字列化するためのクラス。
/// ソースデータの全要素に対し、カラム定義に沿って文字列化したフィールド文字列の最大幅を求め、
/// 幅の狭い文字列には不足分を空白(' ')キャラクタで埋めて固定幅カラムとして文字列化する。
/// カラム間は詰められるため、余白や区切り文字が必要であればそれもフィールド定義として追加する。
/// </remarks>
public class FixedFieldBuilder<T>
{
    // 構築
    #region コンストラクタ
    /// <summary></summary>
    /// <param name="source">文字列構築のデータソース</param>
    /// <param name="widthEvaluater">
    /// 文字列幅評価処理。省略時は単に文字列の Length を利用する。
    /// 幅はフィールドのパディングのために利用され、パディングの空白キャラクタ(' ')を 1 とする値の系となる。
    /// </param>
    public FixedFieldBuilder(IReadOnlyCollection<T> source, Func<string, int>? widthEvaluater = null)
    {
        this.Source = source;
        this.fieldDefines = new();
        this.widthEvaluater = widthEvaluater ?? (t => t.Length);
    }
    #endregion

    // 公開プロパティ
    #region データ
    /// <summary>文字列構築のソースデータ</summary>
    public IReadOnlyCollection<T> Source { get; }
    #endregion

    // 公開メソッド
    #region 定義
    /// <summary>フィールド定義を追加する</summary>
    /// <param name="formatter">フィールド文字列を作るデリゲート</param>
    /// <param name="align">カラム内の整列種別</param>
    /// <returns>インスタンス自身</returns>
    public FixedFieldBuilder<T> AddField(Func<T, string> formatter, FixedFieldAlign align = FixedFieldAlign.Left)
    {
        this.fieldDefines.Add(new(formatter, align));
        return this;
    }
    #endregion

    #region 構築
    /// <summary>フィールド定義を元にデータソースから文字列を構築する</summary>
    /// <param name="useCache">
    /// フィールド幅算出時の結果を保持して文字列処理を行うか否か。
    /// 原理上、幅の算出と固定幅の生成で必ずデータソースを2周分列挙する必要があるが、
    /// このパラメータを true にした場合は幅算出時のフィールド文字列化結果をすべて保持して列挙文字列の構築に利用する。
    /// このパラメータが false の場合、必要な最大幅の算出後に再度フィールド文字列の生成と幅の算出処理を実行する。
    /// メモリ使用量と処理量のトレードオフとなる。
    /// </param>
    /// <returns>データソースの各要素を文字列化したシーケンス</returns>
    public IEnumerable<string> BuildLines(bool useCache = false)
    {
        // 定義された各フィールドの最大幅を算出する。
        // キャッシュ有効時には算出に利用したデータをキャッシュする。
        var columnWidth = new int[this.fieldDefines.Count];
        var fieldsCache = useCache ? new List<FieldCache[]>() : default;
        var recordCache = new FieldCache[columnWidth.Length];
        foreach (var record in this.Source)
        {
            for (var i = 0; i < columnWidth.Length; i++)
            {
                var define = this.fieldDefines[i];
                var maxWidth = columnWidth[i];

                var fieldText = define.Formatter(record);
                var fieldWidth = this.widthEvaluater(fieldText);
                columnWidth[i] = Math.Max(maxWidth, fieldWidth);
                recordCache[i] = new(fieldText, fieldWidth);
            }

            fieldsCache?.Add(recordCache.ToArray());
        }

        // データソースを文字列化する。キャッシュ利用有無により処理分け
        var lineBuilder = new StringBuilder();
        if (fieldsCache == null)
        {
            // キャッシュを利用しない場合、フィールド文字列を再度作成してレコード文字列を作る
            foreach (var record in this.Source)
            {
                lineBuilder.Clear();
                for (var i = 0; i < columnWidth.Length; i++)
                {
                    var define = this.fieldDefines[i];
                    var maxWidth = columnWidth[i];

                    var fieldText = define.Formatter(record);
                    var fieldWidth = this.widthEvaluater(fieldText);
                    var padding = Math.Max(maxWidth - fieldWidth, 0);
                    if (define.Align == FixedFieldAlign.Right)
                    {
                        lineBuilder.Append(' ', padding);
                        lineBuilder.Append(fieldText);
                    }
                    else
                    {
                        lineBuilder.Append(fieldText);
                        lineBuilder.Append(' ', padding);
                    }
                }
                yield return lineBuilder.ToString();
            }
        }
        else
        {
            // キャッシュを利用する場合、キャッシュしておいた文字列からレコード文字列を作る
            foreach (var fields in fieldsCache)
            {
                lineBuilder.Clear();
                for (var i = 0; i < columnWidth.Length; i++)
                {
                    var define = this.fieldDefines[i];
                    var maxWidth = columnWidth[i];
                    var cache = fields[i];

                    var padding = maxWidth - cache.Width;
                    if (define.Align == FixedFieldAlign.Right)
                    {
                        lineBuilder.Append(' ', padding);
                        lineBuilder.Append(cache.Text);
                    }
                    else
                    {
                        lineBuilder.Append(cache.Text);
                        lineBuilder.Append(' ', padding);
                    }
                }
                yield return lineBuilder.ToString();
            }
        }
    }
    #endregion

    // 非公開型
    #region 内部データ型
    /// <summary>フィールド定義</summary>
    /// <param name="Formatter">フィールドの文字列化デリゲート</param>
    /// <param name="Align">カラム内の整列種別</param>
    private record struct FieldDefine(Func<T, string> Formatter, FixedFieldAlign Align);

    /// <summary>フィールドキャッシュ</summary>
    /// <param name="Text">フィールド文字列</param>
    /// <param name="Width">フィールド幅</param>
    private record struct FieldCache(string Text, int Width);
    #endregion

    // 非公開型
    #region 内部データ型
    /// <summary>フィールド定義リスト</summary>
    private readonly List<FieldDefine> fieldDefines;

    /// <summary>フィールド文字列幅評価デリゲート</summary>
    private readonly Func<string, int> widthEvaluater;
    #endregion
}

/// <summary>固定フィールド文字列を構築の補助クラス</summary>
public static class FixedFieldBuilder
{
    /// <summary>固定フィールド文字列構築処理インスタンスを生成する</summary>
    /// <typeparam name="T">文字列化ソースデータ型</typeparam>
    /// <param name="source">文字列構築のデータソース</param>
    /// <param name="widthEvaluater">文字列幅評価処理</param>
    /// <returns>固定フィールド文字列構築処理インスタンス</returns>
    public static FixedFieldBuilder<T> Create<T>(IReadOnlyCollection<T> source, Func<string, int>? widthEvaluater = null)
        => new(source, widthEvaluater);
}
