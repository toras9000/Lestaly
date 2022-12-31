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
        using var tempDir = new TempDir();
        for (var d = 0; d < 3; d++)
        {
            var sub = tempDir.Info.CreateSubdirectory($"D{d:D3}");
            for (var f = 0; f < 3; f++)
            {
                sub.GetRelativeFile($"F{f:D3}.txt").WriteAllText(f.ToString());
            }
        }

        var expect = Enumerable.Range(0, 3)
            .SelectMany(d => Enumerable.Range(0, 3).Select(f => (d, f)))
            .Select(e => Path.Combine(tempDir.Info.FullName, $"D{e.d:D3}", $"F{e.f:D3}.txt"));

        tempDir.Info.SelectFiles(c => c.File?.FullName)
            .OrderBy(n => n)
            .Should()
            .Equal(expect);
    }

    [TestMethod()]
    public void SelectFiles_FuncExcludes()
    {
        using var tempDir = new TempDir();
        for (var d = 0; d < 3; d++)
        {
            var sub = tempDir.Info.CreateSubdirectory($"D{d:D3}");
            for (var f = 0; f < 3; f++)
            {
                sub.GetRelativeFile($"F{f:D3}.txt").WriteAllText(f.ToString());
            }
        }

        var excludes = new[] { new Regex("F001"), };

        var expect = Enumerable.Range(0, 3)
            .SelectMany(d => Enumerable.Range(0, 3).Where(n => n != 1).Select(f => (d, f)))
            .Select(e => Path.Combine(tempDir.Info.FullName, $"D{e.d:D3}", $"F{e.f:D3}.txt"));

        tempDir.Info.SelectFiles(c => c.File?.FullName, excludes)
            .OrderBy(n => n)
            .Should()
            .Equal(expect);
    }

    [TestMethod()]
    public void SelectFiles_FilterFunc()
    {
        using var tempDir = new TempDir();
        for (var d = 0; d < 3; d++)
        {
            var sub = tempDir.Info.CreateSubdirectory($"D{d:D3}");
            for (var f = 0; f < 3; f++)
            {
                sub.GetRelativeFile($"F{f:D3}.txt").WriteAllText(f.ToString());
            }
        }

        var expect = Enumerable.Range(0, 3)
            .SelectMany(d => Enumerable.Range(0, 3).Where(n => n == 1).Select(f => (d, f)))
            .Select(e => Path.Combine(tempDir.Info.FullName, $"D{e.d:D3}", $"F{e.f:D3}.txt"));

        tempDir.Info.SelectFiles(c => c.File?.Name.StartsWith("F001") == true, c => c.File!.FullName)
            .OrderBy(n => n)
            .Should()
            .Equal(expect);
    }
}
