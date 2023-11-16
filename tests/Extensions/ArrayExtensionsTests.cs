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

}
