namespace Lestaly;

/// <summary>
/// Uriに対する拡張メソッド
/// </summary>
public static class UriExtensions
{
    /// <summary>URIの権限セグメントに対するパスで新しいUriを生成する</summary>
    /// <remarks>要するにサーバ相対パスを指定して新しいUriを作る</remarks>
    /// <param name="self">基にするURI</param>
    /// <param name="path">権限セグメントからのパス</param>
    /// <returns>新しいUri</returns>
    public static Uri AuthorityRelative(this Uri self, ReadOnlySpan<char> path)
    {
        var authority = self.GetLeftPart(UriPartial.Authority);
        if (string.IsNullOrWhiteSpace(authority)) return new($"{self.GetLeftPart(UriPartial.Scheme)}{path}");
        if (path.StartsWith("/")) return new($"{authority}{path}");
        return new($"{authority}/{path}");
    }
}
