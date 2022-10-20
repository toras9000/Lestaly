using System.Text.RegularExpressions;

namespace Lestaly;

/// <inheritdoc />
public record SelectFilesOptions : CometFlavor.Extensions.IO.SelectFilesOptions;

/// <summary>列挙したファイル/ディレクトリに対する変換処理デリゲート型</summary>
/// <typeparam name="TResult">変換結果の型</typeparam>
/// <param name="context">変換コンテキストデータ</param>
public delegate void SelectFilesConveter<TResult>(CometFlavor.Extensions.IO.IFileConverter<TResult?> context);

/// <summary>列挙したファイル/ディレクトリに対する非同期変換処理デリゲート型</summary>
/// <typeparam name="TResult">変換結果の型</typeparam>
/// <param name="context">変換コンテキストデータ</param>
/// <returns>処理タスク</returns>
public delegate ValueTask AsyncSelectFilesConveter<TResult>(CometFlavor.Extensions.IO.IFileConverter<TResult?> context);

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

    /// <summary>ディレクトリが指定のディレクトリの子孫であるかを判定する。</summary>
    /// <remarks></remarks>
    /// <param name="self">対象ディレクトリ</param>
    /// <param name="other">比較するディレクトリ</param>
    /// <param name="sameIs">同一階層を真とするか否か</param>
    /// <returns>指定ディレクトリの子孫であるか否か</returns>
    public static bool IsDescendantOf(this DirectoryInfo self, DirectoryInfo other, bool sameIs = true)
        => CometFlavor.Extensions.IO.DirectoryInfoExtensions.IsDescendantOf(self, other, sameIs);

    /// <summary>ディレクトリが指定のディレクトリの祖先であるかを判定する。</summary>
    /// <param name="self">対象ディレクトリ</param>
    /// <param name="other">比較するディレクトリ</param>
    /// <param name="sameIs">同一階層を真とするか否か</param>
    /// <returns>指定ディレクトリの祖先であるか否か</returns>
    public static bool IsAncestorOf(this DirectoryInfo self, DirectoryInfo other, bool sameIs = true)
        => CometFlavor.Extensions.IO.DirectoryInfoExtensions.IsAncestorOf(self, other, sameIs);

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

    #region Search
    /// <summary>ディレクトリ配下のファイル/ディレクトリを検索して変換処理を行う</summary>
    /// <remarks>
    /// ディレクトリ内を列挙する際は最初にファイルを列挙し、オプションで指定されていれば次にサブディレクトリを列挙する。
    /// このメソッドではサブディレクトリ配下の検索に再帰呼び出しを利用する。
    /// ディレクトリ構成によってはスタックを大量に消費する可能性があることに注意。
    /// </remarks>
    /// <typeparam name="TResult">ファイル/ディレクトリに対する変換結果の型</typeparam>
    /// <param name="self">検索の起点ディレクトリ</param>
    /// <param name="selector">ファイル/ディレクトリに対する変換処理</param>
    /// <param name="options">検索オプション</param>
    /// <returns>変換結果のシーケンス</returns>
    public static IEnumerable<TResult?> SelectFiles<TResult>(this DirectoryInfo self, SelectFilesConveter<TResult> selector, SelectFilesOptions? options = null)
    {
        if (selector == null) throw new ArgumentNullException(nameof(selector));
        return CometFlavor.Extensions.IO.DirectoryInfoExtensions.SelectFiles<TResult>(self, c => selector(c), options);
    }

    /// <summary>ディレクトリ配下のファイル/ディレクトリを検索して変換処理を行う</summary>
    /// <typeparam name="TResult">ファイル/ディレクトリに対する変換結果の型</typeparam>
    /// <param name="self">検索の起点ディレクトリ</param>
    /// <param name="selector">ファイル/ディレクトリに対する変換処理</param>
    /// <param name="excludes">列挙対象から除外するパターンのコレクション</param>
    /// <param name="includes">列挙対象に含めるパターンのコレクション。除外されなかったファイルに適用する。nullの場合は全てのファイルが列挙対象。</param>
    /// <param name="options">検索オプション</param>
    /// <returns>変換結果のシーケンス</returns>
    public static IEnumerable<TResult?> SelectFiles<TResult>(this DirectoryInfo self, SelectFilesConveter<TResult> selector, IReadOnlyCollection<Regex> excludes, IReadOnlyCollection<Regex>? includes = null, SelectFilesOptions? options = null)
    {
        if (selector == null) throw new ArgumentNullException(nameof(selector));
        if (excludes == null) throw new ArgumentNullException(nameof(excludes));
        return CometFlavor.Extensions.IO.DirectoryInfoExtensions.SelectFiles<TResult>(self, options: options, selector: c =>
        {
            var name = default(string?);
            // ディレクトリに対する呼び出しであるかを判定
            if (c.File == null)
            {
                // ディレクトリが除外パターンにマッチする場合、そのディレクトリの配下に入らないようフラグを立てる
                if (excludes.Any(e => e.IsMatch(c.Directory.Name))) { c.Break = true; return; }
                // 仲介判定用の名称はディレクトリ名
                name = c.Directory.Name;
            }
            else
            {
                // ファイルが除外パターンにマッチする場合、単に仲介せずに終える。
                if (excludes.Any(e => e.IsMatch(c.File.Name))) { return; }
                // 仲介判定用の名称はファイル名
                name = c.File.Name;
            }
            // 処理対象パターンが無し、もしくは指定されていてパターンに一致する場合に処理を仲介する
            if (includes == null || includes.Any(e => e.IsMatch(name)))
            {
                selector(c);
            }
        });
    }

    /// <summary>ディレクトリ配下のファイル/ディレクトリを検索して変換処理を行う</summary>
    /// <remarks>
    /// ディレクトリ内を列挙する際は最初にファイルを列挙し、オプションで指定されていれば次にサブディレクトリを列挙する。
    /// このメソッドではサブディレクトリ配下の検索に再帰呼び出しを利用する。
    /// ディレクトリ構成によってはスタックを大量に消費する可能性があることに注意。
    /// </remarks>
    /// <typeparam name="TResult">ファイル/ディレクトリに対する変換処理</typeparam>
    /// <param name="self">検索の起点ディレクトリ</param>
    /// <param name="selector">ファイル/ディレクトリに対する変換処理</param>
    /// <param name="options">検索オプション</param>
    /// <returns>変換結果のシーケンス</returns>
    public static IAsyncEnumerable<TResult?> SelectFilesAsync<TResult>(this DirectoryInfo self, AsyncSelectFilesConveter<TResult> selector, SelectFilesOptions? options = null)
    {
        if (selector == null) throw new ArgumentNullException(nameof(selector));
        return CometFlavor.Extensions.IO.DirectoryInfoExtensions.SelectFilesAsync<TResult>(self, c => selector(c), options);
    }

    /// <summary>ディレクトリ配下のファイル/ディレクトリを検索して変換処理を行う</summary>
    /// <typeparam name="TResult">ファイル/ディレクトリに対する変換結果の型</typeparam>
    /// <param name="self">検索の起点ディレクトリ</param>
    /// <param name="selector">ファイル/ディレクトリに対する変換処理</param>
    /// <param name="excludes">列挙対象から除外するパターンのコレクション</param>
    /// <param name="includes">列挙対象に含めるパターンのコレクション。除外されなかったファイルに適用する。nullの場合は全てのファイルが列挙対象。</param>
    /// <param name="options">検索オプション</param>
    /// <returns>変換結果のシーケンス</returns>
    public static IAsyncEnumerable<TResult?> SelectFilesAsync<TResult>(this DirectoryInfo self, AsyncSelectFilesConveter<TResult> selector, IReadOnlyCollection<Regex> excludes, IReadOnlyCollection<Regex>? includes = null, SelectFilesOptions? options = null)
    {
        if (selector == null) throw new ArgumentNullException(nameof(selector));
        if (excludes == null) throw new ArgumentNullException(nameof(excludes));
        return CometFlavor.Extensions.IO.DirectoryInfoExtensions.SelectFilesAsync<TResult>(self, options: options, selector: async c =>
        {
            var name = default(string?);
            // ディレクトリに対する呼び出しであるかを判定
            if (c.File == null)
            {
                // ディレクトリが除外パターンにマッチする場合、そのディレクトリの配下に入らないようフラグを立てる
                if (excludes.Any(e => e.IsMatch(c.Directory.Name))) { c.Break = true; return; }
                // 仲介判定用の名称はディレクトリ名
                name = c.Directory.Name;
            }
            else
            {
                // ファイルが除外パターンにマッチする場合、単に仲介せずに終える。
                if (excludes.Any(e => e.IsMatch(c.File.Name))) { return; }
                // 仲介判定用の名称はファイル名
                name = c.File.Name;
            }
            // 処理対象パターンが無し、もしくは指定されていてパターンに一致する場合に処理を仲介する
            if (includes == null || includes.Any(e => e.IsMatch(name)))
            {
                await selector(c).ConfigureAwait(false);
            }
        });
    }
    #endregion

}
