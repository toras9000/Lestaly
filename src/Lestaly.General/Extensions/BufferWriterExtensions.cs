using System.Buffers;
using System.Buffers.Binary;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Lestaly;

/// <summary>IBufferWriter に対する拡張メソッド</summary>
public static class BufferWriterExtensions
{
    /// <summary>IBufferWriter{byte} に対する拡張メソッド</summary>
    /// <param name="self">バッファライタ</param>
    extension(IBufferWriter<byte> self)
    {
        /// <summary>プリミティブ整数値をリトルエンディアンで書き込む</summary>
        /// <param name="value">書き込む値</param>
        public void WriteByte(byte value)
        {
            var buffer = self.GetSpan(1);
            buffer[0] = value;
            self.Advance(1);
        }

        /// <summary>プリミティブ整数値をリトルエンディアンで書き込む</summary>
        /// <remarks>このメソッドの論理的意味はないが、メソッドの名称を他のプリミティブ型と合わせるためだけに定義している。</remarks>
        /// <typeparam name="TValue">書き込み対象型</typeparam>
        /// <param name="value">書き込む値</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteLittleEndian<TValue>(sbyte value) where TValue : struct, IBinaryInteger<sbyte>
            => self.WriteByte((byte)value);

        /// <summary>プリミティブ整数値をリトルエンディアンで書き込む</summary>
        /// <typeparam name="TValue">書き込み対象型</typeparam>
        /// <param name="value">書き込む値</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteLittleEndian<TValue>(short value) where TValue : struct, IBinaryInteger<short>
            => self.WriteLittleEndian<ushort>((ushort)value);

        /// <summary>プリミティブ整数値をリトルエンディアンで書き込む</summary>
        /// <typeparam name="TValue">書き込み対象型</typeparam>
        /// <param name="value">書き込む値</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteLittleEndian<TValue>(int value) where TValue : struct, IBinaryInteger<int>
            => self.WriteLittleEndian<uint>((uint)value);

        /// <summary>プリミティブ整数値をリトルエンディアンで書き込む</summary>
        /// <typeparam name="TValue">書き込み対象型</typeparam>
        /// <param name="value">書き込む値</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteLittleEndian<TValue>(long value) where TValue : struct, IBinaryInteger<long>
            => self.WriteLittleEndian<ulong>((ulong)value);

        /// <summary>プリミティブ整数値をリトルエンディアンで書き込む</summary>
        /// <remarks>このメソッドの論理的意味はないが、メソッドの名称を他のプリミティブ型と合わせるためだけに定義している。</remarks>
        /// <typeparam name="TValue">書き込み対象型</typeparam>
        /// <param name="value">書き込む値</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteLittleEndian<TValue>(byte value) where TValue : struct, IBinaryInteger<byte>
            => self.WriteByte(value);

        /// <summary>プリミティブ整数値をリトルエンディアンで書き込む</summary>
        /// <typeparam name="TValue">書き込み対象型</typeparam>
        /// <param name="value">書き込む値</param>
        public void WriteLittleEndian<TValue>(ushort value) where TValue : struct, IBinaryInteger<ushort>
        {
            const int size = sizeof(ushort);
            var buffer = self.GetSpan(size);
            BinaryPrimitives.WriteUInt16LittleEndian(buffer, value);
            self.Advance(size);
        }

        /// <summary>プリミティブ整数値をリトルエンディアンで書き込む</summary>
        /// <typeparam name="TValue">書き込み対象型</typeparam>
        /// <param name="value">書き込む値</param>
        public void WriteLittleEndian<TValue>(uint value) where TValue : struct, IBinaryInteger<uint>
        {
            const int size = sizeof(uint);
            var buffer = self.GetSpan(size);
            BinaryPrimitives.WriteUInt32LittleEndian(buffer, value);
            self.Advance(size);
        }

        /// <summary>プリミティブ整数値をリトルエンディアンで書き込む</summary>
        /// <typeparam name="TValue">書き込み対象型</typeparam>
        /// <param name="value">書き込む値</param>
        public void WriteLittleEndian<TValue>(ulong value) where TValue : struct, IBinaryInteger<ulong>
        {
            const int size = sizeof(ulong);
            var buffer = self.GetSpan(size);
            BinaryPrimitives.WriteUInt64LittleEndian(buffer, value);
            self.Advance(size);
        }

        /// <summary>プリミティブ整数値をビッグエンディアンで書き込む</summary>
        /// <remarks>このメソッドの論理的意味はないが、メソッドの名称を他のプリミティブ型と合わせるためだけに定義している。</remarks>
        /// <typeparam name="TValue">書き込み対象型</typeparam>
        /// <param name="value">書き込む値</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteBigEndian<TValue>(byte value) where TValue : struct, IBinaryInteger<byte>
            => self.WriteByte(value);

        /// <summary>プリミティブ整数値をビッグエンディアンで書き込む</summary>
        /// <typeparam name="TValue">書き込み対象型</typeparam>
        /// <param name="value">書き込む値</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteBigEndian<TValue>(short value) where TValue : struct, IBinaryInteger<short>
            => self.WriteBigEndian<ushort>((ushort)value);

        /// <summary>プリミティブ整数値をビッグエンディアンで書き込む</summary>
        /// <typeparam name="TValue">書き込み対象型</typeparam>
        /// <param name="value">書き込む値</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteBigEndian<TValue>(int value) where TValue : struct, IBinaryInteger<int>
            => self.WriteBigEndian<uint>((uint)value);

        /// <summary>プリミティブ整数値をビッグエンディアンで書き込む</summary>
        /// <typeparam name="TValue">書き込み対象型</typeparam>
        /// <param name="value">書き込む値</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteBigEndian<TValue>(long value) where TValue : struct, IBinaryInteger<long>
            => self.WriteBigEndian<ulong>((ulong)value);

        /// <summary>プリミティブ整数値をビッグエンディアンで書き込む</summary>
        /// <typeparam name="TValue">書き込み対象型</typeparam>
        /// <param name="value">書き込む値</param>
        public void WriteBigEndian<TValue>(ushort value) where TValue : struct, IBinaryInteger<ushort>
        {
            const int size = sizeof(ushort);
            var buffer = self.GetSpan(size);
            BinaryPrimitives.WriteUInt16BigEndian(buffer, value);
            self.Advance(size);
        }

        /// <summary>プリミティブ整数値をビッグエンディアンで書き込む</summary>
        /// <typeparam name="TValue">書き込み対象型</typeparam>
        /// <param name="value">書き込む値</param>
        public void WriteBigEndian<TValue>(uint value) where TValue : struct, IBinaryInteger<uint>
        {
            const int size = sizeof(uint);
            var buffer = self.GetSpan(size);
            BinaryPrimitives.WriteUInt32BigEndian(buffer, value);
            self.Advance(size);
        }

        /// <summary>プリミティブ整数値をビッグエンディアンで書き込む</summary>
        /// <typeparam name="TValue">書き込み対象型</typeparam>
        /// <param name="value">書き込む値</param>
        public void WriteBigEndian<TValue>(ulong value) where TValue : struct, IBinaryInteger<ulong>
        {
            const int size = sizeof(ulong);
            var buffer = self.GetSpan(size);
            BinaryPrimitives.WriteUInt64BigEndian(buffer, value);
            self.Advance(size);
        }
    }
}
