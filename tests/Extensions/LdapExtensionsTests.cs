using System.DirectoryServices.Protocols;
using System.Net;
using Lestaly.Extensions;

namespace LestalyTest.Extensions;

[TestClass]
public class LdapExtensionsTests
{
    public static LdapDirectoryIdentifier? TestServer;
    public const string RootDn = "dc=myserver,o=home";
    public const string TestUsersUnitDn = "ou=persons,ou=accounts,dc=myserver,o=home";
    public const string TestUserDn = $"uid=user1,{TestUsersUnitDn}";
    public static readonly NetworkCredential ModifierCredential = new("uid=configurator,ou=operators,dc=myserver,o=home", "configurator-pass");

    [ClassInitialize]
    public static void ClassInitialize(TestContext testContext)
    {
        try
        {
            var server = new LdapDirectoryIdentifier("localhost", 389);
            using var ldap = new LdapConnection(server);
            ldap.SessionOptions.SecureSocketLayer = false;
            ldap.SessionOptions.ProtocolVersion = 3;
            ldap.AuthType = AuthType.Anonymous;
            ldap.Bind();

            TestServer = server;
        }
        catch { }
    }

    [TestMethod()]
    public async Task SendRequestAsync()
    {
        if (TestServer == null) Assert.Inconclusive();

        using var ldap = new LdapConnection(TestServer);
        ldap.SessionOptions.ProtocolVersion = 3;
        ldap.AuthType = AuthType.Anonymous;
        ldap.Bind();

        var searchReq = new SearchRequest();
        searchReq.DistinguishedName = "";
        searchReq.Scope = SearchScope.Base;

        var searchRsp = await ldap.SendRequestAsync(searchReq);
        searchRsp.Should().BeOfType<SearchResponse>().Which.Entries.Count.Should().NotBe(0); ;
    }

    [TestMethod()]
    public async Task SendRequestAsync_Cancel()
    {
        if (TestServer == null) Assert.Inconclusive();

        using var ldap = new LdapConnection(TestServer);
        ldap.SessionOptions.ProtocolVersion = 3;
        ldap.AuthType = AuthType.Anonymous;
        ldap.Bind();

        var searchReq = new SearchRequest();
        searchReq.DistinguishedName = "";
        searchReq.Scope = SearchScope.Base;

        using var canceller = new CancellationTokenSource();
        canceller.Cancel();
        await FluentActions.Awaiting(() => ldap.SendRequestAsync(searchReq, cancelToken: canceller.Token))
                    .Should().ThrowAsync<OperationCanceledException>();
    }

    [TestMethod()]
    public async Task GetEntryAsync()
    {
        if (TestServer == null) Assert.Inconclusive();

        using var ldap = new LdapConnection(TestServer);
        ldap.SessionOptions.ProtocolVersion = 3;
        ldap.AuthType = AuthType.Anonymous;
        ldap.Bind();

        var resultEntry = await ldap.GetEntryAsync(TestUserDn);
        Assert.IsNotNull(resultEntry);
        resultEntry.DistinguishedName.Should().Be(TestUserDn);
    }

    [TestMethod()]
    public async Task GetEntryOrDefaultAsync()
    {
        if (TestServer == null) Assert.Inconclusive();

        using var ldap = new LdapConnection(TestServer);
        ldap.SessionOptions.ProtocolVersion = 3;
        ldap.AuthType = AuthType.Anonymous;
        ldap.Bind();

        var resultEntry = await ldap.GetEntryOrDefaultAsync(TestUserDn);
        Assert.IsNotNull(resultEntry);
        resultEntry.DistinguishedName.Should().Be(TestUserDn);
    }

    [TestMethod()]
    public async Task GetEntryOrDefaultAsync_fail()
    {
        if (TestServer == null) Assert.Inconclusive();

        using var ldap = new LdapConnection(TestServer);
        ldap.SessionOptions.ProtocolVersion = 3;
        ldap.AuthType = AuthType.Anonymous;
        ldap.Bind();

        var resultEntry = await ldap.GetEntryOrDefaultAsync("uid=invalid");
        resultEntry.Should().BeNull();
    }

    [TestMethod()]
    public async Task CreateEntryAsync()
    {
        if (TestServer == null) Assert.Inconclusive();

        using var ldap = new LdapConnection(TestServer);
        ldap.SessionOptions.ProtocolVersion = 3;
        ldap.AuthType = AuthType.Basic;
        ldap.Credential = ModifierCredential;
        ldap.Bind();

        var uid = Guid.NewGuid().ToString();
        var resultEntry = await ldap.CreateEntryAsync($"uid={uid},{TestUsersUnitDn}", [new("objectClass", "account"), new("uid", uid)]);
        Assert.IsNotNull(resultEntry);
    }

    [TestMethod()]
    public async Task AddAttributeAsync()
    {
        if (TestServer == null) Assert.Inconclusive();

        using var ldap = new LdapConnection(TestServer);
        ldap.SessionOptions.ProtocolVersion = 3;
        ldap.AuthType = AuthType.Basic;
        ldap.Credential = ModifierCredential;
        ldap.Bind();

        var uid = Guid.NewGuid().ToString();
        var dn = $"uid={uid},{TestUsersUnitDn}";
        await ldap.CreateEntryAsync(dn, [new("objectClass", "account"), new("uid", uid)]);
        await ldap.AddAttributeAsync(dn, "description", ["test"]);
        var entry = await ldap.GetEntryOrDefaultAsync(dn);
        entry.Should().NotBeNull();
        entry.GetAttributeFirstValue("description").Should().Be("test");
    }

    [TestMethod()]
    public async Task ReplaceAttributeAsync()
    {
        if (TestServer == null) Assert.Inconclusive();

        using var ldap = new LdapConnection(TestServer);
        ldap.SessionOptions.ProtocolVersion = 3;
        ldap.AuthType = AuthType.Basic;
        ldap.Credential = ModifierCredential;
        ldap.Bind();

        var uid = Guid.NewGuid().ToString();
        var dn = $"uid={uid},{TestUsersUnitDn}";
        await ldap.CreateEntryAsync(dn, [new("objectClass", "account"), new("uid", uid)]);
        await ldap.ReplaceAttributeAsync(dn, "description", ["test1", "test2"]);
        var entry = await ldap.GetEntryOrDefaultAsync(dn);
        entry.Should().NotBeNull();
        entry.EnumerateAttributeValues("description").Should().Equal("test1", "test2");
    }

    [TestMethod()]
    public void MakePasswordHashSSHA256()
    {
        var salt = (stackalloc byte[] { 0xCD, 0x22, 0x1C, 0x92, 0xBE, 0x08, 0x52, 0xFE });
        var pass = "abc";
        var expect = "{SSHA256}Q/Qu/OcrWcBlpYYLBIZ0DhJnjt4K82fMJfhESavbRq3NIhySvghS/g==";
        var actual = LdapExtensions.MakePasswordHashSSHA256(pass, salt);
        actual.Should().Be(expect);
    }


}
