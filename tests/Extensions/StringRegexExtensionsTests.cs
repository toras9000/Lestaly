using System.Text.RegularExpressions;
using FluentAssertions;

namespace LestalyTest.Extensions;

[TestClass()]
public class StringRegexExtensionsTests
{
    [TestMethod()]
    public void IsMatch()
    {
        "0".IsMatch(@"\d").Should().BeTrue();
        "a".IsMatch(@"\d").Should().BeFalse();
        "a".IsMatch(@"A", RegexOptions.None).Should().BeFalse();
        "a".IsMatch(@"A", RegexOptions.IgnoreCase).Should().BeTrue();
    }

    [TestMethod()]
    public void Match()
    {
        "0".Match(@"\d").Success.Should().BeTrue();
        "a".Match(@"\d").Success.Should().BeFalse();
        "a".Match(@"A", RegexOptions.None).Success.Should().BeFalse();
        "a".Match(@"A", RegexOptions.IgnoreCase).Success.Should().BeTrue();
    }

    [TestMethod()]
    public void Matches()
    {
        "<abc>xx<def>yy<ghi>".Matches(@"<\w+>").Select(m => m.Captures[0].Value).Should().Equal("<abc>", "<def>", "<ghi>");
        "<xxx>yy<XXX>zz<zzz>".Matches(@"xxx", RegexOptions.None).Select(m => m.Captures[0].Value).Should().Equal("xxx");
        "<xxx>yy<XXX>zz<zzz>".Matches(@"xxx", RegexOptions.IgnoreCase).Select(m => m.Captures[0].Value).Should().Equal("xxx", "XXX");
    }

    [TestMethod()]
    public void MatchReplace()
    {
        "0".MatchReplace(@"\d", "@").Should().Be("@");
        "a".MatchReplace(@"\d", "@").Should().Be("a");
        "a".MatchReplace(@"A", "@", RegexOptions.None).Should().Be("a");
        "a".MatchReplace(@"A", "@", RegexOptions.IgnoreCase).Should().Be("@");
    }

    [TestMethod()]
    public void MatchSplit()
    {
        "abc<>def<>ghi".MatchSplit(@"<>").Should().Equal("abc", "def", "ghi");
        "aaaxxbbbXXccc".MatchSplit(@"xx", RegexOptions.None).Should().Equal("aaa", "bbbXXccc");
        "aaaxxbbbXXccc".MatchSplit(@"xx", RegexOptions.IgnoreCase).Should().Equal("aaa", "bbb", "ccc");
    }

    [TestMethod()]
    public void MatchSelect()
    {
        "0".MatchSelect(@"\d", m => "@", null).Should().Be("@");
        "0".MatchSelect(@"\d", m => "@", "x").Should().Be("@");
        "a".MatchSelect(@"\d", m => "@", null).Should().BeNull();
        "a".MatchSelect(@"\d", m => "@", "x").Should().Be("x");
        "a".MatchSelect(@"A", RegexOptions.None, m => "@", null).Should().BeNull();
        "a".MatchSelect(@"A", RegexOptions.None, m => "@", "x").Should().Be("x");
        "a".MatchSelect(@"A", RegexOptions.IgnoreCase, m => "@", null).Should().Be("@");
        "a".MatchSelect(@"A", RegexOptions.IgnoreCase, m => "@", "x").Should().Be("@");
    }


}
