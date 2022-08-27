using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks.Dataflow;
using ClosedXML.Excel;
using CometFlavor.Reflection;

namespace Lestaly;

/// <summary>
/// IEnumerable{T} に対するデータ処理拡張メソッド
/// </summary>
public static class EnumerableDataExtensions
{
    /// <summary>シーケンス要素を並列に処理する非同期シーケンスに変換する</summary>
    /// <typeparam name="TSource">ソースシーケンスの要素型</typeparam>
    /// <typeparam name="TResult">結果シーケンスの要素型</typeparam>
    /// <param name="self">ソースシーケンス</param>
    /// <param name="transform">要素の処理デリゲート。並列実行される。</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>非同期シーケンス</returns>
    public static IAsyncEnumerable<TResult> ToParallelAsync<TSource, TResult>(this IEnumerable<TSource> self, Func<TSource, TResult> transform, CancellationToken cancelToken = default)
    {
        return self.toParallelAsync(parallels: 4, ordered: false, options => new TransformBlock<TSource, TResult>(transform, options), cancelToken);
    }

    /// <summary>シーケンス要素を並列に処理する非同期シーケンスに変換する</summary>
    /// <typeparam name="TSource">ソースシーケンスの要素型</typeparam>
    /// <typeparam name="TResult">結果シーケンスの要素型</typeparam>
    /// <param name="self">ソースシーケンス</param>
    /// <param name="parallels">並列実行数</param>
    /// <param name="transform">要素の処理デリゲート。並列実行される。</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>非同期シーケンス</returns>
    public static IAsyncEnumerable<TResult> ToParallelAsync<TSource, TResult>(this IEnumerable<TSource> self, int parallels, Func<TSource, TResult> transform, CancellationToken cancelToken = default)
    {
        return self.toParallelAsync(parallels, ordered: false, options => new TransformBlock<TSource, TResult>(transform, options), cancelToken);
    }

    /// <summary>シーケンス要素を並列に処理する非同期シーケンスに変換する</summary>
    /// <typeparam name="TSource">ソースシーケンスの要素型</typeparam>
    /// <typeparam name="TResult">結果シーケンスの要素型</typeparam>
    /// <param name="self">ソースシーケンス</param>
    /// <param name="parallels">並列実行数</param>
    /// <param name="ordered">入力の順序を維持した出力とするか否か</param>
    /// <param name="transform">要素の処理デリゲート。並列実行される。</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>非同期シーケンス</returns>
    public static IAsyncEnumerable<TResult> ToParallelAsync<TSource, TResult>(this IEnumerable<TSource> self, int parallels, bool ordered, Func<TSource, TResult> transform, CancellationToken cancelToken = default)
    {
        return self.toParallelAsync(parallels, ordered, options => new TransformBlock<TSource, TResult>(transform, options), cancelToken);
    }

    /// <summary>シーケンス要素を並列に処理する非同期シーケンスに変換する</summary>
    /// <typeparam name="TSource">ソースシーケンスの要素型</typeparam>
    /// <typeparam name="TResult">結果シーケンスの要素型</typeparam>
    /// <param name="self">ソースシーケンス</param>
    /// <param name="transform">要素の処理デリゲート。並列実行される。</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>非同期シーケンス</returns>
    public static IAsyncEnumerable<TResult> ToParallelAsync<TSource, TResult>(this IEnumerable<TSource> self, Func<TSource, Task<TResult>> transform, CancellationToken cancelToken = default)
    {
        return self.toParallelAsync(parallels: 4, ordered: false, options => new TransformBlock<TSource, TResult>(transform, options), cancelToken);
    }

    /// <summary>シーケンス要素を並列に処理する非同期シーケンスに変換する</summary>
    /// <typeparam name="TSource">ソースシーケンスの要素型</typeparam>
    /// <typeparam name="TResult">結果シーケンスの要素型</typeparam>
    /// <param name="self">ソースシーケンス</param>
    /// <param name="parallels">並列実行数</param>
    /// <param name="transform">要素の処理デリゲート。並列実行される。</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>非同期シーケンス</returns>
    public static IAsyncEnumerable<TResult> ToParallelAsync<TSource, TResult>(this IEnumerable<TSource> self, int parallels, Func<TSource, Task<TResult>> transform, CancellationToken cancelToken = default)
    {
        return self.toParallelAsync(parallels, ordered: false, options => new TransformBlock<TSource, TResult>(transform, options), cancelToken);
    }

