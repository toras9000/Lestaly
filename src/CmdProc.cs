using System.Diagnostics;
using System.Text;

namespace Lestaly;


/// <summary>
/// コマンド実行クラスの例外
/// </summary>
public class CmdProcException : Exception
{
    /// <summary>デフォルトコンストラクタ</summary>
    public CmdProcException() : base() { }

    /// <summary>エラーメッセージを指定するコンストラクタ</summary>
    /// <param name="message">エラーメッセージ</param>
    public CmdProcException(string message) : base(message) { }

    /// <summary>エラーメッセージと内部例外を指定するコンストラクタ</summary>
    /// <param name="message">エラーメッセージ</param>
    /// <param name="innerException">内部例外</param>
    public CmdProcException(string message, Exception innerException) : base(message, innerException) { }
}

/// <summary>
/// コマンド実行結果の終了コードが正常値ではない場合の例外
/// </summary>
public class CmdProcExitCodeException : CmdProcException
{
    /// <summary>終了コードと出力テキストを指定するコンストラクタ</summary>
    /// <param name="exitCode">終了コード</param>
    /// <param name="output">コマンドの出力テキスト</param>
    public CmdProcExitCodeException(int exitCode, string output) : base()
    {
        this.ExitCode = exitCode;
        this.Output = output;
    }

    /// <summary>終了コード</summary>
    public int ExitCode { get; }

    /// <summary>コマンド出力テキスト</summary>
    public string Output { get; }
}

/// <summary>
/// コマンド実行をキャンセルしてプロセスキルしたことを示す例外
/// </summary>
public class CmdProcCancelException : CmdProcException
{
    /// <summary>出力テキストを指定するコンストラクタ</summary>
    /// <param name="output">実行中止するまでのコマンド出力テキスト。</param>
    public CmdProcCancelException(string output) : base()
    {
        this.Output = output;
    }

    /// <summary>コマンド出力テキスト</summary>
    public string Output { get; }
}

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
    /// <param name="readStdOut">標準出力を読み取るか否か</param>
    /// <param name="readStdErr">標準エラーを読み取るか否か</param>
    /// <param name="encoding">出力テキスト読み取り時のエンコーディング</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>出力テキストを得るタスク</returns>
    public static async Task<(int ExitCode, string Output)> CallAsync(string command, IEnumerable<string>? arguments = null, string? workDir = null, IEnumerable<KeyValuePair<string, string?>>? environments = null, bool readStdOut = true, bool readStdErr = true, Encoding? encoding = null, CancellationToken cancelToken = default)
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
        target.RedirectStandardOutput = readStdOut;
        target.RedirectStandardError = readStdErr;
        if (encoding != null)
        {
            target.StandardOutputEncoding = encoding;
            target.StandardErrorEncoding = encoding;
        }

        // コマンドを実行
        var proc = Process.Start(target) ?? throw new CmdProcException("Cannot execute command.");

        // 出力テキストを蓄積するバッファ
        var outputs = new StringBuilder();

        // 出力ストリームを読み取りタスクを生成するローカル関数
        async Task readProcStream(StreamReader reader)
        {
            var buffer = new char[1024];
            while (true)
            {
                var length = await reader.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false);
                if (length <= 0) break;

                lock (outputs)
                {
                    outputs.Append(buffer, 0, length);
                }
            }
        }

        // 指定に応じた出力読み取りタスクを生成
        var stdoutReader = readStdOut ? readProcStream(proc.StandardOutput) : Task.CompletedTask;
        var stderrReader = readStdErr ? readProcStream(proc.StandardError) : Task.CompletedTask;

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
        await Task.WhenAll(stdoutReader, stderrReader).ConfigureAwait(false);

        // 出力テキストを単一文字列化
        var outputText = outputs.ToString();

        // 実行を中止した場合はそれを示す例外を送出
        if (killed)
        {
            throw new CmdProcCancelException(outputText);
        }

        return (proc.ExitCode, outputText);
    }

    /// <summary>コマンドを実行して出力テキストを取得する</summary>
    /// <param name="command">実行コマンド</param>
    /// <param name="arguments">引数リスト</param>
    /// <param name="workDir">作業ディレクトリ</param>
    /// <param name="environments">環境変数</param>
    /// <param name="readStdOut">標準出力を読み取るか否か</param>
    /// <param name="readStdErr">標準エラーを読み取るか否か</param>
    /// <param name="encoding">出力テキスト読み取り時のエンコーディング</param>
    /// <param name="allowExitCodes">正常とみなす終了コード。指定がない場合はゼロのみを正常とする。</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>出力テキストを得るタスク</returns>
    public static async Task<string> ExecAsync(string command, IEnumerable<string>? arguments = null, string? workDir = null, IEnumerable<KeyValuePair<string, string?>>? environments = null, bool readStdOut = true, bool readStdErr = true, Encoding? encoding = null, IEnumerable<int>? allowExitCodes = null, CancellationToken cancelToken = default)
    {
        // 正常とみなす終了コード
        var validCodes = allowExitCodes?.ToHashSet() ?? new() { 0, };

        // コマンドを実行
        var result = await CmdProc.CallAsync(command, arguments, workDir, environments, readStdOut, readStdErr, encoding, cancelToken).ConfigureAwait(false);

        // 実行完了した場合は終了コードが正常を示すか検証。正常でない場合は例外を送出。
        if (!validCodes.Contains(result.ExitCode))
        {
            throw new CmdProcExitCodeException(result.ExitCode, result.Output);
        }

        return result.Output;
    }
}
