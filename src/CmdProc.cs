using System.Diagnostics;
using System.Text;

namespace Lestaly;

/// <summary>
/// コマンド実行クラス
/// </summary>
public static class CmdProc
{
    /// <summary>コマンドを実行して出力テキストを取得する</summary>
    /// <param name="command">実行コマンド</param>
    /// <param name="arguments">引数リスト</param>
    /// <param name="workDir">作業ディレクトリ</param>
    /// <param name="environments">環境変数</param>
    /// <param name="stdOutWriter">標準出力のリダイレクト書き込み先ライター。nullの場合はリダイレクトなし。</param>
    /// <param name="stdErrWriter">標準エラーのリダイレクト書き込み先ライター。nullの場合はリダイレクトなし。</param>
    /// <param name="stdInReader">標準入力のリダイレクト読み込み元リーダー。nullの場合はリダイレクトなし。</param>
    /// <param name="outEncoding">プロセスの出力テキスト読み取り時のエンコーディング</param>
    /// <param name="inEncoding">プロセスの入力テキストを書き込む際のエンコーディング</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>呼び出しプロセスの終了コード</returns>
    public static async Task<int> ExecAsync(string command, IEnumerable<string>? arguments = null, string? workDir = null, IEnumerable<KeyValuePair<string, string?>>? environments = null, TextWriter? stdOutWriter = null, TextWriter? stdErrWriter = null, TextReader? stdInReader = null, Encoding? outEncoding = null, Encoding? inEncoding = null, CancellationToken cancelToken = default)
    {
        // 実行するコマンドの情報を設定
        var target = new ProcessStartInfo();
        target.FileName = command;
        foreach (var arg in arguments.CoalesceEmpty())
        {
            target.ArgumentList.Add(arg);
        }
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
        if (stdOutWriter != null)
        {
            target.RedirectStandardOutput = true;
            if (outEncoding != null) target.StandardOutputEncoding = outEncoding;
        }
        if (stdErrWriter != null)
        {
            target.RedirectStandardError = true;
            if (outEncoding != null) target.StandardErrorEncoding = outEncoding;
        }
        if (stdInReader != null)
        {
            target.RedirectStandardInput = true;
            if (inEncoding != null) target.StandardInputEncoding = inEncoding;
        }

        // コマンドを実行
        var proc = Process.Start(target) ?? throw new CmdProcException("Cannot execute command.");

        // 出力ストリームのリダイレクトタスクを生成するローカル関数
        async Task redirectProcStream(TextReader reader, TextWriter writer, CancellationToken breaker)
        {
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
                    if (length <= 0) break;

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
        var stdoutRedirector = stdOutWriter == null ? Task.CompletedTask : redirectProcStream(proc.StandardOutput, stdOutWriter, CancellationToken.None);
        var stderrRedirector = stdErrWriter == null ? Task.CompletedTask : redirectProcStream(proc.StandardError, stdErrWriter, CancellationToken.None);
        var stdinRedirector = stdInReader == null ? Task.CompletedTask : redirectProcStream(stdInReader, proc.StandardInput, completeCanceller.Token);

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

        return proc.ExitCode;
    }

    /// <summary></summary>
    /// <param name="command">実行コマンド</param>
    /// <param name="arguments">引数リスト</param>
    /// <param name="workDir">作業ディレクトリ</param>
    /// <param name="environments">環境変数</param>
    /// <param name="readStdOut">標準出力を読み取るか否か</param>
    /// <param name="readStdErr">標準エラーを読み取るか否か</param>
    /// <param name="inputText">標準入力に書き込むテキスト。nullの場合は書き込みなし。</param>
    /// <param name="outEncoding">プロセスの出力テキスト読み取り時のエンコーディング</param>
    /// <param name="inEncoding">プロセスの入力テキストを書き込む際のエンコーディング</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>呼び出しプロセスの終了コードと出力テキスト</returns>
    public static async Task<(int ExitCode, string Output)> RunAsync(string command, IEnumerable<string>? arguments = null, string? workDir = null, IEnumerable<KeyValuePair<string, string?>>? environments = null, bool readStdOut = true, bool readStdErr = true, string? inputText = null, Encoding? outEncoding = null, Encoding? inEncoding = null, CancellationToken cancelToken = default)
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
            var result = await CmdProc.ExecAsync(command, arguments, workDir, environments, stdOutWriter, stdErrWriter, stdInReader, outEncoding, inEncoding, cancelToken).ConfigureAwait(false);

            // 出力テキストを取り出し
            var output = strWriter.ToString();

            return (result, output);
        }
        catch (CmdProcCancelException cancelEx)
        {
            // キャンセル例外に途中までの出力テキストを乗せる
            throw new CmdProcCancelException(strWriter.ToString(), cancelEx.Message, innerException: cancelEx);
        }
    }

    /// <summary>コマンドを実行して出力テキストを取得する</summary>
    /// <param name="command">実行コマンド</param>
    /// <param name="arguments">引数リスト</param>
    /// <param name="workDir">作業ディレクトリ</param>
    /// <param name="environments">環境変数</param>
    /// <param name="readStdErr">標準エラーを読み取るか否か</param>
    /// <param name="inputText">標準入力に書き込むテキスト。nullの場合は書き込みなし。</param>
    /// <param name="outEncoding">プロセスの出力テキスト読み取り時のエンコーディング</param>
    /// <param name="inEncoding">プロセスの入力テキストを書き込む際のエンコーディング</param>
    /// <param name="allowExitCodes">正常とみなす終了コード。指定がない場合はゼロのみを正常とする。</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>呼び出しプロセスの出力テキスト</returns>
    public static async Task<string> CallAsync(string command, IEnumerable<string>? arguments = null, string? workDir = null, IEnumerable<KeyValuePair<string, string?>>? environments = null, bool readStdErr = true, string? inputText = null, Encoding? outEncoding = null, Encoding? inEncoding = null, IEnumerable<int>? allowExitCodes = null, CancellationToken cancelToken = default)
    {
        // 正常とみなす終了コード
        var validCodes = allowExitCodes ?? new[] { 0, };

        // コマンドを実行
        var result = await CmdProc.RunAsync(command, arguments, workDir, environments, readStdOut: true, readStdErr, inputText, outEncoding, inEncoding, cancelToken).ConfigureAwait(false);

        // 実行完了した場合は終了コードが正常を示すか検証。正常でない場合は例外を送出。
        if (!validCodes.Contains(result.ExitCode))
        {
            throw new CmdProcExitCodeException(result.ExitCode, result.Output, message: $"Faild. ExitCode={result.ExitCode}");
        }

        return result.Output;
    }
}