    /// <summary>シーケンス要素を並列に処理する非同期シーケンスに変換する</summary>
    /// <typeparam name="TSource">ソースシーケンスの要素型</typeparam>
    /// <typeparam name="TResult">結果シーケンスの要素型</typeparam>
    /// <param name="self">ソースシーケンス</param>
    /// <param name="parallels">並列実行数</param>
    /// <param name="ordered">入力の順序を維持した出力とするか否か</param>
    /// <param name="transform">要素の処理デリゲート。並列実行される。</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>非同期シーケンス</returns>
    public static IAsyncEnumerable<TResult> ToParallelAsync<TSource, TResult>(this IEnumerable<TSource> self, int parallels, bool ordered, Func<TSource, Task<TResult>> transform, CancellationToken cancelToken = default)
    {
        return self.toParallelAsync(parallels, ordered, options => new TransformBlock<TSource, TResult>(transform, options), cancelToken);
    }

    /// <summary>シーケンスデータをCSV(Charactor Separated Field)として保存する。</summary>
    /// <typeparam name="TSource">シーケンスの要素型</typeparam>
    /// <param name="self">保存対象シーケンス</param>
    /// <param name="filePath">保存ファイルパス</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>書き込み処理タスク</returns>
    public static Task SaveToCsvAsync<TSource>(this IEnumerable<TSource> self, string filePath, CancellationToken cancelToken = default)
    {
        return self.SaveToCsvAsync(filePath, new SaveToCsvOptions(), Encoding.UTF8, cancelToken);
    }

    /// <summary>シーケンスデータをCSV(Charactor Separated Field)として保存する。</summary>
    /// <typeparam name="TSource">シーケンスの要素型</typeparam>
    /// <param name="self">保存対象シーケンス</param>
    /// <param name="filePath">保存ファイルパス</param>
    /// <param name="options">保存オプション</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>書き込み処理タスク</returns>
    public static Task SaveToCsvAsync<TSource>(this IEnumerable<TSource> self, string filePath, SaveToCsvOptions options, CancellationToken cancelToken = default)
    {
        return self.SaveToCsvAsync(filePath, options, Encoding.UTF8, cancelToken);
    }

    /// <summary>シーケンスデータをCSV(Charactor Separated Field)として保存する。</summary>
    /// <typeparam name="TSource">シーケンスの要素型</typeparam>
    /// <param name="self">保存対象シーケンス</param>
    /// <param name="filePath">保存ファイルパス</param>
    /// <param name="options">保存オプション</param>
    /// <param name="encoding">保存テキストエンコーディング</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>書き込み処理タスク</returns>
    public static Task SaveToCsvAsync<TSource>(this IEnumerable<TSource> self, string filePath, SaveToCsvOptions options, Encoding encoding, CancellationToken cancelToken = default)
    {
        // パラメータチェック
        if (self == null) throw new ArgumentNullException(nameof(self));
        if (filePath == null) throw new ArgumentNullException(nameof(filePath));
        if (options == null) throw new ArgumentNullException(nameof(options));
        if (encoding == null) throw new ArgumentNullException(nameof(encoding));
        if (options.Separator == options.Quote) throw new NotSupportedException("Invalid quotation");

        // 出力対象カラムを収集
        var columns = collectTypeColumns<TSource>(options);

        // 出力対象カラムが無ければエラーとする。
        if (columns.Length <= 0) throw new NotSupportedException("No members");

        // 行出力テキストの生成ローカル関数
        var buffer = new StringBuilder();
        string makeLine(IEnumerable<string?> fields)
        {
            buffer!.Clear();

            // 各フィールドを
            var col = 0;
            foreach (var field in fields)
            {
                // フィールド区切り追加
                if (col++ != 0) buffer.Append(options.Separator);

                // フィールドが空の場合は次へ
                if (field == null)
                {
                    continue;
                }

                // フィールドがセパレータキャラクタを含むかを判定
                var pos = field.IndexOf(options.Separator);
                if (pos < 0)
                {
                    // 含まなければそのままをフィールドとして追加
                    buffer.Append(field);
                    continue;
                }

                // セパレータを含む場合はクォートする
                buffer.Append(options.Quote);
                foreach (var c in field)
                {
                    // クォートキャラクタ自信を含む場合は2個並べてエスケープする
                    if (c == options.Quote) buffer.Append(options.Quote);
                    buffer.Append(c);
                }
                buffer.Append(options.Quote);
            }

            return buffer.ToString();
        }

        // シーケンスを出力テキスト行に変換するシーケンス
        var lines = self
            .Select(i => makeLine(columns.Select(c => c.Getter(i)?.ToString())))
            .Prepend(makeLine(columns.Select(c => c.Caption)));

        // 保存
        return File.WriteAllLinesAsync(filePath, lines, encoding, cancelToken);
    }

