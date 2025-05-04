namespace LestalyTest.Extensions;

[TestClass]
public class CancellationTokenSourceExtensionsTests
{
    [TestMethod]
    public async Task CreateLink()
    {
        {
            using var breaker = new CancellationTokenSource();
            using var linked = breaker.Token.CreateLink(TimeSpan.FromMilliseconds(500));
            await FluentActions.Awaiting(() => Task.Delay(1000, linked.Token)).Should()
                .ThrowAsync<OperationCanceledException>()
                .Where(ex => ex.CancellationToken.Equals(linked.Token));
        }

        {
            using var source1 = new CancellationTokenSource();
            using var source2 = new CancellationTokenSource();
            using var linked = source1.Token.CreateLink(source2.Token);
            source2.Cancel();
            await FluentActions.Awaiting(() => Task.Delay(1000, linked.Token)).Should()
                .ThrowAsync<OperationCanceledException>()
                .Where(ex => ex.CancellationToken.Equals(linked.Token));
        }
    }
}
