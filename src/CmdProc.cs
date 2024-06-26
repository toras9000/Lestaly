﻿using System.Diagnostics;
using System.Text;

namespace Lestaly;

/// <summary>コマンドの終了コード値表現型</summary>
/// <param name="Code">終了コード</param>
public record struct CmdExit(int Code)
{
    /// <summary>終了コードを整数値に暗黙変換演算子オーバーロード</summary>
    /// <param name="exit">終了コード値表現型</param>
    public static implicit operator int(CmdExit exit) => exit.Code;
}

/// <summary>コマンドの実行結果値表現型</summary>
/// <param name="ExitCode">終了コード</param>
/// <param name="Output">コマンドの出力</param>
public record struct CmdResult(int ExitCode, string Output);

/// <summary>
/// コマンド実行クラス
/// </summary>
[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1068:CancellationToken パラメーターは最後に指定する必要があります", Justification = "パラメータ利用頻度的にここでは無視する")]
public static class CmdProc
{
    /// <summary>コマンドを実行して終了コードを取得する</summary>
    /// <param name="command">実行コマンド</param>
    /// <param name="arguments">引数リスト</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <param name="workDir">作業ディレクトリ</param>
    /// <param name="environments">環境変数</param>
    /// <param name="stdOut">標準出力のリダイレクト書き込み先ライター。nullの場合はリダイレクトなし。</param>
    /// <param name="stdErr">標準エラーのリダイレクト書き込み先ライター。nullの場合はリダイレクトなし。</param>
    /// <param name="stdIn">標準入力のリダイレクト読み込み元リーダー。nullの場合はリダイレクトなし。</param>
    /// <param name="outEncoding">プロセスの出力テキスト読み取り時のエンコーディング</param>
    /// <param name="inEncoding">プロセスの入力テキストを書き込む際のエンコーディング</param>
    /// <returns>呼び出しプロセスの終了コード</returns>
    public static Task<CmdExit> ExecAsync(string command, IEnumerable<string>? arguments = null, CancellationToken cancelToken = default, string? workDir = null, IEnumerable<KeyValuePair<string, string?>>? environments = null, TextWriter? stdOut = null, TextWriter? stdErr = null, TextReader? stdIn = null, Encoding? outEncoding = null, Encoding? inEncoding = null)
            => execAsync(command, listArgumenter(arguments), cancelToken, workDir, environments, stdOut, stdErr, stdIn, outEncoding, inEncoding);

    /// <summary>コマンドを実行して終了コードを取得する</summary>
    /// <param name="command">実行コマンド</param>
    /// <param name="arguments">引数文字列</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <param name="workDir">作業ディレクトリ</param>
    /// <param name="environments">環境変数</param>
    /// <param name="stdOut">標準出力のリダイレクト書き込み先ライター。nullの場合はリダイレクトなし。</param>
    /// <param name="stdErr">標準エラーのリダイレクト書き込み先ライター。nullの場合はリダイレクトなし。</param>
    /// <param name="stdIn">標準入力のリダイレクト読み込み元リーダー。nullの場合はリダイレクトなし。</param>
    /// <param name="outEncoding">プロセスの出力テキスト読み取り時のエンコーディング</param>
    /// <param name="inEncoding">プロセスの入力テキストを書き込む際のエンコーディング</param>
    /// <returns>呼び出しプロセスの終了コード</returns>
    public static Task<CmdExit> ExecAsync(string command, string? arguments, CancellationToken cancelToken = default, string? workDir = null, IEnumerable<KeyValuePair<string, string?>>? environments = null, TextWriter? stdOut = null, TextWriter? stdErr = null, TextReader? stdIn = null, Encoding? outEncoding = null, Encoding? inEncoding = null)
        => execAsync(command, t => t.Arguments = arguments, cancelToken, workDir, environments, stdOut, stdErr, stdIn, outEncoding, inEncoding);

    /// <summary>コマンドを実行して終了コードと出力テキストを取得する</summary>
    /// <param name="command">実行コマンド</param>
    /// <param name="arguments">引数リスト</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <param name="workDir">作業ディレクトリ</param>
    /// <param name="environments">環境変数</param>
    /// <param name="readStdOut">標準出力を読み取るか否か</param>
    /// <param name="readStdErr">標準エラーを読み取るか否か</param>
    /// <param name="inputText">標準入力に書き込むテキスト。nullの場合は書き込みなし。</param>
    /// <param name="outEncoding">プロセスの出力テキスト読み取り時のエンコーディング</param>
    /// <param name="inEncoding">プロセスの入力テキストを書き込む際のエンコーディング</param>
    /// <returns>呼び出しプロセスの終了コードと出力テキスト</returns>
    public static Task<CmdResult> RunAsync(string command, IEnumerable<string>? arguments = null, CancellationToken cancelToken = default, string? workDir = null, IEnumerable<KeyValuePair<string, string?>>? environments = null, bool readStdOut = true, bool readStdErr = true, string? inputText = null, Encoding? outEncoding = null, Encoding? inEncoding = null)
        => runAsync(command, listArgumenter(arguments), cancelToken, workDir, environments, readStdOut, readStdErr, inputText, outEncoding, inEncoding);

    /// <summary>コマンドを実行して終了コードと出力テキストを取得する</summary>
    /// <param name="command">実行コマンド</param>
    /// <param name="arguments">引数文字列</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <param name="workDir">作業ディレクトリ</param>
    /// <param name="environments">環境変数</param>
    /// <param name="readStdOut">標準出力を読み取るか否か</param>
    /// <param name="readStdErr">標準エラーを読み取るか否か</param>
    /// <param name="inputText">標準入力に書き込むテキスト。nullの場合は書き込みなし。</param>
    /// <param name="outEncoding">プロセスの出力テキスト読み取り時のエンコーディング</param>
    /// <param name="inEncoding">プロセスの入力テキストを書き込む際のエンコーディング</param>
    /// <returns>呼び出しプロセスの終了コードと出力テキスト</returns>
    public static Task<CmdResult> RunAsync(string command, string? arguments, CancellationToken cancelToken = default, string? workDir = null, IEnumerable<KeyValuePair<string, string?>>? environments = null, bool readStdOut = true, bool readStdErr = true, string? inputText = null, Encoding? outEncoding = null, Encoding? inEncoding = null)
        => runAsync(command, t => t.Arguments = arguments, cancelToken, workDir, environments, readStdOut, readStdErr, inputText, outEncoding, inEncoding);

    /// <summary>コマンドを実行して出力テキストを取得する</summary>
    /// <param name="command">実行コマンド</param>
    /// <param name="arguments">引数リスト</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <param name="workDir">作業ディレクトリ</param>
    /// <param name="environments">環境変数</param>
    /// <param name="readStdErr">標準エラーを読み取るか否か</param>
    /// <param name="inputText">標準入力に書き込むテキスト。nullの場合は書き込みなし。</param>
    /// <param name="outEncoding">プロセスの出力テキスト読み取り時のエンコーディング</param>
    /// <param name="inEncoding">プロセスの入力テキストを書き込む際のエンコーディング</param>
    /// <param name="allowExitCodes">正常とみなす終了コード。指定がない場合はゼロのみを正常とする。</param>
    /// <returns>呼び出しプロセスの出力テキスト</returns>
    public static Task<string> CallAsync(string command, IEnumerable<string>? arguments = null, CancellationToken cancelToken = default, string? workDir = null, IEnumerable<KeyValuePair<string, string?>>? environments = null, bool readStdErr = true, string? inputText = null, Encoding? outEncoding = null, Encoding? inEncoding = null, IEnumerable<int>? allowExitCodes = null)
        => callAsync(command, listArgumenter(arguments), cancelToken, workDir, environments, readStdErr, inputText, outEncoding, inEncoding, allowExitCodes);

    /// <summary>コマンドを実行して出力テキストを取得する</summary>
    /// <param name="command">実行コマンド</param>
    /// <param name="arguments">引数文字列</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <param name="workDir">作業ディレクトリ</param>
    /// <param name="environments">環境変数</param>
    /// <param name="readStdErr">標準エラーを読み取るか否か</param>
    /// <param name="inputText">標準入力に書き込むテキスト。nullの場合は書き込みなし。</param>
    /// <param name="outEncoding">プロセスの出力テキスト読み取り時のエンコーディング</param>
    /// <param name="inEncoding">プロセスの入力テキストを書き込む際のエンコーディング</param>
    /// <param name="allowExitCodes">正常とみなす終了コード。指定がない場合はゼロのみを正常とする。</param>
    /// <returns>呼び出しプロセスの出力テキスト</returns>
    public static Task<string> CallAsync(string command, string? arguments, CancellationToken cancelToken = default, string? workDir = null, IEnumerable<KeyValuePair<string, string?>>? environments = null, bool readStdErr = true, string? inputText = null, Encoding? outEncoding = null, Encoding? inEncoding = null, IEnumerable<int>? allowExitCodes = null)
        => callAsync(command, t => t.Arguments = arguments, cancelToken, workDir, environments, readStdErr, inputText, outEncoding, inEncoding, allowExitCodes);

    /// <summary>コマンド終了コードに対して有効判定を行う</summary>
    /// <remarks>終了コードが許容する値でない場合は例外をスローする</remarks>
    /// <param name="self">コマンド終了コード値型</param>
    /// <param name="allowExitCodes">許容する(エラーではない)終了コード値</param>
    /// <param name="ex">終了コードがエラー値の場合にスローする例外オブジェクトジェネレータ</param>
    /// <returns>許容値だった場合にその終了コード値を返す</returns>
    public static async Task<int> AsSuccessCode(this Task<CmdExit> self, IEnumerable<int>? allowExitCodes = null, Func<int, Exception>? ex = null)
    {
        // 正常とみなす終了コード
        var validCodes = allowExitCodes ?? new[] { 0, };

        // コマンドを実行
        var exit = await self.ConfigureAwait(false);

        // 実行完了した場合は終了コードが正常を示すか検証。正常でない場合は例外を送出。
        if (!validCodes.Contains(exit.Code))
        {
            throw ex?.Invoke(exit.Code) ?? new CmdProcExitCodeException(exit.Code, "", message: $"Faild. ExitCode={exit.Code}");
        }

        return exit.Code;
    }

    /// <summary>コマンド実行結果に対して有効判定を行う</summary>
    /// <remarks>終了コードが許容する値でない場合は例外をスローする</remarks>
    /// <param name="self">コマンド終了コード値型</param>
    /// <param name="allowCodes">許容する(エラーではない)終了コード値</param>
    /// <param name="ex">終了コードがエラー値の場合にスローする例外オブジェクトジェネレータ</param>
    /// <returns>終了コードが許容値だった場合には実行結果をそのまま返す</returns>
    public static async Task<CmdResult> AsSuccessCode(this Task<CmdResult> self, IEnumerable<int>? allowCodes = null, Func<int, Exception>? ex = null)
    {
        // 正常とみなす終了コード
        var validCodes = allowCodes ?? new[] { 0, };

        // コマンドを実行
        var result = await self.ConfigureAwait(false);

        // 実行完了した場合は終了コードが正常を示すか検証。正常でない場合は例外を送出。
        if (!validCodes.Contains(result.ExitCode))
        {
            throw ex?.Invoke(result.ExitCode) ?? new CmdProcExitCodeException(result.ExitCode, result.Output, message: $"Faild. ExitCode={result.ExitCode}");
        }

        return result;
    }

    /// <summary>コマンドが正常終了の場合に出力を取得する</summary>
    /// <remarks>終了コードが許容する値でない場合は例外をスローする</remarks>
    /// <param name="self">コマンド終了コード値型</param>
    /// <param name="allowCodes">許容する(エラーではない)終了コード値</param>
    /// <param name="ex">終了コードがエラー値の場合にスローする例外オブジェクトジェネレータ</param>
    /// <returns>許容値だった場合に出力文字列を返す</returns>
    public static async Task<string> AsSuccessOutput(this Task<CmdResult> self, IEnumerable<int>? allowCodes = null, Func<int, Exception>? ex = null)
    {
        return (await self.AsSuccessCode(allowCodes, ex).ConfigureAwait(false)).Output;
    }

    /// <summary>コマンドを実行して終了コードを取得する</summary>
    /// <param name="command">実行コマンド</param>
    /// <param name="argumenter">コマンド引数設定デリゲート</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <param name="workDir">作業ディレクトリ</param>
    /// <param name="environments">環境変数</param>
    /// <param name="stdOut">標準出力のリダイレクト書き込み先ライター。nullの場合はリダイレクトなし。</param>
    /// <param name="stdErr">標準エラーのリダイレクト書き込み先ライター。nullの場合はリダイレクトなし。</param>
    /// <param name="stdIn">標準入力のリダイレクト読み込み元リーダー。nullの場合はリダイレクトなし。</param>
    /// <param name="outEncoding">プロセスの出力テキスト読み取り時のエンコーディング</param>
    /// <param name="inEncoding">プロセスの入力テキストを書き込む際のエンコーディング</param>
    /// <returns>呼び出しプロセスの終了コード</returns>
    internal static async Task<CmdExit> execAsync(string command, Action<ProcessStartInfo>? argumenter, CancellationToken cancelToken = default, string? workDir = null, IEnumerable<KeyValuePair<string, string?>>? environments = null, TextWriter? stdOut = null, TextWriter? stdErr = null, TextReader? stdIn = null, Encoding? outEncoding = null, Encoding? inEncoding = null)
    {
        // 実行するコマンドの情報を設定
        var target = new ProcessStartInfo();
        target.FileName = command;
        argumenter?.Invoke(target);
        foreach (var env in environments.CoalesceEmpty())
        {
            target.Environment[env.Key] = env.Value;
        }
        if (workDir != null)
        {
            target.WorkingDirectory = workDir;
        }
        target.CreateNoWindow = true;
        target.UseShellExecute = false;
        if (stdOut != null)
        {
            target.RedirectStandardOutput = true;
            if (outEncoding != null) target.StandardOutputEncoding = outEncoding;
        }
        if (stdErr != null)
        {
            target.RedirectStandardError = true;
            if (outEncoding != null) target.StandardErrorEncoding = outEncoding;
        }
        if (stdIn != null)
        {
            target.RedirectStandardInput = true;
            if (inEncoding != null) target.StandardInputEncoding = inEncoding;
        }

        // コマンドを実行
        var proc = Process.Start(target) ?? throw new CmdProcException("Cannot execute command.");

        // 出力ストリームのリダイレクトタスクを生成するローカル関数
        static async Task redirectProcStream(TextReader reader, TextWriter writer, CancellationToken breaker, bool terminate = false)
        {
            await Task.Yield();
            try
            {
                var buffers = new[] { new char[1024], new char[1024], };
                var phase = 0;
                var redirectTask = Task.CompletedTask;
                while (true)
                {
                    // 利用するバッファを選択
                    var buff = buffers[phase];

                    // 次に利用するバッファを切替え
                    phase++;
                    phase %= buffers.Length;

                    // プロセスの出力を読み取り
                    var length = await reader.ReadAsync(buff.AsMemory(), breaker).ConfigureAwait(false);

                    // 前回のリダイレクト書き込みが完了している事を確認
                    await redirectTask.ConfigureAwait(false);

                    // 読み取りデータが無い場合はここで終える
                    if (length <= 0)
                    {
                        if (terminate) writer.Close();
                        break;
                    }

                    // 読み取ったデータをリダイレクト先に書き込み
                    redirectTask = writer.WriteAsync(buff.AsMemory(0, length), breaker);
                }
            }
            catch (OperationCanceledException) { }
        }

        // プロセスの終了時にリダイレクトを中止するための
        using var completeCanceller = new CancellationTokenSource();

        // 指定に応じたリダイレクトタスクを生成
        // 出力はすべて読み取れるようにキャンセルなし。入力はプロセス終了したらもう無意味なので中断させる。
        var stdoutRedirector = stdOut == null ? Task.CompletedTask : redirectProcStream(proc.StandardOutput, stdOut, CancellationToken.None);
        var stderrRedirector = stdErr == null ? Task.CompletedTask : redirectProcStream(proc.StandardError, stdErr, CancellationToken.None);
        var stdinRedirector = stdIn == null ? Task.CompletedTask : redirectProcStream(stdIn, proc.StandardInput, completeCanceller.Token, terminate: true);

        // コマンドの終了を待機
        var killed = false;
        try
        {
            await proc.WaitForExitAsync(cancelToken).ConfigureAwait(false);
        }
        catch (OperationCanceledException ex) when (ex.CancellationToken == cancelToken)
        {
            // キャンセルされたらプロセスをキル
            proc.Kill();
            killed = true;
        }
        finally
        {
            completeCanceller.Cancel();
        }

        // 出力読み取りタスクの完了を待機
        await Task.WhenAll(stdoutRedirector, stderrRedirector, stdinRedirector).ConfigureAwait(false);

        // 実行を中止した場合はそれを示す例外を送出
        if (killed)
        {
            throw new CmdProcCancelException(null, $"The process was killed by cancellation.");
        }

        return new(proc.ExitCode);
    }

    /// <summary>コマンドを実行して終了コードと出力テキストを取得する</summary>
    /// <param name="command">実行コマンド</param>
    /// <param name="argumenter">コマンド引数設定デリゲート</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <param name="workDir">作業ディレクトリ</param>
    /// <param name="environments">環境変数</param>
    /// <param name="readStdOut">標準出力を読み取るか否か</param>
    /// <param name="readStdErr">標準エラーを読み取るか否か</param>
    /// <param name="inputText">標準入力に書き込むテキスト。nullの場合は書き込みなし。</param>
    /// <param name="outEncoding">プロセスの出力テキスト読み取り時のエンコーディング</param>
    /// <param name="inEncoding">プロセスの入力テキストを書き込む際のエンコーディング</param>
    /// <returns>呼び出しプロセスの終了コードと出力テキスト</returns>
    internal static async Task<CmdResult> runAsync(string command, Action<ProcessStartInfo>? argumenter, CancellationToken cancelToken = default, string? workDir = null, IEnumerable<KeyValuePair<string, string?>>? environments = null, bool readStdOut = true, bool readStdErr = true, string? inputText = null, Encoding? outEncoding = null, Encoding? inEncoding = null)
    {
        // リダイレクト先とする文字列のライター
        using var strWriter = new StringWriter();
        using var syncWriter = TextWriter.Synchronized(strWriter);
        var stdOutWriter = readStdOut ? syncWriter : null;
        var stdErrWriter = readStdErr ? syncWriter : null;
        // 入力用の文字列リーダー
        using var stdInReader = inputText == null ? null : new StringReader(inputText);

        try
        {
            // コマンドを実行
            var result = await execAsync(command, argumenter, cancelToken, workDir, environments, stdOutWriter, stdErrWriter, stdInReader, outEncoding, inEncoding).ConfigureAwait(false);

            // 出力テキストを取り出し
            var output = strWriter.ToString();

            return new(result, output);
        }
        catch (CmdProcCancelException cancelEx)
        {
            // キャンセル例外に途中までの出力テキストを乗せる
            throw new CmdProcCancelException(strWriter.ToString(), cancelEx.Message, innerException: cancelEx);
        }
    }

    /// <summary>コマンドを実行して出力テキストを取得する</summary>
    /// <param name="command">実行コマンド</param>
    /// <param name="argumenter">コマンド引数設定デリゲート</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <param name="workDir">作業ディレクトリ</param>
    /// <param name="environments">環境変数</param>
    /// <param name="readStdErr">標準エラーを読み取るか否か</param>
    /// <param name="inputText">標準入力に書き込むテキスト。nullの場合は書き込みなし。</param>
    /// <param name="outEncoding">プロセスの出力テキスト読み取り時のエンコーディング</param>
    /// <param name="inEncoding">プロセスの入力テキストを書き込む際のエンコーディング</param>
    /// <param name="allowExitCodes">正常とみなす終了コード。指定がない場合はゼロのみを正常とする。</param>
    /// <returns>呼び出しプロセスの出力テキスト</returns>
    internal static async Task<string> callAsync(string command, Action<ProcessStartInfo>? argumenter, CancellationToken cancelToken = default, string? workDir = null, IEnumerable<KeyValuePair<string, string?>>? environments = null, bool readStdErr = true, string? inputText = null, Encoding? outEncoding = null, Encoding? inEncoding = null, IEnumerable<int>? allowExitCodes = null)
    {
        // 正常とみなす終了コード
        var validCodes = allowExitCodes ?? new[] { 0, };

        // コマンドを実行
        var result = await runAsync(command, argumenter, cancelToken, workDir, environments, readStdOut: true, readStdErr, inputText, outEncoding, inEncoding).ConfigureAwait(false);

        // 実行完了した場合は終了コードが正常を示すか検証。正常でない場合は例外を送出。
        if (!validCodes.Contains(result.ExitCode))
        {
            throw new CmdProcExitCodeException(result.ExitCode, result.Output, message: $"Faild. ExitCode={result.ExitCode}");
        }

        return result.Output;
    }

    /// <summary>引数リストをもとに設定するコマンド引数設定デリゲートを生成する</summary>
    /// <param name="arguments">引数リスト</param>
    /// <returns>引数設定デリゲート</returns>
    internal static Action<ProcessStartInfo>? listArgumenter(IEnumerable<string>? arguments)
    {
        if (arguments == null) return null;

        return target =>
        {
            foreach (var arg in arguments.CoalesceEmpty())
            {
                target.ArgumentList.Add(arg);
            }
        };
    }
}
