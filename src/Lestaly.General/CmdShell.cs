using System.Diagnostics;
using System.Net;

namespace Lestaly;

/// <summary>プロセス実行用の引数</summary>
public readonly struct CmdShellArg
{
    /// <summary>コンストラクタ</summary>
    /// <param name="str">引数値</param>
    public CmdShellArg(string str) => this.Value = str;

    /// <summary>文字列から CmdShellArg への暗黙変換オペレータ</summary>
    /// <param name="str">文字列</param>
    public static implicit operator CmdShellArg(string? str) => new CmdShellArg(str ?? "");

    /// <summary>文字列スパンから CmdShellArg への暗黙変換オペレータ</summary>
    /// <param name="span">文字列スパン</param>
    public static implicit operator CmdShellArg(ReadOnlySpan<char> span) => new CmdShellArg(span.ToString());

    /// <summary>ファイルシステム項目から CmdShellArg への暗黙変換オペレータ</summary>
    /// <param name="info">ファイルシステム項目。フルパスとして評価される</param>
    public static implicit operator CmdShellArg(FileSystemInfo info) => new CmdShellArg(info.FullName);

    /// <summary>URIから CmdShellArg への暗黙変換オペレータ</summary>
    /// <param name="uri">URI。絶対URIとして評価される</param>
    public static implicit operator CmdShellArg(Uri uri) => new CmdShellArg(uri.AbsoluteUri);

    /// <summary>IPアドレスから CmdShellArg への暗黙変換オペレータ</summary>
    /// <param name="ip">IPアドレス</param>
    public static implicit operator CmdShellArg(IPAddress ip) => new CmdShellArg(ip.ToString());

    /// <summary>IPエンドポイントから CmdShellArg への暗黙変換オペレータ</summary>
    /// <param name="ep">IPエンドポイント</param>
    public static implicit operator CmdShellArg(IPEndPoint ep) => new CmdShellArg(ep.ToString());

    /// <summary>GUIDから CmdShellArg への暗黙変換オペレータ</summary>
    /// <param name="id">GUID</param>
    public static implicit operator CmdShellArg(Guid id) => new CmdShellArg(id.ToString());

    /// <summary>数値から CmdShellArg への暗黙変換オペレータ</summary>
    /// <param name="num">数値</param>
    public static implicit operator CmdShellArg(SByte num) => new CmdShellArg(num.ToString());

    /// <summary>数値から CmdShellArg への暗黙変換オペレータ</summary>
    /// <param name="num">数値</param>
    public static implicit operator CmdShellArg(Int16 num) => new CmdShellArg(num.ToString());

    /// <summary>数値から CmdShellArg への暗黙変換オペレータ</summary>
    /// <param name="num">数値</param>
    public static implicit operator CmdShellArg(Int32 num) => new CmdShellArg(num.ToString());

    /// <summary>数値から CmdShellArg への暗黙変換オペレータ</summary>
    /// <param name="num">数値</param>
    public static implicit operator CmdShellArg(Int64 num) => new CmdShellArg(num.ToString());

    /// <summary>数値から CmdShellArg への暗黙変換オペレータ</summary>
    /// <param name="num">数値</param>
    public static implicit operator CmdShellArg(Int128 num) => new CmdShellArg(num.ToString());

    /// <summary>数値から CmdShellArg への暗黙変換オペレータ</summary>
    /// <param name="num">数値</param>
    public static implicit operator CmdShellArg(Byte num) => new CmdShellArg(num.ToString());

    /// <summary>数値から CmdShellArg への暗黙変換オペレータ</summary>
    /// <param name="num">数値</param>
    public static implicit operator CmdShellArg(UInt16 num) => new CmdShellArg(num.ToString());

    /// <summary>数値から CmdShellArg への暗黙変換オペレータ</summary>
    /// <param name="num">数値</param>
    public static implicit operator CmdShellArg(UInt32 num) => new CmdShellArg(num.ToString());

    /// <summary>数値から CmdShellArg への暗黙変換オペレータ</summary>
    /// <param name="num">数値</param>
    public static implicit operator CmdShellArg(UInt64 num) => new CmdShellArg(num.ToString());

    /// <summary>数値から CmdShellArg への暗黙変換オペレータ</summary>
    /// <param name="num">数値</param>
    public static implicit operator CmdShellArg(UInt128 num) => new CmdShellArg(num.ToString());

    /// <summary>数値から CmdShellArg への暗黙変換オペレータ</summary>
    /// <param name="num">数値</param>
    public static implicit operator CmdShellArg(Half num) => new CmdShellArg(num.ToString());

    /// <summary>数値から CmdShellArg への暗黙変換オペレータ</summary>
    /// <param name="num">数値</param>
    public static implicit operator CmdShellArg(Single num) => new CmdShellArg(num.ToString());

    /// <summary>数値から CmdShellArg への暗黙変換オペレータ</summary>
    /// <param name="num">数値</param>
    public static implicit operator CmdShellArg(Double num) => new CmdShellArg(num.ToString());

    /// <summary>数値から CmdShellArg への暗黙変換オペレータ</summary>
    /// <param name="num">数値</param>
    public static implicit operator CmdShellArg(Decimal num) => new CmdShellArg(num.ToString());

    /// <summary>引数値</summary>
    public string Value { get; }
}

/// <summary>
/// シェルでのコマンド実行クラス
/// </summary>
public static class CmdShell
{
    /// <summary>コマンドを実行して終了コードを取得する</summary>
    /// <param name="command">実行コマンド</param>
    /// <param name="arguments">引数リスト</param>
    /// <param name="workDir">作業ディレクトリ</param>
    /// <param name="verb">動詞</param>
    /// <param name="noWindow">ウィンドウ無しフラグ</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>呼び出しプロセスの終了コード</returns>
    public static async Task<CmdExit?> ExecAsync(string command, IEnumerable<CmdShellArg>? arguments = null, string? workDir = null, string? verb = default, bool? noWindow = default, CancellationToken cancelToken = default)
    {
        // 実行するコマンドの情報を設定
        var target = new ProcessStartInfo();
        target.FileName = command;
        foreach (var arg in arguments.CoalesceEmpty())
        {
            target.ArgumentList.Add(arg.Value);
        }
        target.UseShellExecute = true;
        if (workDir != null) target.WorkingDirectory = workDir;
        if (verb != null) target.Verb = verb;
        if (noWindow != null) target.CreateNoWindow = noWindow.Value;

        // コマンドを実行
        using var proc = Process.Start(target);

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
