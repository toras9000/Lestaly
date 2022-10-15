using FluentAssertions;
using Moq;

namespace Lestaly.Tests;

[TestClass()]
public class TryTests
{
    [TestMethod()]
    public void Action()
    {
        FluentActions.Invoking(() => Try.Action(() => { }))
            .Should().NotThrow().Which.Should().BeNull();

        FluentActions.Invoking(() => Try.Action(() => throw new InvalidDataException("aaa")))
            .Should().NotThrow().Which.Should().BeOfType<InvalidDataException>();

        var alternater = new Mock<Action<Exception>>();

        FluentActions.Invoking(() => Try.Action(() => { }, alternater.Object)).Should().NotThrow();
        alternater.Verify(m => m(It.IsAny<Exception>()), Times.Never());

        FluentActions.Invoking(() => Try.Action(() => throw new InvalidDataException("aaa"), alternater.Object)).Should().NotThrow();
        alternater.Verify(m => m(It.IsAny<InvalidDataException>()), Times.Once());
    }

    [TestMethod()]
    public async Task ActionAsync()
    {
        (await FluentActions.Awaiting(() => Try.ActionAsync(() => ValueTask.CompletedTask)).Should().NotThrowAsync())
            .Which.Should().BeNull();

        (await FluentActions.Awaiting(() => Try.ActionAsync(() => throw new InvalidDataException("aaa"))).Should().NotThrowAsync())
            .Which.Should().BeOfType<InvalidDataException>();

        var alternater = new Mock<Func<Exception, ValueTask>>();

        await FluentActions.Awaiting(() => Try.ActionAsync(() => ValueTask.CompletedTask, alternater.Object)).Should().NotThrowAsync();
        alternater.Verify(m => m(It.IsAny<Exception>()), Times.Never());

        await FluentActions.Awaiting(() => Try.ActionAsync(() => throw new InvalidDataException("aaa"), alternater.Object)).Should().NotThrowAsync();
        alternater.Verify(m => m(It.IsAny<InvalidDataException>()), Times.Once());
    }

    [TestMethod()]
    public void Func()
    {
        FluentActions.Invoking(() => Try.Func(() => 1, 5))
            .Should().NotThrow().Which.Should().Be((1, null));

        var err = new InvalidDataException("aaa");
        FluentActions.Invoking(() => Try.Func(() => throw err, 5))
            .Should().NotThrow().Which.Should().Be((5, err));

        FluentActions.Invoking(() => Try.Func(() => 1, ex => 9))
            .Should().NotThrow().Which.Should().Be(1);

        FluentActions.Invoking(() => Try.Func<int>(() => throw new InvalidDataException("aaa"), ex => 9))
            .Should().NotThrow().Which.Should().Be(9);
    }

    [TestMethod()]
    public async Task FuncAsync()
    {
        (await FluentActions.Awaiting(() => Try.FuncAsync(() => ValueTask.FromResult(1))).Should().NotThrowAsync())
            .Which.Should().Be((1, null));

        var err = new InvalidDataException("aaa");
        (await FluentActions.Awaiting(() => Try.FuncAsync(() => throw err, 5)).Should().NotThrowAsync())
            .Which.Should().Be((5, err));

        (await FluentActions.Awaiting(() => Try.FuncAsync(() => ValueTask.FromResult(2), ex => ValueTask.FromResult(7))).Should().NotThrowAsync())
            .Which.Should().Be(2);

        (await FluentActions.Awaiting(() => Try.FuncAsync(() => throw err, ex => ValueTask.FromResult(7))).Should().NotThrowAsync())
            .Which.Should().Be(7);
    }

}