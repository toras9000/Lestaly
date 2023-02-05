using System.Text;
using FluentAssertions;

namespace LestalyTest.Extensions;

[TestClass()]
public class StringEncodeExtensionsTests
{
    [TestMethod()]
    public void ReadUtf8()
    {
        var encText = Encoding.UTF8.GetBytes("abcあいう");
        encText.AsReadOnlySpan().ReadUtf8().Should().Be("abcあいう");
    }

    [TestMethod()]
    public void WriteUtf8()
    {
        var buffer = new byte[100];

        var len = buffer.AsSpan().WriteUtf8("defかきく");
        buffer.AsSpan(0, len).ToArray().Should().Equal(Encoding.UTF8.GetBytes("defかきく"));
    }

    [TestMethod()]
    public void EncodeUtf8()
    {
        var text = "abcdef";

        var expect = Encoding.UTF8.GetBytes(text);

        text.EncodeUtf8().Should().Equal(expect);
        text.ToArray().EncodeUtf8().Should().Equal(expect);
        text.ToArray().AsSpan().EncodeUtf8().Should().Equal(expect);
        text.AsSpan().EncodeUtf8().Should().Equal(expect);
    }

    [TestMethod()]
    public void DecodeUtf8()
    {
        var text = "abcdef";

        var encoded = Encoding.UTF8.GetBytes(text);

        encoded.DecodeUtf8().Should().Be(text);
        encoded.AsSpan().DecodeUtf8().Should().Be(text);
        encoded.AsSpan().DecodeUtf8().Should().Be(text);
        ((ReadOnlySpan<byte>)encoded.AsSpan()).DecodeUtf8().Should().Be(text);
    }

    [TestMethod()]
    public void EncodeUtf8Base64()
    {
        var text = "abcdef";

        var expect = Convert.ToBase64String(Encoding.UTF8.GetBytes(text));

        text.EncodeUtf8Base64().Should().Be(expect);
        text.ToArray().EncodeUtf8Base64().Should().Be(expect);
        text.ToArray().AsSpan().EncodeUtf8Base64().Should().Be(expect);
        text.AsSpan().EncodeUtf8Base64().Should().Be(expect);
    }

    [TestMethod()]
    public void DecodeUtf8Base64()
    {
        var text = "abcdef";

        var encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(text));

        encoded.DecodeUtf8Base64().Should().Be(text);
    }

    [TestMethod()]
    public void EncodeUtf8Hex()
    {
        var text = "abcdef";

        var expect = Convert.ToHexString(Encoding.UTF8.GetBytes(text));

        text.EncodeUtf8Hex().Should().Be(expect);
        text.ToArray().EncodeUtf8Hex().Should().Be(expect);
        text.ToArray().AsSpan().EncodeUtf8Hex().Should().Be(expect);
        text.AsSpan().EncodeUtf8Hex().Should().Be(expect);
    }

    [TestMethod()]
    public void DecodeUtf8Hex()
    {
        var text = "abcdef";

        var encoded = Convert.ToHexString(Encoding.UTF8.GetBytes(text));

        encoded.DecodeUtf8Hex().Should().Be(text);
        encoded.ToArray().DecodeUtf8Hex().Should().Be(text);
        encoded.ToArray().AsSpan().DecodeUtf8Hex().Should().Be(text);
        encoded.AsSpan().DecodeUtf8Hex().Should().Be(text);
    }

    [TestMethod()]
    public void EscapeUriData()
    {
        var text = "abcdef";

        var expect = Uri.EscapeDataString(text);

        text.EscapeUriData().Should().Be(expect);
    }

    [TestMethod()]
    public void UnescapeUriData()
    {
        var text = "abcdef";

        var encoded = Uri.EscapeDataString(text);

        encoded.UnescapeUriData().Should().Be(text);
    }

}
