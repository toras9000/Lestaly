using System.Buffers.Binary;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Lestaly;

/// <summary>Span/Memory に対する拡張メソッド</summary>
public static class MemoryExtensions
{
    #region ReadOnly
    /// <summary>Span に対するメソッド</summary>
    /// <typeparam name="T">要素型</typeparam>
    /// <param name="self">Span{T}</param>
    extension<T>(Span<T> self)
    {
        /// <summary>Span{T} を ReadOnlySpan{T} に変換する。</summary>
        /// <remarks>暗黙の型変換が評価されない場面用。たとえば拡張メソッドのオーバーロード解決など。</remarks>
        /// <returns>ReadOnlySpan{T}</returns>
        public ReadOnlySpan<T> AsReadOnly() => self;
    }

    /// <summary>Memory に対するメソッド</summary>
    /// <typeparam name="T">要素型</typeparam>
    /// <param name="self">Memory{T}</param>
    extension<T>(Memory<T> self)
    {
        /// <summary>Memory{T} を ReadOnlyMemory{T} に変換する。</summary>
        /// <remarks>暗黙の型変換が評価されない場面用。たとえば拡張メソッドのオーバーロード解決など。</remarks>
        /// <returns>ReadOnlyMemory{T}</returns>
        public ReadOnlyMemory<T> AsReadOnly() => self;
    }

    /// <summary>配列 に対するメソッド</summary>
    /// <typeparam name="T">要素型</typeparam>
    /// <param name="self">対象配列</param>
    extension<T>(T[] self)
    {
        /// <summary>配列のReadOnlySpanを作成する。</summary>
        /// <remarks>暗黙の型変換が評価されない場面用。たとえば拡張メソッドのオーバーロード解決など。</remarks>
        /// <returns>配列を指すReadOnlySpan</returns>
        public ReadOnlySpan<T> AsReadOnlySpan() => self;

        /// <summary>配列のReadOnlySpanを作成する。</summary>
        /// <remarks>暗黙の型変換が評価されない場面用。たとえば拡張メソッドのオーバーロード解決など。</remarks>
        /// <param name="start">スライス開始位置</param>
        /// <returns>配列を指すReadOnlySpan</returns>
        public ReadOnlySpan<T> AsReadOnlySpan(int start) => self.AsSpan(start);

        /// <summary>配列のReadOnlySpanを作成する。</summary>
        /// <remarks>暗黙の型変換が評価されない場面用。たとえば拡張メソッドのオーバーロード解決など。</remarks>
        /// <param name="start">スライス開始位置</param>
        /// <param name="length">スライス長</param>
        /// <returns>配列を指すReadOnlySpan</returns>
        public ReadOnlySpan<T> AsReadOnlySpan(int start, int length) => self.AsSpan(start, length);

        /// <summary>配列のReadOnlySpanを作成する。</summary>
        /// <remarks>暗黙の型変換が評価されない場面用。たとえば拡張メソッドのオーバーロード解決など。</remarks>
        /// <param name="range">スライス範囲</param>
        /// <returns>配列を指すReadOnlySpan</returns>
        public ReadOnlySpan<T> AsReadOnlySpan(Range range) => self.AsSpan(range);

        /// <summary>配列を指すReadOnlyMemoryを作成する。</summary>
        /// <remarks>暗黙の型変換が評価されない場面用。たとえば拡張メソッドのオーバーロード解決など。</remarks>
        /// <returns>配列を指すReadOnlyMemory</returns>
        public ReadOnlyMemory<T> AsReadOnlyMemory() => self;

        /// <summary>配列を指すReadOnlyMemoryを作成する。</summary>
        /// <remarks>暗黙の型変換が評価されない場面用。たとえば拡張メソッドのオーバーロード解決など。</remarks>
        /// <param name="start">スライス開始位置</param>
        /// <returns>配列を指すReadOnlyMemory</returns>
        public ReadOnlyMemory<T> AsReadOnlyMemory(int start) => self.AsMemory(start);

        /// <summary>配列を指すReadOnlyMemoryを作成する。</summary>
        /// <remarks>暗黙の型変換が評価されない場面用。たとえば拡張メソッドのオーバーロード解決など。</remarks>
        /// <param name="start">スライス開始位置</param>
        /// <param name="length">スライス長</param>
        /// <returns>配列を指すReadOnlyMemory</returns>
        public ReadOnlyMemory<T> AsReadOnlyMemory(int start, int length) => self.AsMemory(start, length);

        /// <summary>配列を指すReadOnlyMemoryを作成する。</summary>
        /// <remarks>暗黙の型変換が評価されない場面用。たとえば拡張メソッドのオーバーロード解決など。</remarks>
        /// <param name="range">スライス範囲</param>
        /// <returns>配列を指すReadOnlyMemory</returns>
        public ReadOnlyMemory<T> AsReadOnlyMemory(Range range) => self.AsMemory(range);
    }
    #endregion

