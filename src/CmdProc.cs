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
    /// <param name="encoding">出力テキスト読み取り時のエンコーディング</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>呼び出しプロセスの終了コード</returns>
    public static async Task<int> ExecAsync(string command, IEnumerable<string>? arguments = null, string? workDir = null, IEnumerable<KeyValuePair<string, string?>>? environments = null, TextWriter? stdOutWriter = null, TextWriter? stdErrWriter = null, Encoding? encoding = null, CancellationToken cancelToken = default)
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
            if (encoding != null) target.StandardOutputEncoding = encoding;
        }
        if (stdErrWriter != null)
        {
            target.RedirectStandardError = true;
            if (encoding != null) target.StandardErrorEncoding = encoding;
        }

        // コマンドを実行
        var proc = Process.Start(target) ?? throw new CmdProcException("Cannot execute command.");

        // 出力ストリームのリダイレクトタスクを生成するローカル関数
        async Task redirectProcStream(StreamReader reader, TextWriter writer)
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
                var length = await reader.ReadAsync(buff, 0, buff.Length).ConfigureAwait(false);

                // 前回のリダイレクト書き込みが完了している事を確認
                await redirectTask.ConfigureAwait(false);

                // 読み取りデータが無い場合はここで終える
                if (length <= 0) break;

                // 読み取ったデータをリダイレクト先に書き込み
                redirectTask = writer.WriteAsync(buff.AsMemory(0, length), cancelToken);
            }
        }

        // 指定に応じた出力読み取りタスクを生成
        var stdoutRedirector = stdOutWriter == null ? Task.CompletedTask : redirectProcStream(proc.StandardOutput, stdOutWriter);
        var stderrRedirector = stdErrWriter == null ? Task.CompletedTask : redirectProcStream(proc.StandardError, stdErrWriter);

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

        // 出力読み取りタスクの完了を待機
        await Task.WhenAll(stdoutRedirector, stderrRedirector).ConfigureAwait(false);

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
    /// <param name="encoding">出力テキスト読み取り時のエンコーディング</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>呼び出しプロセスの終了コードと出力テキスト</returns>
    public static async Task<(int ExitCode, string Output)> RunAsync(string command, IEnumerable<string>? arguments = null, string? workDir = null, IEnumerable<KeyValuePair<string, string?>>? environments = null, bool readStdOut = true, bool readStdErr = true, Encoding? encoding = null, CancellationToken cancelToken = default)
    {
        // リダイレクト先のとする文字列のライター
        using var strWriter = new StringWriter();
        using var syncWriter = TextWriter.Synchronized(strWriter);
        var stdOutWriter = readStdOut ? syncWriter : null;
        var stdErrWriter = readStdErr ? syncWriter : null;

        try
        {
            // コマンドを実行
            var result = await CmdProc.ExecAsync(command, arguments, workDir, environments, stdOutWriter, stdErrWriter, encoding, cancelToken).ConfigureAwait(false);

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
    /// <param name="encoding">出力テキスト読み取り時のエンコーディング</param>
    /// <param name="allowExitCodes">正常とみなす終了コード。指定がない場合はゼロのみを正常とする。</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>呼び出しプロセスの出力テキスト</returns>
    public static async Task<string> CallAsync(string command, IEnumerable<string>? arguments = null, string? workDir = null, IEnumerable<KeyValuePair<string, string?>>? environments = null, bool readStdErr = true, Encoding? encoding = null, IEnumerable<int>? allowExitCodes = null, CancellationToken cancelToken = default)
    {
        // 正常とみなす終了コード
        var validCodes = allowExitCodes ?? new[] { 0, };

        // コマンドを実行
        var result = await CmdProc.RunAsync(command, arguments, workDir, environments, readStdOut: true, readStdErr, encoding, cancelToken).ConfigureAwait(false);

        // 実行完了した場合は終了コードが正常を示すか検証。正常でない場合は例外を送出。
        if (!validCodes.Contains(result.ExitCode))
        {
            throw new CmdProcExitCodeException(result.ExitCode, result.Output, message: $"Faild. ExitCode={result.ExitCode}");
        }

        return result.Output;
    }
}
