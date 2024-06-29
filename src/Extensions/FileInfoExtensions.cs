using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;

namespace Lestaly;

/// <summary>
/// FileInfo に対する拡張メソッド
/// </summary>
public static class FileInfoExtensions
{
    #region Name
    /// <summary>拡張子を除いたファイル名を取得する。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <returns>拡張子を除いたファイル名</returns>
    public static string BaseName(this FileInfo self)
    {
        ArgumentNullException.ThrowIfNull(self);
        return Path.GetFileNameWithoutExtension(self.Name);
    }

    /// <summary>拡張子を取得する。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <returns>拡張子</returns>
    public static string Extension(this FileInfo self)
    {
        ArgumentNullException.ThrowIfNull(self);
        return self.Extension;
    }

    /// <summary>指定した拡張子ファイルを示すFileInfoを取得する。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="extension">拡張子</param>
    /// <returns> 指定した拡張子ファイルを示すFileInfo</returns>
    public static FileInfo GetAnotherExtension(this FileInfo self, string extension)
    {
        ArgumentNullException.ThrowIfNull(self);
        var path = Path.ChangeExtension(self.FullName, extension);
        return new FileInfo(path);
    }

    /// <summary>ファイルが指定したいずれかの拡張子であるか否かを判定する。)</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="extensions">拡張子のリスト</param>
    /// <param name="ignoreCase">大文字/小文字の違いを無視するか否か</param>
    /// <returns>いずれかの拡張子であるか否か</returns>
    public static bool HasAnyExtension(this FileInfo self, IEnumerable<string> extensions, bool ignoreCase = true)
    {
        ArgumentNullException.ThrowIfNull(self);
        ArgumentNullException.ThrowIfNull(extensions);

        var extension = self.Extension;
        if (string.IsNullOrEmpty(extension)) return false;

        var targetExt = extension[0] == '.' ? extension.AsSpan(1) : extension.AsSpan();
        var caseCompare = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;

        foreach (var checkExtension in extensions)
        {
            if (string.IsNullOrEmpty(checkExtension)) continue;
            var checkExt = checkExtension[0] == '.' ? checkExtension.AsSpan(1) : checkExtension.AsSpan();
            var extMatch = targetExt.Equals(checkExt, caseCompare);
            if (extMatch) return true;
        }

        return false;
    }

    /// <summary>ファイルが指定したいずれかの拡張子であるか否かを判定する。(大文字/小文字を区別しない)</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="extensions">拡張子のリスト</param>
    /// <returns>いずれかの拡張子であるか否か</returns>
    public static bool HasAnyExtension(this FileInfo self, params string[] extensions)
        => self.HasAnyExtension(extensions, ignoreCase: true);
    #endregion

    #region FileSystemInfo
    /// <summary>ディレクトリからの相対パス位置に対する FileInfo を取得する。</summary>
    /// <param name="self">基準となるディレクトリの DirectoryInfo</param>
    /// <param name="relativePath">基準ディレクトリからのパス。もし絶対パスの場合は基準ディレクトリは無関係にこの絶対パスが利用される。</param>
    /// <returns>対象ファイルパスの FileInfo。相対パスが空や空白の場合は null を返却。</returns>
    public static FileInfo? RelativeFileAt(this FileInfo self, string? relativePath)
        => (self?.Directory)!.RelativeFileAt(relativePath);

    /// <summary>ディレクトリからの相対パス位置に対する FileInfo を取得する。</summary>
    /// <param name="self">基準となるディレクトリの DirectoryInfo</param>
    /// <param name="relativePath">基準ディレクトリからのパス。もし絶対パスの場合は基準ディレクトリは無関係にこの絶対パスが利用される。</param>
    /// <returns>対象ファイルパスの FileInfo。相対パスが空や空白の場合は例外を送出。</returns>
    public static FileInfo RelativeFile(this FileInfo self, string relativePath)
        => (self?.Directory)!.RelativeFile(relativePath);

    /// <summary>ディレクトリからの相対パス位置に対する DirectoryInfo を取得する。</summary>
    /// <param name="self">基準となるディレクトリのDirectoryInfo</param>
    /// <param name="relativePath">基準ディレクトリからのパス。もし絶対パスの場合は基準ディレクトリは無関係にこの絶対パスが利用される。</param>
    /// <returns>対象ディレクトリパスの DirectoryInfo。相対パスが空や空白の場合は null を返却。</returns>
    public static DirectoryInfo? RelativeDirectoryAt(this FileInfo self, string? relativePath)
        => (self?.Directory)!.RelativeDirectoryAt(relativePath);

