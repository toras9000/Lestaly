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
    /// <param name="self">対象の型</param>
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
        prefix.CopyTo(buff);

        // ビットの文字列化。
        // 特にセパレータを埋める処理をシンプルにする目的で、まずは下位ビットから逆順で文字列化していく。
        var bin = buff.Slice(prefix.Length);
        var adv = 0;
        if (sepa <= 0)
        {
            // セパレータ無しの場合はシンプルに埋める
            for (var i = 0; i < bits; i++)
            {
                bin[i] = (self & (TValue.One << i)) == TValue.Zero ? '0' : '1';
            }
        }
        else
        {
            // セパレータを挟みながら文字列化する
            var thick = 0;
            for (var i = 0; i < bits; i++)
            {
                if (thick == sepa)
                {
                    bin[adv + i] = '_';
                    adv++;
                    thick = 0;
                }
                bin[adv + i] = (self & (TValue.One << i)) == TValue.Zero ? '0' : '1';
                thick++;
            }
        }

        // 逆順で文字列化したものをひっくり返す。
        bin.Slice(0, adv + bits).Reverse();

        // 文字列化結果を返却
        return buff.Slice(0, prefix.Length + adv + bits).ToString();
    }
#endif 
}
