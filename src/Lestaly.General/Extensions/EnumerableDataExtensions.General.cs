namespace Lestaly;

/// <summary>IEnumerable{T} に対するデータ処理拡張メソッド</summary>
public static partial class EnumerableDataExtensions
{
    /// <summary>ファイル保存関係のメソッド</summary>
    /// <param name="self">対象シーケンス</param>
    extension(IEnumerable<string> self)
    {
        /// <summary>シーケンスの文字列を各行として保存する。</summary>
        /// <param name="file">保存ファイル</param>
        /// <param name="cancelToken">キャンセルトークン</param>
        /// <returns>書き込みを行いレシーバ自身を返すタスク</returns>
        public ValueTask<FileInfo> WriteAllLinesAsync(FileInfo file, CancellationToken cancelToken = default)
        {
            return file.WriteAllLinesAsync(self, cancelToken);
        }
    }
}
