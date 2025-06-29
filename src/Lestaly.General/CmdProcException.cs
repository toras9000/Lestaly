namespace Lestaly;

/// <summary>
/// コマンド実行クラスの例外
/// </summary>
public class CmdProcException : Exception
{
    /// <summary>デフォルトコンストラクタ</summary>
    public CmdProcException() : base() { }

    /// <summary>エラーメッセージと内部例外を指定するコンストラクタ</summary>
    /// <param name="message">エラーメッセージ</param>
    /// <param name="innerException">内部例外</param>
    public CmdProcException(string? message, Exception? innerException = null) : base(message, innerException) { }
}

/// <summary>
/// コマンド実行結果の終了コードが正常値ではない場合の例外
/// </summary>
public class CmdProcExitCodeException : CmdProcException
{
    /// <summary>終了コードと出力テキストを指定するコンストラクタ</summary>
    /// <param name="exitCode">終了コード</param>
    /// <param name="output">コマンドの出力テキスト</param>
    /// <param name="message">エラーメッセージ</param>
    /// <param name="innerException">内部例外</param>
    public CmdProcExitCodeException(int exitCode, string output, string? message = null, Exception? innerException = null) : base(message, innerException)
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
    /// <summary>内部例外を指定するコンストラクタ</summary>
    /// <param name="output">実行中止するまでのコマンド出力テキスト。</param>
    /// <param name="message">エラーメッセージ</param>
    /// <param name="innerException">内部例外</param>
    public CmdProcCancelException(string? output, string? message = null, Exception? innerException = null) : base(message, innerException)
    {
        this.Output = output;
    }

    /// <summary>実行中止するまでのコマンド出力テキスト。</summary>
    public string? Output { get; }
}