    /// <summary>シーケンスデータをCSV(Charactor Separated Field)として保存する。</summary>
    /// <typeparam name="TSource">シーケンスの要素型</typeparam>
    /// <param name="self">保存対象シーケンス</param>
    /// <param name="filePath">保存ファイルパス</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>書き込み処理タスク</returns>
    public static Task SaveToCsvAsync<TSource>(this IAsyncEnumerable<TSource> self, string filePath, CancellationToken cancelToken = default)
    {
        return self.SaveToCsvAsync(filePath, new SaveToCsvOptions(), Encoding.UTF8, cancelToken);
    }

    /// <summary>シーケンスデータをCSV(Charactor Separated Field)として保存する。</summary>
    /// <typeparam name="TSource">シーケンスの要素型</typeparam>
    /// <param name="self">保存対象シーケンス</param>
    /// <param name="filePath">保存ファイルパス</param>
    /// <param name="options">保存オプション</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>書き込み処理タスク</returns>
    public static Task SaveToCsvAsync<TSource>(this IAsyncEnumerable<TSource> self, string filePath, SaveToCsvOptions options, CancellationToken cancelToken = default)
    {
        return self.SaveToCsvAsync(filePath, options, Encoding.UTF8, cancelToken);
    }

    /// <summary>シーケンスデータをCSV(Charactor Separated Field)として保存する。</summary>
    /// <typeparam name="TSource">シーケンスの要素型</typeparam>
    /// <param name="self">保存対象シーケンス</param>
    /// <param name="filePath">保存ファイルパス</param>
    /// <param name="options">保存オプション</param>
    /// <param name="encoding">保存テキストエンコーディング</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>書き込み処理タスク</returns>
    public static async Task SaveToCsvAsync<TSource>(this IAsyncEnumerable<TSource> self, string filePath, SaveToCsvOptions options, Encoding encoding, CancellationToken cancelToken = default)
    {
        // パラメータチェック
        if (self == null) throw new ArgumentNullException(nameof(self));
        if (filePath == null) throw new ArgumentNullException(nameof(filePath));
        if (options == null) throw new ArgumentNullException(nameof(options));
        if (encoding == null) throw new ArgumentNullException(nameof(encoding));
        if (options.Separator == options.Quote) throw new NotSupportedException("Invalid quotation");

        // 出力対象カラムを収集
        var columns = collectTypeColumns<TSource>(options);

        // 出力対象カラムが無ければエラーとする。
        if (columns.Length <= 0) throw new NotSupportedException("No members");

        // 行出力テキストの生成ローカル関数
        var buffer = new StringBuilder();
        string makeLine(IEnumerable<string?> fields)
        {
            buffer!.Clear();

            // 各フィールドを
            var col = 0;
            foreach (var field in fields)
            {
                // フィールド区切り追加
                if (col++ != 0) buffer.Append(options.Separator);

                // フィールドが空の場合は次へ
                if (field == null)
                {
                    continue;
                }

                // フィールドがセパレータキャラクタを含むかを判定
                var pos = field.IndexOf(options.Separator);
                if (pos < 0)
                {
                    // 含まなければそのままをフィールドとして追加
                    buffer.Append(field);
                    continue;
                }

                // セパレータを含む場合はクォートする
                buffer.Append(options.Quote);
                foreach (var c in field)
                {
                    // クォートキャラクタ自信を含む場合は2個並べてエスケープする
                    if (c == options.Quote) buffer.Append(options.Quote);
                    buffer.Append(c);
                }
                buffer.Append(options.Quote);
            }

            return buffer.ToString();
        }

        // 出力ストリームを作成
        using var writer = new StreamWriter(filePath, append: false, encoding);

        // ヘッダを出力
        await writer.WriteLineAsync(makeLine(columns.Select(c => c.Caption))).ConfigureAwait(false);

        // シーケンス要素を出力
        await foreach (var elem in self.ConfigureAwait(false))
        {
            var line = makeLine(columns.Select(c => c.Getter(elem)?.ToString()));
            await writer.WriteLineAsync(line).ConfigureAwait(false);
        }
    }

