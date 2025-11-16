namespace Lestaly;

/// <summary>
/// 特殊ディレクトリに関する補助メソッド
/// </summary>
public static class SpecialFolder
{
    /// <summary>ユーザプロファイルディレクトリ情報を取得する。</summary>
    /// <returns>ディレクトリ情報</returns>
    public static DirectoryInfo UserProfile() => Environment.SpecialFolder.UserProfile.GetInfo();

    /// <summary>現在のユーザのアプリケーションデータディレクトリ情報を取得する。</summary>
    /// <returns>ディレクトリ情報</returns>
    public static DirectoryInfo ApplicationData() => Environment.SpecialFolder.ApplicationData.GetInfo();

    /// <summary>テンポラリディレクトリ情報を取得する。</summary>
    /// <returns>ディレクトリ情報</returns>
    public static DirectoryInfo Temporary() => new(Path.GetTempPath());
}

