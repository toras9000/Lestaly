using FluentAssertions;

namespace LestalyTest.Extensions;

[TestClass()]
public class RoughScramblerExtensionsTests
{
    [TestMethod()]
    public void ScrambleText_NoParam()
    {
        var scrambler = new RoughScrambler();
        var enc = scrambler.ScrambleText("abc");
        var dec = scrambler.DescrambleText(enc);
        dec.Should().Be("abc");
    }

    [TestMethod()]
    public void ScrambleText_WithParam()
    {
        var scrambler = new RoughScrambler("xxx");
        var enc = scrambler.ScrambleText("abc");
        var dec = scrambler.DescrambleText(enc);
        dec.Should().Be("abc");
    }

    [TestMethod()]
    public void ScrambleText_Failed()
    {
        var scrambler1 = new RoughScrambler();
        var scrambler2 = new RoughScrambler("xxx");
        var enc = scrambler1.ScrambleText("abc");
        var dec = scrambler2.DescrambleText(enc);
        dec.Should().BeNull();
    }

    record TestItem(string Name, int Number, DateTime Timestamp);

    [TestMethod()]
    public void ScrambleJson_NoParam()
    {
        var item = new TestItem("asd", 123, new DateTime(2023, 1, 2, 3, 4, 5));
        var scrambler = new RoughScrambler();
        var enc = scrambler.ScrambleObject(item);
        var dec = scrambler.DescrambleObject<TestItem>(enc);
        dec.Should().BeEquivalentTo(item);
    }

    [TestMethod()]
    public void ScrambleJson_WithParam()
    {
        var item = new TestItem("asd", 123, new DateTime(2023, 1, 2, 3, 4, 5));
        var scrambler = new RoughScrambler("xyz");
        var enc = scrambler.ScrambleObject(item);
        var dec = scrambler.DescrambleObject<TestItem>(enc);
        dec.Should().BeEquivalentTo(item);
    }

    [TestMethod()]
    public void ScrambleJson_Failed()
    {
        var scrambler1 = new RoughScrambler();
        var scrambler2 = new RoughScrambler("xxx");
        var enc = scrambler1.ScrambleObject("abc");
        var dec = scrambler2.DescrambleObject<TestItem>(enc);
        dec.Should().BeNull();
    }


    [TestMethod()]
    public void ScrambleTextToFile_Descramble()
    {
        var tempDir = new TempDir();
        var testFile = tempDir.Info.RelativeFile("scramble.bin");

        var org = "abc";
        var scrambler = new RoughScrambler();
        scrambler.ScrambleTextToFile(testFile, org);

        var dec = scrambler.DescrambleTextFromFile(testFile);
        dec.Should().Be(org);
    }

    [TestMethod()]
    public void ScrambleTextToFile_Error()
    {
        var tempDir = new TempDir();
        tempDir.Info.RelativeDirectory("scramble.bin").Create();
        var testFile = tempDir.Info.RelativeFile("scramble.bin");

        var org = "abc";
        var scrambler = new RoughScrambler();
        new Action(() => scrambler.ScrambleTextToFile(testFile, org, ignoreErr: false)).Should().Throw<Exception>();
    }

    [TestMethod()]
    public void ScrambleTextToFile_IgnoreError()
    {
        var tempDir = new TempDir();
        tempDir.Info.RelativeDirectory("scramble.bin").Create();
        var testFile = tempDir.Info.RelativeFile("scramble.bin");

        var org = "abc";
        var scrambler = new RoughScrambler();
        new Action(() => scrambler.ScrambleTextToFile(testFile, org, ignoreErr: true)).Should().NotThrow();
    }

    [TestMethod()]
    public void DescrambleTextFromFile_NoFile()
    {
        var tempDir = new TempDir();
        var testFile = tempDir.Info.RelativeFile("nothing.bin");

        var scrambler = new RoughScrambler();
        scrambler.DescrambleTextFromFile(testFile).Should().BeNull();
    }

    [TestMethod()]
    public void DescrambleTextFromFile_NoScrambled()
    {
        var tempDir = new TempDir();
        var testFile = tempDir.Info.RelativeFile("test.bin");
        testFile.WriteAllText("a");

        var scrambler = new RoughScrambler();
        scrambler.DescrambleTextFromFile(testFile).Should().BeNull();
    }

    [TestMethod()]
    public async Task ScrambleTextToFileAsync_Descramble()
    {
        var tempDir = new TempDir();
        var testFile = tempDir.Info.RelativeFile("scramble.bin");

        var org = "abc";
        var scrambler = new RoughScrambler();
        await scrambler.ScrambleTextToFileAsync(testFile, org);

        var dec = await scrambler.DescrambleTextFromFileAsync(testFile);
        dec.Should().Be(org);
    }

    [TestMethod()]
    public async Task ScrambleTextToFileAsync_Error()
    {
        var tempDir = new TempDir();
        tempDir.Info.RelativeDirectory("scramble.bin").Create();
        var testFile = tempDir.Info.RelativeFile("scramble.bin");

        var org = "abc";
        var scrambler = new RoughScrambler();
        await FluentActions.Awaiting(() => scrambler.ScrambleTextToFileAsync(testFile, org, ignoreErr: false).AsTask()).Should().ThrowAsync<Exception>();
    }

    [TestMethod()]
    public async Task ScrambleTextToFileAsync_IgnoreError()
    {
        var tempDir = new TempDir();
        tempDir.Info.RelativeDirectory("scramble.bin").Create();
        var testFile = tempDir.Info.RelativeFile("scramble.bin");

        var org = "abc";
        var scrambler = new RoughScrambler();
        await FluentActions.Awaiting(() => scrambler.ScrambleTextToFileAsync(testFile, org, ignoreErr: true).AsTask()).Should().NotThrowAsync();
    }

