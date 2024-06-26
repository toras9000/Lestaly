﻿using System.Buffers.Binary;
using System.Numerics;

namespace Lestaly;

/// <summary>
/// Span/Memory に対する拡張メソッド
/// </summary>
public static class MemoryExtensions
{
    /// <summary>Span{T} を ReadOnlySpan{T} に変換する。</summary>
    /// <remarks>暗黙の型変換が評価されない場面用。たとえば拡張メソッドのオーバーロード解決など。</remarks>
    /// <param name="self">Span{T}</param>
    /// <returns>ReadOnlySpan{T}</returns>
    public static ReadOnlySpan<T> AsReadOnly<T>(this Span<T> self) => self;

    /// <summary>配列のReadOnlySpanを作成する。</summary>
    /// <remarks>暗黙の型変換が評価されない場面用。たとえば拡張メソッドのオーバーロード解決など。</remarks>
    /// <typeparam name="T">要素の型</typeparam>
    /// <param name="self">対象配列</param>
    /// <returns>配列を指すReadOnlySpan</returns>
    public static ReadOnlySpan<T> AsReadOnlySpan<T>(this T[] self) => self;

    /// <summary>配列のReadOnlySpanを作成する。</summary>
    /// <remarks>暗黙の型変換が評価されない場面用。たとえば拡張メソッドのオーバーロード解決など。</remarks>
    /// <typeparam name="T">要素の型</typeparam>
    /// <param name="self">対象配列</param>
    /// <param name="start">スライス開始位置</param>
    /// <returns>配列を指すReadOnlySpan</returns>
    public static ReadOnlySpan<T> AsReadOnlySpan<T>(this T[] self, int start) => self.AsSpan(start);

    /// <summary>配列のReadOnlySpanを作成する。</summary>
    /// <remarks>暗黙の型変換が評価されない場面用。たとえば拡張メソッドのオーバーロード解決など。</remarks>
    /// <typeparam name="T">要素の型</typeparam>
    /// <param name="self">対象配列</param>
    /// <param name="start">スライス開始位置</param>
    /// <param name="length">スライス長</param>
    /// <returns>配列を指すReadOnlySpan</returns>
    public static ReadOnlySpan<T> AsReadOnlySpan<T>(this T[] self, int start, int length) => self.AsSpan(start, length);

    /// <summary>配列のReadOnlySpanを作成する。</summary>
    /// <remarks>暗黙の型変換が評価されない場面用。たとえば拡張メソッドのオーバーロード解決など。</remarks>
    /// <typeparam name="T">要素の型</typeparam>
    /// <param name="self">対象配列</param>
    /// <param name="range">スライス範囲</param>
    /// <returns>配列を指すReadOnlySpan</returns>
    public static ReadOnlySpan<T> AsReadOnlySpan<T>(this T[] self, Range range) => self.AsSpan(range);

    /// <summary>Memory{T} を ReadOnlyMemory{T} に変換する。</summary>
    /// <remarks>暗黙の型変換が評価されない場面用。たとえば拡張メソッドのオーバーロード解決など。</remarks>
    /// <param name="self">Memory{T}</param>
    /// <returns>ReadOnlyMemory{T}</returns>
    public static ReadOnlyMemory<T> AsReadOnly<T>(this Memory<T> self) => self;

    /// <summary>配列を指すReadOnlyMemoryを作成する。</summary>
    /// <remarks>暗黙の型変換が評価されない場面用。たとえば拡張メソッドのオーバーロード解決など。</remarks>
    /// <typeparam name="T">要素の型</typeparam>
    /// <param name="self">対象配列</param>
    /// <returns>配列を指すReadOnlyMemory</returns>
    public static ReadOnlyMemory<T> AsReadOnlyMemory<T>(this T[] self) => self;

    /// <summary>配列を指すReadOnlyMemoryを作成する。</summary>
    /// <remarks>暗黙の型変換が評価されない場面用。たとえば拡張メソッドのオーバーロード解決など。</remarks>
    /// <typeparam name="T">要素の型</typeparam>
    /// <param name="self">対象配列</param>
    /// <param name="start">スライス開始位置</param>
    /// <returns>配列を指すReadOnlyMemory</returns>
    public static ReadOnlyMemory<T> AsReadOnlyMemory<T>(this T[] self, int start) => self.AsMemory(start);

