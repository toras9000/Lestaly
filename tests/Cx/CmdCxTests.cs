using System.Text;
using Lestaly.Cx;

namespace LestalyTest.Cx;

[TestClass()]
public class CmdCxTests
{
    [TestMethod()]
    public async Task methods()
    {
        // 可能な操作を列挙するためだけのメソッド

        // インスタンスを直接作るのもあり
        await new CmdCx("cmd /c exit 0");
        await new CmdCx("cmd", "/c", "exit 0");

        // しかし文字列の実行準備系拡張メソッドから作る方が書きやすいと思われる
        await "cmd /c exit 0".silent();                         // silent()
        await "cmd".args("/c", "exit 0");                       // args()

        // インスタンスの作成後は準備メソッドを任意に連結し、最後に result() または await で実行を行う。
        await "cmd".args("/c", "exit 0").silent();                          // silent()
        await "cmd".args("/c", "exit 0").redirect(TextWriter.Null);         // redirect()
        await "cmd".args("/c", "exit 0").interactive();                     // interactive()
        await "cmd".args("/c", "exit 0").input(TextReader.Null);            // input()
        await "cmd".args("/c", "exit 0").encoding(Encoding.UTF8);           // encoding()
        await "cmd".args("/c", "exit 0").env("VAR", "value");               // env()
        await "cmd".args("/c", "exit 0").killby(CancellationToken.None);    // killby()
    }


    [TestMethod()]
    public async Task GetAwaiter_StringSimple()
    {
        (await new CmdCx("ipconfig")).Output.Should().Contain("DNS");
    }

    [TestMethod()]
    public async Task GetAwaiter_StringWithArg()
    {
        (await new CmdCx("ipconfig /all")).Output.Should().Contain("DHCP");
    }

    [TestMethod()]
    public async Task GetAwaiter_NoFailByExitCode()
    {
        await FluentActions.Awaiting(async () => await new CmdCx("cmd /c exit 0")).Should().NotThrowAsync();
        await FluentActions.Awaiting(async () => await new CmdCx("cmd /c exit 1")).Should().NotThrowAsync();
    }

    [TestMethod()]
    public async Task result()
    {
        (await new CmdCx("cmd /c echo abc").result()).Output.Should().Contain("abc");
        (await new CmdCx("cmd /c exit 0").result()).ExitCode.Should().Be(0);
        (await new CmdCx("cmd /c exit 1").result()).ExitCode.Should().Be(1);
    }

    [TestMethod()]
    public async Task result_NoFailByExitCode()
    {
        await FluentActions.Awaiting(() => new CmdCx("cmd /c exit 0").result()).Should().NotThrowAsync();
        await FluentActions.Awaiting(() => new CmdCx("cmd /c exit 1").result()).Should().NotThrowAsync();
    }

    [TestMethod()]
    public async Task silent()
    {
        // 検証できる事がないので例外が出ないことだけ見ておく

        await FluentActions.Awaiting(async () => await new CmdCx("cmd /C echo silent-test").silent())
            .Should().NotThrowAsync();
    }

    [TestMethod()]
    public async Task redirect()
    {
        using var tempDir = new TempDir();

        var testFile = tempDir.Info.RelativeFile("test.txt");
        using (var testWriter = testFile.CreateTextWriter())
        {
            await new CmdCx("cmd /C echo redirect-test").redirect(testWriter);
        }

        testFile.ReadAllText().Should().Contain("redirect-test");
    }

    [TestMethod()]
    public async Task interactive()
    {
        // 検証できる事がないので例外が出ないことだけ見ておく

        await FluentActions.Awaiting(async () => await new CmdCx("cmd /C echo silent-test").interactive())
            .Should().NotThrowAsync();
    }

    [TestMethod()]
    public async Task input()
    {
        using var canceller = new CancellationTokenSource();
        canceller.CancelAfter(3000);

        using var stdInReader = new StringReader(" ");

        await FluentActions.Awaiting(async () => await new CmdCx("cmd /C pause").input(stdInReader))
            .Should().NotThrowAsync();
    }

    [TestMethod()]
    public async Task encoding()
    {
        var jpenc = CodePagesEncodingProvider.Instance.GetEncoding("Shift_JIS")!;
        var output = await new CmdCx("cmd /C echo 日本語").encoding(jpenc);
        output.Output.Should().Contain("日本語");
    }

    [TestMethod()]
    public async Task env()
    {
        var output = await new CmdCx("cmd /C echo %TESTENV%").env("TESTENV", "ENV-VAL");
        output.Output.Should().Contain("ENV-VAL");
    }

    [TestMethod()]
    public async Task killby()
    {
        using var canceller = new CancellationTokenSource();
        canceller.CancelAfter(3000);

        await FluentActions.Awaiting(async () => await new CmdCx("ping -t localhost").killby(canceller.Token))
            .Should().ThrowAsync<CmdProcCancelException>();
    }

    [TestMethod()]
    public async Task combination()
    {
        using var tempDir = new TempDir();

        var testFile = tempDir.Info.RelativeFile("test.txt");
        using (var testWriter = testFile.CreateTextWriter())
        {
            var jpenc = CodePagesEncodingProvider.Instance.GetEncoding("Shift_JIS")!;
            await new CmdCx("cmd /C echo combination-%ENV1%-%ENV2%-test")
                .encoding(jpenc)
                .env("ENV1", "日本語")
                .env("ENV2", "ABC")
                .redirect(testWriter);
        }

        testFile.ReadAllText().Should().Contain("combination-日本語-ABC-test");
    }
}
