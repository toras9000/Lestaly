using System.Text;

namespace Lestaly;

/// <summary>
/// StringBuilder に対する拡張メソッド
/// </summary>
public static class StringBuilderExtensions
{
    /// <summary>文字列がnull/空や空であるかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>nullや空ならば true</returns>
    public static bool IsEmpty(this StringBuilder self) => self.Length == 0;

    /// <summary>文字列がnull/空や空以外であるかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>nullや空以外であれば true</returns>
    public static bool IsNotEmpty(this StringBuilder self) => !self.IsEmpty();

    /// <summary>文字列がnull/空や空白文字であるかを判定する</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>null/空や空白文字ならば true</returns>
    public static bool IsWhite(this StringBuilder self)
    {
        ArgumentNullException.ThrowIfNull(self);

        foreach (var chunk in self.GetChunks())
        {
            if (!chunk.Span.IsWhiteSpace()) return false;
        }

        return true;
    }

    /// <summary>文字列がnull/空や空白文字以外であるかを判定する</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>null/空や空白文字以外ならば true</returns>
    public static bool IsNotWhite(this StringBuilder self) => !self.IsWhite();

    /// <summary>文字列が指定の文字列で始まるかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="value">一致判定文字列</param>
    /// <returns>一致するか否か</returns>
    public static bool StartsWith(this StringBuilder self, string value)
    {
        // パラメータの検証 
        ArgumentNullException.ThrowIfNull(self);
        ArgumentException.ThrowIfNullOrEmpty(value);

        // 一致判定対象。スパンでずらしていく。
        var target = value.AsSpan();

        // 各チャンクを順に参照し先頭から比較対象に一致するか検証していく、
        foreach (var chunk in self.GetChunks())
        {
            // 一致判定
            if (chunk.Length < target.Length)
            {
                if (!chunk.Span.SequenceEqual(target[..chunk.Length])) return false;
                target = target[chunk.Length..];
            }
            else
            {
                if (!chunk.Span[..target.Length].SequenceEqual(target)) return false;
                target = target[target.Length..];
            }
            // 不一致なく判定文字列の全長を比較し終わったら終了
            if (target.IsEmpty) return true;
        }

        return false;
    }

    /// <summary>文字列が指定の文字列で始まるかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="value">一致判定文字列</param>
    /// <param name="comparison">文字列比較方法</param>
    /// <returns>一致するか否か</returns>
    public static bool StartsWith(this StringBuilder self, string value, StringComparison comparison)
    {
        // パラメータの検証 
        ArgumentNullException.ThrowIfNull(self);
        ArgumentException.ThrowIfNullOrEmpty(value);

        // 一致判定対象。スパンでずらしていく。
        var target = value.AsSpan();

        // 各チャンクを順に参照し先頭から比較対象に一致するか検証していく、
        foreach (var chunk in self.GetChunks())
        {
            // 一致判定
            if (chunk.Length < target.Length)
            {
                if (!chunk.Span.Equals(target[..chunk.Length], comparison)) return false;
                target = target[chunk.Length..];
            }
            else
            {
                if (!chunk.Span[..target.Length].Equals(target, comparison)) return false;
                target = target[target.Length..];
            }
            // 不一致なく判定文字列の全長を比較し終わったら終了
            if (target.IsEmpty) return true;
        }

        return false;
    }

    /// <summary>文字列が指定の文字列で終端するかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="value">一致判定文字列</param>
    /// <returns>一致するか否か</returns>
    public static bool EndsWith(this StringBuilder self, string value)
    {
        // パラメータの検証 
        ArgumentNullException.ThrowIfNull(self);
        ArgumentException.ThrowIfNullOrEmpty(value);

        // 比較文字列より小さい場合は不一致に確定
        if (self.Length < value.Length) return false;

        // 比較対象の長さによって比較方法を選択
        if (value.Length <= 100)
        {
            // 比較文字列が小さければスタック上で比較
            Span<char> tail = stackalloc char[value.Length];
            self.CopyTo(self.Length - tail.Length, tail, tail.Length);
            return tail.SequenceEqual(value.AsSpan());
        }
        else
        {
            // それ以外は文字列に切り出して比較
            var tail = self.ToString(self.Length - value.Length, value.Length);
            return tail == value;
        }
    }

    /// <summary>文字列が指定の文字列で終端するかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="value">一致判定文字列</param>
    /// <param name="comparison">文字列比較方法</param>
    /// <returns>一致するか否か</returns>
    public static bool EndsWith(this StringBuilder self, string value, StringComparison comparison)
    {
        // パラメータの検証 
        ArgumentNullException.ThrowIfNull(self);
        ArgumentException.ThrowIfNullOrEmpty(value);

        // 比較文字列より小さい場合は不一致に確定
        if (self.Length < value.Length) return false;

        // 比較対象の長さによって比較方法を選択
        if (value.Length <= 100)
        {
            // 比較文字列が小さければスタック上で比較
            Span<char> tail = stackalloc char[value.Length];
            self.CopyTo(self.Length - tail.Length, tail, tail.Length);
            return ((ReadOnlySpan<char>)tail).Equals(value, comparison);
        }
        else
        {
            // それ以外は文字列に切り出して比較
            var tail = self.ToString(self.Length - value.Length, value.Length);
            return tail.Equals(value, comparison);
        }
    }

    /// <summary>テキストを正規化してビルダに追加する</summary>
    /// <param name="self">追加先ビルダ</param>
    /// <param name="text">元のテキスト</param>
    /// <param name="form">ノーマライズ</param>
    /// <returns>レシーバビルダインスタンス</returns>
    public static StringBuilder AppendNormalized(this StringBuilder self, ReadOnlySpan<char> text, NormalizationForm form = NormalizationForm.FormKC)
    {
        // 空ならばなにもせず
        if (text.IsEmpty) return self;

        // 正規化時の長さを算出
        var normLen = text.GetNormalizedLength(form);
        if (normLen < 32)
        {
            // 短い場合、スタック上を利用。
            var buff = (stackalloc char[normLen]);
            if (text.TryNormalize(buff, out var written, form))
            {
                // 処理出来たらビルダに追加して
                self.Append(buff[..written]);
                return self;
            }
        }

        // 長い場合は普通にヒープを利用して処理
        {
            // 作業用メモリをプールから取得
            using var buff = new RentalArray<char>(normLen);
            if (text.TryNormalize(buff.Instance, out var written, form))
            {
                self.Append(buff.Instance.AsSpan(0, written));
            }
            else
            {
                // 念のため、失敗したら文字列変換して処理
                var normalized = text.ToString().Normalize(form);
                self.Append(normalized);
            }
        }

        return self;
    }
}
