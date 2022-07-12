namespace Lestaly;

/// <summary>
/// IEnumerable{T} に対する拡張メソッド
/// </summary>
public static class EnumerableExtensions
{
    /// <summary>
    /// シーケンスのフィルタと条件に適合しない場合のアクションを指定するオペレータ。
    /// </summary>
    /// <typeparam name="TSource">シーケンスの要素型</typeparam>
    /// <param name="self">対象シーケンス</param>
    /// <param name="predicate">要素の通過判定処理</param>
    /// <param name="skipped">要素が通過しない場合に呼び出される処理</param>
    /// <returns>フィルタされたシーケンス</returns>
    public static IEnumerable<TSource> WhereElse<TSource>(this IEnumerable<TSource> self, Func<TSource, bool> predicate, Action<TSource> skipped)
        => CometFlavor.Extensions.Linq.EnumerableExtensions.WhereElse(self, predicate, skipped);
}
