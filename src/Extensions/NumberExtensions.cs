using System.Buffers;
using System.Buffers.Binary;
using System.Buffers.Text;
using System.Globalization;
using System.Numerics;

namespace Lestaly;

/// <summary>
/// 数値に対する拡張メソッド
/// </summary>
public static class NumberExtensions
{
    /// <summary>数値を補助単位付き表記で文字列化する。</summary>
    /// <param name="self">対象の値</param>
    /// <param name="si">true を指定すると 1000 の累乗で補助単位を付与する。falseの場合は 1024 の累乗となる。</param>
    /// <param name="numInfo">文字列化する際の数値書式情報。省略時は現在のカルチャに依存する。</param>
    /// <returns>構築された文字列</returns>
    public static string ToHumanize(this int self, bool si = false, NumberFormatInfo? numInfo = null)
        => NumberUtils.ToHumanize((long)self, si, numInfo);

    /// <summary>数値を補助単位付き表記で文字列化する。</summary>
    /// <param name="self">対象の値</param>
    /// <param name="si">true を指定すると 1000 の累乗で補助単位を付与する。falseの場合は 1024 の累乗となる。</param>
    /// <param name="numInfo">文字列化する際の数値書式情報。省略時は現在のカルチャに依存する。</param>
    /// <returns>構築された文字列</returns>
    public static string ToHumanize(this long self, bool si = false, NumberFormatInfo? numInfo = null)
        => NumberUtils.ToHumanize(self, si, numInfo);

    /// <summary>数値を補助単位付き表記で文字列化する。</summary>
    /// <param name="self">対象の値</param>
    /// <param name="si">true を指定すると 1000 の累乗で補助単位を付与する。falseの場合は 1024 の累乗となる。</param>
    /// <param name="numInfo">文字列化する際の数値書式情報。省略時は現在のカルチャに依存する。</param>
    /// <returns>構築された文字列</returns>
    public static string ToHumanize(this uint self, bool si = false, NumberFormatInfo? numInfo = null)
        => NumberUtils.ToHumanize((ulong)self, si, numInfo);

    /// <summary>数値を補助単位付き表記で文字列化する。</summary>
    /// <param name="self">対象の値</param>
    /// <param name="si">true を指定すると 1000 の累乗で補助単位を付与する。falseの場合は 1024 の累乗となる。</param>
    /// <param name="numInfo">文字列化する際の数値書式情報。省略時は現在のカルチャに依存する。</param>
    /// <returns>構築された文字列</returns>
    public static string ToHumanize(this ulong self, bool si = false, NumberFormatInfo? numInfo = null)
        => NumberUtils.ToHumanize(self, si, numInfo);

#if NET7_0_OR_GREATER
    /// <summary>数値を2進数表現で文字列化する。</summary>
    /// <typeparam name="TValue">値の型</typeparam>
    /// <param name="self">文字列化する値</param>
    /// <param name="prefix">プレフィックス文字列</param>
    /// <param name="sepa">区切り記号の間隔。ゼロの場合は区切り記号無し。</param>
    /// <returns>2進文字列</returns>
    public static string ToBinaryString<TValue>(this TValue self, string prefix = "0b_", int sepa = 8) where TValue : struct, IBinaryInteger<TValue>
    {
        if (sepa < 0) throw new ArgumentException($"Invalid {nameof(sepa)}");

        // 念のためnull除去
        prefix ??= "";

        // ビット幅取得
        var bits = self.GetByteCount() * 8;

        // バッファを用意してプレフィクスを格納
        var buff = (stackalloc char[prefix.Length + bits + bits]);

        // ビットの文字列化。
        var space = buff.Length;
        var thick = 0;
        for (var i = 0; i < bits; i++)
        {
            if (sepa != 0 && thick == sepa)
            {
                buff[space - 1] = '_';
                space--;
                thick = 0;
            }

            buff[space - 1] = (self & (TValue.One << i)) == TValue.Zero ? '0' : '1';
            space--;
            thick++;
        }

        // プレフィックスを付与
        prefix.CopyTo(buff[(space - prefix.Length)..]);
        space -= prefix.Length;

        // 文字列化結果を返却
        return buff[space..].ToString();
    }

    /// <summary>数値を16進数表現で文字列化する。</summary>
    /// <typeparam name="TValue">値の型</typeparam>
    /// <param name="self">文字列化する値</param>
    /// <param name="prefix">プレフィックス文字列</param>
    /// <param name="sepa">区切り記号の間隔。ゼロの場合は区切り記号無し。</param>
    /// <returns>16進文字列</returns>
    public static string ToHexString<TValue>(this TValue self, string prefix = "0x", int sepa = 8) where TValue : struct, IBinaryInteger<TValue>
    {
        if (sepa < 0) throw new ArgumentException($"Invalid {nameof(sepa)}");

        // 念のためnull除去
        prefix ??= "";

        // ビット幅取得
        var bits = self.GetByteCount() * 8;

        // 1桁分のビットマスク
        var mask = (TValue.One << 0) | (TValue.One << 1) | (TValue.One << 2) | (TValue.One << 3);

        // バッファを用意
        var buff = (stackalloc char[prefix.Length + bits]);

        // HEX文字列化。
        var space = buff.Length;
        var thick = 0;
        for (var i = 0; i < bits; i += 4)
        {
            if (sepa != 0 && thick == sepa)
            {
                buff[space - 1] = '_';
                space--;
                thick = 0;
            }
            var nibble = (self >> i) & mask;
            nibble.TryFormat(buff[(space - 1)..], out var _, "X", CultureInfo.InvariantCulture);
            space--;
            thick++;
        }

        // プレフィックスを付与
        prefix.CopyTo(buff[(space - prefix.Length)..]);
        space -= prefix.Length;

        // 文字列化結果を返却
        return buff[space..].ToString();
    }

