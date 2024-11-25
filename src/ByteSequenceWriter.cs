using System.Numerics;

namespace Lestaly;

/// <summary>
/// バイトバッファに対して順に書き進める処理
/// </summary>
public ref struct ByteSequenceWriter
{
    // 構築
    #region コンストラクタ
    /// <summary>書き込み対象のバッファを指定するコンストラクタ</summary>
    /// <param name="buffer">バッファ</param>
    public ByteSequenceWriter(Span<byte> buffer)
    {
        this.Buffer = buffer;
        this.probe = buffer;
    }
    #endregion

    // 公開プロパティ
    #region 状態情報
    /// <summary>書き込み対象バッファ</summary>
    public Span<byte> Buffer { get; }

    /// <summary>書き込み済みバイト数</summary>
    public readonly int Written => this.Buffer.Length - this.probe.Length;

    /// <summary>残り(空き)バイト数</summary>
    public readonly int Remaining => this.probe.Length;
    #endregion

    // 公開メソッド
    #region 書き込み
    /// <summary>現在位置からリトルエンディアンのプリミティブ整数値を書き込む</summary>
    /// <typeparam name="TValue">書き込み対象型</typeparam>
    /// <param name="value">書き込み値</param>
    public void WriteLittleEndian<TValue>(TValue value) where TValue : struct, IBinaryInteger<TValue>, IMinMaxValue<TValue>
    {
        var count = value.GetByteCount();
        value.WriteLittleEndian(this.probe[..count]);
        this.probe = this.probe[count..];
    }

    /// <summary>現在位置からビッグエンディアンのプリミティブ整数値を書き込む</summary>
    /// <typeparam name="TValue">書き込み対象型</typeparam>
    /// <param name="value">書き込み値</param>
    public void WriteBigEndian<TValue>(TValue value) where TValue : struct, IBinaryInteger<TValue>, IMinMaxValue<TValue>
    {
        var count = value.GetByteCount();
        value.WriteBigEndian(this.probe[..count]);
        this.probe = this.probe[count..];
    }

    /// <summary>現在位置からデータを書き込む</summary>
    /// <param name="data">書き込むデータ列</param>
    public void WriteData(ReadOnlySpan<byte> data)
    {
        data.CopyTo(this.probe[..data.Length]);
        this.probe = this.probe[data.Length..];
    }
    #endregion

    // 非公開フィールド
    #region 状態情報
    /// <summary>現在位置以降を示すスパン</summary>
    private Span<byte> probe;
    #endregion
}