    /// <summary>シーケンスデータをExcelファイルとして保存する。</summary>
    /// <typeparam name="TSource">シーケンスの要素型</typeparam>
    /// <param name="self">保存対象シーケンス</param>
    /// <param name="filePath">保存ファイルパス</param>
    /// <returns>書き込み処理タスク</returns>
    public static void SaveToExcel<TSource>(this IEnumerable<TSource> self, string filePath)
    {
        self.SaveToExcel(filePath, new SaveToExcelOptions());
    }

    /// <summary>シーケンスデータをExcelファイルとして保存する。</summary>
    /// <typeparam name="TSource">シーケンスの要素型</typeparam>
    /// <param name="self">保存対象シーケンス</param>
    /// <param name="filePath">保存ファイルパス</param>
    /// <param name="options">保存オプション</param>
    /// <returns>書き込み処理タスク</returns>
    public static void SaveToExcel<TSource>(this IEnumerable<TSource> self, string filePath, SaveToExcelOptions options)
    {
        // パラメータチェック
        if (self == null) throw new ArgumentNullException(nameof(self));
        if (filePath == null) throw new ArgumentNullException(nameof(filePath));
        if (options == null) throw new ArgumentNullException(nameof(options));

        if (options.RowOffset < 0) throw new ArgumentOutOfRangeException(nameof(options.RowOffset));
        if (options.ColumnOffset < 0) throw new ArgumentOutOfRangeException(nameof(options.ColumnOffset));

        // 出力対象カラムを収集
        var columns = collectTypeColumns<TSource>(options);

        // 出力対象カラムが無ければエラーとする。
        if (columns.Length <= 0) throw new NotSupportedException("No members");

        // 値の出力デリゲートを作成
        var exporters = columns
            .Select(column => new { column, writer = makeCellWriter(column, options), })
            .ToArray();

        // 出力ブック作成
        var book = new XLWorkbook();
        var sheet = book.AddWorksheet(options.Sheet.WhenWhite("Sheet1"));

        // 出力の基準セル取得
        var baseCell = sheet.Cell(1 + options.RowOffset, 1 + options.ColumnOffset);

        // ヘッダ出力
        var headerCell = baseCell;
        for (var i = 0; i < exporters.Length; i++)
        {
            headerCell.CellRight(i).SetValue<string>(exporters[i].column.Caption);
        }

        // データ出力
        var dataCell = baseCell;
        foreach (var data in self)
        {
            dataCell = dataCell.CellBelow();
            for (var i = 0; i < exporters.Length; i++)
            {
                var value = exporters[i].column.Getter(data);
                if (value != null)
                {
                    var cell = dataCell.CellRight(i);
                    var writer = exporters[i].writer;
                    writer(cell, value);
                }
            }
        }

        // 出力した範囲取得
        var dataRange = sheet.Range(baseCell, dataCell.CellRight(exporters.Length - 1));

        // テーブルまたはフィルタの設定
        if (options.TableDefine)
        {
            dataRange.CreateTable(options.TableName.WhenWhite("Table1"));
        }
        else if (options.AutoFilter)
        {
            dataRange.SetAutoFilter();
        }

        // カラムサイズ調整
        if (options.AdjustToContents)
        {
            foreach (var column in dataRange.Columns())
            {
                column.WorksheetColumn().AdjustToContents();
            }
        }

        // ファイルに保存
        book.SaveAs(filePath);
    }

    /// <summary>シーケンスデータをCSV(Charactor Separated Field)として保存する。</summary>
    /// <typeparam name="TSource">シーケンスの要素型</typeparam>
    /// <param name="self">保存対象シーケンス</param>
    /// <param name="filePath">保存ファイルパス</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>書き込み処理タスク</returns>
    public static Task SaveToExcelAsync<TSource>(this IAsyncEnumerable<TSource> self, string filePath, CancellationToken cancelToken = default)
    {
        return self.SaveToExcelAsync(filePath, new SaveToExcelOptions(), cancelToken);
    }

