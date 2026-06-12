using System.Buffers;

namespace LestalyTest.Extensions;

[TestClass]
public class BufferWriterExtensionsTests
{
    [TestMethod]
    public void WriteLittleEndian()
    {
        var buffer = new ArrayBufferWriter<byte>();
        buffer.WriteLittleEndian<uint>(0x12345678);
        buffer.WriteLittleEndian<byte>(0x99);
        buffer.WriteLittleEndian<ushort>(0xABCD);

        buffer.WrittenSpan.ToArray().Should().Equal([
            0x78, 0x56, 0x34, 0x12,
            0x99,
            0xCD, 0xAB,
        ]);
    }

    [TestMethod]
    public void WriteBigEndian()
    {
        var buffer = new ArrayBufferWriter<byte>();
        buffer.WriteBigEndian<uint>(0x12345678);
        buffer.WriteBigEndian<byte>(0x99);
        buffer.WriteBigEndian<ushort>(0xABCD);

        buffer.WrittenSpan.ToArray().Should().Equal([
            0x12, 0x34, 0x56, 0x78,
            0x99,
            0xAB, 0xCD,
        ]);
    }
}
