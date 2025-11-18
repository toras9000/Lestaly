using System.Runtime.CompilerServices;

namespace Lestaly;

/// <summary>
/// 呼び出し元ソースファイルに関する補助メソッド
/// </summary>
public static class ThisSource
{
    /// <summary>呼び出し元ソースファイルパスを示すファイル情報を取得する。</summary>
    /// <param name="path">この引数は省略する必要がある。</param>
    /// <returns>呼び出し元ソースファイル情報</returns>
    public static FileInfo File([CallerFilePath] string path = "")
        => new(path);

    /// <summary>呼び出し元ソースファイルからの相対パスで示されるファイル情報を取得する。</summary>
    /// <param name="relativePath">相対パス</param>
    /// <param name="path">この引数は省略する必要がある。</param>
    /// <returns>対象ファイルパスの FileInfo。相対パスが空や空白の場合は null を返却。</returns>
    public static FileInfo? RelativeFileAt(string? relativePath, [CallerFilePath] string path = "")
        => string.IsNullOrWhiteSpace(relativePath) ? default : new FileInfo(Path.Combine(Path.GetDirectoryName(path)!, relativePath));

    /// <summary>呼び出し元ソースファイルからの相対パスで示されるファイル情報を取得する。</summary>
    /// <param name="relativePath">相対パス</param>
    /// <param name="path">この引数は省略する必要がある。</param>
    /// <returns>対象ファイルパスの FileInfo。相対パスが空や空白の場合は例外を送出。</returns>
    public static FileInfo RelativeFile(string relativePath, [CallerFilePath] string path = "")
        => string.IsNullOrWhiteSpace(relativePath) ? throw new ArgumentException("Invalid relative path") : new FileInfo(Path.Combine(Path.GetDirectoryName(path)!, relativePath));

    /// <summary>呼び出し元ソースファイルからの相対パスで示されるディレクトリ情報を取得する。</summary>
    /// <param name="relativePath">相対パス</param>
    /// <param name="path">この引数は省略する必要がある。</param>
    /// <returns>対象ディレクトリパスの DirectoryInfo。相対パスが空や空白の場合は null を返却。</returns>
    public static DirectoryInfo? RelativeDirectoryAt(string? relativePath, [CallerFilePath] string path = "")
        => string.IsNullOrWhiteSpace(relativePath) ? default : new DirectoryInfo(Path.Combine(Path.GetDirectoryName(path)!, relativePath));

    /// <summary>呼び出し元ソースファイルからの相対パスで示されるディレクトリ情報を取得する。</summary>
    /// <param name="relativePath">相対パス</param>
    /// <param name="path">この引数は省略する必要がある。</param>
    /// <returns>対象ディレクトリパスの DirectoryInfo。相対パスが空や空白の場合は基準ディレクトリを返却。</returns>
    public static DirectoryInfo RelativeDirectory(string? relativePath, [CallerFilePath] string path = "")
        => new(Path.Combine(Path.GetDirectoryName(path)!, relativePath ?? ""));

    /// <summary>呼び出し元行番号を取得する</summary>
    /// <param name="lineNumber">この引数は省略する必要がある。</param>
    /// <returns>行番号</returns>
    public static int LineNumber([CallerLineNumber] int lineNumber = 0) => lineNumber;

    /// <summary>呼び出し元メンバー名を取得する</summary>
    /// <param name="member">この引数は省略する必要がある。</param>
    /// <returns>メンバー名</returns>
    public static string MemberName([CallerMemberName] string member = "") => member;

    /// <summary>指定の名前の祖先ディレクトリを探す</summary>
    /// <param name="name">ディレクトリ名</param>
    /// <param name="ignoreCase">大文字と小文字を同一視するか否か</param>
    /// <param name="path">この引数は省略する必要がある。</param>
    /// <returns>条件にマッチしたディレクトリ。見つからない場合は null</returns>
    public static DirectoryInfo? FindAncestor(string name, bool? ignoreCase = default, [CallerFilePath] string path = "")
        => new FileInfo(path).FindAncestor(name, ignoreCase);

    /// <summary>指定の条件にマッチする祖先ディレクトリを探す</summary>
    /// <param name="finder">条件にマッチするかを判定するデリゲート</param>
    /// <param name="path">この引数は省略する必要がある。</param>
    /// <returns>条件にマッチしたディレクトリ。見つからない場合は null</returns>
    public static DirectoryInfo? FindAncestor(Func<DirectoryInfo, bool> finder, [CallerFilePath] string path = "")
        => new FileInfo(path).FindAncestor(finder);
}