    /// <summary>配列を指すReadOnlyMemoryを作成する。</summary>
    /// <remarks>暗黙の型変換が評価されない場面用。たとえば拡張メソッドのオーバーロード解決など。</remarks>
    /// <typeparam name="T">要素の型</typeparam>
    /// <param name="self">対象配列</param>
    /// <param name="start">スライス開始位置</param>
    /// <param name="length">スライス長</param>
    /// <returns>配列を指すReadOnlyMemory</returns>
    public static ReadOnlyMemory<T> AsReadOnlyMemory<T>(this T[] self, int start, int length) => self.AsMemory(start, length);

    /// <summary>配列を指すReadOnlyMemoryを作成する。</summary>
    /// <remarks>暗黙の型変換が評価されない場面用。たとえば拡張メソッドのオーバーロード解決など。</remarks>
    /// <typeparam name="T">要素の型</typeparam>
    /// <param name="self">対象配列</param>
    /// <param name="range">スライス範囲</param>
    /// <returns>配列を指すReadOnlyMemory</returns>
    public static ReadOnlyMemory<T> AsReadOnlyMemory<T>(this T[] self, Range range) => self.AsMemory(range);

    #region Endian
    /// <summary>バイト列からリトルエンディアンで整数を読み取る</summary>
    /// <typeparam name="TResult">読み取り結果とする型</typeparam>
    /// <param name="self">読み取り元スパン</param>
    /// <returns>読み取り結果</returns>
    public static TResult AtLittleEndian<TResult>(this ReadOnlySpan<byte> self) where TResult : struct, IBinaryInteger<TResult>, IMinMaxValue<TResult>
        => TResult.ReadLittleEndian(self, 0 <= TResult.Sign(TResult.MinValue));

    /// <summary>バイト列からリトルエンディアンで整数を読み取る</summary>
    /// <typeparam name="TResult">読み取り結果とする型</typeparam>
    /// <param name="self">読み取り元スパン</param>
    /// <returns>読み取り結果</returns>
    public static TResult AtLittleEndian<TResult>(this Span<byte> self) where TResult : struct, IBinaryInteger<TResult>, IMinMaxValue<TResult>
        => TResult.ReadLittleEndian(self, 0 <= TResult.Sign(TResult.MinValue));

    /// <summary>バイト列からリトルエンディアンで整数を読み取る</summary>
    /// <typeparam name="TResult">読み取り結果とする型</typeparam>
    /// <param name="self">読み取り元スパン</param>
    /// <returns>読み取り結果</returns>
    public static TResult AtLittleEndian<TResult>(this byte[] self) where TResult : struct, IBinaryInteger<TResult>, IMinMaxValue<TResult>
        => TResult.ReadLittleEndian(self, 0 <= TResult.Sign(TResult.MinValue));

    /// <summary>バイト列からリトルエンディアンで半精度浮動小数点数を読み取る</summary>
    /// <param name="self">読み取り元スパン</param>
    /// <returns>読み取り結果</returns>
    public static Half AtHalfFloatLittleEndian(this ReadOnlySpan<byte> self)
        => BinaryPrimitives.ReadHalfLittleEndian(self);

    /// <summary>バイト列からリトルエンディアンで単精度浮動小数点数を読み取る</summary>
    /// <param name="self">読み取り元スパン</param>
    /// <returns>読み取り結果</returns>
    public static Single AtSingleFloatLittleEndian(this ReadOnlySpan<byte> self)
        => BinaryPrimitives.ReadSingleLittleEndian(self);

    /// <summary>バイト列からリトルエンディアンで倍精度浮動小数点数を読み取る</summary>
    /// <param name="self">読み取り元スパン</param>
    /// <returns>読み取り結果</returns>
    public static Double AtDoubleFloatLittleEndian(this ReadOnlySpan<byte> self)
        => BinaryPrimitives.ReadDoubleLittleEndian(self);

    /// <summary>バイト列からビッグエンディアンで整数を読み取る</summary>
    /// <typeparam name="TResult">読み取り結果とする型</typeparam>
    /// <param name="self">読み取り元スパン</param>
    /// <returns>読み取り結果</returns>
    public static TResult AtBigEndian<TResult>(this ReadOnlySpan<byte> self) where TResult : struct, IBinaryInteger<TResult>, IMinMaxValue<TResult>
        => TResult.ReadBigEndian(self, 0 <= TResult.Sign(TResult.MinValue));

