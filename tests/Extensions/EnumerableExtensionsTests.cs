namespace LestalyTest.Extensions;

[TestClass()]
public class EnumerableExtensionsTests
{
    [TestMethod]
    public void WhereElse()
    {
        var source = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, };
        var skipped = new List<int>();

        source.WhereElse(n => n % 2 == 0, n => skipped.Add(n))
            .Should().Equal(new[] { 2, 4, 6, 8, });

        skipped.Should().Equal(new[] { 1, 3, 5, 7, 9, });
    }

    [TestMethod]
    public void CoalesceEmpty()
    {
        var source = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, };
        source.CoalesceEmpty().Should().BeSameAs(source);

        default(IEnumerable<int>).CoalesceEmpty().Should().NotBeNull().And.BeEmpty();
    }

    [TestMethod()]
    public void ErrorIfEmpty()
    {
        new[] { 1, }.ErrorIfEmpty().Should().BeEquivalentTo(new[] { 1, });

        FluentActions.Enumerating(() => Array.Empty<int>().ErrorIfEmpty(() => new ApplicationException("test-ex")))
            .Should().Throw<ApplicationException>().Where(ex => ex.Message == "test-ex");
    }

    [TestMethod()]
    public void Must()
    {
        new[] { 1, 2, 3, }.Must(n => n < 10).Should().BeEquivalentTo(new[] { 1, 2, 3, }, o => o.WithStrictOrdering());

        FluentActions.Enumerating(() => new[] { 1, 2, 10, }.Must(n => n < 10, () => new ApplicationException("test-ex")))
            .Should().Throw<ApplicationException>().Where(ex => ex.Message == "test-ex");
    }

    [TestMethod]
    public void Repetition()
    {
        var source = new[] { 1, 2, 3, }.AsEnumerable();
        source.Repetition(3).Should().Equal([1, 2, 3, 1, 2, 3, 1, 2, 3,]);
        source.Repetition(0).Should().BeEmpty();

        new Action(() => source.Repetition(-1)).Should().Throw<Exception>();
    }

    [TestMethod()]
    public void RoughContains()
    {
        IEnumerable<string> texts = ["abc", "def", "ghi"];
        texts.RoughContains("abc").Should().Be(true);
        texts.RoughContains("ABC").Should().Be(true);
        texts.RoughContains(" \t\r\nABC\t\r\n  ").Should().Be(true);
        texts.RoughContains("abb").Should().Be(false);
        texts.RoughContains("ab c").Should().Be(false);
    }

    [TestMethod()]
    public void Deconstruct2()
    {
        (var ve1, var ve2) = Enumerable.Range(100, 100);
        ve1.Should().Be(100);
        ve2.Should().Be(101);

        (var vl1, var vl2) = Enumerable.Range(200, 1);
        vl1.Should().Be(200);
        vl2.Should().Be(0);

        (var re1, var re2) = Enumerable.Range(100, 100).Select(v => v.ToString());
        re1.Should().Be("100");
        re2.Should().Be("101");

        (var rl1, var rl2) = Enumerable.Range(100, 1).Select(v => v.ToString());
        rl1.Should().Be("100");
        rl2.Should().BeNull();
    }

    [TestMethod()]
    public void Deconstruct3()
    {
        (var ve1, var ve2, var ve3) = Enumerable.Range(100, 100);
        ve1.Should().Be(100);
        ve2.Should().Be(101);
        ve3.Should().Be(102);

        (var vl1, var vl2, var vl3) = Enumerable.Range(200, 1);
        vl1.Should().Be(200);
        vl2.Should().Be(0);
        vl3.Should().Be(0);

        (var re1, var re2, var re3) = Enumerable.Range(100, 100).Select(v => v.ToString());
        re1.Should().Be("100");
        re2.Should().Be("101");
        re3.Should().Be("102");

        (var rl1, var rl2, var rl3) = Enumerable.Range(100, 1).Select(v => v.ToString());
        rl1.Should().Be("100");
        rl2.Should().BeNull();
        rl3.Should().BeNull();
    }

    [TestMethod()]
    public void DeconstructM()
    {
        {
            (var v1, var v2, var v3, var v4) = Enumerable.Range(100, 100);
            v1.Should().Be(100);
            v2.Should().Be(101);
            v3.Should().Be(102);
            v4.Should().Be(103);
        }

        {
            (var v1, var v2, var v3, var v4, var v5) = Enumerable.Range(100, 100);
            v1.Should().Be(100);
            v2.Should().Be(101);
            v3.Should().Be(102);
            v4.Should().Be(103);
            v5.Should().Be(104);
        }
    }

    [TestMethod()]
    public async Task ToPseudoAsyncEnumerable()
    {
        var data = new[] { 1, 3, 5, 7, };

        var tid = Environment.CurrentManagedThreadId;

        var actual = new List<int>();
        await foreach (var item in data.ToPseudoAsyncEnumerable())
        {
            actual.Add(item);
            Environment.CurrentManagedThreadId.Should().Be(tid);
        }

        actual.Should().Equal(data);
    }

}