    /// <summary>ディレクトリからの相対パス位置に対する DirectoryInfo を取得する。</summary>
    /// <param name="self">基準となるディレクトリのDirectoryInfo</param>
    /// <param name="relativePath">基準ディレクトリからのパス。もし絶対パスの場合は基準ディレクトリは無関係にこの絶対パスが利用される。</param>
    /// <returns>対象ディレクトリパスの DirectoryInfo。相対パスが空や空白の場合は基準ディレクトリを返却。</returns>
    public static DirectoryInfo RelativeDirectory(this FileInfo self, string? relativePath)
        => (self?.Directory)!.RelativeDirectory(relativePath);
    #endregion

    #region Read
    /// <summary>ファイル内容の全バイト列を読み出す。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <returns>ファイルから読みだしたバイト列</returns>
    public static byte[] ReadAllBytes(this FileInfo self)
    {
        ArgumentNullException.ThrowIfNull(self);
        return File.ReadAllBytes(self.FullName);
    }

    /// <summary>ファイル内容の全テキストを読み出す。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <returns>ファイルから読みだした全テキスト</returns>
    public static string ReadAllText(this FileInfo self)
    {
        ArgumentNullException.ThrowIfNull(self);
        return File.ReadAllText(self.FullName);
    }

    /// <summary>ファイル内容の全テキストを読み出す。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="encoding">ファイル内容をデコードするテキストエンコーディング</param>
    /// <returns>ファイルから読みだした全テキスト</returns>
    public static string ReadAllText(this FileInfo self, Encoding encoding)
    {
        ArgumentNullException.ThrowIfNull(self);
        return File.ReadAllText(self.FullName, encoding);
    }

    /// <summary>ファイル内容の全テキスト行を読み出す。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <returns>ファイルから読みだした全テキスト行</returns>
    public static string[] ReadAllLines(this FileInfo self)
    {
        ArgumentNullException.ThrowIfNull(self);
        return File.ReadAllLines(self.FullName);
    }

    /// <summary>ファイル内容の全テキスト行を読み出す。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="encoding">ファイル内容をデコードするテキストエンコーディング</param>
    /// <returns>ファイルから読みだした全テキスト行</returns>
    public static string[] ReadAllLines(this FileInfo self, Encoding encoding)
    {
        ArgumentNullException.ThrowIfNull(self);
        return File.ReadAllLines(self.FullName, encoding);
    }

    /// <summary>ファイル内容のテキスト行を読み出す。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <returns>ファイルから読みだしたテキスト行</returns>
    public static IEnumerable<string> ReadLines(this FileInfo self)
    {
        ArgumentNullException.ThrowIfNull(self);
        return File.ReadLines(self.FullName);
    }

    /// <summary>ファイル内容の全テキスト行を読み出す。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="encoding">ファイル内容をデコードするテキストエンコーディング</param>
    /// <returns>ファイルから読みだしたテキスト行</returns>
    public static IEnumerable<string> ReadLines(this FileInfo self, Encoding encoding)
    {
        ArgumentNullException.ThrowIfNull(self);
        return File.ReadLines(self.FullName, encoding);
    }

    /// <summary>ファイル内容の全バイト列を読み出す。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>ファイルから読みだしたバイト列</returns>
    public static Task<byte[]> ReadAllBytesAsync(this FileInfo self, CancellationToken cancelToken = default)
    {
        ArgumentNullException.ThrowIfNull(self);
        return File.ReadAllBytesAsync(self.FullName, cancelToken);
    }

    /// <summary>ファイル内容の全テキストを読み出す。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>ファイルから読みだした全テキスト</returns>
    public static Task<string> ReadAllTextAsync(this FileInfo self, CancellationToken cancelToken = default)
    {
        ArgumentNullException.ThrowIfNull(self);
        return File.ReadAllTextAsync(self.FullName, cancelToken);
    }

    /// <summary>ファイル内容の全テキストを読み出す。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="encoding">ファイル内容をデコードするテキストエンコーディング</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>ファイルから読みだした全テキスト</returns>
    public static Task<string> ReadAllTextAsync(this FileInfo self, Encoding encoding, CancellationToken cancelToken = default)
    {
        ArgumentNullException.ThrowIfNull(self);
        return File.ReadAllTextAsync(self.FullName, encoding, cancelToken);
    }

