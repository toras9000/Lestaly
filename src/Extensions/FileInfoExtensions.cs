using System.Text;

namespace Lestaly;

/// <summary>
/// FileInfo に対する拡張メソッド
/// </summary>
public static class FileInfoExtensions
{
    #region Name
    /// <summary>
    /// 拡張子を除いたファイル名を取得する。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <returns>拡張子を除いたファイル名</returns>
    public static string GetNameWithoutExtension(this FileInfo self)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        return Path.GetFileNameWithoutExtension(self.Name);
    }

    /// <summary>
    /// 拡張子を取得する。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <returns>拡張子</returns>
    public static string GetExtension(this FileInfo self)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        return Path.GetExtension(self.Name);
    }

    /// <summary>
    /// 指定した拡張子ファイルを示すFileInfoを取得する。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="extension">拡張子</param>
    /// <returns> 指定した拡張子ファイルを示すFileInfo</returns>
    public static FileInfo GetAnotherExtension(this FileInfo self, string extension)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        var path = Path.ChangeExtension(self.FullName, extension);
        return new FileInfo(path);
    }
    #endregion

    #region Read
    /// <summary>
    /// ファイル内容の全バイト列を読み出す。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <returns>ファイルから読みだしたバイト列</returns>
    public static byte[] ReadAllBytes(this FileInfo self)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        return File.ReadAllBytes(self.FullName);
    }

    /// <summary>
    /// ファイル内容の全テキストを読み出す。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <returns>ファイルから読みだした全テキスト</returns>
    public static string ReadAllText(this FileInfo self)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        return File.ReadAllText(self.FullName);
    }

    /// <summary>
    /// ファイル内容の全テキストを読み出す。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="encoding">ファイル内容をデコードするテキストエンコーディング</param>
    /// <returns>ファイルから読みだした全テキスト</returns>
    public static string ReadAllText(this FileInfo self, Encoding encoding)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        return File.ReadAllText(self.FullName, encoding);
    }

    /// <summary>
    /// ファイル内容の全テキスト行を読み出す。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <returns>ファイルから読みだした全テキスト行</returns>
    public static string[] ReadAllLines(this FileInfo self)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        return File.ReadAllLines(self.FullName);
    }

    /// <summary>
    /// ファイル内容の全テキスト行を読み出す。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="encoding">ファイル内容をデコードするテキストエンコーディング</param>
    /// <returns>ファイルから読みだした全テキスト行</returns>
    public static string[] ReadAllLines(this FileInfo self, Encoding encoding)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        return File.ReadAllLines(self.FullName, encoding);
    }

    /// <summary>
    /// ファイル内容のテキスト行を読み出す。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <returns>ファイルから読みだしたテキスト行</returns>
    public static IEnumerable<string> ReadLines(this FileInfo self)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        return File.ReadLines(self.FullName);
    }

    /// <summary>
    /// ファイル内容の全テキスト行を読み出す。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="encoding">ファイル内容をデコードするテキストエンコーディング</param>
    /// <returns>ファイルから読みだしたテキスト行</returns>
    public static IEnumerable<string> ReadLines(this FileInfo self, Encoding encoding)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        return File.ReadLines(self.FullName, encoding);
    }

    /// <summary>
    /// ファイル内容の全バイト列を読み出す。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>ファイルから読みだしたバイト列</returns>
    public static Task<byte[]> ReadAllBytesAsync(this FileInfo self, CancellationToken cancelToken = default)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        return File.ReadAllBytesAsync(self.FullName, cancelToken);
    }

    /// <summary>
    /// ファイル内容の全テキストを読み出す。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>ファイルから読みだした全テキスト</returns>
    public static Task<string> ReadAllTextAsync(this FileInfo self, CancellationToken cancelToken = default)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        return File.ReadAllTextAsync(self.FullName, cancelToken);
    }

    /// <summary>
    /// ファイル内容の全テキストを読み出す。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="encoding">ファイル内容をデコードするテキストエンコーディング</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>ファイルから読みだした全テキスト</returns>
    public static Task<string> ReadAllTextAsync(this FileInfo self, Encoding encoding, CancellationToken cancelToken = default)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        return File.ReadAllTextAsync(self.FullName, encoding, cancelToken);
    }

    /// <summary>
    /// ファイル内容の全テキスト行を読み出す。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>ファイルから読みだした全テキスト行</returns>
    public static Task<string[]> ReadAllLinesAsync(this FileInfo self, CancellationToken cancelToken = default)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        return File.ReadAllLinesAsync(self.FullName, cancelToken);
    }

    /// <summary>
    /// ファイル内容の全テキスト行を読み出す。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="encoding">ファイル内容をデコードするテキストエンコーディング</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>ファイルから読みだした全テキスト行</returns>
    public static Task<string[]> ReadAllLinesAsync(this FileInfo self, Encoding encoding, CancellationToken cancelToken = default)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        return File.ReadAllLinesAsync(self.FullName, encoding, cancelToken);
    }
    #endregion

    #region Write
    /// <summary>
    /// ファイル内容が指定のバイト列となるように書き込む。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="bytes">書き込むバイト列</param>
    public static void WriteAllBytes(this FileInfo self, byte[] bytes)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        File.WriteAllBytes(self.FullName, bytes);
        self.Refresh();
    }

    /// <summary>
    /// ファイル内容が指定のテキストとなるように書き込む。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="contents">書き込むテキスト</param>
    public static void WriteAllText(this FileInfo self, string contents)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        File.WriteAllText(self.FullName, contents);
        self.Refresh();
    }

    /// <summary>
    /// ファイル内容が指定のテキストとなるように書き込む。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="contents">書き込むテキスト</param>
    /// <param name="encoding">書き込むテキストをエンコードするテキストエンコーディング</param>
    public static void WriteAllText(this FileInfo self, string contents, Encoding encoding)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        File.WriteAllText(self.FullName, contents, encoding);
        self.Refresh();
    }

    /// <summary>
    /// ファイル内容が指定のテキスト行となるように書き込む。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="contents">書き込むテキスト行</param>
    public static void WriteAllLines(this FileInfo self, IEnumerable<string> contents)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        File.WriteAllLines(self.FullName, contents);
        self.Refresh();
    }

    /// <summary>
    /// ファイル内容が指定のテキスト行となるように書き込む。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="contents">書き込むテキスト行</param>
    /// <param name="encoding">書き込むテキストをエンコードするテキストエンコーディング</param>
    public static void WriteAllLines(this FileInfo self, IEnumerable<string> contents, Encoding encoding)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        File.WriteAllLines(self.FullName, contents, encoding);
        self.Refresh();
    }

