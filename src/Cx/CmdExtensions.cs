using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Lestaly.Cx;

/// <summary>
/// コマンドをシンプルに実行するための拡張メソッド
/// </summary>
public static class CmdExtensions
{
    /// <summary>文字列をコマンドラインとみなしてプロセスを実行する</summary>
    /// <remarks>
    /// このメソッドは ProcessX.Zx にインスパイアされたものとなる。
    /// ただし互換性はない。google/zx は一切意識せず、ディレクトリ変更などもサポートしない。
    /// また、標準エラーに出力がある場合をエラーとは扱わない。
    /// 実行するプロセスの終了コードが 0 の場合のみを成功とみなし、それ以外のコード値はエラー(例外送出)とする。
    /// </remarks>
    /// <param name="commandline">コマンドライン文字列。最初の空白より後ろを引数とみなす。</param>
    /// <returns>コマンドの標準出力と標準エラーのテキストを得るAwaiter。出力テキストは正常終了コードの場合のみ得られる。</returns>
    public static TaskAwaiter<string> GetAwaiter(this string commandline)
        => execAsync(commandline).success().output().GetAwaiter();

    /// <summary>文字列をコマンドラインとみなしてプロセスを実行する</summary>
    /// <param name="commandline">コマンドライン文字列。最初の空白より後ろを引数とみなす。</param>
    /// <returns>コマンドの出力と終了コードを得るタスク。終了コードは検証されない。</returns>
    public static Task<CmdResult> result(this string commandline)
        => execAsync(commandline);

    /// <summary>文字列をコマンドとみなして指定の引数でプロセスを実行する</summary>
    /// <param name="command">コマンド文字列。全体を実行コマンドとみなす。</param>
    /// <param name="arguments">引数リスト</param>
    /// <returns>コマンドの出力と終了コードを得るタスク。終了コードは検証されない。</returns>
    public static Task<CmdResult> args(this string command, params string[] arguments)
        => execAsync(command, CmdProc.listArgumenter(arguments));

    /// <summary>文字列をコマンドラインとみなしてプロセスを実行し、指定の終了コードを正常とする検証を行う</summary>
    /// <param name="commandline">コマンドライン文字列。最初の空白より後ろを引数とみなす。</param>
    /// <param name="codes">正常とみなす終了コード。指定しない場合はゼロのみを正常とみなす。正常以外の終了コードの場合は例外を送出する。</param>
    /// <returns>コマンドの出力と終了コードを得るタスク。正常終了コードの場合のみ結果を得られる。</returns>
    public static Task<CmdResult> success(this string commandline, params int[] codes)
        => execAsync(commandline).success(codes);

    /// <summary>コマンド実行結果に対し、指定の終了コードを正常とする検証を行う。</summary>
    /// <param name="self">コマンド実行結果を得るタスク</param>
    /// <param name="codes">正常とみなす終了コード。指定しない場合はゼロのみを正常とみなす。正常以外の終了コードの場合は例外を送出する。</param>
    /// <returns>コマンドの出力と終了コードを得るタスク。正常終了コードの場合のみ結果を得られる。</returns>
    public static Task<CmdResult> success(this Task<CmdResult> self, params int[] codes)
        => self.AsSuccessCode(codes.Length == 0 ? null : codes);

    /// <summary>コマンド実行結果から終了コードを取得する。</summary>
    /// <param name="self">コマンド実行結果を得るタスク</param>
    /// <returns>終了コードを得るタスク。終了コードは検証されない。</returns>
    public static async Task<int> code(this Task<CmdResult> self)
        => (await self.ConfigureAwait(false)).ExitCode;

    /// <summary>コマンド実行結果から出力テキストを取得する。</summary>
    /// <param name="self">コマンド実行結果を得るタスク</param>
    /// <returns>終出力テキストを得るタスク。終了コードは検証されない。</returns>
    public static async Task<string> output(this Task<CmdResult> self)
        => (await self.ConfigureAwait(false)).Output;

    /// <summary>コマンドラインを必要であれば分解してプロセス実行する</summary>
    /// <param name="commandline">コマンドライン文字列。最初の空白より後ろを引数とみなす。</param>
    /// <returns>コマンドの出力と終了コードを得るタスク。</returns>
    private static Task<CmdResult> execAsync(string commandline)
    {
        // コマンド引数があるかチェック
        var executer = default(Task<CmdResult>);
        var sepaIdx = commandline.IndexOf(' ');
        if (sepaIdx < 0)
        {
            // 引数が無ければそのまま実行
            executer = execAsync(commandline, null);
        }
        else
        {
            // 引数があれば分割して引数として呼び出し
            var command = commandline[..sepaIdx];
            var arguments = commandline[(sepaIdx + 1)..];
            executer = execAsync(command, t => t.Arguments = arguments);
        }

        return executer;
    }

    /// <summary>コマンドと引数によってプロセスを実行する</summary>
    /// <param name="command">コマンド文字列。全体を実行コマンドとみなす。</param>
    /// <param name="argumenter">引数の設定デリゲート</param>
    /// <returns>コマンドの出力と終了コードを得るタスク。</returns>
    private static async Task<CmdResult> execAsync(string command, Action<ProcessStartInfo>? argumenter = null)
    {
        // 出力を格納する文字列ライタ
        using var strWriter = new StringWriter();
        using var syncWriter = TextWriter.Synchronized(strWriter);

        // コンソールと文字列への出力をまとめるライタ
        using var stdOutWriter = new TeeWriter().Bind(Console.Out, syncWriter);
        using var stdErrWriter = new TeeWriter().Bind(Console.Error, syncWriter);

        // コマンド引数があるかチェック
        var exit = await CmdProc.execAsync(command, argumenter, stdOut: stdOutWriter, stdErr: stdErrWriter).ConfigureAwait(false);

        return new(exit.Code, strWriter.ToString());
    }
}