namespace LestalyTest.Extensions;

[TestClass()]
public class StringTokenExtensionsTests
{
    [TestMethod]
    public void TakeLine()
    {
        "abc\ndef".TakeLine(trim: true).ToString().Should().Be("abc");
        "abc\ndef".TakeLine(trim: false).ToString().Should().Be("abc");
        "aaa\rbbb".TakeLine(trim: true).ToString().Should().Be("aaa");
        "aaa\rbbb".TakeLine(trim: false).ToString().Should().Be("aaa");
        "xyz\r\nabc".TakeLine(trim: true).ToString().Should().Be("xyz");
        "xyz\r\nabc".TakeLine(trim: false).ToString().Should().Be("xyz");
        "".TakeLine(trim: true).ToString().Should().BeEmpty();
        "".TakeLine(trim: false).ToString().Should().BeEmpty();
        default(string).TakeLine(trim: true).ToString().Should().BeEmpty();
        default(string).TakeLine(trim: false).ToString().Should().BeEmpty();

        "\n".TakeLine(trim: true).ToString().Should().BeEmpty();
        "\n".TakeLine(trim: false).ToString().Should().BeEmpty();
        "abc\n".TakeLine(trim: true).ToString().Should().Be("abc");
        "abc\n".TakeLine(trim: false).ToString().Should().Be("abc");
        "abc\r".TakeLine(trim: true).ToString().Should().Be("abc");
        "abc\r".TakeLine(trim: false).ToString().Should().Be("abc");
        "abc\r\n".TakeLine(trim: true).ToString().Should().Be("abc");
        "abc\r\n".TakeLine(trim: false).ToString().Should().Be("abc");
        "\nabc".TakeLine(trim: true).ToString().Should().Be("abc");
        "\nabc".TakeLine(trim: false).ToString().Should().BeEmpty();
        "\rabc".TakeLine(trim: true).ToString().Should().Be("abc");
        "\rabc".TakeLine(trim: false).ToString().Should().BeEmpty();
        "\r\nabc".TakeLine(trim: true).ToString().Should().Be("abc");
        "\r\nabc".TakeLine(trim: false).ToString().Should().BeEmpty();
    }

    [TestMethod]
    public void TakeLastLine()
    {
        "abc\ndef".TakeLastLine(trim: true).ToString().Should().Be("def");
        "abc\ndef".TakeLastLine(trim: false).ToString().Should().Be("def");
        "aaa\rbbb".TakeLastLine(trim: true).ToString().Should().Be("bbb");
        "aaa\rbbb".TakeLastLine(trim: false).ToString().Should().Be("bbb");
        "xyz\r\nabc".TakeLastLine(trim: true).ToString().Should().Be("abc");
        "xyz\r\nabc".TakeLastLine(trim: false).ToString().Should().Be("abc");
        "".TakeLastLine(trim: true).ToString().Should().BeEmpty();
        "".TakeLastLine(trim: false).ToString().Should().BeEmpty();
        default(string).TakeLastLine(trim: true).ToString().Should().BeEmpty();
        default(string).TakeLastLine(trim: false).ToString().Should().BeEmpty();

        "\n".TakeLastLine(trim: true).ToString().Should().BeEmpty();
        "\n".TakeLastLine(trim: false).ToString().Should().BeEmpty();
        "abc\n".TakeLastLine(trim: true).ToString().Should().Be("abc");
        "abc\n".TakeLastLine(trim: false).ToString().Should().BeEmpty();
        "abc\r".TakeLastLine(trim: true).ToString().Should().Be("abc");
        "abc\r".TakeLastLine(trim: false).ToString().Should().BeEmpty();
        "abc\r\n".TakeLastLine(trim: true).ToString().Should().Be("abc");
        "abc\r\n".TakeLastLine(trim: false).ToString().Should().BeEmpty();
        "\nabc".TakeLastLine(trim: true).ToString().Should().Be("abc");
        "\nabc".TakeLastLine(trim: false).ToString().Should().Be("abc");
        "\rabc".TakeLastLine(trim: true).ToString().Should().Be("abc");
        "\rabc".TakeLastLine(trim: false).ToString().Should().Be("abc");
        "\r\nabc".TakeLastLine(trim: true).ToString().Should().Be("abc");
        "\r\nabc".TakeLastLine(trim: false).ToString().Should().Be("abc");
    }