    /// <summary>ファイル内容の全テキスト行を読み出す。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>ファイルから読みだした全テキスト行</returns>
    public static Task<string[]> ReadAllLinesAsync(this FileInfo self, CancellationToken cancelToken = default)
    {
        ArgumentNullException.ThrowIfNull(self);
        return File.ReadAllLinesAsync(self.FullName, cancelToken);
    }

    /// <summary>ファイル内容の全テキスト行を読み出す。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="encoding">ファイル内容をデコードするテキストエンコーディング</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>ファイルから読みだした全テキスト行</returns>
    public static Task<string[]> ReadAllLinesAsync(this FileInfo self, Encoding encoding, CancellationToken cancelToken = default)
    {
        ArgumentNullException.ThrowIfNull(self);
        return File.ReadAllLinesAsync(self.FullName, encoding, cancelToken);
    }

    /// <summary>ファイル内容をテキストで読み取るリーダーを生成する。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="encoding">ファイル内容をデコードするテキストエンコーディング</param>
    /// <param name="detectBom">BOMを検出してエンコーディングを決定するか否か</param>
    /// <param name="options">元になるファイルストリームを開くオプション</param>
    /// <returns>ストリームリーダー</returns>
    public static StreamReader CreateTextReader(this FileInfo self, bool detectBom = true, FileStreamOptions? options = null, Encoding? encoding = null)
    {
        return (options == null) ? new StreamReader(self.FullName, encoding ?? Encoding.UTF8, detectBom)
                                 : new StreamReader(self.FullName, encoding ?? Encoding.UTF8, detectBom, options);
    }
    #endregion

    #region Write
    /// <summary>ファイル内容が指定のバイト列となるように書き込む。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="bytes">書き込むバイト列</param>
    public static void WriteAllBytes(this FileInfo self, byte[] bytes)
    {
        ArgumentNullException.ThrowIfNull(self);
        File.WriteAllBytes(self.FullName, bytes);
        self.Refresh();
    }

    /// <summary>ファイル内容が指定のバイト列となるように書き込む。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="bytes">書き込むバイト列</param>
    /// <param name="options">ファイルストリームを開くオプション。Access プロパティは無視する。</param>
    public static void WriteAllBytes(this FileInfo self, ReadOnlySpan<byte> bytes, FileStreamOptions? options = null)
    {
        ArgumentNullException.ThrowIfNull(self);
        using var stream = self.CreateWrite(options);
        stream.Write(bytes);
        self.Refresh();
    }

    /// <summary>ファイル内容が指定のテキストとなるように書き込む。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="contents">書き込むテキスト</param>
    public static void WriteAllText(this FileInfo self, string contents)
    {
        ArgumentNullException.ThrowIfNull(self);
        File.WriteAllText(self.FullName, contents);
        self.Refresh();
    }

    /// <summary>ファイル内容が指定のテキストとなるように書き込む。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="contents">書き込むテキスト</param>
    /// <param name="encoding">書き込むテキストをエンコードするテキストエンコーディング</param>
    public static void WriteAllText(this FileInfo self, string contents, Encoding encoding)
    {
        ArgumentNullException.ThrowIfNull(self);
        File.WriteAllText(self.FullName, contents, encoding);
        self.Refresh();
    }

    /// <summary>ファイル内容が指定のテキストとなるように書き込む。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="contents">書き込むテキスト</param>
    /// <param name="options">ファイルストリームを開くオプション。Access プロパティは無視する。</param>
    /// <param name="encoding">書き込むテキストをエンコードするテキストエンコーディング</param>
    public static void WriteAllText(this FileInfo self, ReadOnlySpan<char> contents, FileStreamOptions? options = null, Encoding? encoding = null)
    {
        ArgumentNullException.ThrowIfNull(self);
        using var writer = self.CreateTextWriter(createStreamWriteOptions(options), encoding);
        writer.Write(contents);
        self.Refresh();
    }

    /// <summary>ファイル内容が指定のテキスト行となるように書き込む。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="contents">書き込むテキスト行</param>
    public static void WriteAllLines(this FileInfo self, IEnumerable<string> contents)
    {
        ArgumentNullException.ThrowIfNull(self);
        File.WriteAllLines(self.FullName, contents);
        self.Refresh();
    }

