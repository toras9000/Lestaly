namespace Lestaly;

/// <summary>キャンセルトークンを保持する区間</summary>
public interface ICancelTokenPeriod : IDisposable
{
    /// <summary>キャンセルトークン</summary>
    CancellationToken Token { get; }
}