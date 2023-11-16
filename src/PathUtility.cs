using System.Text;

namespace Lestaly;

/// <summary>
/// パス関係のユーティリティ
/// </summary>
public static class PathUtility
{
    /// <summary>文字列がファイル名として有効であるかを判定する</summary>
    /// <remarks>空や空白文字のみは無効とみなす。</remarks>
    /// <param name="text">対象文字列</param>
    /// <returns>ファイル名として有効であるか否か</returns>
    public static bool IsValidFileName(string text)
        => !text.AsSpan().Trim().IsEmpty && text.IndexOfAny(InvalidFileNameCharsCache) < 0;

    /// <summary>文字列が相対パスとして有効であるかを判定する</summary>
    /// <remarks>空や空白文字のみは無効とみなす。</remarks>
    /// <param name="text">対象文字列</param>
    /// <returns>ファイル名として有効であるか否か</returns>
    public static bool IsValidRelativePath(string text)
        => !text.AsSpan().Trim().IsEmpty && text.IndexOfAny(InvalidRelativePathCharsCache) < 0;

    /// <summary>文字列をファイル名に利用するためにエスケープする。</summary>
    /// <remarks>長さについては考慮しない。</remarks>
    /// <param name="text">対象文字列</param>
    /// <returns>必要に応じてエスケープされたファイル名用文字列</returns>
    public static string EscapeFileName(string text)
        => escapeName(text, EscapeFileNameCharsCache);

    /// <summary>文字列を相対パスに利用するためにエスケープする。</summary>
    /// <remarks>長さについては考慮しない。</remarks>
    /// <param name="text">対象文字列</param>
    /// <returns>必要に応じてエスケープされた相対パス用文字列</returns>
    public static string EscapeRelativePath(string text)
        => escapeName(text, EscapeRelativePathCharsCache);

    // 構築(クラス)
    #region 静的コンストラクタ
    /// <summary>静的コンストラクタ</summary>
    static PathUtility()
    {
        // ファイル名として無効なキャラクタをキャッシュ
        InvalidFileNameCharsCache = Path.GetInvalidFileNameChars();

        // 相対パスとして無効なキャラクタをキャッシュ
        var separators = new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar, };
        InvalidRelativePathCharsCache = InvalidFileNameCharsCache.Except(separators).ToArray();

        // ファイル名のエスケープ対象キャラクタをキャッシュ
        EscapeFileNameCharsCache = InvalidFileNameCharsCache.Append('%').ToArray();

        // 相対パスのエスケープ対象キャラクタをキャッシュ
        EscapeRelativePathCharsCache = InvalidRelativePathCharsCache.Append('%').ToArray();
    }
    #endregion

    // 非公開フィールド
    #region キャッシュ
    /// <summary>ファイル名として無効なキャラクタのキャッシュ</summary>
    private static readonly char[] InvalidFileNameCharsCache;

    /// <summary>相対パスとして無効なキャラクタのキャッシュ</summary>
    private static readonly char[] InvalidRelativePathCharsCache;

    /// <summary>ファイル名のエスケープ対象キャラクタのキャッシュ</summary>
    private static readonly char[] EscapeFileNameCharsCache;

    /// <summary>相対パスのエスケープ対象キャラクタのキャッシュ</summary>
    private static readonly char[] EscapeRelativePathCharsCache;
    #endregion

    // 非公開メソッド
    #region エスケープ
    /// <summary>文字列の対象キャラクタをパーセントエスケープする</summary>
    /// <param name="text">対象文字列</param>
    /// <param name="escapeChars">エスケープキャラクタ</param>
    /// <returns>エスケープされた文字列</returns>
    private static string escapeName(string text, ReadOnlySpan<char> escapeChars)
    {
        // 前後空白トリムした部分を用いる
        var name = text.AsSpan().Trim();
        if (name.IsEmpty) throw new ArgumentException($"Invlid name.");

        // エスケープが必要だった場合に
        var builder = default(StringBuilder);

        // エスケープした文字列を作る
        while (!name.IsEmpty)
        {
            // エスケープ対象キャラクタを探す
            var idx = name.IndexOfAny(escapeChars);
            if (idx < 0)
            {
                // 見つからなければエスケープ処理終了。残りをバッファに追加。
                builder?.Append(name);
                break;
            }
            else
            {
                // エスケープ対象キャラクタをパーセントエンコード。
                if (builder == null) builder = new();
                builder.Append(name.Slice(0, idx));
                builder.Append('%').Append($"{(ushort)name[idx]:X2}");
                // 次のキャラクタに進む
                name = name.Slice(idx + 1);
            }
        }

        // エスケープした場合はそれを返却
        if (builder != null) return builder.ToString();

        // エスケープはないがトリムしているならばそれを返却
        if (text.Length != name.Length) return name.ToString();

        // そうでもなければ元のまま返却
        return text;
    }
    #endregion
}
