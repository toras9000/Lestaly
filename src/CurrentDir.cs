namespace Lestaly;

/// <summary>
/// カレントディレクトリに関する補助メソッド
/// </summary>
public static class CurrentDir
{
    /// <summary>カレントディレクトリからの相対パスで示されるファイル情報を取得する。</summary>
    /// <param name="relativePath">相対パス</param>
    /// <returns>対象ファイルパスの FileInfo。相対パスが空や空白の場合は null を返却。</returns>
    public static FileInfo? RelativeFileAt(string? relativePath)
        => string.IsNullOrWhiteSpace(relativePath) ? default : new FileInfo(Path.Combine(Environment.CurrentDirectory, relativePath));

    /// <summary>カレントディレクトリからの相対パスで示されるファイル情報を取得する。</summary>
    /// <param name="relativePath">相対パス</param>
    /// <returns>対象ファイルパスの FileInfo。相対パスが空や空白の場合は例外を送出。</returns>
    public static FileInfo? RelativeFile(string relativePath)
        => string.IsNullOrWhiteSpace(relativePath) ? throw new ArgumentException("Invalid relative path") : new FileInfo(Path.Combine(Environment.CurrentDirectory, relativePath));

    /// <summary>カレントディレクトリからの相対パスで示されるディレクトリ情報を取得する。</summary>
    /// <param name="relativePath">相対パス</param>
    /// <returns>対象ディレクトリパスの DirectoryInfo。相対パスが空や空白の場合は null を返却。</returns>
    public static DirectoryInfo? RelativeDirectoryAt(string? relativePath)
        => string.IsNullOrWhiteSpace(relativePath) ? default : new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, relativePath));

    /// <summary>カレントディレクトリからの相対パスで示されるディレクトリ情報を取得する。</summary>
    /// <param name="relativePath">相対パス</param>
    /// <returns>対象ディレクトリパスの DirectoryInfo。相対パスが空や空白の場合は基準ディレクトリを返却。</returns>
    public static DirectoryInfo? RelativeDirectory(string? relativePath)
        => new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, relativePath ?? ""));
}
