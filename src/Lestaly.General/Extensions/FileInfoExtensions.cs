using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace Lestaly;

/// <summary>FileInfo に対する拡張メソッド</summary>
public static class FileInfoExtensions
{
    #region Name
    /// <summary>名称系メソッド</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    extension(FileInfo self)
    {
        /// <summary>拡張子を除いたファイル名を取得する。</summary>
        /// <returns>拡張子を除いたファイル名</returns>
        public string BaseName()
        {
            ArgumentNullException.ThrowIfNull(self);
            return Path.GetFileNameWithoutExtension(self.Name);
        }

        /// <summary>拡張子を取得する。</summary>
        /// <returns>拡張子</returns>
        public string Extension()
        {
            ArgumentNullException.ThrowIfNull(self);
            return self.Extension;
        }

        /// <summary>指定した拡張子ファイルを示すFileInfoを取得する。</summary>
        /// <param name="extension">拡張子</param>
        /// <returns> 指定した拡張子ファイルを示すFileInfo</returns>
        public FileInfo GetAnotherExtension(string extension)
        {
            ArgumentNullException.ThrowIfNull(self);
            var path = Path.ChangeExtension(self.FullName, extension);
            return new FileInfo(path);
        }

        /// <summary>ファイルが指定したいずれかの拡張子であるか否かを判定する。)</summary>
        /// <param name="extensions">拡張子のリスト</param>
        /// <param name="ignoreCase">大文字/小文字の違いを無視するか否か</param>
        /// <returns>いずれかの拡張子であるか否か</returns>
        public bool HasAnyExtension(IEnumerable<string> extensions, bool ignoreCase = true)
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
        /// <param name="extensions">拡張子のリスト</param>
        /// <returns>いずれかの拡張子であるか否か</returns>
        public bool HasAnyExtension(params string[] extensions)
            => self.HasAnyExtension(extensions, ignoreCase: true);
    }
    #endregion

    #region Relative
    /// <summary>相対パス系メソッド</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    extension(FileInfo self)
    {
        /// <summary>ディレクトリからの相対パス位置に対する FileInfo を取得する。</summary>
        /// <param name="relativePath">基準ディレクトリからのパス。もし絶対パスの場合は基準ディレクトリは無関係にこの絶対パスが利用される。</param>
        /// <returns>対象ファイルパスの FileInfo。相対パスが空や空白の場合は null を返却。</returns>
        public FileInfo? RelativeFileAt(string? relativePath)
            => (self?.Directory)!.RelativeFileAt(relativePath);

        /// <summary>ディレクトリからの相対パス位置に対する FileInfo を取得する。</summary>
        /// <param name="relativePath">基準ディレクトリからのパス。もし絶対パスの場合は基準ディレクトリは無関係にこの絶対パスが利用される。</param>
        /// <returns>対象ファイルパスの FileInfo。相対パスが空や空白の場合は例外を送出。</returns>
        public FileInfo RelativeFile(string relativePath)
            => (self?.Directory)!.RelativeFile(relativePath);

        /// <summary>ディレクトリからの相対パス位置に対する DirectoryInfo を取得する。</summary>
        /// <param name="relativePath">基準ディレクトリからのパス。もし絶対パスの場合は基準ディレクトリは無関係にこの絶対パスが利用される。</param>
        /// <returns>対象ディレクトリパスの DirectoryInfo。相対パスが空や空白の場合は null を返却。</returns>
        public DirectoryInfo? RelativeDirectoryAt(string? relativePath)
            => (self?.Directory)!.RelativeDirectoryAt(relativePath);

        /// <summary>ディレクトリからの相対パス位置に対する DirectoryInfo を取得する。</summary>
        /// <param name="relativePath">基準ディレクトリからのパス。もし絶対パスの場合は基準ディレクトリは無関係にこの絶対パスが利用される。</param>
        /// <returns>対象ディレクトリパスの DirectoryInfo。相対パスが空や空白の場合は基準ディレクトリを返却。</returns>
        public DirectoryInfo RelativeDirectory(string? relativePath)
            => (self?.Directory)!.RelativeDirectory(relativePath);
    }
    #endregion

    #region Read bytes
    /// <summary>バイト列読み取り系メソッド</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    extension(FileInfo self)
    {
        /// <summary>ファイル内容の全バイト列を読み出す。</summary>
        /// <returns>ファイルから読みだしたバイト列</returns>
        public byte[] ReadAllBytes()
        {
            ArgumentNullException.ThrowIfNull(self);
            return File.ReadAllBytes(self.FullName);
        }

        /// <summary>ファイル内容の全バイト列読み出しを試みる</summary>
        /// <returns>ファイルから読みだしたバイト列。失敗時は null</returns>
        public byte[]? TryReadAllBytes()
        {
            ArgumentNullException.ThrowIfNull(self);
            try { return File.ReadAllBytes(self.FullName); }
            catch { return default; }
        }

        /// <summary>ファイル内容の全バイト列を読み出す。</summary>
        /// <param name="cancelToken">キャンセルトークン</param>
        /// <returns>ファイルから読みだしたバイト列</returns>
        public Task<byte[]> ReadAllBytesAsync(CancellationToken cancelToken = default)
        {
            ArgumentNullException.ThrowIfNull(self);
            return File.ReadAllBytesAsync(self.FullName, cancelToken);
        }
    }
    #endregion

