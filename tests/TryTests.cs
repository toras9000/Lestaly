namespace LestalyTest;

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
    public void Take()
    {
        FluentActions.Invoking(() => Try.Take(() => 1))
            .Should().NotThrow().Which.Should().Be(1);

        FluentActions.Invoking(() => Try.Take(() => "a"))
            .Should().NotThrow().Which.Should().Be("a");

        var err = new InvalidDataException("aaa");
        FluentActions.Invoking(() => Try.Take<int>(() => throw err))
            .Should().NotThrow().Which.Should().Be(0);

        FluentActions.Invoking(() => Try.Take<string>(() => throw err))
            .Should().NotThrow().Which.Should().BeNull();
    }

    [TestMethod()]
    public async Task TakeAsync()
    {
        (await FluentActions.Awaiting(() => Try.TakeAsync(() => Task.FromResult(2)).AsTask()).Should().NotThrowAsync())
            .Which.Should().Be(2);

        (await FluentActions.Awaiting(() => Try.TakeAsync(() => Task.FromResult("a").AsNullable()).AsTask()).Should().NotThrowAsync())
            .Which.Should().Be("a");

        var err = new InvalidDataException("aaa");

        (await FluentActions.Awaiting(() => Try.TakeAsync<int>(() => throw err).AsTask()).Should().NotThrowAsync())
            .Which.Should().Be(0);

        (await FluentActions.Awaiting(() => Try.TakeAsync<string>(() => throw err).AsTask()).Should().NotThrowAsync())
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