    #region Endian Read
    /// <summary>Span に対するメソッド</summary>
    /// <param name="self">対象スパン</param>
    extension(Span<byte> self)
    {
        /// <summary>バイト列からリトルエンディアンで整数を読み取る</summary>
        /// <typeparam name="TResult">読み取り結果とする型</typeparam>
        /// <returns>読み取り結果</returns>
        public TResult AtLittleEndian<TResult>() where TResult : struct, IBinaryInteger<TResult>, IMinMaxValue<TResult>
            => TResult.ReadLittleEndian(self, 0 <= TResult.Sign(TResult.MinValue));

        /// <summary>バイト列からビッグエンディアンで整数を読み取る</summary>
        /// <typeparam name="TResult">読み取り結果とする型</typeparam>
        /// <returns>読み取り結果</returns>
        public TResult AtBigEndian<TResult>() where TResult : struct, IBinaryInteger<TResult>, IMinMaxValue<TResult>
            => TResult.ReadBigEndian(self, 0 <= TResult.Sign(TResult.MinValue));

        /// <summary>バイト列から指定のエンディアンで整数を読み取る</summary>
        /// <typeparam name="TResult">読み取り結果とする型</typeparam>
        /// <param name="little">リトルエンディアンか否か</param>
        /// <returns>読み取り結果</returns>
        public TResult AtEndian<TResult>(bool little) where TResult : struct, IBinaryInteger<TResult>, IMinMaxValue<TResult>
            => little ? TResult.ReadLittleEndian(self, 0 <= TResult.Sign(TResult.MinValue)) : TResult.ReadBigEndian(self, 0 <= TResult.Sign(TResult.MinValue));
    }

    /// <summary>ReadOnlySpan に対するメソッド</summary>
    /// <param name="self">対象スパン</param>
    extension(ReadOnlySpan<byte> self)
    {
        /// <summary>バイト列からリトルエンディアンで整数を読み取る</summary>
        /// <typeparam name="TResult">読み取り結果とする型</typeparam>
        /// <returns>読み取り結果</returns>
        public TResult AtLittleEndian<TResult>() where TResult : struct, IBinaryInteger<TResult>, IMinMaxValue<TResult>
            => TResult.ReadLittleEndian(self, 0 <= TResult.Sign(TResult.MinValue));

        /// <summary>バイト列からリトルエンディアンで半精度浮動小数点数を読み取る</summary>
        /// <returns>読み取り結果</returns>
        public Half AtHalfFloatLittleEndian()
            => BinaryPrimitives.ReadHalfLittleEndian(self);

        /// <summary>バイト列からリトルエンディアンで単精度浮動小数点数を読み取る</summary>
        /// <returns>読み取り結果</returns>
        public Single AtSingleFloatLittleEndian()
            => BinaryPrimitives.ReadSingleLittleEndian(self);

        /// <summary>バイト列からリトルエンディアンで倍精度浮動小数点数を読み取る</summary>
        /// <returns>読み取り結果</returns>
        public Double AtDoubleFloatLittleEndian()
            => BinaryPrimitives.ReadDoubleLittleEndian(self);

        /// <summary>バイト列からビッグエンディアンで整数を読み取る</summary>
        /// <typeparam name="TResult">読み取り結果とする型</typeparam>
        /// <returns>読み取り結果</returns>
        public TResult AtBigEndian<TResult>() where TResult : struct, IBinaryInteger<TResult>, IMinMaxValue<TResult>
            => TResult.ReadBigEndian(self, 0 <= TResult.Sign(TResult.MinValue));

        /// <summary>バイト列からビッグエンディアンで半精度浮動小数点数を読み取る</summary>
        /// <returns>読み取り結果</returns>
        public Half AtHalfFloatBigEndian()
            => BinaryPrimitives.ReadHalfBigEndian(self);

        /// <summary>バイト列からビッグエンディアンで単精度浮動小数点数を読み取る</summary>
        /// <returns>読み取り結果</returns>
        public Single AtSingleFloatBigEndian()
            => BinaryPrimitives.ReadSingleBigEndian(self);

        /// <summary>バイト列からビッグエンディアンで倍精度浮動小数点数を読み取る</summary>
        /// <returns>読み取り結果</returns>
        public Double AtDoubleFloatBigEndian()
            => BinaryPrimitives.ReadDoubleBigEndian(self);