    #region Read text
    /// <summary>テキスト読み取り系メソッド</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    extension(FileInfo self)
    {
        /// <summary>ファイル内容の全テキストを読み出す。</summary>
        /// <returns>ファイルから読みだした全テキスト</returns>
        public string ReadAllText()
        {
            ArgumentNullException.ThrowIfNull(self);
            return File.ReadAllText(self.FullName);
        }

        /// <summary>ファイル内容の全テキストを読み出す。</summary>
        /// <param name="encoding">ファイル内容をデコードするテキストエンコーディング</param>
        /// <returns>ファイルから読みだした全テキスト</returns>
        public string ReadAllText(Encoding encoding)
        {
            ArgumentNullException.ThrowIfNull(self);
            return File.ReadAllText(self.FullName, encoding);
        }

        /// <summary>ファイル内容の全テキスト読み出しを試みる。</summary>
        /// <param name="encoding">ファイル内容をデコードするテキストエンコーディング</param>
        /// <returns>ファイルから読みだした全テキスト。失敗時は null</returns>
        public string? TryReadAllText(Encoding? encoding = default)
        {
            ArgumentNullException.ThrowIfNull(self);
            try { return File.ReadAllText(self.FullName, encoding ?? Encoding.UTF8); }
            catch { return default; }
        }

        /// <summary>ファイル内容の全テキストを読み出す。</summary>
        /// <param name="cancelToken">キャンセルトークン</param>
        /// <returns>ファイルから読みだした全テキスト</returns>
        public Task<string> ReadAllTextAsync(CancellationToken cancelToken = default)
        {
            ArgumentNullException.ThrowIfNull(self);
            return File.ReadAllTextAsync(self.FullName, cancelToken);
        }

        /// <summary>ファイル内容の全テキストを読み出す。</summary>
        /// <param name="encoding">ファイル内容をデコードするテキストエンコーディング</param>
        /// <param name="cancelToken">キャンセルトークン</param>
        /// <returns>ファイルから読みだした全テキスト</returns>
        public Task<string> ReadAllTextAsync(Encoding encoding, CancellationToken cancelToken = default)
        {
            ArgumentNullException.ThrowIfNull(self);
            return File.ReadAllTextAsync(self.FullName, encoding, cancelToken);
        }
    }
    #endregion

    #region Read lines
    /// <summary>テキスト行読み取り系メソッド</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    extension(FileInfo self)
    {
        /// <summary>ファイル内容の全テキスト行を読み出す。</summary>
        /// <returns>ファイルから読みだした全テキスト行</returns>
        public string[] ReadAllLines()
        {
            ArgumentNullException.ThrowIfNull(self);
            return File.ReadAllLines(self.FullName);
        }

        /// <summary>ファイル内容の全テキスト行を読み出す。</summary>
        /// <param name="encoding">ファイル内容をデコードするテキストエンコーディング</param>
        /// <returns>ファイルから読みだした全テキスト行</returns>
        public string[] ReadAllLines(Encoding encoding)
        {
            ArgumentNullException.ThrowIfNull(self);
            return File.ReadAllLines(self.FullName, encoding);
        }

        /// <summary>ファイル内容のテキスト行を読み出す。</summary>
        /// <returns>ファイルから読みだしたテキスト行</returns>
        public IEnumerable<string> ReadLines()
        {
            ArgumentNullException.ThrowIfNull(self);
            return File.ReadLines(self.FullName);
        }

        /// <summary>ファイル内容の全テキスト行を読み出す。</summary>
        /// <param name="encoding">ファイル内容をデコードするテキストエンコーディング</param>
        /// <returns>ファイルから読みだしたテキスト行</returns>
        public IEnumerable<string> ReadLines(Encoding encoding)
        {
            ArgumentNullException.ThrowIfNull(self);
            return File.ReadLines(self.FullName, encoding);
        }

        /// <summary>ファイル内容の全テキスト行を読み出す。</summary>
        /// <param name="cancelToken">キャンセルトークン</param>
        /// <returns>ファイルから読みだした全テキスト行</returns>
        public Task<string[]> ReadAllLinesAsync(CancellationToken cancelToken = default)
        {
            ArgumentNullException.ThrowIfNull(self);
            return File.ReadAllLinesAsync(self.FullName, cancelToken);
        }

        /// <summary>ファイル内容の全テキスト行を読み出す。</summary>
        /// <param name="encoding">ファイル内容をデコードするテキストエンコーディング</param>
        /// <param name="cancelToken">キャンセルトークン</param>
        /// <returns>ファイルから読みだした全テキスト行</returns>
        public Task<string[]> ReadAllLinesAsync(Encoding encoding, CancellationToken cancelToken = default)
        {
            ArgumentNullException.ThrowIfNull(self);
            return File.ReadAllLinesAsync(self.FullName, encoding, cancelToken);
        }
    }
    #endregion

    #region Read stream
    /// <summary>ストリーム読み取り系メソッド</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    extension(FileInfo self)
    {
        /// <summary>ファイル内容をテキストで読み取るリーダーを生成する。</summary>
        /// <param name="encoding">ファイル内容をデコードするテキストエンコーディング</param>
        /// <param name="detectBom">BOMを検出してエンコーディングを決定するか否か</param>
        /// <param name="options">元になるファイルストリームを開くオプション</param>
        /// <returns>ストリームリーダー</returns>
        public StreamReader CreateTextReader(bool detectBom = true, FileStreamOptions? options = null, Encoding? encoding = null)
        {
            return (options == null) ? new StreamReader(self.FullName, encoding ?? Encoding.UTF8, detectBom)
                                     : new StreamReader(self.FullName, encoding ?? Encoding.UTF8, detectBom, options);
        }
    }
    #endregion

