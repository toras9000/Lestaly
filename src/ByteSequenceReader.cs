using System.Numerics;

namespace Lestaly;

/// <summary>
/// バイト列を順に読み進める処理
/// </summary>
public ref struct ByteSequenceReader
{
    // 構築
    #region コンストラクタ
    /// <summary>読み取り対象のバイト列を指定するコンストラクタ</summary>
    /// <param name="source">バイト列</param>
    public ByteSequenceReader(ReadOnlySpan<byte> source)
    {
        this.Source = source;
        this.probe = source;
    }
    #endregion

    // 公開プロパティ
    #region 状態情報
    /// <summary>読み取り対象バイト列</summary>
    public ReadOnlySpan<byte> Source { get; }

    /// <summary>読み取り済みバイト数</summary>
    public int Consumed => this.Source.Length - this.probe.Length;

    /// <summary>残り(未読み取り)バイト数</summary>
    public int Remaining => this.probe.Length;
    #endregion

    // 公開メソッド
    #region 読み取り
    /// <summary>現在位置からリトルエンディアンのプリミティブ整数値として読み取る</summary>
    /// <typeparam name="TValue">読み取り対象型</typeparam>
    /// <returns>読み取った値</returns>
    public TValue ReadLittleEndian<TValue>() where TValue : struct, IBinaryInteger<TValue>, IMinMaxValue<TValue>
    {
        var count = default(TValue).GetByteCount();
        var value = TValue.ReadLittleEndian(this.probe[..count], 0 <= TValue.Sign(TValue.MinValue));
        this.probe = this.probe[count..];
        return value;
    }

    /// <summary>現在位置からビッグエンディアンのプリミティブ整数値として読み取る</summary>
    /// <typeparam name="TValue">読み取り対象型</typeparam>
    /// <returns>読み取った値</returns>
    public TValue ReadBigEndian<TValue>() where TValue : struct, IBinaryInteger<TValue>, IMinMaxValue<TValue>
    {
        var count = default(TValue).GetByteCount();
        var value = TValue.ReadBigEndian(this.probe[..count], 0 <= TValue.Sign(TValue.MinValue));
        this.probe = this.probe[count..];
        return value;
    }

    /// <summary>現在位置から指定サイズを読み取る</summary>
    /// <returns>読み取り範囲を示すスパン</returns>
    public ReadOnlySpan<byte> ReadLength(int length)
    {
        var span = this.probe[..length];
        this.probe = this.probe[length..];
        return span;
    }
    #endregion

    // 非公開フィールド
    #region 状態情報
    /// <summary>現在位置以降を示すスパン</summary>
    private ReadOnlySpan<byte> probe;
    #endregion
}
