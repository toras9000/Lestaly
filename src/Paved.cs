namespace Lestaly;

/// <summary>
/// 実行補助オプション
/// </summary>
public class PavedOptions<T>
{
    /// <summary>エラー発生時に一時停止するか否か</summary>
    public bool PauseOnError { get; set; } = true;

    /// <summary>キャンセル発生時に一時停止するか否か</summary>
    public bool PauseOnCancel { get; set; } = true;

    /// <summary>処理終了後に一時停止するか否か</summary>
    public bool PauseOnExit { get; set; } = false;

    /// <summary>一時停止時メッセージ</summary>
    public string? PauseMessage { get; set; } = null;

    /// <summary>エラー時ハンドラ</summary>
    public Func<Exception, T>? ErrorHandler { get; set; } = null;

    #region 設定処理
    /// <summary>一時停止無しに設定する。</summary>
    public PavedOptions<T> NoPause()
    {
        this.PauseOnError = false;
        this.PauseOnExit = false;
        this.PauseOnCancel = false;
        return this;
    }

    /// <summary>いずれかの要因による一時停止ありに設定する。</summary>
    public PavedOptions<T> AnyPause()
    {
        this.PauseOnError = true;
        this.PauseOnExit = true;
        this.PauseOnCancel = true;
        return this;
    }
    #endregion
}

/// <summary>
/// 実行補助処理で
/// </summary>
public class PavedMessageException : Exception
{
    /// <summary>エラーメッセージを指定するコンストラクタ</summary>
    /// <param name="message">エラーメッセージ</param>
    public PavedMessageException(string message) : base(message) { this.Fatal = true; }

    /// <summary>エラーメッセージと内部例外を指定するコンストラクタ</summary>
    /// <param name="message">エラーメッセージ</param>
    /// <param name="innerException">内部例外</param>
    public PavedMessageException(string message, Exception innerException) : base(message, innerException) { this.Fatal = true; }

    /// <summary>エラーメッセージと重大度を指定するコンストラクタ</summary>
    /// <param name="message">エラーメッセージ</param>
    /// <param name="fatal">致命的なエラーであるか否か。Pavedでのエラーメッセージ色を決定するために使用。</param>
    public PavedMessageException(string message, bool fatal) : base(message) { this.Fatal = fatal; }

    /// <summary>致命的エラーであるか否か</summary>
    public bool Fatal { get; }
}

/// <summary>
/// 主にスクリプト用の定型実行補助クラス
/// </summary>
public static class Paved
{
    /// <summary>例外を捕捉して処理を実行する。</summary>
    /// <typeparam name="T">戻り値の型</typeparam>
    /// <param name="action">実行処理</param>
    /// <param name="optionsBuilder ">実行オプション設定デリゲート</param>
    /// <returns>処理の戻り値</returns>
    public static async Task<T?> RunAsync<T>(Func<ValueTask<T>> action, Action<PavedOptions<T>>? optionsBuilder = null)
    {
        var options = new PavedOptions<T>();
        var result = default(T);
        var pause = false;
        try
        {
            optionsBuilder?.Invoke(options);

            result = await action();
        }
        catch (OperationCanceledException ex)
        {
            // キャンセル発生時の一時停止フラグを評価
            pause = options.PauseOnCancel == true;

            // エラーハンドラがあればそれを実行。無ければ例外メッセージを出力しておく。
            if (options.ErrorHandler != null)
            {
                result = options.ErrorHandler(ex);
            }
            else
            {
                ConsoleWig.WriteLineColord(ConsoleColor.Yellow, "Operation cancelled.");
            }
        }
        catch (Exception ex)
        {
            // エラー発生時の一時停止フラグを評価
            pause = options.PauseOnError;

            // エラーハンドラがあればそれを実行。無ければ例外メッセージを出力しておく。
            if (options.ErrorHandler != null)
            {
                result = options.ErrorHandler(ex);
            }
            else if (ex is PavedMessageException pex)
            {
                ConsoleWig.WriteLineColord(pex.Fatal ? ConsoleColor.Red : ConsoleColor.Yellow, pex.Message);
            }
            else
            {
                ConsoleWig.WriteLineColord(ConsoleColor.Red, ex.ToString());
            }
        }

        // 終了時の一時停止フラグを評価
        pause = pause || options.PauseOnExit;

        // オプションで要求されていてリダイレクトされていない場合に一時停止する
        if (pause && !Console.IsInputRedirected)
        {
            Console.WriteLine(options.PauseMessage ?? "(Press any key to exit.)");
            Console.ReadKey(true);
        }

        return result;
    }

    /// <summary>例外を捕捉して処理を実行する。</summary>
    /// <param name="action">実行処理</param>
    /// <param name="optionsBuilder ">実行オプション設定デリゲート</param>
    /// <returns>エラーコード</returns>
    public static Task<int> RunAsync(Func<ValueTask> action, Action<PavedOptions<int>>? optionsBuilder = null)
    {
        return RunAsync(async () => { await action(); return 0; }, options =>
        {
            options.ErrorHandler = (ex) =>
            {
                if (ex is OperationCanceledException)
                {
                    ConsoleWig.WriteLineColord(ConsoleColor.Yellow, "Operation cancelled.");
                    return 254;
                }
                if (ex is PavedMessageException pex)
                {
                    ConsoleWig.WriteLineColord(pex.Fatal ? ConsoleColor.Red : ConsoleColor.Yellow, pex.Message);
                }
                else
                {
                    ConsoleWig.WriteLineColord(ConsoleColor.Red, ex.ToString());
                }
                return 255;
            };
            optionsBuilder?.Invoke(options);
        });
    }
}
