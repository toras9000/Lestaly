using System.Text;
using Lestaly.Cx;

namespace LestalyTest.Cx;

[TestClass()]
public class CmdExtensionsTests
{
    [TestMethod()]
    public async Task methods()
    {
        // 可能な操作を列挙するためだけのメソッド

        // 直接実行系
        await "cmd /c exit 0";                                              // GetAwaiter()
        await "cmd /c exit 0".result();                                     // result()
        await "cmd /c exit 0".success();                                    // success()

        // 実行準備系
        await "cmd".args("/c", "exit 0");                                   // args()
        await "cmd /c exit 0".silent();                                     // silent()
        await "cmd /c exit 0".redirect(TextWriter.Null);                    // redirect()
        await "cmd /c exit 0".interactive();                                // interactive()
        await "cmd /c exit 0".input(TextReader.Null);                       // input()
        await "cmd /c exit 0".encoding(Encoding.UTF8);                      // encoding()
        await "cmd /c exit 0".workdir(ThisSource.File().DirectoryName!);    // workdir()
        await "cmd /c exit 0".env("VAR", "value");                          // env()
        await "cmd /c exit 0".killby(CancellationToken.None);               // killby()
        await "cmd /c exit 0".cancelby(CancellationToken.None);             // cancelby()
    }


    [TestMethod()]
    public async Task GetAwaiter_StringSimple()
    {
        (await "ipconfig").Should().Contain("DNS");
    }

    [TestMethod()]
    public async Task GetAwaiter_StringWithArg()
    {
        (await "ipconfig /all").Should().Contain("DHCP");
    }

    [TestMethod()]
    public async Task GetAwaiter_FailByExitCode()
    {
        await FluentActions.Awaiting(async () => await "cmd /c exit 0").Should().NotThrowAsync();
        await FluentActions.Awaiting(async () => await "cmd /c exit 1").Should().ThrowAsync<Exception>();
    }

    [TestMethod()]
    public async Task result()
    {
        (await "cmd /c echo abc".result()).Output.Should().Contain("abc");
        (await "cmd /c exit 0".result()).ExitCode.Should().Be(0);
        (await "cmd /c exit 1".result()).ExitCode.Should().Be(1);
    }

    [TestMethod()]
    public async Task result_NoFailByExitCode()
    {
        await FluentActions.Awaiting(() => "cmd /c exit 0".result()).Should().NotThrowAsync();
        await FluentActions.Awaiting(() => "cmd /c exit 1".result()).Should().NotThrowAsync();
    }

    [TestMethod()]
    public async Task success_commandline()
    {
        await FluentActions.Awaiting(() => "cmd /c exit 0".success()).Should().NotThrowAsync();
        await FluentActions.Awaiting(() => "cmd /c exit 1".success()).Should().ThrowAsync<Exception>();
        await FluentActions.Awaiting(() => "cmd /c exit 0".success(1)).Should().ThrowAsync<Exception>();
        await FluentActions.Awaiting(() => "cmd /c exit 1".success(1)).Should().NotThrowAsync();
    }

    [TestMethod()]
    public async Task args()
    {
        (await "cmd".args("/c", "echo", "abc")).Output.Should().Contain("abc");
        (await "cmd".args("/c", "exit", "0")).ExitCode.Should().Be(0);
        (await "cmd".args("/c", "exit", "1")).ExitCode.Should().Be(1);
    }

    [TestMethod()]
    public async Task args_NoFailByExitCode()
    {
        await FluentActions.Awaiting(async () => await "cmd".args("/c", "exit", "0")).Should().NotThrowAsync();
        await FluentActions.Awaiting(async () => await "cmd".args("/c", "exit", "1")).Should().NotThrowAsync();
    }

    [TestMethod()]
    public async Task silent()
    {
        // 検証できる事がないので例外が出ないことだけ見ておく

        await FluentActions.Awaiting(async () => await "cmd /C echo silent-test".silent())
            .Should().NotThrowAsync();
    }

    [TestMethod()]
    public async Task redirect()
    {
        using var tempDir = new TempDir();

        var testFile = tempDir.Info.RelativeFile("test.txt");
        using (var testWriter = testFile.CreateTextWriter())
        {
            await "cmd /C echo redirect-test".redirect(testWriter);
        }

        testFile.ReadAllText().Should().Contain("redirect-test");
    }

    [TestMethod()]
    public async Task interactive()
    {
        // 検証できる事がないので例外が出ないことだけ見ておく

        await FluentActions.Awaiting(async () => await "cmd /C echo silent-test".interactive())
            .Should().NotThrowAsync();
    }

    [TestMethod()]
    public async Task input()
    {
        using var canceller = new CancellationTokenSource();
        canceller.CancelAfter(3000);

        using var stdInReader = new StringReader(" ");

        await FluentActions.Awaiting(async () => await "cmd /C pause".input(stdInReader))
            .Should().NotThrowAsync();
    }

    [TestMethod()]
    public async Task encoding()
    {
        var jpenc = CodePagesEncodingProvider.Instance.GetEncoding("Shift_JIS")!;
        var output = await "cmd /C echo 日本語".encoding(jpenc);
        output.Output.Should().Contain("日本語");
    }

    [TestMethod()]
    public async Task workdir()
    {
        var workdir = SpecialFolder.Temporary();
        var output = await "cmd /C echo %CD%".workdir(workdir);
        output.Output.Should().Contain(workdir.FullName.TrimEnd('\\', '/'));
    }

    [TestMethod()]
    public async Task env()
    {
        var output = await "cmd /C echo %TESTENV%".env("TESTENV", "ENV-VAL");
        output.Output.Should().Contain("ENV-VAL");
    }

    [TestMethod()]
    public async Task killby()
    {
        using var canceller = new CancellationTokenSource();
        canceller.CancelAfter(3000);

        await FluentActions.Awaiting(async () => await "ping -t localhost".killby(canceller.Token))
            .Should().ThrowAsync<CmdProcCancelException>();
    }

    [TestMethod()]
    public async Task cancelby()
    {
        using var canceller = new CancellationTokenSource();
        canceller.CancelAfter(3000);

        await FluentActions.Awaiting(async () => await "ping -t localhost".cancelby(canceller.Token))
            .Should().ThrowAsync<OperationCanceledException>();
    }
}
