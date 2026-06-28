namespace LestalyTest;

[TestClass]
public class EnvironmentExtensionsTests
{
    [TestMethod]
    public void GetInfo()
    {
        var spInfo = Environment.SpecialFolder.UserProfile.GetInfo();
        var spFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        spInfo.FullName.Should().Be(spFolder);
    }

    [TestMethod]
    public void RelativeFile()
    {
        var spInfo = Environment.SpecialFolder.UserProfile.RelativeFile("test-file.ext");
        var spFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        spInfo.FullName.Should().Be(Path.Combine(spFolder, "test-file.ext"));
    }

    [TestMethod]
    public void RelativeDirectory()
    {
        var spInfo = Environment.SpecialFolder.UserProfile.RelativeDirectory("test-dir");
        var spFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        spInfo.FullName.Should().Be(Path.Combine(spFolder, "test-dir"));
    }
}
