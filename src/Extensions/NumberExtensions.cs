using System.Buffers;
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
    /// <summary>
    /// 数値を2進数表現で文字列化する。
    /// </summary>
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

    /// <summary>
    /// 数値を16進数表現で文字列化する。
    /// </summary>
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
#endif
}
