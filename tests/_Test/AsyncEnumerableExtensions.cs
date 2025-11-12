namespace LestalyTest;

public static class AsyncEnumerableExtensions
{
#if !NET10_0_OR_GREATER
    public static async Task<T[]> ToArrayAsync<T>(this IAsyncEnumerable<T> self)
    {
        var list = new List<T>();
        await foreach (var item in self)
        {
            list.Add(item);
        }
        return list.ToArray();
    }
#endif
}
