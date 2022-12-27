using FluentAssertions;
using Moq;

namespace Lestaly.Tests;

[TestClass()]
public class TryTests
{
    [TestMethod()]
    public void Action()
    {
        var err = new InvalidDataException("aaa");

        FluentActions.Invoking(() => Try.Action(() => { }))
            .Should().NotThrow().Which.Should().BeNull();

        FluentActions.Invoking(() => Try.Action(() => throw err))
            .Should().NotThrow().Which.Should().BeOfType<InvalidDataException>();
    }

    [TestMethod()]
    public void Action_Alt()
    {
        var err = new InvalidDataException("aaa");
        var alternater = new Mock<Action<Exception>>();

        FluentActions.Invoking(() => Try.Action(() => { }, alternater.Object)).Should().NotThrow();
        alternater.Verify(m => m(It.IsAny<Exception>()), Times.Never());

        FluentActions.Invoking(() => Try.Action(() => throw err, alternater.Object)).Should().NotThrow();
        alternater.Verify(m => m(It.IsAny<InvalidDataException>()), Times.Once());

        FluentActions.Invoking(() => Try.Action(() => throw err, ex => throw err)).Should().NotThrow();
    }

    [TestMethod()]
    public async Task ActionAsync()
    {
        var err = new InvalidDataException("aaa");

        (await FluentActions.Awaiting(() => Try.ActionAsync(() => ValueTask.CompletedTask)).Should().NotThrowAsync())
            .Which.Should().BeNull();

        (await FluentActions.Awaiting(() => Try.ActionAsync(() => throw err)).Should().NotThrowAsync())
            .Which.Should().BeOfType<InvalidDataException>();
    }

    [TestMethod()]
    public async Task ActionAsync_Alt()
    {
        var err = new InvalidDataException("aaa");
        var alternater = new Mock<Func<Exception, ValueTask>>();

        await FluentActions.Awaiting(() => Try.ActionAsync(() => ValueTask.CompletedTask, alternater.Object)).Should().NotThrowAsync();
        alternater.Verify(m => m(It.IsAny<Exception>()), Times.Never());

        await FluentActions.Awaiting(() => Try.ActionAsync(() => throw err, alternater.Object)).Should().NotThrowAsync();
        alternater.Verify(m => m(It.IsAny<InvalidDataException>()), Times.Once());

        await FluentActions.Awaiting(() => Try.ActionAsync(() => throw err, ex => throw err)).Should().NotThrowAsync();
    }

    [TestMethod()]
    public void Func_AltValue()
    {
        var err = new InvalidDataException("aaa");

        FluentActions.Invoking(() => Try.Func(() => 1, 5))
            .Should().NotThrow().Which.Should().Be((1, null));

        FluentActions.Invoking(() => Try.Func(() => throw err, 5))
            .Should().NotThrow().Which.Should().Be((5, err));

        FluentActions.Invoking(() => Try.Func(() => "a", "b"))
            .Should().NotThrow().Which.Should().Be(("a", null));

        FluentActions.Invoking(() => Try.Func(() => throw err, "b"))
            .Should().NotThrow().Which.Should().Be(("b", err));
    }

    [TestMethod()]
    public void Func_AltFunc()
    {
        var err = new InvalidDataException("aaa");

        FluentActions.Invoking(() => Try.Func(() => 1, ex => 9))
            .Should().NotThrow().Which.Should().Be(1);

        FluentActions.Invoking(() => Try.Func(() => throw err, ex => 9))
            .Should().NotThrow().Which.Should().Be(9);

        FluentActions.Invoking(() => Try.Func(() => throw err, ex => default(int)))
            .Should().NotThrow().Which.Should().Be(0);


        FluentActions.Invoking(() => Try.Func(() => "a", ex => "b"))
            .Should().NotThrow().Which.Should().Be("a");

        FluentActions.Invoking(() => Try.Func(() => throw err, ex => "b"))
            .Should().NotThrow().Which.Should().Be("b");

        FluentActions.Invoking(() => Try.Func(() => throw err, ex => default(string)))
            .Should().NotThrow().Which.Should().BeNull();
    }

