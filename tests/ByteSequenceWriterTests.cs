namespace LestalyTest;

[TestClass]
public class ByteSequenceWriterTests
{
    [TestMethod]
    public void WriteLittleEndian()
    {
        var data = new byte[16];
        var reader = new ByteSequenceWriter(data);
        reader.Buffer.Length.Should().Be(data.Length);
        reader.Written.Should().Be(0);
        reader.Remaining.Should().Be(data.Length);

        reader.WriteLittleEndian<ushort>(0x3412);
        reader.Buffer.Length.Should().Be(data.Length);
        reader.Written.Should().Be(2);
        reader.Remaining.Should().Be(data.Length - 2);
        data[0..reader.Written].Should().Equal([0x12, 0x34]);

        reader.WriteLittleEndian<byte>(0x56);
        reader.Buffer.Length.Should().Be(data.Length);
        reader.Written.Should().Be(3);
        reader.Remaining.Should().Be(data.Length - 3);
        data[0..reader.Written].Should().Equal([0x12, 0x34, 0x56]);

        reader.WriteLittleEndian<uint>(0xDEBC9A78);
        reader.Buffer.Length.Should().Be(data.Length);
        reader.Written.Should().Be(7);
        reader.Remaining.Should().Be(data.Length - 7);
        data[0..reader.Written].Should().Equal([0x12, 0x34, 0x56, 0x78, 0x9A, 0xBC, 0xDE]);

        reader.WriteLittleEndian<ulong>(0x77665544332211F0);
        reader.Buffer.Length.Should().Be(data.Length);
        reader.Written.Should().Be(15);
        reader.Remaining.Should().Be(data.Length - 15);
        data[0..reader.Written].Should().Equal([0x12, 0x34, 0x56, 0x78, 0x9A, 0xBC, 0xDE, 0xF0, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77]);

        var err = default(Exception);
        try { reader.WriteLittleEndian<uint>(0); } catch (Exception ex) { err = ex; }
        err.Should().NotBeNull();
    }

    [TestMethod]
    public void WriteBigEndian()
    {
        var data = new byte[16];
        var reader = new ByteSequenceWriter(data);
        reader.Buffer.Length.Should().Be(data.Length);
        reader.Written.Should().Be(0);
        reader.Remaining.Should().Be(data.Length);

        reader.WriteBigEndian<ushort>(0x3412);
        reader.Buffer.Length.Should().Be(data.Length);
        reader.Written.Should().Be(2);
        reader.Remaining.Should().Be(data.Length - 2);
        data[0..reader.Written].Should().Equal([0x34, 0x12]);

        reader.WriteBigEndian<byte>(0x56);
        reader.Buffer.Length.Should().Be(data.Length);
        reader.Written.Should().Be(3);
        reader.Remaining.Should().Be(data.Length - 3);
        data[0..reader.Written].Should().Equal([0x34, 0x12, 0x56]);

        reader.WriteBigEndian<uint>(0xDEBC9A78);
        reader.Buffer.Length.Should().Be(data.Length);
        reader.Written.Should().Be(7);
        reader.Remaining.Should().Be(data.Length - 7);
        data[0..reader.Written].Should().Equal([0x34, 0x12, 0x56, 0xDE, 0xBC, 0x9A, 0x78]);

        reader.WriteBigEndian<ulong>(0x77665544332211F0);
        reader.Buffer.Length.Should().Be(data.Length);
        reader.Written.Should().Be(15);
        reader.Remaining.Should().Be(data.Length - 15);
        data[0..reader.Written].Should().Equal([0x34, 0x12, 0x56, 0xDE, 0xBC, 0x9A, 0x78, 0x77, 0x66, 0x55, 0x44, 0x33, 0x22, 0x11, 0xF0]);

        var err = default(Exception);
        try { reader.WriteBigEndian<uint>(0); } catch (Exception ex) { err = ex; }
        err.Should().NotBeNull();
    }

    [TestMethod]
    public void WriteData()
    {
        var data = new byte[16];
        var reader = new ByteSequenceWriter(data);

        reader.WriteData([0x12, 0x34, 0x56, 0x78]);
        reader.Buffer.Length.Should().Be(data.Length);
        reader.Written.Should().Be(4);
        reader.Remaining.Should().Be(data.Length - 4);
        data[0..reader.Written].Should().Equal([0x12, 0x34, 0x56, 0x78]);

        reader.WriteData([]);
        reader.Buffer.Length.Should().Be(data.Length);
        reader.Written.Should().Be(4);
        reader.Remaining.Should().Be(data.Length - 4);
        data[0..reader.Written].Should().Equal([0x12, 0x34, 0x56, 0x78]);

        reader.WriteData([0x9A, 0xBC, 0xDE, 0xF0, 0x11, 0x22]);
        reader.Buffer.Length.Should().Be(data.Length);
        reader.Written.Should().Be(10);
        reader.Remaining.Should().Be(data.Length - 10);
        data[0..reader.Written].Should().Equal([0x12, 0x34, 0x56, 0x78, 0x9A, 0xBC, 0xDE, 0xF0, 0x11, 0x22]);

        reader.WriteData([0x33]);
        reader.Buffer.Length.Should().Be(data.Length);
        reader.Written.Should().Be(11);
        reader.Remaining.Should().Be(data.Length - 11);
        data[0..reader.Written].Should().Equal([0x12, 0x34, 0x56, 0x78, 0x9A, 0xBC, 0xDE, 0xF0, 0x11, 0x22, 0x33]);

        var err = default(Exception);
        try { reader.WriteData([0x44, 0x55, 0x66, 0x77, 0x88, 0x99]); } catch (Exception ex) { err = ex; }
        err.Should().NotBeNull();
    }

}