    /// <summary>バイト列からビッグエンディアンで整数を読み取る</summary>
    /// <typeparam name="TResult">読み取り結果とする型</typeparam>
    /// <param name="self">読み取り元スパン</param>
    /// <returns>読み取り結果</returns>
    public static TResult AtBigEndian<TResult>(this Span<byte> self) where TResult : struct, IBinaryInteger<TResult>, IMinMaxValue<TResult>
        => TResult.ReadBigEndian(self, 0 <= TResult.Sign(TResult.MinValue));

    /// <summary>バイト列からビッグエンディアンで整数を読み取る</summary>
    /// <typeparam name="TResult">読み取り結果とする型</typeparam>
    /// <param name="self">読み取り元スパン</param>
    /// <returns>読み取り結果</returns>
    public static TResult AtBigEndian<TResult>(this byte[] self) where TResult : struct, IBinaryInteger<TResult>, IMinMaxValue<TResult>
        => TResult.ReadBigEndian(self, 0 <= TResult.Sign(TResult.MinValue));

    /// <summary>バイト列からビッグエンディアンで半精度浮動小数点数を読み取る</summary>
    /// <param name="self">読み取り元スパン</param>
    /// <returns>読み取り結果</returns>
    public static Half AtHalfFloatBigEndian(this ReadOnlySpan<byte> self)
        => BinaryPrimitives.ReadHalfBigEndian(self);

    /// <summary>バイト列からビッグエンディアンで単精度浮動小数点数を読み取る</summary>
    /// <param name="self">読み取り元スパン</param>
    /// <returns>読み取り結果</returns>
    public static Single AtSingleFloatBigEndian(this ReadOnlySpan<byte> self)
        => BinaryPrimitives.ReadSingleBigEndian(self);

    /// <summary>バイト列からビッグエンディアンで倍精度浮動小数点数を読み取る</summary>
    /// <param name="self">読み取り元スパン</param>
    /// <returns>読み取り結果</returns>
    public static Double AtDoubleFloatBigEndian(this ReadOnlySpan<byte> self)
        => BinaryPrimitives.ReadDoubleBigEndian(self);

    /// <summary>バイト列から指定のエンディアンで整数を読み取る</summary>
    /// <typeparam name="TResult">読み取り結果とする型</typeparam>
    /// <param name="self">読み取り元スパン</param>
    /// <param name="little">リトルエンディアンか否か</param>
    /// <returns>読み取り結果</returns>
    public static TResult AtEndian<TResult>(this ReadOnlySpan<byte> self, bool little) where TResult : struct, IBinaryInteger<TResult>, IMinMaxValue<TResult>
        => little ? TResult.ReadLittleEndian(self, 0 <= TResult.Sign(TResult.MinValue)) : TResult.ReadBigEndian(self, 0 <= TResult.Sign(TResult.MinValue));

    /// <summary>バイト列から指定のエンディアンで整数を読み取る</summary>
    /// <typeparam name="TResult">読み取り結果とする型</typeparam>
    /// <param name="self">読み取り元スパン</param>
    /// <param name="little">リトルエンディアンか否か</param>
    /// <returns>読み取り結果</returns>
    public static TResult AtEndian<TResult>(this Span<byte> self, bool little) where TResult : struct, IBinaryInteger<TResult>, IMinMaxValue<TResult>
        => little ? TResult.ReadLittleEndian(self, 0 <= TResult.Sign(TResult.MinValue)) : TResult.ReadBigEndian(self, 0 <= TResult.Sign(TResult.MinValue));

    /// <summary>バイト列から指定のエンディアンで整数を読み取る</summary>
    /// <typeparam name="TResult">読み取り結果とする型</typeparam>
    /// <param name="self">読み取り元スパン</param>
    /// <param name="little">リトルエンディアンか否か</param>
    /// <returns>読み取り結果</returns>
    public static TResult AtEndian<TResult>(this byte[] self, bool little) where TResult : struct, IBinaryInteger<TResult>, IMinMaxValue<TResult>
        => little ? TResult.ReadLittleEndian(self, 0 <= TResult.Sign(TResult.MinValue)) : TResult.ReadBigEndian(self, 0 <= TResult.Sign(TResult.MinValue));