    [TestMethod()]
    public void FuncOrDefault()
    {
        FluentActions.Invoking(() => Try.FuncOrDefault(() => 1))
            .Should().NotThrow().Which.Should().Be(1);

        FluentActions.Invoking(() => Try.FuncOrDefault(() => "a"))
            .Should().NotThrow().Which.Should().Be("a");

        var err = new InvalidDataException("aaa");
        FluentActions.Invoking(() => Try.FuncOrDefault<int>(() => throw err))
            .Should().NotThrow().Which.Should().Be(0);

        FluentActions.Invoking(() => Try.FuncOrDefault<string>(() => throw err))
            .Should().NotThrow().Which.Should().BeNull();
    }

    [TestMethod()]
    public async Task FuncAsync_AltValue()
    {
        var err = new InvalidDataException("aaa");

        (await FluentActions.Awaiting(() => Try.FuncAsync(() => ValueTask.FromResult(1))).Should().NotThrowAsync())
            .Which.Should().Be((1, null));

        (await FluentActions.Awaiting(() => Try.FuncAsync(() => throw err, 5)).Should().NotThrowAsync())
            .Which.Should().Be((5, err));

        (await FluentActions.Awaiting(() => Try.FuncAsync(() => ValueTask.FromResult("a"), "b")).Should().NotThrowAsync())
            .Which.Should().Be(("a", null));

        (await FluentActions.Awaiting(() => Try.FuncAsync(() => throw err, "b")).Should().NotThrowAsync())
            .Which.Should().Be(("b", err));
    }

    [TestMethod()]
    public async Task FuncAsync_AltFunc()
    {
        var err = new InvalidDataException("aaa");

        (await FluentActions.Awaiting(() => Try.FuncAsync(() => ValueTask.FromResult(2), ex => ValueTask.FromResult(7))).Should().NotThrowAsync())
            .Which.Should().Be(2);

        (await FluentActions.Awaiting(() => Try.FuncAsync(() => throw err, ex => ValueTask.FromResult(7))).Should().NotThrowAsync())
            .Which.Should().Be(7);

        (await FluentActions.Awaiting(() => Try.FuncAsync(() => throw err, ex => ValueTask.FromResult(default(int)))).Should().NotThrowAsync())
            .Which.Should().Be(0);


        (await FluentActions.Awaiting(() => Try.FuncAsync(() => ValueTask.FromResult("a"), ex => ValueTask.FromResult("b")!)).Should().NotThrowAsync())
            .Which.Should().Be("a");

        (await FluentActions.Awaiting(() => Try.FuncAsync(() => throw err, ex => ValueTask.FromResult("b")!)).Should().NotThrowAsync())
            .Which.Should().Be("b");

        (await FluentActions.Awaiting(() => Try.FuncAsync(() => throw err, ex => ValueTask.FromResult(default(string)))).Should().NotThrowAsync())
            .Which.Should().BeNull();
    }

    [TestMethod()]
    public async Task FuncOrDefaultAsync()
    {
        (await FluentActions.Awaiting(() => Try.FuncOrDefaultAsync(() => ValueTask.FromResult(2))).Should().NotThrowAsync())
            .Which.Should().Be(2);

        (await FluentActions.Awaiting(() => Try.FuncOrDefaultAsync(() => ValueTask.FromResult("a"))).Should().NotThrowAsync())
            .Which.Should().Be("a");

        var err = new InvalidDataException("aaa");

        (await FluentActions.Awaiting(() => Try.FuncOrDefaultAsync<int>(() => throw err)).Should().NotThrowAsync())
            .Which.Should().Be(0);

        (await FluentActions.Awaiting(() => Try.FuncOrDefaultAsync<string>(() => throw err)).Should().NotThrowAsync())
            .Which.Should().BeNull();
    }

}