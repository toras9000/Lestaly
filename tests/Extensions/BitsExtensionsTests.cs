namespace LestalyTest;

[TestClass]
public class BitsExtensionsTests
{
    [TestMethod]
    public void GetBits_byte()
    {
        void testGetBits(byte value, int pos, int len, byte expect)
        {
            var getResult = value.GetBits(pos, len);
            getResult.Should().Be(expect);
        }

        testGetBits(0b_0001_1011, pos: 0, len: 0, expect: 0b_0000_0000);
        testGetBits(0b_0001_1011, pos: 0, len: 1, expect: 0b_0000_0001);
        testGetBits(0b_0001_1011, pos: 0, len: 2, expect: 0b_0000_0011);
        testGetBits(0b_0001_1011, pos: 0, len: 3, expect: 0b_0000_0011);
        testGetBits(0b_0001_1011, pos: 0, len: 4, expect: 0b_0000_1011);
        testGetBits(0b_0001_1011, pos: 0, len: 5, expect: 0b_0001_1011);
        testGetBits(0b_0001_1011, pos: 0, len: 6, expect: 0b_0001_1011);
        testGetBits(0b_0001_1011, pos: 0, len: 7, expect: 0b_0001_1011);
        testGetBits(0b_0001_1011, pos: 0, len: 8, expect: 0b_0001_1011);
        testGetBits(0b_0001_1011, pos: 0, len: 9, expect: 0b_0001_1011);

        testGetBits(0b_0001_1011, pos: 1, len: 0, expect: 0b_0000_000);
        testGetBits(0b_0001_1011, pos: 1, len: 1, expect: 0b_0000_001);
        testGetBits(0b_0001_1011, pos: 1, len: 2, expect: 0b_0000_001);
        testGetBits(0b_0001_1011, pos: 1, len: 3, expect: 0b_0000_101);
        testGetBits(0b_0001_1011, pos: 1, len: 4, expect: 0b_0001_101);
        testGetBits(0b_0001_1011, pos: 1, len: 5, expect: 0b_0001_101);
        testGetBits(0b_0001_1011, pos: 1, len: 6, expect: 0b_0001_101);
        testGetBits(0b_0001_1011, pos: 1, len: 7, expect: 0b_0001_101);
        testGetBits(0b_0001_1011, pos: 1, len: 8, expect: 0b_0001_101);
        testGetBits(0b_0001_1011, pos: 1, len: 9, expect: 0b_0001_101);

        testGetBits(0b_0001_1011, pos: 6, len: 0, expect: 0b_00);
        testGetBits(0b_0001_1011, pos: 6, len: 1, expect: 0b_00);
        testGetBits(0b_0001_1011, pos: 6, len: 2, expect: 0b_00);
        testGetBits(0b_0001_1011, pos: 6, len: 3, expect: 0b_00);
        testGetBits(0b_0001_1011, pos: 6, len: 4, expect: 0b_00);
        testGetBits(0b_0001_1011, pos: 6, len: 5, expect: 0b_00);
        testGetBits(0b_0001_1011, pos: 6, len: 6, expect: 0b_00);
        testGetBits(0b_0001_1011, pos: 6, len: 7, expect: 0b_00);
        testGetBits(0b_0001_1011, pos: 6, len: 8, expect: 0b_00);
        testGetBits(0b_0001_1011, pos: 6, len: 9, expect: 0b_00);

        testGetBits(0b_0001_1011, pos: 7, len: 0, expect: 0b_0);
        testGetBits(0b_0001_1011, pos: 7, len: 1, expect: 0b_0);
        testGetBits(0b_0001_1011, pos: 7, len: 2, expect: 0b_0);
        testGetBits(0b_0001_1011, pos: 7, len: 3, expect: 0b_0);
        testGetBits(0b_0001_1011, pos: 7, len: 4, expect: 0b_0);
        testGetBits(0b_0001_1011, pos: 7, len: 5, expect: 0b_0);
        testGetBits(0b_0001_1011, pos: 7, len: 6, expect: 0b_0);
        testGetBits(0b_0001_1011, pos: 7, len: 7, expect: 0b_0);
        testGetBits(0b_0001_1011, pos: 7, len: 8, expect: 0b_0);
        testGetBits(0b_0001_1011, pos: 7, len: 9, expect: 0b_0);


        testGetBits(0b_1011_0001, pos: 0, len: 0, expect: 0b_0000_0000);
        testGetBits(0b_1011_0001, pos: 0, len: 1, expect: 0b_0000_0001);
        testGetBits(0b_1011_0001, pos: 0, len: 2, expect: 0b_0000_0001);
        testGetBits(0b_1011_0001, pos: 0, len: 3, expect: 0b_0000_0001);
        testGetBits(0b_1011_0001, pos: 0, len: 4, expect: 0b_0000_0001);
        testGetBits(0b_1011_0001, pos: 0, len: 5, expect: 0b_0001_0001);
        testGetBits(0b_1011_0001, pos: 0, len: 6, expect: 0b_0011_0001);
        testGetBits(0b_1011_0001, pos: 0, len: 7, expect: 0b_0011_0001);
        testGetBits(0b_1011_0001, pos: 0, len: 8, expect: 0b_1011_0001);
        testGetBits(0b_1011_0001, pos: 0, len: 9, expect: 0b_1011_0001);

        testGetBits(0b_1011_0001, pos: 1, len: 0, expect: 0b_0000_000);
        testGetBits(0b_1011_0001, pos: 1, len: 1, expect: 0b_0000_000);
        testGetBits(0b_1011_0001, pos: 1, len: 2, expect: 0b_0000_000);
        testGetBits(0b_1011_0001, pos: 1, len: 3, expect: 0b_0000_000);
        testGetBits(0b_1011_0001, pos: 1, len: 4, expect: 0b_0001_000);
        testGetBits(0b_1011_0001, pos: 1, len: 5, expect: 0b_0011_000);
        testGetBits(0b_1011_0001, pos: 1, len: 6, expect: 0b_0011_000);
        testGetBits(0b_1011_0001, pos: 1, len: 7, expect: 0b_1011_000);
        testGetBits(0b_1011_0001, pos: 1, len: 8, expect: 0b_1011_000);
        testGetBits(0b_1011_0001, pos: 1, len: 9, expect: 0b_1011_000);

        testGetBits(0b_1011_0001, pos: 6, len: 0, expect: 0b_00);
        testGetBits(0b_1011_0001, pos: 6, len: 1, expect: 0b_00);
        testGetBits(0b_1011_0001, pos: 6, len: 2, expect: 0b_10);
        testGetBits(0b_1011_0001, pos: 6, len: 3, expect: 0b_10);
        testGetBits(0b_1011_0001, pos: 6, len: 4, expect: 0b_10);
        testGetBits(0b_1011_0001, pos: 6, len: 5, expect: 0b_10);
        testGetBits(0b_1011_0001, pos: 6, len: 6, expect: 0b_10);
        testGetBits(0b_1011_0001, pos: 6, len: 7, expect: 0b_10);
        testGetBits(0b_1011_0001, pos: 6, len: 8, expect: 0b_10);
        testGetBits(0b_1011_0001, pos: 6, len: 9, expect: 0b_10);

        testGetBits(0b_1011_0001, pos: 7, len: 0, expect: 0b_0);
        testGetBits(0b_1011_0001, pos: 7, len: 1, expect: 0b_1);
        testGetBits(0b_1011_0001, pos: 7, len: 2, expect: 0b_1);
        testGetBits(0b_1011_0001, pos: 7, len: 3, expect: 0b_1);
        testGetBits(0b_1011_0001, pos: 7, len: 4, expect: 0b_1);
        testGetBits(0b_1011_0001, pos: 7, len: 5, expect: 0b_1);
        testGetBits(0b_1011_0001, pos: 7, len: 6, expect: 0b_1);
        testGetBits(0b_1011_0001, pos: 7, len: 7, expect: 0b_1);
        testGetBits(0b_1011_0001, pos: 7, len: 8, expect: 0b_1);
        testGetBits(0b_1011_0001, pos: 7, len: 9, expect: 0b_1);
    }