#if NETCOREAPP2_0_OR_GREATER
    /// <summary>
    /// ファイル内容が指定のバイト列となるように書き込む。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="bytes">書き込むバイト列</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    public static async ValueTask WriteAllBytesAsync(this FileInfo self, byte[] bytes, CancellationToken cancelToken = default)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        await File.WriteAllBytesAsync(self.FullName, bytes, cancelToken).ConfigureAwait(false);
        self.Refresh();
    }

    /// <summary>
    /// ファイル内容が指定のテキストとなるように書き込む。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="contents">書き込むテキスト</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    public static async ValueTask WriteAllTextAsync(this FileInfo self, string contents, CancellationToken cancelToken = default)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        await File.WriteAllTextAsync(self.FullName, contents, cancelToken).ConfigureAwait(false);
        self.Refresh();
    }

    /// <summary>
    /// ファイル内容が指定のテキストとなるように書き込む。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="contents">書き込むテキスト</param>
    /// <param name="encoding">書き込むテキストをエンコードするテキストエンコーディング</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    public static async ValueTask WriteAllTextAsync(this FileInfo self, string contents, Encoding encoding, CancellationToken cancelToken = default)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        await File.WriteAllTextAsync(self.FullName, contents, encoding, cancelToken).ConfigureAwait(false);
        self.Refresh();
    }

    /// <summary>
    /// ファイル内容が指定のテキスト行となるように書き込む。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="contents">書き込むテキスト行</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    public static async ValueTask WriteAllLinesAsync(this FileInfo self, IEnumerable<string> contents, CancellationToken cancelToken = default)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        await File.WriteAllLinesAsync(self.FullName, contents, cancelToken);
        self.Refresh();
    }

    /// <summary>
    /// ファイル内容が指定のテキスト行となるように書き込む。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="contents">書き込むテキスト行</param>
    /// <param name="encoding">書き込むテキストをエンコードするテキストエンコーディング</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    public static async ValueTask WriteAllLinesAsync(this FileInfo self, IEnumerable<string> contents, Encoding encoding, CancellationToken cancelToken = default)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        await File.WriteAllLinesAsync(self.FullName, contents, encoding).ConfigureAwait(false);
        self.Refresh();
    }
#endif
    #endregion

    #region Path
    /// <summary>
    /// ファイルパスの構成セグメントを取得する。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <returns>パス構成セグメントのリスト</returns>
    public static IList<string> GetPathSegments(this FileInfo self)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));

        // まずファイル名を処理
        var segments = new List<string>(10);
        segments.Add(self.Name);

        // 構成ディレクトリを取得
        var part = self.Directory;
        while (part != null)
        {
            segments.Add(part.Name);
            part = part.Parent;
        }

        // リスト内容を逆順にする
        segments.Reverse();

        return segments;
    }

    /// <summary>
    /// 指定のディレクトリを起点としたファイルの相対パスを取得する。
    /// </summary>
    /// <remarks>
    /// 単純なパス文字列処理であり、リパースポイントなどを解釈することはない。
    /// </remarks>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="baseDir">基準ディレクトリのDirectoryInfo</param>
    /// <param name="ignoreCase">大文字と小文字を同一視するか否か</param>
    /// <returns>相対パス</returns>
    public static string RelativePathFrom(this FileInfo self, DirectoryInfo baseDir, bool ignoreCase)
    {
        // パラメータチェック
        if (self == null) throw new ArgumentNullException(nameof(self));
        if (baseDir == null) throw new ArgumentNullException(nameof(baseDir));

        return DirectoryInfoExtensions.GegmentsToReletivePath(self.GetPathSegments(), baseDir, ignoreCase);
    }
    #endregion
}