    /// <summary>ファイル内容が指定のテキスト行となるように書き込む。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="contents">書き込むテキスト行</param>
    /// <param name="encoding">書き込むテキストをエンコードするテキストエンコーディング</param>
    public static void WriteAllLines(this FileInfo self, IEnumerable<string> contents, Encoding encoding)
    {
        ArgumentNullException.ThrowIfNull(self);
        File.WriteAllLines(self.FullName, contents, encoding);
        self.Refresh();
    }

    /// <summary>ファイル内容が指定のバイト列となるように書き込む。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="bytes">書き込むバイト列</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    public static async ValueTask WriteAllBytesAsync(this FileInfo self, byte[] bytes, CancellationToken cancelToken = default)
    {
        ArgumentNullException.ThrowIfNull(self);
        await File.WriteAllBytesAsync(self.FullName, bytes, cancelToken).ConfigureAwait(false);
        self.Refresh();
    }

    /// <summary>ファイル内容が指定のバイト列となるように書き込む。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="bytes">書き込むバイト列</param>
    /// <param name="options">ファイルストリームを開くオプション。Access プロパティは無視する。</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    public static async ValueTask WriteAllBytesAsync(this FileInfo self, ReadOnlyMemory<byte> bytes, FileStreamOptions? options = null, CancellationToken cancelToken = default)
    {
        ArgumentNullException.ThrowIfNull(self);
        using var stream = self.CreateWrite(options);
        await stream.WriteAsync(bytes, cancelToken);
        self.Refresh();
    }

    /// <summary>ファイル内容が指定のテキストとなるように書き込む。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="contents">書き込むテキスト</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    public static async ValueTask WriteAllTextAsync(this FileInfo self, string contents, CancellationToken cancelToken = default)
    {
        ArgumentNullException.ThrowIfNull(self);
        await File.WriteAllTextAsync(self.FullName, contents, cancelToken).ConfigureAwait(false);
        self.Refresh();
    }

    /// <summary>ファイル内容が指定のテキストとなるように書き込む。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="contents">書き込むテキスト</param>
    /// <param name="encoding">書き込むテキストをエンコードするテキストエンコーディング</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    public static async ValueTask WriteAllTextAsync(this FileInfo self, string contents, Encoding encoding, CancellationToken cancelToken = default)
    {
        ArgumentNullException.ThrowIfNull(self);
        await File.WriteAllTextAsync(self.FullName, contents, encoding, cancelToken).ConfigureAwait(false);
        self.Refresh();
    }

    /// <summary>ファイル内容が指定のバイト列となるように書き込む。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="contents">書き込むテキスト</param>
    /// <param name="options">ファイルストリームを開くオプション。Access プロパティは無視する。</param>
    /// <param name="encoding">書き込むテキストをエンコードするテキストエンコーディング</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    public static async ValueTask WriteAllTextAsync(this FileInfo self, ReadOnlyMemory<char> contents, FileStreamOptions? options = null, Encoding? encoding = null, CancellationToken cancelToken = default)
    {
        ArgumentNullException.ThrowIfNull(self);
        using var writer = self.CreateTextWriter(createStreamWriteOptions(options), encoding);
        await writer.WriteAsync(contents, cancelToken);
        self.Refresh();
    }

    /// <summary>ファイル内容が指定のテキスト行となるように書き込む。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="contents">書き込むテキスト行</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    public static async ValueTask WriteAllLinesAsync(this FileInfo self, IEnumerable<string> contents, CancellationToken cancelToken = default)
    {
        ArgumentNullException.ThrowIfNull(self);
        await File.WriteAllLinesAsync(self.FullName, contents, cancelToken);
        self.Refresh();
    }

    /// <summary>ファイル内容が指定のテキスト行となるように書き込む。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="contents">書き込むテキスト行</param>
    /// <param name="encoding">書き込むテキストをエンコードするテキストエンコーディング</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    public static async ValueTask WriteAllLinesAsync(this FileInfo self, IEnumerable<string> contents, Encoding encoding, CancellationToken cancelToken = default)
    {
        ArgumentNullException.ThrowIfNull(self);
        await File.WriteAllLinesAsync(self.FullName, contents, encoding, cancelToken).ConfigureAwait(false);
        self.Refresh();
    }

    /// <summary>新しいファイルの作成モードで書き込み用のストリームを開く</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="options">ファイルストリームを開くオプション。Access プロパティは無視する。</param>
    /// <returns>書き込み用のストリーム</returns>
    public static FileStream CreateWrite(this FileInfo self, FileStreamOptions? options = null)
    {
        return new FileStream(self.FullName, createStreamWriteOptions(options));
    }

