using System.Globalization;
using System.Text;
using FluentAssertions;

namespace Lestaly.Tests;

[TestClass()]
public class CmdProcTests
{
    [TestMethod()]
    public async Task ExecAsync_Normal()
    {
        var writer = new StringWriter();
        var exitCode = await CmdProc.ExecAsync("ipconfig", new[] { "/all", }, stdOutWriter: writer);
        var output = writer.ToString();
        exitCode.Should().Be(0);
        output.Should().Contain("DHCP");
    }

    [TestMethod()]
    public async Task ExecAsync_NonZero()
    {
        // cmd /? はリダイレクトしないと入力待ちになってしまうことに注意
        var exitCode = await CmdProc.ExecAsync("cmd", new[] { "/?", }, stdOutWriter: TextWriter.Null);
        exitCode.Should().Be(1);
    }

    [TestMethod()]
    public async Task ExecAsync_NeverEnd()
    {
        using var canceller = new CancellationTokenSource();
        canceller.CancelAfter(3000);

        await FluentActions.Awaiting(() => CmdProc.ExecAsync("cmd", new[] { "/?", }, cancelToken: canceller.Token))
              .Should().ThrowAsync<CmdProcCancelException>();
    }

    [TestMethod()]
    public async Task RunAsync_Normal()
    {
        var result = await CmdProc.RunAsync("ipconfig", new[] { "/all", });
        result.ExitCode.Should().Be(0);
        result.Output.Should().Contain("DHCP");
    }

    [TestMethod()]
    public async Task RunAsync_NonZero()
    {
        var result = await CmdProc.RunAsync("cmd", new[] { "/?", });
        result.ExitCode.Should().Be(1);
    }

    [TestMethod()]
    public async Task RunAsync_NeverEnd()
    {
        using var canceller = new CancellationTokenSource();
        canceller.CancelAfter(3000);

        await FluentActions.Awaiting(() => CmdProc.RunAsync("ping", new[] { "-t", "localhost", }, cancelToken: canceller.Token))
              .Should().ThrowAsync<CmdProcCancelException>();
    }

    [TestMethod()]
    public async Task RunAsync_NoCommand()
    {
        await FluentActions.Awaiting(() => CmdProc.RunAsync("not-exists-command"))
              .Should().ThrowAsync<Exception>();
    }

    [TestMethod()]
    public async Task CallAsync_Normal()
    {
        var result = await CmdProc.CallAsync("ipconfig", new[] { "/all", });
        result.Should().Contain("DHCP");
    }

    [TestMethod()]
    public async Task CallAsync_NotAllow()
    {
        await FluentActions.Awaiting(() => CmdProc.CallAsync("ipconfig", new[] { "/all", }, allowExitCodes: new[] { 100, }))
              .Should().ThrowAsync<CmdProcExitCodeException>();
    }

    [TestMethod()]
    public async Task CallAsync_NeverEnd()
    {
        using var canceller = new CancellationTokenSource();
        canceller.CancelAfter(3000);

        await FluentActions.Awaiting(() => CmdProc.CallAsync("ping", new[] { "-t", "localhost", }, cancelToken: canceller.Token))
              .Should().ThrowAsync<CmdProcCancelException>();
    }

    [TestMethod()]
    public async Task CallAsync_NoCommand()
    {
        await FluentActions.Awaiting(() => CmdProc.CallAsync("not-exists-command"))
              .Should().ThrowAsync<Exception>();
    }

}
