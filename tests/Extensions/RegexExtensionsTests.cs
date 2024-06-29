using System.Text.RegularExpressions;

namespace LestalyTest.Extensions;

[TestClass()]
public class RegexExtensionsTests
{
    [TestMethod()]
    public void MatchSelect()
    {
        new Regex(@"\d").MatchSelect("0", m => "@", null).Should().Be("@");
        new Regex(@"\d").MatchSelect("0", m => "@", "x").Should().Be("@");
        new Regex(@"\d").MatchSelect("a", m => "@", null).Should().BeNull();
        new Regex(@"\d").MatchSelect("a", m => "@", "x").Should().Be("x");
    }


}
