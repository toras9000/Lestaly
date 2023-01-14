using FluentAssertions;

namespace LestalyTest.Extensions;

[TestClass()]
public class RoughScramblerExtensionsTests
{
    [TestMethod()]
    public void ScrambleText_NoParam()
    {
        var scrambler = new RoughScrambler();
        var enc = scrambler.ScrambleText("abc");
        var dec = scrambler.DescrambleText(enc);
        dec.Should().Be("abc");
    }

    [TestMethod()]
    public void ScrambleText_WithParam()
    {
        var scrambler = new RoughScrambler("xxx");
        var enc = scrambler.ScrambleText("abc");
        var dec = scrambler.DescrambleText(enc);
        dec.Should().Be("abc");
    }

    [TestMethod()]
    public void ScrambleText_Failed()
    {
        var scrambler1 = new RoughScrambler();
        var scrambler2 = new RoughScrambler("xxx");
        var enc = scrambler1.ScrambleText("abc");
        var dec = scrambler2.DescrambleText(enc);
        dec.Should().BeNull();
    }

    record TestItem(string Name, int Number, DateTime Timestamp);

    [TestMethod()]
    public void ScrambleJson_NoParam()
    {
        var item = new TestItem("asd", 123, new DateTime(2023, 1, 2, 3, 4, 5));
        var scrambler = new RoughScrambler();
        var enc = scrambler.ScrambleObject(item);
        var dec = scrambler.DescrambleObject<TestItem>(enc);
        dec.Should().BeEquivalentTo(item);
    }

    [TestMethod()]
    public void ScrambleJson_WithParam()
    {
        var item = new TestItem("asd", 123, new DateTime(2023, 1, 2, 3, 4, 5));
        var scrambler = new RoughScrambler("xyz");
        var enc = scrambler.ScrambleObject(item);
        var dec = scrambler.DescrambleObject<TestItem>(enc);
        dec.Should().BeEquivalentTo(item);
    }

    [TestMethod()]
    public void ScrambleJson_Failed()
    {
        var scrambler1 = new RoughScrambler();
        var scrambler2 = new RoughScrambler("xxx");
        var enc = scrambler1.ScrambleObject("abc");
        var dec = scrambler2.DescrambleObject<TestItem>(enc);
        dec.Should().BeNull();
    }
}