using System.Data;
using System.Text.RegularExpressions;
using FluentAssertions;
using Lestaly;

namespace LestalyTest.Extensions;

[TestClass()]
public class DirectoryInfoExtensionsTests
{
    [TestMethod()]
    public void SelectFiles_Func()
    {
        var data = new[]
        {
            "D001/F001.txt",
            "D001/F002.txt",
            "D001/F003.txt",
            "D002/F001.txt",
            "D002/F002.txt",
            "D002/F003.txt",
            "D003/F001.txt",
            "D003/F002.txt",
            "D003/F003.txt",
        };

        using var tempDir = new TempDir();
        foreach (var path in data)
        {
            tempDir.Info.GetRelativeFile(path).WithDirectoryCreate().WriteAllText(path);
        }

        var actual = tempDir.Info.SelectFiles(c => c.Item.RelativePathFrom(tempDir.Info, ignoreCase: true))
            .Select(p => p!.Replace('\\', '/'))
            .ToArray();

        var expect = data;

        actual.Should().BeEquivalentTo(expect);
    }

    [TestMethod()]
    public void SelectFiles_FuncExcludes()
    {
        var data = new[]
        {
            "D001/F001.txt",
            "D001/F002.txt",
            "D001/F003.txt",
            "D002/F001.txt",
            "D002/F002.txt",
            "D002/F003.txt",
            "D003/F001.txt",
            "D003/F002.txt",
            "D003/F003.txt",
        };

        using var tempDir = new TempDir();
        foreach (var path in data)
        {
            tempDir.Info.GetRelativeFile(path).WithDirectoryCreate().WriteAllText(path);
        }

        {// exclude file
            var excludes = new[] { new Regex("F001"), };

            var actual = tempDir.Info.SelectFiles(c => c.Item.RelativePathFrom(tempDir.Info, ignoreCase: true), excludes)
                .Select(p => p!.Replace('\\', '/'))
                .ToArray();

            var expect = new[]
            {
                "D001/F002.txt",
                "D001/F003.txt",
                "D002/F002.txt",
                "D002/F003.txt",
                "D003/F002.txt",
                "D003/F003.txt",
            };

            actual.Should().BeEquivalentTo(expect);
        }

        {// exclude dir
            var excludes = new[] { new Regex("D003"), };

            var actual = tempDir.Info.SelectFiles(
                    c => c.Item.RelativePathFrom(tempDir.Info, ignoreCase: true),
                    excludes,
                    options: new() { DirectoryHandling = true, }
                )
                .Select(p => p!.Replace('\\', '/'))
                .ToArray();

            var expect = new[]
            {
                "D001",
                "D001/F001.txt",
                "D001/F002.txt",
                "D001/F003.txt",
                "D002",
                "D002/F001.txt",
                "D002/F002.txt",
                "D002/F003.txt",
            };

            actual.Should().BeEquivalentTo(expect);
        }
    }

    [TestMethod()]
    public void SelectFiles_FilterFunc()
    {
        var data = new[]
        {
            "D001/F001.txt",
            "D001/F002.txt",
            "D001/F003.txt",
            "D002/F001.txt",
            "D002/F002.txt",
            "D002/F003.txt",
            "D003/F001.txt",
            "D003/F002.txt",
            "D003/F003.txt",
        };

        using var tempDir = new TempDir();
        foreach (var path in data)
        {
            tempDir.Info.GetRelativeFile(path).WithDirectoryCreate().WriteAllText(path);
        }

        {// filter file
            var actual = tempDir.Info.SelectFiles(
                    c => c.Item.Name.StartsWith("F001") == true,
                    c => c.Item.RelativePathFrom(tempDir.Info, ignoreCase: true)
                )
                .Select(p => p!.Replace('\\', '/'))
                .ToArray();
            var expect = new[]
            {
                "D001/F001.txt",
                "D002/F001.txt",
                "D003/F001.txt",
            };
            actual.Should().BeEquivalentTo(expect);
        }

        {// filter dir
            var actual = tempDir.Info.SelectFiles(
                    c => c.Item.Name != "D002",
                    c => c.Item.RelativePathFrom(tempDir.Info, ignoreCase: true),
                    new() { DirectoryHandling = true, }
                )
                .Select(p => p!.Replace('\\', '/'))
                .OrderBy(n => n)
                .ToArray();
            var expect = new[]
            {
                "D001",
                "D001/F001.txt",
                "D001/F002.txt",
                "D001/F003.txt",
                "D003",
                "D003/F001.txt",
                "D003/F002.txt",
                "D003/F003.txt",
            };
            actual.Should().BeEquivalentTo(expect);
        }
    }
}