    /// <summary>シーケンスデータをExcelファイルとして保存する。</summary>
    /// <typeparam name="TSource">シーケンスの要素型</typeparam>
    /// <param name="self">保存対象シーケンス</param>
    /// <param name="filePath">保存ファイルパス</param>
    /// <param name="options">保存オプション</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>書き込み処理タスク</returns>
    public static async Task SaveToExcelAsync<TSource>(this IAsyncEnumerable<TSource> self, string filePath, SaveToExcelOptions options, CancellationToken cancelToken = default)
    {
        // パラメータチェック
        if (self == null) throw new ArgumentNullException(nameof(self));
        if (filePath == null) throw new ArgumentNullException(nameof(filePath));
        if (options == null) throw new ArgumentNullException(nameof(options));

        if (options.RowOffset < 0) throw new ArgumentOutOfRangeException(nameof(options.RowOffset));
        if (options.ColumnOffset < 0) throw new ArgumentOutOfRangeException(nameof(options.ColumnOffset));

        // 出力対象カラムを収集
        var columns = collectTypeColumns<TSource>(options);

        // 出力対象カラムが無ければエラーとする。
        if (columns.Length <= 0) throw new NotSupportedException("No members");

        // 値の出力デリゲートを作成
        var exporters = columns
            .Select(column => new { column, writer = makeCellWriter(column, options), })
            .ToArray();

        // 出力ブック作成
        var book = new XLWorkbook();
        var sheet = book.AddWorksheet(options.Sheet.WhenWhite("Sheet1"));

        // 出力の基準セル取得
        var baseCell = sheet.Cell(1 + options.RowOffset, 1 + options.ColumnOffset);

        // ヘッダ出力
        var headerCell = baseCell;
        for (var i = 0; i < exporters.Length; i++)
        {
            headerCell.CellRight(i).SetValue<string>(exporters[i].column.Caption);
        }

        // データ出力
        var dataCell = baseCell;
        await foreach (var data in self.ConfigureAwait(false))
        {
            dataCell = dataCell.CellBelow();
            for (var i = 0; i < exporters.Length; i++)
            {
                var value = exporters[i].column.Getter(data);
                if (value != null)
                {
                    var cell = dataCell.CellRight(i);
                    var writer = exporters[i].writer;
                    writer(cell, value);
                }
            }
        }

        // 出力した範囲取得
        var dataRange = sheet.Range(baseCell, dataCell.CellRight(exporters.Length - 1));

        // テーブルまたはフィルタの設定
        if (options.TableDefine)
        {
            dataRange.CreateTable(options.TableName.WhenWhite("Table1"));
        }
        else if (options.AutoFilter)
        {
            dataRange.SetAutoFilter();
        }

        // カラムサイズ調整
        if (options.AdjustToContents)
        {
            foreach (var column in dataRange.Columns())
            {
                column.WorksheetColumn().AdjustToContents();
            }
        }

        // ファイルに保存
        book.SaveAs(filePath);
    }

    /// <summary>Excelファイル保存用のセルにカラム値を書き込むデリゲートを作成する</summary>
    /// <typeparam name="TSource">カラムの所属型</typeparam>
    /// <param name="column">カラム情報</param>
    /// <param name="options">保存オプション</param>
    /// <returns>セル書き込みデリゲート</returns>
    private static Action<IXLCell, object> makeCellWriter<TSource>(TypeColumn<TSource> column, SaveToExcelOptions options)
    {
        if (options.AutoLink)
        {
            switch (column.MemberType)
            {
            case var t when t.IsAssignableTo(typeof(Uri)):
                return (cell, value) =>
                {
                    cell.SetHyperlink(new XLHyperlink((Uri)value));
                    cell.Value = value;
                };
            case var t when t.IsAssignableTo(typeof(FileSystemInfo)):
                return (cell, value) =>
                {
                    var info = (FileSystemInfo)value;
                    cell.SetHyperlink(new XLHyperlink(info.FullName));
                    cell.Value = info.FullName;
                };
            default:
                break;
            }
        }

        switch (column.MemberType)
        {
        case var t when t.IsAssignableTo(typeof(ExcelHyperlink)):
            return (cell, value) =>
            {
                var link = (ExcelHyperlink)value;
                cell.SetHyperlink(new XLHyperlink(link.Target, link.Tooltip));
                cell.Value = link.Display ?? link.Target;
            };
        case var t when t.IsAssignableTo(typeof(ExcelFormula)):
            return (cell, value) =>
            {
                var fomula = (ExcelFormula)value;
                if (fomula.IsR1C1) cell.FormulaR1C1 = fomula.Expression;
                else cell.FormulaA1 = fomula.Expression;
            };
        default:
            return (cell, value) => cell.Value = value;
        }
    }

