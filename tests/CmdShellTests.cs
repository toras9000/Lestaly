namespace LestalyTest;

[TestClass()]
public class CmdShellTests
{
    [TestMethod()]
    public async Task ExecAsync()
    {
        (await CmdShell.ExecAsync("cmd", ["/c",]))?.Code.Should().Be(0);
        (await CmdShell.ExecAsync("cmd", ["/c", "exit", "/b", "7"]))?.Code.Should().Be(7);
    }

}