    /// <summary>数値をリトルエンディアンのバイト列でバッファに書き込む。</summary>
    /// <typeparam name="TValue">値の型</typeparam>
    /// <param name="self">バイナリ列として格納する値</param>
    /// <param name="destination">格納先バッファ。</param>
    /// <returns>書き込んだバイト数</returns>
    public static int ToLittleEndian<TValue>(this TValue self, Span<byte> destination) where TValue : struct, IBinaryInteger<TValue>
        => self.WriteLittleEndian(destination);

    /// <summary>数値をリトルエンディアンのバイト列でバッファに書き込む。</summary>
    /// <param name="self">バイナリ列として格納する値</param>
    /// <param name="destination">格納先バッファ。</param>
    /// <returns>書き込んだバイト数</returns>
    public static int ToLittleEndian(this Half self, Span<byte> destination)
    {
        BinaryPrimitives.WriteHalfLittleEndian(destination, self);
        return 2;
    }

    /// <summary>数値をリトルエンディアンのバイト列でバッファに書き込む。</summary>
    /// <param name="self">バイナリ列として格納する値</param>
    /// <param name="destination">格納先バッファ。</param>
    /// <returns>書き込んだバイト数</returns>
    public static int ToLittleEndian(this Single self, Span<byte> destination)
    {
        BinaryPrimitives.WriteSingleLittleEndian(destination, self);
        return 4;
    }

    /// <summary>数値をリトルエンディアンのバイト列でバッファに書き込む。</summary>
    /// <param name="self">バイナリ列として格納する値</param>
    /// <param name="destination">格納先バッファ。</param>
    /// <returns>書き込んだバイト数</returns>
    public static int ToLittleEndian(this Double self, Span<byte> destination)
    {
        BinaryPrimitives.WriteDoubleLittleEndian(destination, self);
        return 8;
    }

    /// <summary>数値をビッグエンディアンのバイト列でバッファに書き込む。</summary>
    /// <typeparam name="TValue">値の型</typeparam>
    /// <param name="self">バイナリ列として格納する値</param>
    /// <param name="destination">格納先バッファ。</param>
    /// <returns>書き込んだバイト数</returns>
    public static int ToBigEndian<TValue>(this TValue self, Span<byte> destination) where TValue : struct, IBinaryInteger<TValue>
        => self.WriteBigEndian(destination);

    /// <summary>数値をビッグエンディアンのバイト列でバッファに書き込む。</summary>
    /// <param name="self">バイナリ列として格納する値</param>
    /// <param name="destination">格納先バッファ。</param>
    /// <returns>書き込んだバイト数</returns>
    public static int ToBigEndian(this Half self, Span<byte> destination)
    {
        BinaryPrimitives.WriteHalfBigEndian(destination, self);
        return 2;
    }

    /// <summary>数値をビッグエンディアンのバイト列でバッファに書き込む。</summary>
    /// <param name="self">バイナリ列として格納する値</param>
    /// <param name="destination">格納先バッファ。</param>
    /// <returns>書き込んだバイト数</returns>
    public static int ToBigEndian(this Single self, Span<byte> destination)
    {
        BinaryPrimitives.WriteSingleBigEndian(destination, self);
        return 4;
    }

    /// <summary>数値をビッグエンディアンのバイト列でバッファに書き込む。</summary>
    /// <param name="self">バイナリ列として格納する値</param>
    /// <param name="destination">格納先バッファ。</param>
    /// <returns>書き込んだバイト数</returns>
    public static int ToBigEndian(this Double self, Span<byte> destination)
    {
        BinaryPrimitives.WriteDoubleBigEndian(destination, self);
        return 8;
    }

    /// <summary>数値を指定したエンディアンのバイト列でバッファに書き込む。</summary>
    /// <typeparam name="TValue">値の型</typeparam>
    /// <param name="self">バイナリ列として格納する値</param>
    /// <param name="little">リトルエンディアンか否か</param>
    /// <param name="destination">格納先バッファ。</param>
    /// <returns>書き込んだバイト数</returns>
    public static int ToEndian<TValue>(this TValue self, bool little, Span<byte> destination) where TValue : struct, IBinaryInteger<TValue>
        => little ? self.ToLittleEndian(destination) : self.ToBigEndian(destination);

    /// <summary>数値を指定したエンディアンのバイト列でバッファに書き込む。</summary>
    /// <param name="self">バイナリ列として格納する値</param>
    /// <param name="little">リトルエンディアンか否か</param>
    /// <param name="destination">格納先バッファ。</param>
    /// <returns>書き込んだバイト数</returns>
    public static int ToEndian(this Half self, bool little, Span<byte> destination)
        => little ? self.ToLittleEndian(destination) : self.ToBigEndian(destination);

    /// <summary>数値を指定したエンディアンのバイト列でバッファに書き込む。</summary>
    /// <param name="self">バイナリ列として格納する値</param>
    /// <param name="little">リトルエンディアンか否か</param>
    /// <param name="destination">格納先バッファ。</param>
    /// <returns>書き込んだバイト数</returns>
    public static int ToEndian(this Single self, bool little, Span<byte> destination)
        => little ? self.ToLittleEndian(destination) : self.ToBigEndian(destination);

    /// <summary>数値を指定したエンディアンのバイト列でバッファに書き込む。</summary>
    /// <param name="self">バイナリ列として格納する値</param>
    /// <param name="little">リトルエンディアンか否か</param>
    /// <param name="destination">格納先バッファ。</param>
    /// <returns>書き込んだバイト数</returns>
    public static int ToEndian(this Double self, bool little, Span<byte> destination)
        => little ? self.ToLittleEndian(destination) : self.ToBigEndian(destination);
#endif
}
