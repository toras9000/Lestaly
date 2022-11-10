﻿using FluentAssertions;
using TestCometFlavor._Test;

namespace LestalyTest.Extensions;

[TestClass()]
public class FileExtensionsTests
{
    [TestMethod()]
    public async Task Touch()
    {
        using var tempDir = new TempDirectory();

        var file = tempDir.Info.GetRelativeFile("asd.txt");
        file.Exists.Should().Be(false);
        file.Touch();
        file.Exists.Should().Be(true);

        var timestamp = file.LastWriteTimeUtc;
        await Task.Delay(3000);
        file.Touch();
        file.LastWriteTimeUtc.Should().NotBe(timestamp);
    }

}