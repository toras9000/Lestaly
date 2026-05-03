namespace Lestaly;

/// <summary>キャンセルトークン関連拡張メソッド</summary>
public static class CancellationTokenSourceExtensions
{
    /// <summary>キャンセルトークン関連拡張メソッド</summary>
    /// <param name="self">リンク対象のトークン</param>
    extension(CancellationToken self)
    {
        /// <summary>指定時間でタイムアウトするリンクトークンソースを作成する</summary>
        /// <param name="timeout">タイムアウト時間</param>
        /// <returns>リンクトークンソース</returns>
        public CancellationTokenSource CreateLink(TimeSpan timeout)
        {
            var linkSource = CancellationTokenSource.CreateLinkedTokenSource(self);
            linkSource.CancelAfter(timeout);
            return linkSource;
        }

        /// <summary>リンクトークンソースを作成する</summary>
        /// <param name="token">リンクするトークン</param>
        /// <returns>リンクトークンソース</returns>
        public CancellationTokenSource CreateLink(CancellationToken token)
            => CancellationTokenSource.CreateLinkedTokenSource(self, token);

        /// <summary>リンクトークンソースを作成する</summary>
        /// <param name="token1">リンクするトークン1</param>
        /// <param name="token2">リンクするトークン2</param>
        /// <returns>リンクトークンソース</returns>
        public CancellationTokenSource CreateLink(CancellationToken token1, CancellationToken token2)
            => CancellationTokenSource.CreateLinkedTokenSource(self, token1, token2);
    }
}
