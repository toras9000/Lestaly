using System.Data;
using System.Text.RegularExpressions;

namespace LestalyTest.Extensions;

[TestClass()]
public class DirectoryInfoExtensionsTests
{
    #region FileSystemInfo
    [TestMethod]
    public void RelativeFileAt()
    {
        new DirectoryInfo(@"X:\abc\def")
            .RelativeFileAt(@"ghi\jkl")
            .Should().BeOfType<FileInfo>()
            .Which.FullName.Should().Be(@"X:\abc\def\ghi\jkl");

        new DirectoryInfo(@"X:\abc\def")
            .RelativeFileAt(@".\ghi\jkl")
            .Should().BeOfType<FileInfo>()
            .Which.FullName.Should().Be(@"X:\abc\def\ghi\jkl");
    }

    [TestMethod]
    public void RelativeFileAt_Traversal()
    {
        new DirectoryInfo(@"X:\abc\def")
            .RelativeFileAt(@"..\ghi\jkl")
            .Should().BeOfType<FileInfo>()
            .Which.FullName.Should().Be(@"X:\abc\ghi\jkl");

        new DirectoryInfo(@"X:\abc\def")
            .RelativeFileAt(@"..\def\jkl")
            .Should().BeOfType<FileInfo>()
            .Which.FullName.Should().Be(@"X:\abc\def\jkl");

        new DirectoryInfo(@"X:\abc\def")
            .RelativeFileAt(@"..\..\..\jkl")
            .Should().BeOfType<FileInfo>()
            .Which.FullName.Should().Be(@"X:\jkl");
    }

    [TestMethod]
    public void RelativeFileAt_Absolute()
    {
        new DirectoryInfo(@"X:\abc\def")
            .RelativeFileAt(@"V:\hoge\fuga")
            .Should().BeOfType<FileInfo>()
            .Which.FullName.Should().Be(@"V:\hoge\fuga");
    }

    [TestMethod]
    public void RelativeFileAt_EmptyRelativeFail()
    {
        new DirectoryInfo(@"X:\abc\def").RelativeFileAt("").Should().BeNull();

        new DirectoryInfo(@"X:\abc\def").RelativeFileAt(" ").Should().BeNull();

        new DirectoryInfo(@"X:\abc\def").RelativeFileAt(null).Should().BeNull();
    }

