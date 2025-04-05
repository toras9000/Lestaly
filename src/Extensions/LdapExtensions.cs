using System.Buffers;
using System.DirectoryServices.Protocols;
using System.Security.Cryptography;
using System.Text;

namespace Lestaly;

/// <summary>LDAP関連の拡張メソッド</summary>
public static class LdapExtensions
{
    /// <summary>非同期でディレクトリリクエストを送信する</summary>
    /// <param name="self">LDAP接続</param>
    /// <param name="request">リクエスト</param>
    /// <param name="partialMode">部分取得モード</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>レスポンス</returns>
    public static Task<DirectoryResponse> SendRequestAsync(this LdapConnection self, DirectoryRequest request, PartialResultProcessing partialMode = PartialResultProcessing.NoPartialResultSupport, CancellationToken cancelToken = default)
    {
        var asyncToken = self.BeginSendRequest(request, partialMode, null, null);

        return Task.Run(() =>
        {
            using var canceller = cancelToken.Register(() => self.Abort(asyncToken));
            try
            {
                return self.EndSendRequest(asyncToken); ;
            }
            catch (ArgumentException)
            {
                cancelToken.ThrowIfCancellationRequested();
                throw;
            }
        });
    }

    /// <summary>識別名(DN)のエントリを検索取得する</summary>
    /// <param name="self">LDAP接続</param>
    /// <param name="dn">識別名(DN)</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>取得されたエントリ</returns>
    public static async Task<SearchResultEntry?> GetEntryAsync(this LdapConnection self, string dn, CancellationToken cancelToken = default)
    {
        // Create a search request.
        var searchReq = new SearchRequest();
        searchReq.DistinguishedName = dn;
        searchReq.Scope = SearchScope.Base;

        // Request a search.
        var searchRsp = await self.SendRequestAsync(searchReq, cancelToken: cancelToken);
        if (searchRsp.ResultCode != 0) throw new PavedMessageException($"failed to search: {searchRsp.ErrorMessage}");
        var searchResult = searchRsp as SearchResponse ?? throw new PavedMessageException("unexpected result");

        return searchResult.Entries[0];
    }

    /// <summary>識別名(DN)のエントリを検索取得する</summary>
    /// <param name="self">LDAP接続</param>
    /// <param name="dn">識別名(DN)</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>取得されたエントリ、またはnull</returns>
    public static async Task<SearchResultEntry?> GetEntryOrDefaultAsync(this LdapConnection self, string dn, CancellationToken cancelToken = default)
    {
        try
        {
            // Create a search request.
            var searchReq = new SearchRequest();
            searchReq.DistinguishedName = dn;
            searchReq.Scope = SearchScope.Base;

            // Request a search.
            var searchRsp = await self.SendRequestAsync(searchReq, cancelToken: cancelToken);
            if (searchRsp.ResultCode != 0) return default;
            if (searchRsp is not SearchResponse searchResult) return default;
            if (searchResult.Entries.Count <= 0) return default;
            return searchResult.Entries[0];
        }
        catch { return default; }
    }

    /// <summary>エントリを作成する</summary>
    /// <param name="self">LDAP接続</param>
    /// <param name="dn">識別名(DN)</param>
    /// <param name="attributes">エントリの属性</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>レスポンス</returns>
    public static async Task<DirectoryResponse> CreateEntryAsync(this LdapConnection self, string dn, DirectoryAttribute[] attributes, CancellationToken cancelToken = default)
    {
        var createEntryReq = new AddRequest();
        createEntryReq.DistinguishedName = dn;
        foreach (var attr in attributes)
        {
            createEntryReq.Attributes.Add(attr);
        }

        var createEntryRsp = await self.SendRequestAsync(createEntryReq, cancelToken: cancelToken);
        if (createEntryRsp.ResultCode != 0) throw new PavedMessageException($"failed to search: {createEntryRsp.ErrorMessage}");

        return createEntryRsp;
    }