    [TestMethod]
    public void SkipLine()
    {
        "abc\ndef".SkipLine(trim: true).ToString().Should().Be("def");
        "abc\ndef".SkipLine(trim: false).ToString().Should().Be("def");
        "aaa\rbbb".SkipLine(trim: true).ToString().Should().Be("bbb");
        "aaa\rbbb".SkipLine(trim: false).ToString().Should().Be("bbb");
        "xyz\r\nabc".SkipLine(trim: true).ToString().Should().Be("abc");
        "xyz\r\nabc".SkipLine(trim: false).ToString().Should().Be("abc");
        "".SkipLine(trim: true).ToString().Should().BeEmpty();
        "".SkipLine(trim: false).ToString().Should().BeEmpty();
        default(string).SkipLine(trim: true).ToString().Should().BeEmpty();
        default(string).SkipLine(trim: false).ToString().Should().BeEmpty();

        "\n".SkipLine(trim: true).ToString().Should().BeEmpty();
        "\n".SkipLine(trim: false).ToString().Should().BeEmpty();
        "abc\n".SkipLine(trim: true).ToString().Should().BeEmpty();
        "abc\n".SkipLine(trim: false).ToString().Should().BeEmpty();
        "abc\n\n".SkipLine(trim: true).ToString().Should().BeEmpty();
        "abc\n\n".SkipLine(trim: false).ToString().Should().Be("\n");
        "abc\r".SkipLine(trim: true).ToString().Should().BeEmpty();
        "abc\r".SkipLine(trim: false).ToString().Should().BeEmpty();
        "abc\r\r".SkipLine(trim: true).ToString().Should().BeEmpty();
        "abc\r\r".SkipLine(trim: false).ToString().Should().Be("\r");
        "abc\r\n".SkipLine(trim: true).ToString().Should().BeEmpty();
        "abc\r\n".SkipLine(trim: false).ToString().Should().BeEmpty();
        "abc\r\n\r\n".SkipLine(trim: true).ToString().Should().BeEmpty();
        "abc\r\n\r\n".SkipLine(trim: false).ToString().Should().Be("\r\n");
        "\nabc".SkipLine(trim: true).ToString().Should().BeEmpty();
        "\nabc".SkipLine(trim: false).ToString().Should().Be("abc");
        "\rabc".SkipLine(trim: true).ToString().Should().BeEmpty();
        "\rabc".SkipLine(trim: false).ToString().Should().Be("abc");
        "\r\nabc".SkipLine(trim: true).ToString().Should().BeEmpty();
        "\r\nabc".SkipLine(trim: false).ToString().Should().Be("abc");
    }

    [TestMethod]
    public void TakeSkipLine()
    {
        {
            var line = "\nabc\n\ndef\n\nghi".TakeSkipLine(out var next, trim: true);
            line.ToString().Should().Be("abc");
            next.ToString().Should().Be("def\n\nghi");
        }
        {
            var line = "\nabc\n\ndef\n\nghi".TakeSkipLine(out var next, trim: false);
            line.ToString().Should().Be("");
            next.ToString().Should().Be("abc\n\ndef\n\nghi");
        }
        {
            var line = "\r\nabc\r\n\n\rdef\r\nghi".TakeSkipLine(out var next, trim: true);
            line.ToString().Should().Be("abc");
            next.ToString().Should().Be("def\r\nghi");
        }
        {
            var line = "\r\n\r\n".TakeSkipLine(out var next, trim: true);
            line.ToString().Should().Be("");
            next.ToString().Should().Be("");
        }
        {
            var line = "\r\n\r\n".TakeSkipLine(out var next, trim: false);
            line.ToString().Should().Be("");
            next.ToString().Should().Be("\r\n");
        }
        {
            var line = "".TakeSkipLine(out var next, trim: true);
            line.ToString().Should().Be("");
            next.ToString().Should().Be("");
        }
    }

