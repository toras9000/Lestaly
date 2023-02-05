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
}