    /// <summary>エントリに属性を追加する</summary>
    /// <param name="self">LDAP接続</param>
    /// <param name="dn">識別名(DN)</param>
    /// <param name="name">属性名</param>
    /// <param name="values">属性値</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>レスポンス</returns>
    public static async Task<DirectoryResponse> AddAttributeAsync(this LdapConnection self, string dn, string name, string[] values, CancellationToken cancelToken = default)
    {
        var attrModify = new DirectoryAttributeModification();
        attrModify.Operation = DirectoryAttributeOperation.Add;
        attrModify.Name = name;
        attrModify.AddRange(values);

        var addAttrReq = new ModifyRequest();
        addAttrReq.DistinguishedName = dn;
        addAttrReq.Modifications.Add(attrModify);

        var addAttrRsp = await self.SendRequestAsync(addAttrReq, cancelToken: cancelToken);
        if (addAttrRsp.ResultCode != 0) throw new PavedMessageException($"failed to search: {addAttrRsp.ErrorMessage}");

        return addAttrRsp;
    }

    /// <summary>エントリの属性を置き換える</summary>
    /// <param name="self">LDAP接続</param>
    /// <param name="dn">識別名(DN)</param>
    /// <param name="name">属性名</param>
    /// <param name="values">属性値</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>レスポンス</returns>
    public static async Task<DirectoryResponse> ReplaceAttributeAsync(this LdapConnection self, string dn, string name, string[] values, CancellationToken cancelToken = default)
    {
        var attrModify = new DirectoryAttributeModification();
        attrModify.Operation = DirectoryAttributeOperation.Replace;
        attrModify.Name = name;
        attrModify.AddRange(values);

        var addAttrReq = new ModifyRequest();
        addAttrReq.DistinguishedName = dn;
        addAttrReq.Modifications.Add(attrModify);

        var addAttrRsp = await self.SendRequestAsync(addAttrReq, cancelToken: cancelToken);
        if (addAttrRsp.ResultCode != 0) throw new PavedMessageException($"failed to search: {addAttrRsp.ErrorMessage}");

        return addAttrRsp;
    }

    /// <summary>検索結果エントリから指定の属性の値を1つ取得する</summary>
    /// <param name="self">検索結果エントリ</param>
    /// <param name="name">属性名</param>
    /// <returns>属性値の1つ、もしくはnull</returns>
    public static object? GetAttributeFirstValue(this SearchResultEntry self, string name)
    {
        var attr = self.Attributes[name];
        if (attr == null) return null;
        if (attr.Count <= 0) return null;

        return attr[0];
    }

    /// <summary>検索結果エントリから指定の属性の値を列挙する</summary>
    /// <param name="self">検索結果エントリ</param>
    /// <param name="name">属性名</param>
    /// <returns>属性値のシーケンス</returns>
    public static IEnumerable<object> EnumerateAttributeValues(this SearchResultEntry self, string name)
    {
        var attr = self.Attributes[name];
        if (attr == null) yield break;

        for (var i = 0; i < attr.Count; i++)
        {
            yield return attr[i];
        }
    }

    /// <summary>パスワードハッシュ化文字列を作成する</summary>
    /// <param name="password">パスワード文字列</param>
    /// <param name="salt">salt値。</param>
    /// <returns>ハッシュ化文字列</returns>
    public static string MakePasswordHashSSHA256(string password, ReadOnlySpan<byte> salt = default)
    {
        // salt を決定。指定されたものがあればそれを、無ければ適当なランダム値を。
        var randSalt = (stackalloc byte[8]);
        if (salt.IsEmpty) Random.Shared.NextBytes(randSalt);
        var useSalt = salt.IsEmpty ? randSalt : salt;
        // パスワードをUTF8エンコード
        var buffer = new ArrayBufferWriter<byte>();
        Encoding.UTF8.GetBytes(password, buffer);
        // salt を追加
        buffer.Write(salt);
        // ハッシュ算出
        var hashed = SHA256.HashData(buffer.WrittenSpan);
        // Base64文字列作成
        buffer.ResetWrittenCount();
        buffer.Write(hashed);
        buffer.Write(salt);
        var encoded = Convert.ToBase64String(buffer.WrittenSpan);
        // ハッシュ化文字列作成
        var value = $"{{SSHA256}}{encoded}";

        return value;
    }
}
