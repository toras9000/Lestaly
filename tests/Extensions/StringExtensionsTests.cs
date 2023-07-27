using FluentAssertions;

namespace LestalyTest.Extensions;

[TestClass()]
public class StringExtensionsTests
{
    [TestMethod()]
    public void OmitEmpty()
    {
        default(string).OmitEmpty().Should().BeNull();
        "".OmitEmpty().Should().BeNull();
        " ".OmitEmpty().Should().Be(" ");
        " a ".OmitEmpty().Should().Be(" a ");
    }

    [TestMethod()]
    public void OmitWhite()
    {
        default(string).OmitWhite().Should().BeNull();
        "".OmitWhite().Should().BeNull();
        " ".OmitWhite().Should().BeNull();
        " a ".OmitWhite().Should().Be("a");
    }

    [TestMethod()]
    public void NotEmptyTo()
    {
        default(string).NotEmptyTo(t => $"<<{t}>>").Should().BeNull();
        "".NotEmptyTo(t => $"<<{t}>>").Should().BeNull();
        " ".NotEmptyTo(t => $"<<{t}>>").Should().Be("<< >>");
        " a ".NotEmptyTo(t => $"<<{t}>>").Should().Be("<< a >>");

        default(string).NotEmptyTo(t => 100).Should().Be(0);
        "".NotEmptyTo(t => 100).Should().Be(0);
        " ".NotEmptyTo(t => 100).Should().Be(100);
        " a ".NotEmptyTo(t => 100).Should().Be(100);
    }

    [TestMethod()]
    public void NotWhiteTo()
    {
        default(string).NotWhiteTo(t => $"<<{t}>>").Should().BeNull();
        "".NotWhiteTo(t => $"<<{t}>>").Should().BeNull();
        " ".NotWhiteTo(t => $"<<{t}>>").Should().BeNull();
        " a ".NotWhiteTo(t => $"<<{t}>>").Should().Be("<< a >>");

        default(string).NotWhiteTo(t => 100).Should().Be(0);
        "".NotWhiteTo(t => 100).Should().Be(0);
        " ".NotWhiteTo(t => 100).Should().Be(0);
        " a ".NotWhiteTo(t => 100).Should().Be(100);
    }

    [TestMethod()]
    public void StartsWithAny()
    {
        {// string
            "abcd".StartsWithAny(new[] { "aa", "ab", "ac" }, ignoreCase: false).Should().Be(true);
            "abcd".StartsWithAny(new[] { "aa", "ab", "ac" }, ignoreCase: true).Should().BeTrue();

            "abcd".StartsWithAny(new[] { "AA", "AB", "AC" }, ignoreCase: false).Should().BeFalse();
            "abcd".StartsWithAny(new[] { "AA", "AB", "AC" }, ignoreCase: true).Should().BeTrue();

            "abcd".StartsWithAny(new[] { "AA", "AB", "AC" }, StringComparison.Ordinal).Should().BeFalse();
            "abcd".StartsWithAny(new[] { "AA", "AB", "AC" }, StringComparison.OrdinalIgnoreCase).Should().BeTrue();

            "abcd".StartsWithAny(new[] { "aa", "cd" }, ignoreCase: true).Should().BeFalse();
            "abcd".StartsWithAny(new[] { "", }, ignoreCase: true).Should().BeFalse();
            "abcd".StartsWithAny(new[] { default(string)!, }, ignoreCase: true).Should().BeFalse();
        }
        {// Span
            "abcd".AsSpan().StartsWithAny(new[] { "aa", "ab", "ac" }, ignoreCase: false).Should().Be(true);
            "abcd".AsSpan().StartsWithAny(new[] { "aa", "ab", "ac" }, ignoreCase: true).Should().BeTrue();

            "abcd".AsSpan().StartsWithAny(new[] { "AA", "AB", "AC" }, ignoreCase: false).Should().BeFalse();
            "abcd".AsSpan().StartsWithAny(new[] { "AA", "AB", "AC" }, ignoreCase: true).Should().BeTrue();

            "abcd".AsSpan().StartsWithAny(new[] { "AA", "AB", "AC" }, StringComparison.Ordinal).Should().BeFalse();
            "abcd".AsSpan().StartsWithAny(new[] { "AA", "AB", "AC" }, StringComparison.OrdinalIgnoreCase).Should().BeTrue();

            "abcd".AsSpan().StartsWithAny(new[] { "aa", "cd" }, ignoreCase: true).Should().BeFalse();
            "abcd".AsSpan().StartsWithAny(new[] { "", }, ignoreCase: true).Should().BeFalse();
            "abcd".AsSpan().StartsWithAny(new[] { default(string)!, }, ignoreCase: true).Should().BeFalse();
        }

    }

