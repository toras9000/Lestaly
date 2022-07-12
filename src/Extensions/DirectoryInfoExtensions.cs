namespace Lestaly;

/// <summary>
/// DirectoryInfo に対する拡張メソッド
/// </summary>
public static class DirectoryInfoExtensions
{
    #region FileSystemInfo
    /// <summary>
    /// ディレクトリからの相対パス位置に対する FileInfo を取得する。
    /// </summary>
    /// <param name="self">基準となるディレクトリの DirectoryInfo</param>
    /// <param name="relativePath">基準ディレクトリからのパス。もし絶対パスの場合は基準ディレクトリは無関係にこの絶対パスが利用される。</param>
    /// <returns>対象ファイルパスの FileInfo</returns>
    public static FileInfo GetRelativeFile(this DirectoryInfo self, string relativePath)
        => CometFlavor.Extensions.IO.DirectoryInfoExtensions.GetRelativeFile(self, relativePath);

    /// <summary>
    /// ディレクトリからの相対パス位置に対する DirectoryInfo を取得する。
    /// </summary>
    /// <param name="self">基準となるディレクトリのDirectoryInfo</param>
    /// <param name="relativePath">基準ディレクトリからのパス。もし絶対パスの場合は基準ディレクトリは無関係にこの絶対パスが利用される。</param>
    /// <returns>対象ディレクトリパスの DirectoryInfo</returns>
    public static DirectoryInfo GetRelativeDirectory(this DirectoryInfo self, string relativePath)
        => CometFlavor.Extensions.IO.DirectoryInfoExtensions.GetRelativeDirectory(self, relativePath);
    #endregion

    #region Path
    /// <summary>
    /// ディレクトリパスの構成セグメントを取得する。
    /// </summary>
    /// <param name="self">対象ディレクトリのDirectoryInfo</param>
    /// <returns>パス構成セグメントのリスト</returns>
    public static IList<string> GetPathSegments(this DirectoryInfo self)
        => CometFlavor.Extensions.IO.DirectoryInfoExtensions.GetPathSegments(self);

    /// <summary>
    /// 指定のディレクトリを起点としたディレクトリの相対パスを取得する。
    /// </summary>
    /// <remarks>
    /// 単純なパス文字列処理であり、リパースポイントなどを解釈することはない。
    /// </remarks>
    /// <param name="self">対象ディレクトリのDirectoryInfo</param>
    /// <param name="baseDir">基準ディレクトリのDirectoryInfo</param>
    /// <param name="ignoreCase">大文字と小文字を同一視するか否か</param>
    /// <returns>相対パス</returns>
    public static string RelativePathFrom(this DirectoryInfo self, DirectoryInfo baseDir, bool ignoreCase)
        => CometFlavor.Extensions.IO.DirectoryInfoExtensions.RelativePathFrom(self, baseDir, ignoreCase);
    #endregion

}