    /// <summary>ファイル内容をテキストで読み取るリーダーを生成する。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="encoding">ファイル内容をデコードするテキストエンコーディング</param>
    /// <param name="append">追記するか否か</param>
    /// <returns>ストリームリーダー</returns>
    public static StreamWriter CreateTextWriter(this FileInfo self, bool append = false, Encoding? encoding = null)
    {
        return new StreamWriter(self.FullName, append, encoding ?? Encoding.UTF8);
    }

    /// <summary>ファイル内容をテキストで読み取るリーダーを生成する。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <param name="encoding">ファイル内容をデコードするテキストエンコーディング</param>
    /// <param name="options">元になるファイルストリームを開くオプション</param>
    /// <returns>ストリームリーダー</returns>
    public static StreamWriter CreateTextWriter(this FileInfo self, FileStreamOptions options, Encoding? encoding = null)
    {
        return new StreamWriter(self.FullName, encoding ?? Encoding.UTF8, options);
    }
    #endregion

    #region Read JSON
    /// <summary>ファイルからJSONデータを読み取る</summary>
    /// <typeparam name="TObject">JSONをデシリアライズする型</typeparam>
    /// <param name="self">読み取り元ファイル</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>デシリアライズしたインスタンス</returns>
    public static ValueTask<TObject?> ReadJsonAsync<TObject>(this FileInfo self, CancellationToken cancelToken)
        => self.ReadJsonAsync<TObject>(default, cancelToken);

    /// <summary>ファイルからJSONデータを読み取る</summary>
    /// <typeparam name="TObject">JSONをデシリアライズする型</typeparam>
    /// <param name="self">読み取り元ファイル</param>
    /// <param name="options">デシリアライズオプション</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>デシリアライズしたインスタンス</returns>
    public static async ValueTask<TObject?> ReadJsonAsync<TObject>(this FileInfo self, JsonSerializerOptions? options = null, CancellationToken cancelToken = default)
    {
        using var stream = self.OpenRead();
        return await JsonSerializer.DeserializeAsync<TObject>(stream, options, cancelToken).ConfigureAwait(false);
    }

    /// <summary>ファイルからJSONデータを読み取る</summary>
    /// <typeparam name="TObject">JSONをデシリアライズする型</typeparam>
    /// <param name="self">読み取り元ファイル</param>
    /// <param name="template">
    /// JSONをデシリアライズする型を示す値。
    /// インスタンス値が利用されることはなく型情報のみを参照する。
    /// 型情報は静的な型が参照される。インスタンスの実体型は影響を与えない。
    /// </param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>デシリアライズしたインスタンス</returns>
    public static ValueTask<TObject?> ReadJsonByTemplateAsync<TObject>(this FileInfo self, TObject? template, CancellationToken cancelToken)
        => self.ReadJsonByTemplateAsync<TObject>(template, default, cancelToken);

    /// <summary>ファイルからJSONデータを読み取る</summary>
    /// <typeparam name="TObject">JSONをデシリアライズする型</typeparam>
    /// <param name="self">読み取り元ファイル</param>
    /// <param name="template">
    /// JSONをデシリアライズする型を示す値。
    /// インスタンス値が利用されることはなく型情報のみを参照する。
    /// 型情報は静的な型が参照される。インスタンスの実体型は影響を与えない。
    /// </param>
    /// <param name="options">デシリアライズオプション</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>デシリアライズしたインスタンス</returns>
    public static async ValueTask<TObject?> ReadJsonByTemplateAsync<TObject>(this FileInfo self, TObject? template, JsonSerializerOptions? options = null, CancellationToken cancelToken = default)
    {
        _ = template;
        using var stream = self.OpenRead();
        var decoded = await JsonSerializer.DeserializeAsync(stream, typeof(TObject), options, cancelToken).ConfigureAwait(false);
        return (TObject?)decoded;
    }
    #endregion

    #region Write JSON
    /// <summary>オブジェクトをJSON形式でファイルに保存する</summary>
    /// <typeparam name="TObject">JSONにデシリアライズする型</typeparam>
    /// <param name="self">保存先ファイル</param>
    /// <param name="value">保存するオブジェクト</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    public static ValueTask WriteJsonAsync<TObject>(this FileInfo self, TObject value, CancellationToken cancelToken)
        => self.WriteJsonAsync(value, default, cancelToken);

