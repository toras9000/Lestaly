using System.Buffers;
using System.Buffers.Text;
using System.Text;

namespace Lestaly;

/// <summary>
/// string に対するエンコード関係拡張メソッド
/// </summary>
public static class StringEncodeExtensions
{
    /// <summary>バイト列をUTF8デコードした文字列として読み取る。</summary>
    /// <param name="self">読み取り元バッファ</param>
    /// <returns>読み取った文字列</returns>
    public static string ReadUtf8(this ReadOnlySpan<byte> self)
        => Encoding.UTF8.GetString(self);

    /// <summary>文字列をUTF8エンコードしてバッファに書き込む</summary>
    /// <param name="self">書き込み先バッファ</param>
    /// <param name="text">書き込み文字列</param>
    /// <returns>書き込んだバイト数</returns>
    public static int WriteUtf8(this Span<byte> self, ReadOnlySpan<char> text)
        => Encoding.UTF8.GetBytes(text, self);


    /// <summary>文字列をUTF8バイト列にエンコードする。</summary>
    /// <param name="self">文字列。nullは空文字列と同じ扱い。</param>
    /// <returns>エンコードしたバイト列</returns>
    public static byte[] EncodeUtf8(this string? self)
        => EncodeUtf8(self.AsSpan());

    /// <summary>文字列をUTF8バイト列にエンコードする。</summary>
    /// <param name="self">文字列。nullは空文字列と同じ扱い。</param>
    /// <returns>エンコードしたバイト列</returns>
    public static byte[] EncodeUtf8(this char[]? self)
        => EncodeUtf8(self.AsSpan());

    /// <summary>文字列をUTF8バイト列にエンコードする。</summary>
    /// <param name="self">文字列</param>
    /// <returns>エンコードしたバイト列</returns>
    public static byte[] EncodeUtf8(this Span<char> self)
        => EncodeUtf8(self.AsReadOnly());

    /// <summary>文字列をUTF8バイト列にエンコードする。</summary>
    /// <param name="self">文字列</param>
    /// <returns>エンコードしたバイト列</returns>
    public static byte[] EncodeUtf8(this ReadOnlySpan<char> self)
    {
        var writer = new ArrayBufferWriter<byte>(self.Length);
        var length = Encoding.UTF8.GetBytes(self, writer);
        return writer.WrittenSpan.ToArray();
    }


    /// <summary>バイト列をUTF8として文字列にデコードする。</summary>
    /// <param name="self">バイト列。nullは空シーケンスと同じ扱い。</param>
    /// <returns>デコードした文字列</returns>
    public static string DecodeUtf8(this byte[]? self)
        => DecodeUtf8(self.AsSpan());

    /// <summary>バイト列をUTF8として文字列にデコードする。</summary>
    /// <param name="self">バイト列</param>
    /// <returns>デコードした文字列</returns>
    public static string DecodeUtf8(this Span<byte> self)
        => DecodeUtf8(self.AsReadOnly());

    /// <summary>バイト列をUTF8として文字列にデコードする。</summary>
    /// <param name="self">バイト列</param>
    /// <returns>デコードした文字列</returns>
    public static string DecodeUtf8(this ReadOnlySpan<byte> self)
        => Encoding.UTF8.GetString(self);


    /// <summary>文字列のUTF8バイト列をBase64表現にエンコードする。</summary>
    /// <param name="self">文字列。nullは空文字列と同じ扱い。</param>
    /// <param name="wrap">エンコードテキストに一定の幅で改行を含めるかどうか</param>
    /// <returns>エンコードしたBase64文字列</returns>
    public static string EncodeUtf8Base64(this string? self, bool wrap = false)
        => EncodeUtf8Base64(self.AsSpan(), wrap);

    /// <summary>文字列のUTF8バイト列をBase64表現にエンコードする。</summary>
    /// <param name="self">文字列。nullは空文字列と同じ扱い。</param>
    /// <param name="wrap">エンコードテキストに一定の幅で改行を含めるかどうか</param>
    /// <returns>エンコードしたBase64文字列</returns>
    public static string EncodeUtf8Base64(this char[]? self, bool wrap = false)
        => EncodeUtf8Base64(self.AsSpan(), wrap);

