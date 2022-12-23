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
        => CometFlavor.Extensions.IO.FileInfoExtensions.GetNameWithoutExtension(self);

    /// <summary>
    /// 拡張子を取得する。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <returns>拡張子</returns>
    public static string GetExtension(this FileInfo self)
        => CometFlavor.Extensions.IO.FileInfoExtensions.GetExtension(self);

    /// <summary>
    /// 指定した拡張子ファイルを示すFileInfoを取得する。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="extension">拡張子</param>
    /// <returns> 指定した拡張子ファイルを示すFileInfo</returns>
    public static FileInfo GetAnotherExtension(this FileInfo self, string extension)
        => CometFlavor.Extensions.IO.FileInfoExtensions.GetAnotherExtension(self, extension);
    #endregion

    #region Read
    /// <summary>
    /// ファイル内容の全バイト列を読み出す。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <returns>ファイルから読みだしたバイト列</returns>
    public static byte[] ReadAllBytes(this FileInfo self)
        => CometFlavor.Extensions.IO.FileInfoExtensions.ReadAllBytes(self);

    /// <summary>
    /// ファイル内容の全テキストを読み出す。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <returns>ファイルから読みだした全テキスト</returns>
    public static string ReadAllText(this FileInfo self)
        => CometFlavor.Extensions.IO.FileInfoExtensions.ReadAllText(self);

    /// <summary>
    /// ファイル内容の全テキストを読み出す。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="encoding">ファイル内容をデコードするテキストエンコーディング</param>
    /// <returns>ファイルから読みだした全テキスト</returns>
    public static string ReadAllText(this FileInfo self, Encoding encoding)
        => CometFlavor.Extensions.IO.FileInfoExtensions.ReadAllText(self, encoding);

    /// <summary>
    /// ファイル内容の全テキスト行を読み出す。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <returns>ファイルから読みだした全テキスト行</returns>
    public static string[] ReadAllLines(this FileInfo self)
        => CometFlavor.Extensions.IO.FileInfoExtensions.ReadAllLines(self);

    /// <summary>
    /// ファイル内容の全テキスト行を読み出す。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="encoding">ファイル内容をデコードするテキストエンコーディング</param>
    /// <returns>ファイルから読みだした全テキスト行</returns>
    public static string[] ReadAllLines(this FileInfo self, Encoding encoding)
        => CometFlavor.Extensions.IO.FileInfoExtensions.ReadAllLines(self, encoding);

    /// <summary>
    /// ファイル内容のテキスト行を読み出す。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <returns>ファイルから読みだしたテキスト行</returns>
    public static IEnumerable<string> ReadLines(this FileInfo self)
        => CometFlavor.Extensions.IO.FileInfoExtensions.ReadLines(self);

    /// <summary>
    /// ファイル内容の全テキスト行を読み出す。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="encoding">ファイル内容をデコードするテキストエンコーディング</param>
    /// <returns>ファイルから読みだしたテキスト行</returns>
    public static IEnumerable<string> ReadLines(this FileInfo self, Encoding encoding)
        => CometFlavor.Extensions.IO.FileInfoExtensions.ReadLines(self, encoding);

    /// <summary>
    /// ファイル内容の全バイト列を読み出す。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>ファイルから読みだしたバイト列</returns>
    public static Task<byte[]> ReadAllBytesAsync(this FileInfo self, CancellationToken cancelToken = default)
        => CometFlavor.Extensions.IO.FileInfoExtensions.ReadAllBytesAsync(self, cancelToken);

    /// <summary>
    /// ファイル内容の全テキストを読み出す。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>ファイルから読みだした全テキスト</returns>
    public static Task<string> ReadAllTextAsync(this FileInfo self, CancellationToken cancelToken = default)
        => CometFlavor.Extensions.IO.FileInfoExtensions.ReadAllTextAsync(self, cancelToken);

    /// <summary>
    /// ファイル内容の全テキストを読み出す。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="encoding">ファイル内容をデコードするテキストエンコーディング</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>ファイルから読みだした全テキスト</returns>
    public static Task<string> ReadAllTextAsync(this FileInfo self, Encoding encoding, CancellationToken cancelToken = default)
        => CometFlavor.Extensions.IO.FileInfoExtensions.ReadAllTextAsync(self, encoding, cancelToken);

    /// <summary>
    /// ファイル内容の全テキスト行を読み出す。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>ファイルから読みだした全テキスト行</returns>
    public static Task<string[]> ReadAllLinesAsync(this FileInfo self, CancellationToken cancelToken = default)
        => CometFlavor.Extensions.IO.FileInfoExtensions.ReadAllLinesAsync(self, cancelToken);

    /// <summary>
    /// ファイル内容の全テキスト行を読み出す。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="encoding">ファイル内容をデコードするテキストエンコーディング</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>ファイルから読みだした全テキスト行</returns>
    public static Task<string[]> ReadAllLinesAsync(this FileInfo self, Encoding encoding, CancellationToken cancelToken = default)
        => CometFlavor.Extensions.IO.FileInfoExtensions.ReadAllLinesAsync(self, encoding, cancelToken);

    /// <summary>
    /// ファイル内容をテキストで読み取るリーダーを生成する。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="encoding">ファイル内容をデコードするテキストエンコーディング</param>
    /// <param name="detectBom">BOMを検出してエンコーディングを決定するか否か</param>
    /// <param name="options">元になるファイルストリームを開くオプション</param>
    /// <returns>ストリームリーダー</returns>
    public static StreamReader CreateTextReader(this FileInfo self, bool detectBom = true, FileStreamOptions? options = null, Encoding? encoding = null)
        => CometFlavor.Extensions.IO.FileInfoExtensions.CreateTextReader(self, detectBom, options, encoding);
    #endregion

    #region Write
    /// <summary>
    /// ファイル内容が指定のバイト列となるように書き込む。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="bytes">書き込むバイト列</param>
    public static void WriteAllBytes(this FileInfo self, byte[] bytes)
        => CometFlavor.Extensions.IO.FileInfoExtensions.WriteAllBytes(self, bytes);

    /// <summary>
    /// ファイル内容が指定のテキストとなるように書き込む。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="contents">書き込むテキスト</param>
    public static void WriteAllText(this FileInfo self, string contents)
        => CometFlavor.Extensions.IO.FileInfoExtensions.WriteAllText(self, contents);

    /// <summary>
    /// ファイル内容が指定のテキストとなるように書き込む。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="contents">書き込むテキスト</param>
    /// <param name="encoding">書き込むテキストをエンコードするテキストエンコーディング</param>
    public static void WriteAllText(this FileInfo self, string contents, Encoding encoding)
        => CometFlavor.Extensions.IO.FileInfoExtensions.WriteAllText(self, contents, encoding);

    /// <summary>
    /// ファイル内容が指定のテキスト行となるように書き込む。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="contents">書き込むテキスト行</param>
    public static void WriteAllLines(this FileInfo self, IEnumerable<string> contents)
        => CometFlavor.Extensions.IO.FileInfoExtensions.WriteAllLines(self, contents);

    /// <summary>
    /// ファイル内容が指定のテキスト行となるように書き込む。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="contents">書き込むテキスト行</param>
    /// <param name="encoding">書き込むテキストをエンコードするテキストエンコーディング</param>
    public static void WriteAllLines(this FileInfo self, IEnumerable<string> contents, Encoding encoding)
        => CometFlavor.Extensions.IO.FileInfoExtensions.WriteAllLines(self, contents, encoding);

    /// <summary>
    /// ファイル内容が指定のバイト列となるように書き込む。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="bytes">書き込むバイト列</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    public static ValueTask WriteAllBytesAsync(this FileInfo self, byte[] bytes, CancellationToken cancelToken = default)
        => CometFlavor.Extensions.IO.FileInfoExtensions.WriteAllBytesAsync(self, bytes, cancelToken);

    /// <summary>
    /// ファイル内容が指定のテキストとなるように書き込む。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="contents">書き込むテキスト</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    public static ValueTask WriteAllTextAsync(this FileInfo self, string contents, CancellationToken cancelToken = default)
        => CometFlavor.Extensions.IO.FileInfoExtensions.WriteAllTextAsync(self, contents, cancelToken);

    /// <summary>
    /// ファイル内容が指定のテキストとなるように書き込む。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="contents">書き込むテキスト</param>
    /// <param name="encoding">書き込むテキストをエンコードするテキストエンコーディング</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    public static ValueTask WriteAllTextAsync(this FileInfo self, string contents, Encoding encoding, CancellationToken cancelToken = default)
        => CometFlavor.Extensions.IO.FileInfoExtensions.WriteAllTextAsync(self, contents, encoding, cancelToken);

    /// <summary>
    /// ファイル内容が指定のテキスト行となるように書き込む。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="contents">書き込むテキスト行</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    public static ValueTask WriteAllLinesAsync(this FileInfo self, IEnumerable<string> contents, CancellationToken cancelToken = default)
        => CometFlavor.Extensions.IO.FileInfoExtensions.WriteAllLinesAsync(self, contents, cancelToken);

    /// <summary>
    /// ファイル内容が指定のテキスト行となるように書き込む。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="contents">書き込むテキスト行</param>
    /// <param name="encoding">書き込むテキストをエンコードするテキストエンコーディング</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    public static ValueTask WriteAllLinesAsync(this FileInfo self, IEnumerable<string> contents, Encoding encoding, CancellationToken cancelToken = default)
        => CometFlavor.Extensions.IO.FileInfoExtensions.WriteAllLinesAsync(self, contents, encoding, cancelToken);

    /// <summary>
    /// ファイル内容をテキストで読み取るリーダーを生成する。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="encoding">ファイル内容をデコードするテキストエンコーディング</param>
    /// <param name="append">追記するか否か</param>
    /// <returns>ストリームリーダー</returns>
    public static StreamWriter CreateTextWriter(this FileInfo self, bool append = false, Encoding? encoding = null)
        => CometFlavor.Extensions.IO.FileInfoExtensions.CreateTextWriter(self, append, encoding);

    /// <summary>
    /// ファイル内容をテキストで読み取るリーダーを生成する。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="encoding">ファイル内容をデコードするテキストエンコーディング</param>
    /// <param name="options">元になるファイルストリームを開くオプション</param>
    /// <returns>ストリームリーダー</returns>
    public static StreamWriter CreateTextWriter(this FileInfo self, FileStreamOptions options, Encoding? encoding = null)
        => CometFlavor.Extensions.IO.FileInfoExtensions.CreateTextWriter(self, options, encoding);
    #endregion

    #region FileSystem
    /// <summary>ファイルまでのディレクトリを作成する。</summary>
    /// <param name="self">対象ファイル情報</param>
    /// <returns>元のファイル情報</returns>
    public static FileInfo WithCreate(this FileInfo self)
        => CometFlavor.Extensions.IO.FileInfoExtensions.WithDirectoryCreate(self);

    /// <summary>ファイルの作成または更新日時の更新を行う</summary>
    /// <param name="self">対象ファイル情報</param>
    /// <returns>元のファイル情報</returns>
    public static FileInfo Touch(this FileInfo self)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        self.Directory?.Create();
        using var stream = new FileStream(self.FullName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None, bufferSize: 0);
        self.LastWriteTimeUtc = DateTime.UtcNow;
        return self;
    }
    #endregion

    #region Path
    /// <summary>
    /// ファイルパスの構成セグメントを取得する。
    /// </summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <returns>パス構成セグメントのリスト</returns>
    public static IList<string> GetPathSegments(this FileInfo self)
        => CometFlavor.Extensions.IO.FileInfoExtensions.GetPathSegments(self);

    /// <summary>ファイルが指定のディレクトリの子孫であるかを判定する。</summary>
    /// <remarks></remarks>
    /// <param name="self">対象ファイル</param>
    /// <param name="other">比較するディレクトリ</param>
    /// <returns>指定ディレクトリの子孫であるか否か</returns>
    public static bool IsDescendantOf(this FileInfo self, DirectoryInfo other)
        => CometFlavor.Extensions.IO.FileInfoExtensions.IsDescendantOf(self, other);

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
        => CometFlavor.Extensions.IO.FileInfoExtensions.RelativePathFrom(self, baseDir, ignoreCase);
    #endregion

}
