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

        var tempDir = testDir.Info.FullName;
        var subDir = Path.Combine(tempDir, "ttt");
        new DirectoryInfo(subDir).WithCreate().FullName.Should().Be(subDir);
        Directory.Exists(subDir).Should().BeTrue();
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
            .IsDescendantOf(new DirectoryInfo(@"c:"), false)
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

        new DirectoryInfo(@"c:")
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

        var testOpt = new SelectFilesOptions
        {
            Recurse = false,
            Buffered = false,
            Sort = false,
        };

        static string selector(FileInfo file) => file.ReadAllText();
        var converter = new SelectFilesWalker<string>(context => context.SetResult(selector(context.File!)));

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

        var testOpt = new SelectFilesOptions
        {
            Recurse = true,
            Buffered = false,
            Sort = false,
        };

        static string selector(FileInfo file) => file.ReadAllText();
        var converter = new SelectFilesWalker<string>(context =>
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

        var testOpt = new SelectFilesOptions
        {
            Recurse = false,
            Handling = new(File: true, Directory: false),
            Buffered = false,
            Sort = false,
        };

        var converter = new SelectFilesWalker<string>(context =>
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

        var testOpt = new SelectFilesOptions
        {
            Recurse = false,
            Handling = new(File: false, Directory: true),
            Buffered = false,
            Sort = false,
        };

        var converter = new SelectFilesWalker<string>(context =>
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

        var testOpt = new SelectFilesOptions
        {
            Recurse = false,
            Handling = new(File: true, Directory: true),
            Buffered = false,
            Sort = false,
        };

        var converter = new SelectFilesWalker<string>(context =>
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

        var testOpt = new SelectFilesOptions
        {
            Recurse = true,
            Handling = new(File: true, Directory: false),
            Buffered = false,
            Sort = false,
        };

        var converter = new SelectFilesWalker<string>(context =>
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

        var testOpt = new SelectFilesOptions
        {
            Recurse = true,
            Handling = new(File: false, Directory: true),
            Buffered = false,
            Sort = false,
        };

        var converter = new SelectFilesWalker<string>(context =>
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

        var testOpt = new SelectFilesOptions
        {
            Recurse = true,
            Handling = new(File: true, Directory: true),
            Buffered = false,
            Sort = false,
        };

        var converter = new SelectFilesWalker<string>(context =>
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
    public async Task VisitFiles_Filter()
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

        var testOpt = new SelectFilesOptions
        {
            Recurse = true,
            Buffered = false,
            Sort = false,
        };

        static string selector(FileInfo file) => file.ReadAllText();
        static bool filter(FileSystemInfo item) => item is DirectoryInfo || item.Name.Contains(".txt");
        var converter = new SelectFilesWalker<string>(context => { if (filter(context.File!)) context.SetResult(selector(context.File!)); });

        var testExpects = testDir.Info.EnumerateFiles("*", SearchOption.AllDirectories)
            .Where(f => filter(f)).Select(selector);

        testDir.Info.VisitFiles(converter, options: testOpt).Should().BeEquivalentTo(testExpects);
    }

    [TestMethod]
    public async Task VisitFiles_Filter_Dir()
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

        var testOpt = new SelectFilesOptions
        {
            Recurse = true,
            Handling = SelectFilesHandling.All,
            Buffered = false,
            Sort = false,
        };

        var converter = new SelectFilesWalker<string>(context =>
        {
            if (context.File == null)
            {
                if (context.Directory.Name == "abc") context.Break = true;
            }
            else
            {
                context.SetResult(context.File.ReadAllText());
            }
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
    public async Task VisitFiles_Excludes()
    {
        // 除外パターン指定

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

        var testOpt = new SelectFilesOptions
        {
            Recurse = true,
            Buffered = false,
            Sort = false,
        };
        var excludes = new[] { new Regex(@"[defg]+.txt"), };

        var converter = new SelectFilesWalker<string>(context => context.SetResult(context.File!.ReadAllText()));

        testDir.Info.VisitFiles(converter, excludes, options: testOpt).Should().BeEquivalentTo(new[]
        {
            @"abc/aaa.txt",
            @"def/ghi/bbb.txt",
            @"ccc.txt",
            @"hhh.txt",
        });
    }

    [TestMethod]
    public async Task VisitFiles_Includes()
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

        var testOpt = new SelectFilesOptions
        {
            Recurse = true,
            Buffered = false,
            Sort = false,
        };
        var excludes = new Regex[0];
        var includes = new[] { new Regex(@"[defg]+.txt"), };

        var converter = new SelectFilesWalker<string>(context => context.SetResult(context.File!.ReadAllText()));

        testDir.Info.VisitFiles(converter, excludes, includes, options: testOpt).Should().BeEquivalentTo(new[]
        {
            @"abc/ddd.txt",
            @"eee.txt",
            @"abc/fff.txt",
            @"asd/qwe/ggg.txt",
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

        var testOpt = new SelectFilesOptions
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
    public async Task SelectFiles_Filter()
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

        var testOpt = new SelectFilesOptions
        {
            Recurse = true,
            Buffered = false,
            Sort = false,
        };

        static string selector(FileInfo file) => file.ReadAllText();
        static bool filter(FileSystemInfo item) => item is DirectoryInfo || item.Name.Contains(".txt");

        var testExpects = testDir.Info.EnumerateFiles("*", SearchOption.AllDirectories)
            .Where(f => filter(f)).Select(selector);

        testDir.Info.SelectFiles(w => filter(w.File!), w => selector(w.File!), options: testOpt).Should().BeEquivalentTo(testExpects);
    }

    [TestMethod]
    public async Task SelectFiles_Excludes()
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

        var testOpt = new SelectFilesOptions
        {
            Recurse = true,
            Buffered = false,
            Sort = false,
        };
        var excludes = new[] { new Regex(@"[defg]+.txt"), };

        static string selector(FileInfo file) => file.ReadAllText();

        testDir.Info.SelectFiles(w => selector(w.File!), excludes, options: testOpt).Should().BeEquivalentTo(new[]
        {
            @"abc/aaa.txt",
            @"def/ghi/bbb.txt",
            @"ccc.txt",
            @"hhh.txt",
        });
    }

    [TestMethod]
    public async Task SelectFiles_Includes()
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

        var testOpt = new SelectFilesOptions
        {
            Recurse = true,
            Buffered = false,
            Sort = false,
        };
        var excludes = new Regex[0];
        var includes = new[] { new Regex(@"[defg]+.txt"), };

        static string selector(FileInfo file) => file.ReadAllText();

        testDir.Info.SelectFiles(w => selector(w.File!), excludes, includes, options: testOpt).Should().BeEquivalentTo(new[]
        {
            @"abc/ddd.txt",
            @"eee.txt",
            @"abc/fff.txt",
            @"asd/qwe/ggg.txt",
        });
    }

    [TestMethod]
    public async Task SelectFiles_SpecialDir()
    {
        var docDir = SpecialFolder.Get(Environment.SpecialFolder.MyDocuments);
        var options = new SelectFilesOptions { Recurse = true, Sort = false, SkipAttributes = FileAttributes.None, SkipInaccessible = true, };

        await FluentActions
            .Awaiting(async () => await docDir.SelectFilesAsync(w => ValueTask.FromResult(w.Item.FullName), options with { Buffered = false, }).ToArrayAsync())
            .Should().NotThrowAsync();

        await FluentActions
            .Awaiting(async () => await docDir.SelectFilesAsync(w => ValueTask.FromResult(w.Item.FullName), options with { Buffered = true, }).ToArrayAsync())
            .Should().NotThrowAsync();

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

        var testOpt = new SelectFilesOptions
        {
            Recurse = false,
            Buffered = false,
            Sort = false,
        };

        var converter = new AsyncSelectFilesWalker<string>(async context =>
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

        var testOpt = new SelectFilesOptions
        {
            Recurse = true,
            Buffered = false,
            Sort = false,
        };

        var converter = new AsyncSelectFilesWalker<string>(async context =>
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

        var testOpt = new SelectFilesOptions
        {
            Recurse = false,
            Handling = new(File: true, Directory: false),
            Buffered = false,
            Sort = false,
        };

        var converter = new AsyncSelectFilesWalker<string>(async context =>
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

        var testOpt = new SelectFilesOptions
        {
            Recurse = false,
            Handling = new(File: false, Directory: true),
            Buffered = false,
            Sort = false,
        };

        var converter = new AsyncSelectFilesWalker<string>(async context =>
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

        var testOpt = new SelectFilesOptions
        {
            Recurse = false,
            Handling = new(File: true, Directory: true),
            Buffered = false,
            Sort = false,
        };

        var converter = new AsyncSelectFilesWalker<string>(async context =>
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

        var testOpt = new SelectFilesOptions
        {
            Recurse = true,
            Handling = new(File: true, Directory: false),
            Buffered = false,
            Sort = false,
        };

        var converter = new AsyncSelectFilesWalker<string>(async context =>
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

        var testOpt = new SelectFilesOptions
        {
            Recurse = true,
            Handling = new(File: false, Directory: true),
            Buffered = false,
            Sort = false,
        };

        var converter = new AsyncSelectFilesWalker<string>(async context =>
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

        var testOpt = new SelectFilesOptions
        {
            Recurse = true,
            Handling = new(File: true, Directory: true),
            Buffered = false,
            Sort = false,
        };

        var converter = new AsyncSelectFilesWalker<string>(async context =>
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
    public async Task VisitFilesAsync_Filter()
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

        var testOpt = new SelectFilesOptions
        {
            Recurse = true,
            Buffered = false,
            Sort = false,
        };

        static bool filter(FileSystemInfo item) => item is DirectoryInfo || item.Name.Contains(".txt");
        var converter = new AsyncSelectFilesWalker<string>(async context =>
        {
            if (filter(context.File!)) context.SetResult(await context.File!.ReadAllTextAsync());
        });

        var testExpects = testDir.Info.EnumerateFiles("*", SearchOption.AllDirectories)
            .Where(f => filter(f)).Select(f => f.ReadAllText());

        (await testDir.Info.VisitFilesAsync(converter, options: testOpt).ToArrayAsync())
            .Should().BeEquivalentTo(testExpects);
    }

    [TestMethod]
    public async Task VisitFilesAsync_Filter_Dir()
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

        var testOpt = new SelectFilesOptions
        {
            Recurse = true,
            Handling = SelectFilesHandling.All,
            Buffered = false,
            Sort = false,
        };

        var converter = new AsyncSelectFilesWalker<string>(async context =>
        {
            if (context.File == null)
            {
                if (context.Directory.Name == "abc") context.Break = true;
            }
            else
            {
                context.SetResult(await context.File.ReadAllTextAsync());
            }
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
    public async Task VisitFilesAsync_Excludes()
    {
        // 除外パターン指定

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

        var testOpt = new SelectFilesOptions
        {
            Recurse = true,
            Buffered = false,
            Sort = false,
        };
        var excludes = new[] { new Regex(@"[defg]+.txt"), };

        var converter = new AsyncSelectFilesWalker<string>(async context => context.SetResult(await context.File!.ReadAllTextAsync()));

        (await testDir.Info.VisitFilesAsync(converter, excludes, options: testOpt).ToArrayAsync()).Should().BeEquivalentTo(new[]
        {
            @"abc/aaa.txt",
            @"def/ghi/bbb.txt",
            @"ccc.txt",
            @"hhh.txt",
        });
    }

    [TestMethod]
    public async Task VisitFilesAsync_Includes()
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

        var testOpt = new SelectFilesOptions
        {
            Recurse = true,
            Buffered = false,
            Sort = false,
        };
        var excludes = new Regex[0];
        var includes = new[] { new Regex(@"[defg]+.txt"), };

        var converter = new AsyncSelectFilesWalker<string>(async context => context.SetResult(await context.File!.ReadAllTextAsync()));

        (await testDir.Info.VisitFilesAsync(converter, excludes, includes, options: testOpt).ToArrayAsync()).Should().BeEquivalentTo(new[]
        {
            @"abc/ddd.txt",
            @"eee.txt",
            @"abc/fff.txt",
            @"asd/qwe/ggg.txt",
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

        var testOpt = new SelectFilesOptions
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
    public async Task SelectFilesAsync_Filter()
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

        var testOpt = new SelectFilesOptions
        {
            Recurse = true,
            Buffered = false,
            Sort = false,
        };

        static bool filter(FileSystemInfo item) => item is DirectoryInfo || item.Name.Contains(".txt");

        var testExpects = testDir.Info.EnumerateFiles("*", SearchOption.AllDirectories)
            .Where(f => filter(f)).Select(f => f.ReadAllText());

        (await testDir.Info.SelectFilesAsync(w => filter(w.File!), async w => await w.File!.ReadAllTextAsync(), options: testOpt).ToArrayAsync())
            .Should().BeEquivalentTo(testExpects);
    }

    [TestMethod]
    public async Task SelectFilesAsync_Excludes()
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

        var testOpt = new SelectFilesOptions
        {
            Recurse = true,
            Buffered = false,
            Sort = false,
        };
        var excludes = new[] { new Regex(@"[defg]+.txt"), };

        (await testDir.Info.SelectFilesAsync(async w => await w.File!.ReadAllTextAsync(), excludes, options: testOpt).ToArrayAsync()).Should().BeEquivalentTo(new[]
        {
            @"abc/aaa.txt",
            @"def/ghi/bbb.txt",
            @"ccc.txt",
            @"hhh.txt",
        });
    }

    [TestMethod]
    public async Task SelectFilesAsync_Includes()
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

        var testOpt = new SelectFilesOptions
        {
            Recurse = true,
            Buffered = false,
            Sort = false,
        };
        var excludes = new Regex[0];
        var includes = new[] { new Regex(@"[defg]+.txt"), };

        (await testDir.Info.SelectFilesAsync(async w => await w.File!.ReadAllTextAsync(), excludes, includes, options: testOpt).ToArrayAsync()).Should().BeEquivalentTo(new[]
        {
            @"abc/ddd.txt",
            @"eee.txt",
            @"abc/fff.txt",
            @"asd/qwe/ggg.txt",
        });
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

        var testOpt = new SelectFilesOptions
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
    public async Task DoFiles_ExcludesIncludes()
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

        var testOpt = new SelectFilesOptions
        {
            Recurse = true,
            Buffered = false,
            Sort = false,
        };
        var excludes = new[] { new Regex(@"[cde]+.txt"), };
        var includes = new[] { new Regex(@"[defg]+.txt"), };
        var contents = new List<string>();

        testDir.Info.DoFiles(w => contents.Add(w.File!.ReadAllText()), excludes, includes, options: testOpt);

        contents.Should().BeEquivalentTo(new[]
        {
            @"abc/fff.txt",
            @"asd/qwe/ggg.txt",
        });
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

        var testOpt = new SelectFilesOptions
        {
            Recurse = true,
            Buffered = false,
            Sort = false,
        };

        var contents = new List<string>();
        await testDir.Info.DoFilesAsync(async w => contents.Add(await w.File!.ReadAllTextAsync()), options: testOpt);
        contents.Should().BeEquivalentTo(testFiles);
    }

    [TestMethod]
    public async Task DoFilesAsync_ExcludesIncludes()
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

        var testOpt = new SelectFilesOptions
        {
            Recurse = true,
            Buffered = false,
            Sort = false,
        };
        var excludes = new[] { new Regex(@"[cde]+.txt"), };
        var includes = new[] { new Regex(@"[defg]+.txt"), };
        var contents = new List<string>();

        await testDir.Info.DoFilesAsync(async w => contents.Add(await w.File!.ReadAllTextAsync()), excludes, includes, options: testOpt);

        contents.Should().BeEquivalentTo(new[]
        {
            @"abc/fff.txt",
            @"asd/qwe/ggg.txt",
        });
    }
    #endregion

    #region Utility
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
        testDir.Info.DoFiles(w => w.Item.SetReadOnly(true), new() { Handling = SelectFilesHandling.All, });

        // 削除する
        testDir.Info.DeleteRecurse();
        testDir.Info.Refresh();
        testDir.Info.Exists.Should().BeFalse();
    }
    #endregion
}
