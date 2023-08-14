using FluentAssertions;

namespace LestalyTest.Extensions;

[TestClass()]
public class DateTimeExtensionsTests
{
    [TestMethod()]
    public void AtTime()
    {
        var utc = new DateTime(2023, 4, 5, 6, 7, 8, 9, DateTimeKind.Utc);
        utc.AtTime(10, 11, 12, 13).Should().Be(new DateTime(2023, 4, 5, 10, 11, 12, 13, DateTimeKind.Utc));
        utc.AtTime(new TimeOnly(10, 11, 12, 13)).Should().Be(new DateTime(2023, 4, 5, 10, 11, 12, 13, DateTimeKind.Utc));

        var local = new DateTime(2023, 4, 5, 6, 7, 8, 9, DateTimeKind.Local);
        local.AtTime(10, 11, 12, 13).Should().Be(new DateTime(2023, 4, 5, 10, 11, 12, 13, DateTimeKind.Local));
        local.AtTime(new TimeOnly(10, 11, 12, 13)).Should().Be(new DateTime(2023, 4, 5, 10, 11, 12, 13, DateTimeKind.Local));
    }

    [TestMethod()]
    public void ToDateOnly()
    {
        new DateTime(2023, 4, 5, 6, 7, 8, 9, DateTimeKind.Utc).ToDateOnly().Should().Be(new DateOnly(2023, 4, 5));
        new DateTime(2023, 4, 5, 6, 7, 8, 9, DateTimeKind.Local).ToDateOnly().Should().Be(new DateOnly(2023, 4, 5));
    }

    [TestMethod()]
    public void ToTimeOnly()
    {
        new DateTime(2023, 4, 5, 6, 7, 8, 9, DateTimeKind.Utc).ToTimeOnly().Should().Be(new TimeOnly(6, 7, 8, 9));
        new DateTime(2023, 4, 5, 6, 7, 8, 9, DateTimeKind.Local).ToTimeOnly().Should().Be(new TimeOnly(6, 7, 8, 9));
    }
}
