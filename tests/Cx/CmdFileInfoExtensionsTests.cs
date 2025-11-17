using Lestaly.Cx;

namespace LestalyTest.Cx;

[TestClass()]
public class CmdFileInfoExtensionsTests
{
    [TestMethod()]
    public async Task launch()
    {
        var ipcnofigExe = Environment.SpecialFolder.System.GetInfo().RelativeFile("ipconfig.exe");
        (await ipcnofigExe.launch().output()).Should().Contain("DNS");
    }

    [TestMethod()]
    public async Task args()
    {
        var cmdExe = Environment.SpecialFolder.System.GetInfo().RelativeFile("cmd.exe");
        (await cmdExe.args("/c", "echo", "abc")).Output.Should().Contain("abc");
        (await cmdExe.args("/c", "exit", "0")).ExitCode.Should().Be(0);
        (await cmdExe.args("/c", "exit", "1")).ExitCode.Should().Be(1);
    }


    [TestMethod()]
    public async Task success()
    {
        using var tempDir = new TempDir();
        var exeRet0 = await tempDir.Info.MakeTestExeAsync("ret0", "return 0;");
        var exeRet1 = await tempDir.Info.MakeTestExeAsync("ret1", "return 1;");
        var exeRet2 = await tempDir.Info.MakeTestExeAsync("ret2", "return 2;");


        await FluentActions.Awaiting(async () => await exeRet0.success()).Should().NotThrowAsync();
        await FluentActions.Awaiting(async () => await exeRet1.success(1)).Should().NotThrowAsync();

        await FluentActions.Awaiting(async () => await exeRet1.success()).Should().ThrowAsync<Exception>();
        await FluentActions.Awaiting(async () => await exeRet0.success(1)).Should().ThrowAsync<Exception>();
    }

    [TestMethod()]
    public async Task redirect()
    {
        using var tempDir = new TempDir();
        var exeHello = await tempDir.Info.MakeTestExeAsync("hello", """Console.WriteLine("hello");""");

        var testFile = tempDir.Info.RelativeFile("test.txt");
        using (var testWriter = testFile.CreateTextWriter())
        {
            await exeHello.redirect(testWriter);
        }

        testFile.ReadAllText().Should().Contain("hello");
    }

    [TestMethod()]
    public async Task input()
    {
        using var tempDir = new TempDir();
        var exeThrough = await tempDir.Info.MakeTestExeAsync("through", "Console.WriteLine(Console.ReadLine());");

        using var stdInReader = new StringReader("abc");
        var output = await exeThrough.input(stdInReader).result().output();
        output.Should().Contain("abc");
    }

    [TestMethod()]
    public async Task workdir()
    {
        using var tempDir = new TempDir();
        var exeCurName = await tempDir.Info.MakeTestExeAsync("cur-name", "Console.WriteLine(System.IO.Path.GetFileName(Environment.CurrentDirectory));");

        var output = await exeCurName.workdir(tempDir.Info.RelativeDirectory("zyx").WithCreate()).result().output();
        output.Should().Contain("zyx");
    }

    [TestMethod()]
    public async Task env()
    {
        using var tempDir = new TempDir();
        var exeEnvVal = await tempDir.Info.MakeTestExeAsync("env-val", """Console.WriteLine(Environment.GetEnvironmentVariable("ABC"));""");

        var output = await exeEnvVal.env("ABC", "test-env-val").result().output();
        output.Should().Contain("test-env-val");
    }

    [TestMethod()]
    public async Task killby()
    {
        using var tempDir = new TempDir();
        var exeWait = await tempDir.Info.MakeTestExeAsync("env-val", "System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);");

        using var canceller = new CancellationTokenSource();
        canceller.CancelAfter(1000);

        await FluentActions.Awaiting(async () => await exeWait.killby(canceller.Token))
            .Should().ThrowAsync<CmdProcCancelException>();
    }

    [TestMethod()]
    public async Task cancelby()
    {
        using var tempDir = new TempDir();
        var exeWait = await tempDir.Info.MakeTestExeAsync("env-val", "System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);");

        using var canceller = new CancellationTokenSource();
        canceller.CancelAfter(1000);

        await FluentActions.Awaiting(async () => await exeWait.cancelby(canceller.Token))
            .Should().ThrowAsync<OperationCanceledException>();
    }
}