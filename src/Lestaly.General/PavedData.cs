namespace Lestaly;

/// <summary>実行補助オプション</summary>
public class PavedOptions<T>
{
    /// <summary>エラー発生時に一時停止するか否か</summary>
    public PavedPauseOptions PauseOnError { get; } = new(true);

    /// <summary>キャンセル発生時に一時停止するか否か</summary>
    public PavedPauseOptions PauseOnCancel { get; } = new(true);

    /// <summary>処理終了後に一時停止するか否か</summary>
    public PavedPauseOptions PauseOnExit { get; } = new(false);

    /// <summary>エラー時ハンドラ</summary>
    public Func<Exception, T>? ErrorHandler { get; set; } = null;

    /// <summary>キャンセル時ハンドラ</summary>
    public Func<Exception, T>? CancelHandler { get; set; } = null;

    #region 設定処理
    /// <summary>一時停止無しに設定する。</summary>
    public PavedOptions<T> NoPause()
    {
        this.PauseOnError.Enabled = false;
        this.PauseOnCancel.Enabled = false;
        this.PauseOnExit.Enabled = false;
        return this;
    }

    /// <summary>エラーによる一時停止ありに設定する。</summary>
    public PavedOptions<T> ErrorPause(string? message = default, TimeSpan time = default)
    {
        this.PauseOnError.Enabled = true;
        this.PauseOnError.Time = time;
        this.PauseOnError.Message = message;
        this.PauseOnCancel.Enabled = false;
        this.PauseOnExit.Enabled = false;
        this.PauseOnExit.Time = time;
        this.PauseOnExit.Message = message;
        return this;
    }

    /// <summary>エラーとキャンセルによる一時停止ありに設定する。</summary>
    public PavedOptions<T> CancelPause(string? message = default, TimeSpan time = default)
    {
        this.PauseOnError.Enabled = true;
        this.PauseOnError.Time = time;
        this.PauseOnError.Message = message;
        this.PauseOnCancel.Enabled = true;
        this.PauseOnCancel.Time = time;
        this.PauseOnCancel.Message = message;
        this.PauseOnExit.Enabled = false;
        this.PauseOnExit.Time = time;
        this.PauseOnExit.Message = message;
        return this;
    }

    /// <summary>いずれかの要因による一時停止ありに設定する。</summary>
    public PavedOptions<T> AnyPause(string? message = default, TimeSpan time = default)
    {
        this.PauseOnError.Enabled = true;
        this.PauseOnError.Time = time;
        this.PauseOnError.Message = message;
        this.PauseOnCancel.Enabled = true;
        this.PauseOnCancel.Time = time;
        this.PauseOnCancel.Message = message;
        this.PauseOnExit.Enabled = true;
        this.PauseOnExit.Time = time;
        this.PauseOnExit.Message = message;
        return this;
    }

    /// <summary>一時停止モードを設定する。</summary>
    public PavedOptions<T> PauseOn(PavedPause mode, string? message = default, TimeSpan time = default) => mode switch
    {
        PavedPause.None => this.NoPause(),
        PavedPause.Any => this.AnyPause(message, time),
        PavedPause.Cancel => this.CancelPause(message, time),
        _ => this.ErrorPause(message, time),
    };
    #endregion
}

/// <summary>一時停止オプション</summary>
public class PavedPauseOptions(bool enabled)
{
    /// <summary>一時停止するか否か</summary>
    public bool Enabled { get; set; } = enabled;

    /// <summary>一時停止時間</summary>
    public TimeSpan Time { get; set; } = TimeSpan.Zero;

    /// <summary>一時停止メッセージ</summary>
    public string? Message { get; set; }
}

/// <summary>一時停止モード識別子</summary>
public enum PavedPause
{
    /// <summary>なし</summary>
    None,
    /// <summary>エラー時に一時停止</summary>
    Error,
    /// <summary>エラーとキャンセル時に一時停止</summary>
    Cancel,
    /// <summary>常に一時停止</summary>
    Any,
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