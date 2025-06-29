using System.Buffers;
using System.ComponentModel;

namespace Lestaly;

/// <summary>ローカルスコープでプールから配列貸し出しを受けるクラス</summary>
/// <typeparam name="T">要素型</typeparam>
/// <remarks>
/// この構造体はローカルスコープ(スタック上)でプールからの配列貸し出しを管理して自動返却するためのものとなる。
/// 以下のようにusingを利用してスコープを抜ける際に自動的に返却させる利用を想定している。
/// <code>
/// using var array = new RentalArray&lt;byte&gt;(100);
/// // array.Ephemeral を使うコード
/// </code>
/// この構造体はコピーしないでください。
/// </remarks>
public ref struct RentalArray<T>
{
    /// <summary>コンストラクタ</summary>
    /// <param name="minimumLength">借り受ける最小サイズ</param>
    public RentalArray(int minimumLength)
    {
        this.rental = ArrayPool<T>.Shared.Rent(minimumLength);
    }

    /// <summary>貸し出された配列インスタンス</summary>
    /// <remarks>スコープ内(破棄されるまで)に限定して利用すること。</remarks> 
    public readonly T[] Instance => this.rental;

    /// <summary>リソースを破棄する</summary>
    /// <remarks>貸し出しを受けた配列を返却する。</remarks>
    public void Dispose()
    {
        if (this.rental != null)
        {
            ArrayPool<T>.Shared.Return(this.rental);
            this.rental = null!;
        }
    }

    /// <summary>使用不可</summary>
    [Obsolete("Cannot use default constructor", true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public RentalArray()
    {
        throw new NotSupportedException("Cannot use default constructor");
    }

    /// <summary>貸し出された配列インスタンス</summary>
    private T[] rental;
}
