namespace LestalyTest;

public static class LestalyExtensions
{
    /// <summary>
    /// ディレクトリからの相対パス位置に対する FileInfo を取得する。
    /// </summary>
    /// <param name="self">基準となるディレクトリの DirectoryInfo</param>
    /// <param name="relativePath">基準ディレクトリからのパス。もし絶対パスの場合は基準ディレクトリは無関係にこの絶対パスが利用される。</param>
    /// <returns>対象ファイルパスの FileInf。相対パスが空や空白の場合は null を返却。o</returns>
    public static FileInfo GetRelativeFile(this DirectoryInfo self, string relativePath)
        => self.RelativeFile(relativePath) ?? throw new Exception("empty path");

    /// <summary>
    /// ディレクトリからの相対パス位置に対する DirectoryInfo を取得する。
    /// </summary>
    /// <param name="self">基準となるディレクトリのDirectoryInfo</param>
    /// <param name="relativePath">基準ディレクトリからのパス。もし絶対パスの場合は基準ディレクトリは無関係にこの絶対パスが利用される。</param>
    /// <returns>対象ディレクトリパスの DirectoryInfo。相対パスが空や空白の場合は null を返却。</returns>
    public static DirectoryInfo GetRelativeDirectory(this DirectoryInfo self, string relativePath)
        => self.RelativeDirectory(relativePath) ?? throw new Exception("empty path");
}
