using Lestaly.Cx;

namespace LestalyTest.Cx;

[TestClass()]
public class CmdExtensionsTests
{
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
    public async Task GetAwaiter_ExitCode()
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
    public async Task result_ExitCode()
    {
        await FluentActions.Awaiting(() => "cmd /c exit 0".result()).Should().NotThrowAsync();
        await FluentActions.Awaiting(() => "cmd /c exit 1".result()).Should().NotThrowAsync();
    }

    [TestMethod()]
    public async Task args()
    {
        (await "cmd".args("/c", "echo", "abc")).Output.Should().Contain("abc");
        (await "cmd".args("/c", "exit", "0")).ExitCode.Should().Be(0);
        (await "cmd".args("/c", "exit", "1")).ExitCode.Should().Be(1);
    }

    [TestMethod()]
    public async Task args_ExitCode()
    {
        await FluentActions.Awaiting(() => "cmd".args("/c", "exit", "0")).Should().NotThrowAsync();
        await FluentActions.Awaiting(() => "cmd".args("/c", "exit", "1")).Should().NotThrowAsync();
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
    public async Task success_result()
    {
        await FluentActions.Awaiting(() => "cmd /c exit 0".result().success()).Should().NotThrowAsync();
        await FluentActions.Awaiting(() => "cmd /c exit 1".result().success()).Should().ThrowAsync<Exception>();
        await FluentActions.Awaiting(() => "cmd /c exit 0".result().success(1)).Should().ThrowAsync<Exception>();
        await FluentActions.Awaiting(() => "cmd /c exit 1".result().success(1)).Should().NotThrowAsync();
    }

    [TestMethod()]
    public async Task code()
    {
        (await "cmd /c exit 0".result().code()).Should().Be(0);
        (await "cmd /c exit 1".result().code()).Should().Be(1);
    }

    [TestMethod()]
    public async Task output()
    {
        (await "cmd /c echo abc".result().output()).Should().Contain("abc");
        (await "cmd /c echo def".result().output()).Should().Contain("def");
    }
}