    [TestMethod()]
    public async Task DescrambleTextFromFileAsync_NoFile()
    {
        var tempDir = new TempDir();
        var testFile = tempDir.Info.RelativeFile("nothing.bin");

        var scrambler = new RoughScrambler();
        (await scrambler.DescrambleTextFromFileAsync(testFile)).Should().BeNull();
    }

    [TestMethod()]
    public async Task DescrambleTextFromFileAsync_NoScrambled()
    {
        var tempDir = new TempDir();
        var testFile = tempDir.Info.RelativeFile("test.bin");
        testFile.WriteAllText("a");

        var scrambler = new RoughScrambler();
        (await scrambler.DescrambleTextFromFileAsync(testFile)).Should().BeNull();
    }


    [TestMethod()]
    public void ScrambleObjectToFile_Descramble()
    {
        var tempDir = new TempDir();
        var testFile = tempDir.Info.RelativeFile("scramble.bin");

        var org = new TestItem("abc", 123, DateTime.Now);
        var scrambler = new RoughScrambler();
        scrambler.ScrambleObjectToFile(testFile, org);

        var dec = scrambler.DescrambleObjectFromFile<TestItem>(testFile);
        dec.Should().Be(org);
    }

    [TestMethod()]
    public void ScrambleObjectToFile_Error()
    {
        var tempDir = new TempDir();
        tempDir.Info.RelativeDirectory("scramble.bin").Create();
        var testFile = tempDir.Info.RelativeFile("scramble.bin");

        var org = new TestItem("abc", 123, DateTime.Now);
        var scrambler = new RoughScrambler();
        new Action(() => scrambler.ScrambleObjectToFile(testFile, org, ignoreErr: false)).Should().Throw<Exception>();
    }

    [TestMethod()]
    public void ScrambleObjectToFile_IgnoreError()
    {
        var tempDir = new TempDir();
        tempDir.Info.RelativeDirectory("scramble.bin").Create();
        var testFile = tempDir.Info.RelativeFile("scramble.bin");

        var org = new TestItem("abc", 123, DateTime.Now);
        var scrambler = new RoughScrambler();
        new Action(() => scrambler.ScrambleObjectToFile(testFile, org, ignoreErr: true)).Should().NotThrow();
    }

    [TestMethod()]
    public void ScrambleObjectToFile_NoFile()
    {
        var tempDir = new TempDir();
        var testFile = tempDir.Info.RelativeFile("scramble.bin");

        var scrambler = new RoughScrambler();
        scrambler.DescrambleObjectFromFile<TestItem>(testFile).Should().BeNull();
    }

    [TestMethod()]
    public void ScrambleObjectToFile_NoScrambled()
    {
        var tempDir = new TempDir();
        var testFile = tempDir.Info.RelativeFile("test.bin");
        testFile.WriteAllText("a");

        var scrambler = new RoughScrambler();
        scrambler.DescrambleObjectFromFile<TestItem>(testFile).Should().BeNull();
    }

    [TestMethod()]
    public async Task ScrambleObjectToFileAsync_Descramble()
    {
        var tempDir = new TempDir();
        var testFile = tempDir.Info.RelativeFile("scramble.bin");

        var org = new TestItem("abc", 123, DateTime.Now);
        var scrambler = new RoughScrambler();
        await scrambler.ScrambleObjectToFileAsync(testFile, org);

        var dec = await scrambler.DescrambleObjectFromFileAsync<TestItem>(testFile);
        dec.Should().Be(org);
    }

    [TestMethod()]
    public async Task ScrambleObjectToFileAsync_Error()
    {
        var tempDir = new TempDir();
        tempDir.Info.RelativeDirectory("scramble.bin").Create();
        var testFile = tempDir.Info.RelativeFile("scramble.bin");

        var org = new TestItem("abc", 123, DateTime.Now);
        var scrambler = new RoughScrambler();
        await FluentActions.Awaiting(() => scrambler.ScrambleObjectToFileAsync(testFile, org, ignoreErr: false).AsTask()).Should().ThrowAsync<Exception>();
    }

    [TestMethod()]
    public async Task ScrambleObjectToFileAsync_IgnoreError()
    {
        var tempDir = new TempDir();
        tempDir.Info.RelativeDirectory("scramble.bin").Create();
        var testFile = tempDir.Info.RelativeFile("scramble.bin");

        var org = new TestItem("abc", 123, DateTime.Now);
        var scrambler = new RoughScrambler();
        await FluentActions.Awaiting(() => scrambler.ScrambleObjectToFileAsync(testFile, org, ignoreErr: true).AsTask()).Should().NotThrowAsync();
    }

    [TestMethod()]
    public async Task DescrambleObjectFromFileAsync_NoFile()
    {
        var tempDir = new TempDir();
        var testFile = tempDir.Info.RelativeFile("nothing.bin");

        var scrambler = new RoughScrambler();
        (await scrambler.DescrambleObjectFromFileAsync<TestItem>(testFile)).Should().BeNull();
    }

    [TestMethod()]
    public async Task DescrambleObjectFromFileAsync_NoScrambled()
    {
        var tempDir = new TempDir();
        var testFile = tempDir.Info.RelativeFile("test.bin");
        testFile.WriteAllText("a");

        var scrambler = new RoughScrambler();
        (await scrambler.DescrambleObjectFromFileAsync<TestItem>(testFile)).Should().BeNull();
    }
}