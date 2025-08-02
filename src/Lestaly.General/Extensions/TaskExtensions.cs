namespace Lestaly;

/// <summary>
/// Taskに対する拡張メソッド
/// </summary>
public static class TaskExtensions
{
    /// <summary>Task{T} を Task{T?} に読みかえる。</summary>
    /// <remarks>!よりも目立たせたい場合向けに。</remarks>
    /// <typeparam name="T">返却型</typeparam>
    /// <param name="self">対象Task</param>
    /// <returns>返却型を読み替えたTask</returns>
    public static Task<T?> AsNullable<T>(this Task<T> self) => self!;

    /// <summary>Task{T} を Task{T?} に読みかえる。</summary>
    /// <remarks>!よりも目立たせたい場合向けに。</remarks>
    /// <typeparam name="T">返却型</typeparam>
    /// <param name="self">対象Task</param>
    /// <returns>返却型を読み替えたTask</returns>
    public static ValueTask<T?> AsNullable<T>(this ValueTask<T> self) => self!;

    /// <summary>非同期で得る文字列をトリムする</summary>
    /// <param name="self">文字列を得るタスク</param>
    /// <returns>トリムした文字列を得るタスク</returns>
    public static async ValueTask<string?> TrimAsync(this Task<string?> self)
        => (await self.ConfigureAwait(false))?.Trim();

    /// <summary>非同期で得る文字列をトリムする</summary>
    /// <param name="self">文字列を得るタスク</param>
    /// <returns>トリムした文字列を得るタスク</returns>
    public static async ValueTask<string?> TrimAsync(this ValueTask<string?> self)
        => (await self.ConfigureAwait(false))?.Trim();
}
