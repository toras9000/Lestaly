using System.Buffers;
using System.Runtime.CompilerServices;
using System.Text;

namespace Lestaly;

/// <summary>
/// Stream に対する拡張メソッド
/// </summary>
public static class StreamExtensions
{
    /// <summary>一定のアイドル時間があるまでStreamを読み取る</summary>
    /// <param name="self">対象Stream</param>
    /// <param name="buffer">読み取りデータを格納するバッファ</param>
    /// <param name="idle">データの読み取りを終了する未受信時間[ms]</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>読み取ったバイト数を得るタスク</returns>
    public static async ValueTask<int> ReadToIdleAsync(this Stream self, Memory<byte> buffer, int idle, CancellationToken cancelToken = default)
    {
        if (idle < 0) throw new ArgumentException($"Invalid {nameof(idle)} time");

        // 読み取りデータ格納先管理
        var space = buffer;

        while (!space.IsEmpty)
        {
            try
            {
                // アイドル時間後にキャンセルを行うキャンセルソースを生成
                using var idleCanceller = CancellationTokenSource.CreateLinkedTokenSource(cancelToken);
                idleCanceller.CancelAfter(idle);

                // ストリームから読み取り
                var length = await self.ReadAsync(space, idleCanceller.Token).ConfigureAwait(false);
                if (length == 0) break;

                // 格納先を残り領域分に更新
                space = space[length..];
            }
            catch (OperationCanceledException) when (!cancelToken.IsCancellationRequested)
            {
                break;
            }
        }

        // 読み取り済みサイズを算出して返却
        return buffer.Length - space.Length;
    }

    /// <summary>一定のアイドル時間があるまでStreamを読み取る</summary>
    /// <param name="self">対象Stream</param>
    /// <param name="writer">読み取ったデータを書き込むバッファライター</param>
    /// <param name="idle">データの読み取りを終了する未受信時間[ms]</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>読み取ったバイト数を得るタスク</returns>
    public static async ValueTask<int> ReadToIdleAsync(this Stream self, IBufferWriter<byte> writer, int idle, CancellationToken cancelToken = default)
    {
        if (idle < 0) throw new ArgumentException($"Invalid {nameof(idle)} time");

        var total = 0;
        while (true)
        {
            // ライターのバッファを取得
            var buffer = writer.GetMemory();
            try
            {
                // アイドル時間後にキャンセルを行うキャンセルソースを生成
                using var idleCanceller = CancellationTokenSource.CreateLinkedTokenSource(cancelToken);
                idleCanceller.CancelAfter(idle);

                // ストリームから読み取り
                var length = await self.ReadAsync(buffer, idleCanceller.Token).ConfigureAwait(false);
                if (length == 0) break;

                // ライターに読み取りサイズを通知
                writer.Advance(length);

                // トータル読み取りサイズを管理
                total += length;
            }
            catch (OperationCanceledException) when (!cancelToken.IsCancellationRequested)
            {
                break;
            }
        }
        return total;
    }

    /// <summary>ストリームからテキストとして全行読み取りを行う</summary>
    /// <param name="self">読み取り元ストリーム</param>
    /// <param name="encoding">テキスト読み取りエンコーディング</param>
    /// <param name="respectBOM">BOMを尊重するか否か</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>読み取り結果を得るタスク</returns>
    public static async IAsyncEnumerable<string> EnumerateLinesAsync(this Stream self, Encoding? encoding = null, bool? respectBOM = null, [EnumeratorCancellation] CancellationToken cancelToken = default)
    {
        // BOM検出が自動の場合、シーク可能で先頭位置の場合のみ検出を有効とする
        var detectBOM = respectBOM ?? (self.CanSeek && self.Position == 0);

        // テキストリーダを生成
        using var reader = new StreamReader(self, encoding: encoding, detectEncodingFromByteOrderMarks: detectBOM, leaveOpen: true);

        // 全行読み取り
        while (true)
        {
            var line = await reader.ReadLineAsync(cancelToken).ConfigureAwait(false);
            if (line == null) break;
            yield return line;
        }
    }

    /// <summary>ストリームからテキストとして全行読み取りを行う</summary>
    /// <param name="self">読み取り元ストリーム</param>
    /// <param name="encoding">テキスト読み取りエンコーディング</param>
    /// <param name="respectBOM">BOMを尊重するか否か</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>読み取り結果を得るタスク</returns>
    public static async ValueTask<List<string>> ReadAllLinesAsync(this Stream self, Encoding? encoding = null, bool? respectBOM = null, CancellationToken cancelToken = default)
    {
        // BOM検出が自動の場合、シーク可能で先頭位置の場合のみ検出を有効とする
        var detectBOM = respectBOM ?? (self.CanSeek && self.Position == 0);

        // テキストリーダを生成
        using var reader = new StreamReader(self, encoding: encoding, detectEncodingFromByteOrderMarks: detectBOM, leaveOpen: true);

        // 全行読み取り
        var lines = new List<string>();
        while (true)
        {
            var line = await reader.ReadLineAsync(cancelToken).ConfigureAwait(false);
            if (line == null) break;
            lines.Add(line);
        }
        return lines;
    }