    /// <summary>バイト列から指定のエンディアンで半精度浮動小数点数を読み取る</summary>
    /// <param name="self">読み取り元スパン</param>
    /// <param name="little">リトルエンディアンか否か</param>
    /// <returns>読み取り結果</returns>
    public static Half AtHalfFloatEndian(this ReadOnlySpan<byte> self, bool little)
        => little ? BinaryPrimitives.ReadHalfLittleEndian(self) : BinaryPrimitives.ReadHalfBigEndian(self);

    /// <summary>バイト列から指定のエンディアンで単精度浮動小数点数を読み取る</summary>
    /// <param name="self">読み取り元スパン</param>
    /// <param name="little">リトルエンディアンか否か</param>
    /// <returns>読み取り結果</returns>
    public static Single AtSingleFloatEndian(this ReadOnlySpan<byte> self, bool little)
        => little ? BinaryPrimitives.ReadSingleLittleEndian(self) : BinaryPrimitives.ReadSingleBigEndian(self);

    /// <summary>バイト列から指定のエンディアンで倍精度浮動小数点数を読み取る</summary>
    /// <param name="self">読み取り元スパン</param>
    /// <param name="little">リトルエンディアンか否か</param>
    /// <returns>読み取り結果</returns>
    public static Double AtDoubleFloatEndian(this ReadOnlySpan<byte> self, bool little)
        => little ? BinaryPrimitives.ReadDoubleLittleEndian(self) : BinaryPrimitives.ReadDoubleBigEndian(self);

    /// <summary>整数をリトルエンディアンでバッファに書き込む</summary>
    /// <typeparam name="TValue">整数の型</typeparam>
    /// <param name="self">書き込むバッファ</param>
    /// <param name="value">書き込む整数</param>
    /// <returns>書き込んだ領域の後ろを指すスパン</returns>
    public static Span<byte> WriteByLittleEndian<TValue>(this Span<byte> self, TValue value) where TValue : struct, IBinaryInteger<TValue>
    {
        var length = value.ToLittleEndian(self);
        return self[length..];
    }

    /// <summary>整数をビッグエンディアンでバッファに書き込む</summary>
    /// <typeparam name="TValue">整数の型</typeparam>
    /// <param name="self">書き込むバッファ</param>
    /// <param name="value">書き込む整数</param>
    /// <returns>書き込んだ領域の後ろを指すスパン</returns>
    public static Span<byte> WriteByBigEndian<TValue>(this Span<byte> self, TValue value) where TValue : struct, IBinaryInteger<TValue>
    {
        var length = value.ToBigEndian(self);
        return self[length..];
    }

    /// <summary>整数を指定のエンディアンでバッファに書き込む</summary>
    /// <typeparam name="TValue">整数の型</typeparam>
    /// <param name="self">書き込むバッファ</param>
    /// <param name="little">リトルエンディアンか否か</param>
    /// <param name="value">書き込む整数</param>
    /// <returns>書き込んだ領域の後ろを指すスパン</returns>
    public static Span<byte> WriteByEndian<TValue>(this Span<byte> self, bool little, TValue value) where TValue : struct, IBinaryInteger<TValue>
    {
        var length = value.ToEndian(little, self);
        return self[length..];
    }
    #endregion

    #region Copy
    /// <summary>指定のソースからスパンに値を格納する</summary>
    /// <typeparam name="T">要素型</typeparam>
    /// <param name="self">格納先スパン</param>
    /// <param name="source">コピー元ソース</param>
    public static void CopyFrom<T>(this Span<T> self, ReadOnlySpan<T> source) => source.CopyTo(self);

    /// <summary>指定のソースからスパンに値を格納し、その後ろのスパンを返却する</summary>
    /// <typeparam name="T">要素型</typeparam>
    /// <param name="self">格納先スパン</param>
    /// <param name="source">コピー元ソース</param>
    /// <returns>コピー範囲の後ろのスパン</returns>
    public static Span<T> CopyAdvanceFrom<T>(this Span<T> self, ReadOnlySpan<T> source)
    {
        source.CopyTo(self);
        return self[source.Length..];
    }
    #endregion 
}