    /// <summary>文字列のUTF8バイト列をBase64表現にエンコードする。</summary>
    /// <param name="self">文字列</param>
    /// <param name="wrap">エンコードテキストに一定の幅で改行を含めるかどうか</param>
    /// <returns>エンコードしたBase64文字列</returns>
    public static string EncodeUtf8Base64(this Span<char> self, bool wrap = false)
        => EncodeUtf8Base64(self.AsReadOnly(), wrap);

    /// <summary>文字列のUTF8バイト列をBase64表現にエンコードする。</summary>
    /// <param name="self">文字列</param>
    /// <param name="wrap">エンコードテキストに一定の幅で改行を含めるかどうか</param>
    /// <returns>エンコードしたBase64文字列</returns>
    public static string EncodeUtf8Base64(this ReadOnlySpan<char> self, bool wrap = false)
    {
        var writer = new ArrayBufferWriter<byte>(self.Length / 4 + self.Length);
        var length = Encoding.UTF8.GetBytes(self, writer);
        var option = wrap ? Base64FormattingOptions.InsertLineBreaks : Base64FormattingOptions.None;
        return Convert.ToBase64String(writer.WrittenSpan, option);
    }


    /// <summary>Base64表現の文字列をデコードしUTF8で文字列解釈する。</summary>
    /// <param name="self">Base64文字列。nullは空文字列と同じ扱い。</param>
    /// <returns>デコードした文字列</returns>
    public static string? DecodeUtf8Base64(this string? self)
        => DecodeUtf8Base64(self.AsSpan());

    /// <summary>Base64表現の文字列をデコードしUTF8で文字列解釈する。</summary>
    /// <param name="self">Base64文字列</param>
    /// <returns>デコードした文字列</returns>
    public static string? DecodeUtf8Base64(this ReadOnlySpan<char> self)
    {
        using var buffer = new RentalArray<byte>(1 + (int)(self.Length / 1.3));  // 必要サイズより大きければいいので大体。
        if (Convert.TryFromBase64Chars(self, buffer.Instance, out var written))
        {
            return Encoding.UTF8.GetString(buffer.Instance.AsSpan(0, written));
        }
        return null;
    }

    /// <summary>UTF8エンコードのBase64文字バイト列をデコードしUTF8で文字列解釈する。</summary>
    /// <param name="self">UTF8互換エンコードによるBase64文字バイト列</param>
    /// <returns>デコードした文字列</returns>
    public static string? DecodeUtf8Base64(this Span<byte> self)
        => DecodeUtf8Base64(self.AsReadOnly());

    /// <summary>UTF8エンコードのBase64文字バイト列をデコードしUTF8で文字列解釈する。</summary>
    /// <param name="self">UTF8互換エンコードによるBase64文字バイト列</param>
    /// <returns>デコードした文字列</returns>
    public static string? DecodeUtf8Base64(this ReadOnlySpan<byte> self)
    {
        var length = Base64.GetMaxDecodedFromUtf8Length(self.Length);
        using var buffer = new RentalArray<byte>(length);
        var status = Base64.DecodeFromUtf8(self, buffer.Instance, out var consumed, out var written);
        if (status == OperationStatus.Done && self.Length == consumed)
        {
            return Encoding.UTF8.GetString(buffer.Instance.AsSpan(0, written));
        }
        return null;
    }


    /// <summary>文字列のUTF8バイト列をHEX文字列にエンコードする。</summary>
    /// <param name="self">文字列。nullは空文字列と同じ扱い。</param>
    /// <returns>エンコードしたBase64文字列</returns>
    public static string EncodeUtf8Hex(this string? self)
        => EncodeUtf8Hex(self.AsSpan());

