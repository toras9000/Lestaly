using System.Buffers;
using System.Collections;
using System.Text;

namespace Lestaly;

/// <summary>フィールドの整列種別</summary>
public enum FixedFieldAlign
{
    /// <summary>左寄せ</summary>
    Left,
    /// <summary>右寄せ</summary>
    Right,
    /// <summary>中央寄せ</summary>
    Center,
}

/// <summary>固定フィールド文字列のカラム情報</summary>
/// <param name="Caption">キャプション文字列</param>
/// <param name="Width">カラム幅</param>
/// <param name="Align">整列種別</param>
public record FixedFieldColumn(string? Caption, int Width, FixedFieldAlign Align);

/// <summary>固定フィールドテキスト生成オブジェクト</summary>
public interface IFixedFields : IEnumerable<string>
{
    /// <summary>カラム情報</summary>
    public IReadOnlyList<FixedFieldColumn> Columns { get; }

    /// <summary>全カラム幅</summary>
    public int FullWidth { get; }

    /// <summary>キャプション行文字列を作る</summary>
    /// <param name="align"></param>
    /// <returns>キャプション行文字列</returns>
    public string CaptionLine(FixedFieldAlign? align = default);
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
    /// <summary>データソースと幅算出処理を指定するコンストラクタ</summary>
    /// <param name="source">文字列構築のデータソース</param>
    /// <param name="widthEvaluater">
    /// 文字列幅評価処理。省略時は単に文字列の Length を利用する。
    /// 幅はフィールドのパディングのために利用され、パディングの空白キャラクタ(' ')を 1 とする系の値となる。
    /// </param>
    public FixedFieldBuilder(IReadOnlyCollection<T> source, Func<string, int>? widthEvaluater = null)
    {
        this.Source = source;
        this.colmnDefines = new();
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
    public FixedFieldBuilder<T> AddColumn(Func<T, string> formatter, FixedFieldAlign align = FixedFieldAlign.Left)
    {
        this.colmnDefines.Add(new(null, formatter, align));
        return this;
    }

    /// <summary>フィールド定義を追加する</summary>
    /// <param name="caption">カラムキャプション</param>
    /// <param name="formatter">フィールド文字列を作るデリゲート</param>
    /// <param name="align">カラム内の整列種別</param>
    /// <returns>インスタンス自身</returns>
    public FixedFieldBuilder<T> AddColumn(string caption, Func<T, string> formatter, FixedFieldAlign align = FixedFieldAlign.Left)
    {
        this.colmnDefines.Add(new(caption, formatter, align));
        return this;
    }
    #endregion

    #region 構築
    /// <summary>データソースから固定フィールド文字列を生成するオブジェクトを構築する</summary>
    /// <param name="useCache">
    /// フィールド幅算出時の結果を保持して文字列処理を行うか否か。
    /// 原理上、幅の算出と固定幅の生成で必ずデータソースを2周分列挙する必要があるが、
    /// このパラメータを true にした場合は幅算出時のフィールド文字列化結果をすべて保持して列挙文字列の構築に利用する。
    /// このパラメータが false の場合、必要な最大幅の算出後に再度フィールド文字列の生成と幅の算出処理を実行する。
    /// メモリ使用量と処理量のトレードオフとなる。
    /// </param>
    /// <returns>固定フィールド文字列生成オブジェクト</returns>
    public IFixedFields Build(bool useCache = false)
    {
        // 全データソースを舐めて、定義された各フィールドの最大幅を算出する。
        // キャッシュ有効時には算出に利用したデータをキャッシュする。
        var columnWidth = new int[this.colmnDefines.Count];
        var fieldsCache = useCache ? new List<FieldCache[]>() : default;
        var recordCache = new FieldCache[columnWidth.Length];
        foreach (var record in this.Source)
        {
            // 各カラムの幅評価
            for (var i = 0; i < columnWidth.Length; i++)
            {
                var define = this.colmnDefines[i];
                var maxWidth = columnWidth[i];

                // フィールドテキストを取得して幅算出
                var fieldText = define.Formatter(record);
                var fieldWidth = this.widthEvaluater(fieldText);
                var captionWidth = this.widthEvaluater(define.Caption ?? "");

                // 算出結果を保持
                columnWidth[i] = Math.Max(maxWidth, Math.Max(fieldWidth, captionWidth));
                recordCache[i] = new(fieldText, fieldWidth);
            }

            // キャッシュ有効時は算出結果をコピーして保持
            fieldsCache?.Add(recordCache.ToArray());
        }

        // 上記算出結果からカラム情報を作成
        var columns = new FixedFieldColumn[this.colmnDefines.Count];
        for (var i = 0; i < columnWidth.Length; i++)
        {
            var define = this.colmnDefines[i];
            var maxWidth = columnWidth[i];
            columns[i] = new(define.Caption, maxWidth, define.Align);
        }

        // 固定フィールド文字列生成オブジェクトを返却
        return new FixedFields<T>(this, columns, fieldsCache);
    }
    #endregion

    // 非公開型
    #region 内部データ型
    /// <summary>カラム定義</summary>
    /// <param name="Caption">カラムキャプション</param>
    /// <param name="Formatter">フィールドの文字列化デリゲート</param>
    /// <param name="Align">カラム内の整列種別</param>
    private record struct ColumnDefine(string? Caption, Func<T, string> Formatter, FixedFieldAlign Align);

    /// <summary>フィールドキャッシュ</summary>
    /// <param name="Text">フィールド文字列</param>
    /// <param name="Width">フィールド幅</param>
    private record struct FieldCache(string Text, int Width);
    #endregion

    #region データ構築型
    /// <summary>固定フィールドテキスト生成オブジェクト実装</summary>
    /// <typeparam name="U">データソース要素型</typeparam>
    private class FixedFields<U> : IFixedFields
    {
        // 構築
        #region コンストラクタ
        /// <summary>固定フィールド文字列生成用の情報を指定するコンストラクタ</summary>
        /// <param name="outer">アウターインスタンス</param>
        /// <param name="columns">カラム情報</param>
        /// <param name="cache">フィールドキャッシュ</param>
        public FixedFields(FixedFieldBuilder<U> outer, IReadOnlyList<FixedFieldColumn> columns, List<FieldCache[]>? cache)
        {
            this.outer = outer;
            this.cache = cache;
            this.Columns = columns;
            this.FullWidth = columns.Sum(c => c.Width);
        }
        #endregion

        // 公開プロパティ
        #region 固定フィールド情報
        /// <summary>カラム情報</summary>
        public IReadOnlyList<FixedFieldColumn> Columns { get; }

        /// <summary>全カラム幅</summary>
        public int FullWidth { get; }
        #endregion

        #region 文字列構築
        /// <inheritdoc />
        public string CaptionLine(FixedFieldAlign? align = default)
        {
            return string.Create(this.FullWidth, 0, (span, _) =>
            {
                var offset = span;
                for (var i = 0; i < this.Columns.Count; i++)
                {
                    var column = this.Columns[i];

                    // フィールド幅を算出。カラム幅に対してパディング量を求め、ソーステキストと併せた合計フィールド幅を求める。
                    var captionText = column.Caption ?? "";
                    var textWidth = this.outer.widthEvaluater(captionText);
                    var padWidth = Math.Max(column.Width - textWidth, 0);
                    var fieldWidth = captionText.Length + padWidth;

                    // バッファにキャプションを書き込み
                    writeField(offset[..fieldWidth], captionText, align ?? column.Align);
                    offset = offset[fieldWidth..];
                }
            });
        }

        /// <inheritdoc />
        public IEnumerator<string> GetEnumerator()
        {
            // データソースを文字列化する。キャッシュ利用有無により処理分け
            var lineBuilder = new ArrayBufferWriter<char>();
            if (this.cache == null)
            {
                // キャッシュを利用しない場合、フィールド文字列を再度作成してレコード文字列を作る
                foreach (var record in this.outer.Source)
                {
                    // バッファ書き込み量リセット
                    lineBuilder.ResetWrittenCount();

                    // 行テキストを構築
                    for (var i = 0; i < this.Columns.Count; i++)
                    {
                        var column = this.Columns[i];
                        var define = this.outer.colmnDefines[i];

                        // フィールド文字列を構築
                        var fieldText = define.Formatter(record);

                        // フィールド幅を算出。カラム幅に対してパディング量を求め、ソーステキストと併せた合計フィールド幅を求める。
                        var textWidth = this.outer.widthEvaluater(fieldText);
                        var padWidth = Math.Max(column.Width - textWidth, 0);
                        var fieldWidth = fieldText.Length + padWidth;

                        // バッファにフィールドを書き込み
                        var fieldSpan = lineBuilder.GetSpan(fieldWidth)[..fieldWidth];
                        writeField(fieldSpan, fieldText, define.Align);
                        lineBuilder.Advance(fieldWidth);
                    }

                    // 構築した行テキストを返却
                    yield return new string(lineBuilder.WrittenSpan);
                }
            }
            else
            {
                // キャッシュを利用する場合、キャッシュしておいた文字列からレコード文字列を作る
                foreach (var fields in cache)
                {
                    // バッファ書き込み量リセット
                    lineBuilder.ResetWrittenCount();

                    // 行テキストを構築
                    for (var i = 0; i < this.Columns.Count; i++)
                    {
                        var column = this.Columns[i];
                        var define = this.outer.colmnDefines[i];
                        ref readonly var cache = ref fields[i];

                        // フィールド幅を算出。カラム幅に対してパディング量を求め、ソーステキストと併せた合計フィールド幅を求める。
                        var padWidth = column.Width - cache.Width;
                        var fieldWidth = cache.Text.Length + padWidth;

                        // フィールド文字列を構築
                        var fieldSpan = lineBuilder.GetSpan(fieldWidth)[..fieldWidth];
                        writeField(fieldSpan, cache.Text, define.Align);
                        lineBuilder.Advance(fieldWidth);
                    }

                    // 構築した行テキストを返却
                    yield return new string(lineBuilder.WrittenSpan);
                }
            }
        }

        /// <inheritdoc />
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => this.GetEnumerator();
        #endregion

        // 非公開フィールド
        #region 文字列生成用の情報
        /// <summary>アウターインスタンス</summary>
        private readonly FixedFieldBuilder<U> outer;

        /// <summary>フィールドキャッシュ</summary>
        private readonly IEnumerable<FieldCache[]>? cache;
        #endregion

        // 非公開フィールド
        #region 文字列生成
        /// <summary>フィールド文字列をバッファに書き込む</summary>
        /// <param name="buffer">バッファ</param>
        /// <param name="source">ソース文字列</param>
        /// <param name="align">フィールド内配置</param>
        private void writeField(Span<char> buffer, ReadOnlySpan<char> source, FixedFieldAlign align)
        {
            // ソース文字列が長い場合は入る分だけを書き込み
            if (buffer.Length <= source.Length)
            {
                source.Cap(buffer.Length).CopyTo(buffer);
                return;
            }

            // 配置方法に応じてパディングを追加した書き込み
            if (align == FixedFieldAlign.Right)
            {
                var padLen = buffer.Length - source.Length;
                buffer[..padLen].Fill(' ');
                source.CopyTo(buffer[padLen..]);
            }
            else if (align == FixedFieldAlign.Center)
            {
                buffer.Fill(' ');
                var padLen = (buffer.Length - source.Length) / 2;
                source.CopyTo(buffer[padLen..]);
            }
            else
            {
                source.CopyTo(buffer);
                buffer[source.Length..].Fill(' ');
            }
        }
        #endregion
    }
    #endregion

    // 非公開型
    #region 内部データ型
    /// <summary>カラム定義リスト</summary>
    private readonly List<ColumnDefine> colmnDefines;

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