        /// <summary>バイト列から指定のエンディアンで整数を読み取る</summary>
        /// <typeparam name="TResult">読み取り結果とする型</typeparam>
        /// <param name="little">リトルエンディアンか否か</param>
        /// <returns>読み取り結果</returns>
        public TResult AtEndian<TResult>(bool little) where TResult : struct, IBinaryInteger<TResult>, IMinMaxValue<TResult>
            => little ? TResult.ReadLittleEndian(self, 0 <= TResult.Sign(TResult.MinValue)) : TResult.ReadBigEndian(self, 0 <= TResult.Sign(TResult.MinValue));

        /// <summary>バイト列から指定のエンディアンで半精度浮動小数点数を読み取る</summary>
        /// <param name="little">リトルエンディアンか否か</param>
        /// <returns>読み取り結果</returns>
        public Half AtHalfFloatEndian(bool little)
            => little ? BinaryPrimitives.ReadHalfLittleEndian(self) : BinaryPrimitives.ReadHalfBigEndian(self);

        /// <summary>バイト列から指定のエンディアンで単精度浮動小数点数を読み取る</summary>
        /// <param name="little">リトルエンディアンか否か</param>
        /// <returns>読み取り結果</returns>
        public Single AtSingleFloatEndian(bool little)
            => little ? BinaryPrimitives.ReadSingleLittleEndian(self) : BinaryPrimitives.ReadSingleBigEndian(self);

        /// <summary>バイト列から指定のエンディアンで倍精度浮動小数点数を読み取る</summary>
        /// <param name="little">リトルエンディアンか否か</param>
        /// <returns>読み取り結果</returns>
        public Double AtDoubleFloatEndian(bool little)
            => little ? BinaryPrimitives.ReadDoubleLittleEndian(self) : BinaryPrimitives.ReadDoubleBigEndian(self);
    }

    /// <summary>配列 に対するメソッド</summary>
    /// <param name="self">対象スパン</param>
    extension(byte[] self)
    {
        /// <summary>バイト列からリトルエンディアンで整数を読み取る</summary>
        /// <typeparam name="TResult">読み取り結果とする型</typeparam>
        /// <returns>読み取り結果</returns>
        public TResult AtLittleEndian<TResult>() where TResult : struct, IBinaryInteger<TResult>, IMinMaxValue<TResult>
            => TResult.ReadLittleEndian(self, 0 <= TResult.Sign(TResult.MinValue));

        /// <summary>バイト列からビッグエンディアンで整数を読み取る</summary>
        /// <typeparam name="TResult">読み取り結果とする型</typeparam>
        /// <returns>読み取り結果</returns>
        public TResult AtBigEndian<TResult>() where TResult : struct, IBinaryInteger<TResult>, IMinMaxValue<TResult>
            => TResult.ReadBigEndian(self, 0 <= TResult.Sign(TResult.MinValue));

        /// <summary>バイト列から指定のエンディアンで整数を読み取る</summary>
        /// <typeparam name="TResult">読み取り結果とする型</typeparam>
        /// <param name="little">リトルエンディアンか否か</param>
        /// <returns>読み取り結果</returns>
        public TResult AtEndian<TResult>(bool little) where TResult : struct, IBinaryInteger<TResult>, IMinMaxValue<TResult>
            => little ? TResult.ReadLittleEndian(self, 0 <= TResult.Sign(TResult.MinValue)) : TResult.ReadBigEndian(self, 0 <= TResult.Sign(TResult.MinValue));
    }
    #endregion

    #region Endian Write
    /// <summary>Span に対するメソッド</summary>
    /// <param name="self">書き込むバッファ</param>
    extension(Span<byte> self)
    {
        /// <summary>整数をリトルエンディアンでバッファに書き込む</summary>
        /// <typeparam name="TValue">整数の型</typeparam>
        /// <param name="value">書き込む整数</param>
        /// <returns>書き込んだ領域の後ろを指すスパン</returns>
        public Span<byte> WriteByLittleEndian<TValue>(TValue value) where TValue : struct, IBinaryInteger<TValue>
        {
            var length = value.ToLittleEndian(self);
            return self[length..];
        }

        /// <summary>整数をビッグエンディアンでバッファに書き込む</summary>
        /// <typeparam name="TValue">整数の型</typeparam>
        /// <param name="value">書き込む整数</param>
        /// <returns>書き込んだ領域の後ろを指すスパン</returns>
        public Span<byte> WriteByBigEndian<TValue>(TValue value) where TValue : struct, IBinaryInteger<TValue>
        {
            var length = value.ToBigEndian(self);
            return self[length..];
        }

        /// <summary>整数を指定のエンディアンでバッファに書き込む</summary>
        /// <typeparam name="TValue">整数の型</typeparam>
        /// <param name="little">リトルエンディアンか否か</param>
        /// <param name="value">書き込む整数</param>
        /// <returns>書き込んだ領域の後ろを指すスパン</returns>
        public Span<byte> WriteByEndian<TValue>(bool little, TValue value) where TValue : struct, IBinaryInteger<TValue>
        {
            var length = value.ToEndian(little, self);
            return self[length..];
        }
    }
    #endregion

