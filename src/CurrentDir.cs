namespace Lestaly;

/// <summary>
/// カレントディレクトリに関する補助メソッド
/// </summary>
public static class CurrentDir
{
    /// <summary>カレントディレクトリからの相対パスで示されるファイル情報を取得する。</summary>
    /// <param name="relativePath">相対パス</param>
    /// <returns>相対ファイル情報</returns>
    public static FileInfo GetRelativeFile(string relativePath) => new FileInfo(Path.Combine(Environment.CurrentDirectory, relativePath));

    /// <summary>カレントディレクトリからの相対パスで示されるディレクトリ情報を取得する。</summary>
    /// <param name="relativePath">相対パス</param>
    /// <returns>相対ディレクトリ情報</returns>
    public static DirectoryInfo GetRelativeDirectory(string relativePath) => new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, relativePath));
}