    [TestMethod]
    public void SetModBits_byte()
    {
        void testSetModBits(byte value, int pos, int len, byte write, byte expect)
        {
            var setSrc = value;
            var setResult = setSrc.SetBits(pos, len, write);
            setResult.Should().Be(expect);
            setSrc.Should().Be(expect);

            var modSrc = value;
            var modResult = setSrc.ModBits(pos, len, write);
            modResult.Should().Be(expect);
            modSrc.Should().Be(value);
        }

        testSetModBits(0b_1111_1111, pos: 0, len: 0, write: 0, expect: 0b_1111_1111);
        testSetModBits(0b_1111_1111, pos: 0, len: 1, write: 0, expect: 0b_1111_1110);
        testSetModBits(0b_1111_1111, pos: 0, len: 2, write: 0, expect: 0b_1111_1100);
        testSetModBits(0b_1111_1111, pos: 0, len: 3, write: 0, expect: 0b_1111_1000);
        testSetModBits(0b_1111_1111, pos: 0, len: 4, write: 0, expect: 0b_1111_0000);
        testSetModBits(0b_1111_1111, pos: 0, len: 5, write: 0, expect: 0b_1110_0000);
        testSetModBits(0b_1111_1111, pos: 0, len: 6, write: 0, expect: 0b_1100_0000);
        testSetModBits(0b_1111_1111, pos: 0, len: 7, write: 0, expect: 0b_1000_0000);
        testSetModBits(0b_1111_1111, pos: 0, len: 8, write: 0, expect: 0b_0000_0000);
        testSetModBits(0b_1111_1111, pos: 0, len: 9, write: 0, expect: 0b_0000_0000);

        testSetModBits(0b_1111_1111, pos: 0, len: 0, write: 0xFF, expect: 0b_1111_1111);
        testSetModBits(0b_1111_1111, pos: 0, len: 1, write: 0xFF, expect: 0b_1111_1111);
        testSetModBits(0b_1111_1111, pos: 0, len: 2, write: 0xFF, expect: 0b_1111_1111);
        testSetModBits(0b_1111_1111, pos: 0, len: 3, write: 0xFF, expect: 0b_1111_1111);
        testSetModBits(0b_1111_1111, pos: 0, len: 4, write: 0xFF, expect: 0b_1111_1111);
        testSetModBits(0b_1111_1111, pos: 0, len: 5, write: 0xFF, expect: 0b_1111_1111);
        testSetModBits(0b_1111_1111, pos: 0, len: 6, write: 0xFF, expect: 0b_1111_1111);
        testSetModBits(0b_1111_1111, pos: 0, len: 7, write: 0xFF, expect: 0b_1111_1111);
        testSetModBits(0b_1111_1111, pos: 0, len: 8, write: 0xFF, expect: 0b_1111_1111);
        testSetModBits(0b_1111_1111, pos: 0, len: 9, write: 0xFF, expect: 0b_1111_1111);

        testSetModBits(0b_1111_1111, pos: 0, len: 0, write: 0b_1010_1010, expect: 0b_1111_1111);
        testSetModBits(0b_1111_1111, pos: 0, len: 1, write: 0b_1010_1010, expect: 0b_1111_1110);
        testSetModBits(0b_1111_1111, pos: 0, len: 2, write: 0b_1010_1010, expect: 0b_1111_1110);
        testSetModBits(0b_1111_1111, pos: 0, len: 3, write: 0b_1010_1010, expect: 0b_1111_1010);
        testSetModBits(0b_1111_1111, pos: 0, len: 4, write: 0b_1010_1010, expect: 0b_1111_1010);
        testSetModBits(0b_1111_1111, pos: 0, len: 5, write: 0b_1010_1010, expect: 0b_1110_1010);
        testSetModBits(0b_1111_1111, pos: 0, len: 6, write: 0b_1010_1010, expect: 0b_1110_1010);
        testSetModBits(0b_1111_1111, pos: 0, len: 7, write: 0b_1010_1010, expect: 0b_1010_1010);
        testSetModBits(0b_1111_1111, pos: 0, len: 8, write: 0b_1010_1010, expect: 0b_1010_1010);
        testSetModBits(0b_1111_1111, pos: 0, len: 9, write: 0b_1010_1010, expect: 0b_1010_1010);

        testSetModBits(0b_1111_1111, pos: 3, len: 0, write: 0, expect: 0b_1111_1111);
        testSetModBits(0b_1111_1111, pos: 3, len: 1, write: 0, expect: 0b_1111_0111);
        testSetModBits(0b_1111_1111, pos: 3, len: 2, write: 0, expect: 0b_1110_0111);
        testSetModBits(0b_1111_1111, pos: 3, len: 3, write: 0, expect: 0b_1100_0111);
        testSetModBits(0b_1111_1111, pos: 3, len: 4, write: 0, expect: 0b_1000_0111);
        testSetModBits(0b_1111_1111, pos: 3, len: 5, write: 0, expect: 0b_0000_0111);
        testSetModBits(0b_1111_1111, pos: 3, len: 6, write: 0, expect: 0b_0000_0111);
        testSetModBits(0b_1111_1111, pos: 3, len: 7, write: 0, expect: 0b_0000_0111);
        testSetModBits(0b_1111_1111, pos: 3, len: 8, write: 0, expect: 0b_0000_0111);
        testSetModBits(0b_1111_1111, pos: 3, len: 9, write: 0, expect: 0b_0000_0111);

        testSetModBits(0b_1111_1111, pos: 7, len: 0, write: 0, expect: 0b_1111_1111);
        testSetModBits(0b_1111_1111, pos: 7, len: 1, write: 0, expect: 0b_0111_1111);
        testSetModBits(0b_1111_1111, pos: 7, len: 2, write: 0, expect: 0b_0111_1111);
        testSetModBits(0b_1111_1111, pos: 7, len: 3, write: 0, expect: 0b_0111_1111);
        testSetModBits(0b_1111_1111, pos: 7, len: 4, write: 0, expect: 0b_0111_1111);
        testSetModBits(0b_1111_1111, pos: 7, len: 5, write: 0, expect: 0b_0111_1111);
        testSetModBits(0b_1111_1111, pos: 7, len: 6, write: 0, expect: 0b_0111_1111);
        testSetModBits(0b_1111_1111, pos: 7, len: 7, write: 0, expect: 0b_0111_1111);
        testSetModBits(0b_1111_1111, pos: 7, len: 8, write: 0, expect: 0b_0111_1111);
        testSetModBits(0b_1111_1111, pos: 7, len: 9, write: 0, expect: 0b_0111_1111);


        testSetModBits(0b_0000_0000, pos: 0, len: 0, write: 0xFF, expect: 0b_0000_0000);
        testSetModBits(0b_0000_0000, pos: 0, len: 1, write: 0xFF, expect: 0b_0000_0001);
        testSetModBits(0b_0000_0000, pos: 0, len: 2, write: 0xFF, expect: 0b_0000_0011);
        testSetModBits(0b_0000_0000, pos: 0, len: 3, write: 0xFF, expect: 0b_0000_0111);
        testSetModBits(0b_0000_0000, pos: 0, len: 4, write: 0xFF, expect: 0b_0000_1111);
        testSetModBits(0b_0000_0000, pos: 0, len: 5, write: 0xFF, expect: 0b_0001_1111);
        testSetModBits(0b_0000_0000, pos: 0, len: 6, write: 0xFF, expect: 0b_0011_1111);
        testSetModBits(0b_0000_0000, pos: 0, len: 7, write: 0xFF, expect: 0b_0111_1111);
        testSetModBits(0b_0000_0000, pos: 0, len: 8, write: 0xFF, expect: 0b_1111_1111);
        testSetModBits(0b_0000_0000, pos: 0, len: 9, write: 0xFF, expect: 0b_1111_1111);

        testSetModBits(0b_0000_0000, pos: 0, len: 0, write: 0b_1010_1010, expect: 0b_0000_0000);
        testSetModBits(0b_0000_0000, pos: 0, len: 1, write: 0b_1010_1010, expect: 0b_0000_0000);
        testSetModBits(0b_0000_0000, pos: 0, len: 2, write: 0b_1010_1010, expect: 0b_0000_0010);
        testSetModBits(0b_0000_0000, pos: 0, len: 3, write: 0b_1010_1010, expect: 0b_0000_0010);
        testSetModBits(0b_0000_0000, pos: 0, len: 4, write: 0b_1010_1010, expect: 0b_0000_1010);
        testSetModBits(0b_0000_0000, pos: 0, len: 5, write: 0b_1010_1010, expect: 0b_0000_1010);
        testSetModBits(0b_0000_0000, pos: 0, len: 6, write: 0b_1010_1010, expect: 0b_0010_1010);
        testSetModBits(0b_0000_0000, pos: 0, len: 7, write: 0b_1010_1010, expect: 0b_0010_1010);
        testSetModBits(0b_0000_0000, pos: 0, len: 8, write: 0b_1010_1010, expect: 0b_1010_1010);
        testSetModBits(0b_0000_0000, pos: 0, len: 9, write: 0b_1010_1010, expect: 0b_1010_1010);

        testSetModBits(0b_0000_0000, pos: 3, len: 0, write: 0xFF, expect: 0b_0000_0000);
        testSetModBits(0b_0000_0000, pos: 3, len: 1, write: 0xFF, expect: 0b_0000_1000);
        testSetModBits(0b_0000_0000, pos: 3, len: 2, write: 0xFF, expect: 0b_0001_1000);
        testSetModBits(0b_0000_0000, pos: 3, len: 3, write: 0xFF, expect: 0b_0011_1000);
        testSetModBits(0b_0000_0000, pos: 3, len: 4, write: 0xFF, expect: 0b_0111_1000);
        testSetModBits(0b_0000_0000, pos: 3, len: 5, write: 0xFF, expect: 0b_1111_1000);
        testSetModBits(0b_0000_0000, pos: 3, len: 6, write: 0xFF, expect: 0b_1111_1000);
        testSetModBits(0b_0000_0000, pos: 3, len: 7, write: 0xFF, expect: 0b_1111_1000);
        testSetModBits(0b_0000_0000, pos: 3, len: 8, write: 0xFF, expect: 0b_1111_1000);
        testSetModBits(0b_0000_0000, pos: 3, len: 9, write: 0xFF, expect: 0b_1111_1000);

        testSetModBits(0b_0000_0000, pos: 7, len: 0, write: 0xFF, expect: 0b_0000_0000);
        testSetModBits(0b_0000_0000, pos: 7, len: 1, write: 0xFF, expect: 0b_1000_0000);
        testSetModBits(0b_0000_0000, pos: 7, len: 2, write: 0xFF, expect: 0b_1000_0000);
        testSetModBits(0b_0000_0000, pos: 7, len: 3, write: 0xFF, expect: 0b_1000_0000);
        testSetModBits(0b_0000_0000, pos: 7, len: 4, write: 0xFF, expect: 0b_1000_0000);
        testSetModBits(0b_0000_0000, pos: 7, len: 5, write: 0xFF, expect: 0b_1000_0000);
        testSetModBits(0b_0000_0000, pos: 7, len: 6, write: 0xFF, expect: 0b_1000_0000);
        testSetModBits(0b_0000_0000, pos: 7, len: 7, write: 0xFF, expect: 0b_1000_0000);
        testSetModBits(0b_0000_0000, pos: 7, len: 8, write: 0xFF, expect: 0b_1000_0000);
        testSetModBits(0b_0000_0000, pos: 7, len: 9, write: 0xFF, expect: 0b_1000_0000);
    }

