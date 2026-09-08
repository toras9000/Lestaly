namespace LestalyTest;

[TestClass]
public class WinUACTests
{
    [TestMethod]
    public void IsElevated()
    {
        if (!OperatingSystem.IsWindows()) throw new AssertInconclusiveException();

        WinUAC.IsElevated();
    }

    [TestMethod]
    public void IsAdminRole()
    {
        if (!OperatingSystem.IsWindows()) throw new AssertInconclusiveException();

        WinUAC.IsAdminRole();
    }
}
