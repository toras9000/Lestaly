namespace Lestaly;

/// <summary>
/// FileSystemInfo に対する拡張メソッド
/// </summary>
public static class FileSystemInfoExtensions
{
    #region FileSystem
    /// <summary>ファイル/ディレクトリがReadOnlyであるかを取得する。</summary>
    /// <param name="self">対象ファイル/ディレクトリ情報</param>
    /// <returns>ReadOnlyであるか否か</returns>
    public static bool GetReadOnly(this FileSystemInfo self)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        return (self.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly;
    }

    /// <summary>ファイル/ディレクトリのReadOnly属性を設定する。</summary>
    /// <param name="self">対象ファイル/ディレクトリ情報</param>
    /// <param name="readOnly">設定するReadOnly状態</param>
    /// <returns>対象ファイル/ディレクトリ情報</returns>
    public static T SetReadOnly<T>(this T self, bool readOnly) where T : FileSystemInfo
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        if (readOnly)
        {
            self.Attributes |= FileAttributes.ReadOnly;
        }
        else
        {
            self.Attributes &= ~FileAttributes.ReadOnly;
        }
        return self;
    }
    #endregion

    #region Path
    /// <summary>ファイルパスの構成セグメントを取得する。</summary>
    /// <param name="self">対象ファイルシステムアイテムのFileSystemInfo</param>
    /// <returns>パス構成セグメントのリスト</returns>
    public static IList<string> GetPathSegments(this FileSystemInfo self)
        => CometFlavor.Extensions.IO.FileSystemInfoExtensions.GetPathSegments(self);

    /// <summary>ファイルが指定のディレクトリの子孫であるかを判定する。</summary>
    /// <remarks></remarks>
    /// <param name="self">対象ファイルシステムアイテムのFileSystemInfo</param>
    /// <param name="other">比較するディレクトリ</param>
    /// <param name="sameIs">同一階層を真とするか否か</param>
    /// <returns>指定ディレクトリの子孫であるか否か</returns>
    public static bool IsDescendantOf(this FileSystemInfo self, DirectoryInfo other, bool sameIs = true)
        => CometFlavor.Extensions.IO.FileSystemInfoExtensions.IsDescendantOf(self, other, sameIs);

    /// <summary>指定のディレクトリを起点としたファイルの相対パスを取得する。</summary>
    /// <remarks>
    /// 単純なパス文字列処理であり、リパースポイントなどを解釈することはない。
    /// </remarks>
    /// <param name="self">対象ファイルシステムアイテムのFileSystemInfo</param>
    /// <param name="baseDir">基準ディレクトリのDirectoryInfo</param>
    /// <param name="ignoreCase">大文字と小文字を同一視するか否か</param>
    /// <returns>相対パス</returns>
    public static string RelativePathFrom(this FileSystemInfo self, DirectoryInfo baseDir, bool ignoreCase)
        => CometFlavor.Extensions.IO.FileSystemInfoExtensions.RelativePathFrom(self, baseDir, ignoreCase);
    #endregion

    #region Check
    /// <summary>ファイルシステムオブジェクトが存在する場合に null に置き換える。</summary>
    /// <param name="self">対象ファイル/ディレクトリ情報</param>
    /// <returns>元のファイル/ディレクトリ情報もしくは null</returns>
    public static TInfo? OmitExists<TInfo>(this TInfo self) where TInfo : FileSystemInfo
        => self.Exists ? default : self;

    /// <summary>ファイルシステムオブジェクトが存在しない場合に例外を送出する。</summary>
    /// <param name="self">対象ファイル/ディレクトリ情報</param>
    /// <returns>元のファイル/ディレクトリ情報</returns>
    public static TInfo? OmitNotExists<TInfo>(this TInfo self) where TInfo : FileSystemInfo
        => self.Exists ? self : default;
    #endregion

    #region Throw
    /// <summary>ファイルシステムオブジェクトが存在する場合に例外を送出する。</summary>
    /// <param name="self">対象ファイル/ディレクトリ情報</param>
    /// <param name="generator">例外オブジェクト生成デリゲート</param>
    /// <returns>元のファイル/ディレクトリ情報</returns>
    public static TInfo ThrowIfExists<TInfo>(this TInfo self, Func<TInfo, Exception>? generator = null) where TInfo : FileSystemInfo
    {
        if (self.Exists)
        {
            throw generator?.Invoke(self) ?? self switch
            {
                FileInfo f => new FileNotFoundException($"`{self.FullName}` is not found.", self.FullName),
                DirectoryInfo d => new DirectoryNotFoundException($"`{self.FullName}` is not found."),
                _ => new InvalidDataException(),
            };
        }
        return self;
    }

    /// <summary>ファイルシステムオブジェクトが存在しない場合に例外を送出する。</summary>
    /// <param name="self">対象ファイル/ディレクトリ情報</param>
    /// <param name="generator">例外オブジェクト生成デリゲート</param>
    /// <returns>元のファイル/ディレクトリ情報</returns>
    public static TInfo ThrowIfNotExists<TInfo>(this TInfo self, Func<TInfo, Exception>? generator = null) where TInfo : FileSystemInfo
    {
        if (!self.Exists)
        {
            throw generator?.Invoke(self) ?? self switch
            {
                FileInfo f => new FileNotFoundException($"`{self.FullName}` is not found.", self.FullName),
                DirectoryInfo d => new DirectoryNotFoundException($"`{self.FullName}` is not found."),
                _ => new InvalidDataException(),
            };
        }
        return self;
    }

    /// <summary>ファイルシステムオブジェクトが存在する場合にキャンセル例外を送出する。</summary>
    /// <param name="self">対象ファイル/ディレクトリ情報</param>
    /// <param name="generator">例外メッセージ生成デリゲート</param>
    /// <returns>元のファイル/ディレクトリ情報</returns>
    public static TInfo CanceIfExists<TInfo>(this TInfo self, Func<TInfo, string>? generator = null) where TInfo : FileSystemInfo
    => self.ThrowIfExists(i => new OperationCanceledException(generator?.Invoke(i)));

    /// <summary>ファイルシステムオブジェクトが存在しない場合にキャンセル例外を送出する。</summary>
    /// <param name="self">対象ファイル/ディレクトリ情報</param>
    /// <param name="generator">例外メッセージ生成デリゲート</param>
    /// <returns>元のファイル/ディレクトリ情報</returns>
    public static TInfo CanceIfNotExists<TInfo>(this TInfo self, Func<TInfo, string>? generator = null) where TInfo : FileSystemInfo
    => self.ThrowIfNotExists(i => new OperationCanceledException(generator?.Invoke(i)));
    #endregion

}