    [TestMethod()]
    public void EndsWithAny_StringAndFlag()
    {
        {// string
            "abcd".EndsWithAny(new[] { "cc", "cd", "ce" }, ignoreCase: false).Should().Be(true);
            "abcd".EndsWithAny(new[] { "cc", "cd", "ce" }, ignoreCase: true).Should().BeTrue();

            "abcd".EndsWithAny(new[] { "CC", "CD", "CE" }, ignoreCase: false).Should().BeFalse();
            "abcd".EndsWithAny(new[] { "CC", "CD", "CE" }, ignoreCase: true).Should().BeTrue();

            "abcd".EndsWithAny(new[] { "CC", "CD", "CE" }, StringComparison.Ordinal).Should().BeFalse();
            "abcd".EndsWithAny(new[] { "CC", "CD", "CE" }, StringComparison.OrdinalIgnoreCase).Should().BeTrue();

            "abcd".EndsWithAny(new[] { "ab", "ce" }, ignoreCase: true).Should().BeFalse();
            "abcd".EndsWithAny(new[] { "", }, ignoreCase: true).Should().BeFalse();
            "abcd".EndsWithAny(new[] { default(string)!, }, ignoreCase: true).Should().BeFalse();
        }
        {// Span
            "abcd".AsSpan().EndsWithAny(new[] { "cc", "cd", "ce" }, ignoreCase: false).Should().Be(true);
            "abcd".AsSpan().EndsWithAny(new[] { "cc", "cd", "ce" }, ignoreCase: true).Should().BeTrue();

            "abcd".AsSpan().EndsWithAny(new[] { "CC", "CD", "CE" }, ignoreCase: false).Should().BeFalse();
            "abcd".AsSpan().EndsWithAny(new[] { "CC", "CD", "CE" }, ignoreCase: true).Should().BeTrue();

            "abcd".AsSpan().EndsWithAny(new[] { "CC", "CD", "CE" }, StringComparison.Ordinal).Should().BeFalse();
            "abcd".AsSpan().EndsWithAny(new[] { "CC", "CD", "CE" }, StringComparison.OrdinalIgnoreCase).Should().BeTrue();

            "abcd".AsSpan().EndsWithAny(new[] { "ab", "ce" }, ignoreCase: true).Should().BeFalse();
            "abcd".AsSpan().EndsWithAny(new[] { "", }, ignoreCase: true).Should().BeFalse();
            "abcd".AsSpan().EndsWithAny(new[] { default(string)!, }, ignoreCase: true).Should().BeFalse();
        }
    }

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
    public void JoinString()
    {
        new[] { null, "a", " ", "b", null }.JoinString("/").Should().Be("/a/ /b/");
    }

    [TestMethod()]
    public void Decorate()
    {
        "abc".Decorate("<{0}>").Should().Be("<abc>");
        "".Decorate("<{0}>").Should().Be("");
        default(string).Decorate("<{0}>").Should().Be(null);

        "abc".Decorate(t => $"<{t}>").Should().Be("<abc>");
        "".Decorate(t => $"<{t}>").Should().Be("");
        default(string).Decorate(t => $"<{t}>").Should().Be(null);
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
        "".TieIn("").Should().Be("");
        default(string).TieIn(null).Should().Be("");

        "abc".TieIn("xxx", (x, y) => $"({x}/{y})").Should().Be("(abc/xxx)");
        "abc".TieIn("", (x, y) => $"({x}/{y})").Should().Be("");
        "".TieIn("xxx", (x, y) => $"({x}/{y})").Should().Be("");
        default(string).TieIn(null, (x, y) => $"({x}/{y})").Should().Be("");

        "abc".TieIn("xxx", '/').Should().Be("abc/xxx");
        "abc".TieIn("", '/').Should().Be("");
        "".TieIn("xxx", '/').Should().Be("");
        default(string).TieIn(null, '/').Should().Be("");

        "abc".TieIn("xxx", "/").Should().Be("abc/xxx");
        "abc".TieIn("", "/").Should().Be("");
        "".TieIn("xxx", "/").Should().Be("");
        default(string).TieIn(null, "/").Should().Be("");
    }

