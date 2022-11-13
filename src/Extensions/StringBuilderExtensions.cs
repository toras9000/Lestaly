using System.Text;

namespace Lestaly;

/// <summary>
/// StringBuilder に対する拡張メソッド
/// </summary>
public static class StringBuilderExtensions
{
    /// <summary>文字列がnullや空であるかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>nullや空ならば true</returns>
    public static bool IsEmpty(this StringBuilder self) => self.Length == 0;

    /// <summary>文字列がnullや空以外であるかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>nullや空以外であれば true</returns>
    public static bool IsNotEmpty(this StringBuilder self) => !self.IsEmpty();

    /// <summary>文字列がnullや空白文字であるかを判定する</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>nullや空白文字ならば true</returns>
    public static bool IsWhite(this StringBuilder self)
        => CometFlavor.Extensions.Text.StringBuilderExtensions.IsWhite(self);

    /// <summary>文字列がnullや空白文字以外であるかを判定する</summary>
    /// <param name="self">対象文字列</param>
    /// <returns>nullや空白文字以外ならば true</returns>
    public static bool IsNotWhite(this StringBuilder self) => !self.IsWhite();

    /// <summary>文字列が指定の文字列で始まるかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="value">一致判定文字列</param>
    /// <returns>一致するか否か</returns>
    public static bool StartsWith(this StringBuilder self, string value)
        => CometFlavor.Extensions.Text.StringBuilderExtensions.StartsWith(self, value);

    /// <summary>文字列が指定の文字列で始まるかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="value">一致判定文字列</param>
    /// <param name="comparison">文字列比較方法</param>
    /// <returns>一致するか否か</returns>
    public static bool StartsWith(this StringBuilder self, string value, StringComparison comparison)
        => CometFlavor.Extensions.Text.StringBuilderExtensions.StartsWith(self, value, comparison);

    /// <summary>文字列が指定の文字列で終端するかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="value">一致判定文字列</param>
    /// <returns>一致するか否か</returns>
    public static bool EndsWith(this StringBuilder self, string value)
        => CometFlavor.Extensions.Text.StringBuilderExtensions.EndsWith(self, value);

    /// <summary>文字列が指定の文字列で終端するかを判定する。</summary>
    /// <param name="self">対象文字列</param>
    /// <param name="value">一致判定文字列</param>
    /// <param name="comparison">文字列比較方法</param>
    /// <returns>一致するか否か</returns>
    public static bool EndsWith(this StringBuilder self, string value, StringComparison comparison)
        => CometFlavor.Extensions.Text.StringBuilderExtensions.EndsWith(self, value, comparison);
}
