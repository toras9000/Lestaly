using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Lestaly;

/// <summary>
/// DirectoryInfo に対する拡張メソッド
/// </summary>
public static class DirectoryInfoExtensions
{
    #region FileSystemInfo
    /// <summary>ディレクトリからの相対パス位置に対する FileInfo を取得する。</summary>
    /// <param name="self">基準となるディレクトリの DirectoryInfo</param>
    /// <param name="relativePath">基準ディレクトリからのパス。もし絶対パスの場合は基準ディレクトリは無関係にこの絶対パスが利用される。</param>
    /// <returns>対象ファイルパスの FileInfo。相対パスが空や空白の場合は null を返却。</returns>
    public static FileInfo? RelativeFileAt(this DirectoryInfo self, string? relativePath)
    {
        ArgumentNullException.ThrowIfNull(self);
        if (string.IsNullOrWhiteSpace(relativePath)) return default;
        return new FileInfo(Path.Combine(self.FullName, relativePath));
    }

    /// <summary>ディレクトリからの相対パス位置に対する FileInfo を取得する。</summary>
    /// <param name="self">基準となるディレクトリの DirectoryInfo</param>
    /// <param name="relativePath">基準ディレクトリからのパス。もし絶対パスの場合は基準ディレクトリは無関係にこの絶対パスが利用される。</param>
    /// <returns>対象ファイルパスの FileInfo。</returns>
    public static FileInfo RelativeFile(this DirectoryInfo self, string relativePath)
    {
        ArgumentNullException.ThrowIfNull(self);
        if (string.IsNullOrWhiteSpace(relativePath)) throw new ArgumentException($"Invalid relative path.");
        return new FileInfo(Path.Combine(self.FullName, relativePath));
    }

    /// <summary>ディレクトリからの相対パス位置に対する DirectoryInfo を取得する。</summary>
    /// <param name="self">基準となるディレクトリのDirectoryInfo</param>
    /// <param name="relativePath">基準ディレクトリからのパス。もし絶対パスの場合は基準ディレクトリは無関係にこの絶対パスが利用される。</param>
    /// <returns>対象ディレクトリパスの DirectoryInfo。相対パスが空や空白の場合は null を返却。</returns>
    public static DirectoryInfo? RelativeDirectoryAt(this DirectoryInfo self, string? relativePath)
    {
        ArgumentNullException.ThrowIfNull(self);
        if (string.IsNullOrWhiteSpace(relativePath)) return default;
        return new DirectoryInfo(Path.Combine(self.FullName, relativePath));
    }

    /// <summary>ディレクトリからの相対パス位置に対する DirectoryInfo を取得する。</summary>
    /// <param name="self">基準となるディレクトリのDirectoryInfo</param>
    /// <param name="relativePath">基準ディレクトリからのパス。もし絶対パスの場合は基準ディレクトリは無関係にこの絶対パスが利用される。</param>
    /// <returns>対象ディレクトリパスの DirectoryInfo。相対パスが空や空白の場合は基準ディレクトリを返却。</returns>
    public static DirectoryInfo RelativeDirectory(this DirectoryInfo self, string? relativePath)
    {
        ArgumentNullException.ThrowIfNull(self);
        if (string.IsNullOrWhiteSpace(relativePath)) return self;
        return new DirectoryInfo(Path.Combine(self.FullName, relativePath));
    }
    #endregion

    #region FileSystem
    /// <summary>ディレクトリを作成する。</summary>
    /// <param name="self">対象ディレクトリ情報</param>
    /// <returns>元のディレクトリ情報</returns>
    public static DirectoryInfo WithCreate(this DirectoryInfo self)
    {
        ArgumentNullException.ThrowIfNull(self);
        self.Create();
        return self;
    }
    #endregion

