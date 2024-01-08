using System.Buffers;

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
}
