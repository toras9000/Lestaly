using FluentAssertions;
using Lestaly;

namespace LestalyTest.Extensions;

[TestClass()]
public class FileSystemInfoExtensionsTests
{
    [TestMethod()]
    public void GetReadOnly_File()
    {
        using var tempDir = new TempDir();

        var file = tempDir.Info.GetRelativeFile("asd.txt").Touch();
        file.GetReadOnly().Should().Be(false);
        file.Attributes |= FileAttributes.ReadOnly;
        file.GetReadOnly().Should().Be(true);
    }

    [TestMethod()]
    public void GetReadOnly_Dir()
    {
        using var tempDir = new TempDir();

        var dir = tempDir.Info.GetRelativeDirectory("def").WithCreate();
        dir.GetReadOnly().Should().Be(false);
        dir.Attributes |= FileAttributes.ReadOnly;
        dir.GetReadOnly().Should().Be(true);
    }

    [TestMethod()]
    public void SetReadOnly_File()
    {
        using var tempDir = new TempDir();

        var file = tempDir.Info.GetRelativeFile("asd.txt").Touch();
        file.GetReadOnly().Should().Be(false);
        file.SetReadOnly(true);
        file.GetReadOnly().Should().Be(true);
        file.SetReadOnly(false);
        file.GetReadOnly().Should().Be(false);
    }

    [TestMethod()]
    public void SetReadOnly_Dir()
    {
        using var tempDir = new TempDir();

        var dir = tempDir.Info.GetRelativeDirectory("def").WithCreate();
        dir.GetReadOnly().Should().Be(false);
        dir.SetReadOnly(true);
        dir.GetReadOnly().Should().Be(true);
        dir.SetReadOnly(false);
        dir.GetReadOnly().Should().Be(false);
    }

}
