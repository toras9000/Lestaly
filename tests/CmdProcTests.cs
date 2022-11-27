using FluentAssertions;

namespace Lestaly.Tests;

[TestClass()]
public class CmdProcTests
{
    [TestMethod()]
    public async Task CallAsync_Normal()
    {
        var result = await CmdProc.CallAsync("ipconfig", new[] { "/all", });
        result.ExitCode.Should().Be(0);
        result.Output.Should().Contain("DHCP");
    }

    [TestMethod()]
    public async Task CallAsync_NonZero()
    {
        var result = await CmdProc.CallAsync("cmd", new[] { "/?", });
        result.ExitCode.Should().Be(1);
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

    [TestMethod()]
    public async Task ExecAsync_Normal()
    {
        var result = await CmdProc.ExecAsync("ipconfig", new[] { "/all", });
        result.Should().Contain("DHCP");
    }

    [TestMethod()]
    public async Task ExecAsync_NotAllow()
    {
        await FluentActions.Awaiting(() => CmdProc.ExecAsync("ipconfig", new[] { "/all", }, allowExitCodes: new[] { 100, }))
              .Should().ThrowAsync<CmdProcExitCodeException>();
    }

    [TestMethod()]
    public async Task ExecAsync_NeverEnd()
    {
        using var canceller = new CancellationTokenSource();
        canceller.CancelAfter(3000);

        await FluentActions.Awaiting(() => CmdProc.ExecAsync("ping", new[] { "-t", "localhost", }, cancelToken: canceller.Token))
              .Should().ThrowAsync<CmdProcCancelException>();
    }

    [TestMethod()]
    public async Task ExecAsync_NoCommand()
    {
        await FluentActions.Awaiting(() => CmdProc.ExecAsync("not-exists-command"))
              .Should().ThrowAsync<Exception>();
    }

}