    #region Find
    /// <summary>ディレクトリ配下の指定のパターンにマッチする単一のファイルを取得する。</summary>
    /// <param name="self">基準となるディレクトリ</param>
    /// <param name="pattern">検索パターン。パターン解釈は MatchType.Simple による。パスが階層状の場合、途中のパスはプラットフォーム依存のマッチングのようなので注意。</param>
    /// <param name="casing">キャラクタ照合方法</param>
    /// <param name="first">複数ファイルが見つかった場合に最初のファイルを返すか否か</param>
    /// <returns>
    /// 検索結果が単一の場合はそのファイル情報。見つからない場合は null を返却。
    /// 検索結果が複数の場合、first 引数が真であれば最初のファイル情報を、そうでなければ null を返却。
    /// </returns>
    public static FileInfo? FindFile(this DirectoryInfo self, string pattern, MatchCasing casing = MatchCasing.PlatformDefault, bool first = false)
    {
        ArgumentNullException.ThrowIfNull(self);
        ArgumentException.ThrowIfNullOrWhiteSpace(pattern);

        // 検索オプション
        var options = new EnumerationOptions();
        options.MatchCasing = casing;
        options.MatchType = MatchType.Simple;
        options.IgnoreInaccessible = true;
        options.AttributesToSkip = FileAttributes.None;
        options.ReturnSpecialDirectories = false;
        options.RecurseSubdirectories = false;

        // 指定のファイルを検索
        var found = default(FileInfo);
        foreach (var file in self.EnumerateFiles(pattern, options))
        {
            // 2つ以上見つかった場合は未確定として結果無しにする
            if (found != null)
            {
                if (!first) found = null;
                break;
            }
            // 見つかったファイルを保持
            found = file;
        }
        return found;
    }

    /// <summary>ディレクトリ配下の指定のパターンにマッチする単一のファイルを取得する。</summary>
    /// <param name="self">基準となるディレクトリ</param>
    /// <param name="path">検索するパス階層リスト。パス区切り文字を含まない1階層分のエントリのリストであるべき。</param>
    /// <param name="casing">キャラクタ照合方法</param>
    /// <param name="first">途中のパスとファイルで複数項目が見つかった場合に最初の項目を採用するか否か</param>
    /// <returns>
    /// 検索結果が一意に特定できる場合はそのファイル情報。見つからない場合は null を返却。
    /// 経路および結果が複数の場合、first 引数が真であれば最初のファイル情報を、そうでなければ null を返却。
    /// </returns>
    public static FileInfo? FindPathFile(this DirectoryInfo self, Span<string> path, MatchCasing casing = MatchCasing.PlatformDefault, bool first = false)
    {
        ArgumentNullException.ThrowIfNull(self);
        ArgumentOutOfRangeException.ThrowIfZero(path.Length);

        // ファイルの検索基準ディレクトリ
        var place = self;
        if (1 < path.Length)
        {
            // 途中のディレクトリ階層を検索
            place = self.FindPathDirectory(path[..^1], casing, first);
            if (place == null) return null;
        }

        // 基準ディレクトリでのファイル検索
        return place.FindFile(path[^1], casing, first);
    }

    /// <summary>ディレクトリ配下の指定のパターンにマッチする単一のファイルを取得する。</summary>
    /// <param name="self">基準となるディレクトリ</param>
    /// <param name="path">検索するパス階層リスト。パス区切り文字を含まない1階層分のエントリのリストであるべき。</param>
    /// <param name="fuzzy">キャラクタケーシングを無視するか否か</param>
    /// <param name="first">途中のパスとファイルで複数項目が見つかった場合に最初の項目を採用するか否か</param>
    /// <returns>
    /// 検索結果が一意に特定できる場合はそのファイル情報。見つからない場合は null を返却。
    /// 経路および結果が複数の場合、first 引数が真であれば最初のファイル情報を、そうでなければ null を返却。
    /// </returns>
    public static FileInfo? FindPathFile(this DirectoryInfo self, Span<string> path, bool fuzzy, bool first = false)
        => self.FindPathFile(path, fuzzy ? MatchCasing.CaseInsensitive : MatchCasing.CaseSensitive, first);

    /// <summary>ディレクトリ配下の指定のパターンにマッチする単一のディレクトリを取得する。</summary>
    /// <param name="self">基準となるディレクトリ</param>
    /// <param name="pattern">検索パターン。パターン解釈は MatchType.Simple による。パスが階層状の場合、途中のパスはプラットフォーム依存のマッチングのようなので注意。</param>
    /// <param name="casing">キャラクタ照合方法</param>
    /// <param name="first">複数ディレクトリが見つかった場合に最初のディレクトリを返すか否か</param>
    /// <returns>
    /// 検索結果が単一の場合はそのディレクトリ情報。見つからない場合は null を返却。
    /// 検索結果が複数の場合、first 引数が真であれば最初のディレクトリ情報を、そうでなければ null を返却。
    /// </returns>
    public static DirectoryInfo? FindDirectory(this DirectoryInfo self, string pattern, MatchCasing casing = MatchCasing.PlatformDefault, bool first = false)
    {
        ArgumentNullException.ThrowIfNull(self);
        ArgumentException.ThrowIfNullOrWhiteSpace(pattern);

        // 検索オプション
        var options = new EnumerationOptions();
        options.MatchCasing = casing;
        options.MatchType = MatchType.Simple;
        options.IgnoreInaccessible = true;
        options.AttributesToSkip = FileAttributes.None;
        options.ReturnSpecialDirectories = false;
        options.RecurseSubdirectories = false;

        // 指定のディレクトリを検索
        var found = default(DirectoryInfo);
        foreach (var dir in self.EnumerateDirectories(pattern, options))
        {
            // 2つ以上見つかった場合は未確定として結果無しにする
            if (found != null)
            {
                if (!first) found = null;
                break;
            }
            // 見つかったディレクトリを保持
            found = dir;
        }
        return found;
    }

