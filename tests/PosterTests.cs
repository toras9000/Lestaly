namespace LestalyTest;

[TestClass]
public class PosterTests
{
    [TestMethod]
    public void Link()
    {
        Poster.Link["http://localhost"].Should().Be($"\x1b]8;;http://localhost\x1b\\http://localhost\x1b]8;;\x1b\\");
        Poster.Link["http://localhost", "Display"].Should().Be($"\x1b]8;;http://localhost\x1b\\Display\x1b]8;;\x1b\\");
    }

}