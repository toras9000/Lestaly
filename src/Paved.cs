namespace Lestaly;

/// <summary>
/// 実行補助オプション
/// </summary>
public class PavedOptions
{
    /// <summary>エラー発生時に一時停止するか否か</summary>
    public bool PauseOnError { get; set; } = true;

    /// <summary>キャンセル発生時に一時停止するか否か</summary>
    public bool PauseOnCancel { get; set; } = true;

    /// <summary>処理終了後に一時停止するか否か</summary>
    public bool PauseOnExit { get; set; } = false;

    /// <summary>一時停止時メッセージ</summary>
    public string? PauseMessage { get; set; } = null;

    #region 定義済みインスタンス
    /// <summary>一時停止無しの定義済みインスタンス</summary>
    public static PavedOptions NoPause { get; } = new PavedOptions { PauseOnError = false, PauseOnExit = false, PauseOnCancel = false, };

    /// <summary>なんらかで一時停止することを示す定義済みインスタンス</summary>
    public static PavedOptions AnyPause { get; } = new PavedOptions { PauseOnError = true, PauseOnExit = true, PauseOnCancel = true, };
    #endregion
}

/// <summary>
/// 主にスクリプト用の定型実行補助クラス
/// </summary>
public static class Paved
{
    /// <summary>例外を捕捉して処理を実行する。</summary>
    /// <typeparam name="T">戻り値の型</typeparam>
    /// <param name="action">実行処理</param>
    /// <param name="options">実行オプション</param>
    /// <param name="errorHandler">エラーハンドラ</param>
    /// <returns>処理の戻り値</returns>
    public static async Task<T?> RunAsync<T>(Func<ValueTask<T>> action, PavedOptions options, Func<Exception, T>? errorHandler = null)
    {
        var result = default(T);
        var pause = false;
        try
        {
            result = await action();
        }
        catch (OperationCanceledException ex)
        {
            // キャンセル発生時の一時停止フラグを評価
            pause = options?.PauseOnCancel == true;

            // エラーハンドラがあればそれを実行。無ければ例外メッセージを出力しておく。
            if (errorHandler != null)
            {
                result = errorHandler(ex);
            }
            else
            {
                ConsoleWig.WriteLineColord(ConsoleColor.Yellow, "Operation cancelled.");
            }
        }
        catch (Exception ex)
        {
            // エラー発生時の一時停止フラグを評価
            pause = options?.PauseOnError == true;

            // エラーハンドラがあればそれを実行。無ければ例外メッセージを出力しておく。
            if (errorHandler != null)
            {
                result = errorHandler(ex);
            }
            else
            {
                ConsoleWig.WriteLineColord(ConsoleColor.Red, ex.ToString());
            }
        }

        // 終了時の一時停止フラグを評価
        pause = pause || options?.PauseOnExit == true;

        // オプションで要求されていてリダイレクトされていない場合に一時停止する
        if (pause && !Console.IsInputRedirected)
        {
            Console.WriteLine(options?.PauseMessage ?? "(Press any key to exit.)");
            Console.ReadKey(true);
        }

        return result;
    }

    /// <summary>例外を捕捉して処理を実行する。</summary>
    /// <typeparam name="T">戻り値の型</typeparam>
    /// <param name="action">実行処理</param>
    /// <param name="errorHandler">エラーハンドラ</param>
    /// <returns>処理の戻り値</returns>
    public static Task<T?> RunAsync<T>(Func<ValueTask<T>> action, Func<Exception, T>? errorHandler = null)
    {
        var options = new PavedOptions();
        return RunAsync(action, options, errorHandler);
    }

    /// <summary>例外を捕捉して処理を実行する。</summary>
    /// <param name="action">実行処理</param>
    /// <param name="options">実行オプション</param>
    /// <param name="errorHandler">エラーハンドラ</param>
    /// <returns>エラーコード</returns>
    public static Task<int> RunAsync(Func<ValueTask> action, PavedOptions options, Func<Exception, int>? errorHandler = null)
    {
        var wrapErrHandler = errorHandler ?? ((ex) =>
        {
            if (ex is OperationCanceledException)
            {
                ConsoleWig.WriteLineColord(ConsoleColor.Yellow, "Operation cancelled.");
                return 254;
            }
            ConsoleWig.WriteLineColord(ConsoleColor.Red, ex.ToString());
            return 255;
        });
        return RunAsync(async () => { await action(); return 0; }, options, wrapErrHandler);
    }

    /// <summary>例外を捕捉して処理を実行する。</summary>
    /// <param name="action">実行処理</param>
    /// <param name="errorHandler">エラーハンドラ</param>
    /// <returns>エラーコード</returns>
    public static Task<int> RunAsync(Func<ValueTask> action, Func<Exception, int>? errorHandler = null)
    {
        var options = new PavedOptions();
        return RunAsync(async () => { await action(); return 0; }, options, errorHandler);
    }
}
