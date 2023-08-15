namespace Lestaly;

/// <summary>
/// 実行補助オプション
/// </summary>
public class PavedOptions<T>
{
    /// <summary>エラー発生時に一時停止するか否か</summary>
    public bool PauseOnError { get; set; } = true;

    /// <summary>キャンセル発生時に一時停止するか否か</summary>
    public bool PauseOnCancel { get; set; } = true;

    /// <summary>処理終了後に一時停止するか否か</summary>
    public bool PauseOnExit { get; set; } = false;

    /// <summary>一時停止時メッセージ</summary>
    public string? PauseMessage { get; set; } = null;

    /// <summary>一時停止時間[ms]</summary>
    public int PauseTime { get; set; } = Timeout.Infinite;

    /// <summary>エラー時ハンドラ</summary>
    public Func<Exception, T>? ErrorHandler { get; set; } = null;

    /// <summary>コンソール出力I/F</summary>
    public IConsoleWig? Console { get; set; } = null;

    #region 設定処理
    /// <summary>一時停止無しに設定する。</summary>
    public PavedOptions<T> NoPause()
    {
        this.PauseOnError = false;
        this.PauseOnExit = false;
        this.PauseOnCancel = false;
        return this;
    }

    /// <summary>いずれかの要因による一時停止ありに設定する。</summary>
    public PavedOptions<T> AnyPause(int timeout = Timeout.Infinite)
    {
        this.PauseOnError = true;
        this.PauseOnExit = true;
        this.PauseOnCancel = true;
        this.PauseTime = timeout;
        return this;
    }
    #endregion
}

/// <summary>
/// メッセージ種別
/// </summary>
public enum PavedMessageKind
{
    /// <summary>エラー</summary>
    Error,
    /// <summary>警告</summary>
    Warning,
    /// <summary>中止</summary>
    Cancelled,
    /// <summary>情報</summary>
    Information,
}

/// <summary>
/// 実行補助処理で特別に扱うエラーメッセージ伝達用例外
/// </summary>
public class PavedMessageException : Exception
{
    /// <summary>エラーメッセージを指定するコンストラクタ</summary>
    /// <param name="message">エラーメッセージ</param>
    public PavedMessageException(string message) : base(message) { this.Kind = PavedMessageKind.Error; }

    /// <summary>エラーメッセージと内部例外を指定するコンストラクタ</summary>
    /// <param name="message">エラーメッセージ</param>
    /// <param name="innerException">内部例外</param>
    public PavedMessageException(string message, Exception innerException) : base(message, innerException) { this.Kind = PavedMessageKind.Error; }

    /// <summary>エラーメッセージと重大度を指定するコンストラクタ</summary>
    /// <param name="message">エラーメッセージ</param>
    /// <param name="fatal">致命的なエラーであるか否か。Pavedでのエラーメッセージ色を決定するために使用。</param>
    public PavedMessageException(string message, bool fatal) : base(message) { this.Kind = fatal ? PavedMessageKind.Error : PavedMessageKind.Warning; }

    /// <summary>エラーメッセージとメッセージ種別を指定するコンストラクタ</summary>
    /// <param name="message">エラーメッセージ</param>
    /// <param name="kind">メッセージの種類。</param>
    public PavedMessageException(string message, PavedMessageKind kind) : base(message) { this.Kind = kind; }

    /// <summary>メッセージの種類</summary>
    public PavedMessageKind Kind { get; }
}

/// <summary>
/// 実行補助処理で特別に扱うエラーメッセージ/終了コード伝達用例外
/// </summary>
public class PavedExitException : PavedMessageException
{
    /// <summary>エラーメッセージとメッセージ種別および終了コードを指定するコンストラクタ</summary>
    /// <param name="message">エラーメッセージ</param>
    /// <param name="kind">メッセージの種類。</param>
    /// <param name="exitCode">終了コード。</param>
    public PavedExitException(string message, PavedMessageKind kind, int exitCode = 255) : base(message, kind) { this.ExitCode = exitCode; }

    /// <summary>終了コード</summary>
    public int ExitCode { get; }
}