using System.Diagnostics;
using System.Text;

namespace Lestaly;

/// <summary>
/// シェルでのコマンド実行クラス
/// </summary>
public static class CmdShell
{
    /// <summary>コマンドを実行して終了コードを取得する</summary>
    /// <param name="command">実行コマンド</param>
    /// <param name="arguments">引数リスト</param>
    /// <param name="workDir">作業ディレクトリ</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>呼び出しプロセスの終了コード</returns>
    public static async Task<int> ExecAsync(string command, IEnumerable<string>? arguments = null, string? workDir = null, CancellationToken cancelToken = default)
    {
        // 実行するコマンドの情報を設定
        var target = new ProcessStartInfo();
        target.FileName = command;
        foreach (var arg in arguments.CoalesceEmpty())
        {
            target.ArgumentList.Add(arg);
        }
        if (workDir != null)
        {
            target.WorkingDirectory = workDir;
        }
        target.UseShellExecute = true;

        // コマンドを実行
        var proc = Process.Start(target) ?? throw new CmdShellException("Cannot execute command.");

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

        // 実行を中止した場合はそれを示す例外を送出
        if (killed)
        {
            throw new CmdProcCancelException(null, $"The process was killed by cancellation.");
        }

        return proc.ExitCode;
    }
}
