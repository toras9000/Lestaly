namespace Lestaly;

/// <summary>
/// 特殊ディレクトリに関する補助メソッド
/// </summary>
public static class SpecialFolder
{
    /// <summary>指定の種別の特殊ディレクトリ情報を取得する。</summary>
    /// <param name="kind">特殊フォルダ種別</param>
    /// <returns>ディレクトリ情報</returns>
    public static DirectoryInfo Get(Environment.SpecialFolder kind) => new(Environment.GetFolderPath(kind));

    /// <summary>ユーザプロファイルディレクトリ情報を取得する。</summary>
    /// <returns>ディレクトリ情報</returns>
    public static DirectoryInfo UserProfile() => Get(Environment.SpecialFolder.UserProfile);

    /// <summary>現在のユーザのアプリケーションデータディレクトリ情報を取得する。</summary>
    /// <returns>ディレクトリ情報</returns>
    public static DirectoryInfo ApplicationData() => Get(Environment.SpecialFolder.ApplicationData);

    /// <summary>テンポラリディレクトリ情報を取得する。</summary>
    /// <returns>ディレクトリ情報</returns>
    public static DirectoryInfo Temporary() => new(Path.GetTempPath());
}

