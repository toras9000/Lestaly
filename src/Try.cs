using System.Diagnostics.CodeAnalysis;

namespace Lestaly;

/// <summary>
/// 例外を抑制した実行補助クラス
/// </summary>
public static class Try
{
    /// <summary>処理を実行する</summary>
    /// <param name="action">何らかの処理</param>
    /// <returns>例外が発生した場合はその例外オブジェクト。発生しなければ null を返却。</returns>
    public static Exception? Action(Action action)
    {
        if (action != null)
        {
            try { action.Invoke(); }
            catch (Exception ex) { return ex; }
        }
        return null;
    }

    /// <summary>処理を実行する</summary>
    /// <param name="action">何らかの処理</param>
    /// <param name="alternater">例外発生時の代替処理</param>
    public static void Action(Action action, Action<Exception> alternater)
    {
        if (action != null)
        {
            try { action.Invoke(); }
            catch (Exception ex) { if (alternater != null) try { alternater(ex); } catch { } }
        }
    }

    /// <summary>非同期処理を実行する</summary>
    /// <param name="action">何らかの処理</param>
    /// <returns>例外が発生した場合はそのオブジェクト。発生しなければ null を返却。するタスク</returns>
    public static async Task<Exception?> ActionAsync(Func<Task> action)
    {
        if (action != null)
        {
            try { await action().ConfigureAwait(false); }
            catch (Exception ex) { return ex; }
        }
        return null;
    }

    /// <summary>非同期処理を実行する</summary>
    /// <param name="action">何らかの処理</param>
    /// <param name="alternater">例外発生時の代替処理</param>
    public static async Task ActionAsync(Func<Task> action, Func<Exception, Task> alternater)
    {
        if (action != null)
        {
            try { await action().ConfigureAwait(false); }
            catch (Exception ex) { if (alternater != null) try { await alternater(ex); } catch { } }
        }
    }

    /// <summary>処理を実行して結果値を得る</summary>
    /// <typeparam name="TResult">戻り値型</typeparam>
    /// <param name="action">値を得る処理</param>
    /// <param name="alternate">処理で値を得られなかった場合の代替値</param>
    /// <returns>処理結果値または代替値、および例外発生時はその例外オブジェクト(未発生の場合はnull)。</returns>
    public static (TResult? value, Exception?) Func<TResult>(Func<TResult?> action, TResult? alternate = default)
    {
        if (action != null)
        {
            try { return (action(), null); }
            catch (Exception ex) { return (alternate, ex); }
        }
        return (alternate, null);
    }

    /// <summary>処理を実行して結果値を得る</summary>
    /// <typeparam name="TResult">戻り値型</typeparam>
    /// <param name="action">値を得る処理</param>
    /// <param name="alternater">例外発生時の代替処理</param>
    /// <returns>処理結果値または代替値。</returns>
    public static TResult? Func<TResult>(Func<TResult?> action, Func<Exception, TResult?> alternater)
    {
        if (action != null)
        {
            try { return action(); }
            catch (Exception ex) { if (alternater != null) try { return alternater(ex); } catch { } }
        }
        return default;
    }

    /// <summary>処理を実行して結果値を得る</summary>
    /// <typeparam name="TResult">戻り値型</typeparam>
    /// <param name="action">値を得る処理</param>
    /// <returns>処理結果値または型のデフォルト値。</returns>
    public static TResult? FuncOrDefault<TResult>(Func<TResult?> action)
        => Func(action, _ => default);

    /// <summary>非同期処理を実行して結果値を得る</summary>
    /// <typeparam name="TResult">戻り値型</typeparam>
    /// <param name="action">値を得る処理</param>
    /// <param name="alternate">処理で値を得られなかった場合の代替値</param>
    /// <returns>例外なく実行されたら処理の結果値。例外発生時は代替値。</returns>
    public static async Task<(TResult? value, Exception? error)> FuncAsync<TResult>(Func<Task<TResult?>> action, TResult? alternate = default)
    {
        if (action != null)
        {
            try { return (await action().ConfigureAwait(false), null); }
            catch (Exception ex) { return (alternate, ex); }
        }
        return (alternate, null);
    }

    /// <summary>非同期処理を実行して結果値を得る</summary>
    /// <typeparam name="TResult">戻り値型</typeparam>
    /// <param name="action">値を得る処理</param>
    /// <param name="alternater">例外発生時の代替処理</param>
    /// <returns>例外なく実行されたら処理の結果値。例外発生時は代替値。</returns>
    public static async Task<TResult?> FuncAsync<TResult>(Func<Task<TResult?>> action, Func<Exception, Task<TResult?>> alternater)
    {
        if (action != null)
        {
            try { return await action().ConfigureAwait(false); }
            catch (Exception ex) { if (alternater != null) try { return await alternater(ex); } catch { } }
        }
        return default;
    }

    /// <summary>非同期処理を実行して結果値を得る</summary>
    /// <typeparam name="TResult">戻り値型</typeparam>
    /// <param name="action">値を得る処理</param>
    /// <returns>例外なく実行されたら処理の結果値。例外発生時は型のデフォルト値。</returns>
    public static Task<TResult?> FuncOrDefaultAsync<TResult>(Func<Task<TResult?>> action)
        => FuncAsync(action, _ => Task.FromResult<TResult?>(default));
}
