using System.Diagnostics;
using System.Text;

namespace Lestaly;

/// <summary>
/// シェルでのコマンド実行クラスの例外
/// </summary>
public class CmdShellException : Exception
{
    /// <summary>デフォルトコンストラクタ</summary>
    public CmdShellException() : base() { }

    /// <summary>エラーメッセージと内部例外を指定するコンストラクタ</summary>
    /// <param name="message">エラーメッセージ</param>
    /// <param name="innerException">内部例外</param>
    public CmdShellException(string? message, Exception? innerException = null) : base(message, innerException) { }
}

/// <summary>
/// シェルでのコマンド実行をキャンセルしてプロセスキルしたことを示す例外
/// </summary>
public class CmdShellCancelException : CmdShellException
{
    /// <summary>内部例外を指定するコンストラクタ</summary>
    /// <param name="message">エラーメッセージ</param>
    /// <param name="innerException">内部例外</param>
    public CmdShellCancelException(string? message = null, Exception? innerException = null) : base(message, innerException) { }
}