    #region Write bytes
    /// <summary>バイト列書き込み系メソッド</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    extension(FileInfo self)
    {
        /// <summary>ファイル内容が指定のバイト列となるように書き込む。</summary>
        /// <param name="bytes">書き込むバイト列</param>
        /// <returns>レシーバ自身</returns>
        public FileInfo WriteAllBytes(byte[] bytes)
        {
            ArgumentNullException.ThrowIfNull(self);
            File.WriteAllBytes(self.FullName, bytes);
            self.Refresh();
            return self;
        }

        /// <summary>ファイル内容が指定のバイト列となるように書き込む。</summary>
        /// <param name="bytes">書き込むバイト列</param>
        /// <param name="options">ファイルストリームを開くオプション。Access プロパティは無視する。</param>
        /// <returns>レシーバ自身</returns>
        public FileInfo WriteAllBytes(ReadOnlySpan<byte> bytes, FileStreamOptions? options = null)
        {
            ArgumentNullException.ThrowIfNull(self);
            using (var stream = self.CreateWrite(options))
            {
                stream.Write(bytes);
            }
            self.Refresh();
            return self;
        }

        /// <summary>ファイル内容に指定のストリーム内容を書き込む。</summary>
        /// <param name="stream">書き込みデータを読み出すストリーム</param>
        /// <param name="options">ファイルストリームを開くオプション。Access プロパティは無視する。</param>
        /// <returns>レシーバ自身</returns>
        public FileInfo WriteAllBytes(Stream stream, FileStreamOptions? options = null)
        {
            ArgumentNullException.ThrowIfNull(self);
            using (var fileStream = self.CreateWrite(options))
            {
                stream.CopyTo(fileStream);
            }
            self.Refresh();
            return self;
        }

        /// <summary>ファイル内容が指定のバイト列となるように書き込む。</summary>
        /// <param name="bytes">書き込むバイト列</param>
        /// <param name="cancelToken">キャンセルトークン</param>
        /// <returns>書き込みを行いレシーバ自身を返すタスク</returns>
        public async ValueTask<FileInfo> WriteAllBytesAsync(byte[] bytes, CancellationToken cancelToken = default)
        {
            ArgumentNullException.ThrowIfNull(self);
            await File.WriteAllBytesAsync(self.FullName, bytes, cancelToken).ConfigureAwait(false);
            self.Refresh();
            return self;
        }

        /// <summary>ファイル内容が指定のバイト列となるように書き込む。</summary>
        /// <param name="bytes">書き込むバイト列</param>
        /// <param name="options">ファイルストリームを開くオプション。Access プロパティは無視する。</param>
        /// <param name="cancelToken">キャンセルトークン</param>
        /// <returns>書き込みを行いレシーバ自身を返すタスク</returns>
        public async ValueTask<FileInfo> WriteAllBytesAsync(ReadOnlyMemory<byte> bytes, FileStreamOptions? options = null, CancellationToken cancelToken = default)
        {
            ArgumentNullException.ThrowIfNull(self);
            using (var stream = self.CreateWrite(options))
            {
                await stream.WriteAsync(bytes, cancelToken).ConfigureAwait(false);
            }
            self.Refresh();
            return self;
        }

        /// <summary>ファイル内容が指定のバイト列となるように書き込む。</summary>
        /// <param name="stream">書き込みデータを読み出すストリーム</param>
        /// <param name="options">ファイルストリームを開くオプション。Access プロパティは無視する。</param>
        /// <param name="cancelToken">キャンセルトークン</param>
        /// <returns>書き込みを行いレシーバ自身を返すタスク</returns>
        public async ValueTask<FileInfo> WriteAllBytesAsync(Stream stream, FileStreamOptions? options = null, CancellationToken cancelToken = default)
        {
            ArgumentNullException.ThrowIfNull(self);
            using (var fileStream = self.CreateWrite(options))
            {
                await stream.CopyToAsync(fileStream, cancelToken).ConfigureAwait(false);
            }
            self.Refresh();
            return self;
        }
    }
    #endregion

