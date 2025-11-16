using Lestaly.Cx;

namespace LestalyTest.Cx;

[TestClass()]
public class CmdFileInfoExtensionsTests
{
    [TestMethod()]
    public async Task GetAwaiter()
    {
        var ipcnofigExe = Environment.SpecialFolder.System.GetInfo().RelativeFile("ipconfig.exe");
        (await ipcnofigExe).Should().Contain("DNS");
    }

    [TestMethod()]
    public async Task launch()
    {
        var ipcnofigExe = Environment.SpecialFolder.System.GetInfo().RelativeFile("ipconfig.exe");
        (await ipcnofigExe.launch().output()).Should().Contain("DNS");
    }
}
