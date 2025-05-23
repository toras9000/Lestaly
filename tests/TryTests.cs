﻿namespace LestalyTest;

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
        alternater.Verify(m => m(It.IsAny<InvalidDataException>()), Moq.Times.Once());

        FluentActions.Invoking(() => Try.Action(() => throw err, ex => throw err)).Should().NotThrow();
    }

    [TestMethod()]
    public async Task ActionAsync()
    {
        var err = new InvalidDataException("aaa");

        (await FluentActions.Awaiting(() => Try.ActionAsync(() => Task.CompletedTask)).Should().NotThrowAsync())
            .Which.Should().BeNull();

        (await FluentActions.Awaiting(() => Try.ActionAsync(() => throw err)).Should().NotThrowAsync())
            .Which.Should().BeOfType<InvalidDataException>();
    }

    [TestMethod()]
    public async Task ActionAsync_Alt()
    {
        var err = new InvalidDataException("aaa");
        var alternater = new Mock<Func<Exception, Task>>();

        await FluentActions.Awaiting(() => Try.ActionAsync(() => Task.CompletedTask, alternater.Object)).Should().NotThrowAsync();
        alternater.Verify(m => m(It.IsAny<Exception>()), Moq.Times.Never());

        await FluentActions.Awaiting(() => Try.ActionAsync(() => throw err, alternater.Object)).Should().NotThrowAsync();
        alternater.Verify(m => m(It.IsAny<InvalidDataException>()), Moq.Times.Once());

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

        (await FluentActions.Awaiting(() => Try.FuncAsync(() => Task.FromResult(1))).Should().NotThrowAsync())
            .Which.Should().Be((1, null));

        (await FluentActions.Awaiting(() => Try.FuncAsync(() => throw err, 5)).Should().NotThrowAsync())
            .Which.Should().Be((5, err));

        (await FluentActions.Awaiting(() => Try.FuncAsync(() => Task.FromResult("a").AsNullable(), "b")).Should().NotThrowAsync())
            .Which.Should().Be(("a", null));

        (await FluentActions.Awaiting(() => Try.FuncAsync(() => throw err, "b")).Should().NotThrowAsync())
            .Which.Should().Be(("b", err));
    }

    [TestMethod()]
    public async Task FuncAsync_AltFunc()
    {
        var err = new InvalidDataException("aaa");

        (await FluentActions.Awaiting(() => Try.FuncAsync(() => Task.FromResult(2), ex => Task.FromResult(7))).Should().NotThrowAsync())
            .Which.Should().Be(2);

        (await FluentActions.Awaiting(() => Try.FuncAsync(() => throw err, ex => Task.FromResult(7))).Should().NotThrowAsync())
            .Which.Should().Be(7);

        (await FluentActions.Awaiting(() => Try.FuncAsync(() => throw err, ex => Task.FromResult(default(int)))).Should().NotThrowAsync())
            .Which.Should().Be(0);


        (await FluentActions.Awaiting(() => Try.FuncAsync(() => Task.FromResult("a").AsNullable(), ex => Task.FromResult("b").AsNullable())).Should().NotThrowAsync())
            .Which.Should().Be("a");

        (await FluentActions.Awaiting(() => Try.FuncAsync(() => throw err, ex => Task.FromResult("b").AsNullable())).Should().NotThrowAsync())
            .Which.Should().Be("b");

        (await FluentActions.Awaiting(() => Try.FuncAsync(() => throw err, ex => Task.FromResult(default(string)))).Should().NotThrowAsync())
            .Which.Should().BeNull();
    }

    [TestMethod()]
    public async Task FuncOrDefaultAsync()
    {
        (await FluentActions.Awaiting(() => Try.FuncOrDefaultAsync(() => Task.FromResult(2))).Should().NotThrowAsync())
            .Which.Should().Be(2);

        (await FluentActions.Awaiting(() => Try.FuncOrDefaultAsync(() => Task.FromResult("a").AsNullable())).Should().NotThrowAsync())
            .Which.Should().Be("a");

        var err = new InvalidDataException("aaa");

        (await FluentActions.Awaiting(() => Try.FuncOrDefaultAsync<int>(() => throw err)).Should().NotThrowAsync())
            .Which.Should().Be(0);

        (await FluentActions.Awaiting(() => Try.FuncOrDefaultAsync<string>(() => throw err)).Should().NotThrowAsync())
            .Which.Should().BeNull();
    }

    [TestMethod()]
    public void TryTimes()
    {
        Try.Times(3, TimeSpan.FromMicroseconds(1), (n) => (n == 2) ? 100 : throw new Exception()).Should().Be(100);

        FluentActions.Invoking(() => Try.Times(3, n => n < 0 ? 0 : throw new Exception())).Should().Throw<Exception>();
    }

    [TestMethod()]
    public async Task TimesAsync()
    {
        (await Try.TimesAsync(3, TimeSpan.FromMicroseconds(1), (n) => (n == 2) ? ValueTask.FromResult(100) : throw new Exception())).Should().Be(100);

        await FluentActions.Awaiting(async () => await Try.TimesAsync(3, _ => ValueTask.FromException<int>(new Exception()))).Should().ThrowAsync<Exception>();
    }

}