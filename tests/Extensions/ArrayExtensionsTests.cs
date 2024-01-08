namespace LestalyTest.Extensions;

[TestClass]
public class ArrayExtensionsTests
{
    [TestMethod]
    public void AsReadOnly()
    {
        var source = new[] { 1, 2, 3, };
        var wrapper = source.AsReadOnly();
        wrapper.Should().Equal(1, 2, 3);

        source[0] = 5;
        wrapper.Should().Equal(5, 2, 3);
    }

    [TestMethod]
    public void Repetition()
    {
        var source = new[] { 1, 2, 3, };
        source.CreateRepeat(3).Should().Equal([1, 2, 3, 1, 2, 3, 1, 2, 3,]);
        source.CreateRepeat(0).Should().BeEmpty();

        new Action(() => source.CreateRepeat(-1)).Should().Throw<Exception>();
    }

    [TestMethod]
    public void FillBy()
    {
        var source = new[] { 1, 2, 3, };
        source.FillBy(9).Should().BeSameAs(source).And.AllBeEquivalentTo(9);

        Array.Empty<int>().FillBy(10).Should().BeEmpty();
    }
}
