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
    public async Task SearchAsync()
    {
        if (TestServer == null) Assert.Inconclusive();

        using var ldap = new LdapConnection(TestServer);
        ldap.SessionOptions.ProtocolVersion = 3;
        ldap.AuthType = AuthType.Anonymous;
        ldap.Bind();

        var searchRsp = await ldap.SearchAsync(TestUsersUnitDn, SearchScope.OneLevel);
        searchRsp.Should().BeOfType<SearchResponse>().Which.Entries.Count.Should().NotBe(0); ;
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
    public async Task DeleteAttributeAsync()
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
        await ldap.AddAttributeAsync(dn, "description", ["test1", "test2", "test3", "test4"]);

        await ldap.DeleteAttributeAsync(dn, "description", ["test2", "test4"]);
        var entry = await ldap.GetEntryOrDefaultAsync(dn);
        entry.Should().NotBeNull();
        entry.EnumerateAttributeValues("description").ToArray().Should().BeEquivalentTo(["test1", "test3"]);
    }

    [TestMethod()]
    public void MakePasswordHashSSHA()
    {
        var salt = (stackalloc byte[] { 0x75, 0x48, 0xE1, 0x72, 0x74, 0xAB, 0x86, 0x60 });
        var pass = "abc";
        var expect = "{SSHA}59vYnJApzos31ftPdg5QI2el1+p1SOFydKuGYA==";
        var actual = LdapExtensions.MakePasswordHash.SSHA(pass, salt);
        actual.Should().Be(expect);
    }

    [TestMethod()]
    public void MakePasswordHashSSHA256()
    {
        var salt = (stackalloc byte[] { 0xD4, 0xDD, 0x61, 0x23, 0x8E, 0xD9, 0x77, 0x59 });
        var pass = "abc";
        var expect = "{SSHA256}QMyOKYH3mkeTr6nNgB2EpYFHy9TH8sKeavPbayNdob3U3WEjjtl3WQ==";
        var actual = LdapExtensions.MakePasswordHash.SSHA256(pass, salt);
        actual.Should().Be(expect);
    }

    [TestMethod()]
    public void MakePasswordHashSSHA384()
    {
        var salt = (stackalloc byte[] { 0x44, 0x06, 0xDF, 0x71, 0x20, 0x84, 0xEE, 0x0F });
        var pass = "abc";
        var expect = "{SSHA384}Eb1Wuu95elyDfBwSHoEL7/VzOXpA9vTl3+rbPYbaecSp1EJAVzyNQ4d4qrHURn9VRAbfcSCE7g8=";
        var actual = LdapExtensions.MakePasswordHash.SSHA384(pass, salt);
        actual.Should().Be(expect);
    }

    [TestMethod()]
    public void MakePasswordHashSSHA512()
    {
        var salt = (stackalloc byte[] { 0xDA, 0x7D, 0xC5, 0x7D, 0xB5, 0x86, 0x32, 0x26 });
        var pass = "abc";
        var expect = "{SSHA512}r/sMUIADwgCiD1DBlTnbkJrM+olA+mjG0oM2RmWleN6bc8+0xOygksDFVOgmTQW6ZUp4C5qAmE2+275eaTuKOtp9xX21hjIm";
        var actual = LdapExtensions.MakePasswordHash.SSHA512(pass, salt);
        actual.Should().Be(expect);
    }

    [TestMethod()]
    public void MakePasswordHashSHA()
    {
        var pass = "abc";
        var expect = "{SHA}qZk+NkcGgWq6PiVxeFDCbJzQ2J0=";
        var actual = LdapExtensions.MakePasswordHash.SHA(pass);
        actual.Should().Be(expect);
    }

    [TestMethod()]
    public void MakePasswordHashSHA256()
    {
        var pass = "abc";
        var expect = "{SHA256}ungWv48Bz+pBQUDeXa4iI7ADYaOWF3qctBD/YfIAFa0=";
        var actual = LdapExtensions.MakePasswordHash.SHA256(pass);
        actual.Should().Be(expect);
    }

    [TestMethod()]
    public void MakePasswordHashSHA384()
    {
        var pass = "abc";
        var expect = "{SHA384}ywB1P0WjXou1oD1pmsZQBycsMqsO3tFjGotgWkP/W+2AhgcroefMI1i67KE0yCWn";
        var actual = LdapExtensions.MakePasswordHash.SHA384(pass);
        actual.Should().Be(expect);
    }

    [TestMethod()]
    public void MakePasswordHashSHA512()
    {
        var pass = "abc";
        var expect = "{SHA512}3a81oZNherrMQXNJriBBMRLm+k6JqX6iCp7u5ktV05ohkpkqJ0/BqDa6PCOj/uu9RU1EI2Q86A4qmslPpUyknw==";
        var actual = LdapExtensions.MakePasswordHash.SHA512(pass);
        actual.Should().Be(expect);
    }


}
