namespace Lestaly;

/// <summary>
/// 主にスクリプト用の定型実行補助クラス
/// </summary>
public static class Paved
{
    /// <summary>例外を捕捉して処理を実行する。</summary>
    /// <typeparam name="T">戻り値の型</typeparam>
    /// <param name="action">実行処理</param>
    /// <returns>処理の戻り値</returns>
    public static async Task<T?> RunAsync<T>(Func<PavedOptions<T>, ValueTask<T>> action)
    {
        var options = new PavedOptions<T>();
        var console = ConsoleWig.Facade;
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
                if (message.IsNotEmpty()) using (console.ForegroundColorPeriod(ConsoleColor.Yellow)) console.WriteLine();
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
                    using var _ = console.ForegroundColorPeriod(ConsoleColor.Red);
                    console.WriteLine(options.PauseOnError.Message);
                }
                else if (ex is CmdProcExitCodeException cex)
                {
                    using var _ = console.ForegroundColorPeriod(ConsoleColor.Red);
                    console.WriteLine(cex.Message);
                    if (cex.Output.IsNotWhite()) console.WriteLine($"Output: {cex.Output}");
                }
                else if (ex is PavedMessageException pex)
                {
                    switch (pex.Kind)
                    {
                    case PavedMessageKind.Error:
                        using (console.ForegroundColorPeriod(ConsoleColor.Red)) console.WriteLine(pex.Message);
                        break;
                    case PavedMessageKind.Warning:
                    case PavedMessageKind.Cancelled:
                        using (console.ForegroundColorPeriod(ConsoleColor.Yellow)) console.WriteLine(pex.Message);
                        break;
                    case PavedMessageKind.Information:
                    default:
                        console.WriteLine(pex.Message);
                        break;
                    }
                }
                else
                {
                    using (console.ForegroundColorPeriod(ConsoleColor.Red)) console.WriteLine(ex.ToString());
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
            time = time ?? options.PauseOnExit.Time;
            if (time.Value == TimeSpan.Zero)
            {
                Console.ReadKey(true);
            }
            else
            {
                await ConsoleWig.WaitKeyAsync(intercept: true, time.Value).ConfigureAwait(false);
            }
        }

        return result;
    }

    /// <summary>例外を捕捉して処理を実行する。</summary>
    /// <param name="action">実行処理</param>
    /// <returns>エラーコード</returns>
    public static async Task<int> RunAsync(Func<PavedOptions<int>, ValueTask> action)
    {
        var exitCode = 0;

        await RunAsync<int>(async (options) =>
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
    public static Task<int> RunAsync(Func<ValueTask> action)
        => RunAsync(_ => action());

}
