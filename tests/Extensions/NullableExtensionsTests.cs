namespace LestalyTest.Extensions;

[TestClass]
public class NullableExtensionsTests
{
    [TestMethod]
    public void AddPrefer()
    {
        ((int?)null).AddPrefer(null).Should().BeNull();
        ((int?)null).AddPrefer((int?)1).Should().Be(1);
        ((int?)2).AddPrefer(null).Should().Be(2);
        ((int?)1).AddPrefer((int?)2).Should().Be(3);
    }

}
