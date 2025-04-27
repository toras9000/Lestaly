namespace LestalyTest.Extensions;

[TestClass()]
public class StringExtensionsTests
{

    // 以下の情報を参考にさせて頂いた。
    // ・Unicode 絵文字にまつわるあれこれ (絵文字の標準とプログラム上でのハンドリング)
    // 　https://qiita.com/_sobataro/items/47989ee4b573e0c2adfc

    [TestMethod()]
    public void IsEmpty()
    {
        "".IsEmpty().Should().BeTrue();
        default(string).IsEmpty().Should().BeTrue();
        " ".IsEmpty().Should().BeFalse();
        "a".IsEmpty().Should().BeFalse();
    }

    [TestMethod()]
    public void IsNotEmpty()
    {
        "".IsNotEmpty().Should().BeFalse();
        default(string).IsNotEmpty().Should().BeFalse();
        " ".IsNotEmpty().Should().BeTrue();
        "a".IsNotEmpty().Should().BeTrue();
    }

    [TestMethod()]
    public void IsWhite()
    {
        "".IsWhite().Should().BeTrue();
        default(string).IsWhite().Should().BeTrue();
        " ".IsWhite().Should().BeTrue();
        "a".IsWhite().Should().BeFalse();
    }

    [TestMethod()]
    public void IsNotWhite()
    {
        "".IsNotWhite().Should().BeFalse();
        default(string).IsNotWhite().Should().BeFalse();
        " ".IsNotWhite().Should().BeFalse();
        "a".IsNotWhite().Should().BeTrue();
    }

    [TestMethod()]
    public void WhenEmpty()
    {
        "".WhenEmpty("x").Should().Be("x");
        default(string).WhenEmpty("x").Should().Be("x");
        " ".WhenEmpty("x").Should().Be(" ");
        "a".WhenEmpty("x").Should().Be("a");
        "".WhenEmpty(default(string)!).Should().BeNull();
        " ".WhenEmpty(default(string)!).Should().Be(" ");
        "a".WhenEmpty(default(string)!).Should().Be("a");

        "".WhenEmpty(() => "x").Should().Be("x");
        default(string).WhenEmpty(() => "x").Should().Be("x");
        " ".WhenEmpty(() => "x").Should().Be(" ");
        "a".WhenEmpty(() => "x").Should().Be("a");
        "".WhenEmpty(() => null!).Should().BeNull();
        " ".WhenEmpty(() => null!).Should().Be(" ");
        "a".WhenEmpty(() => null!).Should().Be("a");
    }

    [TestMethod()]
    public void WhenWhite()
    {
        "".WhenWhite("x").Should().Be("x");
        default(string).WhenWhite("x").Should().Be("x");
        " ".WhenWhite("x").Should().Be("x");
        "a".WhenWhite("x").Should().Be("a");
        "".WhenWhite(default(string)!).Should().BeNull();
        " ".WhenWhite(default(string)!).Should().BeNull();
        "a".WhenWhite(default(string)!).Should().Be("a");

        "".WhenWhite(() => "x").Should().Be("x");
        default(string).WhenWhite(() => "x").Should().Be("x");
        " ".WhenWhite(() => "x").Should().Be("x");
        "a".WhenWhite(() => "x").Should().Be("a");
        "".WhenWhite(() => null!).Should().BeNull();
        " ".WhenWhite(() => null!).Should().BeNull();
        "a".WhenWhite(() => null!).Should().Be("a");
    }

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
            "abcd".StartsWithAny(["aa", "ab", "ac"], ignoreCase: false).Should().Be(true);
            "abcd".StartsWithAny(["aa", "ab", "ac"], ignoreCase: true).Should().BeTrue();

            "abcd".StartsWithAny(["AA", "AB", "AC"], ignoreCase: false).Should().BeFalse();
            "abcd".StartsWithAny(["AA", "AB", "AC"], ignoreCase: true).Should().BeTrue();

            "abcd".StartsWithAny(["AA", "AB", "AC"], StringComparison.Ordinal).Should().BeFalse();
            "abcd".StartsWithAny(["AA", "AB", "AC"], StringComparison.OrdinalIgnoreCase).Should().BeTrue();

