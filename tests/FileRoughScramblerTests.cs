namespace LestalyTest;

[TestClass()]
public class FileRoughScramblerTests
{
    [TestMethod()]
    public void ScrambleText()
    {
        using var tempDir = new TempDir();

        var scrambler = tempDir.Info.RelativeFile("test.txt").CreateScrambler();
        scrambler.ScrambleText("abcd");

        scrambler.File.ReadAllText().Should().NotBe("abcd");

        scrambler.DescrambleText().Should().Be("abcd");
    }

    [TestMethod()]
    public async ValueTask ScrambleTextAsync()
    {
        using var tempDir = new TempDir();

        var scrambler = tempDir.Info.RelativeFile("test.txt").CreateScrambler();
        await scrambler.ScrambleTextAsync("abcd");

        scrambler.File.ReadAllText().Should().NotBe("abcd");

        (await scrambler.DescrambleTextAsync()).Should().Be("abcd");
    }

    [TestMethod()]
    public void ScrambleObject()
    {
        var data = new { Text = "abc", Value = 123, };

        using var tempDir = new TempDir();

        var scrambler = tempDir.Info.RelativeFile("test.txt").CreateScrambler();
        scrambler.ScrambleObject(data);

        T? descrambleObjectByTemplate<T>(T _) => scrambler.DescrambleObject<T>();
        descrambleObjectByTemplate(data).Should().Be(data);
    }

    [TestMethod()]
    public async ValueTask ScrambleObjectAsync()
    {
        var data = new { Text = "abc", Value = 123, };

        using var tempDir = new TempDir();

        var scrambler = tempDir.Info.RelativeFile("test.txt").CreateScrambler();
        await scrambler.ScrambleObjectAsync(data);

        ValueTask<T?> descrambleObjectByTemplateAsync<T>(T _) => scrambler.DescrambleObjectAsync<T>();
        (await descrambleObjectByTemplateAsync(data)).Should().Be(data);
    }
}