using System.Buffers;
using System.Numerics;

namespace Lestaly;

/// <summary>IBufferWriter に対する拡張メソッド</summary>
public static class BufferWriterExtensions
{
    /// <summary>IBufferWriter{byte} に対する拡張メソッド</summary>
    /// <param name="self">バッファライタ</param>
    extension(IBufferWriter<byte> self)
    {
        /// <summary>プリミティブ整数値をリトルエンディアンで書き込む</summary>
        /// <typeparam name="TValue">書き込み対象型</typeparam>
        /// <param name="value">書き込む値</param>
        public void WriteLittleEndian<TValue>(TValue value) where TValue : struct, IBinaryInteger<TValue>, IMinMaxValue<TValue>
        {
            var size = value.GetByteCount();
            var buffer = self.GetSpan(size);
            var written = value.WriteLittleEndian(buffer);
            self.Advance(written);
        }

        /// <summary>プリミティブ整数値をビッグエンディアンで書き込む</summary>
        /// <typeparam name="TValue">書き込み対象型</typeparam>
        /// <param name="value">書き込む値</param>
        public void WriteBigEndian<TValue>(TValue value) where TValue : struct, IBinaryInteger<TValue>, IMinMaxValue<TValue>
        {
            var size = value.GetByteCount();
            var buffer = self.GetSpan(size);
            var written = value.WriteBigEndian(buffer);
            self.Advance(written);
        }
    }
}
