using System.Runtime.CompilerServices;
using System.Threading.Tasks.Dataflow;

namespace Lestaly;

/// <summary>
/// IEnumerable{T} に対するデータ処理拡張メソッド
/// </summary>
public static partial class EnumerableDataExtensions
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
        }, cancelToken);

        // データブロックの出力を取り出して列挙
        while (await block.OutputAvailableAsync(cancelToken).ConfigureAwait(false))
        {
            yield return await block.ReceiveAsync(cancelToken).ConfigureAwait(false);
        }

        // 投入タスクとブロックの終了を待機
        // ブロックは上記で
        await Task.WhenAll(sender, block.Completion);
    }
}
