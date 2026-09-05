namespace LestalyTest;

[TestClass]
public class WinUACTests
{
    [TestMethod]
    public void IsElevated()
    {
        WinUAC.IsElevated();
    }
}
