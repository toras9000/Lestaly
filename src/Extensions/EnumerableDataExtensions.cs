using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using ClosedXML.Excel;
using CometFlavor.Reflection;

namespace Lestaly;

/// <summary>
/// IEnumerable{T} に対するデータ処理拡張メソッド
/// </summary>
public static class EnumerableDataExtensions
{
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

    /// <summary>型カラム情報</summary>
    /// <typeparam name="TSource">対象の型</typeparam>
    /// <param name="Member">メンバ情報</param>
    /// <param name="MemberType">メンバの取得値型</param>
    /// <param name="Getter">メンバからの値取得デリゲート</param>
    /// <param name="Caption">カラムキャプション</param>
    /// <param name="Order">カラムの順序</param>
    private record TypeColumn<TSource>(MemberInfo Member, Type MemberType, Func<TSource, object?> Getter, string Caption, int Order);
}
