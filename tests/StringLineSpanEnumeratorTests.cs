namespace LestalyTest;

[TestClass]
public class StringLineSpanEnumeratorTests
{
    [TestMethod]
    public void Enumerate()
    {
        var source = "abc\rdef\nghi\r\njkl";
        var enumerator = new StringLineSpanEnumerator(source);
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
        var enumerator = new StringLineSpanEnumerator(source);
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
}