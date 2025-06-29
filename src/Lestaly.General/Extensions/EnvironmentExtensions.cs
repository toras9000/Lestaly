namespace Lestaly;

/// <summary>
/// Environment関連の拡張メソッド
/// </summary>
public static class EnvironmentExtensions
{
    /// <summary>特殊フォルダを示すディレクトリ情報を取得する</summary>
    /// <param name="self">特殊フォルダ種別</param>
    /// <returns>ディレクトリ情報</returns>
    public static DirectoryInfo GetInfo(this Environment.SpecialFolder self)
    {
        return new DirectoryInfo(Environment.GetFolderPath(self));
    }

    /// <summary>特殊フォルダを示すディレクトリ情報を取得する</summary>
    /// <param name="self">特殊フォルダ種別</param>
    /// <param name="option">取得オプション</param>
    /// <returns>ディレクトリ情報</returns>
    public static DirectoryInfo GetInfo(this Environment.SpecialFolder self, Environment.SpecialFolderOption option)
    {
        return new DirectoryInfo(Environment.GetFolderPath(self, option));
    }
}