    [TestMethod]
    public void TakeToken()
    {
        "ab cd ef".AsSpan().TakeToken().ToString().Should().Be("ab");
        "ab  cd  ef".AsSpan().TakeToken().ToString().Should().Be("ab");
        "  ab cd ef".AsSpan().TakeToken().ToString().Should().Be("ab");
        "  ab  cd ef".AsSpan().TakeToken().ToString().Should().Be("ab");

        "abcdef".AsSpan().TakeToken().ToString().Should().Be("abcdef");
        "  abcdef".AsSpan().TakeToken().ToString().Should().Be("abcdef");

        "ab cd,ef".AsSpan().TakeToken(',').ToString().Should().Be("ab cd");
        "ab  cd,ef".AsSpan().TakeToken(',').ToString().Should().Be("ab  cd");
        ",,ab cd,ef".AsSpan().TakeToken(',').ToString().Should().Be("ab cd");
        ",,ab cd,,ef".AsSpan().TakeToken(',').ToString().Should().Be("ab cd");
        "  ab cd,ef".AsSpan().TakeToken(',').ToString().Should().Be("  ab cd");
        "  ,,ab cd,ef".AsSpan().TakeToken(',').ToString().Should().Be("  ");

        "abcdef".AsSpan().TakeToken(',').ToString().Should().Be("abcdef");
        ",,abcdef".AsSpan().TakeToken(',').ToString().Should().Be("abcdef");
        "  abcdef".AsSpan().TakeToken(',').ToString().Should().Be("  abcdef");

        "ab cd ef".TakeToken().ToString().Should().Be("ab");
        "  ab cd,ef".TakeToken(',').ToString().Should().Be("  ab cd");
    }

    [TestMethod]
    public void TakeTokenAny()
    {
        "ab cd ef".AsSpan().TakeTokenAny([' ']).ToString().Should().Be("ab");
        "ab  cd  ef".AsSpan().TakeTokenAny([' ']).ToString().Should().Be("ab");
        "  ab cd ef".AsSpan().TakeTokenAny([' ']).ToString().Should().Be("ab");
        "  ab  cd ef".AsSpan().TakeTokenAny([' ']).ToString().Should().Be("ab");
        "abcdef".AsSpan().TakeTokenAny([' ']).ToString().Should().Be("abcdef");
        "  abcdef".AsSpan().TakeTokenAny([' ']).ToString().Should().Be("abcdef");

        "ab,cd,ef".AsSpan().TakeTokenAny([',']).ToString().Should().Be("ab");
        "ab,,cd,,ef".AsSpan().TakeTokenAny([',']).ToString().Should().Be("ab");
        ",,ab,cd,ef".AsSpan().TakeTokenAny([',']).ToString().Should().Be("ab");
        ",,ab,,cd,ef".AsSpan().TakeTokenAny([',']).ToString().Should().Be("ab");
        "abcdef".AsSpan().TakeTokenAny([',']).ToString().Should().Be("abcdef");
        ",,abcdef".AsSpan().TakeTokenAny([',']).ToString().Should().Be("abcdef");

        "ab,cd,ef".AsSpan().TakeTokenAny([' ']).ToString().Should().Be("ab,cd,ef");
        "ab cd ef".AsSpan().TakeTokenAny([',']).ToString().Should().Be("ab cd ef");

        "ab cd,ef".AsSpan().TakeTokenAny([' ', ',']).ToString().Should().Be("ab");
        "ab,cd ef".AsSpan().TakeTokenAny([' ', ',']).ToString().Should().Be("ab");

        "ab cd ef".TakeTokenAny([' ']).ToString().Should().Be("ab");
        "ab,cd ef".TakeTokenAny([' ', ',']).ToString().Should().Be("ab");
    }