    [TestMethod]
    public void GetBits_ushort()
    {
        void testGetBits(ushort value, int pos, int len, ushort expect)
        {
            var getResult = value.GetBits(pos, len);
            getResult.Should().Be(expect);
        }

        testGetBits(0b_1110_0100_0001_1011, pos: 0, len: 0, expect: 0b_0000_0000_0000_0000);
        testGetBits(0b_1110_0100_0001_1011, pos: 0, len: 1, expect: 0b_0000_0000_0000_0001);
        testGetBits(0b_1110_0100_0001_1011, pos: 0, len: 15, expect: 0b_0110_0100_0001_1011);
        testGetBits(0b_1110_0100_0001_1011, pos: 0, len: 16, expect: 0b_1110_0100_0001_1011);
        testGetBits(0b_1110_0100_0001_1011, pos: 0, len: 17, expect: 0b_1110_0100_0001_1011);

        testGetBits(0b_1110_0100_0001_1011, pos: 14, len: 0, expect: 0b_00);
        testGetBits(0b_1110_0100_0001_1011, pos: 14, len: 1, expect: 0b_01);
        testGetBits(0b_1110_0100_0001_1011, pos: 14, len: 15, expect: 0b_11);
        testGetBits(0b_1110_0100_0001_1011, pos: 14, len: 16, expect: 0b_11);
        testGetBits(0b_1110_0100_0001_1011, pos: 14, len: 17, expect: 0b_11);
    }

