using System.Text.Json.Serialization;
using Lestaly.Converters;

namespace LestalyTest.Converters;

[TestClass]
public class FileInfoJsonConverterTests
{
    private record TestData([property: JsonConverter(typeof(FileInfoJsonConverter))] FileInfo File);

    [TestMethod]
    public async Task ConvertAsync()
    {
        var tempDir = new TempDir();
        var testFile = tempDir.Info.RelativeFile("test.json");

        var data = new TestData(new(@"C:\tekito\desu"));
        await testFile.WritePrettyJsonAsync(data);

        var restore = await testFile.ReadRoughJsonAsync<TestData>();

        restore.Should().NotBeNull();
        restore.File.FullName.Should().Be(data.File.FullName);
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
        restore.File.Should().BeNull();
    }
}
