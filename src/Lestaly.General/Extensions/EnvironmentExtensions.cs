namespace Lestaly;

/// <summary>
/// Environment関連の拡張メソッド
/// </summary>
public static class EnvironmentExtensions
{
    // 特殊フォルダ関連の拡張メソッド
    extension(Environment.SpecialFolder self)
    {
        /// <summary>指定の種別の特殊ディレクトリ情報を取得する。</summary>
        /// <returns>ディレクトリ情報</returns>
        public DirectoryInfo GetInfo() => new(Environment.GetFolderPath(self));

        /// <summary>特殊フォルダを示すディレクトリ情報を取得する</summary>
        /// <param name="option">取得オプション</param>
        /// <returns>ディレクトリ情報</returns>
        public DirectoryInfo GetInfo(Environment.SpecialFolderOption option) => new(Environment.GetFolderPath(self, option));
    }
}
