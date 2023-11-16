using System.Buffers;
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
    {
        return Encoding.UTF8.GetString(self);
    }

    /// <summary>文字列をUTF8エンコードしてバッファに書き込む</summary>
    /// <param name="self">書き込み先バッファ</param>
    /// <param name="text">書き込み文字列</param>
    /// <returns>書き込んだバイト数</returns>
    public static int WriteUtf8(this Span<byte> self, ReadOnlySpan<char> text)
    {
        return Encoding.UTF8.GetBytes(text, self);
    }

    /// <summary>文字列をUTF8バイト列にエンコードする。</summary>
    /// <param name="self">バイト列</param>
    /// <returns>エンコードしたバイト列</returns>
    public static byte[] EncodeUtf8(this string self)
    {
        ArgumentNullException.ThrowIfNull(self);
        return EncodeUtf8(self.AsSpan());
    }

    /// <summary>文字列をUTF8バイト列にエンコードする。</summary>
    /// <param name="self">バイト列</param>
    /// <returns>エンコードしたバイト列</returns>
    public static byte[] EncodeUtf8(this char[] self)
    {
        ArgumentNullException.ThrowIfNull(self);
        return EncodeUtf8(self.AsSpan());
    }

    /// <summary>文字列をUTF8バイト列にエンコードする。</summary>
    /// <param name="self">バイト列</param>
    /// <returns>エンコードしたバイト列</returns>
    public static byte[] EncodeUtf8(this Span<char> self) => EncodeUtf8((ReadOnlySpan<char>)self);

    /// <summary>文字列をUTF8バイト列にエンコードする。</summary>
    /// <param name="self">バイト列</param>
    /// <returns>エンコードしたバイト列</returns>
    public static byte[] EncodeUtf8(this ReadOnlySpan<char> self)
    {
        var writer = new ArrayBufferWriter<byte>(self.Length);
        var length = Encoding.UTF8.GetBytes(self, writer);
        return writer.WrittenSpan.ToArray();
    }


    /// <summary>バイト列をUTF8として文字列にデコードする。</summary>
    /// <param name="self">バイト列</param>
    /// <returns>デコードした文字列</returns>
    public static string DecodeUtf8(this byte[] self)
    {
        ArgumentNullException.ThrowIfNull(self);
        return DecodeUtf8(self.AsSpan());
    }

    /// <summary>バイト列をUTF8として文字列にデコードする。</summary>
    /// <param name="self">バイト列</param>
    /// <returns>デコードした文字列</returns>
    public static string DecodeUtf8(this Span<byte> self) => DecodeUtf8((ReadOnlySpan<byte>)self);

    /// <summary>バイト列をUTF8として文字列にデコードする。</summary>
    /// <param name="self">バイト列</param>
    /// <returns>デコードした文字列</returns>
    public static string DecodeUtf8(this ReadOnlySpan<byte> self)
    {
        return Encoding.UTF8.GetString(self);
    }


    /// <summary>文字列のUTF8バイト列をBase64文字列にエンコードする。</summary>
    /// <param name="self">文字列</param>
    /// <param name="wrap">エンコードテキストに一定の幅で改行を含めるかどうか</param>
    /// <returns>エンコードしたBase64文字列</returns>
    public static string EncodeUtf8Base64(this string self, bool wrap = false)
    {
        ArgumentNullException.ThrowIfNull(self);
        return EncodeUtf8Base64(self.AsSpan(), wrap);
    }

    /// <summary>文字列のUTF8バイト列をBase64文字列にエンコードする。</summary>
    /// <param name="self">文字列</param>
    /// <param name="wrap">エンコードテキストに一定の幅で改行を含めるかどうか</param>
    /// <returns>エンコードしたBase64文字列</returns>
    public static string EncodeUtf8Base64(this char[] self, bool wrap = false)
    {
        ArgumentNullException.ThrowIfNull(self);
        return EncodeUtf8Base64(self.AsSpan(), wrap);
    }

    /// <summary>文字列のUTF8バイト列をBase64文字列にエンコードする。</summary>
    /// <param name="self">文字列</param>
    /// <param name="wrap">エンコードテキストに一定の幅で改行を含めるかどうか</param>
    /// <returns>エンコードしたBase64文字列</returns>
    public static string EncodeUtf8Base64(this Span<char> self, bool wrap = false) => EncodeUtf8Base64((ReadOnlySpan<char>)self, wrap);

    /// <summary>文字列のUTF8バイト列をBase64文字列にエンコードする。</summary>
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


    /// <summary>Base64文字列をUTF8として文字列にデコードする。</summary>
    /// <param name="self">Base64文字列</param>
    /// <returns>デコードした文字列</returns>
    public static string DecodeUtf8Base64(this string self)
    {
        var bin = Convert.FromBase64String(self);
        return Encoding.UTF8.GetString(bin);
    }


    /// <summary>文字列のUTF8バイト列をHEX文字列にエンコードする。</summary>
    /// <param name="self">文字列</param>
    /// <returns>エンコードしたBase64文字列</returns>
    public static string EncodeUtf8Hex(this string self)
    {
        ArgumentNullException.ThrowIfNull(self);
        return EncodeUtf8Hex(self.AsSpan());
    }

    /// <summary>文字列のUTF8バイト列をHEX文字列にエンコードする。</summary>
    /// <param name="self">文字列</param>
    /// <returns>エンコードしたBase64文字列</returns>
    public static string EncodeUtf8Hex(this char[] self)
    {
        ArgumentNullException.ThrowIfNull(self);
        return EncodeUtf8Hex(self.AsSpan());
    }

    /// <summary>文字列のUTF8バイト列をHEX文字列にエンコードする。</summary>
    /// <param name="self">文字列</param>
    /// <returns>エンコードしたBase64文字列</returns>
    public static string EncodeUtf8Hex(this Span<char> self) => EncodeUtf8Hex((ReadOnlySpan<char>)self);

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
    /// <param name="self">Base64文字列</param>
    /// <returns>デコードした文字列</returns>
    public static string DecodeUtf8Hex(this string self)
    {
        ArgumentNullException.ThrowIfNull(self);
        return DecodeUtf8Hex(self.AsSpan());
    }

    /// <summary>HEX文字列をUTF8として文字列にデコードする。</summary>
    /// <param name="self">Base64文字列</param>
    /// <returns>デコードした文字列</returns>
    public static string DecodeUtf8Hex(this char[] self)
    {
        ArgumentNullException.ThrowIfNull(self);
        return DecodeUtf8Hex(self.AsSpan());
    }

    /// <summary>HEX文字列をUTF8として文字列にデコードする。</summary>
    /// <param name="self">Base64文字列</param>
    /// <returns>デコードした文字列</returns>
    public static string DecodeUtf8Hex(this Span<char> self) => DecodeUtf8Hex((ReadOnlySpan<char>)self);

    /// <summary>HEX文字列をUTF8として文字列にデコードする。</summary>
    /// <param name="self">Base64文字列</param>
    /// <returns>デコードした文字列</returns>
    public static string DecodeUtf8Hex(this ReadOnlySpan<char> self)
    {
        var bin = Convert.FromHexString(self);
        return Encoding.UTF8.GetString(bin);
    }


    /// <summary>文字列のURIデータ文字列のルールでエスケープする。</summary>
    /// <param name="self">文字列</param>
    /// <returns>エスケープした文字列</returns>
    public static string EscapeUriData(this string self) => Uri.EscapeDataString(self);

    /// <summary>URIデータ文字列のルールでエスケープされた文字列をアンエスケープする。</summary>
    /// <param name="self">文字列</param>
    /// <returns>アンエスケープした文字列</returns>
    public static string UnescapeUriData(this string self) => Uri.UnescapeDataString(self);


    /// <summary>文字列をファイル名に利用するためにエスケープする。</summary>
    /// <param name="self">文字列</param>
    /// <returns>必要に応じてエスケープされたファイル名用文字列</returns>
    public static string ToFileName(this string self) => PathUtility.EscapeFileName(self);

    /// <summary>文字列を相対パスに利用するためにエスケープする。</summary>
    /// <param name="self">文字列</param>
    /// <returns>必要に応じてエスケープされたファイル名用文字列</returns>
    public static string ToRelativePath(this string self) => PathUtility.EscapeRelativePath(self);
}
