﻿using System.Text;
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
        if (self == null) throw new ArgumentNullException(nameof(self));
        if (string.IsNullOrWhiteSpace(relativePath)) return default;
        return new FileInfo(Path.Combine(self.FullName, relativePath));
    }

    /// <summary>ディレクトリからの相対パス位置に対する FileInfo を取得する。</summary>
    /// <param name="self">基準となるディレクトリの DirectoryInfo</param>
    /// <param name="relativePath">基準ディレクトリからのパス。もし絶対パスの場合は基準ディレクトリは無関係にこの絶対パスが利用される。</param>
    /// <returns>対象ファイルパスの FileInfo。</returns>
    public static FileInfo RelativeFile(this DirectoryInfo self, string relativePath)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        if (string.IsNullOrWhiteSpace(relativePath)) throw new ArgumentException($"Invalid relative path.");
        return new FileInfo(Path.Combine(self.FullName, relativePath));
    }

    /// <summary>ディレクトリからの相対パス位置に対する DirectoryInfo を取得する。</summary>
    /// <param name="self">基準となるディレクトリのDirectoryInfo</param>
    /// <param name="relativePath">基準ディレクトリからのパス。もし絶対パスの場合は基準ディレクトリは無関係にこの絶対パスが利用される。</param>
    /// <returns>対象ディレクトリパスの DirectoryInfo。相対パスが空や空白の場合は null を返却。</returns>
    public static DirectoryInfo? RelativeDirectoryAt(this DirectoryInfo self, string? relativePath)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        if (string.IsNullOrWhiteSpace(relativePath)) return default;
        return new DirectoryInfo(Path.Combine(self.FullName, relativePath));
    }

    /// <summary>ディレクトリからの相対パス位置に対する DirectoryInfo を取得する。</summary>
    /// <param name="self">基準となるディレクトリのDirectoryInfo</param>
    /// <param name="relativePath">基準ディレクトリからのパス。もし絶対パスの場合は基準ディレクトリは無関係にこの絶対パスが利用される。</param>
    /// <returns>対象ディレクトリパスの DirectoryInfo。相対パスが空や空白の場合は基準ディレクトリを返却。</returns>
    public static DirectoryInfo RelativeDirectory(this DirectoryInfo self, string? relativePath)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
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
        if (self == null) throw new ArgumentNullException(nameof(self));
        self.Create();
        return self;
    }
    #endregion

    #region Path
    /// <summary>ディレクトリパスの構成セグメントを取得する。</summary>
    /// <param name="self">対象ディレクトリのDirectoryInfo</param>
    /// <returns>パス構成セグメントのリスト</returns>
    public static IList<string> GetPathSegments(this DirectoryInfo self)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));

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
        if (self == null) throw new ArgumentNullException(nameof(self));
        if (other == null) throw new ArgumentNullException(nameof(other));

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
        if (self == null) throw new ArgumentNullException(nameof(self));
        if (other == null) throw new ArgumentNullException(nameof(other));

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
    public static string RelativePathFrom(this DirectoryInfo self, DirectoryInfo baseDir, bool ignoreCase)
    {
        // パラメータチェック
        if (self == null) throw new ArgumentNullException(nameof(self));
        if (baseDir == null) throw new ArgumentNullException(nameof(baseDir));

        return SegmentsToReletivePath(self.GetPathSegments(), baseDir, ignoreCase);
    }

    /// <summary>パスセグメントから指定のディレクトリを起点とした相対パスを取得する。</summary>
    /// <param name="segments">パスセグメントリスト</param>
    /// <param name="baseDir">基準ディレクトリのDirectoryInfo</param>
    /// <param name="ignoreCase">大文字と小文字を同一視するか否か</param>
    /// <returns>相対パス</returns>
    internal static string SegmentsToReletivePath(IList<string> segments, DirectoryInfo baseDir, bool ignoreCase)
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
        var matchRule = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
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
                var lastChar = builder[builder.Length - 1];
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
    public static IEnumerable<TResult> VisitFiles<TResult>(this DirectoryInfo self, SelectFilesWalker<TResult> selector, SelectFilesOptions? options = null)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        if (selector == null) throw new ArgumentNullException(nameof(selector));

        // ディレクトリ配下を検索するローカル関数
        static IEnumerable<TResult> enumerate(SelectFilesContext<TResult> context, DirectoryInfo directory, SelectFilesWalker<TResult> selector, SelectFilesOptions options, SelectFilesHandling handling)
        {
            // ファイルのハンドリングが有効であればファイルを処理
            if (handling.File)
            {
                // ファイルの列挙シーケンスを取得。オプションによってバッファリングとソート。
                var files = options.Buffered ? directory.GetFiles() : directory.EnumerateFiles();
                if (options.Sort) files = files.OrderBy(f => f.Name);

                // ファイル列挙
                foreach (var file in files)
                {
                    // ファイル処理上の状態初期化
                    context.SetFile(file);

                    // ファイルに対する変換処理を呼び出し
                    selector(context);

                    // 結果が設定されていたら列挙する。
                    if (context.HasValue) yield return context.Value;

                    // 列挙終了が指定されているかを判定
                    if (context.Exit)
                    {
                        yield break;    // 列挙終了
                    }
                    else
                    {
                        // ファイル列挙中の中断指定はファイルの列挙を停止する。そのままディレクトリ列挙へ。
                        if (context.Break) break;
                    }
                }
            }

            // ディレクトリに対する処理が無効であればここで終える
            if (!options.Recurse && !handling.Directory) yield break;

            // サブディレクトリの列挙シーケンスを取得。オプションによってバッファリングとソート。
            var subdirs = options.Buffered ? directory.GetDirectories() : directory.EnumerateDirectories();
            if (options.Sort) subdirs = subdirs.OrderBy(d => d.Name);

            // サブディレクトリ列挙
            foreach (var subdir in subdirs)
            {
                // ディレクトリのハンドリングが有効であればディレクトリを処理
                if (handling.Directory)
                {
                    // このディレクトリ処理のための状態初期化
                    context.SetDirectory(subdir);

                    // ディレクトリに対する変換処理(というか主に継続判定)を呼び出し
                    selector(context);

                    // ディレクトリに対してでも結果が設定されていたら列挙する。
                    if (context.HasValue) yield return context.Value;

                    // 列挙終了が指定されているかを判定
                    if (context.Exit)
                    {
                        yield break;    // 列挙終了
                    }
                    else
                    {
                        // ディレクトリに対する中断指定はディレクトリのスキップ。次のディレクトリ列挙へ。
                        if (context.Break) continue;
                    }
                }

                // 再帰検索が有効であればサブディレクトリ配下を検索し、変換結果を列挙
                if (options.Recurse)
                {
                    foreach (var result in enumerate(context, subdir, selector, options, handling))
                    {
                        yield return result;
                    }
                }

                // ディレクトリ内で列挙終了が指定されていたらこの階層からも抜ける
                if (context.Exit) yield break;
            }
        }

        // 検索オプションが指定されていなければデフォルト設定とする。
        options ??= new();

        // 列挙ハンドリングを決定
        var handling = options.Handling ?? SelectFilesHandling.Default;

        // 列挙
        var context = new SelectFilesContext<TResult>(self);
        return enumerate(context, self, selector, options, handling);
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
    /// <param name="excludes">列挙対象から除外するパターンのコレクション</param>
    /// <param name="includes">列挙対象に含めるパターンのコレクション。除外されなかったファイルに適用する。nullの場合は全てのファイルが列挙対象。</param>
    /// <param name="options">検索オプション</param>
    /// <returns>変換結果のシーケンス</returns>
    public static IEnumerable<TResult> VisitFiles<TResult>(this DirectoryInfo self, SelectFilesWalker<TResult> selector, IReadOnlyCollection<Regex> excludes, IReadOnlyCollection<Regex>? includes = null, SelectFilesOptions? options = null)
    {
        if (selector == null) throw new ArgumentNullException(nameof(selector));
        if (excludes == null) throw new ArgumentNullException(nameof(excludes));
        return self.VisitFiles<TResult>(options: options, selector: c =>
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
    /// <typeparam name="TResult">ファイル/ディレクトリに対する変換結果の型</typeparam>
    /// <param name="self">検索の起点ディレクトリ</param>
    /// <param name="selector">ファイル/ディレクトリに対する変換処理</param>
    /// <param name="options">検索オプション</param>
    /// <returns>変換結果のシーケンス</returns>
    public static IEnumerable<TResult> SelectFiles<TResult>(this DirectoryInfo self, Func<IFileWalker, TResult> selector, SelectFilesOptions? options = null)
    {
        if (selector == null) throw new ArgumentNullException(nameof(selector));
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
    /// <param name="filter">検索対象をフィルタする処理</param>
    /// <param name="selector">ファイル/ディレクトリに対する変換処理</param>
    /// <param name="options">検索オプション</param>
    /// <returns>変換結果のシーケンス</returns>
    public static IEnumerable<TResult> SelectFiles<TResult>(this DirectoryInfo self, Func<IFileWalker, bool> filter, Func<IFileWalker, TResult> selector, SelectFilesOptions? options = null)
    {
        if (selector == null) throw new ArgumentNullException(nameof(selector));
        return self.VisitFiles<TResult>(c => { if (filter(c)) { c.SetResult(selector(c)); } else if (c.File == null) { c.Break = true; } }, options);
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
    /// <param name="excludes">列挙対象から除外するパターンのコレクション</param>
    /// <param name="includes">列挙対象に含めるパターンのコレクション。除外されなかったファイルに適用する。nullの場合は全てのファイルが列挙対象。</param>
    /// <param name="options">検索オプション</param>
    /// <returns>変換結果のシーケンス</returns>
    public static IEnumerable<TResult> SelectFiles<TResult>(this DirectoryInfo self, Func<IFileWalker, TResult> selector, IReadOnlyCollection<Regex> excludes, IReadOnlyCollection<Regex>? includes = null, SelectFilesOptions? options = null)
    {
        if (selector == null) throw new ArgumentNullException(nameof(selector));
        return self.VisitFiles<TResult>(c => c.SetResult(selector(c)), excludes, includes, options);
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
    public static IAsyncEnumerable<TResult> VisitFilesAsync<TResult>(this DirectoryInfo self, AsyncSelectFilesWalker<TResult> selector, SelectFilesOptions? options = null)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        if (selector == null) throw new ArgumentNullException(nameof(selector));

        // ディレクトリ配下を検索するローカル関数
        static async IAsyncEnumerable<TResult> enumerateAsync(SelectFilesContext<TResult> context, DirectoryInfo directory, AsyncSelectFilesWalker<TResult> selector, SelectFilesOptions options, SelectFilesHandling handling)
        {
            // ファイルの列挙シーケンスを取得。オプションによってバッファリングとソート。
            var files = options.Buffered ? directory.GetFiles() : directory.EnumerateFiles();
            if (options.Sort) files = files.OrderBy(f => f.Name);

            // ファイルのハンドリングが有効であればファイルを処理
            if (handling.File)
            {
                // ファイル列挙
                foreach (var file in files)
                {
                    // ファイル処理上の状態初期化
                    context.SetFile(file);

                    // ファイルに対する変換処理を呼び出し
                    await selector(context).ConfigureAwait(false);

                    // 結果が設定されていたら列挙する。
                    if (context.HasValue) yield return context.Value;

                    // 列挙終了が指定されているかを判定
                    if (context.Exit)
                    {
                        yield break;    // 列挙終了
                    }
                    else
                    {
                        // ファイル列挙中の中断指定はファイルの列挙を停止する。そのままディレクトリ列挙へ。
                        if (context.Break) break;
                    }
                }
            }

            // ディレクトリに対する処理が無効であればここで終える
            if (!options.Recurse && !handling.Directory) yield break;

            // サブディレクトリの列挙シーケンスを取得。オプションによってバッファリングとソート。
            var subdirs = options.Buffered ? directory.GetDirectories() : directory.EnumerateDirectories();
            if (options.Sort) subdirs = subdirs.OrderBy(d => d.Name);

            // サブディレクトリ列挙
            foreach (var subdir in subdirs)
            {
                // ディレクトリに対するハンドリングが有効な場合は処理デリゲートを呼び出す
                if (handling.Directory)
                {
                    // このディレクトリ処理のための状態初期化
                    context.SetDirectory(subdir);

                    // ディレクトリに対する変換処理(というか主に継続判定)を呼び出し
                    await selector(context).ConfigureAwait(false);

                    // ディレクトリに対してでも結果が設定されていたら列挙する。
                    if (context.HasValue) yield return context.Value;

                    // 列挙終了が指定されているかを判定
                    if (context.Exit)
                    {
                        yield break;    // 列挙終了
                    }
                    else
                    {
                        // ディレクトリに対する中断指定はディレクトリのスキップ。次のディレクトリ列挙へ。
                        if (context.Break) continue;
                    }
                }


                // 再帰検索が有効であればサブディレクトリ配下を検索し、変換結果を列挙
                if (options.Recurse)
                {
                    // サブディレクトリ配下を再帰検索し、変換結果を列挙
                    await foreach (var result in enumerateAsync(context, subdir, selector, options, handling))
                    {
                        yield return result;
                    }
                }

                // ディレクトリ内で列挙終了が指定されていたらこの階層からも抜ける
                if (context.Exit) yield break;
            }
        }

        // 検索オプションが指定されていなければデフォルト設定とする。
        options ??= new();

        // 列挙ハンドリングを決定
        var handling = options.Handling ?? SelectFilesHandling.Default;

        // 列挙
        var context = new SelectFilesContext<TResult>(self);
        return enumerateAsync(context, self, selector, options, handling);
    }

    /// <summary>ディレクトリ配下のファイル/ディレクトリを検索して変換処理を行う</summary>
    /// <typeparam name="TResult">ファイル/ディレクトリに対する変換結果の型</typeparam>
    /// <param name="self">検索の起点ディレクトリ</param>
    /// <param name="selector">ファイル/ディレクトリに対する変換処理</param>
    /// <param name="excludes">列挙対象から除外するパターンのコレクション</param>
    /// <param name="includes">列挙対象に含めるパターンのコレクション。除外されなかったファイルに適用する。nullの場合は全てのファイルが列挙対象。</param>
    /// <param name="options">検索オプション</param>
    /// <returns>変換結果のシーケンス</returns>
    public static IAsyncEnumerable<TResult> VisitFilesAsync<TResult>(this DirectoryInfo self, AsyncSelectFilesWalker<TResult> selector, IReadOnlyCollection<Regex> excludes, IReadOnlyCollection<Regex>? includes = null, SelectFilesOptions? options = null)
    {
        if (selector == null) throw new ArgumentNullException(nameof(selector));
        if (excludes == null) throw new ArgumentNullException(nameof(excludes));
        return self.VisitFilesAsync<TResult>(options: options, selector: async c =>
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
    public static IAsyncEnumerable<TResult> SelectFilesAsync<TResult>(this DirectoryInfo self, Func<IFileWalker, ValueTask<TResult>> selector, SelectFilesOptions? options = null)
    {
        if (selector == null) throw new ArgumentNullException(nameof(selector));
        return self.VisitFilesAsync<TResult>(async c => c.SetResult(await selector(c).ConfigureAwait(false)), options);
    }

    /// <summary>ディレクトリ配下のファイル/ディレクトリを検索して変換処理を行う</summary>
    /// <remarks>
    /// ディレクトリ内を列挙する際は最初にファイルを列挙し、オプションで指定されていれば次にサブディレクトリを列挙する。
    /// このメソッドではサブディレクトリ配下の検索に再帰呼び出しを利用する。
    /// ディレクトリ構成によってはスタックを大量に消費する可能性があることに注意。
    /// </remarks>
    /// <typeparam name="TResult">ファイル/ディレクトリに対する変換結果の型</typeparam>
    /// <param name="self">検索の起点ディレクトリ</param>
    /// <param name="filter">検索対象をフィルタする処理</param>
    /// <param name="selector">ファイル/ディレクトリに対する変換処理</param>
    /// <param name="options">検索オプション</param>
    /// <returns>変換結果のシーケンス</returns>
    public static IAsyncEnumerable<TResult> SelectFilesAsync<TResult>(this DirectoryInfo self, Func<IFileWalker, bool> filter, Func<IFileWalker, ValueTask<TResult>> selector, SelectFilesOptions? options = null)
    {
        if (selector == null) throw new ArgumentNullException(nameof(selector));
        return self.VisitFilesAsync<TResult>(async c => { if (filter(c)) { c.SetResult(await selector(c).ConfigureAwait(false)); } else if (c.File == null) { c.Break = true; } }, options);
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
    /// <param name="excludes">列挙対象から除外するパターンのコレクション</param>
    /// <param name="includes">列挙対象に含めるパターンのコレクション。除外されなかったファイルに適用する。nullの場合は全てのファイルが列挙対象。</param>
    /// <param name="options">検索オプション</param>
    /// <returns>変換結果のシーケンス</returns>
    public static IAsyncEnumerable<TResult> SelectFilesAsync<TResult>(this DirectoryInfo self, Func<IFileWalker, ValueTask<TResult>> selector, IReadOnlyCollection<Regex> excludes, IReadOnlyCollection<Regex>? includes = null, SelectFilesOptions? options = null)
    {
        if (selector == null) throw new ArgumentNullException(nameof(selector));
        return self.VisitFilesAsync<TResult>(async c => c.SetResult(await selector(c).ConfigureAwait(false)), excludes, includes, options);
    }

    /// <summary>ディレクトリ配下のファイル/ディレクトリを検索して処理を行う</summary>
    /// <param name="self">検索の起点ディレクトリ</param>
    /// <param name="processor">ファイル/ディレクトリに対する処理</param>
    /// <param name="options">検索オプション</param>
    public static void DoFiles(this DirectoryInfo self, DoFilesWalker processor, SelectFilesOptions? options = null)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        if (processor == null) throw new ArgumentNullException(nameof(processor));
        foreach (var _ in self.VisitFiles<int>(c => processor(c), options)) ;
    }

    /// <summary>ディレクトリ配下のファイル/ディレクトリを検索して処理を行う</summary>
    /// <param name="self">検索の起点ディレクトリ</param>
    /// <param name="processor">ファイル/ディレクトリに対する処理</param>
    /// <param name="excludes">列挙対象から除外するパターンのコレクション</param>
    /// <param name="includes">列挙対象に含めるパターンのコレクション。除外されなかったファイルに適用する。nullの場合は全てのファイルが列挙対象。</param>
    /// <param name="options">検索オプション</param>
    public static void DoFiles(this DirectoryInfo self, DoFilesWalker processor, IReadOnlyCollection<Regex> excludes, IReadOnlyCollection<Regex>? includes = null, SelectFilesOptions? options = null)
    {
        if (processor == null) throw new ArgumentNullException(nameof(processor));
        if (excludes == null) throw new ArgumentNullException(nameof(excludes));
        self.DoFiles(options: options, processor: c =>
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
                processor(c);
            }
        });
    }

    /// <summary>ディレクトリ配下のファイル/ディレクトリを検索して処理を行う</summary>
    /// <param name="self">検索の起点ディレクトリ</param>
    /// <param name="processor">ファイル/ディレクトリに対する処理</param>
    /// <param name="options">検索オプション</param>
    /// <returns>検索処理タスク</returns>
    public static async Task DoFilesAsync(this DirectoryInfo self, AsyncDoFilesWalker processor, SelectFilesOptions? options = null)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        if (processor == null) throw new ArgumentNullException(nameof(processor));
        await foreach (var _ in self.VisitFilesAsync<int>(c => processor(c), options).ConfigureAwait(false)) ;
    }

    /// <summary>ディレクトリ配下のファイル/ディレクトリを検索して処理を行う</summary>
    /// <param name="self">検索の起点ディレクトリ</param>
    /// <param name="processor">ファイル/ディレクトリに対する処理</param>
    /// <param name="excludes">列挙対象から除外するパターンのコレクション</param>
    /// <param name="includes">列挙対象に含めるパターンのコレクション。除外されなかったファイルに適用する。nullの場合は全てのファイルが列挙対象。</param>
    /// <param name="options">検索オプション</param>
    /// <returns>検索処理タスク</returns>
    public static Task DoFilesAsync(this DirectoryInfo self, AsyncDoFilesWalker processor, IReadOnlyCollection<Regex> excludes, IReadOnlyCollection<Regex>? includes = null, SelectFilesOptions? options = null)
    {
        if (processor == null) throw new ArgumentNullException(nameof(processor));
        return self.DoFilesAsync(options: options, processor: async c =>
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
                await processor(c).ConfigureAwait(false);
            }
        });
    }

    /// <summary>ファイル列挙変換コンテキスト</summary>
    /// <typeparam name="TResult">変換結果の型</typeparam>
    private class SelectFilesContext<TResult> : IFileConverter<TResult>
    {
        // 構築
        #region コンストラクタ
        /// <summary>起点ディレクトリを指定するコンストラクタ</summary>
        /// <param name="dir">起点ディレクトリ</param>
        public SelectFilesContext(DirectoryInfo dir)
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
    /// <summary>ディレクトリを再帰的に削除する</summary>
    /// <remarks></remarks>
    /// <param name="self">削除対象ディレクトリ</param>
    public static void DeleteRecurse(this DirectoryInfo self)
    {
        // 存在しないならばすることはない
        if (self == null || !self.Exists) return;

        // 配下のファイル/ディレクトリに読み取り専用属性が付いていれば削除する。
        // ただ、これだけが削除を阻害する要因ではないので気休め程度ではある。
        var options = new SelectFilesOptions(Recurse: true, new(File: true, Directory: true), Sort: false, Buffered: false);
        self.DoFiles(w => w.Item.SetReadOnly(false), options);

        // 再帰的に削除する
        self.Delete(recursive: true);
    }
    #endregion
}