    #region Write text
    /// <summary>テキスト書き込み系メソッド</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    extension(FileInfo self)
    {
        /// <summary>ファイル内容が指定のテキストとなるように書き込む。</summary>
        /// <param name="contents">書き込むテキスト</param>
        /// <returns>レシーバ自身</returns>
        public FileInfo WriteAllText(string contents)
        {
            ArgumentNullException.ThrowIfNull(self);
            File.WriteAllText(self.FullName, contents);
            self.Refresh();
            return self;
        }

        /// <summary>ファイル内容が指定のテキストとなるように書き込む。</summary>
        /// <param name="contents">書き込むテキスト</param>
        /// <param name="encoding">書き込むテキストをエンコードするテキストエンコーディング</param>
        /// <returns>レシーバ自身</returns>
        public FileInfo WriteAllText(string contents, Encoding encoding)
        {
            ArgumentNullException.ThrowIfNull(self);
            File.WriteAllText(self.FullName, contents, encoding);
            self.Refresh();
            return self;
        }

        /// <summary>ファイル内容が指定のテキストとなるように書き込む。</summary>
        /// <param name="contents">書き込むテキスト</param>
        /// <param name="options">ファイルストリームを開くオプション。Access プロパティは無視する。</param>
        /// <param name="encoding">書き込むテキストをエンコードするテキストエンコーディング</param>
        /// <returns>レシーバ自身</returns>
        public FileInfo WriteAllText(ReadOnlySpan<char> contents, FileStreamOptions? options = null, Encoding? encoding = null)
        {
            ArgumentNullException.ThrowIfNull(self);
            using (var writer = self.CreateTextWriter(createStreamWriteOptions(options), encoding))
            {
                writer.Write(contents);
            }
            self.Refresh();
            return self;
        }

        /// <summary>ファイル内容が指定のテキストとなるように書き込む。</summary>
        /// <param name="contents">書き込むテキスト</param>
        /// <param name="options">ファイルストリームを開くオプション。Access プロパティは無視する。</param>
        /// <param name="encoding">書き込むテキストをエンコードするテキストエンコーディング</param>
        /// <returns>レシーバ自身</returns>
        public FileInfo WriteAllText(StringBuilder contents, FileStreamOptions? options = null, Encoding? encoding = null)
        {
            ArgumentNullException.ThrowIfNull(self);
            using (var writer = self.CreateTextWriter(createStreamWriteOptions(options), encoding))
            {
                writer.Write(contents);
            }
            self.Refresh();
            return self;
        }

        /// <summary>ファイルに行末文字を正規化した複数行テキストを書き込む。</summary>
        /// <param name="lineBreak">行末文字</param>
        /// <param name="multiline">書き込む複数行テキスト</param>
        /// <returns>レシーバ自身</returns>
        public FileInfo WriteMultilineText(ReadOnlySpan<char> lineBreak, ReadOnlySpan<char> multiline)
            => self.WriteMultilineText(multiline, options: default, encoding: default, lineBreak);

        /// <summary>ファイルに行末文字を正規化した複数行テキストを書き込む。</summary>
        /// <param name="lineBreak">行末文字</param>
        /// <param name="encoding">書き込むテキストをエンコードするテキストエンコーディング</param>
        /// <param name="multiline">書き込む複数行テキスト</param>
        /// <returns>レシーバ自身</returns>
        public FileInfo WriteMultilineText(ReadOnlySpan<char> lineBreak, Encoding encoding, ReadOnlySpan<char> multiline)
            => self.WriteMultilineText(multiline, options: default, encoding, lineBreak);

        /// <summary>ファイルに行末文字を正規化した複数行テキストを書き込む。</summary>
        /// <param name="multiline">書き込む複数行テキスト</param>
        /// <param name="lineBreak">行末文字</param>
        /// <param name="options">ファイルストリームを開くオプション。Access プロパティは無視する。</param>
        /// <param name="encoding">書き込むテキストをエンコードするテキストエンコーディング</param>
        /// <returns>レシーバ自身</returns>
        public FileInfo WriteMultilineText(ReadOnlySpan<char> multiline, FileStreamOptions? options = null, Encoding? encoding = null, ReadOnlySpan<char> lineBreak = default)
        {
            ArgumentNullException.ThrowIfNull(self);
            var breaker = lineBreak;
            if (breaker.IsEmpty) breaker = Environment.NewLine;
            using (var writer = self.CreateTextWriter(createStreamWriteOptions(options), encoding))
            {
                var first = true;
                foreach (var line in multiline.EnumerateLines())
                {
                    if (first) first = false;
                    else writer.Write(breaker);
                    writer.Write(line);
                }
            }
            self.Refresh();
            return self;
        }

        /// <summary>ファイル内容が指定のテキストとなるように書き込む。</summary>
        /// <param name="contents">書き込むテキスト</param>
        /// <param name="cancelToken">キャンセルトークン</param>
        /// <returns>書き込みを行いレシーバ自身を返すタスク</returns>
        public async ValueTask<FileInfo> WriteAllTextAsync(string contents, CancellationToken cancelToken = default)
        {
            ArgumentNullException.ThrowIfNull(self);
            await File.WriteAllTextAsync(self.FullName, contents, cancelToken).ConfigureAwait(false);
            self.Refresh();
            return self;
        }

        /// <summary>ファイル内容が指定のテキストとなるように書き込む。</summary>
        /// <param name="contents">書き込むテキスト</param>
        /// <param name="encoding">書き込むテキストをエンコードするテキストエンコーディング</param>
        /// <param name="cancelToken">キャンセルトークン</param>
        /// <returns>書き込みを行いレシーバ自身を返すタスク</returns>
        public async ValueTask<FileInfo> WriteAllTextAsync(string contents, Encoding encoding, CancellationToken cancelToken = default)
        {
            ArgumentNullException.ThrowIfNull(self);
            await File.WriteAllTextAsync(self.FullName, contents, encoding, cancelToken).ConfigureAwait(false);
            self.Refresh();
            return self;
        }

        /// <summary>ファイル内容が指定のバイト列となるように書き込む。</summary>
        /// <param name="contents">書き込むテキスト</param>
        /// <param name="options">ファイルストリームを開くオプション。Access プロパティは無視する。</param>
        /// <param name="encoding">書き込むテキストをエンコードするテキストエンコーディング</param>
        /// <param name="cancelToken">キャンセルトークン</param>
        /// <returns>書き込みを行いレシーバ自身を返すタスク</returns>
        public async ValueTask<FileInfo> WriteAllTextAsync(ReadOnlyMemory<char> contents, FileStreamOptions? options = null, Encoding? encoding = null, CancellationToken cancelToken = default)
        {
            ArgumentNullException.ThrowIfNull(self);
            using (var writer = self.CreateTextWriter(createStreamWriteOptions(options), encoding))
            {
                await writer.WriteAsync(contents, cancelToken).ConfigureAwait(false);
            }
            self.Refresh();
            return self;
        }

        /// <summary>ファイル内容が指定のバイト列となるように書き込む。</summary>
        /// <param name="contents">書き込むテキスト</param>
        /// <param name="options">ファイルストリームを開くオプション。Access プロパティは無視する。</param>
        /// <param name="encoding">書き込むテキストをエンコードするテキストエンコーディング</param>
        /// <param name="cancelToken">キャンセルトークン</param>
        /// <returns>書き込みを行いレシーバ自身を返すタスク</returns>
        public async ValueTask<FileInfo> WriteAllTextAsync(StringBuilder contents, FileStreamOptions? options = null, Encoding? encoding = null, CancellationToken cancelToken = default)
        {
            ArgumentNullException.ThrowIfNull(self);
            using (var writer = self.CreateTextWriter(createStreamWriteOptions(options), encoding))
            {
                await writer.WriteAsync(contents, cancelToken).ConfigureAwait(false);
            }
            self.Refresh();
            return self;
        }

        /// <summary>ファイルに行末文字を正規化した複数行テキストを書き込む。</summary>
        /// <param name="lineBreak">行末文字</param>
        /// <param name="multiline">書き込む複数行テキスト</param>
        /// <param name="cancelToken">キャンセルトークン</param>
        /// <returns>書き込みを行いレシーバ自身を返すタスク</returns>
        public ValueTask<FileInfo> WriteMultilineTextAsync(string? lineBreak, ReadOnlyMemory<char> multiline, CancellationToken cancelToken = default)
            => self.WriteMultilineTextAsync(multiline, options: default, encoding: default, lineBreak, cancelToken);

        /// <summary>ファイルに行末文字を正規化した複数行テキストを書き込む。</summary>
        /// <param name="lineBreak">行末文字</param>
        /// <param name="encoding">書き込むテキストをエンコードするテキストエンコーディング</param>
        /// <param name="multiline">書き込む複数行テキスト</param>
        /// <param name="cancelToken">キャンセルトークン</param>
        /// <returns>書き込みを行いレシーバ自身を返すタスク</returns>
        public ValueTask<FileInfo> WriteMultilineTextAsync(string? lineBreak, Encoding? encoding, ReadOnlyMemory<char> multiline, CancellationToken cancelToken = default)
            => self.WriteMultilineTextAsync(multiline, options: default, encoding, lineBreak, cancelToken);

        /// <summary>ファイルに行末文字を正規化した複数行テキストを書き込む。</summary>
        /// <param name="multiline">書き込む複数行テキスト</param>
        /// <param name="lineBreak">行末文字</param>
        /// <param name="options">ファイルストリームを開くオプション。Access プロパティは無視する。</param>
        /// <param name="encoding">書き込むテキストをエンコードするテキストエンコーディング</param>
        /// <param name="cancelToken">キャンセルトークン</param>
        /// <returns>書き込みを行いレシーバ自身を返すタスク</returns>
        public async ValueTask<FileInfo> WriteMultilineTextAsync(ReadOnlyMemory<char> multiline, FileStreamOptions? options = null, Encoding? encoding = null, string? lineBreak = default, CancellationToken cancelToken = default)
        {
            ArgumentNullException.ThrowIfNull(self);

            // 改行文字を決定
            var breaker = lineBreak ?? Environment.NewLine;

            using (var writer = self.CreateTextWriter(createStreamWriteOptions(options), encoding))
            {
                var first = true;
                var scan = multiline;
                while (true)
                {
                    // 改行書き出し。これは同期で。
                    if (first) first = false;
                    else writer.Write(breaker);

                    // 改行検索
                    var breakIdx = scan.Span.IndexOfAny('\r', '\n');
                    if (breakIdx < 0)
                    {
                        // 改行が見つからなければ最終パート
                        await writer.WriteAsync(scan, cancelToken).ConfigureAwait(false);
                        break;
                    }

                    // 行の書き出し。
                    await writer.WriteAsync(scan[..breakIdx], cancelToken).ConfigureAwait(false);

                    // 改行文字の後ろから
                    var breakLen = scan.Span[breakIdx..] is ['\r', '\n', ..] ? 2 : 1;
                    scan = scan[(breakIdx + breakLen)..];
                }
            }
            self.Refresh();
            return self;
        }
    }
    #endregion

