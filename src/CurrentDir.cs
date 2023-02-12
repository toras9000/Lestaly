namespace Lestaly;

/// <summary>
/// カレントディレクトリに関する補助メソッド
/// </summary>
public static class CurrentDir
{
    /// <summary>カレントディレクトリからの相対パスで示されるファイル情報を取得する。</summary>
    /// <param name="relativePath">相対パス</param>
    /// <returns>相対ファイル情報。相対パスが空や空白の場合は null を返却。</returns>
    public static FileInfo? RelativeFile(string relativePath)
        => string.IsNullOrWhiteSpace(relativePath) ? default : new FileInfo(Path.Combine(Environment.CurrentDirectory, relativePath));

    /// <summary>カレントディレクトリからの相対パスで示されるディレクトリ情報を取得する。</summary>
    /// <param name="relativePath">相対パス</param>
    /// <returns>相対ディレクトリ情報。相対パスが空や空白の場合は null を返却。</returns>
    public static DirectoryInfo? RelativeDirectory(string relativePath)
        => string.IsNullOrWhiteSpace(relativePath) ? default : new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, relativePath));
}