    /// <summary>型のメンバをカラムとして取り扱うための情報を収集する</summary>
    /// <typeparam name="TSource">対象の型</typeparam>
    /// <param name="options">型のカラム収集設定</param>
    /// <returns>型カラム情報</returns>
    private static TypeColumn<TSource>[] collectTypeColumns<TSource>(TypeColumnOptions options)
    {
        // シーケンス要素型の出力メンバ情報を収集
        var flags = BindingFlags.Instance | BindingFlags.Public;
        var kinds = MemberTypes.Property | (options.IncludeFields ? MemberTypes.Field : 0);
        var columns = typeof(TSource).FindMembers(kinds, flags, (m, c) => options.MemberFilter?.Invoke(m) ?? true, null)
            .Select(member =>
            {
                // メンバのゲッターを作成
                // フィールドはリフレクションによって、プロパティは取得用デリゲート生成をして。
                var getter = default(Func<TSource, object?>);
                var returnType = default(Type);
                if (member is FieldInfo fieldInfo)
                {
                    getter = o => fieldInfo.GetValue(o);
                    returnType = fieldInfo.FieldType;
                }
                else
                {
                    var propInfo = (PropertyInfo)member;
                    getter = MemberAccessor.CreatePropertyGetter<TSource>(propInfo, nonPublic: false);
                    returnType = propInfo.PropertyType;
                }

                // Display属性の利用が指定されていれば、その情報を取得する。
                var caption = default(string);
                var order = default(int);
                if (options.UseDisplayAttribute)
                {
                    var display = member.GetCustomAttribute<DisplayAttribute>();
                    caption = display?.Name;
                    order = display?.GetOrder() ?? 0;
                }

                // カラム名を決定する。
                // 上記の属性からの情報があればそれを優先し、
                // それがなければキャプション選択デリゲートから、それもなければメンバ名をカラム名とする。
                caption ??= options.CaptionSelector?.Invoke(member) ?? member.Name;

                return new TypeColumn<TSource>(member, returnType, getter, caption, order);
            })
            .OrderBy(c => options.UseDisplayAttribute ? c.Order : 0)    // 属性利用時はその Order プロパティを最優先。
            .ThenBy(c => options.SortCaption ? c.Caption : "")          // キャンプションソートが指定されていればそのソート
            .ThenBy(c => options.SortMemberName ? c.Member.Name : "")   // メンバ名ソートが指定されていればそのソート。両方指定されていたらキャプションよりも後(同一キャプションの場合の順序)
            .ToArray();

        return columns;
    }

    /// <summary>シーケンス要素を並列に処理する非同期シーケンスに変換する</summary>
    /// <typeparam name="TSource">ソースシーケンスの要素型</typeparam>
    /// <typeparam name="TResult">結果シーケンスの要素型</typeparam>
    /// <param name="self">ソースシーケンス</param>
    /// <param name="parallels">並列実行数</param>
    /// <param name="ordered">入力の順序を維持した出力とするか否か</param>
    /// <param name="generator">ブロックインスタンス生成デリゲート</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>非同期シーケンス</returns>
    private static async IAsyncEnumerable<TResult> toParallelAsync<TSource, TResult>(this IEnumerable<TSource> self, int parallels, bool ordered, Func<ExecutionDataflowBlockOptions, TransformBlock<TSource, TResult>> generator, [EnumeratorCancellation] CancellationToken cancelToken = default)
    {
        // データブロックのオプション
        var options = new ExecutionDataflowBlockOptions();
        options.MaxDegreeOfParallelism = parallels;
        options.BoundedCapacity = parallels;
        options.EnsureOrdered = ordered;
        options.CancellationToken = cancelToken;

        // 変換ブロック作成
        var block = generator(options);

        // 変換ブロックにデータを投入するタスク
        var sender = Task.Run(async () =>
        {
            try
            {
                foreach (var item in self)
                {
                    await block.SendAsync(item, cancelToken);
                }
            }
            finally
            {
                block.Complete();
            }
        });

        // データブロックの出力を取り出して列挙
        while (await block.OutputAvailableAsync(cancelToken).ConfigureAwait(false))
        {
            yield return await block.ReceiveAsync(cancelToken).ConfigureAwait(false);
        }

        // 投入タスクとブロックの終了を待機
        // ブロックは上記で
        await Task.WhenAll(sender, block.Completion);
    }

    /// <summary>型カラム情報</summary>
    /// <typeparam name="TSource">対象の型</typeparam>
    /// <param name="Member">メンバ情報</param>
    /// <param name="MemberType">メンバの取得値型</param>
    /// <param name="Getter">メンバからの値取得デリゲート</param>
    /// <param name="Caption">カラムキャプション</param>
    /// <param name="Order">カラムの順序</param>
    private record TypeColumn<TSource>(MemberInfo Member, Type MemberType, Func<TSource, object?> Getter, string Caption, int Order);
}