    /// <summary>文字列のUTF8バイト列をHEX文字列にエンコードする。</summary>
    /// <param name="self">文字列。nullは空文字列と同じ扱い。</param>
    /// <returns>エンコードしたBase64文字列</returns>
    public static string EncodeUtf8Hex(this char[]? self)
        => EncodeUtf8Hex(self.AsSpan());

    /// <summary>文字列のUTF8バイト列をHEX文字列にエンコードする。</summary>
    /// <param name="self">文字列</param>
    /// <returns>エンコードしたBase64文字列</returns>
    public static string EncodeUtf8Hex(this Span<char> self)
        => EncodeUtf8Hex(self.AsReadOnly());

    /// <summary>文字列のUTF8バイト列をHEX文字列にエンコードする。</summary>
    /// <param name="self">文字列</param>
    /// <returns>エンコードしたBase64文字列</returns>
    public static string EncodeUtf8Hex(this ReadOnlySpan<char> self)
    {
        var writer = new ArrayBufferWriter<byte>(self.Length / 4 + self.Length);
        var length = Encoding.UTF8.GetBytes(self, writer);
        return Convert.ToHexString(writer.WrittenSpan);
    }


    /// <summary>HEX文字列をUTF8として文字列にデコードする。</summary>
    /// <param name="self">Base64文字列。nullは空文字列と同じ扱い。</param>
    /// <returns>デコードした文字列</returns>
    public static string DecodeUtf8Hex(this string? self)
        => DecodeUtf8Hex(self.AsSpan());

    /// <summary>HEX文字列をUTF8として文字列にデコードする。</summary>
    /// <param name="self">Base64文字列。nullは空シーケンスと同じ扱い。</param>
    /// <returns>デコードした文字列</returns>
    public static string DecodeUtf8Hex(this char[] self)
        => DecodeUtf8Hex(self.AsSpan());

    /// <summary>HEX文字列をUTF8として文字列にデコードする。</summary>
    /// <param name="self">Base64文字列</param>
    /// <returns>デコードした文字列</returns>
    public static string DecodeUtf8Hex(this Span<char> self)
        => DecodeUtf8Hex(self.AsReadOnly());

    /// <summary>HEX文字列をUTF8として文字列にデコードする。</summary>
    /// <param name="self">Base64文字列</param>
    /// <returns>デコードした文字列</returns>
    public static string DecodeUtf8Hex(this ReadOnlySpan<char> self)
        => Encoding.UTF8.GetString(Convert.FromHexString(self));


    /// <summary>バイト列をBase64でエンコードする。</summary>
    /// <param name="self">バイト列。nullは空シーケンスと同じ扱い。</param>
    /// <param name="wrap">エンコードテキストに一定の幅で改行を含めるかどうか</param>
    /// <returns>エンコードした文字列</returns>
    public static string EncodeBase64(this byte[]? self, bool wrap = false)
        => EncodeBase64(self.AsSpan(), wrap);

    /// <summary>バイト列をBase64でエンコードする。</summary>
    /// <param name="self">バイト列</param>
    /// <param name="wrap">エンコードテキストに一定の幅で改行を含めるかどうか</param>
    /// <returns>エンコードした文字列</returns>
    public static string EncodeBase64(this Span<byte> self, bool wrap = false)
        => EncodeBase64(self.AsReadOnly(), wrap);

    /// <summary>バイト列をBase64でエンコードする。</summary>
    /// <param name="self">バイト列</param>
    /// <param name="wrap">エンコードテキストに一定の幅で改行を含めるかどうか</param>
    /// <returns>エンコードした文字列</returns>
    public static string EncodeBase64(this ReadOnlySpan<byte> self, bool wrap = false)
        => Convert.ToBase64String(self, wrap ? Base64FormattingOptions.InsertLineBreaks : Base64FormattingOptions.None);


    /// <summary>Base64表現の文字列をデコードする。</summary>
    /// <param name="self">Base64文字列。nullは空文字列と同じ扱い。</param>
    /// <returns>デコードしたバイト列。でコード失敗時はnull</returns>
    public static byte[]? DecodeBase64(this string? self)
        => DecodeBase64(self.AsSpan());

