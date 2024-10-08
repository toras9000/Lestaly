﻿using System.Text;

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
        {
            var text = "abcdef";
            var expect = Encoding.UTF8.GetBytes(text);

            text.EncodeUtf8().Should().Equal(expect);
            text.ToArray().EncodeUtf8().Should().Equal(expect);
            text.ToArray().AsSpan().EncodeUtf8().Should().Equal(expect);
            text.AsSpan().EncodeUtf8().Should().Equal(expect);
        }
        {
            var text = new string('a', 2000);
            var expect = Encoding.UTF8.GetBytes(text);

            text.EncodeUtf8().Should().Equal(expect);
            text.ToArray().EncodeUtf8().Should().Equal(expect);
            text.ToArray().AsSpan().EncodeUtf8().Should().Equal(expect);
            text.AsSpan().EncodeUtf8().Should().Equal(expect);
        }
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
    public void DecodeUtf8Base64_String()
    {
        var text = "abcdef";

        var encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
        encoded.DecodeUtf8Base64().Should().Be(text);

        ("*" + encoded).DecodeUtf8Base64().Should().BeNull();
    }

    [TestMethod()]
    public void DecodeUtf8Base64_Chars()
    {
        var text = "abcdef";

        var encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
        encoded.AsSpan().DecodeUtf8Base64().Should().Be(text);

        ("*" + encoded).DecodeUtf8Base64().Should().BeNull();
    }

    [TestMethod()]
    public void DecodeUtf8Base64_Utf8Bytes()
    {
        var text = "abcdef";

        var encodedBytes = Encoding.UTF8.GetBytes(Convert.ToBase64String(Encoding.UTF8.GetBytes(text)));
        var illegalBytes = new byte[encodedBytes.Length + 1];
        illegalBytes[0] = (byte)'*';
        encodedBytes.CopyTo(illegalBytes.AsSpan(1));
        illegalBytes.AsSpan().DecodeUtf8Base64().Should().BeNull();
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
    public void EncodeBase64()
    {
        ReadOnlySpan<byte> bin = [0x56, 0x3f, 0xbc, 0x1b, 0xf2, 0xd0, 0x68,];

        bin.EncodeBase64().Should().Be("Vj+8G/LQaA==");
    }

    [TestMethod()]
    public void DecodeBase64_Chars()
    {
        var text = "abcdef";
        var binary = Encoding.UTF8.GetBytes(text);

        var encoded = Convert.ToBase64String(binary);
        encoded.AsSpan().DecodeBase64().Should().Equal(binary);

        ("*" + encoded).AsSpan().DecodeBase64().Should().BeNull();
    }

    [TestMethod()]
    public void DecodeBase64_Utf8Bytes()
    {
        var text = "abcdef";
        var binary = Encoding.UTF8.GetBytes(text);

        var encoded = Encoding.UTF8.GetBytes(Convert.ToBase64String(binary));
        encoded.AsSpan().AsReadOnly().DecodeBase64().Should().Equal(binary);

        byte[] illegal = [(byte)'*', .. encoded];
        illegal.AsSpan().AsReadOnly().DecodeBase64().Should().BeNull();
    }


    [TestMethod()]
    public void EncodeBase64Url()
    {
        ReadOnlySpan<byte> bin = [0x56, 0x3f, 0xbc, 0x1b, 0xf2, 0xd0, 0x68,];

        bin.EncodeBase64Url().Should().Be("Vj-8G_LQaA");
    }


    [TestMethod()]
    public void ToHexString()
    {
        var binary = new byte[] { 0x12, 0x34, 0xAB, 0xEF };

        binary.ToHexString().Should().Be("1234ABEF");
        binary.ToHexString("-").Should().Be("12-34-AB-EF");
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
