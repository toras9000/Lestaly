﻿namespace Lestaly;

/// <summary>
/// 主にスクリプト用の定型実行補助クラス
/// </summary>
public static class Paved
{
    /// <summary>例外を捕捉して処理を実行する。</summary>
    /// <typeparam name="T">戻り値の型</typeparam>
    /// <param name="action">実行処理</param>
    /// <param name="config ">実行オプション設定デリゲート</param>
    /// <returns>処理の戻り値</returns>
    public static async Task<T?> RunAsync<T>(Func<ValueTask<T>> action, Action<PavedOptions<T>>? config = null)
    {
        var options = new PavedOptions<T>();
        var console = ConsoleWig.Facade;
        var result = default(T);
        var pause = false;
        try
        {
            config?.Invoke(options);
            if (options.Console != null) console = options.Console;

            result = await action();
        }
        catch (Exception ex) when (ex is OperationCanceledException or CmdProcCancelException or CmdShellCancelException)
        {
            // キャンセル発生時の一時停止フラグを評価
            pause = options.PauseOnCancel == true;

            // エラーハンドラがあればそれを実行。無ければ例外メッセージを出力しておく。
            var handler = options.CancelHandler ?? options.ErrorHandler;
            if (handler != null)
            {
                result = handler(ex);
            }
            else
            {
                console.WriteLineColored(ConsoleColor.Yellow, "Operation cancelled.");
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
                switch (pex.Kind)
                {
                case PavedMessageKind.Error:
                    console.WriteLineColored(ConsoleColor.Red, pex.Message);
                    break;
                case PavedMessageKind.Warning:
                case PavedMessageKind.Cancelled:
                    console.WriteLineColored(ConsoleColor.Yellow, pex.Message);
                    break;
                case PavedMessageKind.Information:
                default:
                    Console.WriteLine(pex.Message);
                    break;
                }
            }
            else
            {
                console.WriteLineColored(ConsoleColor.Red, ex.ToString());
            }
        }

        // 終了時の一時停止フラグを評価
        pause = pause || options.PauseOnExit;

        // オプションで要求されていてリダイレクトされていない場合に一時停止する
        if (pause && !Console.IsInputRedirected)
        {
            if (0 < options.PauseTime)
            {
                Console.WriteLine(options.PauseMessage ?? "(Press any key to exit.)");
                await ConsoleWig.WaitKeyAsync(intercept: true, options.PauseTime).ConfigureAwait(false);
            }
            else
            {
                Console.WriteLine(options.PauseMessage ?? "(Press any key to exit.)");
                Console.ReadKey(true);
            }
        }

        return result;
    }

    /// <summary>例外を捕捉して処理を実行する。</summary>
    /// <param name="action">実行処理</param>
    /// <param name="config ">実行オプション設定デリゲート</param>
    /// <returns>エラーコード</returns>
    public static Task<int> RunAsync(Func<ValueTask> action, Action<PavedOptions<int>>? config = null)
    {
        return RunAsync(async () => { await action(); return 0; }, options =>
        {
            var console = ConsoleWig.Facade;
            options.ErrorHandler = (ex) =>
            {
                if (ex is OperationCanceledException)
                {
                    console.WriteLineColored(ConsoleColor.Yellow, "Operation cancelled.");
                    return 254;
                }
                if (ex is PavedMessageException pex)
                {
                    var exitCode = 255;
                    switch (pex.Kind)
                    {
                    case PavedMessageKind.Error:
                        console.WriteLineColored(ConsoleColor.Red, pex.Message);
                        break;
                    case PavedMessageKind.Warning:
                        console.WriteLineColored(ConsoleColor.Yellow, pex.Message);
                        break;
                    case PavedMessageKind.Cancelled:
                        exitCode = 254;
                        console.WriteLineColored(ConsoleColor.Yellow, pex.Message);
                        break;
                    case PavedMessageKind.Information:
                    default:
                        Console.WriteLine(pex.Message);
                        break;
                    }
                    if (pex is PavedExitException eex)
                    {
                        exitCode = eex.ExitCode;
                    }
                    return exitCode;
                }

                console.WriteLineColored(ConsoleColor.Red, ex.ToString());
                return 255;
            };
            config?.Invoke(options);
            if (options.Console != null) console = options.Console;
        });
    }
}