    /// <summary>ストリームから全てのテキスト行を書き込む</summary>
    /// <param name="self">書き込み先ストリーム</param>
    /// <param name="lines">書き込む行ストリーム</param>
    /// <param name="encoding">テキスト書き込みエンコーディング</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>書き込みタスク</returns>
    public static async ValueTask WriteAllLinesAsync(this Stream self, IEnumerable<string?> lines, Encoding? encoding = null, CancellationToken cancelToken = default)
    {
        // テキストライターを生成
        using var writer = new StreamWriter(self, encoding: encoding, leaveOpen: true);

        // 全行書き込み
        foreach (var line in lines)
        {
            await writer.WriteLineAsync(line.AsMemory(), cancelToken);
        }
    }

    /// <summary>ストリーム内容をファイルに保存する</summary>
    /// <param name="self">ストリームを得るタスク</param>
    /// <param name="file">保存先ファイル</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>保存したファイル情報</returns>
    public static async ValueTask<FileInfo> WriteToFileAsync(this Stream self, FileInfo file, CancellationToken cancelToken = default)
    {
        await file.WriteAllBytesAsync(self, default, cancelToken);
        return file;
    }

    /// <summary>ストリーム内容をファイルに保存する</summary>
    /// <param name="self">ストリームを得るタスク</param>
    /// <param name="filePath">保存先ファイルパス</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>保存したファイル情報</returns>
    public static ValueTask<FileInfo> WriteToFileAsync(this Stream self, string filePath, CancellationToken cancelToken = default)
        => self.WriteToFileAsync(new FileInfo(filePath), cancelToken);

    /// <summary>ストリーム内容をファイルに保存する</summary>
    /// <param name="self">ストリームを得るタスク。ここで得たストリームは書き込み後に破棄する。</param>
    /// <param name="file">保存先ファイル</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>保存したファイル情報</returns>
    public static async ValueTask<FileInfo> WriteToFileAsync<TStream>(this Task<TStream> self, FileInfo file, CancellationToken cancelToken = default) where TStream : Stream
    {
        using var stream = await self.ConfigureAwait(false);
        await file.WriteAllBytesAsync(stream, default, cancelToken);
        return file;
    }

    /// <summary>ストリーム内容をファイルに保存する</summary>
    /// <param name="self">ストリームを得るタスク。ここで得たストリームは書き込み後に破棄する。</param>
    /// <param name="filePath">保存先ファイルパス</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>保存したファイル情報</returns>
    public static ValueTask<FileInfo> WriteToFileAsync<TStream>(this Task<TStream> self, string filePath, CancellationToken cancelToken = default) where TStream : Stream
        => self.WriteToFileAsync(new FileInfo(filePath), cancelToken);

    /// <summary>ストリーム内容を読み出す</summary>
    /// <param name="self">読み出すストリーム</param>
    /// <param name="bufferSize">バッファサイズ</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>読みだした内容</returns>
    public static async ValueTask<ReadOnlyMemory<byte>> ToMemoryAsync(this Stream self, int bufferSize, CancellationToken cancelToken = default)
    {
        var writer = new ArrayBufferWriter<byte>();
        while (true)
        {
            var buffer = writer.GetMemory(bufferSize);
            var length = await self.ReadAsync(buffer, cancelToken);
            if (length == 0) break;
            writer.Advance(length);
        }
        return writer.WrittenMemory;
    }

    /// <summary>ストリーム内容を読み出す</summary>
    /// <param name="self">読み出すストリーム</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>読みだした内容</returns>
    public static ValueTask<ReadOnlyMemory<byte>> ToMemoryAsync(this Stream self, CancellationToken cancelToken = default)
        => self.ToMemoryAsync(64 * 1024, cancelToken);

    /// <summary>ストリーム内容を読み出す</summary>
    /// <param name="self">読み出すストリームを得るタスク。ここで得たストリームは書き込み後に破棄する。</param>
    /// <param name="bufferSize">バッファサイズ</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>読みだした内容</returns>
    public static async ValueTask<ReadOnlyMemory<byte>> ToMemoryAsync<TStream>(this Task<TStream> self, int bufferSize, CancellationToken cancelToken = default) where TStream : Stream
    {
        using var stream = await self.ConfigureAwait(false);
        return await stream.ToMemoryAsync(bufferSize, cancelToken);
    }

    /// <summary>ストリーム内容を読み出す</summary>
    /// <param name="self">読み出すストリームを得るタスク。ここで得たストリームは書き込み後に破棄する。</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>読みだした内容</returns>
    public static ValueTask<ReadOnlyMemory<byte>> ToMemoryAsync<TStream>(this Task<TStream> self, CancellationToken cancelToken = default) where TStream : Stream
        => self.ToMemoryAsync(64 * 1024, cancelToken);

}
