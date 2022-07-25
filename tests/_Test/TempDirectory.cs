namespace TestCometFlavor._Test;

/// <summary>
/// 一時ディレクトリを扱うクラス
/// </summary>
public class TempDirectory : IDisposable
{
    // 構築
    #region コンストラクタ
    /// <summary>
    /// デフォルトコンストラクタ
    /// </summary>
    public TempDirectory()
    {
        // システムの一時ディレクトリ取得
        var tempDir = Path.GetTempPath();

        // 一時ディレクトリ配下にGUID名のディレクトリ作成を試みる
        for (var i = 0; i < 3; i++)
        {
            var tempTestDir = Path.Combine(tempDir, Guid.NewGuid().ToString());
            if (!Directory.Exists(tempTestDir))
            {
                this.Info = Directory.CreateDirectory(tempTestDir);
                break;
            }
        }

        // 作れなかったらエラーとする。
        if (this.Info == null)
        {
            throw new InvalidOperationException("Unable to create temporary directory.");
        }
    }
    #endregion

    // 公開プロパティ
    #region 状態情報
    /// <summary>一時ディレクト入りのDirectoryInfo</summary>
    public DirectoryInfo Info { get; }
    #endregion

    // 公開メソッド
    #region 破棄
    /// <summary>
    /// リソース破棄
    /// </summary>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// デストラクタ
    /// </summary>
    ~TempDirectory()
    {
        Dispose(disposing: false);
    }
    #endregion

    // 保護メソッド
    #region 破棄
    /// <summary>
    /// リソース破棄
    /// </summary>
    /// <param name="disposing">マネージリソース破棄過程であるか否か</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            // マネージドリソースの破棄
            if (disposing)
            {
                // TODO: マネージド状態を破棄します (マネージド オブジェクト)
            }

            // アンネージドリソースの破棄
            try { this.Info.Delete(recursive: true); } catch { /* ignore exception */ }

            // 破棄済みフラグ設定
            this.disposed = true;
        }
    }
    #endregion

    // 非公開フィールド
    #region 状態
    /// <summary>破棄済みフラグ</summary>
    private bool disposed;
    #endregion
}
