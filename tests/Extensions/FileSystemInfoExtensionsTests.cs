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

    [TestMethod()]
    public void OmitExists()
    {
        using var tempDir = new TempDir();

        var existFile = tempDir.Info.GetRelativeFile("asd.txt").Touch();
        var notExistFile = tempDir.Info.GetRelativeFile("qwe.txt");

        existFile.OmitExists().Should().BeNull();
        notExistFile.OmitExists().Should().NotBeNull();

        var existDir = tempDir.Info.GetRelativeDirectory("abc").WithCreate();
        var notExistDir = tempDir.Info.GetRelativeDirectory("def");

        existDir.OmitExists().Should().BeNull();
        notExistDir.OmitExists().Should().NotBeNull();
    }

    [TestMethod()]
    public void OmitNotExists()
    {
        using var tempDir = new TempDir();

        var existFile = tempDir.Info.GetRelativeFile("asd.txt").Touch();
        var notExistFile = tempDir.Info.GetRelativeFile("qwe.txt");

        existFile.OmitNotExists().Should().NotBeNull();
        notExistFile.OmitNotExists().Should().BeNull();

        var existDir = tempDir.Info.GetRelativeDirectory("abc").WithCreate();
        var notExistDir = tempDir.Info.GetRelativeDirectory("def");

        existDir.OmitNotExists().Should().NotBeNull();
        notExistDir.OmitNotExists().Should().BeNull();
    }

    [TestMethod()]
    public void ThrowIfExists()
    {
        using var tempDir = new TempDir();

        var existFile = tempDir.Info.GetRelativeFile("asd.txt").Touch();
        var notExistFile = tempDir.Info.GetRelativeFile("qwe.txt");

        new Action(() => existFile.ThrowIfExists()).Should().Throw<Exception>();
        new Action(() => existFile.ThrowIfExists(i => new ApplicationException())).Should().Throw<ApplicationException>();
        new Action(() => notExistFile.ThrowIfExists()).Should().NotThrow();
        new Action(() => notExistFile.ThrowIfExists(i => new ApplicationException())).Should().NotThrow();

        var existDir = tempDir.Info.GetRelativeDirectory("abc").WithCreate();
        var notExistDir = tempDir.Info.GetRelativeDirectory("def");

        new Action(() => existDir.ThrowIfExists()).Should().Throw<Exception>();
        new Action(() => existDir.ThrowIfExists(i => new ApplicationException())).Should().Throw<ApplicationException>();
        new Action(() => notExistDir.ThrowIfExists()).Should().NotThrow();
        new Action(() => notExistDir.ThrowIfExists(i => new ApplicationException())).Should().NotThrow();
    }

    [TestMethod()]
    public void ThrowIfNotExists()
    {
        using var tempDir = new TempDir();

        var existFile = tempDir.Info.GetRelativeFile("asd.txt").Touch();
        var notExistFile = tempDir.Info.GetRelativeFile("qwe.txt");

        new Action(() => notExistFile.ThrowIfNotExists()).Should().Throw<Exception>();
        new Action(() => notExistFile.ThrowIfNotExists(i => new ApplicationException())).Should().Throw<ApplicationException>();
        new Action(() => existFile.ThrowIfNotExists()).Should().NotThrow();
        new Action(() => existFile.ThrowIfNotExists(i => new ApplicationException())).Should().NotThrow();

        var existDir = tempDir.Info.GetRelativeDirectory("abc").WithCreate();
        var notExistDir = tempDir.Info.GetRelativeDirectory("def");

        new Action(() => notExistFile.ThrowIfNotExists()).Should().Throw<Exception>();
        new Action(() => notExistFile.ThrowIfNotExists(i => new ApplicationException())).Should().Throw<ApplicationException>();
        new Action(() => existFile.ThrowIfNotExists()).Should().NotThrow();
        new Action(() => existFile.ThrowIfNotExists(i => new ApplicationException())).Should().NotThrow();
    }

    [TestMethod()]
    public void CanceIfExists()
    {
        using var tempDir = new TempDir();

        var existFile = tempDir.Info.GetRelativeFile("asd.txt").Touch();
        var notExistFile = tempDir.Info.GetRelativeFile("qwe.txt");

        new Action(() => existFile.CanceIfExists(i => "CF")).Should().Throw<OperationCanceledException>().WithMessage("CF");
        new Action(() => notExistFile.CanceIfExists()).Should().NotThrow();

        var existDir = tempDir.Info.GetRelativeDirectory("abc").WithCreate();
        var notExistDir = tempDir.Info.GetRelativeDirectory("def");

        new Action(() => existDir.CanceIfExists(i => "CD")).Should().Throw<OperationCanceledException>().WithMessage("CD");
        new Action(() => notExistDir.CanceIfExists()).Should().NotThrow();
    }

    [TestMethod()]
    public void CanceIfNotExists()
    {
        using var tempDir = new TempDir();

        var existFile = tempDir.Info.GetRelativeFile("asd.txt").Touch();
        var notExistFile = tempDir.Info.GetRelativeFile("qwe.txt");

        new Action(() => notExistFile.CanceIfNotExists(i => "CF")).Should().Throw<OperationCanceledException>().WithMessage("CF");
        new Action(() => existFile.CanceIfNotExists()).Should().NotThrow();

        var existDir = tempDir.Info.GetRelativeDirectory("abc").WithCreate();
        var notExistDir = tempDir.Info.GetRelativeDirectory("def");

        new Action(() => notExistDir.CanceIfNotExists(i => "CD")).Should().Throw<OperationCanceledException>().WithMessage("CD");
        new Action(() => existDir.CanceIfNotExists()).Should().NotThrow();
    }


}
