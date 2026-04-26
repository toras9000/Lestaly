using System.Reflection;
using System.Text;

namespace Lestaly;

/// <summary>
/// IEnumerable{T} に対するデータ処理拡張メソッド
/// </summary>
public static partial class EnumerableDataExtensions
{
    /// <summary>シーケンスデータをCSV(Charactor Separated Field)として保存する。</summary>
    /// <typeparam name="TSource">シーケンスの要素型</typeparam>
    /// <param name="self">保存対象シーケンス</param>
    /// <param name="file">保存ファイル</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>書き込み処理タスク</returns>
    public static Task SaveToCsvAsync<TSource>(this IEnumerable<TSource> self, FileInfo file, CancellationToken cancelToken = default)
    {
        ArgumentNullException.ThrowIfNull(file);
        return self.SaveToCsvAsync(file.FullName, cancelToken);
    }

    /// <summary>シーケンスデータをCSV(Charactor Separated Field)として保存する。</summary>
    /// <typeparam name="TSource">シーケンスの要素型</typeparam>
    /// <param name="self">保存対象シーケンス</param>
    /// <param name="file">保存ファイル</param>
    /// <param name="options">保存オプション</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>書き込み処理タスク</returns>
    public static Task SaveToCsvAsync<TSource>(this IEnumerable<TSource> self, FileInfo file, SaveToCsvOptions options, CancellationToken cancelToken = default)
    {
        ArgumentNullException.ThrowIfNull(file);
        return self.SaveToCsvAsync(file.FullName, options, cancelToken);
    }

    /// <summary>シーケンスデータをCSV(Charactor Separated Field)として保存する。</summary>
    /// <typeparam name="TSource">シーケンスの要素型</typeparam>
    /// <param name="self">保存対象シーケンス</param>
    /// <param name="file">保存ファイル</param>
    /// <param name="options">保存オプション</param>
    /// <param name="encoding">保存テキストエンコーディング</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>書き込み処理タスク</returns>
    public static Task SaveToCsvAsync<TSource>(this IEnumerable<TSource> self, FileInfo file, SaveToCsvOptions options, Encoding encoding, CancellationToken cancelToken = default)
    {
        ArgumentNullException.ThrowIfNull(file);
        return self.SaveToCsvAsync(file.FullName, options, encoding, cancelToken);
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
        return self.ToPseudoAsyncEnumerable().SaveToCsvAsync(filePath, options, encoding, cancelToken);
    }

    /// <summary>シーケンスデータをCSV(Charactor Separated Field)として保存する。</summary>
    /// <typeparam name="TSource">シーケンスの要素型</typeparam>
    /// <param name="self">保存対象シーケンス</param>
    /// <param name="file">保存ファイル</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>書き込み処理タスク</returns>
    public static Task SaveToCsvAsync<TSource>(this IAsyncEnumerable<TSource> self, FileInfo file, CancellationToken cancelToken = default)
    {
        ArgumentNullException.ThrowIfNull(file);
        return self.SaveToCsvAsync(file.FullName, cancelToken);
    }

    /// <summary>シーケンスデータをCSV(Charactor Separated Field)として保存する。</summary>
    /// <typeparam name="TSource">シーケンスの要素型</typeparam>
    /// <param name="self">保存対象シーケンス</param>
    /// <param name="file">保存ファイル</param>
    /// <param name="options">保存オプション</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>書き込み処理タスク</returns>
    public static Task SaveToCsvAsync<TSource>(this IAsyncEnumerable<TSource> self, FileInfo file, SaveToCsvOptions options, CancellationToken cancelToken = default)
    {
        ArgumentNullException.ThrowIfNull(file);
        return self.SaveToCsvAsync(file.FullName, options, cancelToken);
    }

    /// <summary>シーケンスデータをCSV(Charactor Separated Field)として保存する。</summary>
    /// <typeparam name="TSource">シーケンスの要素型</typeparam>
    /// <param name="self">保存対象シーケンス</param>
    /// <param name="file">保存ファイル</param>
    /// <param name="options">保存オプション</param>
    /// <param name="encoding">保存テキストエンコーディング</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>書き込み処理タスク</returns>
    public static Task SaveToCsvAsync<TSource>(this IAsyncEnumerable<TSource> self, FileInfo file, SaveToCsvOptions options, Encoding encoding, CancellationToken cancelToken = default)
    {
        ArgumentNullException.ThrowIfNull(file);
        return self.SaveToCsvAsync(file.FullName, options, encoding, cancelToken);
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
        ArgumentNullException.ThrowIfNull(self);
        ArgumentNullException.ThrowIfNull(filePath);
        ArgumentNullException.ThrowIfNull(options);
        ArgumentNullException.ThrowIfNull(encoding);
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

            // クォート対象キャラクタ
            var quoteChars = (stackalloc char[] { options.Separator, '\r', '\n', });

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

                // フィールドがクォート対象キャラクタを含むかを判定
                var pos = field.AsSpan().IndexOfAny(quoteChars);
                if (0 <= pos)
                {
                    // クォートする
                    buffer.Append(options.Quote);
                    foreach (var c in field)
                    {
                        // クォートキャラクタ自信を含む場合は2個並べてエスケープする
                        if (c == options.Quote) buffer.Append(options.Quote);
                        buffer.Append(c);
                    }
                    buffer.Append(options.Quote);
                }
                else
                {
                    // クォートが必要ない場合はそのままをフィールドとして追加
                    buffer.Append(field);
                }
            }

