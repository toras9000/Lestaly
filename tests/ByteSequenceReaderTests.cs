namespace LestalyTest;

[TestClass]
public class ByteSequenceReaderTests
{
    [TestMethod]
    public void ReadLittleEndian()
    {
        var data = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x9A, 0xBC, 0xDE, 0xF0, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, };
        var reader = new ByteSequenceReader(data);
        reader.Source.Length.Should().Be(data.Length);
        reader.Consumed.Should().Be(0);
        reader.Remaining.Should().Be(data.Length);

        reader.ReadLittleEndian<ushort>().Should().Be(0x3412);
        reader.Source.Length.Should().Be(data.Length);
        reader.Consumed.Should().Be(2);
        reader.Remaining.Should().Be(data.Length - 2);

        reader.ReadLittleEndian<byte>().Should().Be(0x56);
        reader.Source.Length.Should().Be(data.Length);
        reader.Consumed.Should().Be(3);
        reader.Remaining.Should().Be(data.Length - 3);

        reader.ReadLittleEndian<uint>().Should().Be(0xDEBC9A78);
        reader.Source.Length.Should().Be(data.Length);
        reader.Consumed.Should().Be(7);
        reader.Remaining.Should().Be(data.Length - 7);

        reader.ReadLittleEndian<ulong>().Should().Be(0x77665544332211F0);
        reader.Source.Length.Should().Be(data.Length);
        reader.Consumed.Should().Be(15);
        reader.Remaining.Should().Be(data.Length - 15);

        var err = default(Exception);
        try { reader.ReadLittleEndian<uint>(); } catch (Exception ex) { err = ex; }
        err.Should().NotBeNull();
    }

    [TestMethod]
    public void ReadBigEndian()
    {
        var data = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x9A, 0xBC, 0xDE, 0xF0, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, };
        var reader = new ByteSequenceReader(data);
        reader.Source.Length.Should().Be(data.Length);
        reader.Consumed.Should().Be(0);
        reader.Remaining.Should().Be(data.Length);

        reader.ReadBigEndian<ushort>().Should().Be(0x1234);
        reader.Source.Length.Should().Be(data.Length);
        reader.Consumed.Should().Be(2);
        reader.Remaining.Should().Be(data.Length - 2);

        reader.ReadBigEndian<byte>().Should().Be(0x56);
        reader.Source.Length.Should().Be(data.Length);
        reader.Consumed.Should().Be(3);
        reader.Remaining.Should().Be(data.Length - 3);

        reader.ReadBigEndian<uint>().Should().Be(0x789ABCDE);
        reader.Source.Length.Should().Be(data.Length);
        reader.Consumed.Should().Be(7);
        reader.Remaining.Should().Be(data.Length - 7);

        reader.ReadBigEndian<ulong>().Should().Be(0xF011223344556677);
        reader.Source.Length.Should().Be(data.Length);
        reader.Consumed.Should().Be(15);
        reader.Remaining.Should().Be(data.Length - 15);

        var err = default(Exception);
        try { reader.ReadBigEndian<uint>(); } catch (Exception ex) { err = ex; }
        err.Should().NotBeNull();
    }

    [TestMethod]
    public void ReadLength()
    {
        var data = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x9A, 0xBC, 0xDE, 0xF0, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, };
        var reader = new ByteSequenceReader(data);

        reader.ReadLength(3).ToArray().Should().Equal([0x12, 0x34, 0x56]);
        reader.Source.Length.Should().Be(data.Length);
        reader.Consumed.Should().Be(3);
        reader.Remaining.Should().Be(data.Length - 3);

        reader.ReadLength(1).ToArray().Should().Equal([0x78]);
        reader.Source.Length.Should().Be(data.Length);
        reader.Consumed.Should().Be(4);
        reader.Remaining.Should().Be(data.Length - 4);

        reader.ReadLength(10).ToArray().Should().Equal([0x9A, 0xBC, 0xDE, 0xF0, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66]);
        reader.Source.Length.Should().Be(data.Length);
        reader.Consumed.Should().Be(14);
        reader.Remaining.Should().Be(data.Length - 14);

        var err = default(Exception);
        try { reader.ReadLength(20); } catch (Exception ex) { err = ex; }
        err.Should().NotBeNull();
    }
}
