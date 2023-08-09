using System.Buffers.Binary;
using System.Numerics;

namespace Lestaly;

/// <summary>
/// Span に対する拡張メソッド
/// </summary>
public static class SpanExtensions
{
    /// <summary>Span{T} を ReadOnlySpan{T} に変換する。</summary>
    /// <remarks>暗黙の型変換が評価されない場面用。たとえば拡張メソッドのオーバーロード解決など。</remarks>
    /// <param name="self">Span{T}</param>
    /// <returns>ReadOnlySpan{T}</returns>
    public static ReadOnlySpan<T> AsReadOnly<T>(this Span<T> self) => self;

    /// <summary>配列の読み取り専用スパンを作成する。</summary>
    /// <remarks>暗黙の型変換が評価されない場面用。たとえば拡張メソッドのオーバーロード解決など。</remarks>
    /// <typeparam name="T">要素の型</typeparam>
    /// <param name="self">対象配列</param>
    /// <returns>読み取り専用スパン</returns>
    public static ReadOnlySpan<T> AsReadOnlySpan<T>(this T[] self) => self;

    /// <summary>配列の読み取り専用スパンを作成する。</summary>
    /// <remarks>暗黙の型変換が評価されない場面用。たとえば拡張メソッドのオーバーロード解決など。</remarks>
    /// <typeparam name="T">要素の型</typeparam>
    /// <param name="self">対象配列</param>
    /// <param name="start">スライス開始位置</param>
    /// <returns>読み取り専用スパン</returns>
    public static ReadOnlySpan<T> AsReadOnlySpan<T>(this T[] self, int start) => self.AsSpan(start);

    /// <summary>配列の読み取り専用スパンを作成する。</summary>
    /// <remarks>暗黙の型変換が評価されない場面用。たとえば拡張メソッドのオーバーロード解決など。</remarks>
    /// <typeparam name="T">要素の型</typeparam>
    /// <param name="self">対象配列</param>
    /// <param name="start">スライス開始位置</param>
    /// <param name="length">スライス長</param>
    /// <returns>読み取り専用スパン</returns>
    public static ReadOnlySpan<T> AsReadOnlySpan<T>(this T[] self, int start, int length) => self.AsSpan(start, length);


    #region Endian

#if NET7_0_OR_GREATER
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
#endif 
    #endregion
}
