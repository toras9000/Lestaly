namespace Lestaly;

/// <summary>キャンセルトークン関連拡張メソッド</summary>
public static class CancellationTokenSourceExtensions
{
    /// <summary>指定時間でタイムアウトするリンクトークンソースを作成する</summary>
    /// <param name="self">リンク対象のトークン</param>
    /// <param name="timeout">タイムアウト時間</param>
    /// <returns>リンクトークンソース</returns>
    public static CancellationTokenSource CreateLink(this CancellationToken self, TimeSpan timeout)
    {
        var linkSource = CancellationTokenSource.CreateLinkedTokenSource(self);
        linkSource.CancelAfter(timeout);
        return linkSource;
    }

    /// <summary>リンクトークンソースを作成する</summary>
    /// <param name="self">リンク対象のトークン</param>
    /// <param name="token">リンクするトークン</param>
    /// <returns>リンクトークンソース</returns>
    public static CancellationTokenSource CreateLink(this CancellationToken self, CancellationToken token)
        => CancellationTokenSource.CreateLinkedTokenSource(self, token);

    /// <summary>リンクトークンソースを作成する</summary>
    /// <param name="self">リンク対象のトークン</param>
    /// <param name="token1">リンクするトークン1</param>
    /// <param name="token2">リンクするトークン2</param>
    /// <returns>リンクトークンソース</returns>
    public static CancellationTokenSource CreateLink(this CancellationToken self, CancellationToken token1, CancellationToken token2)
        => CancellationTokenSource.CreateLinkedTokenSource(self, token1, token2);
}
