using System.Runtime.InteropServices;

namespace Lestaly;

/// <summary>POSIXシグナルをハンドルしてキャンセルトークンをシグナルする区間</summary>
public sealed class SignalCancellationPeriod : ICancelTokenPeriod
{
    // 構築
    #region コンストラクタ
    /// <summary>デフォルトコンストラクタ</summary>
    public SignalCancellationPeriod()
        : this([PosixSignal.SIGTERM, PosixSignal.SIGQUIT, PosixSignal.SIGINT, PosixSignal.SIGHUP])
    { }

    /// <summary>ハンドルするシグナル種別を指定するコンストラクタ</summary>
    /// <param name="signals"></param>
    public SignalCancellationPeriod(ReadOnlySpan<PosixSignal> signals)
    {
        this.canceller = new CancellationTokenSource();
        this.handlers = new PosixSignalRegistration[signals.Length];
        try
        {
            for (int i = 0; i < signals.Length; i++)
            {
                this.handlers[i] = PosixSignalRegistration.Create(signals[i], signalHandler);
            }
        }
        catch
        {
            this.Dispose();
            throw;
        }
    }
    #endregion

    // 公開プロパティ
    #region シグナル反応
    /// <summary>キャンセルトークン</summary>
    public CancellationToken Token => this.canceller.Token;

    /// <summary>キャンセル要因のシグナル名</summary>
    public string? Cause { get; private set; }
    #endregion

    // 公開メソッド
    #region 破棄
    /// <summary>インスタンスを破棄する</summary>
    public void Dispose()
    {
        for (int i = 0; i < this.handlers.Length; i++)
        {
            try { this.handlers[i]?.Dispose(); } catch { }
        }
        this.handlers = [];
        this.canceller?.Dispose();
        this.canceller = default!;
    }
    #endregion

    // 非公開フィールド
    #region リソース
    /// <summary>キャンセルトークンソース</summary>
    private CancellationTokenSource canceller;

    /// <summary>シグナルハンドルオブジェクト</summary>
    private PosixSignalRegistration[] handlers;
    #endregion

    // 非公開メソッド
    #region シグナル
    /// <summary>シグナルハンドラ</summary>
    private void signalHandler(PosixSignalContext context)
    {
        context.Cancel = true;
        this.Cause ??= context.Signal.ToString();
        this.canceller.Cancel();
    }
    #endregion
}