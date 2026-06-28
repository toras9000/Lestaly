namespace Lestaly;

/// <summary>
/// Environment関連の拡張メソッド
/// </summary>
public static class EnvironmentExtensions
{
    /// <summary>特殊フォルダ関連の拡張メソッド</summary>
    /// <param name="self">特殊フォルダ種別</param>
    extension(Environment.SpecialFolder self)
    {
        /// <summary>指定の種別の特殊ディレクトリ情報を取得する。</summary>
        /// <returns>ディレクトリ情報</returns>
        public DirectoryInfo GetInfo() => new(Environment.GetFolderPath(self));

        /// <summary>特殊フォルダを示すディレクトリ情報を取得する</summary>
        /// <param name="option">取得オプション</param>
        /// <returns>ディレクトリ情報</returns>
        public DirectoryInfo GetInfo(Environment.SpecialFolderOption option) => new(Environment.GetFolderPath(self, option));

        /// <summary>特殊フォルダからの相対パス位置に対する FileInfo を取得する。</summary>
        /// <param name="relativePath">基準ディレクトリからのパス。もし絶対パスの場合は基準ディレクトリは無関係にこの絶対パスが利用される。</param>
        /// <returns>対象ファイルパスの FileInfo。</returns>
        public FileInfo RelativeFile(string relativePath)
            => self.GetInfo().RelativeFile(relativePath);

        /// <summary>特殊フォルダからの相対パス位置に対する DirectoryInfo を取得する。</summary>
        /// <param name="relativePath">基準ディレクトリからのパス。もし絶対パスの場合は基準ディレクトリは無関係にこの絶対パスが利用される。</param>
        /// <returns>対象ディレクトリパスの DirectoryInfo。相対パスが空や空白の場合は基準ディレクトリを返却。</returns>
        public DirectoryInfo RelativeDirectory(string? relativePath)
            => self.GetInfo().RelativeDirectory(relativePath);
    }
}
