using System.Text;

namespace Lestaly;

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
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        return new FileInfo(Path.Combine(self.FullName, relativePath));
    }

    /// <summary>
    /// ディレクトリからの相対パス位置に対する DirectoryInfo を取得する。
    /// </summary>
    /// <param name="self">基準となるディレクトリのDirectoryInfo</param>
    /// <param name="relativePath">基準ディレクトリからのパス。もし絶対パスの場合は基準ディレクトリは無関係にこの絶対パスが利用される。</param>
    /// <returns>対象ディレクトリパスの DirectoryInfo</returns>
    public static DirectoryInfo GetRelativeDirectory(this DirectoryInfo self, string relativePath)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        return new DirectoryInfo(Path.Combine(self.FullName, relativePath));
    }
    #endregion

    #region Path
    /// <summary>
    /// ディレクトリパスの構成セグメントを取得する。
    /// </summary>
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
    {
        // パラメータチェック
        if (self == null) throw new ArgumentNullException(nameof(self));
        if (baseDir == null) throw new ArgumentNullException(nameof(baseDir));

        return GegmentsToReletivePath(self.GetPathSegments(), baseDir, ignoreCase);
    }

    /// <summary>
    /// パスセグメントから指定のディレクトリを起点とした相対パスを取得する。
    /// </summary>
    /// <param name="segments">パスセグメントリスト</param>
    /// <param name="baseDir">基準ディレクトリのDirectoryInfo</param>
    /// <param name="ignoreCase">大文字と小文字を同一視するか否か</param>
    /// <returns>相対パス</returns>
    internal static string GegmentsToReletivePath(IList<string> segments, DirectoryInfo baseDir, bool ignoreCase)
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

}
