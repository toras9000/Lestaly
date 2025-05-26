namespace LestalyTest;

[TestClass()]
public class EnvVarsTests
{
    [TestMethod()]
    public void String()
    {
        {
            var varName = $"TEST-{DateTime.Now.Ticks:X}";
            Environment.SetEnvironmentVariable(varName, "ABCDEF");
            EnvVers.String(varName).Should().Be("ABCDEF");
            EnvVers.String(varName + "-MISSING").Should().BeNull();
        }
        {
            var varName = $"TEST-{DateTime.Now.Ticks:X}";
            Environment.SetEnvironmentVariable(varName, "ABCDEF");
            EnvVers.String(varName, "XYZ").Should().Be("ABCDEF");
            EnvVers.String(varName + "-MISSING", "XYZ").Should().Be("XYZ");
        }
    }

    [TestMethod()]
    public void Number()
    {
        {
            var varName = $"TEST-{DateTime.Now.Ticks:X}";
            Environment.SetEnvironmentVariable(varName, "123");
            EnvVers.Number<int>(varName).Should().Be(123);
            EnvVers.Number<int>(varName + "-MISSING").Should().BeNull();
        }
        {
            var varName = $"TEST-{DateTime.Now.Ticks:X}";
            Environment.SetEnvironmentVariable(varName, "123");
            EnvVers.Number<int>(varName, 456).Should().Be(123);
            EnvVers.Number<int>(varName + "-MISSING", 456).Should().Be(456);
        }
        {
            var varName = $"TEST-{DateTime.Now.Ticks:X}";
            Environment.SetEnvironmentVariable(varName, "ABC");
            EnvVers.Number<int>(varName, 456).Should().Be(456);
            EnvVers.Number<int>(varName + "-MISSING", 456).Should().Be(456);
        }
    }

    [TestMethod()]
    public void ThisSourceRelativeFile()
    {
        var thisDir = ThisSource.RelativeDirectory(".");
        var varName = $"ABC{DateTime.Now.Ticks:X}";
        var fileName = "abc.txt";
        Environment.SetEnvironmentVariable(varName, fileName);

        EnvVers.ThisSourceRelativeFile(varName)!.FullName.Should().Be(thisDir.RelativeFile(fileName).FullName);
    }

    [TestMethod()]
    public void ThisSourceRelativeDirectory()
    {
        var thisDir = ThisSource.RelativeDirectory(".");
        var varName = $"ABC{DateTime.Now.Ticks:X}";
        var fileName = "abc.txt";
        Environment.SetEnvironmentVariable(varName, fileName);

        EnvVers.ThisSourceRelativeDirectory(varName)!.FullName.Should().Be(thisDir.RelativeDirectory(fileName).FullName);
    }

    [TestMethod()]
    public void CurrentRelativeFile()
    {
        using var tempDir = new TempDir();
        Directory.SetCurrentDirectory(tempDir.Info.FullName);

        var varName = $"ABC{DateTime.Now.Ticks:X}";
        var fileName = "abc.txt";
        Environment.SetEnvironmentVariable(varName, fileName);

        EnvVers.CurrentRelativeFile(varName)!.FullName.Should().Be(tempDir.Info.RelativeFile(fileName).FullName);
    }

    [TestMethod()]
    public void CurrentRelativeDirectory()
    {
        using var tempDir = new TempDir();
        Directory.SetCurrentDirectory(tempDir.Info.FullName);

        var varName = $"ABC{DateTime.Now.Ticks:X}";
        var fileName = "abc.txt";
        Environment.SetEnvironmentVariable(varName, fileName);

        EnvVers.CurrentRelativeDirectory(varName)!.FullName.Should().Be(tempDir.Info.RelativeDirectory(fileName).FullName);
    }

    [TestMethod()]
    public void AppRelativeFile()
    {
        var appDir = new DirectoryInfo(AppContext.BaseDirectory);
        var varName = $"ABC{DateTime.Now.Ticks:X}";
        var fileName = "abc.txt";
        Environment.SetEnvironmentVariable(varName, fileName);

        EnvVers.AppRelativeFile(varName)!.FullName.Should().Be(appDir.RelativeFile(fileName).FullName);
    }

    [TestMethod()]
    public void AppRelativeDirectory()
    {
        var appDir = new DirectoryInfo(AppContext.BaseDirectory);
        var varName = $"ABC{DateTime.Now.Ticks:X}";
        var fileName = "abc.txt";
        Environment.SetEnvironmentVariable(varName, fileName);

        EnvVers.AppRelativeDirectory(varName)!.FullName.Should().Be(appDir.RelativeDirectory(fileName).FullName);
    }
}