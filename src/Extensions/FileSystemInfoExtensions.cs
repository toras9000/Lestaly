using System.Text;

namespace Lestaly;

/// <summary>
/// FileSystemInfo に対する拡張メソッド
/// </summary>
public static class FileSystemInfoExtensions
{
    #region FileSystem
    /// <summary>ファイル/ディレクトリがReadOnlyであるかを取得する。</summary>
    /// <param name="self">対象ファイル/ディレクトリ情報</param>
    /// <returns>ReadOnlyであるか否か</returns>
    public static bool GetReadOnly(this FileSystemInfo self)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        return (self.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly;
    }

    /// <summary>ファイル/ディレクトリのReadOnly属性を設定する。</summary>
    /// <param name="self">対象ファイル/ディレクトリ情報</param>
    /// <param name="readOnly">設定するReadOnly状態</param>
    /// <returns>対象ファイル/ディレクトリ情報</returns>
    public static T SetReadOnly<T>(this T self, bool readOnly) where T : FileSystemInfo
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
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
    #endregion

}
