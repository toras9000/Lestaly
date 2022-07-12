namespace Lestaly;

/// <summary>
/// 呼び出し元ソースファイルに関する補助メソッド
/// </summary>
public static class ThisSource
{
    /// <summary>呼び出し元ソースファイルパスを示すファイル情報を取得する。</summary>
    /// <param name="path">この引数は省略する必要がある。</param>
    /// <returns>呼び出し元ソースファイル情報</returns>
    public static FileInfo GetFile([System.Runtime.CompilerServices.CallerFilePath] string path = "") => new FileInfo(path);

    /// <summary>呼び出し元ソースファイルからの相対パスで示されるファイル情報を取得する。</summary>
    /// <param name="relativePath">相対パス</param>
    /// <param name="path">この引数は省略する必要がある。</param>
    /// <returns>相対ファイル情報</returns>
    public static FileInfo GetRelativeFile(string relativePath, [System.Runtime.CompilerServices.CallerFilePath] string path = "") => new FileInfo(Path.Combine(Path.GetDirectoryName(path)!, relativePath));

    /// <summary>呼び出し元ソースファイルからの相対パスで示されるディレクトリ情報を取得する。</summary>
    /// <param name="relativePath">相対パス</param>
    /// <param name="path">この引数は省略する必要がある。</param>
    /// <returns>相対ディレクトリ情報</returns>
    public static DirectoryInfo GetRelativeDirectory(string relativePath, [System.Runtime.CompilerServices.CallerFilePath] string path = "") => new DirectoryInfo(Path.Combine(Path.GetDirectoryName(path)!, relativePath));
}
