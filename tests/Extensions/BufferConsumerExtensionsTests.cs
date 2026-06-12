using System.Buffers;
using Lestaly.Extensions;

namespace LestalyTest.Extensions;

[TestClass]
public class BufferConsumerExtensionsTests
{
    [TestMethod]
    public void ConsumeLittleEndian()
    {
        var buffer = new ArrayBufferConsumer<byte>();
        buffer.Write<byte>([
            0x12, 0x34, 0x56, 0x78,
            0x99,
            0xAB, 0xCD,
        ]);

        buffer.ConsumeLittleEndian<uint>().Should().Be(0x78563412);
        buffer.ConsumeLittleEndian<byte>().Should().Be(0x99);
        buffer.ConsumeLittleEndian<ushort>().Should().Be(0xCDAB);
        buffer.WrittenCount.Should().Be(0);
    }

    [TestMethod]
    public void ConsumeBigEndian()
    {
        var buffer = new ArrayBufferConsumer<byte>();
        buffer.Write<byte>([
            0x12, 0x34, 0x56, 0x78,
            0x99,
            0xAB, 0xCD,
        ]);

        buffer.ConsumeBigEndian<uint>().Should().Be(0x12345678);
        buffer.ConsumeBigEndian<byte>().Should().Be(0x99);
        buffer.ConsumeBigEndian<ushort>().Should().Be(0xABCD);
        buffer.WrittenCount.Should().Be(0);
    }
}
