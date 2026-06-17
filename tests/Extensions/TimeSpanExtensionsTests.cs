namespace LestalyTest.Extensions;

[TestClass]
public class TimeSpanExtensionsTests
{
    [TestMethod]
    public void ToSimple()
    {
        new TimeSpan(1, 2, 3).ToSimple().Should().Be("1:02:03");
        new TimeSpan(12, 23, 34).ToSimple().Should().Be("12:23:34");
        new TimeSpan(234, 34, 45).ToSimple().Should().Be("234:34:45");
    }
}
