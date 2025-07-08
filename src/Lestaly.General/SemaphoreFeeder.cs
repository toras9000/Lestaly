namespace Lestaly;

/// <summary>セマフォのカウンタをDisposableオブジェクトを扱うクラス</summary>
public class SemaphoreFeeder : IDisposable
{
    // 構築
    #region コンストラクタ
    /// <summary>指定したパラメータでセマフォを初期化するコンストラクタ</summary>
    /// <param name="initialCount">セマフォの初期カウント値</param>
    public SemaphoreFeeder(int initialCount)
    {
        this.semaphore = new SemaphoreSlim(initialCount);
    }

    /// <summary>指定したパラメータでセマフォを初期化するコンストラクタ</summary>
    /// <param name="initialCount">セマフォの初期カウント値</param>
    /// <param name="maxCount">セマフォの最大カウント値</param>
    public SemaphoreFeeder(int initialCount, int maxCount)
    {
        this.semaphore = new SemaphoreSlim(initialCount, maxCount);
    }
    #endregion

    // 公開メソッド
    #region カウンタ取得
    /// <summary>セマフォカウントの取得を待機し、解放トークンを得る</summary>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>セマフォカウントの解放トークン</returns>
    public async ValueTask<IDisposable> WaitAsync(CancellationToken cancelToken = default)
    {
        return await Token.WaitAsync(this, cancelToken);
    }

    /// <summary>セマフォカウントの取得を待機し、解放トークンを得る</summary>
    /// <param name="timeout">取得タイムアウト時間</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>カウント取得できた場合は解放トークン、取得できなかった場合はnull</returns>
    public async ValueTask<IDisposable?> WaitAsync(TimeSpan timeout, CancellationToken cancelToken = default)
    {
        return await Token.WaitAsync(this, timeout, cancelToken);
    }
    #endregion

    #region 破棄
    /// <summary>リソースを破棄する</summary>
    public void Dispose()
    {
        this.semaphore.Dispose();
    }
    #endregion

    // 非公開型
    #region カウンタトークン
    /// <summary>セマフォカウンタの解放トークンとして機能するDisposable</summary>
    /// <param name="outer">トークン生成元インスタンス</param>
    private class Token(SemaphoreFeeder outer) : IDisposable
    {
        /// <summary>セマフォカウントの取得を待機し、解放トークンを生成する</summary>
        /// <param name="outer">トークン生成元インスタンス</param>
        /// <param name="cancelToken">キャンセルトークン</param>
        /// <returns>セマフォカウントの解放トークン</returns>
        public static async ValueTask<Token> WaitAsync(SemaphoreFeeder outer, CancellationToken cancelToken)
        {
            await outer.semaphore.WaitAsync(cancelToken);
            return new Token(outer);
        }

        /// <summary>セマフォカウントの取得を待機し、解放トークンを生成する</summary>
        /// <param name="outer">トークン生成元インスタンス</param>
        /// <param name="timeout">取得タイムアウト時間</param>
        /// <param name="cancelToken">キャンセルトークン</param>
        /// <returns>カウント取得できた場合は解放トークン、取得できなかった場合はnull</returns>
        public static async ValueTask<Token?> WaitAsync(SemaphoreFeeder outer, TimeSpan timeout, CancellationToken cancelToken)
        {
            var entere = await outer.semaphore.WaitAsync(timeout, cancelToken);
            return entere ? new Token(outer) : default;
        }

        /// <summary>リソースを破棄する</summary>
        public void Dispose()
        {
            if (outer != null)
            {
                outer.semaphore.Release();
                outer = null!;
            }
        }
    }
    #endregion

    // 非公開フィールド
    #region リソース
    /// <summary></summary>
    private readonly SemaphoreSlim semaphore;
    #endregion
}
