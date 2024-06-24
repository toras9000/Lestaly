using System.Collections.ObjectModel;

namespace Lestaly;

/// <summary>
/// HttpClient に対する拡張メソッド
/// </summary>
public static class HttpClientExtensions
{
    /// <summary>GET要求の応答をファイルに保存する。</summary>
    /// <param name="self">クライアント</param>
    /// <param name="resource">要求URI</param>
    /// <param name="path">保存ファイルパス</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>保存先ファイル情報</returns>
    public static Task<FileInfo> GetFileAsync(this HttpClient self, Uri resource, string path, CancellationToken cancelToken = default)
        => self.GetFileAsync(resource, new FileInfo(path), cancelToken);

    /// <summary>GET要求の応答をファイルに保存する。</summary>
    /// <param name="self">クライアント</param>
    /// <param name="resource">要求URI</param>
    /// <param name="directory">保存先ディレクトリ</param>
    /// <param name="name">保存ファイル名</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>保存先ファイル情報</returns>
    public static Task<FileInfo> GetFileAsync(this HttpClient self, Uri resource, DirectoryInfo directory, string? name = default, CancellationToken cancelToken = default)
    {
        var fileName = name ?? resource.Segments.Last();
        return self.GetFileAsync(resource, directory.RelativeFile(fileName), cancelToken);
    }

    /// <summary>GET要求の応答をファイルに保存する。</summary>
    /// <param name="self">クライアント</param>
    /// <param name="resource">要求URI</param>
    /// <param name="file">保存先ファイル</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>保存先ファイル情報</returns>
    public static async Task<FileInfo> GetFileAsync(this HttpClient self, Uri resource, FileInfo file, CancellationToken cancelToken = default)
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
    /// <param name="self">クライアント</param>
    /// <param name="resource">要求URI</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>正常ステータスの場合は応答ボディ。アクセス不可/異常ステータスの場合はnull</returns>
    public static async Task<string?> TryGetAsync(this HttpClient self, Uri resource, CancellationToken cancelToken = default)
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
    /// <param name="self">クライアント</param>
    /// <param name="resource">要求URI</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>HTTP応答ステータスコード。アクセス不可/異常ステータスの場合はnull</returns>
    public static async Task<int?> GetStatusAsync(this HttpClient self, Uri resource, CancellationToken cancelToken = default)
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
    /// <param name="self">クライアント</param>
    /// <param name="resource">要求URI</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>HTTP応答ステータスコード。アクセス不可/異常ステータスの場合はnull</returns>
    public static async Task<bool> IsSuccessStatusAsync(this HttpClient self, Uri resource, CancellationToken cancelToken = default)
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
