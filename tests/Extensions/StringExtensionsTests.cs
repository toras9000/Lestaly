using FluentAssertions;

namespace LestalyTest.Extensions;

[TestClass()]
public class StringExtensionsTests
{
    [TestMethod()]
    public void FirstLine()
    {
        "a".FirstLine().Should().Be("a");
        "a\nb\nc".FirstLine().Should().Be("a");
        "a\rb\rc".FirstLine().Should().Be("a");
        "".FirstLine().Should().Be("");
        default(string).FirstLine().Should().Be("");
    }

    [TestMethod()]
    public void LastLine()
    {
        "a".LastLine().Should().Be("a");
        "a\nb\nc".LastLine().Should().Be("c");
        "a\rb\rc".LastLine().Should().Be("c");
        "".LastLine().Should().Be("");
        default(string).LastLine().Should().Be("");
    }

    [TestMethod()]
    public void BeforeAt_Char()
    {
        "a.b.c".BeforeAt('.').Should().Be("a");
        "".BeforeAt('.').Should().Be("");
        default(string).BeforeAt('.').Should().Be("");
    }

    [TestMethod()]
    public void BeforeAt_Str()
    {
        "a//b//c".BeforeAt("//").Should().Be("a");
        "".BeforeAt("//").Should().Be("");
        default(string).BeforeAt("//").Should().Be("");
    }

    [TestMethod()]
    public void AfterAt_Char()
    {
        "a.b.c".AfterAt('.').Should().Be("b.c");
        "".AfterAt('.').Should().Be("");
        default(string).AfterAt('.').Should().Be("");
    }

    [TestMethod()]
    public void AfterAt_Str()
    {
        "a//b//c".AfterAt("//").Should().Be("b//c");
        "".AfterAt("//").Should().Be("");
        default(string).AfterAt("//").Should().Be("");
    }

    [TestMethod()]
    public void JoinString()
    {
        new[] { null, "a", " ", "b", null }.JoinString("/").Should().Be("/a/ /b/");
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
    }

    [TestMethod()]
    public void CutLeftElements()
    {
        "abc".CutLeftElements(1).Should().Be("a");
        "abc".CutLeftElements(5).Should().Be("abc");

        "".CutLeftElements(1).Should().Be("");
        default(string).CutLeftElements(1).Should().Be("");
    }

    [TestMethod()]
    public void CutRightElements()
    {
        "abc".CutRightElements(1).Should().Be("c");
        "abc".CutRightElements(5).Should().Be("abc");

        "".CutRightElements(1).Should().Be("");
        default(string).CutRightElements(1).Should().Be("");
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
