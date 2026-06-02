using System.Net;
using System.Text.Json.Serialization;
using Lestaly.Converters;

namespace LestalyTest;

[TestClass]
public class IPEndPointJsonConverterTests
{
    private record TestData([property: JsonConverter(typeof(IPEndPointJsonConverter))] IPEndPoint Endpoint);

    [TestMethod]
    public async Task ConvertV4Async()
    {
        var tempDir = new TempDir();
        var testFile = tempDir.Info.RelativeFile("test.json");

        var data = new TestData(new(IPAddress.Parse("192.168.12.34"), 6543));
        await testFile.WritePrettyJsonAsync(data);

        var restore = await testFile.ReadRoughJsonAsync<TestData>();

        restore.Should().NotBeNull();
        restore.Endpoint.Should().Be(data.Endpoint);
    }

    [TestMethod]
    public async Task ConvertV6Async()
    {
        var tempDir = new TempDir();
        var testFile = tempDir.Info.RelativeFile("test.json");

        var data = new TestData(new(IPAddress.Parse("[fe80::1]"), 6543));
        await testFile.WritePrettyJsonAsync(data);

        var restore = await testFile.ReadRoughJsonAsync<TestData>();

        restore.Should().NotBeNull();
        restore.Endpoint.Should().Be(data.Endpoint);
    }

    [TestMethod]
    public async Task ConvertNullAsync()
    {
        var tempDir = new TempDir();
        var testFile = tempDir.Info.RelativeFile("test.json");

        var data = new TestData(null!);
        await testFile.WritePrettyJsonAsync(data);

        var restore = await testFile.ReadRoughJsonAsync<TestData>();

        restore.Should().NotBeNull();
        restore.Endpoint.Should().BeNull();
    }
}
