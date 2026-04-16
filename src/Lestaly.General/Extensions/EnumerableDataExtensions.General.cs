namespace Lestaly;

/// <summary>
/// IEnumerable{T} に対するデータ処理拡張メソッド
/// </summary>
public static partial class EnumerableDataExtensions
{
    /// <summary>シーケンスの文字列を各行として保存する。</summary>
    /// <param name="self">保存対象シーケンス</param>
    /// <param name="file">保存ファイル</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>書き込みを行いレシーバ自身を返すタスク</returns>
    public static ValueTask<FileInfo> WriteAllLinesAsync(this IEnumerable<string> self, FileInfo file, CancellationToken cancelToken = default)
    {
        return file.WriteAllLinesAsync(self, cancelToken);
    }
}