    #region Write lines
    /// <summary>テキスト行書き込み系メソッド</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    extension(FileInfo self)
    {
        /// <summary>ファイル内容が指定のテキスト行となるように書き込む。</summary>
        /// <param name="contents">書き込むテキスト行</param>
        /// <returns>レシーバ自身</returns>
        public FileInfo WriteAllLines(IEnumerable<string> contents)
        {
            ArgumentNullException.ThrowIfNull(self);
            File.WriteAllLines(self.FullName, contents);
            self.Refresh();
            return self;
        }

        /// <summary>ファイル内容が指定のテキスト行となるように書き込む。</summary>
        /// <param name="contents">書き込むテキスト行</param>
        /// <param name="encoding">書き込むテキストをエンコードするテキストエンコーディング</param>
        /// <returns>レシーバ自身</returns>
        public FileInfo WriteAllLines(IEnumerable<string> contents, Encoding encoding)
        {
            ArgumentNullException.ThrowIfNull(self);
            File.WriteAllLines(self.FullName, contents, encoding);
            self.Refresh();
            return self;
        }

        /// <summary>ファイル内容が指定のテキスト行となるように書き込む。</summary>
        /// <param name="contents">書き込むテキスト行</param>
        /// <param name="lineBreak">改行文字</param>
        /// <param name="encoding">書き込むテキストをエンコードするテキストエンコーディング。省略時は UTF-8</param>
        /// <returns>レシーバ自身</returns>
        public FileInfo WriteAllLines(IEnumerable<string> contents, string lineBreak, Encoding? encoding = default)
        {
            ArgumentNullException.ThrowIfNull(self);
            using (var writer = self.CreateTextWriter(append: false, encoding: encoding ?? Encoding.UTF8))
            {
                writer.NewLine = lineBreak;
                foreach (var line in contents)
                {
                    writer.WriteLine(line);
                }
            }
            self.Refresh();
            return self;
        }

        /// <summary>ファイル内容が指定のテキスト行となるように書き込む。</summary>
        /// <param name="contents">書き込むテキスト行</param>
        /// <param name="cancelToken">キャンセルトークン</param>
        /// <returns>書き込みを行いレシーバ自身を返すタスク</returns>
        public async ValueTask<FileInfo> WriteAllLinesAsync(IEnumerable<string> contents, CancellationToken cancelToken = default)
        {
            ArgumentNullException.ThrowIfNull(self);
            await File.WriteAllLinesAsync(self.FullName, contents, cancelToken).ConfigureAwait(false);
            self.Refresh();
            return self;
        }

        /// <summary>ファイル内容が指定のテキスト行となるように書き込む。</summary>
        /// <param name="contents">書き込むテキスト行</param>
        /// <param name="encoding">書き込むテキストをエンコードするテキストエンコーディング</param>
        /// <param name="cancelToken">キャンセルトークン</param>
        /// <returns>書き込みを行いレシーバ自身を返すタスク</returns>
        public async ValueTask<FileInfo> WriteAllLinesAsync(IEnumerable<string> contents, Encoding encoding, CancellationToken cancelToken = default)
        {
            ArgumentNullException.ThrowIfNull(self);
            await File.WriteAllLinesAsync(self.FullName, contents, encoding, cancelToken).ConfigureAwait(false);
            self.Refresh();
            return self;
        }

        /// <summary>ファイル内容が指定のテキスト行となるように書き込む。</summary>
        /// <param name="contents">書き込むテキスト行</param>
        /// <param name="lineBreak">行末文字</param>
        /// <param name="encoding">書き込むテキストをエンコードするテキストエンコーディング</param>
        /// <param name="cancelToken">キャンセルトークン</param>
        /// <returns>書き込みを行いレシーバ自身を返すタスク</returns>
        public async ValueTask<FileInfo> WriteAllLinesAsync(IEnumerable<string> contents, string lineBreak, Encoding? encoding = default, CancellationToken cancelToken = default)
        {
            ArgumentNullException.ThrowIfNull(self);
            using (var writer = self.CreateTextWriter(append: false, encoding: encoding ?? Encoding.UTF8))
            {
                writer.NewLine = lineBreak;
                foreach (var line in contents)
                {
                    await writer.WriteLineAsync(line.AsMemory(), cancelToken).ConfigureAwait(false);
                }
            }
            self.Refresh();
            return self;
        }
    }
    #endregion

    #region Write stream
    /// <summary>ストリーム書き込み系メソッド</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    extension(FileInfo self)
    {
        /// <summary>新しいファイルの作成モードで書き込み用のストリームを開く</summary>
        /// <param name="options">ファイルストリームを開くオプション。Access プロパティは無視する。</param>
        /// <returns>書き込み用のストリーム</returns>
        public FileStream CreateWrite(FileStreamOptions? options = null)
        {
            return new FileStream(self.FullName, createStreamWriteOptions(options));
        }

        /// <summary>ファイル内容をテキストで読み取るリーダーを生成する。</summary>
        /// <param name="encoding">ファイル内容をデコードするテキストエンコーディング</param>
        /// <param name="append">追記するか否か</param>
        /// <returns>ストリームリーダー</returns>
        public StreamWriter CreateTextWriter(bool append = false, Encoding? encoding = null)
        {
            return new StreamWriter(self.FullName, append, encoding ?? Encoding.UTF8);
        }

        /// <summary>ファイル内容をテキストで読み取るリーダーを生成する。</summary>
        /// <param name="encoding">ファイル内容をデコードするテキストエンコーディング</param>
        /// <param name="options">元になるファイルストリームを開くオプション</param>
        /// <returns>ストリームリーダー</returns>
        public StreamWriter CreateTextWriter(FileStreamOptions options, Encoding? encoding = null)
        {
            return new StreamWriter(self.FullName, encoding ?? Encoding.UTF8, options);
        }
    }
    #endregion

