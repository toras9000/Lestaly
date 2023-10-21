using Lestaly.Cx;

namespace LestalyTest.Cx;

[TestClass()]
public class CmdResultExtensionsTests
{
    [TestMethod()]
    public async Task success()
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
