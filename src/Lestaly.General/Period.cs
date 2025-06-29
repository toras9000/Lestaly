namespace Lestaly;

/// <summary>
/// なんらかの区間で何らかの管理をする目的のクラス
/// </summary>
public class Period : IDisposable
{
    // 構築
    #region コンストラクタ
    /// <summary>区間終了時処理を指定するコンストラクタ</summary>
    /// <param name="leaveAction">区間終了時処理</param>
    public Period(Action leaveAction)
    {
        this.disposer = leaveAction;
    }
    #endregion

    // 公開プロパティ
    #region 状態情報
    /// <summary>有効区間内であるか否か</summary>
    public bool Available => !this.disposed;
    #endregion

    // 公開メソッド
    #region 破棄
    /// <summary>区間を終了する。</summary>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
    #endregion

    // 保護メソッド
    #region 破棄
    /// <summary>区間を終了する</summary>
    /// <param name="disposing">マネージ破棄過程であるか否か</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                try { this.disposer?.Invoke(); } catch { }
                this.disposer = null;
            }

            this.disposed = true;
        }
    }
    #endregion

    // 非公開フィールド
    #region 区間管理
    /// <summary>破棄済みフラグ</summary>
    private bool disposed;

    /// <summary>区間終了時処理</summary>
    private Action? disposer;
    #endregion
}
