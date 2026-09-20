namespace Lestaly;

/// <summary>
/// 主にスクリプト用の定型実行補助クラス
/// </summary>
public static class Paved
{
    /// <summary>例外を捕捉して処理を実行する。</summary>
    /// <typeparam name="T">戻り値の型</typeparam>
    /// <param name="init">実行設定</param>
    /// <param name="action">実行処理</param>
    /// <returns>処理の戻り値</returns>
    public static async Task<T?> RunAsync<T>(PavedInit? init, Func<PavedOptions<T>, ValueTask<T>> action)
    {
        using var outenc = init?.UseOutputUtf8 == true ? Console.OutputUtf8EncodingPeriod() : default;
        using var inenc = init?.UseInputUtf8 == true ? Console.InputUtf8EncodingPeriod() : default;

        var options = new PavedOptions<T>();
        if (init?.DetectPauseArgs == true)
        {
            var arguments = Environment.GetCommandLineArgs().Skip(1);
            if (arguments.RoughContainsAny(["--no-pause", "--nopause"]))
            {
                options.PauseOn(PavedPause.None);
            }
            else if (arguments.RoughContainsAny(["--pause"]))
            {
                options.PauseOn(PavedPause.Any);
            }
        }

        var result = default(T?);
        var time = default(TimeSpan?);
        var pause = false;
        try
        {
            result = await action(options);
        }
        catch (Exception ex) when (ex is OperationCanceledException or CmdProcCancelException or CmdShellCancelException)
        {
            // キャンセル発生時の一時停止フラグを評価
            pause = options.PauseOnCancel.Enabled;
            time = options.PauseOnCancel.Time;

            // エラーハンドラがあればそれを実行。無ければメッセージを出力しておく。
            if (options.CancelHandler == null)
            {
                var message = options.PauseOnCancel.Message ?? "Operation cancelled.";
                if (message.IsNotEmpty()) using (Console.ForegroundColorPeriod(ConsoleColor.Yellow)) Console.WriteLine(message);
            }
            else
            {
                result = options.CancelHandler(ex);
            }
        }
        catch (Exception ex)
        {
            // エラー発生時の一時停止フラグを評価
            pause = options.PauseOnError.Enabled;
            time = options.PauseOnError.Time;

            // エラーハンドラがあればそれを実行。無ければ例外メッセージを出力しておく。
            if (options.ErrorHandler == null)
            {
                if (options.PauseOnError.Message.IsNotEmpty())
                {
                    using var _ = Console.ForegroundColorPeriod(ConsoleColor.Red);
                    Console.WriteLine(options.PauseOnError.Message);
                }
                else if (ex is CmdProcExitCodeException cex)
                {
                    using var _ = Console.ForegroundColorPeriod(ConsoleColor.Red);
                    Console.WriteLine(cex.Message);
                    if (cex.Output.IsNotWhite()) Console.WriteLine($"Output: {cex.Output}");
                }
                else if (ex is PavedMessageException pex)
                {
                    switch (pex.Kind)
                    {
                    case PavedMessageKind.Error:
                        using (Console.ForegroundColorPeriod(ConsoleColor.Red)) Console.WriteLine(pex.Message);
                        break;
                    case PavedMessageKind.Warning:
                    case PavedMessageKind.Cancelled:
                        using (Console.ForegroundColorPeriod(ConsoleColor.Yellow)) Console.WriteLine(pex.Message);
                        break;
                    case PavedMessageKind.Information:
                    default:
                        Console.WriteLine(pex.Message);
                        break;
                    }
                }
                else
                {
                    using (Console.ForegroundColorPeriod(ConsoleColor.Red)) Console.WriteLine(ex.ToString());
                }
            }
            else
            {
                result = options.ErrorHandler(ex);
            }
        }

        // 終了時の一時停止フラグを評価
        pause = pause || options.PauseOnExit.Enabled;

        // オプションで要求されていてリダイレクトされていない場合に一時停止する
        if (pause && !Console.IsInputRedirected)
        {
            Console.WriteLine(options.PauseOnExit.Message ?? "(Press any key to exit.)");
            time ??= options.PauseOnExit.Time;
            if (time.Value == TimeSpan.Zero)
            {
                Console.ReadKey(true);
            }
            else
            {
                await Console.WaitKeyAsync(intercept: true, time.Value).ConfigureAwait(false);
            }
        }

        return result;
    }

