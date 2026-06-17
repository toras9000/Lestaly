using System.Buffers.Binary;
using System.Numerics;
using System.Runtime.CompilerServices;

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
    /// <param name="value">書き込み値</param>
    public void WriteByte(byte value)
    {
        this.probe[0] = value;
        this.probe = this.probe[sizeof(byte)..];
    }

    /// <summary>現在位置からリトルエンディアンのプリミティブ整数値を書き込む</summary>
    /// <remarks>このメソッドの論理的意味はないが、メソッドの名称を他のプリミティブ型と合わせるためだけに定義している。</remarks>
    /// <typeparam name="TValue">書き込み対象型</typeparam>
    /// <param name="value">書き込み値</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteLittleEndian<TValue>(sbyte value) where TValue : struct, IBinaryInteger<sbyte>
        => this.WriteByte((byte)value);

    /// <summary>現在位置からリトルエンディアンのプリミティブ整数値を書き込む</summary>
    /// <typeparam name="TValue">書き込み対象型</typeparam>
    /// <param name="value">書き込み値</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteLittleEndian<TValue>(short value) where TValue : struct, IBinaryInteger<short>
        => this.WriteLittleEndian<ushort>((ushort)value);

    /// <summary>現在位置からリトルエンディアンのプリミティブ整数値を書き込む</summary>
    /// <typeparam name="TValue">書き込み対象型</typeparam>
    /// <param name="value">書き込み値</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteLittleEndian<TValue>(int value) where TValue : struct, IBinaryInteger<int>
        => this.WriteLittleEndian<uint>((uint)value);

    /// <summary>現在位置からリトルエンディアンのプリミティブ整数値を書き込む</summary>
    /// <typeparam name="TValue">書き込み対象型</typeparam>
    /// <param name="value">書き込み値</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteLittleEndian<TValue>(long value) where TValue : struct, IBinaryInteger<long>
        => this.WriteLittleEndian<ulong>((ulong)value);

    /// <summary>現在位置からリトルエンディアンのプリミティブ整数値を書き込む</summary>
    /// <remarks>このメソッドの論理的意味はないが、メソッドの名称を他のプリミティブ型と合わせるためだけに定義している。</remarks>
    /// <typeparam name="TValue">書き込み対象型</typeparam>
    /// <param name="value">書き込み値</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteLittleEndian<TValue>(byte value) where TValue : struct, IBinaryInteger<byte>
        => this.WriteByte(value);

    /// <summary>現在位置からリトルエンディアンのプリミティブ整数値を書き込む</summary>
    /// <typeparam name="TValue">書き込み対象型</typeparam>
    /// <param name="value">書き込み値</param>
    public void WriteLittleEndian<TValue>(ushort value) where TValue : struct, IBinaryInteger<ushort>
    {
        BinaryPrimitives.WriteUInt16LittleEndian(this.probe, value);
        this.probe = this.probe[sizeof(ushort)..];
    }

    /// <summary>現在位置からリトルエンディアンのプリミティブ整数値を書き込む</summary>
    /// <typeparam name="TValue">書き込み対象型</typeparam>
    /// <param name="value">書き込み値</param>
    public void WriteLittleEndian<TValue>(uint value) where TValue : struct, IBinaryInteger<uint>
    {
        BinaryPrimitives.WriteUInt32LittleEndian(this.probe, value);
        this.probe = this.probe[sizeof(uint)..];
    }

    /// <summary>現在位置からリトルエンディアンのプリミティブ整数値を書き込む</summary>
    /// <typeparam name="TValue">書き込み対象型</typeparam>
    /// <param name="value">書き込み値</param>
    public void WriteLittleEndian<TValue>(ulong value) where TValue : struct, IBinaryInteger<ulong>
    {
        BinaryPrimitives.WriteUInt64LittleEndian(this.probe, value);
        this.probe = this.probe[sizeof(ulong)..];
    }

    /// <summary>現在位置からビッグエンディアンのプリミティブ整数値を書き込む</summary>
    /// <remarks>このメソッドの論理的意味はないが、メソッドの名称を他のプリミティブ型と合わせるためだけに定義している。</remarks>
    /// <typeparam name="TValue">書き込み対象型</typeparam>
    /// <param name="value">書き込み値</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteBigEndian<TValue>(sbyte value) where TValue : struct, IBinaryInteger<sbyte>
        => this.WriteByte((byte)value);

    /// <summary>現在位置からビッグエンディアンのプリミティブ整数値を書き込む</summary>
    /// <typeparam name="TValue">書き込み対象型</typeparam>
    /// <param name="value">書き込み値</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteBigEndian<TValue>(short value) where TValue : struct, IBinaryInteger<short>
        => this.WriteBigEndian<ushort>((ushort)value);

    /// <summary>現在位置からビッグエンディアンのプリミティブ整数値を書き込む</summary>
    /// <typeparam name="TValue">書き込み対象型</typeparam>
    /// <param name="value">書き込み値</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteBigEndian<TValue>(int value) where TValue : struct, IBinaryInteger<int>
        => this.WriteBigEndian<uint>((uint)value);

    /// <summary>現在位置からビッグエンディアンのプリミティブ整数値を書き込む</summary>
    /// <typeparam name="TValue">書き込み対象型</typeparam>
    /// <param name="value">書き込み値</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteBigEndian<TValue>(long value) where TValue : struct, IBinaryInteger<long>
        => this.WriteBigEndian<ulong>((ulong)value);

    /// <summary>現在位置からビッグエンディアンのプリミティブ整数値を書き込む</summary>
    /// <remarks>このメソッドの論理的意味はないが、メソッドの名称を他のプリミティブ型と合わせるためだけに定義している。</remarks>
    /// <typeparam name="TValue">書き込み対象型</typeparam>
    /// <param name="value">書き込み値</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteBigEndian<TValue>(byte value) where TValue : struct, IBinaryInteger<byte>
        => this.WriteByte(value);

    /// <summary>現在位置からビッグエンディアンのプリミティブ整数値を書き込む</summary>
    /// <typeparam name="TValue">書き込み対象型</typeparam>
    /// <param name="value">書き込み値</param>
    public void WriteBigEndian<TValue>(ushort value) where TValue : struct, IBinaryInteger<ushort>
    {
        BinaryPrimitives.WriteUInt16BigEndian(this.probe, value);
        this.probe = this.probe[sizeof(ushort)..];
    }

    /// <summary>現在位置からビッグエンディアンのプリミティブ整数値を書き込む</summary>
    /// <typeparam name="TValue">書き込み対象型</typeparam>
    /// <param name="value">書き込み値</param>
    public void WriteBigEndian<TValue>(uint value) where TValue : struct, IBinaryInteger<uint>
    {
        BinaryPrimitives.WriteUInt32BigEndian(this.probe, value);
        this.probe = this.probe[sizeof(uint)..];
    }

    /// <summary>現在位置からビッグエンディアンのプリミティブ整数値を書き込む</summary>
    /// <typeparam name="TValue">書き込み対象型</typeparam>
    /// <param name="value">書き込み値</param>
    public void WriteBigEndian<TValue>(ulong value) where TValue : struct, IBinaryInteger<ulong>
    {
        BinaryPrimitives.WriteUInt64BigEndian(this.probe, value);
        this.probe = this.probe[sizeof(ulong)..];
    }

    /// <summary>現在位置からデータを書き込む</summary>
    /// <param name="data">書き込むデータ列</param>
    public void WriteBytes(ReadOnlySpan<byte> data)
    {
        data.CopyTo(this.probe);
        this.probe = this.probe[data.Length..];
    }
    #endregion

    // 非公開フィールド
    #region 状態情報
    /// <summary>現在位置以降を示すスパン</summary>
    private Span<byte> probe;
    #endregion
}