    [TestMethod()]
    public void Mux()
    {
        "abc".Mux("xxx").Should().Be("abcxxx");
        "abc".Mux("").Should().Be("abc");
        "abc".Mux(null).Should().Be("abc");
        "".Mux("xxx").Should().Be("xxx");
        default(string).Mux("xxx").Should().Be("xxx");
        "".Mux("").Should().Be("");
        default(string).Mux(null).Should().Be("");

        "abc".Mux("xxx", (x, y) => $"({x}/{y})").Should().Be("(abc/xxx)");
        "abc".Mux("", (x, y) => $"({x}/{y})").Should().Be("abc");
        "".Mux("xxx", (x, y) => $"({x}/{y})").Should().Be("xxx");
        default(string).Mux(null, (x, y) => $"({x}/{y})").Should().Be("");

        "abc".Mux("xxx", '/').Should().Be("abc/xxx");
        "abc".Mux("", '/').Should().Be("abc");
        "".Mux("xxx", '/').Should().Be("xxx");
        default(string).Mux(null, '/').Should().Be("");

        "abc".Mux("xxx", "/").Should().Be("abc/xxx");
        "abc".Mux("", "/").Should().Be("abc");
        "".Mux("xxx", "/").Should().Be("xxx");
        default(string).Mux(null, "/").Should().Be("");
    }

    [TestMethod()]
    public void ThrowIfEmpty()
    {
        "abc".ThrowIfEmpty().Should().Be("abc");
        " ".ThrowIfEmpty().Should().Be(" ");

        new Action(() => "".ThrowIfEmpty())
            .Should().Throw<Exception>();
        new Action(() => default(string).ThrowIfEmpty())
            .Should().Throw<Exception>();

        "abc".ThrowIfEmpty(() => new InvalidOperationException()).Should().Be("abc");
        " ".ThrowIfEmpty(() => new InvalidOperationException()).Should().Be(" ");

        new Action(() => "".ThrowIfEmpty(() => new InvalidOperationException()))
            .Should().Throw<InvalidOperationException>();
        new Action(() => default(string).ThrowIfEmpty(() => new InvalidOperationException()))
            .Should().Throw<InvalidOperationException>();
    }

    [TestMethod()]
    public void ThrowIfWhite()
    {
        "abc".ThrowIfWhite().Should().Be("abc");

        new Action(() => "".ThrowIfWhite())
            .Should().Throw<Exception>();
        new Action(() => " ".ThrowIfWhite())
            .Should().Throw<Exception>();
        new Action(() => default(string).ThrowIfWhite())
            .Should().Throw<Exception>();

        "abc".ThrowIfWhite(() => new InvalidOperationException()).Should().Be("abc");

        new Action(() => "".ThrowIfWhite(() => new InvalidOperationException()))
            .Should().Throw<InvalidOperationException>();
        new Action(() => " ".ThrowIfWhite(() => new InvalidOperationException()))
            .Should().Throw<InvalidOperationException>();
        new Action(() => default(string).ThrowIfWhite(() => new InvalidOperationException()))
            .Should().Throw<InvalidOperationException>();
    }

    [TestMethod()]
    public void CancelIfEmpty()
    {
        "abc".CancelIfEmpty().Should().Be("abc");
        " ".CancelIfEmpty().Should().Be(" ");

        new Action(() => "".CancelIfEmpty())
            .Should().Throw<OperationCanceledException>();
        new Action(() => default(string).CancelIfEmpty())
            .Should().Throw<OperationCanceledException>();
    }

    [TestMethod()]
    public void CancelIfWhite()
    {
        "abc".CancelIfWhite().Should().Be("abc");

        new Action(() => "".CancelIfWhite())
            .Should().Throw<OperationCanceledException>();
        new Action(() => " ".CancelIfWhite())
            .Should().Throw<OperationCanceledException>();
        new Action(() => default(string).CancelIfWhite())
            .Should().Throw<OperationCanceledException>();
    }

}