    /// <summary>ディレクトリ配下の指定のパスマッチする単一のディレクトリを取得する。</summary>
    /// <param name="self">基準となるディレクトリ</param>
    /// <param name="path">検索するパス階層リスト。パス区切り文字を含まない1階層分のエントリのリストであるべき。</param>
    /// <param name="casing">キャラクタ照合方法</param>
    /// <param name="first">途中のパスを含めたディレクトリが複数項目が見つかった場合に最初の項目を採用するか否か</param>
    /// <returns>
    /// 検索結果が一意に特定できる場合はそのディレクトリ情報。見つからない場合は null を返却。
    /// 経路およびが複数の場合、first 引数が真であれば最初のディレクトリ情報を、そうでなければ null を返却。
    /// </returns>
    public static DirectoryInfo? FindPathDirectory(this DirectoryInfo self, Span<string> path, MatchCasing casing = MatchCasing.PlatformDefault, bool first = false)
    {
        ArgumentNullException.ThrowIfNull(self);
        ArgumentOutOfRangeException.ThrowIfZero(path.Length);

        // 検索オプション
        var options = new EnumerationOptions();
        options.MatchCasing = casing;
        options.MatchType = MatchType.Simple;
        options.IgnoreInaccessible = true;
        options.AttributesToSkip = FileAttributes.None;
        options.ReturnSpecialDirectories = false;
        options.RecurseSubdirectories = false;

        // 指定のディレクトリを順次検索
        var place = self;
        for (var i = 0; i < path.Length; i++)
        {
            var item = path[i];
            var next = default(DirectoryInfo);
            foreach (var dir in place.EnumerateDirectories(item, options))
            {
                // 2つ以上見つかった場合は未確定として結果無しにする
                if (next != null)
                {
                    if (!first) return null;
                    break;
                }
                // 見つかったファイルを保持
                next = dir;
            }

            // 見つからなければ結果なし
            if (next == null) return null;

            // 次の階層へ
            place = next;
        }

        return place;
    }

    /// <summary>ディレクトリ配下の指定のパスマッチする単一のディレクトリを取得する。</summary>
    /// <param name="self">基準となるディレクトリ</param>
    /// <param name="path">検索するパス階層リスト。パス区切り文字を含まない1階層分のエントリのリストであるべき。</param>
    /// <param name="fuzzy">キャラクタケーシングを無視するか否か</param>
    /// <param name="first">途中のパスを含めたディレクトリが複数項目が見つかった場合に最初の項目を採用するか否か</param>
    /// <returns>
    /// 検索結果が一意に特定できる場合はそのディレクトリ情報。見つからない場合は null を返却。
    /// 経路およびが複数の場合、first 引数が真であれば最初のディレクトリ情報を、そうでなければ null を返却。
    /// </returns>
    public static DirectoryInfo? FindPathDirectory(this DirectoryInfo self, Span<string> path, bool fuzzy, bool first = false)
        => self.FindPathDirectory(path, fuzzy ? MatchCasing.CaseInsensitive : MatchCasing.CaseSensitive, first);
    #endregion

    #region Path
    /// <summary>ディレクトリパスの構成セグメントを取得する。</summary>
    /// <param name="self">対象ディレクトリのDirectoryInfo</param>
    /// <returns>パス構成セグメントのリスト</returns>
    public static IList<string> GetPathSegments(this DirectoryInfo self)
    {
        ArgumentNullException.ThrowIfNull(self);

        // 構成ディレクトリを取得
        var segments = new List<string>(10);
        var part = self;
        while (part != null)
        {
            segments.Add(part.Name);
            part = part.Parent;
        }

        // リスト内容を逆順にする
        segments.Reverse();

        return segments;
    }

