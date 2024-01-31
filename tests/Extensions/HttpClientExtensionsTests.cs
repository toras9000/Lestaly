using System.Net.Http.Headers;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace LestalyTest.Extensions;

[TestClass()]
public class HttpClientExtensionsTests
{
    public static WebApplication? TestServer;
    public static ushort TestPort = 10001;

    [ClassInitialize]
    public static void ClassInitialize(TestContext testContext)
    {
        var builder = WebApplication.CreateBuilder();
        builder.Logging.ClearProviders();
        TestServer = builder.Build();
        TestServer.MapGet("/base64/{*encoded}", (string encoded) => Results.Bytes(Convert.FromBase64String(encoded)));
        TestServer.MapGet("/plain/{*text}", (string text) => Results.Text(text));
        TestServer.MapGet("/error/{*code}", (int code) => Results.StatusCode(code));
        TestServer.RunAsync($"http://*:{TestPort}");
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
        TestServer?.StopAsync().GetAwaiter().GetResult();
    }


    [TestMethod()]
    public async Task GetFileAsync()
    {
        var tempDir = new TempDir();

        using var client = new HttpClient();

        {
            var encoded = Convert.ToBase64String(new byte[] { 0x12, 0x34, 0x56, });
            var uri = new Uri($"http://localhost:{TestPort}/base64/{encoded}");
            var file = await client.GetFileAsync(uri, tempDir.Info.RelativeFile("aaa"));
            file.ReadAllBytes().Should().Equal(new byte[] { 0x12, 0x34, 0x56, });
        }
        {
            var encoded = Convert.ToBase64String(new byte[] { 0x34, 0x56, 0x78, });
            var uri = new Uri($"http://localhost:{TestPort}/base64/{encoded}");
            var file = await client.GetFileAsync(uri, tempDir.Info.RelativeFile("bbb").FullName);
            file.ReadAllBytes().Should().Equal(new byte[] { 0x34, 0x56, 0x78, });
        }
        {
            var encoded = Convert.ToBase64String(new byte[] { 0x56, 0x78, 0x9A });
            var uri = new Uri($"http://localhost:{TestPort}/base64/{encoded}");
            var file = await client.GetFileAsync(uri, tempDir.Info, "ccc");
            file.ReadAllBytes().Should().Equal(new byte[] { 0x56, 0x78, 0x9A, });
        }
        {
            var encoded = Convert.ToBase64String(new byte[] { 0x78, 0x9A, 0xBC, });
            var uri = new Uri($"http://localhost:{TestPort}/base64/{encoded}");
            var file = await client.GetFileAsync(uri, tempDir.Info);
            file.ReadAllBytes().Should().Equal(new byte[] { 0x78, 0x9A, 0xBC, });
        }
        {
            var encoded = Convert.ToBase64String(new byte[] { 0x78, 0x9A, 0xBC, });
            var uri = new Uri($"http://localhost:{TestPort}/base64/{encoded}");
            var name = "truncate-test";
            var file = tempDir.Info.RelativeFile(name);
            File.WriteAllBytes(file.FullName, new byte[8192]);
            var getfile = await client.GetFileAsync(uri, tempDir.Info, name);
            getfile.ReadAllBytes().Should().Equal(new byte[] { 0x78, 0x9A, 0xBC, });
        }
    }

    [TestMethod()]
    public async Task TryGetAsync()
    {
        var client = new HttpClient();

        (await client.TryGetAsync(new Uri($"http://localhost:{TestPort}/plain/abc"))).Should().Be("abc");
        (await client.TryGetAsync(new Uri($"http://localhost:{TestPort}/error/404"))).Should().BeNull();
        (await client.TryGetAsync(new Uri($"http://localhost:{TestPort + 100}/plain/abc"))).Should().BeNull();
    }
}