    [TestMethod]
    public void SetModBits_ushort()
    {
        void testSetModBits(ushort value, int pos, int len, ushort write, ushort expect)
        {
            var setSrc = value;
            var setResult = setSrc.SetBits(pos, len, write);
            setResult.Should().Be(expect);
            setSrc.Should().Be(expect);

            var modSrc = value;
            var modResult = setSrc.ModBits(pos, len, write);
            modResult.Should().Be(expect);
            modSrc.Should().Be(value);
        }

        testSetModBits(0b_1111_1111_1111_1111, pos: 0, len: 0, write: 0, expect: 0b_1111_1111_1111_1111);
        testSetModBits(0b_1111_1111_1111_1111, pos: 0, len: 1, write: 0, expect: 0b_1111_1111_1111_1110);
        testSetModBits(0b_1111_1111_1111_1111, pos: 0, len: 15, write: 0, expect: 0b_1000_0000_0000_0000);
        testSetModBits(0b_1111_1111_1111_1111, pos: 0, len: 16, write: 0, expect: 0b_0000_0000_0000_0000);
        testSetModBits(0b_1111_1111_1111_1111, pos: 0, len: 17, write: 0, expect: 0b_0000_0000_0000_0000);

        testSetModBits(0b_1111_1111_1111_1111, pos: 14, len: 0, write: 0, expect: 0b_1111_1111_1111_1111);
        testSetModBits(0b_1111_1111_1111_1111, pos: 14, len: 1, write: 0, expect: 0b_1011_1111_1111_1111);
        testSetModBits(0b_1111_1111_1111_1111, pos: 14, len: 15, write: 0, expect: 0b_0011_1111_1111_1111);
        testSetModBits(0b_1111_1111_1111_1111, pos: 14, len: 16, write: 0, expect: 0b_0011_1111_1111_1111);
        testSetModBits(0b_1111_1111_1111_1111, pos: 14, len: 17, write: 0, expect: 0b_0011_1111_1111_1111);

        testSetModBits(0b_0000_0000_0000_0000, pos: 0, len: 0, write: 0xFFFF, expect: 0b_0000_0000_0000_0000);
        testSetModBits(0b_0000_0000_0000_0000, pos: 0, len: 1, write: 0xFFFF, expect: 0b_0000_0000_0000_0001);
        testSetModBits(0b_0000_0000_0000_0000, pos: 0, len: 15, write: 0xFFFF, expect: 0b_0111_1111_1111_1111);
        testSetModBits(0b_0000_0000_0000_0000, pos: 0, len: 16, write: 0xFFFF, expect: 0b_1111_1111_1111_1111);
        testSetModBits(0b_0000_0000_0000_0000, pos: 0, len: 17, write: 0xFFFF, expect: 0b_1111_1111_1111_1111);

        testSetModBits(0b_0000_0000_0000_0000, pos: 14, len: 0, write: 0xFFFF, expect: 0b_0000_0000_0000_0000);
        testSetModBits(0b_0000_0000_0000_0000, pos: 14, len: 1, write: 0xFFFF, expect: 0b_0100_0000_0000_0000);
        testSetModBits(0b_0000_0000_0000_0000, pos: 14, len: 15, write: 0xFFFF, expect: 0b_1100_0000_0000_0000);
        testSetModBits(0b_0000_0000_0000_0000, pos: 14, len: 16, write: 0xFFFF, expect: 0b_1100_0000_0000_0000);
        testSetModBits(0b_0000_0000_0000_0000, pos: 14, len: 17, write: 0xFFFF, expect: 0b_1100_0000_0000_0000);
    }

}
