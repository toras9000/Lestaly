using System.Globalization;
using System.Text;
using FluentAssertions;

namespace Lestaly.Tests;

[TestClass()]
public class CmdShellTests
{
    [TestMethod()]
    public async Task ExecAsync()
    {
        (await CmdShell.ExecAsync("cmd", new[] { "/c", })).Should().Be(0);
        (await CmdShell.ExecAsync("cmd", new[] { "/c", "exit", "/b", "7", })).Should().Be(7);
    }

}