    #region Update
    /// <summary>行の更新デリゲート</summary>
    /// <param name="line">行テキスト</param>
    /// <param name="writer">更新テキスト書き込み処理</param>
    /// <returns>行末を書き込むか否か</returns>
    public delegate bool LineUpdater(ReadOnlySpan<char> line, StringBuilder writer);

    /// <summary>ファイル更新系メソッド</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    extension(FileInfo self)
    {
        /// <summary>ファイルを行ごとに更新する</summary>
        /// <param name="updater">行更新デリゲート</param>
        /// <param name="lineBreak">行末文字。</param>
        /// <param name="encoding">読み書きテキストエンコーディング</param>
        /// <returns>レシーバ自身</returns>
        public FileInfo UpdateAllLines(LineUpdater updater, ReadOnlySpan<char> lineBreak = default, Encoding? encoding = default)
        {
            ArgumentNullException.ThrowIfNull(self);

            // 読み書きオープン
            using (var file = self.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None))
            {
                // デフォルトはUTF8とする
                encoding ??= Encoding.UTF8;

                // ファイル内容を全てテキスト読み取り
                var contents = default(string);
                using (var reader = new StreamReader(file, encoding, detectEncodingFromByteOrderMarks: true, leaveOpen: true))
                {
                    contents = reader.ReadToEnd();
                }

                // 更新テキストを作成
                var builder = new StringBuilder(capacity: contents.Length + 1024);

                // 1行ずつ処理してバッファに溜めていく
                var remaining = contents.AsSpan();
                while (!remaining.IsEmpty)
                {
                    // 行の切り出し
                    var termIdx = remaining.IndexOfAny(['\r', '\n']);
                    var line = termIdx < 0 ? remaining : remaining[..termIdx];
                    remaining = remaining[line.Length..];

                    // 行の更新処理
                    var written = updater(line, builder);

                    // 行末処理
                    if (remaining is ['\r', '\n', ..])
                    {
                        // 行末書き込み無しでなければ書く
                        if (written != false)
                        {
                            if (lineBreak.IsEmpty) builder.Append("\r\n");
                            else builder.Append(lineBreak);
                        }
                        // 行末消費
                        remaining = remaining[2..];
                    }
                    else if (remaining is ['\r', ..] or ['\n', ..])
                    {
                        // 行末書き込み無しでなければ書く
                        if (written != false)
                        {
                            if (lineBreak.IsEmpty) builder.Append(remaining[0]);
                            else builder.Append(lineBreak);
                        }
                        // 行末消費
                        remaining = remaining[1..];
                    }
                }

                // ファイル内容を空にする
                file.Position = 0;
                file.SetLength(0);

                // 更新した内容を書き込み
                using var writer = new StreamWriter(file, encoding, leaveOpen: true);
                writer.Write(builder);
            }

            self.Refresh();
            return self;
        }

        /// <summary>ファイルを行ごとに更新する</summary>
        /// <param name="updater">行更新デリゲート</param>
        /// <param name="lineBreak">行末文字。</param>
        /// <param name="encoding">読み書きテキストエンコーディング</param>
        /// <param name="cancelToken">キャンセルトークン</param>
        /// <returns>書き込みを行いレシーバ自身を返すタスク</returns>
        public async Task<FileInfo> UpdateAllLinesAsync(LineUpdater updater, string? lineBreak = default, Encoding? encoding = default, CancellationToken cancelToken = default)
        {
            ArgumentNullException.ThrowIfNull(self);

            // 読み書きオープン
            using (var file = self.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None))
            {
                // デフォルトはUTF8とする
                encoding ??= Encoding.UTF8;

                // ファイル内容を全てテキスト読み取り
                var contents = default(string);
                using (var reader = new StreamReader(file, encoding, detectEncodingFromByteOrderMarks: true, leaveOpen: true))
                {
                    contents = await reader.ReadToEndAsync(cancelToken);
                }

                // 更新テキストを作成
                var builder = new StringBuilder(capacity: contents.Length + 1024);

                // 1行ずつ処理してバッファに溜めていく
                var remaining = contents.AsSpan();
                while (!remaining.IsEmpty)
                {
                    // 行の切り出し
                    var termIdx = remaining.IndexOfAny(['\r', '\n']);
                    var line = termIdx < 0 ? remaining : remaining[..termIdx];
                    remaining = remaining[line.Length..];

                    // 行の更新処理
                    var written = updater(line, builder);

                    // 行末処理
                    if (remaining is ['\r', '\n', ..])
                    {
                        // 行末書き込み無しでなければ書く
                        if (written != false)
                        {
                            if (lineBreak == null) builder.Append("\r\n");
                            else builder.Append(lineBreak);
                        }
                        // 行末消費
                        remaining = remaining[2..];
                    }
                    else if (remaining is ['\r', ..] or ['\n', ..])
                    {
                        // 行末書き込み無しでなければ書く
                        if (written != false)
                        {
                            if (lineBreak == null) builder.Append(remaining[0]);
                            else builder.Append(lineBreak);
                        }
                        // 行末消費
                        remaining = remaining[1..];
                    }
                }

                // ファイル内容を空にする
                file.Position = 0;
                file.SetLength(0);

                // 更新した内容を書き込み
                using var writer = new StreamWriter(file, encoding, leaveOpen: true);
                await writer.WriteAsync(builder, cancelToken);
            }

