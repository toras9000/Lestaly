namespace Lestaly;

/// <summary>
/// 例外を抑制した実行補助クラス
/// </summary>
public static class Try
{
    /// <summary>処理を実行する</summary>
    /// <param name="action">何らかの処理</param>
    /// <returns>例外なく実行されたら true を返却。</returns>
    public static bool Action(Action action)
    {
        if (action != null)
        {
            try { action(); return true; }
            catch { }
        }
        return false;
    }

    /// <summary>非同期処理を実行する</summary>
    /// <param name="action">何らかの処理</param>
    /// <returns>例外なく実行されたら true を返却</returns>
    public static async Task<bool> ActionAsync(Func<ValueTask> action)
    {
        if (action != null)
        {
            try { await action().ConfigureAwait(false); return true; }
            catch { }
        }
        return false;
    }

    /// <summary>処理を実行して結果値を得る</summary>
    /// <typeparam name="T">戻り値型</typeparam>
    /// <param name="action">値を得る処理</param>
    /// <returns>例外なく実行されたら処理の結果値。例外発生時は型のデフォルト値。</returns>
    public static T? Func<T>(Func<T> action)
    {
        if (action != null)
        {
            try { return action(); }
            catch { }
        }
        return default(T?);
    }

    /// <summary>非同期処理を実行して結果値を得る</summary>
    /// <typeparam name="T">戻り値型</typeparam>
    /// <param name="action">値を得る処理</param>
    /// <returns>例外なく実行されたら処理の結果値。例外発生時は型のデフォルト値。</returns>
    public static async Task<T?> FuncAsync<T>(Func<ValueTask<T>> action)
    {
        if (action != null)
        {
            try { return await action().ConfigureAwait(false); }
            catch { }
        }
        return default(T?);
    }
}