    /// <summary>オブジェクトをJSON形式でファイルに保存する</summary>
    /// <typeparam name="TObject">JSONにデシリアライズする型</typeparam>
    /// <param name="self">保存先ファイル</param>
    /// <param name="value">保存するオブジェクト</param>
    /// <param name="options">シリアライズオプション</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    public static async ValueTask WriteJsonAsync<TObject>(this FileInfo self, TObject value, JsonSerializerOptions? options = null, CancellationToken cancelToken = default)
    {
        using var stream = self.CreateWrite();
        await JsonSerializer.SerializeAsync(stream, value, options, cancelToken).ConfigureAwait(false);
    }
    #endregion

    #region FileSystem
    /// <summary>ファイルまでのディレクトリを作成する。</summary>
    /// <param name="self">対象ファイル情報</param>
    /// <returns>元のファイル情報</returns>
    public static FileInfo WithDirectoryCreate(this FileInfo self)
    {
        ArgumentNullException.ThrowIfNull(self);
        self.Directory?.WithCreate();
        return self;
    }

    /// <summary>ファイルの作成または更新日時の更新を行う</summary>
    /// <param name="self">対象ファイル情報</param>
    /// <returns>元のファイル情報</returns>
    public static FileInfo Touch(this FileInfo self)
    {
        ArgumentNullException.ThrowIfNull(self);
        self.Directory?.Create();
        using var stream = new FileStream(self.FullName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None, bufferSize: 0);
        self.LastWriteTimeUtc = DateTime.UtcNow;
        return self;
    }
    #endregion

    #region Path
    /// <summary>ファイルパスの構成セグメントを取得する。</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    /// <returns>パス構成セグメントのリスト</returns>
    public static IList<string> GetPathSegments(this FileInfo self)
    {
        ArgumentNullException.ThrowIfNull(self);

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

    /// <summary>ファイルが指定のディレクトリの子孫であるかを判定する。</summary>
    /// <remarks></remarks>
    /// <param name="self">対象ファイル</param>
    /// <param name="other">比較するディレクトリ</param>
    /// <returns>指定ディレクトリの子孫であるか否か</returns>
    public static bool IsDescendantOf(this FileInfo self, DirectoryInfo other)
    {
        ArgumentNullException.ThrowIfNull(self);
        ArgumentNullException.ThrowIfNull(other);
        ArgumentNullException.ThrowIfNull(self.Directory, $"{nameof(self)}.{nameof(self.Directory)}");

        // ファイル格納ディレクトリと比較対象のパス階層を取得
        var selfDirSegs = self.Directory.GetPathSegments();
        var otherSegs = other.GetPathSegments();

        // 比較対象のほうが階層が深い場合はその子孫ではありえない。
        if (selfDirSegs.Count < otherSegs.Count) return false;

        // 比較対象が対象ファイルのディレクトリを全て含んでいるかを判定
        for (var i = 0; i < otherSegs.Count; i++)
        {
            if (!string.Equals(selfDirSegs[i], otherSegs[i], StringComparison.OrdinalIgnoreCase)) return false;
        }

        return true;
    }

    /// <summary>指定のディレクトリを起点としたファイルの相対パスを取得する。</summary>
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
        ArgumentNullException.ThrowIfNull(self);
        ArgumentNullException.ThrowIfNull(baseDir);

        return DirectoryInfoExtensions.SegmentsToReletivePath(self.GetPathSegments(), baseDir, ignoreCase);
    }
    #endregion

    // 非公開メソッド
    #region Helper
    /// <summary>Write向けのストリームオプションを生成する。</summary>
    /// <param name="options">元にするオプション。nullの場合はデフォルトの値とする。</param>
    /// <returns>生成したオプション</returns>
    private static FileStreamOptions createStreamWriteOptions(FileStreamOptions? options = null)
    {
        var writeOpt = new FileStreamOptions();
        writeOpt.Mode = FileMode.Create;
        writeOpt.Access = FileAccess.Write;
        writeOpt.Share = FileShare.Read;
        if (options != null)
        {
            writeOpt.Mode = options.Mode;
            writeOpt.Share = options.Share;
            writeOpt.Options = options.Options;
            writeOpt.PreallocationSize = options.PreallocationSize;
            writeOpt.BufferSize = options.BufferSize;
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) writeOpt.UnixCreateMode = options.UnixCreateMode;
        }

        return writeOpt;
    }
    #endregion
}