    /// <summary>例外を捕捉して処理を実行する。</summary>
    /// <typeparam name="T">戻り値の型</typeparam>
    /// <param name="action">実行処理</param>
    /// <returns>処理の戻り値</returns>
    public static Task<T?> RunAsync<T>(Func<PavedOptions<T>, ValueTask<T>> action)
        => RunAsync(null, action);

    /// <summary>例外を捕捉して処理を実行する。</summary>
    /// <param name="init">実行設定</param>
    /// <param name="action">実行処理</param>
    /// <returns>エラーコード</returns>
    public static async Task<int> RunAsync(PavedInit? init, Func<PavedOptions<int>, ValueTask> action)
    {
        var exitCode = 0;

        await RunAsync<int>(init, async (options) =>
        {
            try
            {
                await action(options);
                return 0;
            }
            catch (OperationCanceledException)
            {
                exitCode = 254;
                throw;
            }
            catch (CmdProcExitCodeException cex)
            {
                exitCode = cex.ExitCode;
                throw;
            }
            catch (Exception)
            {
                exitCode = 255;
                throw;
            }
        });

        return exitCode;
    }

    /// <summary>例外を捕捉して処理を実行する。</summary>
    /// <param name="action">実行処理</param>
    /// <returns>エラーコード</returns>
    public static Task<int> RunAsync(Func<PavedOptions<int>, ValueTask> action)
        => RunAsync(null, action);

    /// <summary>例外を捕捉して処理を実行する。</summary>
    /// <param name="init">実行設定</param>
    /// <param name="action">実行処理</param>
    /// <returns>エラーコード</returns>
    public static Task<int> RunAsync(PavedInit? init, Func<ValueTask> action)
        => RunAsync(init, _ => action());

    /// <summary>例外を捕捉して処理を実行する。</summary>
    /// <param name="action">実行処理</param>
    /// <returns>エラーコード</returns>
    public static Task<int> RunAsync(Func<ValueTask> action)
        => RunAsync(_ => action());

    /// <summary>例外を捕捉して処理を実行する。デフォルトで一時停止。</summary>
    /// <param name="action">実行処理</param>
    /// <returns>エラーコード</returns>
    public static Task<int> ProceedAsync(Func<PavedOptions<int>, ValueTask> action)
        => RunAsync(new PavedInit(DetectPauseArgs: true, UseInputUtf8: true, UseOutputUtf8: true), action);

    /// <summary>例外を捕捉して処理を実行する。デフォルトで一時停止。</summary>
    /// <param name="noPause">非停止フラグ</param>
    /// <param name="action">実行処理</param>
    /// <returns>エラーコード</returns>
    public static Task<int> ProceedAsync(bool noPause, Func<PavedOptions<int>, ValueTask> action)
        => RunAsync(new PavedInit(UseInputUtf8: true, UseOutputUtf8: true), options =>
        {
            options.PauseOn(noPause ? PavedPause.None : PavedPause.Any);
            return action(options);
        });

    /// <summary>例外を捕捉して処理を実行する。デフォルトで一時停止。</summary>
    /// <param name="action">実行処理</param>
    /// <returns>エラーコード</returns>
    public static Task<int> ProceedAsync(Func<ValueTask> action)
        => ProceedAsync(_ => action());

    /// <summary>例外を捕捉して処理を実行する。デフォルトで一時停止。</summary>
    /// <param name="noPause">非停止フラグ</param>
    /// <param name="action">実行処理</param>
    /// <returns>エラーコード</returns>
    public static Task<int> ProceedAsync(bool noPause, Func<ValueTask> action)
        => ProceedAsync(noPause, _ => action());

}