    /// <summary>ディレクトリが指定のディレクトリの子孫であるかを判定する。</summary>
    /// <param name="self">対象ディレクトリ</param>
    /// <param name="other">比較するディレクトリ</param>
    /// <param name="sameIs">同一階層を真とするか否か</param>
    /// <returns>指定ディレクトリの子孫であるか否か</returns>
    public static bool IsDescendantOf(this DirectoryInfo self, DirectoryInfo other, bool sameIs = true)
    {
        ArgumentNullException.ThrowIfNull(self);
        ArgumentNullException.ThrowIfNull(other);

        // 対象ディレクトリと比較対象のパス階層を取得
        var selfSegs = self.GetPathSegments();
        var otherSegs = other.GetPathSegments();

        // 比較対象のほうが階層が深い場合はその子孫ではありえない。
        if (selfSegs.Count < otherSegs.Count) return false;

        // 対象ディレクトリが比較対象を全て含んでいるかを判定
        for (var i = 0; i < otherSegs.Count; i++)
        {
            if (!string.Equals(selfSegs[i], otherSegs[i], StringComparison.OrdinalIgnoreCase)) return false;
        }

        // 両者の階層が同じ場合はパラメータで指定された結果とする。
        if (selfSegs.Count == otherSegs.Count) return sameIs;

        return true;
    }

    /// <summary>ディレクトリが指定のディレクトリの祖先であるかを判定する。</summary>
    /// <param name="self">対象ディレクトリ</param>
    /// <param name="other">比較するディレクトリ</param>
    /// <param name="sameIs">同一階層を真とするか否か</param>
    /// <returns>指定ディレクトリの祖先であるか否か</returns>
    public static bool IsAncestorOf(this DirectoryInfo self, DirectoryInfo other, bool sameIs = true)
    {
        ArgumentNullException.ThrowIfNull(self);
        ArgumentNullException.ThrowIfNull(other);

        return other.IsDescendantOf(self, sameIs);
    }

    /// <summary>指定のディレクトリを起点としたディレクトリの相対パスを取得する。</summary>
    /// <remarks>
    /// 単純なパス文字列処理であり、リパースポイントなどを解釈することはない。
    /// </remarks>
    /// <param name="self">対象ディレクトリのDirectoryInfo</param>
    /// <param name="baseDir">基準ディレクトリのDirectoryInfo</param>
    /// <param name="ignoreCase">大文字と小文字を同一視するか否か</param>
    /// <returns>相対パス</returns>
    public static string RelativePathFrom(this DirectoryInfo self, DirectoryInfo baseDir, bool? ignoreCase = default)
    {
        // パラメータチェック
        ArgumentNullException.ThrowIfNull(self);
        ArgumentNullException.ThrowIfNull(baseDir);

        return SegmentsToReletivePath(self.GetPathSegments(), baseDir, ignoreCase);
    }

