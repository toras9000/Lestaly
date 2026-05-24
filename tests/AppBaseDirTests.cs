namespace LestalyTest;

[TestClass]
public class AppBaseDirTests
{
    [TestMethod]
    public void RelativeFileAt()
    {
        AppBaseDir.RelativeFileAt(@"ghi\jkl")
            .Should().BeOfType<FileInfo>()
            .Which.FullName.Should().Be(Path.Combine(AppContext.BaseDirectory, @"ghi\jkl"));

        AppBaseDir.RelativeFileAt(@".\ghi\jkl")
            .Should().BeOfType<FileInfo>()
            .Which.FullName.Should().Be(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @".\ghi\jkl")));
    }

    [TestMethod]
    public void RelativeFileAt_Traversal()
    {
        {
            AppBaseDir.RelativeFileAt(@"..\ghi\jkl")
                .Should().BeOfType<FileInfo>()
                .Which.FullName.Should().Be(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\ghi\jkl")));
        }

        {
            var segCount = new DirectoryInfo(AppContext.BaseDirectory).GetPathSegments().Count;
            var relPath = @$"{Enumerable.Repeat("..", segCount).JoinString(@"\").TieIn(@"\")}def\jkl";
            AppBaseDir.RelativeFileAt(relPath)
                .Should().BeOfType<FileInfo>()
                .Which.FullName.Should().Be(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, relPath)));
        }

        {
            var segCount = new DirectoryInfo(AppContext.BaseDirectory).GetPathSegments().Count;
            var relPath = @$"{Enumerable.Repeat("..", segCount + 1).JoinString(@"\").TieIn(@"\")}def\jkl";
            AppBaseDir.RelativeFileAt(relPath)
                .Should().BeOfType<FileInfo>()
                .Which.FullName.Should().Be(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, relPath)));
        }
    }

    [TestMethod]
    public void RelativeFileAt_Absolute()
    {
        AppBaseDir.RelativeFileAt(@"V:\hoge\fuga")
            .Should().BeOfType<FileInfo>()
            .Which.FullName.Should().Be(@"V:\hoge\fuga");
    }

    [TestMethod]
    public void RelativeFileAt_EmptyRelativeFail()
    {
        AppBaseDir.RelativeFileAt("").Should().BeNull();

        AppBaseDir.RelativeFileAt(" ").Should().BeNull();

        AppBaseDir.RelativeFileAt(null).Should().BeNull();
    }

    [TestMethod]
    public void RelativeFile()
    {
        AppBaseDir.RelativeFile(@"ghi\jkl")
            .Should().BeOfType<FileInfo>()
            .Which.FullName.Should().Be(Path.Combine(AppContext.BaseDirectory, @"ghi\jkl"));

        AppBaseDir.RelativeFile(@".\ghi\jkl")
            .Should().BeOfType<FileInfo>()
            .Which.FullName.Should().Be(Path.Combine(AppContext.BaseDirectory, @"ghi\jkl"));
    }

    [TestMethod]
    public void RelativeFile_Traversal()
    {
        {
            AppBaseDir.RelativeFile(@"..\ghi\jkl")
                .Should().BeOfType<FileInfo>()
                .Which.FullName.Should().Be(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\ghi\jkl")));
        }

        {
            var segCount = new DirectoryInfo(AppContext.BaseDirectory).GetPathSegments().Count;
            var relPath = @$"{Enumerable.Repeat("..", segCount).JoinString(@"\").TieIn(@"\")}def\jkl";
            AppBaseDir.RelativeFile(relPath)
                .Should().BeOfType<FileInfo>()
                .Which.FullName.Should().Be(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, relPath)));
        }

        {
            var segCount = new DirectoryInfo(AppContext.BaseDirectory).GetPathSegments().Count;
            var relPath = @$"{Enumerable.Repeat("..", segCount + 1).JoinString(@"\").TieIn(@"\")}def\jkl";
            AppBaseDir.RelativeFile(relPath)
                .Should().BeOfType<FileInfo>()
                .Which.FullName.Should().Be(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, relPath)));
        }
    }

    [TestMethod]
    public void RelativeFile_Absolute()
    {
        AppBaseDir.RelativeFile(@"V:\hoge\fuga")
            .Should().BeOfType<FileInfo>()
            .Which.FullName.Should().Be(@"V:\hoge\fuga");
    }

    [TestMethod]
    public void RelativeFile_EmptyRelativeFail()
    {
        FluentActions.Invoking(() => AppBaseDir.RelativeFile(""))
            .Should().Throw<ArgumentException>();

        FluentActions.Invoking(() => AppBaseDir.RelativeFile(" "))
            .Should().Throw<ArgumentException>();

        FluentActions.Invoking(() => AppBaseDir.RelativeFile(null!))
            .Should().Throw<ArgumentException>();
    }

    [TestMethod]
    public void RelativeDirectoryAt()
    {
        AppBaseDir.RelativeDirectoryAt(@"ghi\jkl")
            .Should().BeOfType<DirectoryInfo>()
            .Which.FullName.Should().Be(Path.Combine(AppContext.BaseDirectory, @"ghi\jkl"));

        AppBaseDir.RelativeDirectoryAt(@".\ghi\jkl")
            .Should().BeOfType<DirectoryInfo>()
            .Which.FullName.Should().Be(Path.Combine(AppContext.BaseDirectory, @"ghi\jkl"));
    }

    [TestMethod]
    public void RelativeDirectoryAt_Traversal()
    {
        {
            AppBaseDir.RelativeDirectoryAt(@"..\ghi\jkl")
                .Should().BeOfType<DirectoryInfo>()
                .Which.FullName.Should().Be(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\ghi\jkl")));
        }

        {
            var segCount = new DirectoryInfo(AppContext.BaseDirectory).GetPathSegments().Count;
            var relPath = @$"{Enumerable.Repeat("..", segCount).JoinString(@"\").TieIn(@"\")}def\jkl";
            AppBaseDir.RelativeDirectoryAt(relPath)
                .Should().BeOfType<DirectoryInfo>()
                .Which.FullName.Should().Be(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, relPath)));
        }

        {
            var segCount = new DirectoryInfo(AppContext.BaseDirectory).GetPathSegments().Count;
            var relPath = @$"{Enumerable.Repeat("..", segCount + 1).JoinString(@"\").TieIn(@"\")}def\jkl";
            AppBaseDir.RelativeDirectoryAt(relPath)
                .Should().BeOfType<DirectoryInfo>()
                .Which.FullName.Should().Be(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, relPath)));
        }
    }

    [TestMethod]
    public void RelativeDirectoryAt_Absolute()
    {
        AppBaseDir.RelativeDirectoryAt(@"V:\hoge\fuga")
            .Should().BeOfType<DirectoryInfo>()
            .Which.FullName.Should().Be(@"V:\hoge\fuga");
    }

    [TestMethod]
    public void RelativeDirectoryAt_EmptyRelative()
    {
        AppBaseDir.RelativeDirectoryAt("")
            .Should().BeNull();

        AppBaseDir.RelativeDirectoryAt(" ")
            .Should().BeNull();

        AppBaseDir.RelativeDirectoryAt(null)
            .Should().BeNull();
    }

    [TestMethod]
    public void RelativeDirectory()
    {
        AppBaseDir.RelativeDirectory(@"ghi\jkl")
            .Should().BeOfType<DirectoryInfo>()
            .Which.FullName.Should().Be(Path.Combine(AppContext.BaseDirectory, @"ghi\jkl"));

        AppBaseDir.RelativeDirectory(@".\ghi\jkl")
            .Should().BeOfType<DirectoryInfo>()
            .Which.FullName.Should().Be(Path.Combine(AppContext.BaseDirectory, @"ghi\jkl"));
    }

    [TestMethod]
    public void RelativeDirectory_Traversal()
    {
        {
            AppBaseDir.RelativeDirectory(@"..\ghi\jkl")
                .Should().BeOfType<DirectoryInfo>()
                .Which.FullName.Should().Be(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\ghi\jkl")));
        }

        {
            var segCount = new DirectoryInfo(AppContext.BaseDirectory).GetPathSegments().Count;
            var relPath = @$"{Enumerable.Repeat("..", segCount).JoinString(@"\").TieIn(@"\")}def\jkl";
            AppBaseDir.RelativeDirectory(relPath)
                .Should().BeOfType<DirectoryInfo>()
                .Which.FullName.Should().Be(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, relPath)));
        }

        {
            var segCount = new DirectoryInfo(AppContext.BaseDirectory).GetPathSegments().Count;
            var relPath = @$"{Enumerable.Repeat("..", segCount + 1).JoinString(@"\").TieIn(@"\")}def\jkl";
            AppBaseDir.RelativeDirectory(relPath)
                .Should().BeOfType<DirectoryInfo>()
                .Which.FullName.Should().Be(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, relPath)));
        }
    }

    [TestMethod]
    public void RelativeDirectory_Absolute()
    {
        AppBaseDir.RelativeDirectory(@"V:\hoge\fuga")
            .Should().BeOfType<DirectoryInfo>()
            .Which.FullName.Should().Be(@"V:\hoge\fuga");
    }

    [TestMethod]
    public void RelativeDirectory_EmptyRelative()
    {
        AppBaseDir.RelativeDirectory("")
            .Should().BeOfType<DirectoryInfo>()
            .Which.FullName.Should().Be(AppContext.BaseDirectory);

        AppBaseDir.RelativeDirectory(null)
            .Should().BeOfType<DirectoryInfo>()
            .Which.FullName.Should().Be(AppContext.BaseDirectory);
    }
}
