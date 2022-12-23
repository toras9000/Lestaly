using FluentAssertions;

namespace LestalyTest.Extensions;

[TestClass()]
public class StringExtensionsTests
{
    [TestMethod()]
    public void DropEmpty()
    {
        new[] { "", "abc", null, null, "  ", "", "def", null, }
            .DropEmpty().Should().Equal(new[] { "abc", "  ", "def", });
    }

    [TestMethod()]
    public void DropWhite()
    {
        new[] { "", "abc", null, null, "  ", "", "def", null, }
            .DropWhite().Should().Equal(new[] { "abc", "def", });
    }

    [TestMethod()]
    public void DecoratePrefix()
    {
        "abc".DecoratePrefix("xxx").Should().Be("xxxabc");
        "".DecoratePrefix("xxx").Should().Be("");
        default(string).DecoratePrefix("xxx").Should().Be(null);
    }

    [TestMethod()]
    public void DecorateSuffix()
    {
        "abc".DecorateSuffix("xxx").Should().Be("abcxxx");
        "".DecorateSuffix("xxx").Should().Be("");
        default(string).DecorateSuffix("xxx").Should().Be(null);
    }

    [TestMethod()]
    public void TieIn()
    {
        "abc".TieIn("xxx").Should().Be("abcxxx");
        "abc".TieIn("").Should().Be("");
        "abc".TieIn(null).Should().Be("");
        "".TieIn("xxx").Should().Be("");
        default(string).TieIn("xxx").Should().Be("");
    }
}
