using FluentAssertions;

namespace LestalyTest.Extensions;

[TestClass()]
public class EnumerableExtensionsTests
{
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
}
