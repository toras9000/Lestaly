namespace LestalyTest;

[TestClass]
public class SemaphoreFeederTests
{
    [TestMethod]
    public async Task WaitAsync_sequential()
    {
        using var feeder = new SemaphoreFeeder(1);

        using (var token = await feeder.WaitAsync()) { }
        using (var token = await feeder.WaitAsync()) { }
    }

    [TestMethod]
    public async Task WaitAsync_nest()
    {
        using var gate1 = new SemaphoreSlim(0);
        using var gate2 = new SemaphoreSlim(0);
        using var feeder = new SemaphoreFeeder(1);
        var list = new List<int>();

        var task1 = Task.Run(async () =>
        {
            using var token = await feeder.WaitAsync();
            await gate1.WaitAsync();
            list.Add(1);
        });

        await Task.Delay(100);

        var task2 = Task.Run(async () =>
        {
            using var token = await feeder.WaitAsync();
            await gate2.WaitAsync();
            list.Add(2);
        });

        gate2.Release();
        await Task.Delay(100);
        gate1.Release();

        await Task.WhenAll(task1, task2);
        list.Should().Equal(1, 2);
    }

    [TestMethod]
    public async Task WaitAsync_timeout()
    {
        using var feeder = new SemaphoreFeeder(1);

        using var token = await feeder.WaitAsync();

        using var nested = await feeder.WaitAsync(TimeSpan.FromMilliseconds(100));
        nested.Should().BeNull();
    }
}