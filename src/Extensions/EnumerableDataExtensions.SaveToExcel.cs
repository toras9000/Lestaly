using System.ComponentModel.DataAnnotations;
using System.Reflection;
using ClosedXML.Excel;
using CometFlavor.Reflection;

namespace Lestaly;

/// <summary>
/// IEnumerable{T} に対するデータ処理拡張メソッド
/// </summary>
public static partial class EnumerableDataExtensions
{
    /// <summary>シーケンスデータをExcelファイルとして保存する。</summary>
    /// <typeparam name="TSource">シーケンスの要素型</typeparam>
    /// <param name="self">保存対象シーケンス</param>
    /// <param name="file">保存ファイル</param>
    /// <returns>書き込み処理タスク</returns>
    public static void SaveToExcel<TSource>(this IEnumerable<TSource> self, FileInfo file)
    {
        if (file == null) throw new ArgumentNullException(nameof(file));
        self.SaveToExcel(file.FullName);
    }

    /// <summary>シーケンスデータをExcelファイルとして保存する。</summary>
    /// <typeparam name="TSource">シーケンスの要素型</typeparam>
    /// <param name="self">保存対象シーケンス</param>
    /// <param name="file">保存ファイル</param>
    /// <param name="options">保存オプション</param>
    /// <returns>書き込み処理タスク</returns>
    public static void SaveToExcel<TSource>(this IEnumerable<TSource> self, FileInfo file, SaveToExcelOptions options)
    {
        if (file == null) throw new ArgumentNullException(nameof(file));
        self.SaveToExcel(file.FullName, options);
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
        self.ToPseudoAsyncEnumerable().SaveToExcelAsync(filePath, options).GetAwaiter().GetResult();
    }

    /// <summary>シーケンスデータをCSV(Charactor Separated Field)として保存する。</summary>
    /// <typeparam name="TSource">シーケンスの要素型</typeparam>
    /// <param name="self">保存対象シーケンス</param>
    /// <param name="file">保存ファイル</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>書き込み処理タスク</returns>
    public static Task SaveToExcelAsync<TSource>(this IAsyncEnumerable<TSource> self, FileInfo file, CancellationToken cancelToken = default)
    {
        if (file == null) throw new ArgumentNullException(nameof(file));
        return self.SaveToExcelAsync(file.FullName, cancelToken);
    }

