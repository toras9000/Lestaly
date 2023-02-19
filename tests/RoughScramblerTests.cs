using FluentAssertions;

namespace LestalyTest;

[TestClass()]
public class RoughScramblerTests
{
    [TestMethod()]
    public void Scramble_NoParam()
    {
        var data = new byte[] { 0x12, 0x34, 0x56, };

        var enc1 = new RoughScrambler().Scramble(data);
        var enc2 = new RoughScrambler().Scramble(data);

        enc1.Should().NotBeNull();
        enc2.Should().NotBeNull();

        enc1.Should().Equal(enc2);
    }

    [TestMethod()]
    public void Scramble_WithParam()
    {
        var data = new byte[] { 0x12, 0x34, 0x56, };

        var enc1 = new RoughScrambler("abc").Scramble(data);
        var enc2 = new RoughScrambler("def").Scramble(data);
        var enc3 = new RoughScrambler("abc", "zyx").Scramble(data);
        var enc4 = new RoughScrambler("abc", "zyx").Scramble(data);

        enc1.Should().NotBeNull();
        enc2.Should().NotBeNull();
        enc3.Should().NotBeNull();
        enc4.Should().NotBeNull();

        enc1.Should().NotEqual(enc2);
        enc1.Should().NotEqual(enc3);

        enc3.Should().Equal(enc4);
    }

    [TestMethod()]
    public void Scramble_Empty()
    {
        var enc1 = new RoughScrambler().Scramble(Array.Empty<byte>());
        var enc2 = new RoughScrambler("asd").Scramble(Array.Empty<byte>());

        enc1.Should().NotBeNull();
        enc2.Should().NotBeNull();

        enc1.Should().NotEqual(enc2);
    }

    [TestMethod()]
    public void Descramble_NoParam()
    {
        var data = new byte[] { 0x12, 0x34, 0x56, };

        var scrambler = new RoughScrambler();
        var enc = scrambler.Scramble(data);
        var dec = scrambler.Descramble(enc);

        dec.Should().Equal(data);
    }

    [TestMethod()]
    public void Descramble_WithParam()
    {
        var data = new byte[] { 0x12, 0x34, 0x56, };

        var enc = new RoughScrambler("asd").Scramble(data);
        var dec = new RoughScrambler("asd").Descramble(enc);

        dec.Should().Equal(data);
    }

    [TestMethod()]
    public void Descramble_Empty()
    {
        var enc1 = new RoughScrambler().Scramble(Array.Empty<byte>());
        var dec1 = new RoughScrambler().Descramble(enc1);

        var enc2 = new RoughScrambler("asd").Scramble(Array.Empty<byte>());
        var dec2 = new RoughScrambler("asd").Descramble(enc2);

        dec1.Should().BeEmpty();
        dec2.Should().BeEmpty();
    }

    [TestMethod()]
    public void Descramble_Fail()
    {
        var data = new byte[] { 0x12, 0x34, 0x56, };

        var scrambler1 = new RoughScrambler();
        var scrambler2 = new RoughScrambler("asd");

        var enc = scrambler1.Scramble(data);

        new Action(() => scrambler2.Descramble(enc)).Should().Throw<Exception>();
    }
}