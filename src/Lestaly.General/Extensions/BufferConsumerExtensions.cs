using System.Numerics;

namespace Lestaly.Extensions;

/// <summary>IBufferConsumer に対する拡張メソッド</summary>
public static class BufferConsumerExtensions
{
    /// <summary>IBufferConsumer{byte} に対する拡張メソッド</summary>
    /// <param name="self">バッファコンシューマ</param>
    extension(IBufferConsumer<byte> self)
    {
        /// <summary>リトルエンディアンのプリミティブ整数値として読み取ってバッファを消費する</summary>
        /// <typeparam name="TValue">読み取り対象型</typeparam>
        /// <returns>読み取った値</returns>
        public TValue ConsumeLittleEndian<TValue>() where TValue : struct, IBinaryInteger<TValue>, IMinMaxValue<TValue>
        {
            var size = default(TValue).GetByteCount();
            var value = TValue.ReadLittleEndian(self.WrittenSpan[..size], 0 <= TValue.Sign(TValue.MinValue));
            self.Consume(size);
            return value;
        }

        /// <summary>ビッグエンディアンのプリミティブ整数値として読み取ってバッファを消費する</summary>
        /// <typeparam name="TValue">読み取り対象型</typeparam>
        /// <returns>読み取った値</returns>
        public TValue ConsumeBigEndian<TValue>() where TValue : struct, IBinaryInteger<TValue>, IMinMaxValue<TValue>
        {
            var size = default(TValue).GetByteCount();
            var value = TValue.ReadBigEndian(self.WrittenSpan[..size], 0 <= TValue.Sign(TValue.MinValue));
            self.Consume(size);
            return value;
        }
    }
}
