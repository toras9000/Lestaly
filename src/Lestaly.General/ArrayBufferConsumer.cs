namespace Lestaly;

/// <summary>消費可能な ArrayBufferWriter </summary>
/// <typeparam name="T">要素型</typeparam>
public sealed class ArrayBufferConsumer<T> : IBufferConsumer<T>
{
    // 構築
    #region コンストラクタ
    /// <summary>初期キャパシティを指定するコンストラクタ</summary>
    /// <param name="capacity">初期キャパシティ</param>
    public ArrayBufferConsumer(int capacity = 1024)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(capacity);

        this.buffer = new T[capacity];
        this.written = 0;
        this.consumed = 0;
    }
    #endregion

    // 公開プロパティ
    #region バッファ状態
    /// <summary>現在のトータルキャパシティサイズ</summary>
    public int Capacity => this.buffer.Length;

    /// <summary>現在の空きキャパシティサイズ</summary>
    public int FreeCapacity => this.buffer.Length - this.written;

    /// <summary>書き込み済みサイズ</summary>
    public int WrittenCount => this.written - this.consumed;
    #endregion

    #region バッファ状態
    /// <summary>書き込み済みデータMemory</summary>
    public ReadOnlyMemory<T> WrittenMemory => new(this.buffer, this.consumed, this.written - this.consumed);

    /// <summary>書き込み済みデータSpan</summary>
    public ReadOnlySpan<T> WrittenSpan => new(this.buffer, this.consumed, this.written - this.consumed);
    #endregion

    // 公開メソッド
    #region バッファアクセス
    /// <summary>書き込み用 Memory を取得する</summary>
    /// <param name="sizeHint">必要とする最小長のヒント</param>
    /// <returns>書き込み用 Memory</returns>
    public Memory<T> GetMemory(int sizeHint = 0)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(sizeHint);

        this.enoughBuffer(sizeHint);
        return this.buffer.AsMemory(this.written);
    }

    /// <summary>書き込み用 Span を取得する</summary>
    /// <param name="sizeHint">必要とする最小長のヒント</param>
    /// <returns>書き込み用 Span</returns>
    public Span<T> GetSpan(int sizeHint = 0)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(sizeHint);

        this.enoughBuffer(sizeHint);
        return this.buffer.AsSpan(this.written);
    }
    #endregion

    #region データ管理
    /// <summary>書き込みデータを確定する</summary>
    /// <param name="count">確定するサイズ</param>
    public void Advance(int count)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(count);

        var newWritten = checked(this.written + count);
        if (this.buffer.Length < newWritten) throw new InvalidOperationException();

        this.written = newWritten;
    }

    /// <summary>書き込み済みデータを消費する</summary>
    /// <param name="count">消費するサイズ</param>
    public void Consume(int count)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(count);

        var newConsume = checked(this.consumed + count);
        if (this.written < newConsume) throw new InvalidOperationException();

        this.consumed = newConsume;

        if (this.written == this.consumed)
        {
            this.ResetWrittenCount();
        }
    }

    /// <summary>書き込みデータをクリアする</summary>
    public void Clear()
    {
        this.buffer.AsSpan().Clear();
        this.ResetWrittenCount();
    }

    /// <summary>書き込みサイズをクリアする</summary>
    public void ResetWrittenCount()
    {
        this.written = 0;
        this.consumed = 0;
    }
    #endregion

    // 非公開フィールド
    #region バッファ管理
    /// <summary>現在のバッファ</summary>
    private T[] buffer;

    /// <summary>書き込み済みサイズ(消費済みを含む)</summary>
    private int written;

    /// <summary>消費済みサイズ</summary>
    private int consumed;
    #endregion

    // 非公開メンバ
    #region バッファ管理
    /// <summary>書き込み可能領域を確保できるようなバッファを準備する</summary>
    /// <param name="sizeHint">必要とする最小長のヒント</param>
    private void enoughBuffer(int sizeHint)
    {
        // 少なくとも空きを1バイト以上必要とするよう補正
        var needSize = Math.Max(1, sizeHint);

        // 空きサイズが足りているかを判定
        var freeSize = this.buffer.Length - this.written;
        if (needSize <= freeSize) return;

        // 消費済み分を考慮すると足りるのかを判定
        var spaceSize = freeSize + this.consumed;
        var validSize = this.written - this.consumed;
        if (needSize <= spaceSize)
        {
            // 消費領域分、前に詰める
            Array.Copy(
                sourceArray: this.buffer,
                sourceIndex: this.consumed,
                destinationArray: this.buffer,
                destinationIndex: 0,
                length: validSize
            );
        }
        else
        {
            // 詰めても収まらない場合はバッファ再確保
            var needNewSize = checked(this.buffer.Length - spaceSize + needSize);
            var doubleSize = (int.MaxValue / 2) < this.buffer.Length ? int.MaxValue : this.buffer.Length * 2;
            var newSize = Math.Max(needNewSize, doubleSize);
            var newArray = GC.AllocateUninitializedArray<T>(newSize);
            Array.Copy(
                sourceArray: this.buffer,
                sourceIndex: this.consumed,
                destinationArray: newArray,
                destinationIndex: 0,
                length: validSize
            );
            newArray.AsSpan(validSize).Clear();
            this.buffer = newArray;
        }

        // 有効領域情報更新
        this.written -= this.consumed;
        this.consumed = 0;
    }
    #endregion
}