            "abcd".StartsWithAny(["aa", "cd"], ignoreCase: true).Should().BeFalse();
            "abcd".StartsWithAny(["",], ignoreCase: true).Should().BeFalse();
            "abcd".StartsWithAny([default(string)!,], ignoreCase: true).Should().BeFalse();
        }
        {// Span
            "abcd".AsSpan().StartsWithAny(["aa", "ab", "ac"], ignoreCase: false).Should().Be(true);
            "abcd".AsSpan().StartsWithAny(["aa", "ab", "ac"], ignoreCase: true).Should().BeTrue();

            "abcd".AsSpan().StartsWithAny(["AA", "AB", "AC"], ignoreCase: false).Should().BeFalse();
            "abcd".AsSpan().StartsWithAny(["AA", "AB", "AC"], ignoreCase: true).Should().BeTrue();

            "abcd".AsSpan().StartsWithAny(["AA", "AB", "AC"], StringComparison.Ordinal).Should().BeFalse();
            "abcd".AsSpan().StartsWithAny(["AA", "AB", "AC"], StringComparison.OrdinalIgnoreCase).Should().BeTrue();

            "abcd".AsSpan().StartsWithAny(["aa", "cd"], ignoreCase: true).Should().BeFalse();
            "abcd".AsSpan().StartsWithAny(["",], ignoreCase: true).Should().BeFalse();
            "abcd".AsSpan().StartsWithAny([default(string)!,], ignoreCase: true).Should().BeFalse();
        }

    }

    [TestMethod()]
    public void StartsWithAnyIgnoreCase()
    {
        {// string
            "abcd".StartsWithAnyIgnoreCase(["aa", "ab", "ac"]).Should().BeTrue();
            "abcd".StartsWithAnyIgnoreCase(["AA", "AB", "AC"]).Should().BeTrue();

            "abcd".StartsWithAnyIgnoreCase(["aa", "cd"]).Should().BeFalse();
            "abcd".StartsWithAnyIgnoreCase([""]).Should().BeFalse();
            "abcd".StartsWithAnyIgnoreCase([]).Should().BeFalse();
        }
        {// Span
            "abcd".AsSpan().StartsWithAnyIgnoreCase(["aa", "ab", "ac"]).Should().BeTrue();
            "abcd".AsSpan().StartsWithAnyIgnoreCase(["AA", "AB", "AC"]).Should().BeTrue();

            "abcd".AsSpan().StartsWithAnyIgnoreCase(["aa", "cd"]).Should().BeFalse();
            "abcd".AsSpan().StartsWithAnyIgnoreCase([""]).Should().BeFalse();
            "abcd".AsSpan().StartsWithAnyIgnoreCase([]).Should().BeFalse();
        }

    }

    [TestMethod()]
    public void EndsWithAny()
    {
        {// string
            "abcd".EndsWithAny(["cc", "cd", "ce"], ignoreCase: false).Should().Be(true);
            "abcd".EndsWithAny(["cc", "cd", "ce"], ignoreCase: true).Should().BeTrue();

            "abcd".EndsWithAny(["CC", "CD", "CE"], ignoreCase: false).Should().BeFalse();
            "abcd".EndsWithAny(["CC", "CD", "CE"], ignoreCase: true).Should().BeTrue();

            "abcd".EndsWithAny(["CC", "CD", "CE"], StringComparison.Ordinal).Should().BeFalse();
            "abcd".EndsWithAny(["CC", "CD", "CE"], StringComparison.OrdinalIgnoreCase).Should().BeTrue();

            "abcd".EndsWithAny(["ab", "ce"], ignoreCase: true).Should().BeFalse();
            "abcd".EndsWithAny(["",], ignoreCase: true).Should().BeFalse();
            "abcd".EndsWithAny([default(string)!,], ignoreCase: true).Should().BeFalse();
        }
        {// Span
            "abcd".AsSpan().EndsWithAny(["cc", "cd", "ce"], ignoreCase: false).Should().Be(true);
            "abcd".AsSpan().EndsWithAny(["cc", "cd", "ce"], ignoreCase: true).Should().BeTrue();

            "abcd".AsSpan().EndsWithAny(["CC", "CD", "CE"], ignoreCase: false).Should().BeFalse();
            "abcd".AsSpan().EndsWithAny(["CC", "CD", "CE"], ignoreCase: true).Should().BeTrue();

            "abcd".AsSpan().EndsWithAny(["CC", "CD", "CE"], StringComparison.Ordinal).Should().BeFalse();
            "abcd".AsSpan().EndsWithAny(["CC", "CD", "CE"], StringComparison.OrdinalIgnoreCase).Should().BeTrue();

            "abcd".AsSpan().EndsWithAny(["ab", "ce"], ignoreCase: true).Should().BeFalse();
            "abcd".AsSpan().EndsWithAny(["",], ignoreCase: true).Should().BeFalse();
            "abcd".AsSpan().EndsWithAny([default(string)!,], ignoreCase: true).Should().BeFalse();
        }
    }

    [TestMethod()]
    public void EndsWithAnyIgnoreCase()
    {
        {// string
            "abcd".EndsWithAnyIgnoreCase(["cc", "cd", "ce"]).Should().BeTrue();
            "abcd".EndsWithAnyIgnoreCase(["CC", "CD", "CE"]).Should().BeTrue();

            "abcd".EndsWithAnyIgnoreCase(["ab", "ce"]).Should().BeFalse();
            "abcd".EndsWithAnyIgnoreCase([""]).Should().BeFalse();
            "abcd".EndsWithAnyIgnoreCase([]).Should().BeFalse();
        }
        {// Span
            "abcd".AsSpan().EndsWithAnyIgnoreCase(["cc", "cd", "ce"]).Should().BeTrue();
            "abcd".AsSpan().EndsWithAnyIgnoreCase(["CC", "CD", "CE"]).Should().BeTrue();

            "abcd".AsSpan().EndsWithAnyIgnoreCase(["ab", "ce"]).Should().BeFalse();
            "abcd".AsSpan().EndsWithAnyIgnoreCase([""]).Should().BeFalse();
            "abcd".AsSpan().EndsWithAnyIgnoreCase([]).Should().BeFalse();
        }
    }

    [TestMethod()]
    public void EqualsAny()
    {
        {// string
            "abcd".EqualsAny(["ABC", "ABCD", "BCD"], StringComparison.Ordinal).Should().BeFalse();
            "abcd".EqualsAny(["ABC", "ABCD", "BCD"], StringComparison.OrdinalIgnoreCase).Should().BeTrue();
        }
        {// Span
            "abcd".AsSpan().EqualsAny(["ABC", "ABCD", "BCD"], StringComparison.Ordinal).Should().BeFalse();
            "abcd".AsSpan().EqualsAny(["ABC", "ABCD", "BCD"], StringComparison.OrdinalIgnoreCase).Should().BeTrue();
        }
    }

    [TestMethod()]
    public void EqualsAnyIgnoreCase()
    {
        {// string
            "abcd".EqualsAnyIgnoreCase(["abc", "abcd", "bcd"]).Should().BeTrue();
            "abcd".EqualsAnyIgnoreCase(["ABC", "ABCD", "BCD"]).Should().BeTrue();
            "abcd".EqualsAnyIgnoreCase(["ABC", "BCD"]).Should().BeFalse();
        }
        {// Span
            "abcd".AsSpan().EqualsAnyIgnoreCase(["ABC", "ABCD", "BCD"]).Should().BeTrue();
            "abcd".AsSpan().EqualsAnyIgnoreCase(["ABC", "ABCD", "BCD"]).Should().BeTrue();
            "abcd".AsSpan().EqualsAnyIgnoreCase(["ABC", "BCD"]).Should().BeFalse();
        }
    }

    [TestMethod()]
    public void RoughAny()
    {
        {// string
            "abcd".AsSpan().RoughAny(["ABC", "ABCD", "BCD"]).Should().Be(true);
            "ABCD".AsSpan().RoughAny(["ABC", "ABCD", "BCD"]).Should().Be(true);
            "abcd".AsSpan().RoughAny([" ABC ", " ABCD ", " BCD "]).Should().Be(true);
            "abcd".AsSpan().RoughAny([" ABC ", " ABCD ", " BCD "]).Should().Be(true);
            "ABCD".AsSpan().RoughAny(["ABC", "AB CD", "BCD"]).Should().Be(false);
        }
        {// Span
            "abcd".RoughAny(["ABC", "ABCD", "BCD"]).Should().Be(true);
            "ABCD".RoughAny(["ABC", "ABCD", "BCD"]).Should().Be(true);
            "abcd".RoughAny([" ABC ", " ABCD ", " BCD "]).Should().Be(true);
            "abcd".RoughAny([" ABC ", " ABCD ", " BCD "]).Should().Be(true);
            "ABCD".RoughAny(["ABC", "AB CD", "BCD"]).Should().Be(false);
        }
    }

    [TestMethod()]
    public void TrimStartString()
    {
        {// string
            "abcd".TrimStartString("AB", ignoreCase: false).ToString().Should().Be("abcd");
            "abcd".TrimStartString("ab", ignoreCase: false).ToString().Should().Be("cd");
            "abcd".TrimStartString("AB", ignoreCase: true).ToString().Should().Be("cd");
            "abcd".TrimStartString("ab", ignoreCase: true).ToString().Should().Be("cd");
        }
        {// Span
            "abcd".AsSpan().TrimStartString("AB", ignoreCase: false).ToString().Should().Be("abcd");
            "abcd".AsSpan().TrimStartString("ab", ignoreCase: false).ToString().Should().Be("cd");
            "abcd".AsSpan().TrimStartString("AB", ignoreCase: true).ToString().Should().Be("cd");
            "abcd".AsSpan().TrimStartString("ab", ignoreCase: true).ToString().Should().Be("cd");
        }
    }

    [TestMethod()]
    public void TrimEndString()
    {
        {// string
            "abcd".TrimEndString("CD", ignoreCase: false).ToString().Should().Be("abcd");
            "abcd".TrimEndString("cd", ignoreCase: false).ToString().Should().Be("ab");
            "abcd".TrimEndString("CD", ignoreCase: true).ToString().Should().Be("ab");
            "abcd".TrimEndString("cd", ignoreCase: true).ToString().Should().Be("ab");
        }
        {// Span
            "abcd".AsSpan().TrimEndString("CD", ignoreCase: false).ToString().Should().Be("abcd");
            "abcd".AsSpan().TrimEndString("cd", ignoreCase: false).ToString().Should().Be("ab");
            "abcd".AsSpan().TrimEndString("CD", ignoreCase: true).ToString().Should().Be("ab");
            "abcd".AsSpan().TrimEndString("cd", ignoreCase: true).ToString().Should().Be("ab");
        }
    }

    [TestMethod()]
    public void EnsureStarts()
    {
        "abcd".EnsureStarts("a", ignoreCase: false).Should().Be("abcd");
        "abcd".EnsureStarts("A", ignoreCase: false).Should().Be("Aabcd");

        "abcd".EnsureStarts("a", ignoreCase: true).Should().Be("abcd");
        "abcd".EnsureStarts("A", ignoreCase: true).Should().Be("abcd");

        "abcd".EnsureStarts("a", StringComparison.Ordinal).Should().Be("abcd");
        "abcd".EnsureStarts("A", StringComparison.Ordinal).Should().Be("Aabcd");

        "abcd".EnsureStarts("a", StringComparison.OrdinalIgnoreCase).Should().Be("abcd");
        "abcd".EnsureStarts("A", StringComparison.OrdinalIgnoreCase).Should().Be("abcd");

        "a".EnsureStarts("a").Should().Be("a");
        "".EnsureStarts("a").Should().Be("a");
        default(string)!.EnsureStarts("a").Should().Be("a");
    }

    [TestMethod()]
    public void EnsureEnds()
    {
        "abcd".EnsureEnds("d", ignoreCase: false).Should().Be("abcd");
        "abcd".EnsureEnds("D", ignoreCase: false).Should().Be("abcdD");

        "abcd".EnsureEnds("d", ignoreCase: true).Should().Be("abcd");
        "abcd".EnsureEnds("D", ignoreCase: true).Should().Be("abcd");

        "abcd".EnsureEnds("d", StringComparison.Ordinal).Should().Be("abcd");
        "abcd".EnsureEnds("D", StringComparison.Ordinal).Should().Be("abcdD");

        "abcd".EnsureEnds("d", StringComparison.OrdinalIgnoreCase).Should().Be("abcd");
        "abcd".EnsureEnds("D", StringComparison.OrdinalIgnoreCase).Should().Be("abcd");

        "a".EnsureEnds("a").Should().Be("a");
        "".EnsureEnds("a").Should().Be("a");
        default(string)!.EnsureEnds("a").Should().Be("a");
    }

    [TestMethod]
    public void SplitAt_Char()
    {
        static void assert(string source, char delimiter, Func<(string token, string next)> expectation)
        {
            var token = source.SplitAt(delimiter, out var next);
            var expect = expectation();
            token.ToString().Should().Be(expect.token);
            next.ToString().Should().Be(expect.next);
        }

        assert("abc@def@ghi", '@', () => ("abc", "def@ghi"));
        assert("abc@@def@@ghi", '@', () => ("abc", "@def@@ghi"));
        assert("@abc@def@ghi", '@', () => ("", "abc@def@ghi"));
        assert("@", '@', () => ("", ""));
        assert("", '@', () => ("", ""));
        assert("abcdef", '@', () => ("abcdef", ""));
    }

    [TestMethod]
    public void SplitAt_String()
    {
        static void assert(string source, string delimiter, Func<(string token, string next)> expectation)
        {
            var token = source.SplitAt(delimiter, out var next);
            var expect = expectation();
            token.ToString().Should().Be(expect.token);
            next.ToString().Should().Be(expect.next);
        }

        assert("abc@def@@ghi", "@", () => ("abc", "def@@ghi"));
        assert("abc@def@@ghi", "@@", () => ("abc@def", "ghi"));

        assert("@@abc@@def@@ghi", "@@", () => ("", "abc@@def@@ghi"));
        assert("@@", "@@", () => ("", ""));
        assert("", "@@", () => ("", ""));
        assert("abcdef", "@@", () => ("abcdef", ""));
    }

    [TestMethod]
    public void SplitAtAny_Chars()
    {
        static void assert(string source, ReadOnlySpan<char> delimiters, Func<(string token, string next)> expectation)
        {
            var token = source.SplitAtAny(delimiters, out var next);
            var expect = expectation();
            token.ToString().Should().Be(expect.token);
            next.ToString().Should().Be(expect.next);
        }

        assert("abc@def:ghi", ['@', ':'], () => ("abc", "def:ghi"));
        assert("abc:def@ghi", ['@', ':'], () => ("abc", "def@ghi"));

        assert("abc@@def::ghi", ['@', ':'], () => ("abc", "@def::ghi"));
        assert("abc::def@@ghi", ['@', ':'], () => ("abc", ":def@@ghi"));

        assert("@abc@def:ghi", ['@', ':'], () => ("", "abc@def:ghi"));
        assert(":abc@def:ghi", ['@', ':'], () => ("", "abc@def:ghi"));

        assert(":@", ['@', ':'], () => ("", "@"));
        assert("@:", ['@', ':'], () => ("", ":"));

        assert(":", ['@', ':'], () => ("", ""));
        assert("@", ['@', ':'], () => ("", ""));

        assert("", ['@', ':'], () => ("", ""));
        assert("", ['@', ':'], () => ("", ""));

        assert("abcde", ['@', ':'], () => ("abcde", ""));
    }

    [TestMethod]
    public void AsTextLines()
    {
        "".AsTextLines().Should().Equal("");
        "a".AsTextLines().Should().Equal("a");
        "a\rb\nc".AsTextLines().Should().Equal("a", "b", "c");
        "a\r\nb\n\rc".AsTextLines().Should().Equal("a", "b", "", "c");
        "\ra\n".AsTextLines().Should().Equal("", "a", "");
        "\r".AsTextLines().Should().Equal("", "");
        "\n".AsTextLines().Should().Equal("", "");
        "\r\n".AsTextLines().Should().Equal("", "");
        "\n\r".AsTextLines().Should().Equal("", "", "");
    }

    [TestMethod()]
    public void DropEmpty()
    {
        new[] { "", "abc", null, null, "  ", "", "def", null, }
            .DropEmpty().Should().Equal(["abc", "  ", "def",]);
    }

    [TestMethod()]
    public void DropWhite()
    {
        new[] { "", "abc", null, null, "  ", "", "def", null, }
            .DropWhite().Should().Equal(["abc", "def",]);
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

    [TestMethod]
    public void Quote()
    {
        "abc".Quote().Should().Be("\"abc\"");
        "a\"bc".Quote().Should().Be("\"a\"\"bc\"");

        "abc".Quote(quote: '\'').Should().Be("'abc'");
        "ab'c".Quote(quote: '\'').Should().Be("'ab''c'");
        "ab\"c".Quote(quote: '\'').Should().Be("'ab\"c'");

        "abc".Quote(quote: '\'', escape: '/').Should().Be("'abc'");
        "ab'c".Quote(quote: '\'', escape: '/').Should().Be("'ab/'c'");


        "abc".AsSpan().Quote().Should().Be("\"abc\"");
        "a\"bc".AsSpan().Quote().Should().Be("\"a\"\"bc\"");

        "abc".AsSpan().Quote(quote: '\'').Should().Be("'abc'");
        "ab'c".AsSpan().Quote(quote: '\'').Should().Be("'ab''c'");
        "ab\"c".AsSpan().Quote(quote: '\'').Should().Be("'ab\"c'");

        "abc".AsSpan().Quote(quote: '\'', escape: '/').Should().Be("'abc'");
        "ab'c".AsSpan().Quote(quote: '\'', escape: '/').Should().Be("'ab/'c'");
    }

    [TestMethod]
    public void Unquote()
    {
        "\"abc\"".Unquote().Should().Be("abc");
        "\"a\"\"bc\"".Unquote().Should().Be("a\"bc");

        "'abc'".Unquote(quotes: ['\'']).Should().Be("abc");
        "'ab''c'".Unquote(quotes: ['\'']).Should().Be("ab'c");
        "'ab\"c'".Unquote(quotes: ['\'']).Should().Be("ab\"c");

        "'abc'".Unquote(quotes: ['\''], escape: '/').Should().Be("abc");
        "'ab/'c'".Unquote(quotes: ['\''], escape: '/').Should().Be("ab'c");
    }

    [TestMethod]
    public void AsTextElements()
    {
        "".AsTextElements().Should().BeEmpty();

        "abc".AsTextElements().Should().Equal("a", "b", "c");

        "あいう".AsTextElements().Should().Equal("あ", "い", "う");

        "アイウエ".AsTextElements().Should().Equal("ア", "イ", "ウ", "エ");

        "ｱｲｳｴｵ".AsTextElements().Should().Equal("ｱ", "ｲ", "ｳ", "ｴ", "ｵ");

        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".AsTextElements().Should().Equal("1️⃣", "2️⃣", "3️⃣", "4️⃣", "5️⃣", "6️⃣", "7️⃣", "8️⃣", "9️⃣");

        "👏🏻👏🏼👏🏽👏🏾👏🏿".AsTextElements().Should().Equal("👏🏻", "👏🏼", "👏🏽", "👏🏾", "👏🏿");

        "🇯🇵🇬🇧🇺🇸".AsTextElements().Should().Equal("🇯🇵", "🇬🇧", "🇺🇸");

        // 👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾
        // なおVS2022(v17.0.5)で見るとコメント内に記載した場合とコード中のリテラルで表示が変わる。各所で解釈の実装が異なるのだろうか。ただコメント内の記載位置でも表示が異なったりもする。ZWJ Sequenceのサポートが完全では無いのだろうか。
        // var t = "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾";
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".AsTextElements().Should().Equal("👩🏻‍👦🏼", "👨🏽‍👦🏾‍👦🏿", "👩🏼‍👨🏽‍👦🏼‍👧🏽", "👩🏻‍👩🏿‍👧🏼‍👧🏾");
    }

    [TestMethod]
    public void TextElementCount()
    {
        "".TextElementCount().Should().Be(0);

        "abcdef".TextElementCount().Should().Be(6);

        "あいう".TextElementCount().Should().Be(3);

        "アイウエ".TextElementCount().Should().Be(4);

        "ｱｲｳｴｵ".TextElementCount().Should().Be(5);

        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".TextElementCount().Should().Be(9);

        "👏🏻👏🏼👏🏽👏🏾👏🏿".TextElementCount().Should().Be(5);

        "🇯🇵🇬🇧🇺🇸".TextElementCount().Should().Be(3);

        // 👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".TextElementCount().Should().Be(4);
    }

    [TestMethod]
    public void CutLeftElements()
    {
        // Empty
        "".CutLeftElements(0).ToString().Should().BeEmpty();
        default(string).CutLeftElements(0).ToString().Should().BeEmpty();
        "abcdef".CutLeftElements(0).ToString().Should().BeEmpty();

        "abcdef".CutLeftElements(0).ToString().Should().BeEmpty();
        "abcdef".CutLeftElements(1).ToString().Should().Be("a");
        "abcdef".CutLeftElements(2).ToString().Should().Be("ab");
        "abcdef".CutLeftElements(6).ToString().Should().Be("abcdef");
        "abcdef".CutLeftElements(7).ToString().Should().Be("abcdef");
        "abcdef".CutLeftElements(999).ToString().Should().Be("abcdef");

        "あいうえお".CutLeftElements(0).ToString().Should().BeEmpty();
        "あいうえお".CutLeftElements(1).ToString().Should().Be("あ");
        "あいうえお".CutLeftElements(5).ToString().Should().Be("あいうえお");
        "あいうえお".CutLeftElements(6).ToString().Should().Be("あいうえお");
        "あいうえお".CutLeftElements(999).ToString().Should().Be("あいうえお");

        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".CutLeftElements(0).ToString().Should().BeEmpty();
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".CutLeftElements(1).ToString().Should().Be("1️⃣");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".CutLeftElements(2).ToString().Should().Be("1️⃣2️⃣");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".CutLeftElements(9).ToString().Should().Be("1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".CutLeftElements(10).ToString().Should().Be("1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣");

        "👏🏻👏🏼👏🏽👏🏾👏🏿".CutLeftElements(0).ToString().Should().BeEmpty();
        "👏🏻👏🏼👏🏽👏🏾👏🏿".CutLeftElements(1).ToString().Should().Be("👏🏻");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".CutLeftElements(2).ToString().Should().Be("👏🏻👏🏼");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".CutLeftElements(5).ToString().Should().Be("👏🏻👏🏼👏🏽👏🏾👏🏿");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".CutLeftElements(6).ToString().Should().Be("👏🏻👏🏼👏🏽👏🏾👏🏿");

        "🇯🇵🇬🇧🇺🇸".CutLeftElements(0).ToString().Should().BeEmpty();
        "🇯🇵🇬🇧🇺🇸".CutLeftElements(1).ToString().Should().Be("🇯🇵");
        "🇯🇵🇬🇧🇺🇸".CutLeftElements(2).ToString().Should().Be("🇯🇵🇬🇧");
        "🇯🇵🇬🇧🇺🇸".CutLeftElements(3).ToString().Should().Be("🇯🇵🇬🇧🇺🇸");
        "🇯🇵🇬🇧🇺🇸".CutLeftElements(4).ToString().Should().Be("🇯🇵🇬🇧🇺🇸");

        // 👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".CutLeftElements(1).ToString().Should().Be("👩🏻‍👦🏼");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".CutLeftElements(2).ToString().Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".CutLeftElements(3).ToString().Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".CutLeftElements(4).ToString().Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".CutLeftElements(5).ToString().Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".CutLeftElements(999).ToString().Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾");
    }

    [TestMethod]
    public void CutRightElements()
    {
        // Empty
        "".CutRightElements(0).ToString().Should().BeEmpty();
        default(string).CutRightElements(0).ToString().Should().BeEmpty();
        "abcdef".CutRightElements(0).ToString().Should().BeEmpty();

        "abcdef".CutRightElements(0).ToString().Should().Be("");
        "abcdef".CutRightElements(1).ToString().Should().Be("f");
        "abcdef".CutRightElements(2).ToString().Should().Be("ef");
        "abcdef".CutRightElements(6).ToString().Should().Be("abcdef");
        "abcdef".CutRightElements(7).ToString().Should().Be("abcdef");
        "abcdef".CutRightElements(999).ToString().Should().Be("abcdef");

        "あいうえお".CutRightElements(1).ToString().Should().Be("お");
        "あいうえお".CutRightElements(5).ToString().Should().Be("あいうえお");
        "あいうえお".CutRightElements(6).ToString().Should().Be("あいうえお");
        "あいうえお".CutRightElements(999).ToString().Should().Be("あいうえお");

        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".CutRightElements(0).ToString().Should().BeEmpty();
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".CutRightElements(1).ToString().Should().Be("9️⃣");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".CutRightElements(2).ToString().Should().Be("8️⃣9️⃣");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".CutRightElements(9).ToString().Should().Be("1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".CutRightElements(10).ToString().Should().Be("1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣");

        "👏🏻👏🏼👏🏽👏🏾👏🏿".CutRightElements(0).ToString().Should().BeEmpty();
        "👏🏻👏🏼👏🏽👏🏾👏🏿".CutRightElements(1).ToString().Should().Be("👏🏿");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".CutRightElements(2).ToString().Should().Be("👏🏾👏🏿");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".CutRightElements(5).ToString().Should().Be("👏🏻👏🏼👏🏽👏🏾👏🏿");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".CutRightElements(6).ToString().Should().Be("👏🏻👏🏼👏🏽👏🏾👏🏿");

        "🇯🇵🇬🇧🇺🇸".CutRightElements(0).ToString().Should().BeEmpty();
        "🇯🇵🇬🇧🇺🇸".CutRightElements(1).ToString().Should().Be("🇺🇸");
        "🇯🇵🇬🇧🇺🇸".CutRightElements(2).ToString().Should().Be("🇬🇧🇺🇸");
        "🇯🇵🇬🇧🇺🇸".CutRightElements(3).ToString().Should().Be("🇯🇵🇬🇧🇺🇸");
        "🇯🇵🇬🇧🇺🇸".CutRightElements(4).ToString().Should().Be("🇯🇵🇬🇧🇺🇸");

        // 👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".CutRightElements(1).ToString().Should().Be("👩🏻‍👩🏿‍👧🏼‍👧🏾");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".CutRightElements(2).ToString().Should().Be("👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".CutRightElements(3).ToString().Should().Be("👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".CutRightElements(4).ToString().Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".CutRightElements(5).ToString().Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".CutRightElements(999).ToString().Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾");
    }

    [TestMethod]
    public void EllipsisByLength_Marker()
    {
        "a".Length.Should().Be(1);
        "abcdefghi".EllipsisByLength(10, "...").Should().Be("abcdefghi");
        "abcdefghi".EllipsisByLength(9, "...").Should().Be("abcdefghi");
        "abcdefghi".EllipsisByLength(8, "...").Should().Be("abcde...");
        "abcdefghi".EllipsisByLength(4, "...").Should().Be("a...");
        "abcdefghi".EllipsisByLength(3, "...").Should().Be("...");

        "あ".Length.Should().Be(1);
        "あいうえおかきくけこ".EllipsisByLength(11, "**").Should().Be("あいうえおかきくけこ");
        "あいうえおかきくけこ".EllipsisByLength(10, "**").Should().Be("あいうえおかきくけこ");
        "あいうえおかきくけこ".EllipsisByLength(9, "**").Should().Be("あいうえおかき**");
        "あいうえおかきくけこ".EllipsisByLength(3, "**").Should().Be("あ**");
        "あいうえおかきくけこ".EllipsisByLength(2, "**").Should().Be("**");

        "1️⃣".Length.Should().Be(3);
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByLength(28, "@@").Should().Be("1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByLength(27, "@@").Should().Be("1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByLength(26, "@@").Should().Be("1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣@@");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByLength(7, "@@").Should().Be("1️⃣@@");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByLength(5, "@@").Should().Be("1️⃣@@");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByLength(2, "@@").Should().Be("@@");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByLength(11, "1️⃣2️⃣3️⃣").Should().Be("ab1️⃣2️⃣3️⃣");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByLength(10, "1️⃣2️⃣3️⃣").Should().Be("a1️⃣2️⃣3️⃣");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByLength(9, "1️⃣2️⃣3️⃣").Should().Be("1️⃣2️⃣3️⃣");

        "👏🏻".Length.Should().Be(4);
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByLength(21, "??").Should().Be("👏🏻👏🏼👏🏽👏🏾👏🏿");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByLength(20, "??").Should().Be("👏🏻👏🏼👏🏽👏🏾👏🏿");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByLength(19, "??").Should().Be("👏🏻👏🏼👏🏽👏🏾??");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByLength(9, "??").Should().Be("👏🏻??");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByLength(6, "??").Should().Be("👏🏻??");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByLength(2, "??").Should().Be("??");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByLength(14, "👏🏻👏🏼👏🏽").Should().Be("ab👏🏻👏🏼👏🏽");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByLength(13, "👏🏻👏🏼👏🏽").Should().Be("a👏🏻👏🏼👏🏽");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByLength(12, "👏🏻👏🏼👏🏽").Should().Be("👏🏻👏🏼👏🏽");

        "🇯🇵".Length.Should().Be(4);
        "🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷".EllipsisByLength(21, "!!").Should().Be("🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷");
        "🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷".EllipsisByLength(20, "!!").Should().Be("🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷");
        "🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷".EllipsisByLength(19, "!!").Should().Be("🇯🇵🇬🇧🇺🇸🇩🇪!!");
        "🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷".EllipsisByLength(9, "!!").Should().Be("🇯🇵!!");
        "🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷".EllipsisByLength(6, "!!").Should().Be("🇯🇵!!");
        "🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷".EllipsisByLength(2, "!!").Should().Be("!!");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByLength(10, "🇯🇵🇬🇧").Should().Be("ab🇯🇵🇬🇧");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByLength(9, "🇯🇵🇬🇧").Should().Be("a🇯🇵🇬🇧");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByLength(8, "🇯🇵🇬🇧").Should().Be("🇯🇵🇬🇧");

        // 👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾
        "👩🏻‍👦🏼".Length.Should().Be(9);
        "👨🏽‍👦🏾‍👦🏿".Length.Should().Be(14);
        "👩🏼‍👨🏽‍👦🏼‍👧🏽".Length.Should().Be(19);
        "👩🏻‍👩🏿‍👧🏼‍👧🏾".Length.Should().Be(19);
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByLength(9 + 14 + 19 + 19 + 1, "#").Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByLength(9 + 14 + 19 + 19, "#").Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByLength(9 + 14, "#").Should().Be("👩🏻‍👦🏼#");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByLength(9 + 1, "#").Should().Be("👩🏻‍👦🏼#");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByLength(9, "#").Should().Be("#");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByLength(1, "#").Should().Be("#");
        "abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz".EllipsisByLength(2 + 9 + 14 + 19, "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽").Should().Be("ab👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽");
        "abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz".EllipsisByLength(1 + 9 + 14 + 19, "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽").Should().Be("a👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽");
        "abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz".EllipsisByLength(0 + 9 + 14 + 19, "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽").Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽");
    }

    [TestMethod]
    public void EllipsisByLength_NoMarker()
    {
        "a".Length.Should().Be(1);
        "abcdefghi".EllipsisByLength(10).Should().Be("abcdefghi");
        "abcdefghi".EllipsisByLength(9).Should().Be("abcdefghi");
        "abcdefghi".EllipsisByLength(1).Should().Be("a");
        "abcdefghi".EllipsisByLength(0).Should().BeEmpty();

        "あ".Length.Should().Be(1);
        "あいうえおかきくけこ".EllipsisByLength(11).Should().Be("あいうえおかきくけこ");
        "あいうえおかきくけこ".EllipsisByLength(10).Should().Be("あいうえおかきくけこ");
        "あいうえおかきくけこ".EllipsisByLength(9).Should().Be("あいうえおかきくけ");
        "あいうえおかきくけこ".EllipsisByLength(1).Should().Be("あ");
        "あいうえおかきくけこ".EllipsisByLength(0).Should().BeEmpty();

        "1️⃣".Length.Should().Be(3);
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByLength(28).Should().Be("1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByLength(27).Should().Be("1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByLength(26).Should().Be("1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByLength(3).Should().Be("1️⃣");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByLength(2).Should().BeEmpty();
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByLength(0).Should().BeEmpty();

        "👏🏻".Length.Should().Be(4);
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByLength(21).Should().Be("👏🏻👏🏼👏🏽👏🏾👏🏿");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByLength(20).Should().Be("👏🏻👏🏼👏🏽👏🏾👏🏿");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByLength(19).Should().Be("👏🏻👏🏼👏🏽👏🏾");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByLength(4).Should().Be("👏🏻");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByLength(3).Should().BeEmpty();
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByLength(0).Should().BeEmpty();

        "🇯🇵".Length.Should().Be(4);
        "🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷".EllipsisByLength(21).Should().Be("🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷");
        "🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷".EllipsisByLength(20).Should().Be("🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷");
        "🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷".EllipsisByLength(19).Should().Be("🇯🇵🇬🇧🇺🇸🇩🇪");
        "🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷".EllipsisByLength(4).Should().Be("🇯🇵");
        "🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷".EllipsisByLength(3).Should().BeEmpty();
        "🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷".EllipsisByLength(0).Should().BeEmpty();

        // 👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾
        "👩🏻‍👦🏼".Length.Should().Be(9);
        "👨🏽‍👦🏾‍👦🏿".Length.Should().Be(14);
        "👩🏼‍👨🏽‍👦🏼‍👧🏽".Length.Should().Be(19);
        "👩🏻‍👩🏿‍👧🏼‍👧🏾".Length.Should().Be(19);
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByLength(9 + 14 + 19 + 19 + 1).Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByLength(9 + 14 + 19 + 19).Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByLength(9 + 14 + 19 + 19 - 1).Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByLength(9).Should().Be("👩🏻‍👦🏼");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByLength(9 - 1).Should().BeEmpty();
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByLength(0).Should().BeEmpty();
    }

    [TestMethod]
    public void EllipsisByLength_Error()
    {
        new Action(() => default(string)!.EllipsisByLength(3)).Should().Throw<Exception>();

        new Action(() => "abcdef".EllipsisByLength(-1)).Should().Throw<Exception>();

        new Action(() => "abcdef".EllipsisByLength(2, "xyz")).Should().Throw<Exception>();
        new Action(() => "".EllipsisByLength(2, "xyz")).Should().Throw<Exception>();
    }

    [TestMethod]
    public void EllipsisByElements_Marker()
    {
        "a".TextElementCount().Should().Be(1);
        "abcdefghi".EllipsisByElements(10, "...").Should().Be("abcdefghi");
        "abcdefghi".EllipsisByElements(9, "...").Should().Be("abcdefghi");
        "abcdefghi".EllipsisByElements(8, "...").Should().Be("abcde...");
        "abcdefghi".EllipsisByElements(4, "...").Should().Be("a...");
        "abcdefghi".EllipsisByElements(3, "...").Should().Be("...");

        "あ".TextElementCount().Should().Be(1);
        "あいうえおかきくけこ".EllipsisByElements(11, "**").Should().Be("あいうえおかきくけこ");
        "あいうえおかきくけこ".EllipsisByElements(10, "**").Should().Be("あいうえおかきくけこ");
        "あいうえおかきくけこ".EllipsisByElements(9, "**").Should().Be("あいうえおかき**");
        "あいうえおかきくけこ".EllipsisByElements(3, "**").Should().Be("あ**");
        "あいうえおかきくけこ".EllipsisByElements(2, "**").Should().Be("**");

        "1️⃣".TextElementCount().Should().Be(1);
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByElements(10, "@@").Should().Be("1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByElements(9, "@@").Should().Be("1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByElements(8, "@@").Should().Be("1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣@@");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByElements(3, "@@").Should().Be("1️⃣@@");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByElements(2, "@@").Should().Be("@@");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByElements(5, "1️⃣2️⃣3️⃣").Should().Be("ab1️⃣2️⃣3️⃣");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByElements(4, "1️⃣2️⃣3️⃣").Should().Be("a1️⃣2️⃣3️⃣");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByElements(3, "1️⃣2️⃣3️⃣").Should().Be("1️⃣2️⃣3️⃣");

        "👏🏻".TextElementCount().Should().Be(1);
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByElements(6, "??").Should().Be("👏🏻👏🏼👏🏽👏🏾👏🏿");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByElements(5, "??").Should().Be("👏🏻👏🏼👏🏽👏🏾👏🏿");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByElements(4, "??").Should().Be("👏🏻👏🏼??");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByElements(3, "??").Should().Be("👏🏻??");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByElements(2, "??").Should().Be("??");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByElements(5, "👏🏻👏🏼👏🏽").Should().Be("ab👏🏻👏🏼👏🏽");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByElements(4, "👏🏻👏🏼👏🏽").Should().Be("a👏🏻👏🏼👏🏽");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByElements(3, "👏🏻👏🏼👏🏽").Should().Be("👏🏻👏🏼👏🏽");

        "🇯🇵".TextElementCount().Should().Be(1);
        "🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷".EllipsisByElements(6, "!!").Should().Be("🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷");
        "🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷".EllipsisByElements(5, "!!").Should().Be("🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷");
        "🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷".EllipsisByElements(4, "!!").Should().Be("🇯🇵🇬🇧!!");
        "🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷".EllipsisByElements(3, "!!").Should().Be("🇯🇵!!");
        "🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷".EllipsisByElements(2, "!!").Should().Be("!!");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByElements(4, "🇯🇵🇬🇧").Should().Be("ab🇯🇵🇬🇧");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByElements(3, "🇯🇵🇬🇧").Should().Be("a🇯🇵🇬🇧");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByElements(2, "🇯🇵🇬🇧").Should().Be("🇯🇵🇬🇧");

        // 👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾
        "👩🏻‍👦🏼".TextElementCount().Should().Be(1);
        "👨🏽‍👦🏾‍👦🏿".TextElementCount().Should().Be(1);
        "👩🏼‍👨🏽‍👦🏼‍👧🏽".TextElementCount().Should().Be(1);
        "👩🏻‍👩🏿‍👧🏼‍👧🏾".TextElementCount().Should().Be(1);
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByElements(5, "#").Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByElements(4, "#").Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByElements(3, "#").Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿#");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByElements(2, "#").Should().Be("👩🏻‍👦🏼#");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByElements(1, "#").Should().Be("#");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByElements(5, "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽").Should().Be("ab👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByElements(4, "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽").Should().Be("a👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽");
        "abcdefghijklmnopqrstuvwxyz".EllipsisByElements(3, "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽").Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽");
    }

    [TestMethod]
    public void EllipsisByElements_NoMarker()
    {
        "a".TextElementCount().Should().Be(1);
        "abcdefghi".EllipsisByElements(10).Should().Be("abcdefghi");
        "abcdefghi".EllipsisByElements(9).Should().Be("abcdefghi");
        "abcdefghi".EllipsisByElements(8).Should().Be("abcdefgh");
        "abcdefghi".EllipsisByElements(3).Should().Be("abc");
        "abcdefghi".EllipsisByElements(1).Should().Be("a");
        "abcdefghi".EllipsisByElements(0).Should().BeEmpty();

        "あ".TextElementCount().Should().Be(1);
        "あいうえおかきくけこ".EllipsisByElements(11).Should().Be("あいうえおかきくけこ");
        "あいうえおかきくけこ".EllipsisByElements(10).Should().Be("あいうえおかきくけこ");
        "あいうえおかきくけこ".EllipsisByElements(9).Should().Be("あいうえおかきくけ");
        "あいうえおかきくけこ".EllipsisByElements(2).Should().Be("あい");
        "あいうえおかきくけこ".EllipsisByElements(1).Should().Be("あ");
        "あいうえおかきくけこ".EllipsisByElements(0).Should().BeEmpty();

        "1️⃣".TextElementCount().Should().Be(1);
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByElements(10).Should().Be("1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByElements(9).Should().Be("1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByElements(8).Should().Be("1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByElements(2).Should().Be("1️⃣2️⃣");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByElements(1).Should().Be("1️⃣");
        "1️⃣2️⃣3️⃣4️⃣5️⃣6️⃣7️⃣8️⃣9️⃣".EllipsisByElements(0).Should().BeEmpty(); ;

        "👏🏻".TextElementCount().Should().Be(1);
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByElements(6).Should().Be("👏🏻👏🏼👏🏽👏🏾👏🏿");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByElements(5).Should().Be("👏🏻👏🏼👏🏽👏🏾👏🏿");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByElements(2).Should().Be("👏🏻👏🏼");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByElements(1).Should().Be("👏🏻");
        "👏🏻👏🏼👏🏽👏🏾👏🏿".EllipsisByElements(0).Should().BeEmpty();

        "🇯🇵".TextElementCount().Should().Be(1);
        "🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷".EllipsisByElements(6).Should().Be("🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷");
        "🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷".EllipsisByElements(5).Should().Be("🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷");
        "🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷".EllipsisByElements(2).Should().Be("🇯🇵🇬🇧");
        "🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷".EllipsisByElements(1).Should().Be("🇯🇵");
        "🇯🇵🇬🇧🇺🇸🇩🇪🇫🇷".EllipsisByElements(0).Should().BeEmpty();

        // 👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾
        "👩🏻‍👦🏼".TextElementCount().Should().Be(1);
        "👨🏽‍👦🏾‍👦🏿".TextElementCount().Should().Be(1);
        "👩🏼‍👨🏽‍👦🏼‍👧🏽".TextElementCount().Should().Be(1);
        "👩🏻‍👩🏿‍👧🏼‍👧🏾".TextElementCount().Should().Be(1);
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByElements(5).Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByElements(4).Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByElements(2).Should().Be("👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByElements(1).Should().Be("👩🏻‍👦🏼");
        "👩🏻‍👦🏼👨🏽‍👦🏾‍👦🏿👩🏼‍👨🏽‍👦🏼‍👧🏽👩🏻‍👩🏿‍👧🏼‍👧🏾".EllipsisByElements(0).Should().BeEmpty();
    }

    [TestMethod]
    public void EllipsisByElements_Error()
    {
        new Action(() => default(string)!.EllipsisByElements(3)).Should().Throw<Exception>();

        new Action(() => "abcdef".EllipsisByElements(-1)).Should().Throw<Exception>();

        new Action(() => "abcdef".EllipsisByElements(2, "xyz")).Should().Throw<Exception>();
        new Action(() => "".EllipsisByElements(2, "xyz")).Should().Throw<Exception>();
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
