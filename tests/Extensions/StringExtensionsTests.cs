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
