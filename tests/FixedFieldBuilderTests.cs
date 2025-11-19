using CometFlavor.Unicode.Extensions.Text;

namespace LestalyTest;

[TestClass()]
public class FixedFieldBuilderTests
{
    [TestMethod()]
    public void Build()
    {
        var source = new[]
        {
            new{ date = new DateOnly(2000, 1, 2),   name = "abc",    num = 25,   rate = 1.2,   },
            new{ date = new DateOnly(2000, 3, 15),  name = "defghi", num = 3,    rate = 27.44, },
            new{ date = new DateOnly(2000, 11, 2),  name = "jlkmn",  num = 416,  rate = 331.5, },
            new{ date = new DateOnly(2000, 12, 31), name = "o",      num = 2973, rate = 0.01,  },
        };

        var builder = FixedFieldBuilder.Create(source);
        builder.AddField(o => o.name);
        builder.AddField(o => ": ");
        builder.AddField(o => o.num.ToString(), FixedFieldAlign.Right);
        builder.AddField(o => ", ");
        builder.AddField(o => o.rate.ToString("F2"), FixedFieldAlign.Right);
        builder.AddField(o => " : ");
        builder.AddField(o => o.date.ToString("yyyy/MM/dd"));

        var expect = new[]
        {
            "abc   :   25,   1.20 : 2000/01/02",
            "defghi:    3,  27.44 : 2000/03/15",
            "jlkmn :  416, 331.50 : 2000/11/02",
            "o     : 2973,   0.01 : 2000/12/31",
        };

        builder.BuildLines(useCache: false).Should().Equal(expect);
        builder.BuildLines(useCache: true).Should().Equal(expect);
    }

    [TestMethod()]
    public void WidthEval()
    {
        var widthMeas = new EawMeasure(1, 2, 2);

        var source = new[]
        {
            new{ name = "あいう",      value = "123"     },
            new{ name = "123",         value = "１２３"  },
            new{ name = "かきくけ45",  value = "100文字" },
            new{ name = "4567こ",      value = "２７%"   },
        };

        var builder = FixedFieldBuilder.Create(source, t => t.EvaluateEaw(widthMeas));
        builder.AddField(o => o.name);
        builder.AddField(o => ":");
        builder.AddField(o => o.value, FixedFieldAlign.Right);

        var expect = new[]
        {
            "あいう    :    123",
            "123       : １２３",
            "かきくけ45:100文字",
            "4567こ    :  ２７%",
        };

        builder.BuildLines(useCache: false).Should().Equal(expect);
        builder.BuildLines(useCache: true).Should().Equal(expect);
    }
}
