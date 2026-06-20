namespace LestalyTest;

[TestClass]
public class StringLineRangeEnumeratorTests
{
    [TestMethod]
    public void Enumerate()
    {
        var source = "abc\rdef\nghi\r\njkl";
        var enumerator = new StringLineRangeEnumerator(source);
        source[enumerator.Current].Length.Should().Be(0);

        var lines = new List<string>();
        foreach (var range in enumerator)
        {
            lines.Add(source[range]);
        }

        lines.Should().Equal([
            "abc",
            "def",
            "ghi",
            "jkl",
        ]);
    }

    [TestMethod]
    public void EmptyLine()
    {
        var source = "a\r\rb\n\nc\r\n\r\nd\r\n\n\re";
        var enumerator = new StringLineRangeEnumerator(source);
        source[enumerator.Current].Length.Should().Be(0);

        var lines = new List<string>();
        foreach (var range in enumerator)
        {
            lines.Add(source[range]);
        }

        lines.Should().Equal([
            "a",
            "",
            "b",
            "",
            "c",
            "",
            "d",
            "",
            "",
            "e",
        ]);
    }

    [TestMethod]
    public void EmptyLine_Raw()
    {
        var source = "a\r\rb\n\nc\r\n\r\nd\r\n\n\re";
        var enumerator = new StringLineRangeEnumerator(source, raw: true);
        source[enumerator.Current].Length.Should().Be(0);

        var lines = new List<string>();
        foreach (var range in enumerator)
        {
            lines.Add(source[range]);
        }

        lines.Should().Equal([
            "a\r",
            "\r",
            "b\n",
            "\n",
            "c\r\n",
            "\r\n",
            "d\r\n",
            "\n",
            "\r",
            "e",
        ]);
    }
}