using System.Linq.Expressions;
using System.Reflection;
using FluentAssertions;

namespace LestalyTest.Extensions;

[TestClass()]
public class StringDataExtensionsTests
{
    [TestMethod()]
    public void SplitFieldsTest_Normal()
    {
        var text = "";
        text += "aaa,bbb,ccc\r\n";
        text += "ddd,eee,,,\r\n";
        text += ",,fff,,ggg\r\n";
        text += "hhh,iii";

        var options = new SplitFieldsOptions { Separator = ',', };

        text.SplitFields(options).Should().BeEquivalentTo(new[]
        {
            new[]{ "aaa", "bbb", "ccc",            },
            new[]{ "ddd", "eee", "",    "", "",    },
            new[]{ "",    "",    "fff", "", "ggg", },
            new[]{ "hhh", "iii",                   },
        });
    }

    [TestMethod()]
    public void SplitFieldsTest_SeparatorOther()
    {
        var text = "";
        text += "aaa/bbb/ccc\r\n";
        text += "ddd/eee///\r\n";
        text += "//fff//ggg\r\n";
        text += "hhh/iii";

        var options = new SplitFieldsOptions { Separator = '/', };

        text.SplitFields(options).Should().BeEquivalentTo(new[]
        {
            new[]{ "aaa", "bbb", "ccc",            },
            new[]{ "ddd", "eee", "",    "", "",    },
            new[]{ "",    "",    "fff", "", "ggg", },
            new[]{ "hhh", "iii",                   },
        });
    }

    [TestMethod()]
    public void SplitFieldsTest_Quoted()
    {
        var text = "";
        text += "'aaa','b,b,b',ccc\r\n";
        text += "'ddd\r\nddd',eee,'fff''fff'\r\n";

        var options = new SplitFieldsOptions { Separator = ',', Quote = '\'', };

        text.SplitFields(options).Should().BeEquivalentTo(new[]
        {
            new[]{ "aaa",        "b,b,b", "ccc",   },
            new[]{ "ddd\r\nddd", "eee",   "fff'fff", },
        });
    }

    [TestMethod()]
    public void SplitFieldsTest_QuotedOther()
    {
        var text = "";
        text += "\"aaa\",\"b,b,b\",ccc\r\n";
        text += "\"ddd\r\nddd\",eee,\"fff\"\"fff\"\r\n";

        var options = new SplitFieldsOptions { Separator = ',', Quote = '"', };

        text.SplitFields(options).Should().BeEquivalentTo(new[]
        {
            new[]{ "aaa",        "b,b,b", "ccc",   },
            new[]{ "ddd\r\nddd", "eee",   "fff\"fff", },
        });
    }

    [TestMethod()]
    public void SplitFieldsTest_MiddleQuote()
    {
        var text = "";
        text += "aaa,b'b'b,ccc\r\n";
        text += "ddd,ee'e,ff'f\r\n";

        var options = new SplitFieldsOptions { Separator = ',', Quote = '\'', };

        text.SplitFields(options).Should().BeEquivalentTo(new[]
        {
            new[]{ "aaa", "bbb", "ccc",      },
            new[]{ "ddd", "eee,fff", },
        });
    }

    [TestMethod()]
    public void SplitFieldsTest_EscapeQuote()
    {
        var text = "";
        text += "'''aaa',b'bb''',''''\r\n";

        var options = new SplitFieldsOptions { Separator = ',', Quote = '\'', };

        text.SplitFields(options).Should().BeEquivalentTo(new[]
        {
            new[]{ "'aaa", "bbb'", "'",      },
        });
    }

    [TestMethod()]
    public void SplitFieldsTest_NoQuote()
    {
        var text = "";
        text += "aaa,'bbb',ccc\r\n";
        text += "ddd,ee\"e,ff\"f\r\n";

        var options = new SplitFieldsOptions { Separator = ',', Quote = null, };

        text.SplitFields(options).Should().BeEquivalentTo(new[]
        {
            new[]{ "aaa", "'bbb'", "ccc",   },
            new[]{ "ddd", "ee\"e", "ff\"f", },
        });
    }

    [TestMethod()]
    public void SplitFieldsTest_UnCloseQuote()
    {
        var text = "";
        text += "aaa,bbb,c'cc\r\n";
        text += "ddd,eee,fff\r\n";

        var options = new SplitFieldsOptions { Separator = ',', Quote = '\'', };

        text.SplitFields(options).Should().BeEquivalentTo(new[]
        {
            new[]{ "aaa", "bbb", "ccc\r\nddd,eee,fff\r\n", },
        });
    }

    [TestMethod()]
    public void SplitFieldsTest_NewLine()
    {
        var text = "";
        text += "aaa,bbb,ccc\r";
        text += "ddd,eee,fff\n";
        text += "ggg\n\r";

        var options = new SplitFieldsOptions { Separator = ',', Quote = '\'', };

        text.SplitFields(options).Should().BeEquivalentTo(new[]
        {
            new[]{ "aaa", "bbb", "ccc", },
            new[]{ "ddd", "eee", "fff", },
            new[]{ "ggg", },
            new string [0],
        });
    }

    [TestMethod()]
    public void SplitFieldsTest_EmptyLine()
    {
        var text = "";
        text += "aaa,bbb\r\n";
        text += "\r\n";
        text += "ccc,ddd\r\n";

        var options = new SplitFieldsOptions { Separator = ',', };

        text.SplitFields(options).Should().BeEquivalentTo(new[]
        {
            new[]{ "aaa", "bbb", },
            new string [0],
            new[]{ "ccc", "ddd", },
        });
    }

    [TestMethod()]
    public void SplitFieldsTest_AllAmpty()
    {
        var text = "";
        text += "\r\n";
        text += "\r\n";
        text += "\r\n";

        var options = new SplitFieldsOptions { Separator = ',', };

        text.SplitFields(options).Should().BeEquivalentTo(new[]
        {
            new string [0],
            new string [0],
            new string [0],
        });
    }
}