    /// <summary>Base64表現の文字列をデコードする。</summary>
    /// <param name="self">Base64文字列</param>
    /// <returns>デコードした文字列</returns>
    public static byte[]? DecodeBase64(this Span<char> self)
        => DecodeBase64(self.AsReadOnly());

    /// <summary>Base64表現の文字列をデコードする。</summary>
    /// <param name="self">Base64文字列</param>
    /// <returns>デコードした文字列</returns>
    public static byte[]? DecodeBase64(this ReadOnlySpan<char> self)
    {
        using var buffer = new RentalArray<byte>(1 + (int)(self.Length / 1.3));  // 必要サイズより大きければいいので大体。
        if (Convert.TryFromBase64Chars(self, buffer.Instance, out var written))
        {
            return buffer.Instance.AsSpan(0, written).ToArray();
        }
        return null;
    }

    /// <summary>UTF8エンコードのBase64文字バイト列をデコードする。</summary>
    /// <param name="self">UTF8互換エンコードによるBase64文字バイト列</param>
    /// <returns>デコードした文字列</returns>
    public static byte[]? DecodeBase64(this ReadOnlySpan<byte> self)
    {
        var length = Base64.GetMaxDecodedFromUtf8Length(self.Length);
        using var buffer = new RentalArray<byte>(length);
        var status = Base64.DecodeFromUtf8(self, buffer.Instance, out var consumed, out var written);
        if (status == OperationStatus.Done && self.Length == consumed)
        {
            return buffer.Instance.AsSpan(0, written).ToArray();
        }
        return null;
    }

    /// <summary>バイト列をHEX文字列化する</summary>
    /// <param name="self">バイト列</param>
    /// <param name="delimiter">区切り文字列</param>
    /// <returns>HEX文字列</returns>
    public static string ToHexString(this byte[] self, string? delimiter = default)
        => self.AsReadOnlySpan().ToHexString(delimiter);

    /// <summary>バイト列をHEX文字列化する</summary>
    /// <param name="self">バイト列</param>
    /// <param name="delimiter">区切り文字列</param>
    /// <returns>HEX文字列</returns>
    public static string ToHexString(this ReadOnlyMemory<byte> self, string? delimiter = default)
        => self.Span.ToHexString(delimiter);

    /// <summary>バイト列をHEX文字列化する</summary>
    /// <param name="self">バイト列</param>
    /// <param name="delimiter">区切り文字列</param>
    /// <returns>HEX文字列</returns>
    public static string ToHexString(this ReadOnlySpan<byte> self, string? delimiter = default)
    {
        var builder = new StringBuilder();
        for (var i = 0; i < self.Length; i++)
        {
            if (delimiter != null && i != 0) builder.Append(delimiter);
            builder.Append($"{self[i]:X2}");
        }
        return builder.ToString();
    }


    /// <summary>文字列のURIデータ文字列のルールでエスケープする。</summary>
    /// <param name="self">文字列</param>
    /// <returns>エスケープした文字列</returns>
    public static string EscapeUriData(this string self)
        => Uri.EscapeDataString(self);

    /// <summary>URIデータ文字列のルールでエスケープされた文字列をアンエスケープする。</summary>
    /// <param name="self">文字列</param>
    /// <returns>アンエスケープした文字列</returns>
    public static string UnescapeUriData(this string self)
        => Uri.UnescapeDataString(self);


    /// <summary>文字列をファイル名に利用するためにエスケープする。</summary>
    /// <param name="self">文字列</param>
    /// <returns>必要に応じてエスケープされたファイル名用文字列</returns>
    public static string ToFileName(this string self)
        => PathUtility.EscapeFileName(self);

    /// <summary>文字列を相対パスに利用するためにエスケープする。</summary>
    /// <param name="self">文字列</param>
    /// <returns>必要に応じてエスケープされたファイル名用文字列</returns>
    public static string ToRelativePath(this string self)
        => PathUtility.EscapeRelativePath(self);
}
