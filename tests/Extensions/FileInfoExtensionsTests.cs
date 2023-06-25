using System.Text.Json;
using FluentAssertions;
using Lestaly;

namespace LestalyTest.Extensions;

[TestClass()]
public class FileExtensionsTests
{
    private static TObject? decodeJsonByTemplate<TObject>(string json, TObject template)
        => (TObject?)JsonSerializer.Deserialize(json, template!.GetType());

    private record TestData(string String, int Integer);

    #region Read JSON
    [TestMethod()]
    public async Task ReadJsonAsync()
    {
        using var tempDir = new TempDir();

        var file = tempDir.Info.RelativeFile("asd.txt");
        var data = new TestData("test", 123);
        await file.WriteJsonAsync(data);

        var actual = await file.ReadJsonAsync<TestData>();
        actual.Should().Be(data);
    }

    [TestMethod()]
    public async Task ReadJsonByTemplateAsync()
    {
        using var tempDir = new TempDir();

        var file = tempDir.Info.RelativeFile("asd.txt");
        var data = new
        {
            Text = "test",
            Number = 123,
        };
        await file.WriteJsonAsync(data);

        var actual = await file.ReadJsonByTemplateAsync(data);
        actual.Should().Be(data);
    }
    #endregion

    #region Write JSON
    [TestMethod()]
    public async Task WriteJsonAsync()
    {
        using var tempDir = new TempDir();

        var file = tempDir.Info.RelativeFile("asd.txt");
        var data = new
        {
            Text = "test",
            Number = 123,
        };
        await file.WriteJsonAsync(data);

        var json = await file.ReadAllTextAsync();
        var actual = decodeJsonByTemplate(json, data);
        actual.Should().Be(data);
    }
    #endregion

    #region FileSystem
    [TestMethod()]
    public async Task Touch()
    {
        using var tempDir = new TempDir();

        var file = tempDir.Info.RelativeFile("asd.txt");
        file.Exists.Should().Be(false);
        file.Touch();
        file.Exists.Should().Be(true);

        var timestamp = file.LastWriteTimeUtc;
        await Task.Delay(3000);
        file.Touch();
        file.LastWriteTimeUtc.Should().NotBe(timestamp);
    }
    #endregion
}