    /// <summary>パスセグメントから指定のディレクトリを起点とした相対パスを取得する。</summary>
    /// <param name="segments">パスセグメントリスト</param>
    /// <param name="baseDir">基準ディレクトリのDirectoryInfo</param>
    /// <param name="ignoreCase">大文字と小文字を同一視するか否か</param>
    /// <returns>相対パス</returns>
    internal static string SegmentsToReletivePath(IList<string> segments, DirectoryInfo baseDir, bool? ignoreCase)
    {
        // パスセグメント長をチェック
        if (segments.Count <= 0)
        {
            // 一応後続処理のためのガードとして。
            return String.Empty;
        }

        // ディレクトリのパスセグメントを取得
        var dirSegments = baseDir.GetPathSegments();

        // ファイルとディレクトリのパスセグメントで一致する部分を検出
        var index = 0;
        var matchRule = (ignoreCase ?? RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
        while (index < dirSegments.Count)
        {
            // ファイル名位置まで到達した場合はそこで終了。
            if (segments.Count <= index)
            {
                break;
            }

            // 一致しないディレクトリ名を見つけたらそこで終了
            var match = string.Equals(dirSegments[index], segments[index], matchRule);
            if (!match)
            {
                break;
            }

            // 一致している場合は次のセグメントへ
            index++;
        }

        var builder = new StringBuilder();

        // もし最初のセグメントから不一致だった場合は相対表現にならないので元のファイルフルパスを返却
        if (index != 0)
        {
            // 相対パスを構築：一致しなかった基準ディレクトリの残り数分だけ上に辿る必要がある。
            for (var i = index; i < dirSegments.Count; i++)
            {
                builder.Append("..");
                builder.Append(Path.DirectorySeparatorChar);
            }
        }
        // 相対パスを構築：一致しなかったディレクトリ分とファイル名を繋げる
        for (var i = index; i < segments.Count; i++)
        {
            builder.Append(segments[i]);

            // 必要な場合に区切り文字を追加
            if (0 < builder.Length && i < (segments.Count - 1))
            {
                // パスの最初のセグメントではルートを示す区切り文字が含まれる場合がある。
                // その場合を除外するために、区切り文字が付いていない場合のみ付与する。
                var lastChar = builder[^1];
                if (lastChar != Path.DirectorySeparatorChar && lastChar != Path.AltDirectorySeparatorChar)
                {
                    builder.Append(Path.DirectorySeparatorChar);
                }
            }
        }

        return builder.ToString();
    }
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
    public static IEnumerable<TResult> VisitFiles<TResult>(this DirectoryInfo self, VisitFilesWalker<TResult> selector, VisitFilesOptions? options = null)
    {
        ArgumentNullException.ThrowIfNull(self);
        ArgumentNullException.ThrowIfNull(selector);

        // ディレクトリ配下を検索するローカル関数
        static IEnumerable<TResult> enumerate(VisitFilesContext<TResult> context, DirectoryInfo directory)
        {
            // ファイルのハンドリングが有効であればファイルを処理
            if (context.Handling.File)
            {
                // ファイルの列挙シーケンスを取得。オプションによってバッファリングとソート。
                var files = context.Options.Buffered ? directory.GetFiles("*", context.Enumeration) : directory.EnumerateFiles("*", context.Enumeration);
                if (context.Options.Sort) files = files.OrderBy(f => f.Name);

                // ファイル列挙
                foreach (var file in files)
                {
                    // フィルタの適用
                    var pass = context.Options.FileFilter?.Invoke(file) ?? true;
                    if (!pass) continue;

                    // ファイル処理上の状態初期化
                    context.State.SetFile(file);

                    // ファイルに対する変換処理を呼び出し
                    context.Selector(context.State);

                    // 結果が設定されていたら列挙する。
                    if (context.State.HasValue) yield return context.State.Value;

                    // 列挙終了が指定されているかを判定
                    if (context.State.Exit)
                    {
                        yield break;    // 列挙終了
                    }
                    else
                    {
                        // ファイル列挙中の中断指定はファイルの列挙を停止する。そのままディレクトリ列挙へ。
                        if (context.State.Break) break;
                    }
                }
            }

            // ディレクトリに対する処理が無効であればここで終える
            if (!context.Options.Recurse && !context.Handling.Directory) yield break;

            // サブディレクトリの列挙シーケンスを取得。オプションによってバッファリングとソート。
            var subdirs = context.Options.Buffered ? directory.GetDirectories("*", context.Enumeration) : directory.EnumerateDirectories("*", context.Enumeration);
            if (context.Options.Sort) subdirs = subdirs.OrderBy(d => d.Name);

            // サブディレクトリ列挙
            foreach (var subdir in subdirs)
            {
                // フィルタの適用
                var pass = context.Options.DirectoryFilter?.Invoke(subdir) ?? true;
                if (!pass) continue;

                // ディレクトリのハンドリングが有効であればディレクトリを処理
                if (context.Handling.Directory)
                {
                    // このディレクトリ処理のための状態初期化
                    context.State.SetDirectory(subdir);

                    // ディレクトリに対する変換処理(というか主に継続判定)を呼び出し
                    context.Selector(context.State);

                    // ディレクトリに対してでも結果が設定されていたら列挙する。
                    if (context.State.HasValue) yield return context.State.Value;

                    // 列挙終了が指定されているかを判定
                    if (context.State.Exit)
                    {
                        yield break;    // 列挙終了
                    }
                    else
                    {
                        // ディレクトリに対する中断指定はディレクトリのスキップ。次のディレクトリ列挙へ。
                        if (context.State.Break) continue;
                    }
                }

                // 再帰検索が有効であればサブディレクトリ配下を検索し、変換結果を列挙
                if (context.Options.Recurse)
                {
                    foreach (var result in enumerate(context, subdir))
                    {
                        yield return result;
                    }
                }

                // ディレクトリ内で列挙終了が指定されていたらこの階層からも抜ける
                if (context.State.Exit) yield break;
            }
        }

        // 検索オプションが指定されていなければデフォルト設定とする。
        options ??= new();

        // 列挙ハンドリングを決定
        var handling = options.Handling ?? VisitFilesHandling.Default;

        // ファイルシステム列挙オプション
        var enumeration = new EnumerationOptions();
        enumeration.RecurseSubdirectories = false;  // 独自の再帰処理をするため、このオプションは無効で使う。
        enumeration.MatchType = MatchType.Simple;
        enumeration.IgnoreInaccessible = options.SkipInaccessible;
        enumeration.AttributesToSkip = options.SkipAttributes;

        // 列挙
        var context = new VisitFilesContext<TResult>(new VisitFileWalkContext<TResult>(self), selector, options, handling, enumeration);
        return enumerate(context, self);
    }

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
    /// <returns>変換結果の非同期シーケンス</returns>
    public static IAsyncEnumerable<TResult> VisitFilesAsync<TResult>(this DirectoryInfo self, AsyncVisitFilesWalker<TResult> selector, VisitFilesOptions? options = null)
    {
        ArgumentNullException.ThrowIfNull(self);
        ArgumentNullException.ThrowIfNull(selector);

        // ディレクトリ配下を検索するローカル関数
        static async IAsyncEnumerable<TResult> enumerateAsync(VisitFilesAsyncContext<TResult> context, DirectoryInfo directory)
        {
            // ファイルの列挙シーケンスを取得。オプションによってバッファリングとソート。
            var files = context.Options.Buffered ? directory.GetFiles("*", context.Enumeration) : directory.EnumerateFiles("*", context.Enumeration);
            if (context.Options.Sort) files = files.OrderBy(f => f.Name);

            // ファイルのハンドリングが有効であればファイルを処理
            if (context.Handling.File)
            {
                // ファイル列挙
                foreach (var file in files)
                {
                    // フィルタの適用
                    var pass = context.Options.FileFilter?.Invoke(file) ?? true;
                    if (!pass) continue;

                    // ファイル処理上の状態初期化
                    context.State.SetFile(file);

                    // ファイルに対する変換処理を呼び出し
                    await context.Selector(context.State).ConfigureAwait(false);

                    // 結果が設定されていたら列挙する。
                    if (context.State.HasValue) yield return context.State.Value;

                    // 列挙終了が指定されているかを判定
                    if (context.State.Exit)
                    {
                        yield break;    // 列挙終了
                    }
                    else
                    {
                        // ファイル列挙中の中断指定はファイルの列挙を停止する。そのままディレクトリ列挙へ。
                        if (context.State.Break) break;
                    }
                }
            }

            // ディレクトリに対する処理が無効であればここで終える
            if (!context.Options.Recurse && !context.Handling.Directory) yield break;

            // サブディレクトリの列挙シーケンスを取得。オプションによってバッファリングとソート。
            var subdirs = context.Options.Buffered ? directory.GetDirectories("*", context.Enumeration) : directory.EnumerateDirectories("*", context.Enumeration);
            if (context.Options.Sort) subdirs = subdirs.OrderBy(d => d.Name);

            // サブディレクトリ列挙
            foreach (var subdir in subdirs)
            {
                // フィルタの適用
                var pass = context.Options.DirectoryFilter?.Invoke(subdir) ?? true;
                if (!pass) continue;

                // ディレクトリに対するハンドリングが有効な場合は処理デリゲートを呼び出す
                if (context.Handling.Directory)
                {
                    // このディレクトリ処理のための状態初期化
                    context.State.SetDirectory(subdir);

                    // ディレクトリに対する変換処理(というか主に継続判定)を呼び出し
                    await context.Selector(context.State).ConfigureAwait(false);

                    // ディレクトリに対してでも結果が設定されていたら列挙する。
                    if (context.State.HasValue) yield return context.State.Value;

                    // 列挙終了が指定されているかを判定
                    if (context.State.Exit)
                    {
                        yield break;    // 列挙終了
                    }
                    else
                    {
                        // ディレクトリに対する中断指定はディレクトリのスキップ。次のディレクトリ列挙へ。
                        if (context.State.Break) continue;
                    }
                }


                // 再帰検索が有効であればサブディレクトリ配下を検索し、変換結果を列挙
                if (context.Options.Recurse)
                {
                    // サブディレクトリ配下を再帰検索し、変換結果を列挙
                    await foreach (var result in enumerateAsync(context, subdir))
                    {
                        yield return result;
                    }
                }

                // ディレクトリ内で列挙終了が指定されていたらこの階層からも抜ける
                if (context.State.Exit) yield break;
            }
        }

        // 検索オプションが指定されていなければデフォルト設定とする。
        options ??= new();

        // 列挙ハンドリングを決定
        var handling = options.Handling ?? VisitFilesHandling.Default;

        // ファイルシステム列挙オプション
        var enumeration = new EnumerationOptions();
        enumeration.RecurseSubdirectories = false;  // 独自の再帰処理をするため、このオプションは無効で使う。
        enumeration.MatchType = MatchType.Simple;
        enumeration.IgnoreInaccessible = options.SkipInaccessible;
        enumeration.AttributesToSkip = options.SkipAttributes;

        // 列挙
        var context = new VisitFilesAsyncContext<TResult>(new VisitFileWalkContext<TResult>(self), selector, options, handling, enumeration);
        return enumerateAsync(context, self);
    }

    /// <summary>ディレクトリ配下のファイル/ディレクトリを検索して処理を行う</summary>
    /// <param name="self">検索の起点ディレクトリ</param>
    /// <param name="processor">ファイル/ディレクトリに対する処理</param>
    /// <param name="options">検索オプション</param>
    public static void DoFiles(this DirectoryInfo self, Action<IVisitFilesContext> processor, VisitFilesOptions? options = null)
    {
        ArgumentNullException.ThrowIfNull(self);
        ArgumentNullException.ThrowIfNull(processor);
        foreach (var _ in self.VisitFiles<int>(c => processor(c), options)) ;
    }

    /// <summary>ディレクトリ配下のファイル/ディレクトリを検索して処理を行う</summary>
    /// <param name="self">検索の起点ディレクトリ</param>
    /// <param name="processor">ファイル/ディレクトリに対する処理</param>
    /// <param name="options">検索オプション</param>
    /// <returns>検索処理タスク</returns>
    public static async Task DoFilesAsync(this DirectoryInfo self, Func<IVisitFilesContext, ValueTask> processor, VisitFilesOptions? options = null)
    {
        ArgumentNullException.ThrowIfNull(self);
        ArgumentNullException.ThrowIfNull(processor);
        await foreach (var _ in self.VisitFilesAsync<int>(c => processor(c), options).ConfigureAwait(false)) ;
    }

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
    public static IEnumerable<TResult> SelectFiles<TResult>(this DirectoryInfo self, Func<IVisitFilesContext, TResult> selector, VisitFilesOptions? options = null)
    {
        ArgumentNullException.ThrowIfNull(selector);
        return self.VisitFiles<TResult>(c => c.SetResult(selector(c)), options);
    }

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
    public static IAsyncEnumerable<TResult> SelectFilesAsync<TResult>(this DirectoryInfo self, Func<IVisitFilesContext, ValueTask<TResult>> selector, VisitFilesOptions? options = null)
    {
        ArgumentNullException.ThrowIfNull(selector);
        return self.VisitFilesAsync<TResult>(async c => c.SetResult(await selector(c)), options);
    }

    /// <summary>ファイル列挙変換コンテキスト情報</summary>
    /// <typeparam name="TResult">変換結果の型</typeparam>
    /// <param name="State">ファイル列挙変換状態情報</param>
    /// <param name="Selector">ファイル/ディレクトリに対する変換処理</param>
    /// <param name="Options">検索オプション</param>
    /// <param name="Handling">処理対象設定</param>
    /// <param name="Enumeration">ファイルシステム列挙オプション</param>
    private record VisitFilesContext<TResult>(VisitFileWalkContext<TResult> State, VisitFilesWalker<TResult> Selector, VisitFilesOptions Options, VisitFilesHandling Handling, EnumerationOptions Enumeration);

    /// <summary>ファイル列挙変換コンテキスト情報(非同期)</summary>
    /// <typeparam name="TResult">変換結果の型</typeparam>
    /// <param name="State">ファイル列挙変換状態情報</param>
    /// <param name="Selector">ファイル/ディレクトリに対する変換処理</param>
    /// <param name="Options">検索オプション</param>
    /// <param name="Handling">処理対象設定</param>
    /// <param name="Enumeration">ファイルシステム列挙オプション</param>
    private record VisitFilesAsyncContext<TResult>(VisitFileWalkContext<TResult> State, AsyncVisitFilesWalker<TResult> Selector, VisitFilesOptions Options, VisitFilesHandling Handling, EnumerationOptions Enumeration);

    /// <summary>ファイル列挙変換状態情報</summary>
    /// <typeparam name="TResult">変換結果の型</typeparam>
    private class VisitFileWalkContext<TResult> : IVisitFilesContext<TResult>
    {
        // 構築
        #region コンストラクタ
        /// <summary>起点ディレクトリを指定するコンストラクタ</summary>
        /// <param name="dir">起点ディレクトリ</param>
        public VisitFileWalkContext(DirectoryInfo dir)
        {
            this.Directory = dir;
            this.Value = default!;
        }
        #endregion

        // 公開プロパティ
        #region IFileWalker インタフェース
        /// <inheritdoc />
        public FileSystemInfo Item => (FileSystemInfo?)this.File ?? this.Directory;

        /// <inheritdoc />
        public DirectoryInfo Directory { get; private set; }

        /// <inheritdoc />
        public FileInfo? File { get; private set; }

        /// <inheritdoc />
        public bool Break { get; set; }

        /// <inheritdoc />
        public bool Exit { get; set; }
        #endregion

        #region 列挙処理向け
        /// <summary>結果値が設定されたかどうか</summary>
        public bool HasValue { get; private set; }

        /// <summary>結果値</summary>
        public TResult Value { get; private set; }
        #endregion

        // 公開メソッド
        #region IFileWalker インタフェース
        /// <inheritdoc />
        public void SetResult(TResult value)
        {
            this.Value = value;
            this.HasValue = true;
        }
        #endregion

        #region 列挙処理向け
        /// <summary>列挙対象ディレクトリを更新する</summary>
        /// <param name="dir">ディレクトリ情報</param>
        public void SetDirectory(DirectoryInfo dir)
        {
            Clear();
            this.Directory = dir;
            this.File = null;
        }

        /// <summary>列挙対象ファイルを更新する</summary>
        /// <param name="file">ファイル情報</param>
        public void SetFile(FileInfo file)
        {
            Clear();
            this.Directory = file.Directory ?? new DirectoryInfo(Path.GetDirectoryName(file.FullName)!);
            this.File = file;
        }

        /// <summary>状態をクリアする</summary>
        public void Clear()
        {
            this.Break = false;
            this.Value = default!;
            this.HasValue = false;
        }
        #endregion
    }
    #endregion

    #region Utility
    /// <summary>ディレクトリ配下のファイルをコピーする</summary>
    /// <param name="self">コピー元ディレクトリ</param>
    /// <param name="dest">コピー先ディレクトリ</param>
    /// <param name="overwrite">上書きするか否か</param>
    /// <param name="options">ファイル列挙オプション</param>
    /// <param name="predicator">ファイルコピー判定デリゲート</param>
    public static void CopyFilesTo(this DirectoryInfo self, DirectoryInfo dest, bool overwrite = false, EnumerationOptions? options = null, Func<FileInfo, bool>? predicator = null)
    {
        // オプション指定が無ければデフォルト設定
        var enumOptions = options ?? new EnumerationOptions()
        {
            AttributesToSkip = FileAttributes.None,
            RecurseSubdirectories = true,
            MatchType = MatchType.Simple,
        };

        // 全ファイルをコピー
        foreach (var file in self.EnumerateFiles("*", enumOptions))
        {
            // コピー先ファイルパス作成
            var relPath = file.RelativePathFrom(self);
            var destFile = dest.RelativeFile(relPath);

            // デリゲートが指定されていればコピー判定
            var doCopy = predicator?.Invoke(destFile) ?? true;
            if (!doCopy) continue;

            // コピー実施
            file.CopyTo(destFile.WithDirectoryCreate().FullName, overwrite);
        }
    }

    /// <summary>ディレクトリをリネームする</summary>
    /// <param name="self">対象ディレクトリ</param>
    /// <param name="name">新しい名前。元の場所を基準とした名前。</param>
    /// <returns>リネームした対象ディレクトリ情報。元のインスタンスと同一。</returns>
    public static DirectoryInfo Rename(this DirectoryInfo self, string name)
    {
        if (self.Parent == null) throw new InvalidOperationException();
        var newPath = Path.Combine(self.Parent.FullName, name);
        self.MoveTo(newPath);
        return self;
    }

    /// <summary>ディレクトリを再帰的に削除する</summary>
    /// <remarks></remarks>
    /// <param name="self">削除対象ディレクトリ</param>
    public static void DeleteRecurse(this DirectoryInfo self)
    {
        // 存在しないならばすることはない
        if (self == null || !self.Exists) return;

        // 配下のファイル/ディレクトリに読み取り専用属性が付いていれば削除する。
        // ただ、これだけが削除を阻害する要因ではないので気休め程度ではある。
        var options = new VisitFilesOptions(Recurse: true, new(File: true, Directory: true), Sort: false, Buffered: false);
        self.DoFiles(w => w.Item.SetReadOnly(false), options);

        // 再帰的に削除する
        self.Delete(recursive: true);
    }
    #endregion
}
