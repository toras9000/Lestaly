using System.Text.RegularExpressions;

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

    [TestMethod()]
    public void StartsWithPattern()
    {
        {// string
            "abcd".StartsWithPattern("ax").Should().Be(false);
            "abcd".StartsWithPattern("ab").Should().Be(true);
            "abcd".StartsWithPattern("AB").Should().Be(false);
            "abcd".StartsWithPattern("AB", RegexOptions.IgnoreCase).Should().Be(true);
        }
        {// Span
            "abcd".AsSpan().StartsWithPattern("ax").Should().Be(false);
            "abcd".AsSpan().StartsWithPattern("ab").Should().Be(true);
            "abcd".AsSpan().StartsWithPattern("AB").Should().Be(false);
            "abcd".AsSpan().StartsWithPattern("AB", RegexOptions.IgnoreCase).Should().Be(true);
        }
    }

    [TestMethod()]
    public void EndsWithPattern()
    {
        {// string
            "abcd".EndsWithPattern("cc").Should().Be(false);
            "abcd".EndsWithPattern("cd").Should().Be(true);
            "abcd".EndsWithPattern("CD").Should().Be(false);
            "abcd".EndsWithPattern("cd", RegexOptions.IgnoreCase).Should().Be(true);
        }
        {// Span
            "abcd".AsSpan().EndsWithPattern("cc").Should().Be(false);
            "abcd".AsSpan().EndsWithPattern("cd").Should().Be(true);
            "abcd".AsSpan().EndsWithPattern("CD").Should().Be(false);
            "abcd".AsSpan().EndsWithPattern("cd", RegexOptions.IgnoreCase).Should().Be(true);
        }
    }

    [TestMethod()]
    public void TrimStartPattern()
    {
        {// string
            "abcd".TrimStartPattern("ax").ToString().Should().Be("abcd");
            "abcd".TrimStartPattern("ab").ToString().Should().Be("cd");
            "abcd".TrimStartPattern("AB").ToString().Should().Be("abcd");
            "abcd".TrimStartPattern("AB", RegexOptions.IgnoreCase).ToString().Should().Be("cd");
        }
        {// Span
            "abcd".AsSpan().TrimStartPattern("ax").ToString().Should().Be("abcd");
            "abcd".AsSpan().TrimStartPattern("ab").ToString().Should().Be("cd");
            "abcd".AsSpan().TrimStartPattern("AB").ToString().Should().Be("abcd");
            "abcd".AsSpan().TrimStartPattern("AB", RegexOptions.IgnoreCase).ToString().Should().Be("cd");
        }
    }

    [TestMethod()]
    public void TrimEndPattern()
    {
        {// string
            "abcd".TrimEndPattern("cc").ToString().Should().Be("abcd");
            "abcd".TrimEndPattern("cd").ToString().Should().Be("ab");
            "abcd".TrimEndPattern("CD").ToString().Should().Be("abcd");
            "abcd".TrimEndPattern("cd", RegexOptions.IgnoreCase).ToString().Should().Be("ab");
        }
        {// Span
            "abcd".AsSpan().TrimEndPattern("cc").ToString().Should().Be("abcd");
            "abcd".AsSpan().TrimEndPattern("cd").ToString().Should().Be("ab");
            "abcd".AsSpan().TrimEndPattern("CD").ToString().Should().Be("abcd");
            "abcd".AsSpan().TrimEndPattern("cd", RegexOptions.IgnoreCase).ToString().Should().Be("ab");
        }
    }


}
