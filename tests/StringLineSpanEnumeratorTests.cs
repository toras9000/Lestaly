namespace LestalyTest;

[TestClass]
public class StringLineSpanEnumeratorTests
{
    [TestMethod]
    public void Enumerate()
    {
        var source = "abc\rdef\nghi\r\njkl";
        var enumerator = new StringLineSpanEnumerator(source);
        enumerator.Current.Length.Should().Be(0);

        var lines = new List<string>();
        foreach (var line in enumerator)
        {
            lines.Add(line.ToString());
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
        var enumerator = new StringLineSpanEnumerator(source, raw: false);

        var lines = new List<string>();
        foreach (var line in enumerator)
        {
            lines.Add(line.ToString());
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
        var enumerator = new StringLineSpanEnumerator(source, raw: true);

        var lines = new List<string>();
        foreach (var line in enumerator)
        {
            lines.Add(line.ToString());
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

    [TestMethod]
    public void EmptySource()
    {
        var source = "";
        var enumerator = new StringLineSpanEnumerator(source, raw: false);

        var lines = new List<string>();
        foreach (var line in enumerator)
        {
            lines.Add(line.ToString());
        }

        lines.Should().BeEmpty();
    }
}
