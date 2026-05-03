namespace Lestaly;

/// <summary>HttpClient に対する拡張メソッド</summary>
public static class HttpClientExtensions
{
    /// <summary>HttpClient に対する拡張メソッド</summary>
    /// <param name="self">クライアント</param>
    extension(HttpClient self)
    {
        /// <summary>GET要求の応答をファイルに保存する。</summary>
        /// <param name="resource">要求URI</param>
        /// <param name="path">保存ファイルパス</param>
        /// <param name="cancelToken">キャンセルトークン</param>
        /// <returns>保存先ファイル情報</returns>
        public Task<FileInfo> GetFileAsync(Uri resource, string path, CancellationToken cancelToken = default)
        => self.GetFileAsync(resource, new FileInfo(path), cancelToken);

        /// <summary>GET要求の応答をファイルに保存する。</summary>
        /// <param name="resource">要求URI</param>
        /// <param name="directory">保存先ディレクトリ</param>
        /// <param name="name">保存ファイル名</param>
        /// <param name="cancelToken">キャンセルトークン</param>
        /// <returns>保存先ファイル情報</returns>
        public Task<FileInfo> GetFileAsync(Uri resource, DirectoryInfo directory, string? name = default, CancellationToken cancelToken = default)
        {
            var fileName = name ?? resource.Segments.Last();
            return self.GetFileAsync(resource, directory.RelativeFile(fileName), cancelToken);
        }

        /// <summary>GET要求の応答をファイルに保存する。</summary>
        /// <param name="resource">要求URI</param>
        /// <param name="file">保存先ファイル</param>
        /// <param name="cancelToken">キャンセルトークン</param>
        /// <returns>保存先ファイル情報</returns>
        public async Task<FileInfo> GetFileAsync(Uri resource, FileInfo file, CancellationToken cancelToken = default)
        {
            // HTTP要求
            using var stream = await self.GetStreamAsync(resource, cancelToken).ConfigureAwait(false);

            // 保存先ファイル作成
            using var writer = file.WithDirectoryCreate().CreateWrite();

            // ダウンロード内容をファイルに書き込み
            await stream.CopyToAsync(writer, cancelToken).ConfigureAwait(false);

            return file;
        }

        /// <summary>GET要求の応答が正常HTTPステータスの場合にボディテキストを取得する。</summary>
        /// <param name="resource">要求URI</param>
        /// <param name="cancelToken">キャンセルトークン</param>
        /// <returns>正常ステータスの場合は応答ボディ。アクセス不可/異常ステータスの場合はnull</returns>
        public async Task<string?> TryGetAsync(Uri resource, CancellationToken cancelToken = default)
        {
            var body = default(string);
            try
            {
                using var response = await self.GetAsync(resource, cancelToken).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    body = await response.Content.ReadAsStringAsync(cancelToken).ConfigureAwait(false);
                }
            }
            catch
            {
            }

            return body;
        }

        /// <summary>GET要求の応答が正常HTTPステータスの場合にボディテキストを取得する。</summary>
        /// <param name="resource">要求URI</param>
        /// <param name="cancelToken">キャンセルトークン</param>
        /// <returns>HTTP応答ステータスコード。アクセス不可/異常ステータスの場合はnull</returns>
        public async Task<int?> GetStatusAsync(Uri resource, CancellationToken cancelToken = default)
        {
            var status = default(int);
            try
            {
                using var response = await self.GetAsync(resource, cancelToken).ConfigureAwait(false);
                status = (int)response.StatusCode;
            }
            catch
            {
            }

            return status;
        }

        /// <summary>GET要求の応答が正常HTTPステータスの場合にボディテキストを取得する。</summary>
        /// <param name="resource">要求URI</param>
        /// <param name="cancelToken">キャンセルトークン</param>
        /// <returns>HTTP応答ステータスコード。アクセス不可/異常ステータスの場合はnull</returns>
        public async Task<bool> IsSuccessStatusAsync(Uri resource, CancellationToken cancelToken = default)
        {
            var success = false;
            try
            {
                using var response = await self.GetAsync(resource, cancelToken).ConfigureAwait(false);
                success = response.IsSuccessStatusCode;
            }
            catch
            {
            }

            return success;
        }
    }
}