    [TestMethod]
    public void SkipToken()
    {
        "ab cd ef".AsSpan().SkipToken().ToString().Should().Be("cd ef");
        "ab  cd  ef".AsSpan().SkipToken().ToString().Should().Be("cd  ef");
        "  ab cd ef".AsSpan().SkipToken().ToString().Should().Be("cd ef");
        "  ab  cd ef".AsSpan().SkipToken().ToString().Should().Be("cd ef");
        "abcdef".AsSpan().SkipToken().ToString().Should().Be("");
        "  abcdef".AsSpan().SkipToken().ToString().Should().Be("");

        "ab cd,ef,gh ij".AsSpan().SkipToken(',').ToString().Should().Be("ef,gh ij");
        "ab  cd,ef,gh ij".AsSpan().SkipToken(',').ToString().Should().Be("ef,gh ij");
        ",,ab cd,ef,gh ij".AsSpan().SkipToken(',').ToString().Should().Be("ef,gh ij");
        ",,ab cd,,ef,gh ij".AsSpan().SkipToken(',').ToString().Should().Be("ef,gh ij");
        "  ab cd,ef,gh ij".AsSpan().SkipToken(',').ToString().Should().Be("ef,gh ij");
        "  ,,ab cd,ef,gh ij".AsSpan().SkipToken(',').ToString().Should().Be("ab cd,ef,gh ij");
        "  ,,ab cd,,ef,gh ij".AsSpan().SkipToken(',').ToString().Should().Be("ab cd,,ef,gh ij");
        "abcdef".AsSpan().SkipToken(',').ToString().Should().Be("");
        ",,abcdef".AsSpan().SkipToken(',').ToString().Should().Be("");
        "  abcdef".AsSpan().SkipToken(',').ToString().Should().Be("");

        "ab cd ef".SkipToken().ToString().Should().Be("cd ef");
        "  ab cd,ef,gh ij".SkipToken(',').ToString().Should().Be("ef,gh ij");
    }

    [TestMethod]
    public void SkipTokenAny()
    {
        "ab cd ef".AsSpan().SkipTokenAny([' ']).ToString().Should().Be("cd ef");
        "ab  cd  ef".AsSpan().SkipTokenAny([' ']).ToString().Should().Be("cd  ef");
        "  ab cd ef".AsSpan().SkipTokenAny([' ']).ToString().Should().Be("cd ef");
        "  ab  cd  ef".AsSpan().SkipTokenAny([' ']).ToString().Should().Be("cd  ef");
        "abcdef".AsSpan().SkipTokenAny([' ']).ToString().Should().Be("");
        "  abcdef".AsSpan().SkipTokenAny([' ']).ToString().Should().Be("");

        "12 ab,cd,ef".AsSpan().SkipTokenAny([',']).ToString().Should().Be("cd,ef");
        "12 ab,,cd,,ef".AsSpan().SkipTokenAny([',']).ToString().Should().Be("cd,,ef");
        ",,ab,cd,ef".AsSpan().SkipTokenAny([',']).ToString().Should().Be("cd,ef");
        ",,ab,,cd,ef".AsSpan().SkipTokenAny([',']).ToString().Should().Be("cd,ef");
        "  ,,ab,cd,ef".AsSpan().SkipTokenAny([',']).ToString().Should().Be("ab,cd,ef");
        "  ,,ab,,cd,ef".AsSpan().SkipTokenAny([',']).ToString().Should().Be("ab,,cd,ef");
        "abcdef".AsSpan().SkipTokenAny([',']).ToString().Should().Be("");
        ",,abcdef".AsSpan().SkipTokenAny([',']).ToString().Should().Be("");

        "ab,cd,ef".AsSpan().SkipTokenAny([' ']).ToString().Should().Be("");
        "ab cd ef".AsSpan().SkipTokenAny([',']).ToString().Should().Be("");

        "ab cd,ef".AsSpan().SkipTokenAny([' ', ',']).ToString().Should().Be("cd,ef");
        "ab,cd ef".AsSpan().SkipTokenAny([' ', ',']).ToString().Should().Be("cd ef");

        "ab cd ef".SkipTokenAny([' ']).ToString().Should().Be("cd ef");
        "ab,cd ef".SkipTokenAny([' ', ',']).ToString().Should().Be("cd ef");
    }

