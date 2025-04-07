using System.Buffers;
using System.DirectoryServices.Protocols;
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

    /// <summary>エントリを検索する</summary>
    /// <param name="self">LDAP接続</param>
    /// <param name="request">リクエスト</param>
    /// <param name="partialMode">部分取得モード</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>検索レスポンス</returns>
    public static async Task<SearchResponse> SearchAsync(this LdapConnection self, SearchRequest request, PartialResultProcessing partialMode = PartialResultProcessing.NoPartialResultSupport, CancellationToken cancelToken = default)
    {
        var response = await self.SendRequestAsync(request, partialMode, cancelToken);
        if (response.ResultCode != 0) throw new PavedMessageException($"failed to search: {response.ErrorMessage}");
        var searchResult = response as SearchResponse ?? throw new PavedMessageException("unexpected result");
        return searchResult;
    }

    /// <summary>エントリを検索する</summary>
    /// <param name="self">LDAP接続</param>
    /// <param name="baseDn">検索ベースDN</param>
    /// <param name="scope">検索スコープ</param>
    /// <param name="filter">フィルタ</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>検索レスポンス</returns>
    public static Task<SearchResponse> SearchAsync(this LdapConnection self, string baseDn, SearchScope scope, object? filter = null, CancellationToken cancelToken = default)
    {
        var searchReq = new SearchRequest();
        searchReq.DistinguishedName = baseDn;
        searchReq.Scope = scope;
        if (searchReq.Filter != null) searchReq.Filter = filter;

        return self.SearchAsync(searchReq, PartialResultProcessing.NoPartialResultSupport, cancelToken);
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

    /// <summary>バイト列に対するハッシュ算出メソッド</summary>
    /// <param name="span">バイト列</param>
    /// <returns>ハッシュ値</returns>
    public delegate byte[] HashGenerator(ReadOnlySpan<byte> span);

    /// <summary>パスワードハッシュ文字列の生成</summary>
    public static class MakePasswordHash
    {
        /// <summary>ソルトを利用したパスワードハッシュ化文字列を作成する</summary>
        /// <param name="marker">ハッシュアルゴリズムマーカ文字列</param>
        /// <param name="hasher">ハッシュ算出デリゲート</param>
        /// <param name="password">パスワード文字列</param>
        /// <param name="salt">salt値。</param>
        /// <returns>ハッシュ化文字列</returns>
        public static string Salted(string marker, HashGenerator hasher, string password, ReadOnlySpan<byte> salt = default)
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
            var hashed = hasher(buffer.WrittenSpan);
            // Base64文字列作成
            buffer.ResetWrittenCount();
            buffer.Write(hashed);
            buffer.Write(salt);
            var encoded = Convert.ToBase64String(buffer.WrittenSpan);
            // ハッシュ化文字列作成
            var value = $"{{{marker}}}{encoded}";

            return value;
        }

        /// <summary>パスワードハッシュ化文字列を作成する</summary>
        /// <param name="marker">ハッシュアルゴリズムマーカ文字列</param>
        /// <param name="hasher">ハッシュ算出デリゲート</param>
        /// <param name="password">パスワード文字列</param>
        /// <returns>ハッシュ化文字列</returns>
        public static string Dry(string marker, HashGenerator hasher, string password)
        {
            // パスワードをUTF8エンコード
            var buffer = new ArrayBufferWriter<byte>();
            Encoding.UTF8.GetBytes(password, buffer);
            // ハッシュ算出
            var hashed = hasher(buffer.WrittenSpan);
            var encoded = Convert.ToBase64String(hashed);
            // ハッシュ化文字列作成
            var value = $"{{{marker}}}{encoded}";

            return value;
        }

        /// <summary>SSHA パスワードハッシュ化文字列を作成する</summary>
        /// <param name="password">パスワード文字列</param>
        /// <param name="salt">salt値。</param>
        /// <returns>ハッシュ化文字列</returns>
        public static string SSHA(string password, ReadOnlySpan<byte> salt = default)
            => Salted("SSHA", System.Security.Cryptography.SHA1.HashData, password, salt);

        /// <summary>SSHA256 パスワードハッシュ化文字列を作成する</summary>
        /// <param name="password">パスワード文字列</param>
        /// <param name="salt">salt値。</param>
        /// <returns>ハッシュ化文字列</returns>
        public static string SSHA256(string password, ReadOnlySpan<byte> salt = default)
            => Salted("SSHA256", System.Security.Cryptography.SHA256.HashData, password, salt);

        /// <summary>SSHA384 パスワードハッシュ化文字列を作成する</summary>
        /// <param name="password">パスワード文字列</param>
        /// <param name="salt">salt値。</param>
        /// <returns>ハッシュ化文字列</returns>
        public static string SSHA384(string password, ReadOnlySpan<byte> salt = default)
            => Salted("SSHA384", System.Security.Cryptography.SHA384.HashData, password, salt);

        /// <summary>SSHA512 パスワードハッシュ化文字列を作成する</summary>
        /// <param name="password">パスワード文字列</param>
        /// <param name="salt">salt値。</param>
        /// <returns>ハッシュ化文字列</returns>
        public static string SSHA512(string password, ReadOnlySpan<byte> salt = default)
            => Salted("SSHA512", System.Security.Cryptography.SHA512.HashData, password, salt);

        /// <summary>SHA パスワードハッシュ化文字列を作成する</summary>
        /// <param name="password">パスワード文字列</param>
        /// <returns>ハッシュ化文字列</returns>
        public static string SHA(string password)
            => Dry("SHA", System.Security.Cryptography.SHA1.HashData, password);

        /// <summary>SHA256 パスワードハッシュ化文字列を作成する</summary>
        /// <param name="password">パスワード文字列</param>
        /// <returns>ハッシュ化文字列</returns>
        public static string SHA256(string password)
            => Dry("SHA256", System.Security.Cryptography.SHA256.HashData, password);

        /// <summary>SHA384 パスワードハッシュ化文字列を作成する</summary>
        /// <param name="password">パスワード文字列</param>
        /// <returns>ハッシュ化文字列</returns>
        public static string SHA384(string password)
            => Dry("SHA384", System.Security.Cryptography.SHA384.HashData, password);

        /// <summary>SHA512 パスワードハッシュ化文字列を作成する</summary>
        /// <param name="password">パスワード文字列</param>
        /// <returns>ハッシュ化文字列</returns>
        public static string SHA512(string password)
            => Dry("SHA512", System.Security.Cryptography.SHA512.HashData, password);
    }

}
