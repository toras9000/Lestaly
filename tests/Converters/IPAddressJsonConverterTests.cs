using System.Net;
using System.Text.Json.Serialization;
using Lestaly.Converters;

namespace LestalyTest;

[TestClass]
public class IPAddressJsonConverterTests
{
    private record TestData([property: JsonConverter(typeof(IPAddressJsonConverter))] IPAddress Address);

    [TestMethod]
    public async Task ConvertV4Async()
    {
        var tempDir = new TempDir();
        var testFile = tempDir.Info.RelativeFile("test.json");

        var data = new TestData(IPAddress.Parse("192.168.12.34"));
        await testFile.WritePrettyJsonAsync(data);

        var restore = await testFile.ReadRoughJsonAsync<TestData>();

        restore.Should().NotBeNull();
        restore.Address.Should().Be(data.Address);
    }

    [TestMethod]
    public async Task ConvertV6Async()
    {
        var tempDir = new TempDir();
        var testFile = tempDir.Info.RelativeFile("test.json");

        var data = new TestData(IPAddress.Parse("[fe80::1]"));
        await testFile.WritePrettyJsonAsync(data);

        var restore = await testFile.ReadRoughJsonAsync<TestData>();

        restore.Should().NotBeNull();
        restore.Address.Should().Be(data.Address);
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
        restore.Address.Should().BeNull();
    }
}
