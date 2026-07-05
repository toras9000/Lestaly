namespace LestalyTest.Extensions;

[TestClass()]
public class RandomExtensionsTests
{
    [TestMethod()]
    public void GetBytes()
    {
        Random.Shared.GetBytes(10).Should().HaveCount(10);
    }

    [TestMethod()]
    public void Type_RandomRange()
    {
        new RandomRange(0, 100).Should().BeEquivalentTo(new
        {
            Start = 0,
            Count = 100,
            End = 100,
            Last = 99,
        });
        new RandomRange(20, 100).Should().BeEquivalentTo(new
        {
            Start = 20,
            Count = 100,
            End = 120,
            Last = 119,
        });
        new RandomRange(int.MaxValue - 2, 2).Should().BeEquivalentTo(new
        {
            Start = int.MaxValue - 2,
            Count = 2,
            End = int.MaxValue,
            Last = int.MaxValue - 1,
        });

        RandomRange.WithCount(100, 200).Should().BeEquivalentTo(new
        {
            Start = 100,
            Count = 200,
            End = 300,
            Last = 299,
        });

        RandomRange.WithEnd(100, 200).Should().BeEquivalentTo(new
        {
            Start = 100,
            Count = 100,
            End = 200,
            Last = 199,
        });

        RandomRange.WithLast(100, 200).Should().BeEquivalentTo(new
        {
            Start = 100,
            Count = 101,
            End = 201,
            Last = 200,
        });

        FluentActions.Invoking(() => new RandomRange(int.MaxValue, 1)).Should().Throw<Exception>();
        FluentActions.Invoking(() => new RandomRange(0, int.MaxValue)).Should().NotThrow();
        FluentActions.Invoking(() => new RandomRange(1, int.MaxValue)).Should().Throw<Exception>();
        FluentActions.Invoking(() => new RandomRange(1, 0)).Should().Throw<Exception>();
        FluentActions.Invoking(() => RandomRange.WithEnd(5, 5)).Should().Throw<Exception>();
        FluentActions.Invoking(() => RandomRange.WithEnd(5, 4)).Should().Throw<Exception>();
        FluentActions.Invoking(() => RandomRange.WithEnd(0, int.MaxValue)).Should().NotThrow();
    }

    [TestMethod()]
    public void GetNumber()
    {
        for (var i = 0; i < 100; i++)
        {
            Random.Shared.GetNumber(new(10, 5)).Should().BeInRange(10, 14);
        }

        var numbers = Enumerable.Range(0, 10000).Select(_ => Random.Shared.GetNumber(new(0, 5))).ToArray();
        numbers.Should().Contain(0);
        numbers.Should().Contain(1);
        numbers.Should().Contain(2);
        numbers.Should().Contain(3);
        numbers.Should().Contain(4);
        numbers.Should().OnlyContain(n => 0 <= n && n < 15);
    }

    [TestMethod()]
    public void Pick()
    {
        var items = Enumerable.Range(1, 200).Select(n => n * 3).ToArray();
        Random.Shared.Pick(items).Should().BeOneOf(items);
        Random.Shared.Pick(items).Should().BeOneOf(items);
        Random.Shared.Pick(items).Should().BeOneOf(items);
    }

    [TestMethod()]
    public void PickItems()
    {
        var items = Enumerable.Range(1, 100).Select(n => n * 3).ToArray();

        Random.Shared.PickItems(items, 10).Should().HaveCount(10).And.BeSubsetOf(items).And.OnlyHaveUniqueItems();
        Random.Shared.PickItems(items, 99).Should().HaveCount(99).And.BeSubsetOf(items).And.OnlyHaveUniqueItems();
        Random.Shared.PickItems(items, 100).Should().HaveCount(100).And.BeSubsetOf(items).And.OnlyHaveUniqueItems();
        Random.Shared.PickItems(items, 101).Should().HaveCount(100).And.BeSubsetOf(items).And.OnlyHaveUniqueItems();
    }

    [TestMethod()]
    public void RemoveItems()
    {
        var items = Enumerable.Range(1, 100).Select(n => n * 3).ToList();

        var picked = Random.Shared.RemoveItems(items, 10);
        picked.Should().HaveCount(10);
        items.Should().HaveCount(90);
        picked.Should().NotIntersectWith(items);
    }

    [TestMethod()]
    public void PassRate()
    {
        var items = Enumerable.Range(1, 100).Select(n => n * 3).ToArray();

        var passed1 = Random.Shared.PassRate(items, 0.3).ToArray();
        passed1.Should().HaveCountLessThan(50);

        var passed2 = items.PassRate(0.3, Random.Shared).ToArray();
        passed2.Should().HaveCountLessThan(50);

        var passed3 = items.PassRate(0.3).ToArray();
        passed3.Should().HaveCountLessThan(50);
    }

    [TestMethod()]
    public void Choice()
    {
        var options = new[]
        {
            OptionWeight.Create(DateTimeKind.Unspecified, 10),
            OptionWeight.Create(DateTimeKind.Utc, 20),
            OptionWeight.Create(DateTimeKind.Local, 30),
        };

        var choices = Enumerable.Range(0, 10000)
            .Select(_ => Random.Shared.Choice(options))
            .ToLookup(v => v);
        choices.Count.Should().Be(3);

        var unspecCount = choices[DateTimeKind.Unspecified].Count();
        var utfCount = choices[DateTimeKind.Utc].Count();
        var localCount = choices[DateTimeKind.Local].Count();
        unspecCount.Should().NotBe(0).And.BeLessThan(utfCount).And.BeLessThan(localCount);
        utfCount.Should().NotBe(0).And.BeLessThan(localCount);
        localCount.Should().NotBe(0);
    }

}