    /// <summary>シーケンスデータをExcelファイルとして保存する。</summary>
    /// <typeparam name="TSource">シーケンスの要素型</typeparam>
    /// <param name="self">保存対象シーケンス</param>
    /// <param name="file">保存ファイル</param>
    /// <param name="options">保存オプション</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>書き込み処理タスク</returns>
    public static Task SaveToExcelAsync<TSource>(this IAsyncEnumerable<TSource> self, FileInfo file, SaveToExcelOptions options, CancellationToken cancelToken = default)
    {
        if (file == null) throw new ArgumentNullException(nameof(file));
        return self.SaveToExcelAsync(file.FullName, options, cancelToken);
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
        if (options.FontName != null)
        {
            book.Style.Font.FontName = options.FontName;
        }

        // 出力シート作成
        var sheet = book.AddWorksheet(options.Sheet.WhenWhite("Sheet1"));

        // 出力の基準セル取得
        var baseCell = sheet.Cell(1 + options.RowOffset, 1 + options.ColumnOffset);

        // ヘッダ出力
        var headerCell = baseCell;
        var offset = 0;
        for (var i = 0; i < exporters.Length; i++)
        {
            var column = exporters[i].column;
            for (var a = 0; a < column.Span; a++)
            {
                var caption = column.Captions[a];
                headerCell.CellRight(offset).Value = caption;
                offset++;
            }
        }

        // データ出力
        var dataCell = baseCell;
        await foreach (var data in self.ConfigureAwait(false))
        {
            dataCell = dataCell.CellBelow();
            offset = 0;
            for (var i = 0; i < exporters.Length; i++)
            {
                var column = exporters[i].column;
                if (column.Span <= 0) continue;

                var value = column.Getter(data);
                if (value != null && 0 < column.Span)
                {
                    var cell = dataCell.CellRight(offset);
                    var writer = exporters[i].writer;
                    writer(cell, value);
                }
                offset += column.Span;
            }
        }

        if (0 < offset)
        {
            // 出力した範囲取得
            var dataRange = sheet.Range(baseCell, dataCell.CellRight(offset - 1));

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
        if (column.MemberType is var t && t.IsAssignableTo(typeof(ExcelExpand)))
        {
            return (cell, value) =>
            {
                var expand = (ExcelExpand)value;
                if (expand.Values == null) return;
                var count = 0;
                foreach (var item in expand.Values)
                {
                    if (column.Span <= count)
                    {
                        if (!options.DropSpanOver) throw new InvalidDataException($"'{column.Member.Name}' has exceeded the maximum span.");
                        break;
                    }
                    var putCell = cell.CellRight(count);
                    if (expand.DynamicValue && (item is ExcelHyperlink || item is ExcelFormula || item is ExcelStyle))
                    {
                        var writer = makeCellWriter(item.GetType(), options);
                        writer(putCell, item);
                    }
                    else
                    {
                        writeCellValue(putCell, item);
                    }
                    count++;
                }
            };
        }

        return makeCellWriter(column.MemberType, options);
    }

    /// <summary>Excelファイル保存用のセルにカラム値を書き込むデリゲートを作成する</summary>
    /// <param name="type">書き込み対象の型情報</param>
    /// <param name="options">保存オプション</param>
    /// <returns>セル書き込みデリゲート</returns>
    private static Action<IXLCell, object> makeCellWriter(Type type, SaveToExcelOptions options)
    {
        if (options.AutoLink)
        {
            switch (type)
            {
            case var t when t.IsAssignableTo(typeof(Uri)):
                return (cell, value) =>
                {
                    var uri = (Uri)value;
                    cell.SetHyperlink(new XLHyperlink(uri));
                    cell.Value = uri.ToString();
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

        switch (type)
        {
        case var t when t == typeof(bool): return (cell, value) => cell.Value = (bool)value;
        case var t when t == typeof(sbyte): return (cell, value) => cell.Value = (sbyte?)value;
        case var t when t == typeof(byte): return (cell, value) => cell.Value = (byte?)value;
        case var t when t == typeof(short): return (cell, value) => cell.Value = (short?)value;
        case var t when t == typeof(ushort): return (cell, value) => cell.Value = (ushort?)value;
        case var t when t == typeof(int): return (cell, value) => cell.Value = (int?)value;
        case var t when t == typeof(uint): return (cell, value) => cell.Value = (uint?)value;
        case var t when t == typeof(long): return (cell, value) => cell.Value = (long?)value;
        case var t when t == typeof(ulong): return (cell, value) => cell.Value = (ulong?)value;
        case var t when t == typeof(float): return (cell, value) => cell.Value = (float?)value;
        case var t when t == typeof(double): return (cell, value) => cell.Value = (double?)value;
        case var t when t == typeof(decimal): return (cell, value) => cell.Value = (decimal?)value;
        case var t when t == typeof(DateTime): return (cell, value) => cell.Value = (DateTime?)value;
        case var t when t == typeof(TimeSpan): return (cell, value) => cell.Value = (TimeSpan?)value;
        case var t when t == typeof(sbyte?): return (cell, value) => cell.Value = (sbyte?)value;
        case var t when t == typeof(byte?): return (cell, value) => cell.Value = (byte?)value;
        case var t when t == typeof(short?): return (cell, value) => cell.Value = (short?)value;
        case var t when t == typeof(ushort?): return (cell, value) => cell.Value = (ushort?)value;
        case var t when t == typeof(int?): return (cell, value) => cell.Value = (int?)value;
        case var t when t == typeof(uint?): return (cell, value) => cell.Value = (uint?)value;
        case var t when t == typeof(long?): return (cell, value) => cell.Value = (long?)value;
        case var t when t == typeof(ulong?): return (cell, value) => cell.Value = (ulong?)value;
        case var t when t == typeof(float?): return (cell, value) => cell.Value = (float?)value;
        case var t when t == typeof(double?): return (cell, value) => cell.Value = (double?)value;
        case var t when t == typeof(decimal?): return (cell, value) => cell.Value = (decimal?)value;
        case var t when t == typeof(DateTime?): return (cell, value) => cell.Value = (DateTime?)value;
        case var t when t == typeof(TimeSpan?): return (cell, value) => cell.Value = (TimeSpan?)value;
        case var t when t == typeof(string): return (cell, value) => cell.Value = (string)value;

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
        case var t when t.IsAssignableTo(typeof(ExcelStyle)):
            return (cell, value) =>
            {
                static XLColor fromText(string text) => text.StartsWith('#') ? XLColor.FromHtml(text) : XLColor.FromName(text);
                var style = (ExcelStyle)value;
                if (style.BackColor != null) cell.Style.Fill.BackgroundColor = fromText(style.BackColor);
                if (style.ForeColor != null) cell.Style.Font.FontColor = fromText(style.ForeColor);
                if (style.Extra is var extra && extra is not null)
                {
                    if (extra.Bold) cell.Style.Font.Bold = true;
                    if (extra.Italic) cell.Style.Font.Italic = true;
                    if (extra.Strike) cell.Style.Font.Strikethrough = true;
                    if (extra.Font != null) cell.Style.Font.FontName = extra.Font;
                    if (double.IsFinite(extra.FontSize)) cell.Style.Font.FontSize = extra.FontSize;
                    if (extra.Comment != null) cell.CreateComment().AddText(extra.Comment);
                }
                if (style.Value != null)
                {
                    if (style.DynamicValue && (style.Value is ExcelHyperlink || style.Value is ExcelFormula))
                    {
                        var writer = makeCellWriter(style.Value.GetType(), options);
                        writer(cell, style.Value);
                    }
                    else
                    {
                        writeCellValue(cell, style.Value);
                    }
                }
            };
        default:
            return (cell, value) => writeCellValue(cell, value);
        }
    }

    /// <summary>セルにプリミティブ値を設定する</summary>
    /// <param name="cell">対象セル</param>
    /// <param name="value">設定する値。サポートされるプリミティブ値でない場合は文字列化して設定する。</param>
    private static void writeCellValue(IXLCell cell, object value)
    {
        switch (value)
        {
        case bool v: cell.Value = v; break;
        case sbyte v: cell.Value = v; break;
        case byte v: cell.Value = v; break;
        case short v: cell.Value = v; break;
        case ushort v: cell.Value = v; break;
        case int v: cell.Value = v; break;
        case uint v: cell.Value = v; break;
        case long v: cell.Value = v; break;
        case ulong v: cell.Value = v; break;
        case float v: cell.Value = v; break;
        case double v: cell.Value = v; break;
        case decimal v: cell.Value = v; break;
        case DateTime v: cell.Value = v; break;
        case TimeSpan v: cell.Value = v; break;
        default: cell.Value = value?.ToString(); break;
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
                // フィールドはリフレクションにて、プロパティは取得用デリゲート生成にて。
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

                // カラム数取得の属性利用が指定されていれば、その情報を取得する。
                var span = 1;
                if (options.UseColumnSpanAttribute)
                {
                    var maxLenAttr = member.GetCustomAttribute<MaxLengthAttribute>();
                    if (maxLenAttr != null && 1 < maxLenAttr.Length)
                    {
                        span = maxLenAttr.Length;
                    }
                }

                // デリゲートによるカラム数取得。こちらが指定されたら優先する。
                if (options.ColumnSpanSelector?.Invoke(member) is int selectedSpan && 1 < selectedSpan)
                {
                    span = selectedSpan;
                }

                // カラムキャプションを作成する。
                // とりあえずカラムに出力するメンバ名からデフォルトの名称を作成。
                var captions = (span <= 1) ? new[] { member.Name, } : Enumerable.Range(0, span).Select(i => $"{member.Name}[{i}]").ToArray();

                // キャプション取得の属性利用が指定されていれば、その情報を取得する。
                var order = default(int);
                if (options.UseCaptionAttribute)
                {
                    // Display 属性の取得を試みる
                    var display = member.GetCustomAttribute<DisplayAttribute>();
                    if (display != null)
                    {
                        // 属性から順序を取得
                        order = display.GetOrder() ?? 0;

                        // 名前が指定されていれば、カラム名に採用する
                        if (!string.IsNullOrWhiteSpace(display.Name))
                        {
                            if (span <= 1)
                            {
                                // 単独カラムの場合はそのままカラム名にする
                                captions[0] = display.Name;
                            }
                            else if (display.GroupName is var separator && !string.IsNullOrEmpty(separator))
                            {
                                // GroupName プロパティに文字列が指定されていればそれをセパレータとみなし、分割した文字列をカラム名にする。
                                var names = (display.Name ?? "").Split(separator);
                                for (var i = 0; i < names.Length && i < captions.Length; i++)
                                {
                                    if (names[i] is var name && !string.IsNullOrWhiteSpace(name))
                                    {
                                        // カラム位置に対応する有効な名前があればカラム名に採用
                                        captions[i] = name;
                                    }
                                }
                            }
                            else
                            {
                                // セパレータ無しの場合は名前に添え時付きのカラム名とする
                                for (var i = 0; i < captions.Length; i++)
                                {
                                    captions[i] = $"{display.Name}[{i}]";
                                }
                            }
                        }
                    }
                }

                // デリゲートによるキャプション名取得。こちらが指定されたら優先する。
                if (options.CaptionSelector != null)
                {
                    for (var i = 0; i < captions.Length; i++)
                    {
                        var name = options.CaptionSelector.Invoke(member, i);
                        if (!string.IsNullOrWhiteSpace(name))
                        {
                            captions[i] = name;
                        }
                    }
                }

                // カラム情報を作る
                return new TypeColumn<TSource>(member, returnType, getter, span, captions, order);
            })
            .OrderBy(c => options.UseCaptionAttribute ? c.Order : 0)    // 属性利用時はその Order プロパティを最優先。
            .ThenBy(c => options.SortCaption ? c.Caption : "")          // キャンプションソートが指定されていればそのソート
            .ThenBy(c => options.SortMemberName ? c.Member.Name : "")   // メンバ名ソートが指定されていればそのソート。両方指定されていたらキャプションよりも後(同一キャプションの場合の順序)
            .ToArray();

        return columns;
    }

    /// <summary>型カラム情報</summary>
    /// <typeparam name="TSource">対象の型</typeparam>
    /// <param name="Member">メンバ情報</param>
    /// <param name="MemberType">メンバの取得値型</param>
    /// <param name="Getter">メンバからの値取得デリゲート</param>
    /// <param name="Span">メンバが利用するカラム数</param>
    /// <param name="Captions">カラムキャプション</param>
    /// <param name="Order">カラムの順序</param>
    private record TypeColumn<TSource>(MemberInfo Member, Type MemberType, Func<TSource, object?> Getter, int Span, string[] Captions, int Order)
    {
        /// <summary>代表キャプション</summary>
        public string Caption => this.Captions?.FirstOrDefault() ?? this.Member.Name;
    }
}