            self.Refresh();
            return self;
        }
    }
    #endregion

    #region Read JSON
    /// <summary>JSON読み取り系のメソッド</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    extension(FileInfo self)
    {
        /// <summary>ファイルからJSONデータを読み取る</summary>
        /// <typeparam name="TObject">JSONをデシリアライズする型</typeparam>
        /// <param name="cancelToken">キャンセルトークン</param>
        /// <returns>デシリアライズしたインスタンス</returns>
        [RequiresUnreferencedCode("This uses JsonSerializer.")]
        [RequiresDynamicCode("This uses JsonSerializer.")]
        public ValueTask<TObject?> ReadJsonAsync<TObject>(CancellationToken cancelToken)
        => self.ReadJsonAsync<TObject>(options: default, cancelToken);

        /// <summary>ファイルからJSONデータを読み取る</summary>
        /// <typeparam name="TObject">JSONをデシリアライズする型</typeparam>
        /// <param name="typeInfo">変換メタデータ</param>
        /// <param name="cancelToken">キャンセルトークン</param>
        /// <returns>デシリアライズしたインスタンス</returns>
        public async ValueTask<TObject?> ReadJsonAsync<TObject>(JsonTypeInfo<TObject> typeInfo, CancellationToken cancelToken = default)
        {
            using var stream = self.OpenRead();
            return await JsonSerializer.DeserializeAsync(stream, typeInfo, cancelToken).ConfigureAwait(false);
        }

        /// <summary>ファイルからJSONデータを読み取る</summary>
        /// <typeparam name="TObject">JSONをデシリアライズする型</typeparam>
        /// <param name="options">デシリアライズオプション</param>
        /// <param name="cancelToken">キャンセルトークン</param>
        /// <returns>デシリアライズしたインスタンス</returns>
        [RequiresUnreferencedCode("This uses JsonSerializer.")]
        [RequiresDynamicCode("This uses JsonSerializer.")]
        public async ValueTask<TObject?> ReadJsonAsync<TObject>(JsonSerializerOptions? options = null, CancellationToken cancelToken = default)
        {
            using var stream = self.OpenRead();
            return await JsonSerializer.DeserializeAsync<TObject>(stream, options, cancelToken).ConfigureAwait(false);
        }

        /// <summary>ファイルからJSONデータを読み取る</summary>
        /// <typeparam name="TObject">JSONをデシリアライズする型</typeparam>
        /// <param name="template">
        /// JSONをデシリアライズする型を示す値。
        /// インスタンス値が利用されることはなく型情報のみを参照する。
        /// 型情報は静的な型が参照される。インスタンスの実体型は影響を与えない。
        /// </param>
        /// <param name="cancelToken">キャンセルトークン</param>
        /// <returns>デシリアライズしたインスタンス</returns>
        [RequiresUnreferencedCode("This uses JsonSerializer.")]
        [RequiresDynamicCode("This uses JsonSerializer.")]
        public ValueTask<TObject?> ReadJsonByTemplateAsync<TObject>(TObject? template, CancellationToken cancelToken)
            => self.ReadJsonByTemplateAsync<TObject>(template, default, cancelToken);

        /// <summary>ファイルからJSONデータを読み取る</summary>
        /// <typeparam name="TObject">JSONをデシリアライズする型</typeparam>
        /// <param name="template">
        /// JSONをデシリアライズする型を示す値。
        /// インスタンス値が利用されることはなく型情報のみを参照する。
        /// 型情報は静的な型が参照される。インスタンスの実体型は影響を与えない。
        /// </param>
        /// <param name="options">デシリアライズオプション</param>
        /// <param name="cancelToken">キャンセルトークン</param>
        /// <returns>デシリアライズしたインスタンス</returns>
        [RequiresUnreferencedCode("This uses JsonSerializer.")]
        [RequiresDynamicCode("This uses JsonSerializer.")]
        public async ValueTask<TObject?> ReadJsonByTemplateAsync<TObject>(TObject? template, JsonSerializerOptions? options = null, CancellationToken cancelToken = default)
        {
            _ = template;
            using var stream = self.OpenRead();
            var decoded = await JsonSerializer.DeserializeAsync(stream, typeof(TObject), options, cancelToken).ConfigureAwait(false);
            return (TObject?)decoded;
        }

        /// <summary>ファイルから緩くJSONデータを読み取る</summary>
        /// <typeparam name="TObject">JSONをデシリアライズする型</typeparam>
        /// <param name="cancelToken">キャンセルトークン</param>
        /// <returns>デシリアライズしたインスタンス</returns>
        [RequiresUnreferencedCode("This uses JsonSerializer.")]
        [RequiresDynamicCode("This uses JsonSerializer.")]
        public ValueTask<TObject?> ReadRoughJsonAsync<TObject>(CancellationToken cancelToken = default)
            => self.ReadJsonAsync<TObject>(JsonRoughReadOptions, cancelToken);
    }
    #endregion

    #region Write JSON
    /// <summary>JSON書き込み系のメソッド</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    extension(FileInfo self)
    {
        /// <summary>オブジェクトをJSON形式でファイルに保存する</summary>
        /// <typeparam name="TObject">JSONにデシリアライズする型</typeparam>
        /// <param name="value">保存するオブジェクト</param>
        /// <param name="cancelToken">キャンセルトークン</param>
        /// <returns>書き込みを行いレシーバ自身を返すタスク</returns>
        [RequiresUnreferencedCode("This uses JsonSerializer.")]
        [RequiresDynamicCode("This uses JsonSerializer.")]
        public ValueTask<FileInfo> WriteJsonAsync<TObject>(TObject value, CancellationToken cancelToken)
        => self.WriteJsonAsync(value, options: default, cancelToken);

        /// <summary>オブジェクトをJSON形式でファイルに保存する</summary>
        /// <typeparam name="TObject">JSONにデシリアライズする型</typeparam>
        /// <param name="value">保存するオブジェクト</param>
        /// <param name="typeInfo">変換メタデータ</param>
        /// <param name="cancelToken">キャンセルトークン</param>
        /// <returns>書き込みを行いレシーバ自身を返すタスク</returns>
        public async ValueTask<FileInfo> WriteJsonAsync<TObject>(TObject value, JsonTypeInfo<TObject> typeInfo, CancellationToken cancelToken = default)
        {
            using var stream = self.CreateWrite();
            await JsonSerializer.SerializeAsync(stream, value, typeInfo, cancelToken).ConfigureAwait(false);
            return self;
        }

        /// <summary>オブジェクトをJSON形式でファイルに保存する</summary>
        /// <typeparam name="TObject">JSONにデシリアライズする型</typeparam>
        /// <param name="value">保存するオブジェクト</param>
        /// <param name="options">シリアライズオプション</param>
        /// <param name="cancelToken">キャンセルトークン</param>
        /// <returns>書き込みを行いレシーバ自身を返すタスク</returns>
        [RequiresUnreferencedCode("This uses JsonSerializer.")]
        [RequiresDynamicCode("This uses JsonSerializer.")]
        public async ValueTask<FileInfo> WriteJsonAsync<TObject>(TObject value, JsonSerializerOptions? options = null, CancellationToken cancelToken = default)
        {
            using var stream = self.CreateWrite();
            await JsonSerializer.SerializeAsync(stream, value, options, cancelToken).ConfigureAwait(false);
            return self;
        }

        /// <summary>オブジェクトを整形したJSON形式でファイルに保存する</summary>
        /// <typeparam name="TObject">JSONにデシリアライズする型</typeparam>
        /// <param name="value">保存するオブジェクト</param>
        /// <param name="cancelToken">キャンセルトークン</param>
        /// <returns>書き込みを行いレシーバ自身を返すタスク</returns>
        [RequiresUnreferencedCode("This uses JsonSerializer.")]
        [RequiresDynamicCode("This uses JsonSerializer.")]
        public ValueTask<FileInfo> WritePrettyJsonAsync<TObject>(TObject value, CancellationToken cancelToken = default)
            => self.WriteJsonAsync(value, JsonPrettyWriteOptions, cancelToken);

        /// <summary>オブジェクトを整形したJSON形式でファイルに保存する</summary>
        /// <typeparam name="TObject">JSONにデシリアライズする型</typeparam>
        /// <param name="value">保存するオブジェクト</param>
        /// <param name="ignoreNulls">nullプロパティを無視するか否か</param>
        /// <param name="cancelToken">キャンセルトークン</param>
        /// <returns>書き込みを行いレシーバ自身を返すタスク</returns>
        [RequiresUnreferencedCode("This uses JsonSerializer.")]
        [RequiresDynamicCode("This uses JsonSerializer.")]
        public ValueTask<FileInfo> WritePrettyJsonAsync<TObject>(TObject value, bool ignoreNulls, CancellationToken cancelToken = default)
            => self.WriteJsonAsync(value, ignoreNulls ? JsonPrettyNullIgnoreWriteOptions : JsonPrettyWriteOptions, cancelToken);
    }
    #endregion

    #region FileSystem
    /// <summary>ファイルシステム系のメソッド</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    extension(FileInfo self)
    {
        /// <summary>ファイルまでのディレクトリを作成する。</summary>
        /// <returns>元のファイル情報</returns>
        public FileInfo WithDirectoryCreate()
        {
            ArgumentNullException.ThrowIfNull(self);
            self.Directory?.WithCreate();
            return self;
        }

        /// <summary>ファイルの作成または更新日時の更新を行う</summary>
        /// <returns>元のファイル情報</returns>
        public FileInfo Touch()
        {
            ArgumentNullException.ThrowIfNull(self);
            self.Directory?.Create();
            using var stream = new FileStream(self.FullName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None, bufferSize: 0);
            self.LastWriteTimeUtc = DateTime.UtcNow;
            return self;
        }

        /// <summary>ファイルをリネームする</summary>
        /// <param name="name">新しい名前。元の場所を基準とした名前。</param>
        /// <returns>リネームした対象ファイル情報。元のインスタンスと同一。</returns>
        public FileInfo Rename(string name)
        {
            if (self.DirectoryName == null) throw new InvalidOperationException();
            var newPath = Path.Combine(self.DirectoryName, name);
            self.MoveTo(newPath);
            return self;
        }
    }
    #endregion

    #region Find
    /// <summary>検索系のメソッド</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    extension(FileInfo self)
    {
        /// <summary>指定の名前の祖先ディレクトリを探す</summary>
        /// <param name="name">ディレクトリ名</param>
        /// <param name="ignoreCase">大文字と小文字を同一視するか否か</param>
        /// <returns>条件にマッチしたディレクトリ。見つからない場合は null</returns>
        public DirectoryInfo? FindAncestor(string name, bool? ignoreCase = default)
        {
            var matchRule = (ignoreCase ?? RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
            return self.FindAncestor(dir => string.Equals(dir.Name, name, matchRule));
        }

        /// <summary>指定の条件にマッチする祖先ディレクトリを探す</summary>
        /// <param name="finder">条件にマッチするかを判定するデリゲート</param>
        /// <returns>条件にマッチしたディレクトリ。見つからない場合は null</returns>
        public DirectoryInfo? FindAncestor(Func<DirectoryInfo, bool> finder)
        {
            var scan = self.Directory;
            while (scan != null)
            {
                if (finder(scan)) break;
                scan = scan.Parent;
            }
            return scan;
        }
    }
    #endregion

    #region Path
    /// <summary>パス系のメソッド</summary>
    /// <param name="self">対象ファイルのFileInfo</param>
    extension(FileInfo self)
    {
        /// <summary>ファイルパスの構成セグメントを取得する。</summary>
        /// <returns>パス構成セグメントのリスト</returns>
        public IList<string> GetPathSegments()
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
        /// <param name="other">比較するディレクトリ</param>
        /// <returns>指定ディレクトリの子孫であるか否か</returns>
        public bool IsDescendantOf(DirectoryInfo other)
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
        /// <param name="baseDir">基準ディレクトリのDirectoryInfo</param>
        /// <param name="ignoreCase">大文字と小文字を同一視するか否か</param>
        /// <returns>相対パス</returns>
        public string RelativePathFrom(DirectoryInfo baseDir, bool? ignoreCase = default)
        {
            // パラメータチェック
            ArgumentNullException.ThrowIfNull(self);
            ArgumentNullException.ThrowIfNull(baseDir);

            return DirectoryInfoExtensions.SegmentsToReletivePath(self.GetPathSegments(), baseDir, ignoreCase);
        }
    }
    #endregion

    // 非公開フィールド
    #region Fixed Values
    /// <summary>整形保存用オプション</summary>
    private static readonly JsonSerializerOptions JsonPrettyWriteOptions = new JsonSerializerOptions
    {
        WriteIndented = true,
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
    };

    /// <summary>整形保存(null無視)用オプション</summary>
    private static readonly JsonSerializerOptions JsonPrettyNullIgnoreWriteOptions = new JsonSerializerOptions
    {
        WriteIndented = true,
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
    };

    /// <summary>大雑把な読み取り用オプション</summary>
    private static readonly JsonSerializerOptions JsonRoughReadOptions = new JsonSerializerOptions
    {
        AllowTrailingCommas = true,
        ReadCommentHandling = JsonCommentHandling.Skip,
        NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString,
    };
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
