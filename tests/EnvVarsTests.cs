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
}