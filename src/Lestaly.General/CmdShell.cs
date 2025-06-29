using System.Diagnostics;

namespace Lestaly;

/// <summary>
/// シェルでのコマンド実行クラス
/// </summary>
[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1068:CancellationToken パラメーターは最後に指定する必要があります", Justification = "パラメータ利用頻度的にここでは無視する")]
public static class CmdShell
{
    /// <summary>コマンドを実行して終了コードを取得する</summary>
    /// <param name="command">実行コマンド</param>
    /// <param name="arguments">引数リスト</param>
    /// <param name="workDir">作業ディレクトリ</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>呼び出しプロセスの終了コード</returns>
    public static async Task<CmdExit?> ExecAsync(string command, IEnumerable<string>? arguments = null, CancellationToken cancelToken = default, string? workDir = null)
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
        var proc = Process.Start(target);

        // プロセスが起動されない場合は null が返る
        if (proc == null) return null;

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

        return new(proc.ExitCode);
    }
}