    [TestMethod]
    public void TakeSkipToken()
    {
        static void assert_def(string source, Func<(string token, string next)> expectation)
        {
            var token = source.TakeSkipToken(out var next);
            var expect = expectation();
            token.ToString().Should().Be(expect.token);
            next.ToString().Should().Be(expect.next);
        }
        static void assert_delim(string source, char delimiter, Func<(string token, string next)> expectation)
        {
            var token = source.TakeSkipToken(out var next, delimiter);
            var expect = expectation();
            token.ToString().Should().Be(expect.token);
            next.ToString().Should().Be(expect.next);
        }

        assert_def("ab  cd  ef", () => ("ab", "cd  ef"));
        assert_def("  ab  cd  ef", () => ("ab", "cd  ef"));
        assert_def("  abcdef", () => ("abcdef", ""));

        assert_delim("ab cd,,ef,gh ij", ',', () => ("ab cd", "ef,gh ij"));
        assert_delim(",,ab cd,,ef,gh ij", ',', () => ("ab cd", "ef,gh ij"));
    }

    [TestMethod]
    public void TakeSkipTokenAny()
    {
        static void assert(string source, ReadOnlySpan<char> delimiters, Func<(string token, string next)> expectation)
        {
            var token = source.TakeSkipTokenAny(out var next, delimiters);
            var expect = expectation();
            token.ToString().Should().Be(expect.token);
            next.ToString().Should().Be(expect.next);
        }

        assert("ab  cd  ef", [' '], () => ("ab", "cd  ef"));
        assert("  ab  cd  ef", [' '], () => ("ab", "cd  ef"));
        assert("  abcdef", [' '], () => ("abcdef", ""));
        assert(",,ab cd,,ef,gh ij", [','], () => ("ab cd", "ef,gh ij"));
        assert(",,ab cd,,ef,gh ij", [' ', ','], () => ("ab", "cd,,ef,gh ij"));

        {
            var token = "ab  cd  ef".TakeSkipTokenAny(out var next, [' ']);
            token.ToString().Should().Be("ab");
            next.ToString().Should().Be("cd  ef");
        }
        {
            var token = ",,ab cd,,ef,gh ij".TakeSkipTokenAny(out var next, [' ', ',']);
            token.ToString().Should().Be("ab");
            next.ToString().Should().Be("cd,,ef,gh ij");
        }
    }

    [TestMethod]
    public void TakeTokenAndSkipTake_Scenario()
    {
        var text = "  abc def   ghi  ";

        var scan = text.AsSpan();
        var token1 = scan.TakeToken();
        scan = scan.SkipToken();
        var token2 = scan.TakeToken();
        scan = scan.SkipToken();
        var token3 = scan.TakeToken();

        token1.ToString().Should().Be("abc");
        token2.ToString().Should().Be("def");
        token3.ToString().Should().Be("ghi");
    }

    [TestMethod]
    public void TakeSkipToken_Scenario()
    {
        var text = "  abc def   ghi   jkl   mno";

        var scan = text.AsSpan();
        var token1 = scan.TakeSkipToken(out scan);
        var token2 = scan.TakeSkipToken(out scan);
        var token3 = scan.TakeSkipToken(out scan);

        token1.ToString().Should().Be("abc");
        token2.ToString().Should().Be("def");
        token3.ToString().Should().Be("ghi");

        scan.ToString().Should().Be("jkl   mno");
    }

}
