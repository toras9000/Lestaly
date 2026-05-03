namespace Lestaly;

/// <summary>FileSystemInfo に対する拡張メソッド</summary>
public static class FileSystemInfoExtensions
{
    #region Convert
    /// <summary>変換系メソッド</summary>
    /// <param name="self">パス文字列</param>
    extension(string self)
    {
        /// <summary>文字列をパスとして FileInfo インスタンスを作成する</summary>
        /// <returns>FileInfo インスタンス</returns>
        public FileInfo AsFileInfo()
            => new FileInfo(self);

        /// <summary>文字列をパスとして DirectoryInfo インスタンスを作成する</summary>
        /// <returns>DirectoryInfo インスタンス</returns>
        public DirectoryInfo AsDirectoryInfo()
            => new DirectoryInfo(self);
    }
    #endregion

    #region FileSystem
    /// <summary>ファイルシステム系メソッド</summary>
    /// <param name="self">対象ファイル/ディレクトリ情報</param>
    extension<TInfo>(TInfo self) where TInfo : FileSystemInfo
    {
        /// <summary>ファイル/ディレクトリがReadOnlyであるかを取得する。</summary>
        /// <returns>ReadOnlyであるか否か</returns>
        public bool GetReadOnly()
        {
            ArgumentNullException.ThrowIfNull(self);
            return (self.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly;
        }

        /// <summary>ファイル/ディレクトリのReadOnly属性を設定する。</summary>
        /// <param name="readOnly">設定するReadOnly状態</param>
        /// <returns>対象ファイル/ディレクトリ情報</returns>
        public TInfo SetReadOnly(bool readOnly)
        {
            ArgumentNullException.ThrowIfNull(self);
            if (readOnly)
            {
                self.Attributes |= FileAttributes.ReadOnly;
            }
            else
            {
                self.Attributes &= ~FileAttributes.ReadOnly;
            }
            return self;
        }
    }
    #endregion

    #region Path
    /// <summary>パス系メソッド</summary>
    /// <param name="self">対象ファイルシステムアイテムのFileSystemInfo</param>
    extension(FileSystemInfo self)
    {
        /// <summary>ファイルパスの構成セグメントを取得する。</summary>
        /// <returns>パス構成セグメントのリスト</returns>
        public IList<string> GetPathSegments()
        {
            return self switch
            {
                FileInfo file => file.GetPathSegments(),
                DirectoryInfo dir => dir.GetPathSegments(),
                _ => throw new InvalidCastException(),
            };
        }

        /// <summary>ファイルが指定のディレクトリの子孫であるかを判定する。</summary>
        /// <remarks></remarks>
        /// <param name="other">比較するディレクトリ</param>
        /// <param name="sameIs">同一階層を真とするか否か</param>
        /// <returns>指定ディレクトリの子孫であるか否か</returns>
        public bool IsDescendantOf(DirectoryInfo other, bool sameIs = true)
        {
            return self switch
            {
                FileInfo file => file.IsDescendantOf(other),
                DirectoryInfo dir => dir.IsDescendantOf(other, sameIs),
                _ => throw new InvalidCastException(),
            };
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
            return self switch
            {
                FileInfo file => file.RelativePathFrom(baseDir, ignoreCase),
                DirectoryInfo dir => dir.RelativePathFrom(baseDir, ignoreCase),
                _ => throw new InvalidCastException(),
            };
        }
    }
    #endregion

    #region Info
    /// <summary>ファイルシステム系メソッド</summary>
    /// <param name="self">対象ファイル/ディレクトリ情報</param>
    extension<TInfo>(TInfo self) where TInfo : FileSystemInfo
    {
        /// <summary>ファイルシステムオブジェクトの状態を更新する。</summary>
        /// <returns>元のファイル/ディレクトリ情報</returns>
        public TInfo WithRefresh()
        {
            self.Refresh();
            return self;
        }

        /// <summary>ファイルシステムオブジェクトが存在する場合に null に置き換える。</summary>
        /// <returns>元のファイル/ディレクトリ情報もしくは null</returns>
        public TInfo? OmitExists()
            => self.Exists ? default : self;

        /// <summary>ファイルシステムオブジェクトが存在しない場合に例外を送出する。</summary>
        /// <returns>元のファイル/ディレクトリ情報</returns>
        public TInfo? OmitNotExists()
            => self.Exists ? self : default;

        /// <summary>ファイルシステムオブジェクトが存在する場合に例外を送出する。</summary>
        /// <param name="generator">例外オブジェクト生成デリゲート</param>
        /// <returns>元のファイル/ディレクトリ情報</returns>
        public TInfo ThrowIfExists(Func<TInfo, Exception>? generator = null)
        {
            if (self.Exists)
            {
                throw generator?.Invoke(self) ?? self switch
                {
                    FileInfo f => new FileNotFoundException($"`{f.FullName}` is already exists.", f.FullName),
                    DirectoryInfo d => new DirectoryNotFoundException($"`{d.FullName}` is already exists."),
                    _ => new InvalidDataException(),
                };
            }
            return self;
        }

        /// <summary>ファイルシステムオブジェクトが存在しない場合に例外を送出する。</summary>
        /// <param name="generator">例外オブジェクト生成デリゲート</param>
        /// <returns>元のファイル/ディレクトリ情報</returns>
        public TInfo ThrowIfNotExists(Func<TInfo, Exception>? generator = null)
        {
            if (!self.Exists)
            {
                throw generator?.Invoke(self) ?? self switch
                {
                    FileInfo f => new FileNotFoundException($"`{f.FullName}` is not found.", f.FullName),
                    DirectoryInfo d => new DirectoryNotFoundException($"`{d.FullName}` is not found."),
                    _ => new InvalidDataException(),
                };
            }
            return self;
        }

        /// <summary>ファイルシステムオブジェクトが存在する場合にキャンセル例外を送出する。</summary>
        /// <param name="generator">例外メッセージ生成デリゲート</param>
        /// <returns>元のファイル/ディレクトリ情報</returns>
        public TInfo CanceIfExists(Func<TInfo, string>? generator = null)
            => self.ThrowIfExists(i => new OperationCanceledException(generator?.Invoke(i)));

        /// <summary>ファイルシステムオブジェクトが存在しない場合にキャンセル例外を送出する。</summary>
        /// <param name="generator">例外メッセージ生成デリゲート</param>
        /// <returns>元のファイル/ディレクトリ情報</returns>
        public TInfo CanceIfNotExists(Func<TInfo, string>? generator = null)
            => self.ThrowIfNotExists(i => new OperationCanceledException(generator?.Invoke(i)));
    }
    #endregion

}
