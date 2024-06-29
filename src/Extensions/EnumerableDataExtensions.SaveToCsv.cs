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
        await foreach (var elem in self.WithCancellation(cancelToken).ConfigureAwait(false))
        {
            var line = makeLine(columns.Select(c => c.Getter(elem)?.ToString()));
            await writer.WriteLineAsync(line).ConfigureAwait(false);
        }
    }
}
