namespace LestalyTest;

[TestClass()]
public class CmdProcTests
{
    [TestMethod()]
    public async Task ExecAsync_Normal()
    {
        var writer = new StringWriter();
        var exit = await CmdProc.ExecAsync("ipconfig", new[] { "/all", }, stdOut: writer);
        var output = writer.ToString();
        exit.Code.Should().Be(0);
        output.Should().Contain("DHCP");
    }

    [TestMethod()]
    public async Task ExecAsync_NonZero()
    {
        // cmd /? はリダイレクトしないと入力待ちになってしまうことに注意
        var exit = await CmdProc.ExecAsync("cmd", new[] { "/?", }, stdOut: TextWriter.Null);
        exit.Code.Should().Be(1);
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
    public async Task ExecAsync_Input()
    {
        using var canceller = new CancellationTokenSource();
        canceller.CancelAfter(3000);

        using var stdInReader = new StringReader(" ");

        await FluentActions.Awaiting(() => CmdProc.ExecAsync("cmd", new[] { "/C", "pause" }, stdIn: stdInReader, cancelToken: canceller.Token))
              .Should().NotThrowAsync();
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

    [TestMethod()]
    public void CmdExit_implicit()
    {
        (new CmdExit(3) == 3).Should().Be(true);
    }

    [TestMethod()]
    public async Task CmdExit_AllowCodes_Allow()
    {
        await FluentActions.Awaiting(() => CmdProc.ExecAsync("ipconfig", new[] { "/all", }).AsSuccessCode(new[] { 0, }))
              .Should().NotThrowAsync();
    }

    [TestMethod()]
    public async Task CmdExit_AllowCodes_NotAllow()
    {
        await FluentActions.Awaiting(() => CmdProc.ExecAsync("ipconfig", new[] { "/all", }).AsSuccessCode(new[] { 100, }))
              .Should().ThrowAsync<CmdProcExitCodeException>();
    }

    [TestMethod()]
    public async Task CmdExit_AllowCodes_NotAllowEx()
    {
        await FluentActions.Awaiting(() => CmdProc.ExecAsync("ipconfig", new[] { "/all", }).AsSuccessCode(new[] { 100, }, code => new ArgumentOutOfRangeException($"{code}")))
              .Should().ThrowAsync<ArgumentOutOfRangeException>();
    }

    [TestMethod()]
    public async Task CmdResult_AllowCodes_Allow()
    {
        await FluentActions.Awaiting(() => CmdProc.RunAsync("ipconfig", new[] { "/all", }).AsSuccessCode(new[] { 0, }))
              .Should().NotThrowAsync();
    }

    [TestMethod()]
    public async Task CmdResult_AllowCodes_NotAllow()
    {
        await FluentActions.Awaiting(() => CmdProc.RunAsync("ipconfig", new[] { "/all", }).AsSuccessCode(new[] { 100, }))
              .Should().ThrowAsync<CmdProcExitCodeException>();
    }

    [TestMethod()]
    public async Task CmdResult_AllowCodes_NotAllowEx()
    {
        await FluentActions.Awaiting(() => CmdProc.RunAsync("ipconfig", new[] { "/all", }).AsSuccessCode(new[] { 100, }, code => new ArgumentOutOfRangeException($"{code}")))
              .Should().ThrowAsync<ArgumentOutOfRangeException>();
    }

    [TestMethod()]
    public async Task CmdResult_SuccessOutput_Success()
    {
        var output = await CmdProc.RunAsync("ipconfig", new[] { "/all", }).AsSuccessOutput();
        output.Should().Contain("DHCP");
    }

    [TestMethod()]
    public async Task CmdResult_SuccessOutput_Error()
    {
        await FluentActions.Awaiting(() => CmdProc.RunAsync("ipconfig", new[] { "/all", }).AsSuccessOutput(new[] { 100, }))
              .Should().ThrowAsync<CmdProcExitCodeException>();
    }
}
