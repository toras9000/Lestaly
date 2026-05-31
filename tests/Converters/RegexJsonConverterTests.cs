using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Lestaly.Converters;

namespace LestalyTest;

[TestClass]
public class RegexJsonConverterTests
{
    private record TestData([property: JsonConverter(typeof(RegexJsonConverter))] Regex Regex);

    [TestMethod]
    public async Task ConvertAsync()
    {
        var tempDir = new TempDir();
        var testFile = tempDir.Info.RelativeFile("test.json");

        var data = new TestData(new(@"^a", RegexOptions.IgnoreCase | RegexOptions.Singleline, TimeSpan.FromSeconds(1)));
        await testFile.WritePrettyJsonAsync(data);

        var restore = await testFile.ReadRoughJsonAsync<TestData>();

        restore.Should().NotBeNull();
        restore.Regex.ToString().Should().Be(data.Regex.ToString());
        restore.Regex.Options.Should().Be(data.Regex.Options);
        restore.Regex.MatchTimeout.Should().Be(data.Regex.MatchTimeout);
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
        restore.Regex.Should().BeNull();
    }
}
