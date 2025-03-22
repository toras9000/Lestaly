using System.Text;

namespace LestalyTest;

[TestClass]
public class EmbedStringBuilderTests
{
    [TestMethod]
    public void Write_Simple()
    {
        var buffer = new StringBuilder();
        var builder = new EmbedStringBuilder();

        builder.Default = "***";
        builder.Variables["abc"] = "value1";
        builder.Variables["def"] = "value2";

        builder.Write("abc", buffer);
        buffer.ToString().Should().Be("abc");

        builder.Write("[$abc]", buffer);
        buffer.ToString().Should().Be("abc[value1]");

        builder.Write("ccc$abcd$def", buffer);
        buffer.ToString().Should().Be("abc[value1]ccc***value2");
    }

    [TestMethod]
    public void Write_Simple_Chars()
    {
        var buffer = new StringBuilder();
        var builder = new EmbedStringBuilder(c => c is >= 'a' and <= 'z');

        builder.Default = "***";
        builder.Variables["aaa"] = "value1";
        builder.Variables["AAA"] = "value2";

        buffer.Clear();
        builder.Write("$aaa", buffer);
        buffer.ToString().Should().Be("value1");

        buffer.Clear();
        builder.Write("$AAA", buffer);
        buffer.ToString().Should().Be("AAA");   // 変数名にならないキャラクタなのでそのまま
    }

    [TestMethod]
    public void Write_Enclose()
    {
        var buffer = new StringBuilder();
        var builder = new EmbedStringBuilder();

        builder.Default = "***";
        builder.Variables["abc"] = "value1";
        builder.Variables["def"] = "value2";

        builder.Write("${abc}", buffer);
        buffer.ToString().Should().Be("value1");

        builder.Write("ddd${def}ghi", buffer);
        buffer.ToString().Should().Be("value1dddvalue2ghi");

        builder.Write("${abcd}", buffer);
        buffer.ToString().Should().Be("value1dddvalue2ghi***");
    }

    [TestMethod]
    public void Write_Enclose_Chars()
    {
        var buffer = new StringBuilder();
        var builder = new EmbedStringBuilder(c => c is >= 'a' and <= 'z');

        builder.Default = "***";
        builder.Variables["aaa"] = "value1";
        builder.Variables["AAA"] = "value2";

        buffer.Clear();
        builder.Write("${aaa}", buffer);
        buffer.ToString().Should().Be("value1");

        buffer.Clear();
        builder.Write("${AAA}", buffer);
        buffer.ToString().Should().Be("value2");    // 範囲を囲っているので有効キャラクタ以外も変数名として参照される
    }

    [TestMethod]
    public void Build()
    {
        var builder = new EmbedStringBuilder();

        builder.Default = "***";
        builder.Variables["abc"] = "value1";
        builder.Variables["def"] = "value2";

        builder.Build("[$abc]").Should().Be("[value1]");
        builder.Build("[${abc}${def}]").Should().Be("[value1value2]");
    }
}