    [TestMethod]
    public void RelativeFileAt_NullSelfFail()
    {
        FluentActions.Invoking(() => default(DirectoryInfo)!.RelativeFileAt(@"V:\hoge\fuga"))
            .Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void RelativeFile()
    {
        new DirectoryInfo(@"X:\abc\def")
            .RelativeFile(@"ghi\jkl")
            .Should().BeOfType<FileInfo>()
            .Which.FullName.Should().Be(@"X:\abc\def\ghi\jkl");

        new DirectoryInfo(@"X:\abc\def")
            .RelativeFile(@".\ghi\jkl")
            .Should().BeOfType<FileInfo>()
            .Which.FullName.Should().Be(@"X:\abc\def\ghi\jkl");
    }

    [TestMethod]
    public void RelativeFile_Traversal()
    {
        new DirectoryInfo(@"X:\abc\def")
            .RelativeFile(@"..\ghi\jkl")
            .Should().BeOfType<FileInfo>()
            .Which.FullName.Should().Be(@"X:\abc\ghi\jkl");

        new DirectoryInfo(@"X:\abc\def")
            .RelativeFile(@"..\def\jkl")
            .Should().BeOfType<FileInfo>()
            .Which.FullName.Should().Be(@"X:\abc\def\jkl");

        new DirectoryInfo(@"X:\abc\def")
            .RelativeFile(@"..\..\..\jkl")
            .Should().BeOfType<FileInfo>()
            .Which.FullName.Should().Be(@"X:\jkl");
    }

    [TestMethod]
    public void RelativeFile_Absolute()
    {
        new DirectoryInfo(@"X:\abc\def")
            .RelativeFile(@"V:\hoge\fuga")
            .Should().BeOfType<FileInfo>()
            .Which.FullName.Should().Be(@"V:\hoge\fuga");
    }

    [TestMethod]
    public void RelativeFile_EmptyRelativeFail()
    {
        FluentActions.Invoking(() => new DirectoryInfo(@"X:\abc\def").RelativeFile(""))
            .Should().Throw<ArgumentException>();

        FluentActions.Invoking(() => new DirectoryInfo(@"X:\abc\def").RelativeFile(" "))
            .Should().Throw<ArgumentException>();

        FluentActions.Invoking(() => new DirectoryInfo(@"X:\abc\def").RelativeFile(null!))
            .Should().Throw<ArgumentException>();
    }

    [TestMethod]
    public void RelativeFile_NullSelfFail()
    {
        FluentActions.Invoking(() => default(DirectoryInfo)!.RelativeFile(@"V:\hoge\fuga"))
            .Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void RelativeDirectoryAt()
    {
        new DirectoryInfo(@"X:\abc\def")
            .RelativeDirectoryAt(@"ghi\jkl")
            .Should().BeOfType<DirectoryInfo>()
            .Which.FullName.Should().Be(@"X:\abc\def\ghi\jkl");

        new DirectoryInfo(@"X:\abc\def")
            .RelativeDirectoryAt(@".\ghi\jkl")
            .Should().BeOfType<DirectoryInfo>()
            .Which.FullName.Should().Be(@"X:\abc\def\ghi\jkl");
    }

    [TestMethod]
    public void RelativeDirectoryAt_Traversal()
    {
        new DirectoryInfo(@"X:\abc\def")
            .RelativeDirectoryAt(@"..\ghi\jkl")
            .Should().BeOfType<DirectoryInfo>()
            .Which.FullName.Should().Be(@"X:\abc\ghi\jkl");

        new DirectoryInfo(@"X:\abc\def")
            .RelativeDirectoryAt(@"..\def\jkl")
            .Should().BeOfType<DirectoryInfo>()
            .Which.FullName.Should().Be(@"X:\abc\def\jkl");

        new DirectoryInfo(@"X:\abc\def")
            .RelativeDirectoryAt(@"..\..\..\jkl")
            .Should().BeOfType<DirectoryInfo>()
            .Which.FullName.Should().Be(@"X:\jkl");
    }

    [TestMethod]
    public void RelativeDirectoryAt_Absolute()
    {
        new DirectoryInfo(@"X:\abc\def")
            .RelativeDirectoryAt(@"V:\hoge\fuga")
            .Should().BeOfType<DirectoryInfo>()
            .Which.FullName.Should().Be(@"V:\hoge\fuga");
    }

    [TestMethod]
    public void RelativeDirectoryAt_EmptyRelative()
    {
        new DirectoryInfo(@"X:\abc\def")
            .RelativeDirectoryAt("")
            .Should().BeNull();

        new DirectoryInfo(@"X:\abc\def")
            .RelativeDirectoryAt(" ")
            .Should().BeNull();

        new DirectoryInfo(@"X:\abc\def")
            .RelativeDirectoryAt(null)
            .Should().BeNull();
    }

    [TestMethod]
    public void RelativeDirectoryAt_NullFail()
    {
        FluentActions.Invoking(() => default(DirectoryInfo)!.RelativeDirectoryAt(@"V:\hoge\fuga"))
            .Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void RelativeDirectory()
    {
        new DirectoryInfo(@"X:\abc\def")
            .RelativeDirectory(@"ghi\jkl")
            .Should().BeOfType<DirectoryInfo>()
            .Which.FullName.Should().Be(@"X:\abc\def\ghi\jkl");

        new DirectoryInfo(@"X:\abc\def")
            .RelativeDirectory(@".\ghi\jkl")
            .Should().BeOfType<DirectoryInfo>()
            .Which.FullName.Should().Be(@"X:\abc\def\ghi\jkl");
    }

    [TestMethod]
    public void RelativeDirectory_Traversal()
    {
        new DirectoryInfo(@"X:\abc\def")
            .RelativeDirectory(@"..\ghi\jkl")
            .Should().BeOfType<DirectoryInfo>()
            .Which.FullName.Should().Be(@"X:\abc\ghi\jkl");

        new DirectoryInfo(@"X:\abc\def")
            .RelativeDirectory(@"..\def\jkl")
            .Should().BeOfType<DirectoryInfo>()
            .Which.FullName.Should().Be(@"X:\abc\def\jkl");

        new DirectoryInfo(@"X:\abc\def")
            .RelativeDirectory(@"..\..\..\jkl")
            .Should().BeOfType<DirectoryInfo>()
            .Which.FullName.Should().Be(@"X:\jkl");
    }

    [TestMethod]
    public void RelativeDirectory_Absolute()
    {
        new DirectoryInfo(@"X:\abc\def")
            .RelativeDirectory(@"V:\hoge\fuga")
            .Should().BeOfType<DirectoryInfo>()
            .Which.FullName.Should().Be(@"V:\hoge\fuga");
    }

    [TestMethod]
    public void RelativeDirectory_EmptyRelative()
    {
        new DirectoryInfo(@"X:\abc\def")
            .RelativeDirectory("")
            .Should().BeOfType<DirectoryInfo>()
            .Which.FullName.Should().Be(@"X:\abc\def");

        new DirectoryInfo(@"X:\abc\def")
            .RelativeDirectory(null)
            .Should().BeOfType<DirectoryInfo>()
            .Which.FullName.Should().Be(@"X:\abc\def");
    }

    [TestMethod]
    public void RelativeDirectory_NullFail()
    {
        FluentActions.Invoking(() => default(DirectoryInfo)!.RelativeDirectory(@"V:\hoge\fuga"))
            .Should().Throw<ArgumentNullException>();
    }
    #endregion

    #region FileSystem
    [TestMethod]
    public void WithCreate()
    {
        using var testDir = new TempDir();

        {
            var subDir = testDir.Info.RelativeDirectory("ttt");
            subDir.WithCreate().FullName.Should().Be(subDir.FullName);
            Directory.Exists(subDir.FullName).Should().BeTrue();
        }
        {
            var deepDir = testDir.Info.RelativeDirectory("aaa/bbb/ccc");
            deepDir.WithCreate().FullName.Should().Be(deepDir.FullName);
            Directory.Exists(deepDir.FullName).Should().BeTrue();
        }
    }
    #endregion

    #region Find
    [TestMethod]
    public void FindFile()
    {
        var tempDir = new TempDir();
        tempDir.Info.RelativeFile("abc").Touch();
        tempDir.Info.RelativeFile("def").Touch();
        tempDir.Info.RelativeFile("XXX/abc").Touch();
        tempDir.Info.RelativeFile("XXX/def").Touch();
        tempDir.Info.RelativeFile("YYY/abc").Touch();
        tempDir.Info.RelativeFile("YYY/def").Touch();

        tempDir.Info.FindFile("ABC", MatchCasing.CaseSensitive).Should().BeNull();
        tempDir.Info.FindFile("ABC", MatchCasing.CaseInsensitive).Should().NotBeNull();
        tempDir.Info.FindFile("abc", MatchCasing.CaseSensitive).Should().NotBeNull();
        tempDir.Info.FindFile("abc", MatchCasing.CaseInsensitive).Should().NotBeNull();
        tempDir.Info.FindFile("ZZZ", MatchCasing.CaseInsensitive).Should().BeNull();

        // 途中のディレクトリに対するCasingは制御できないようなので、影響のないようにしておく。
        tempDir.Info.FindFile("XXX/ABC", MatchCasing.CaseSensitive).Should().BeNull();
        tempDir.Info.FindFile("XXX/ABC", MatchCasing.CaseInsensitive).Should().NotBeNull();
        tempDir.Info.FindFile("XXX/abc", MatchCasing.CaseSensitive).Should().NotBeNull();
        tempDir.Info.FindFile("XXX/abc", MatchCasing.CaseInsensitive).Should().NotBeNull();

        tempDir.Info.FindFile("ab?", MatchCasing.CaseSensitive).Should().NotBeNull();
        tempDir.Info.FindFile("ab*", MatchCasing.CaseInsensitive).Should().NotBeNull();
        tempDir.Info.FindFile("*", MatchCasing.CaseInsensitive).Should().BeNull();
        tempDir.Info.FindFile("*", MatchCasing.CaseInsensitive, first: true).Should().NotBeNull();

        FluentActions.Invoking(() => tempDir.Info.FindFile("nothing/ABC", MatchCasing.CaseSensitive)).Should().Throw<Exception>();
    }

    [TestMethod]
    public void FindPathFile()
    {
        var tempDir = new TempDir();
        tempDir.Info.RelativeFile("abc").Touch();
        tempDir.Info.RelativeFile("def").Touch();
        tempDir.Info.RelativeFile("XXX/abc").Touch();
        tempDir.Info.RelativeFile("XXX/def").Touch();
        tempDir.Info.RelativeFile("YYY/abc").Touch();
        tempDir.Info.RelativeFile("YYY/def").Touch();

        tempDir.Info.FindPathFile(["ABC"], MatchCasing.CaseSensitive).Should().BeNull();
        tempDir.Info.FindPathFile(["ABC"], MatchCasing.CaseInsensitive).Should().NotBeNull();
        tempDir.Info.FindPathFile(["abc"], MatchCasing.CaseSensitive).Should().NotBeNull();
        tempDir.Info.FindPathFile(["abc"], MatchCasing.CaseInsensitive).Should().NotBeNull();
        tempDir.Info.FindPathFile(["ZZZ"], MatchCasing.CaseInsensitive).Should().BeNull();

        tempDir.Info.FindPathFile(["XXX", "ABC"], MatchCasing.CaseSensitive).Should().BeNull();
        tempDir.Info.FindPathFile(["XXX", "ABC"], MatchCasing.CaseInsensitive).Should().NotBeNull();
        tempDir.Info.FindPathFile(["XXX", "abc"], MatchCasing.CaseSensitive).Should().NotBeNull();
        tempDir.Info.FindPathFile(["XXX", "abc"], MatchCasing.CaseInsensitive).Should().NotBeNull();

        tempDir.Info.FindPathFile(["yyy", "abc"], MatchCasing.CaseSensitive).Should().BeNull();
        tempDir.Info.FindPathFile(["yyy", "abc"], MatchCasing.CaseInsensitive).Should().NotBeNull();

        tempDir.Info.FindPathFile(["yyy", "abc"], fuzzy: false).Should().BeNull();
        tempDir.Info.FindPathFile(["yyy", "abc"], fuzzy: true).Should().NotBeNull();
    }

    [TestMethod]
    public void FindDirectory()
    {
        var tempDir = new TempDir();
        tempDir.Info.RelativeFile("abc/file").Touch();
        tempDir.Info.RelativeFile("def/file").Touch();
        tempDir.Info.RelativeFile("XXX/abc/file").Touch();
        tempDir.Info.RelativeFile("XXX/def/file").Touch();
        tempDir.Info.RelativeFile("YYY/abc/file").Touch();
        tempDir.Info.RelativeFile("YYY/def/file").Touch();

        tempDir.Info.FindDirectory("ABC", MatchCasing.CaseSensitive).Should().BeNull();
        tempDir.Info.FindDirectory("ABC", MatchCasing.CaseInsensitive).Should().NotBeNull();
        tempDir.Info.FindDirectory("abc", MatchCasing.CaseSensitive).Should().NotBeNull();
        tempDir.Info.FindDirectory("abc", MatchCasing.CaseInsensitive).Should().NotBeNull();
        tempDir.Info.FindDirectory("ZZZ", MatchCasing.CaseSensitive).Should().BeNull();

        // 途中のディレクトリに対するCasingは制御できないようなので、影響のないようにしておく。
        tempDir.Info.FindDirectory("XXX/ABC", MatchCasing.CaseSensitive).Should().BeNull();
        tempDir.Info.FindDirectory("XXX/ABC", MatchCasing.CaseInsensitive).Should().NotBeNull();
        tempDir.Info.FindDirectory("XXX/abc", MatchCasing.CaseSensitive).Should().NotBeNull();
        tempDir.Info.FindDirectory("XXX/abc", MatchCasing.CaseInsensitive).Should().NotBeNull();

        tempDir.Info.FindDirectory("ab?", MatchCasing.CaseSensitive).Should().NotBeNull();
        tempDir.Info.FindDirectory("ab*", MatchCasing.CaseInsensitive).Should().NotBeNull();
        tempDir.Info.FindDirectory("*", MatchCasing.CaseInsensitive).Should().BeNull();
        tempDir.Info.FindDirectory("*", MatchCasing.CaseInsensitive, first: true).Should().NotBeNull();

        FluentActions.Invoking(() => tempDir.Info.FindDirectory("nothing/ABC", MatchCasing.CaseSensitive)).Should().Throw<Exception>();
    }

    [TestMethod]
    public void FindPathDirectory()
    {
        var tempDir = new TempDir();
        tempDir.Info.RelativeFile("abc/file").Touch();
        tempDir.Info.RelativeFile("def/file").Touch();
        tempDir.Info.RelativeFile("XXX/abc/file").Touch();
        tempDir.Info.RelativeFile("XXX/def/file").Touch();
        tempDir.Info.RelativeFile("YYY/abc/file").Touch();
        tempDir.Info.RelativeFile("YYY/def/file").Touch();

        tempDir.Info.FindPathDirectory(["ABC"], MatchCasing.CaseSensitive).Should().BeNull();
        tempDir.Info.FindPathDirectory(["ABC"], MatchCasing.CaseInsensitive).Should().NotBeNull();
        tempDir.Info.FindPathDirectory(["abc"], MatchCasing.CaseSensitive).Should().NotBeNull();
        tempDir.Info.FindPathDirectory(["abc"], MatchCasing.CaseInsensitive).Should().NotBeNull();
        tempDir.Info.FindPathDirectory(["ZZZ"], MatchCasing.CaseSensitive).Should().BeNull();

        tempDir.Info.FindPathDirectory(["XXX", "ABC"], MatchCasing.CaseSensitive).Should().BeNull();
        tempDir.Info.FindPathDirectory(["XXX", "ABC"], MatchCasing.CaseInsensitive).Should().NotBeNull();
        tempDir.Info.FindPathDirectory(["XXX", "abc"], MatchCasing.CaseSensitive).Should().NotBeNull();
        tempDir.Info.FindPathDirectory(["XXX", "abc"], MatchCasing.CaseInsensitive).Should().NotBeNull();

        tempDir.Info.FindPathDirectory(["yyy", "abc"], MatchCasing.CaseSensitive).Should().BeNull();
        tempDir.Info.FindPathDirectory(["yyy", "abc"], MatchCasing.CaseInsensitive).Should().NotBeNull();

        tempDir.Info.FindPathDirectory(["yyy", "abc"], fuzzy: false).Should().BeNull();
        tempDir.Info.FindPathDirectory(["yyy", "abc"], fuzzy: true).Should().NotBeNull();
    }

    [TestMethod]
    public void FindAncestor()
    {
        using var testDir = new TempDir();

        var dir = testDir.Info.RelativeDirectory("abc/def/ghi/jkl").WithCreate();
        var found = dir.FindAncestor("def");
        found.Should().NotBeNull();
        found.RelativePathFrom(testDir.Info).Replace('\\', '/').Should().Be("abc/def");
    }
    #endregion

    #region Path
    [TestMethod]
    public void GetPathSegments()
    {
        new DirectoryInfo(@"c:/abc/def/zxc/asd/qwe.txt").GetPathSegments()
            .Should().Equal(new[] { @"c:\", "abc", "def", "zxc", "asd", "qwe.txt", });

        new DirectoryInfo(@"c:/abc/../asd/qwe.txt").GetPathSegments()
            .Should().Equal(new[] { @"c:\", "asd", "qwe.txt", });

        new DirectoryInfo(@"/abc/def/qwe.txt").GetPathSegments()
            .Should().HaveElementAt(0, Path.GetPathRoot(Environment.CurrentDirectory))
            .And.Subject.Skip(1).Should().Equal(new[] { "abc", "def", "qwe.txt", });

        new DirectoryInfo(@"\\aaa\bbb\ccc\ddd").GetPathSegments()
            .Should().Equal(new[] { @"\\aaa\bbb", "ccc", "ddd", });
    }

    [TestMethod]
    public void IsDescendantOf()
    {
        new DirectoryInfo(@"c:/abc/def/zxc/asd")
            .IsDescendantOf(new DirectoryInfo(@"c:/abc/def/zxc"), false)
            .Should().BeTrue();

        new DirectoryInfo(@"c:/abc/def/zxc/asd")
            .IsDescendantOf(new DirectoryInfo(@"c:/abc/def"), false)
            .Should().BeTrue();

        new DirectoryInfo(@"c:/abc/def/zxc/asd")
            .IsDescendantOf(new DirectoryInfo(@"c:\"), false)
            .Should().BeTrue();

        new DirectoryInfo(@"c:/abc/def/zxc/asd")
            .IsDescendantOf(new DirectoryInfo(@"C:/ABC/DEF/ZXC"), false)
            .Should().BeTrue();

        new DirectoryInfo(@"c:/abc/def/zxc/asd")
            .IsDescendantOf(new DirectoryInfo(@"c:/abc/def/zxc/asd/qwe"), false)
            .Should().BeFalse();

        new DirectoryInfo(@"c:/abc/def/zxc/asd")
            .IsDescendantOf(new DirectoryInfo(@"c:/abc/def/zxc/asd"), false)
            .Should().BeFalse();

        new DirectoryInfo(@"c:/abc/def/zxc/asd")
            .IsDescendantOf(new DirectoryInfo(@"c:/abc/def/zxc/asd"), true)
            .Should().BeTrue();
    }

    [TestMethod]
    public void IsAncestorOf()
    {
        new DirectoryInfo(@"c:/abc/def/zxc")
            .IsAncestorOf(new DirectoryInfo(@"c:/abc/def/zxc/asd"), false)
            .Should().BeTrue();

        new DirectoryInfo(@"c:/abc/def")
            .IsAncestorOf(new DirectoryInfo(@"c:/abc/def/zxc/asd"), false)
            .Should().BeTrue();

        new DirectoryInfo(@"c:\")
            .IsAncestorOf(new DirectoryInfo(@"c:/abc/def/zxc/asd"), false)
            .Should().BeTrue();

        new DirectoryInfo(@"C:/ABC/DEF/ZXC")
            .IsAncestorOf(new DirectoryInfo(@"c:/abc/def/zxc/asd"), false)
            .Should().BeTrue();

        new DirectoryInfo(@"c:/abc/def/zxc/asd/qwe")
            .IsAncestorOf(new DirectoryInfo(@"c:/abc/def/zxc/asd"), false)
            .Should().BeFalse();

        new DirectoryInfo(@"c:/abc/def/zxc/asd")
            .IsAncestorOf(new DirectoryInfo(@"c:/abc/def/zxc/asd"), false)
            .Should().BeFalse();

        new DirectoryInfo(@"c:/abc/def/zxc/asd")
            .IsAncestorOf(new DirectoryInfo(@"c:/abc/def/zxc/asd"), true)
            .Should().BeTrue();
    }

    [TestMethod]
    public void RelativePathFrom()
    {
        new DirectoryInfo(@"c:/abc/def/ghi/asd/qwe").RelativePathFrom(new DirectoryInfo(@"c:/abc/def/ghi/"), ignoreCase: true)
            .Should().Be(@"asd\qwe");

        new DirectoryInfo(@"c:/abc/def/ghi/qwe").RelativePathFrom(new DirectoryInfo(@"c:/abc/def/ghi/"), ignoreCase: true)
            .Should().Be(@"qwe");

        new DirectoryInfo(@"c:/abc/def/zxc/asd/qwe").RelativePathFrom(new DirectoryInfo(@"c:/abc/def/ghi/"), ignoreCase: true)
            .Should().Be(@"..\zxc\asd\qwe");

        new DirectoryInfo(@"D:/abc/def/zxc/asd/qwe").RelativePathFrom(new DirectoryInfo(@"c:/abc/def/ghi/"), ignoreCase: true)
            .Should().Be(@"D:\abc\def\zxc\asd\qwe");

        new DirectoryInfo(@"\\aaa\bbb\ccc").RelativePathFrom(new DirectoryInfo(@"c:/abc/def/ghi/"), ignoreCase: true)
            .Should().Be(@"\\aaa\bbb\ccc");

        new DirectoryInfo(@"\\aaa\bbb\ccc\eee\fff").RelativePathFrom(new DirectoryInfo(@"\\aaa\bbb\ccc\ddd"), ignoreCase: true)
            .Should().Be(@"..\eee\fff");

        new DirectoryInfo(@"\\aaa\ggg\ccc\eee\fff").RelativePathFrom(new DirectoryInfo(@"\\aaa\bbb\ccc\ddd"), ignoreCase: true)
            .Should().Be(@"\\aaa\ggg\ccc\eee\fff");
    }

    [TestMethod]
    public void RelativePathFrom_IgnoreCase()
    {
        new DirectoryInfo(@"c:/abc/def/ghi/asd/qwe").RelativePathFrom(new DirectoryInfo(@"c:/abc/def/ghi/"), ignoreCase: false)
            .Should().Be(@"asd\qwe");

        new DirectoryInfo(@"c:/abc/def/Ghi/asd/qwe").RelativePathFrom(new DirectoryInfo(@"c:/abc/def/ghi/"), ignoreCase: false)
            .Should().Be(@"..\Ghi\asd\qwe");
    }
    #endregion

    #region Search
    [TestMethod]
    public async Task VisitFiles_TopOnly()
    {
        // 再帰フラグOFFでのファイル列挙処理

        var testFiles = new[]
        {
            @"abc/aaa.txt",
            @"def/ghi/bbb.txt",
            @"ccc.txt",
            @"abc/ddd.txt",
            @"eee.txt",
            @"abc/fff.txt",
            @"asd/qwe/ggg.txt",
            @"hhh.txt",
        };

        using var testDir = new TempDir();
        foreach (var path in testFiles)
        {
            await testDir.Info.RelativeFile(path).WithDirectoryCreate().WriteAllTextAsync(path);
        }

        var testOpt = new VisitFilesOptions
        {
            Recurse = false,
            Buffered = false,
            Sort = false,
        };

        static string selector(FileInfo file) => file.ReadAllText();
        var converter = new VisitFilesWalker<string>(context => context.SetResult(selector(context.File!)));

        var testExpects = testDir.Info.EnumerateFiles("*", SearchOption.TopDirectoryOnly).Select(selector);

        testDir.Info.VisitFiles(converter, options: testOpt).Should().BeEquivalentTo(testExpects);
    }

    [TestMethod]
    public async Task VisitFiles_Recurse()
    {
        // 再帰フラグONでのファイル列挙処理

        var testFiles = new[]
        {
            @"abc/aaa.txt",
            @"def/ghi/bbb.txt",
            @"ccc.txt",
            @"abc/ddd.txt",
            @"eee.txt",
            @"abc/fff.txt",
            @"asd/qwe/ggg.txt",
            @"hhh.txt",
        };

        using var testDir = new TempDir();
        foreach (var path in testFiles)
        {
            await testDir.Info.RelativeFile(path).WithDirectoryCreate().WriteAllTextAsync(path);
        }

        var testOpt = new VisitFilesOptions
        {
            Recurse = true,
            Buffered = false,
            Sort = false,
        };

        static string selector(FileInfo file) => file.ReadAllText();
        var converter = new VisitFilesWalker<string>(context =>
        {
            if (context.File == null) context.Item.Should().BeOfType<DirectoryInfo>().And.Be(context.Directory);
            else context.Item.Should().BeOfType<FileInfo>().And.Be(context.File);

            context.SetResult(selector(context.File!));
        });
        var testExpects = testDir.Info.EnumerateFiles("*", SearchOption.AllDirectories).Select(selector);

        testDir.Info.VisitFiles(converter, options: testOpt).Should().BeEquivalentTo(testExpects);
    }

    [TestMethod]
    public async Task VisitFiles_Handling_OnlyFile()
    {
        var testFiles = new[]
        {
            @"aaa.txt",
            @"bbb.txt",
            @"ccc.txt",
            @"abc/ddd.txt",
            @"abc/eee.txt",
            @"abc/fff.txt",
            @"abc/def/ggg.txt",
            @"abc/def/hhh.txt",
            @"ghi/iii.txt",
            @"ghi/jjj.txt",
        };

        using var testDir = new TempDir();
        foreach (var path in testFiles)
        {
            await testDir.Info.RelativeFile(path).WithDirectoryCreate().WriteAllTextAsync(path);
        }

        var testOpt = new VisitFilesOptions
        {
            Recurse = false,
            Handling = new(File: true, Directory: false),
            Buffered = false,
            Sort = false,
        };

        var converter = new VisitFilesWalker<string>(context =>
        {
            if (context.File == null)
            {
                context.SetResult("Unexpected");
            }
            else
            {
                var relPath = context.Item.RelativePathFrom(testDir.Info, ignoreCase: true).Replace('\\', '/');
                context.SetResult(relPath);
            }
        });

        testDir.Info.VisitFiles(converter, options: testOpt).Should().BeEquivalentTo(new[]
        {
            @"aaa.txt",
            @"bbb.txt",
            @"ccc.txt",
        });
    }

    [TestMethod]
    public async Task VisitFiles_Handling_OnlyDirectory()
    {
        var testFiles = new[]
        {
            @"aaa.txt",
            @"bbb.txt",
            @"ccc.txt",
            @"abc/ddd.txt",
            @"abc/eee.txt",
            @"abc/fff.txt",
            @"abc/def/ggg.txt",
            @"abc/def/hhh.txt",
            @"ghi/iii.txt",
            @"ghi/jjj.txt",
        };

        using var testDir = new TempDir();
        foreach (var path in testFiles)
        {
            await testDir.Info.RelativeFile(path).WithDirectoryCreate().WriteAllTextAsync(path);
        }

        var testOpt = new VisitFilesOptions
        {
            Recurse = false,
            Handling = new(File: false, Directory: true),
            Buffered = false,
            Sort = false,
        };

        var converter = new VisitFilesWalker<string>(context =>
        {
            if (context.File == null)
            {
                var relPath = context.Item.RelativePathFrom(testDir.Info, ignoreCase: true).Replace('\\', '/');
                context.SetResult(relPath);
            }
            else
            {
                context.SetResult("Unexpected");
            }
        });

        testDir.Info.VisitFiles(converter, options: testOpt).Should().BeEquivalentTo(new[]
        {
            @"abc",
            @"ghi",
        });
    }

    [TestMethod]
    public async Task VisitFiles_Handling_FileAndDirectory()
    {
        var testFiles = new[]
        {
            @"aaa.txt",
            @"bbb.txt",
            @"ccc.txt",
            @"abc/ddd.txt",
            @"abc/eee.txt",
            @"abc/fff.txt",
            @"abc/def/ggg.txt",
            @"abc/def/hhh.txt",
            @"ghi/iii.txt",
            @"ghi/jjj.txt",
        };

        using var testDir = new TempDir();
        foreach (var path in testFiles)
        {
            await testDir.Info.RelativeFile(path).WithDirectoryCreate().WriteAllTextAsync(path);
        }

        var testOpt = new VisitFilesOptions
        {
            Recurse = false,
            Handling = new(File: true, Directory: true),
            Buffered = false,
            Sort = false,
        };

        var converter = new VisitFilesWalker<string>(context =>
        {
            var relPath = context.Item.RelativePathFrom(testDir.Info, ignoreCase: true).Replace('\\', '/');
            context.SetResult(relPath);
        });

        testDir.Info.VisitFiles(converter, options: testOpt).Should().BeEquivalentTo(new[]
        {
            @"aaa.txt",
            @"bbb.txt",
            @"ccc.txt",
            @"abc",
            @"ghi",
        });
    }

    [TestMethod]
    public async Task VisitFiles_Handling_Recurse_OnlyFile()
    {
        var testFiles = new[]
        {
            @"aaa.txt",
            @"bbb.txt",
            @"ccc.txt",
            @"abc/ddd.txt",
            @"abc/eee.txt",
            @"abc/fff.txt",
            @"abc/def/ggg.txt",
            @"abc/def/hhh.txt",
            @"ghi/iii.txt",
            @"ghi/jjj.txt",
        };

        using var testDir = new TempDir();
        foreach (var path in testFiles)
        {
            await testDir.Info.RelativeFile(path).WithDirectoryCreate().WriteAllTextAsync(path);
        }

        var testOpt = new VisitFilesOptions
        {
            Recurse = true,
            Handling = new(File: true, Directory: false),
            Buffered = false,
            Sort = false,
        };

        var converter = new VisitFilesWalker<string>(context =>
        {
            if (context.File == null)
            {
                context.SetResult("Unexpected");
            }
            else
            {
                var relPath = context.Item.RelativePathFrom(testDir.Info, ignoreCase: true).Replace('\\', '/');
                context.SetResult(relPath);
            }
        });

        testDir.Info.VisitFiles(converter, options: testOpt).Should().BeEquivalentTo(new[]
        {
            @"aaa.txt",
            @"bbb.txt",
            @"ccc.txt",
            @"abc/ddd.txt",
            @"abc/eee.txt",
            @"abc/fff.txt",
            @"abc/def/ggg.txt",
            @"abc/def/hhh.txt",
            @"ghi/iii.txt",
            @"ghi/jjj.txt",
        });
    }

    [TestMethod]
    public async Task VisitFiles_Handling_Recurse_OnlyDirectory()
    {
        var testFiles = new[]
        {
            @"aaa.txt",
            @"bbb.txt",
            @"ccc.txt",
            @"abc/ddd.txt",
            @"abc/eee.txt",
            @"abc/fff.txt",
            @"abc/def/ggg.txt",
            @"abc/def/hhh.txt",
            @"ghi/iii.txt",
            @"ghi/jjj.txt",
        };

        using var testDir = new TempDir();
        foreach (var path in testFiles)
        {
            await testDir.Info.RelativeFile(path).WithDirectoryCreate().WriteAllTextAsync(path);
        }

        var testOpt = new VisitFilesOptions
        {
            Recurse = true,
            Handling = new(File: false, Directory: true),
            Buffered = false,
            Sort = false,
        };

        var converter = new VisitFilesWalker<string>(context =>
        {
            if (context.File == null)
            {
                var relPath = context.Item.RelativePathFrom(testDir.Info, ignoreCase: true).Replace('\\', '/');
                context.SetResult(relPath);
            }
            else
            {
                context.SetResult("Unexpected");
            }
        });

        testDir.Info.VisitFiles(converter, options: testOpt).Should().BeEquivalentTo(new[]
        {
            @"abc",
            @"abc/def",
            @"ghi",
        });
    }

    [TestMethod]
    public async Task VisitFiles_Handling_Recurse_FileAndDirectory()
    {
        var testFiles = new[]
        {
            @"aaa.txt",
            @"bbb.txt",
            @"ccc.txt",
            @"abc/ddd.txt",
            @"abc/eee.txt",
            @"abc/fff.txt",
            @"abc/def/ggg.txt",
            @"abc/def/hhh.txt",
            @"ghi/iii.txt",
            @"ghi/jjj.txt",
        };

        using var testDir = new TempDir();
        foreach (var path in testFiles)
        {
            await testDir.Info.RelativeFile(path).WithDirectoryCreate().WriteAllTextAsync(path);
        }

        var testOpt = new VisitFilesOptions
        {
            Recurse = true,
            Handling = new(File: true, Directory: true),
            Buffered = false,
            Sort = false,
        };

        var converter = new VisitFilesWalker<string>(context =>
        {
            var relPath = context.Item.RelativePathFrom(testDir.Info, ignoreCase: true).Replace('\\', '/');
            context.SetResult(relPath);
        });

        testDir.Info.VisitFiles(converter, options: testOpt).Should().BeEquivalentTo(new[]
        {
            @"aaa.txt",
            @"bbb.txt",
            @"ccc.txt",
            @"abc",
            @"abc/ddd.txt",
            @"abc/eee.txt",
            @"abc/fff.txt",
            @"abc/def",
            @"abc/def/ggg.txt",
            @"abc/def/hhh.txt",
            @"ghi",
            @"ghi/iii.txt",
            @"ghi/jjj.txt",
        });
    }

    [TestMethod]
    public async Task VisitFiles_FileFilter()
    {
        // Walkerデリゲートで条件付けされた変換結果の設定

        var testFiles = new[]
        {
            @"abc/aaa.txt",
            @"def/ghi/bbb.png",
            @"ccc.jpg",
            @"abc/ddd.txt",
            @"eee.png",
            @"abc/fff.jpg",
            @"asd/qwe/ggg.txt",
            @"hhh.txt",
        };

        using var testDir = new TempDir();
        foreach (var path in testFiles)
        {
            await testDir.Info.RelativeFile(path).WithDirectoryCreate().WriteAllTextAsync(path);
        }

        static string selector(FileInfo file) => file.ReadAllText();
        static bool filter(FileSystemInfo item) => item is DirectoryInfo || item.Name.Contains(".txt");
        var converter = new VisitFilesWalker<string>(context => context.SetResult(selector(context.File!)));

        var testOpt = new VisitFilesOptions
        {
            Recurse = true,
            Buffered = false,
            Sort = false,
            Handling = VisitFilesHandling.OnlyFile,
            FileFilter = filter,
        };

        var testExpects = testDir.Info.EnumerateFiles("*", SearchOption.AllDirectories)
            .Where(f => filter(f)).Select(selector);

        testDir.Info.VisitFiles(converter, options: testOpt).Should().BeEquivalentTo(testExpects);
    }

    [TestMethod]
    public async Task VisitFiles_DirectoryFilter()
    {
        // ディレクトリをWalkerハンドラ呼び出しにし、一部のディレクトリ列挙をスキップする

        var testFiles = new[]
        {
            @"abc/aaa.txt",
            @"def/ghi/bbb.png",
            @"ccc.jpg",
            @"abc/ddd.txt",
            @"eee.png",
            @"abc/fff.jpg",
            @"asd/qwe/ggg.txt",
            @"hhh.txt",
        };

        using var testDir = new TempDir();
        foreach (var path in testFiles)
        {
            await testDir.Info.RelativeFile(path).WithDirectoryCreate().WriteAllTextAsync(path);
        }

        var testOpt = new VisitFilesOptions
        {
            Recurse = true,
            Handling = VisitFilesHandling.All,
            Buffered = false,
            Sort = false,
            DirectoryFilter = dir => dir.Name != "abc",
        };

        var converter = new VisitFilesWalker<string>(context =>
        {
            if (context.File != null) context.SetResult(context.File.ReadAllText());
        });

        testDir.Info.VisitFiles(converter, options: testOpt).Should().BeEquivalentTo(new[]
        {
            @"def/ghi/bbb.png",
            @"ccc.jpg",
            @"eee.png",
            @"asd/qwe/ggg.txt",
            @"hhh.txt",
        });
    }

    [TestMethod]
    public async Task SelectFiles_Selector()
    {
        var testFiles = new[]
        {
            @"abc/aaa.txt",
            @"def/ghi/bbb.png",
            @"ccc.jpg",
            @"abc/ddd.txt",
            @"eee.png",
            @"abc/fff.jpg",
            @"asd/qwe/ggg.txt",
            @"hhh.txt",
        };

        using var testDir = new TempDir();
        foreach (var path in testFiles)
        {
            await testDir.Info.RelativeFile(path).WithDirectoryCreate().WriteAllTextAsync(path);
        }

        var testOpt = new VisitFilesOptions
        {
            Recurse = true,
            Buffered = false,
            Sort = false,
        };

        static string selector(FileInfo file) => file.ReadAllText();

        var testExpects = testDir.Info.EnumerateFiles("*", SearchOption.AllDirectories)
            .Select(selector);

        testDir.Info.SelectFiles(w => selector(w.File!), options: testOpt).Should().BeEquivalentTo(testExpects);
    }

    [TestMethod]
    public async Task VisitFilesAsync_TopOnly()
    {
        var testFiles = new[]
        {
            @"abc/aaa.txt",
            @"def/ghi/bbb.txt",
            @"ccc.txt",
            @"abc/ddd.txt",
            @"eee.txt",
            @"abc/fff.txt",
            @"asd/qwe/ggg.txt",
            @"hhh.txt",
        };

        using var testDir = new TempDir();
        foreach (var path in testFiles)
        {
            await testDir.Info.RelativeFile(path).WithDirectoryCreate().WriteAllTextAsync(path);
        }

        var testOpt = new VisitFilesOptions
        {
            Recurse = false,
            Buffered = false,
            Sort = false,
        };

        var converter = new AsyncVisitFilesWalker<string>(async context =>
        {
            context.SetResult(await context.File!.ReadAllTextAsync());
        });

        var testExpects = testDir.Info.EnumerateFiles("*", SearchOption.TopDirectoryOnly)
            .Select(f => f.ReadAllText());

        (await testDir.Info.VisitFilesAsync(converter, options: testOpt).ToArrayAsync())
            .Should().BeEquivalentTo(testExpects);
    }

    [TestMethod]
    public async Task VisitFilesAsync_Recurse()
    {
        // 再帰フラグONでのファイル列挙処理

        var testFiles = new[]
        {
            @"abc/aaa.txt",
            @"def/ghi/bbb.txt",
            @"ccc.txt",
            @"abc/ddd.txt",
            @"eee.txt",
            @"abc/fff.txt",
            @"asd/qwe/ggg.txt",
            @"hhh.txt",
        };

        using var testDir = new TempDir();
        foreach (var path in testFiles)
        {
            await testDir.Info.RelativeFile(path).WithDirectoryCreate().WriteAllTextAsync(path);
        }

        var testOpt = new VisitFilesOptions
        {
            Recurse = true,
            Buffered = false,
            Sort = false,
        };

        var converter = new AsyncVisitFilesWalker<string>(async context =>
        {
            if (context.File == null) context.Item.Should().BeOfType<DirectoryInfo>().And.Be(context.Directory);
            else context.Item.Should().BeOfType<FileInfo>().And.Be(context.File);

            context.SetResult(await context.File!.ReadAllTextAsync());
        });

        var testExpects = testDir.Info.EnumerateFiles("*", SearchOption.AllDirectories)
            .Select(f => f.ReadAllText());

        (await testDir.Info.VisitFilesAsync(converter, options: testOpt).ToArrayAsync())
            .Should().BeEquivalentTo(testExpects);
    }

    [TestMethod]
    public async Task VisitFilesAsync_Handling_OnlyFile()
    {
        var testFiles = new[]
        {
            @"aaa.txt",
            @"bbb.txt",
            @"ccc.txt",
            @"abc/ddd.txt",
            @"abc/eee.txt",
            @"abc/fff.txt",
            @"abc/def/ggg.txt",
            @"abc/def/hhh.txt",
            @"ghi/iii.txt",
            @"ghi/jjj.txt",
        };

        using var testDir = new TempDir();
        foreach (var path in testFiles)
        {
            await testDir.Info.RelativeFile(path).WithDirectoryCreate().WriteAllTextAsync(path);
        }

        var testOpt = new VisitFilesOptions
        {
            Recurse = false,
            Handling = new(File: true, Directory: false),
            Buffered = false,
            Sort = false,
        };

        var converter = new AsyncVisitFilesWalker<string>(async context =>
        {
            await Task.CompletedTask;
            if (context.File == null)
            {
                context.SetResult("Unexpected");
            }
            else
            {
                var relPath = context.Item.RelativePathFrom(testDir.Info, ignoreCase: true).Replace('\\', '/');
                context.SetResult(relPath);
            }
        });

        (await testDir.Info.VisitFilesAsync(converter, options: testOpt).ToArrayAsync()).Should().BeEquivalentTo(new[]
        {
            @"aaa.txt",
            @"bbb.txt",
            @"ccc.txt",
        });
    }

    [TestMethod]
    public async Task VisitFilesAsync_Handling_OnlyDirectory()
    {
        var testFiles = new[]
        {
            @"aaa.txt",
            @"bbb.txt",
            @"ccc.txt",
            @"abc/ddd.txt",
            @"abc/eee.txt",
            @"abc/fff.txt",
            @"abc/def/ggg.txt",
            @"abc/def/hhh.txt",
            @"ghi/iii.txt",
            @"ghi/jjj.txt",
        };

        using var testDir = new TempDir();
        foreach (var path in testFiles)
        {
            await testDir.Info.RelativeFile(path).WithDirectoryCreate().WriteAllTextAsync(path);
        }

        var testOpt = new VisitFilesOptions
        {
            Recurse = false,
            Handling = new(File: false, Directory: true),
            Buffered = false,
            Sort = false,
        };

        var converter = new AsyncVisitFilesWalker<string>(async context =>
        {
            await Task.CompletedTask;
            if (context.File == null)
            {
                var relPath = context.Item.RelativePathFrom(testDir.Info, ignoreCase: true).Replace('\\', '/');
                context.SetResult(relPath);
            }
            else
            {
                context.SetResult("Unexpected");
            }
        });

        (await testDir.Info.VisitFilesAsync(converter, options: testOpt).ToArrayAsync()).Should().BeEquivalentTo(new[]
        {
            @"abc",
            @"ghi",
        });
    }

    [TestMethod]
    public async Task VisitFilesAsync_Handling_FileAndDirectory()
    {
        var testFiles = new[]
        {
            @"aaa.txt",
            @"bbb.txt",
            @"ccc.txt",
            @"abc/ddd.txt",
            @"abc/eee.txt",
            @"abc/fff.txt",
            @"abc/def/ggg.txt",
            @"abc/def/hhh.txt",
            @"ghi/iii.txt",
            @"ghi/jjj.txt",
        };

        using var testDir = new TempDir();
        foreach (var path in testFiles)
        {
            await testDir.Info.RelativeFile(path).WithDirectoryCreate().WriteAllTextAsync(path);
        }

        var testOpt = new VisitFilesOptions
        {
            Recurse = false,
            Handling = new(File: true, Directory: true),
            Buffered = false,
            Sort = false,
        };

        var converter = new AsyncVisitFilesWalker<string>(async context =>
        {
            await Task.CompletedTask;
            var relPath = context.Item.RelativePathFrom(testDir.Info, ignoreCase: true).Replace('\\', '/');
            context.SetResult(relPath);
        });

        (await testDir.Info.VisitFilesAsync(converter, options: testOpt).ToArrayAsync()).Should().BeEquivalentTo(new[]
        {
            @"aaa.txt",
            @"bbb.txt",
            @"ccc.txt",
            @"abc",
            @"ghi",
        });
    }

    [TestMethod]
    public async Task VisitFilesAsync_Handling_Recurse_OnlyFile()
    {
        var testFiles = new[]
        {
            @"aaa.txt",
            @"bbb.txt",
            @"ccc.txt",
            @"abc/ddd.txt",
            @"abc/eee.txt",
            @"abc/fff.txt",
            @"abc/def/ggg.txt",
            @"abc/def/hhh.txt",
            @"ghi/iii.txt",
            @"ghi/jjj.txt",
        };

        using var testDir = new TempDir();
        foreach (var path in testFiles)
        {
            await testDir.Info.RelativeFile(path).WithDirectoryCreate().WriteAllTextAsync(path);
        }

        var testOpt = new VisitFilesOptions
        {
            Recurse = true,
            Handling = new(File: true, Directory: false),
            Buffered = false,
            Sort = false,
        };

        var converter = new AsyncVisitFilesWalker<string>(async context =>
        {
            await Task.CompletedTask;
            if (context.File == null)
            {
                context.SetResult("Unexpected");
            }
            else
            {
                var relPath = context.Item.RelativePathFrom(testDir.Info, ignoreCase: true).Replace('\\', '/');
                context.SetResult(relPath);
            }
        });

        (await testDir.Info.VisitFilesAsync(converter, options: testOpt).ToArrayAsync()).Should().BeEquivalentTo(new[]
        {
            @"aaa.txt",
            @"bbb.txt",
            @"ccc.txt",
            @"abc/ddd.txt",
            @"abc/eee.txt",
            @"abc/fff.txt",
            @"abc/def/ggg.txt",
            @"abc/def/hhh.txt",
            @"ghi/iii.txt",
            @"ghi/jjj.txt",
        });
    }

    [TestMethod]
    public async Task VisitFilesAsync_Handling_Recurse_OnlyDirectory()
    {
        var testFiles = new[]
        {
            @"aaa.txt",
            @"bbb.txt",
            @"ccc.txt",
            @"abc/ddd.txt",
            @"abc/eee.txt",
            @"abc/fff.txt",
            @"abc/def/ggg.txt",
            @"abc/def/hhh.txt",
            @"ghi/iii.txt",
            @"ghi/jjj.txt",
        };

        using var testDir = new TempDir();
        foreach (var path in testFiles)
        {
            await testDir.Info.RelativeFile(path).WithDirectoryCreate().WriteAllTextAsync(path);
        }

        var testOpt = new VisitFilesOptions
        {
            Recurse = true,
            Handling = new(File: false, Directory: true),
            Buffered = false,
            Sort = false,
        };

        var converter = new AsyncVisitFilesWalker<string>(async context =>
        {
            await Task.CompletedTask;
            if (context.File == null)
            {
                var relPath = context.Item.RelativePathFrom(testDir.Info, ignoreCase: true).Replace('\\', '/');
                context.SetResult(relPath);
            }
            else
            {
                context.SetResult("Unexpected");
            }
        });

        (await testDir.Info.VisitFilesAsync(converter, options: testOpt).ToArrayAsync()).Should().BeEquivalentTo(new[]
        {
            @"abc",
            @"abc/def",
            @"ghi",
        });
    }

    [TestMethod]
    public async Task VisitFilesAsync_Handling_Recurse_FileAndDirectory()
    {
        var testFiles = new[]
        {
            @"aaa.txt",
            @"bbb.txt",
            @"ccc.txt",
            @"abc/ddd.txt",
            @"abc/eee.txt",
            @"abc/fff.txt",
            @"abc/def/ggg.txt",
            @"abc/def/hhh.txt",
            @"ghi/iii.txt",
            @"ghi/jjj.txt",
        };

        using var testDir = new TempDir();
        foreach (var path in testFiles)
        {
            await testDir.Info.RelativeFile(path).WithDirectoryCreate().WriteAllTextAsync(path);
        }

        var testOpt = new VisitFilesOptions
        {
            Recurse = true,
            Handling = new(File: true, Directory: true),
            Buffered = false,
            Sort = false,
        };

        var converter = new AsyncVisitFilesWalker<string>(async context =>
        {
            await Task.CompletedTask;
            var relPath = context.Item.RelativePathFrom(testDir.Info, ignoreCase: true).Replace('\\', '/');
            context.SetResult(relPath);
        });

        (await testDir.Info.VisitFilesAsync(converter, options: testOpt).ToArrayAsync()).Should().BeEquivalentTo(new[]
        {
            @"aaa.txt",
            @"bbb.txt",
            @"ccc.txt",
            @"abc",
            @"abc/ddd.txt",
            @"abc/eee.txt",
            @"abc/fff.txt",
            @"abc/def",
            @"abc/def/ggg.txt",
            @"abc/def/hhh.txt",
            @"ghi",
            @"ghi/iii.txt",
            @"ghi/jjj.txt",
        });
    }

    [TestMethod]
    public async Task VisitFilesAsync_FileFilter()
    {
        // Walkerデリゲートで条件付けされた変換結果の設定

        var testFiles = new[]
        {
            @"abc/aaa.txt",
            @"def/ghi/bbb.png",
            @"ccc.jpg",
            @"abc/ddd.txt",
            @"eee.png",
            @"abc/fff.jpg",
            @"asd/qwe/ggg.txt",
            @"hhh.txt",
        };

        using var testDir = new TempDir();
        foreach (var path in testFiles)
        {
            await testDir.Info.RelativeFile(path).WithDirectoryCreate().WriteAllTextAsync(path);
        }

        static bool filter(FileSystemInfo item) => item is DirectoryInfo || item.Name.Contains(".txt");
        var converter = new AsyncVisitFilesWalker<string>(async context =>
        {
            context.SetResult(await context.File!.ReadAllTextAsync());
        });

        var testOpt = new VisitFilesOptions
        {
            Recurse = true,
            Buffered = false,
            Sort = false,
            Handling = VisitFilesHandling.OnlyFile,
            FileFilter = filter,
        };

        var testExpects = testDir.Info.EnumerateFiles("*", SearchOption.AllDirectories)
            .Where(f => filter(f)).Select(f => f.ReadAllText());

        (await testDir.Info.VisitFilesAsync(converter, options: testOpt).ToArrayAsync())
            .Should().BeEquivalentTo(testExpects);
    }

    [TestMethod]
    public async Task VisitFilesAsync_DirectoryFilter()
    {
        // ディレクトリをWalkerハンドラ呼び出しにし、一部のディレクトリ列挙をスキップする

        var testFiles = new[]
        {
            @"abc/aaa.txt",
            @"def/ghi/bbb.png",
            @"ccc.jpg",
            @"abc/ddd.txt",
            @"eee.png",
            @"abc/fff.jpg",
            @"asd/qwe/ggg.txt",
            @"hhh.txt",
        };

        using var testDir = new TempDir();
        foreach (var path in testFiles)
        {
            await testDir.Info.RelativeFile(path).WithDirectoryCreate().WriteAllTextAsync(path);
        }

        var testOpt = new VisitFilesOptions
        {
            Recurse = true,
            Handling = VisitFilesHandling.All,
            Buffered = false,
            Sort = false,
            DirectoryFilter = dir => dir.Name != "abc",
        };

        var converter = new AsyncVisitFilesWalker<string>(async context =>
        {
            if (context.File != null) context.SetResult(await context.File.ReadAllTextAsync());
        });

        (await testDir.Info.VisitFilesAsync(converter, options: testOpt).ToArrayAsync()).Should().BeEquivalentTo(new[]
        {
            @"def/ghi/bbb.png",
            @"ccc.jpg",
            @"eee.png",
            @"asd/qwe/ggg.txt",
            @"hhh.txt",
        });
    }

    [TestMethod]
    public async Task SelectFilesAsync_Selector()
    {
        var testFiles = new[]
        {
            @"abc/aaa.txt",
            @"def/ghi/bbb.png",
            @"ccc.jpg",
            @"abc/ddd.txt",
            @"eee.png",
            @"abc/fff.jpg",
            @"asd/qwe/ggg.txt",
            @"hhh.txt",
        };

        using var testDir = new TempDir();
        foreach (var path in testFiles)
        {
            await testDir.Info.RelativeFile(path).WithDirectoryCreate().WriteAllTextAsync(path);
        }

        var testOpt = new VisitFilesOptions
        {
            Recurse = true,
            Buffered = false,
            Sort = false,
        };

        var testExpects = testDir.Info.EnumerateFiles("*", SearchOption.AllDirectories)
            .Select(f => f.ReadAllText());

        (await testDir.Info.SelectFilesAsync(async w => await w.File!.ReadAllTextAsync(), options: testOpt).ToArrayAsync())
            .Should().BeEquivalentTo(testExpects);
    }

    [TestMethod]
    public async Task DoFiles_Action()
    {
        // 再帰フラグONでのファイル列挙処理

        var testFiles = new[]
        {
            @"abc/aaa.txt",
            @"def/ghi/bbb.txt",
            @"ccc.txt",
            @"abc/ddd.txt",
            @"eee.txt",
            @"abc/fff.txt",
            @"asd/qwe/ggg.txt",
            @"hhh.txt",
        };

        using var testDir = new TempDir();
        foreach (var path in testFiles)
        {
            await testDir.Info.RelativeFile(path).WithDirectoryCreate().WriteAllTextAsync(path);
        }

        var testOpt = new VisitFilesOptions
        {
            Recurse = true,
            Buffered = false,
            Sort = false,
        };

        var contents = new List<string>();
        testDir.Info.DoFiles(w => contents.Add(w.File!.ReadAllText()), options: testOpt);
        contents.Should().BeEquivalentTo(testFiles);
    }

    [TestMethod]
    public async Task DoFilesAsync_Action()
    {
        // 再帰フラグONでのファイル列挙処理

        var testFiles = new[]
        {
            @"abc/aaa.txt",
            @"def/ghi/bbb.txt",
            @"ccc.txt",
            @"abc/ddd.txt",
            @"eee.txt",
            @"abc/fff.txt",
            @"asd/qwe/ggg.txt",
            @"hhh.txt",
        };

        using var testDir = new TempDir();
        foreach (var path in testFiles)
        {
            await testDir.Info.RelativeFile(path).WithDirectoryCreate().WriteAllTextAsync(path);
        }

        var testOpt = new VisitFilesOptions
        {
            Recurse = true,
            Buffered = false,
            Sort = false,
        };

        var contents = new List<string>();
        await testDir.Info.DoFilesAsync(async w => contents.Add(await w.File!.ReadAllTextAsync()), options: testOpt);
        contents.Should().BeEquivalentTo(testFiles);
    }
    #endregion

    #region Utility
    [TestMethod]
    public async Task CopyFilesTo()
    {
        var testFiles = new[]
        {
            @"abc/aaa.txt",
            @"def/ghi/bbb.txt",
            @"ccc.txt",
            @"abc/ddd.txt",
            @"eee.txt",
            @"abc/fff.txt",
            @"asd/qwe/ggg.txt",
            @"hhh.txt",
        };

        using var testDir = new TempDir();

        // テストデータ作成
        var srcDir = testDir.Info.RelativeDirectory("src").WithCreate();
        foreach (var path in testFiles)
        {
            await srcDir.RelativeFile(path).WithDirectoryCreate().WriteAllTextAsync(path);
        }

        // テスト実行 (新規作成)
        var destDir = testDir.Info.RelativeDirectory("dest");
        srcDir.CopyFilesTo(destDir);

        // 結果検証
        var destFilePaths = destDir.EnumerateFiles("*", SearchOption.AllDirectories)
            .Select(f => f.RelativePathFrom(destDir).Replace('\\', '/'))
            .ToArray();
        destFilePaths.Should().BeEquivalentTo(testFiles);

        // テストファイルを更新
        foreach (var file in srcDir.GetFiles("*", SearchOption.AllDirectories))
        {
            await file.WriteAllTextAsync(file.FullName);
        }

        // テスト実行 (上書き)
        srcDir.CopyFilesTo(destDir, overwrite: true);

        // 結果検証
        foreach (var file in destDir.GetFiles("*", SearchOption.AllDirectories))
        {
            var expectContent = srcDir.RelativeFile(file.RelativePathFrom(destDir)).FullName;
            var fileContent = await file.ReadAllTextAsync();
            fileContent.Should().Be(expectContent);
        }
    }

    [TestMethod]
    public async Task CopyFilesTo_filter()
    {
        var testFiles = new[]
        {
            @"abc/aaa.txt",
            @"def/ghi/bbb.txt",
            @"ccc.txt",
            @"abc/ddd.txt",
            @"eee.txt",
            @"abc/fff.txt",
            @"asd/qwe/ggg.txt",
            @"hhh.txt",
        };

        using var testDir = new TempDir();

        // テストデータ作成
        var srcDir = testDir.Info.RelativeDirectory("src").WithCreate();
        foreach (var path in testFiles)
        {
            await srcDir.RelativeFile(path).WithDirectoryCreate().WriteAllTextAsync(path);
        }

        // テスト実行 (フィルタ適用)
        var destDir = testDir.Info.RelativeDirectory("dest");
        srcDir.CopyFilesTo(destDir, predicator: file => file.Name == "eee.txt");

        // 結果検証
        destDir.GetFiles("*", SearchOption.AllDirectories).Select(f => f.Name).Should().AllBe("eee.txt");
    }

    [TestMethod]
    public async Task Rename()
    {
        var testFiles = new[]
        {
            @"abc/aaa.txt",
            @"def/ghi/bbb.txt",
            @"ccc.txt",
            @"abc/ddd.txt",
            @"eee.txt",
            @"abc/fff.txt",
            @"asd/qwe/ggg.txt",
            @"hhh.txt",
        };

        using var testDir = new TempDir();

        var orgDir = testDir.Info.RelativeDirectory("aaa");
        var orgPath = orgDir.FullName;
        foreach (var path in testFiles)
        {
            await orgDir.RelativeFile(path).WithDirectoryCreate().WriteAllTextAsync(path);
        }

        var newDir = orgDir.Rename("bbb");
        newDir.Exists.Should().BeTrue();

        orgDir.Should().BeSameAs(newDir);
        orgDir.Name.Should().Be("bbb");

        Directory.Exists(orgPath).Should().BeFalse();
    }

    [TestMethod]
    public async Task DeleteRecurse()
    {
        var testFiles = new[]
        {
            @"abc/aaa.txt",
            @"def/ghi/bbb.txt",
            @"ccc.txt",
            @"abc/ddd.txt",
            @"eee.txt",
            @"abc/fff.txt",
            @"asd/qwe/ggg.txt",
            @"hhh.txt",
        };

        using var testDir = new TempDir();
        foreach (var path in testFiles)
        {
            await testDir.Info.RelativeFile(path).WithDirectoryCreate().WriteAllTextAsync(path);
        }

        // 作成したファイル・ディレクトリに読み取り専用属性を付与
        testDir.Info.DoFiles(w => w.Item.SetReadOnly(true), new() { Handling = VisitFilesHandling.All, });

        // 削除する
        testDir.Info.DeleteRecurse();
        testDir.Info.Refresh();
        testDir.Info.Exists.Should().BeFalse();
    }
    #endregion
}
