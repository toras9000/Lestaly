using System.Reactive.Disposables;

namespace LestalyTest.Extensions;

[TestClass()]
public class EnumerableDataExtensions_ToParallel_Tests
{
    [TestMethod]
    public async Task ToParallelAsync_ParallelNum()
    {
        var actives = new HashSet<int>();
        var history = new List<int>();
        IDisposable makePeriod(int n)
        {
            lock (actives!)
            {
                actives.Add(n);
                history!.Add(actives.Count);
            }
            return Disposable.Create(() =>
            {
                lock (actives)
                {
                    actives.Remove(n);
                    history.Add(actives.Count);
                }
            });
        }

        await Enumerable.Range(0, 10)
            .ToParallelAsync(parallels: 4, ordered: false, async n =>
            {
                using var period = makePeriod(n);
                await Task.Delay(100);
                return n;
            })
            .ToArrayAsync();

        history.Max().Should().Be(4);
    }

    [TestMethod]
    public async Task ToParallelAsync_NoOrder()
    {
        var source = Enumerable.Range(0, 10).ToArray();
        var results = await source
            .ToParallelAsync(parallels: 4, ordered: false, async n =>
            {
                await Task.Delay(1100 - (n * 100));
                return n;
            })
            .ToArrayAsync();

        results.Should().NotEqual(source);
    }

    [TestMethod]
    public async Task ToParallelAsync_Ordered()
    {
        var source = Enumerable.Range(0, 10).ToArray();
        var results = await source
            .ToParallelAsync(parallels: 4, ordered: true, async n =>
            {
                await Task.Delay(1100 - (n * 100));
                return n;
            })
            .ToArrayAsync();

        results.Should().Equal(source);
    }
}