    #region Copy
    /// <summary>Span に対するメソッド</summary>
    /// <param name="self">格納先スパン</param>
    extension<T>(Span<T> self)
    {
        /// <summary>指定のソースからスパンに値を格納する</summary>
        /// <param name="source">コピー元ソース</param>
        public void CopyFrom(ReadOnlySpan<T> source) => source.CopyTo(self);

        /// <summary>指定のソースからスパンに値を格納し、その後ろのスパンを返却する</summary>
        /// <param name="source">コピー元ソース</param>
        /// <returns>コピー範囲の後ろのスパン</returns>
        public Span<T> CopyAdvanceFrom(ReadOnlySpan<T> source)
        {
            source.CopyTo(self);
            return self[source.Length..];
        }
    }
    #endregion

    #region Utility
    /// <summary>ReadOnlySpan に対するメソッド</summary>
    /// <typeparam name="T">要素型</typeparam>
    /// <param name="self">対象スパン</param>
    extension<T>(ReadOnlySpan<T> self)
    {
        /// <summary>スパンを指定の数を最大長としてスライスする</summary>
        /// <param name="count">最大数</param>
        /// <returns>指定した最大長以下ならばそのまま、最大長より大きければその長さに切り出したスパン</returns>
        public ReadOnlySpan<T> Cap(int count)
        {
            if (self.Length < count) return self;
            if (count <= 0) return self[..0];
            return self[..count];
        }
    }
    #endregion

    #region Count
    /// <summary>ReadOnlySpan に対するメソッド</summary>
    /// <typeparam name="T">要素型</typeparam>
    /// <param name="self">対象スパン</param>
    extension<T>(ReadOnlySpan<T> self)
    {
#if NET10_0_OR_GREATER
        /// <summary>シーケンスに含まれるパターンの数をカウントする</summary>
        /// <param name="pattern">コピー元ソース</param>
        /// <param name="comparer">比較子</param>
        public int CountPattern(ReadOnlySpan<T> pattern, IEqualityComparer<T>? comparer = null)
        {
            var count = 0;
            var scan = self;
            while (!scan.IsEmpty)
            {
                var pos = scan.IndexOf(pattern, comparer);
                if (pos < 0) break;
                count++;
                scan = scan[(pos + pattern.Length)..];
            }
            return count;
        }
#endif
    }
    #endregion

    #region string
    /// <summary>文字列の ReadOnlySpan に対するメソッド</summary>
    /// <param name="self">象スパン</param>
    extension(ReadOnlySpan<string> self)
    {
        /// <summary>文字列スパンの先頭からパターンに一致する要素を取り除く</summary>
        /// <param name="pattern">正規表現パターン</param>
        /// <param name="options">正規表現オプション</param>
        /// <returns>一致要素が取り除かれたスパン</returns>
        public ReadOnlySpan<string> TrimStartPattern([StringSyntax(StringSyntaxAttribute.Regex, nameof(options))] string pattern, RegexOptions options = default)
            => self.TrimStartPattern(new Regex(pattern, options));

        /// <summary>文字列スパンの先頭からパターンに一致する要素を取り除く</summary>
        /// <param name="pattern">正規表現</param>
        /// <returns>一致要素が取り除かれたスパン</returns>
        public ReadOnlySpan<string> TrimStartPattern(Regex pattern)
        {
            var contents = self;
            while (!contents.IsEmpty)
            {
                if (!pattern.IsMatch(contents[0])) break;
                contents = contents[1..];
            }
            return contents;
        }

        /// <summary>文字列スパンの末尾からパターンに一致する要素を取り除く</summary>
        /// <param name="pattern">正規表現パターン</param>
        /// <param name="options">正規表現オプション</param>
        /// <returns>一致要素が取り除かれたスパン</returns>
        public ReadOnlySpan<string> TrimEndPattern([StringSyntax(StringSyntaxAttribute.Regex, nameof(options))] string pattern, RegexOptions options = default)
            => self.TrimEndPattern(new Regex(pattern, options));

        /// <summary>文字列スパンの末尾からパターンに一致する要素を取り除く</summary>
        /// <param name="pattern">正規表現</param>
        /// <returns>一致要素が取り除かれたスパン</returns>
        public ReadOnlySpan<string> TrimEndPattern(Regex pattern)
        {
            var contents = self;
            while (!contents.IsEmpty)
            {
                if (!pattern.IsMatch(contents[^1])) break;
                contents = contents[..^1];
            }
            return contents;
        }
    }
    #endregion
}
