namespace LestalyTest.Extensions;

[TestClass]
public class UriExtensionsTests
{
    [TestMethod]
    public void AuthorityRelative_http()
    {
        var baseUri = new Uri("http://abc:def@host.server.com/original/path");
        baseUri.AuthorityRelative("path/to/other").AbsoluteUri.Should().Be("http://abc:def@host.server.com/path/to/other");
        baseUri.AuthorityRelative("/slashed/other/path").AbsoluteUri.Should().Be("http://abc:def@host.server.com/slashed/other/path");
    }

    [TestMethod]
    public void AuthorityRelative_mailto()
    {
        var baseUri = new Uri("mailto:user@contoso.com");
        baseUri.AuthorityRelative("hoge").AbsoluteUri.Should().Be("mailto:hoge");
    }

}