            return buffer.ToString();
        }

        // 出力ストリームを作成
        using var writer = new StreamWriter(filePath, append: false, encoding);

        // ヘッダを出力
        await writer.WriteLineAsync(makeLine(columns.Select(c => c.Caption))).ConfigureAwait(false);

        // シーケンス要素を出力
        await foreach (var elem in self.WithCancellation(cancelToken).ConfigureAwait(false))
        {
            var line = makeLine(columns.Select(c => c.Getter(elem)?.ToString()));
            await writer.WriteLineAsync(line).ConfigureAwait(false);
        }
    }

    /// <summary>型カラム情報</summary>
    /// <typeparam name="TSource">対象の型</typeparam>
    /// <param name="Member">メンバ情報</param>
    /// <param name="MemberType">メンバの取得値型</param>
    /// <param name="Getter">メンバからの値取得デリゲート</param>
    /// <param name="Span">メンバが利用するカラム数</param>
    /// <param name="Captions">カラムキャプション</param>
    /// <param name="Attribute">カラムの属性</param>
    private record TypeColumn<TSource>(MemberInfo Member, Type MemberType, Func<TSource, object?> Getter, int Span, string[] Captions, TypeColumnAttribute? Attribute)
    {
        /// <summary>代表キャプション</summary>
        public string Caption => this.Captions?.FirstOrDefault() ?? this.Member.Name;
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
                var getter = default(Func<TSource, object?>);
                var returnType = default(Type);
                if (member is FieldInfo fieldInfo)
                {
                    getter = options.UseCompiledGetter ? MemberAccessor.CompileFieldGetter<TSource>(fieldInfo, nonPublic: false) : o => fieldInfo.GetValue(o);
                    returnType = fieldInfo.FieldType;
                }
                else
                {
                    var propInfo = (PropertyInfo)member;
                    getter = options.UseCompiledGetter ? MemberAccessor.CompilePropertyGetter<TSource>(propInfo, nonPublic: false) : MemberAccessor.CreatePropertyGetter<TSource>(propInfo, nonPublic: false);
                    returnType = propInfo.PropertyType;
                }

                // メンバに設定された属性を取得
                var columnAttr = options.UseColumnAttribute ? member.GetCustomAttribute<TypeColumnAttribute>() : default;

                // 属性利用が指定されていれば、その列結合情報を取得する。
                var span = 1;
                if (columnAttr != null && 1 < columnAttr.ColumnSpan)
                {
                    span = columnAttr.ColumnSpan;
                }

                // デリゲートによるカラム数取得。こちらが指定されていればそれを優先として上書きする。
                if (options.ColumnSpanSelector?.Invoke(member) is int selectedSpan && 1 < selectedSpan)
                {
                    span = selectedSpan;
                }

                // カラムキャプションを作成する。
                // とりあえずカラムに出力するメンバ名からデフォルトの名称を作成。
                var captions = (span <= 1) ? [member.Name,] : Enumerable.Range(0, span).Select(i => $"{member.Name}[{i}]").ToArray();

                // 属性利用が指定されていれば、カラム名情報を取得する。
                if (columnAttr != null && columnAttr.Caption != null)
                {
                    if (span <= 1)
                    {
                        // 単独カラムの場合はそのままカラム名にする
                        captions[0] = columnAttr.Caption;
                    }
                    else if (columnAttr.Separator.IsNotEmpty())
                    {
                        // Separator プロパティに文字列が指定されていればそのセパレータで分割した文字列をカラム名にする。
                        var names = columnAttr.Caption.Split(columnAttr.Separator);
                        for (var i = 0; i < names.Length && i < captions.Length; i++)
                        {
                            if (names[i].IsNotEmpty())
                            {
                                captions[i] = names[i];
                            }
                        }
                    }
                    else
                    {
                        // セパレータ無しの場合は名前に添え時付きのカラム名とする
                        for (var i = 0; i < captions.Length; i++)
                        {
                            captions[i] = $"{columnAttr.Caption}[{i}]";
                        }
                    }
                }

                // デリゲートによるキャプション名取得。こちらが指定されていればそれを優先として上書きする。
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
                return new TypeColumn<TSource>(member, returnType, getter, span, captions, columnAttr);
            })
            .OrderBy(c => c.Attribute?.Order ?? 0)                      // 属性で指定された順序があればそれでソート
            .ThenBy(c => options.SortCaption ? c.Caption : "")          // キャンプションソートが指定されていればそのソート
            .ThenBy(c => options.SortMemberName ? c.Member.Name : "")   // メンバ名ソートが指定されていればそのソート。両方指定されていたらキャプションよりも後(同一キャプションの場合の順序)
            .ToArray();

        return columns;
    }
}
