namespace LestalyTest;

[TestClass()]
public class PathUtilityTests
{
    [TestMethod()]
    public void IsValidFileName()
    {
        PathUtility.IsValidFileName("a").Should().BeTrue();
        PathUtility.IsValidFileName("a.b").Should().BeTrue();
        PathUtility.IsValidFileName(" a ").Should().BeTrue();
        PathUtility.IsValidFileName("a b").Should().BeTrue();

        PathUtility.IsValidFileName(" a/b ").Should().BeFalse();
        PathUtility.IsValidFileName(" a\\b ").Should().BeFalse();
        PathUtility.IsValidFileName(" a\nb ").Should().BeFalse();
        PathUtility.IsValidFileName(" a:b ").Should().BeFalse();
        PathUtility.IsValidFileName("").Should().BeFalse();
        PathUtility.IsValidFileName(" ").Should().BeFalse();
    }

    [TestMethod()]
    public void IsValidRelativePath()
    {
        PathUtility.IsValidRelativePath("a").Should().BeTrue();
        PathUtility.IsValidRelativePath("a.b").Should().BeTrue();
        PathUtility.IsValidRelativePath(" a ").Should().BeTrue();
        PathUtility.IsValidRelativePath("a b").Should().BeTrue();

        PathUtility.IsValidRelativePath(" a/b ").Should().BeTrue();
        PathUtility.IsValidRelativePath(" a\\b ").Should().BeTrue();

        PathUtility.IsValidRelativePath(" a\nb ").Should().BeFalse();
        PathUtility.IsValidRelativePath(" a:b ").Should().BeFalse();
        PathUtility.IsValidRelativePath("").Should().BeFalse();
        PathUtility.IsValidRelativePath(" ").Should().BeFalse();
    }

    [TestMethod()]
    public void EscapeFileName()
    {
        PathUtility.EscapeFileName("a").Should().Be("a");
        PathUtility.EscapeFileName("a.b").Should().Be("a.b");
        PathUtility.EscapeFileName(" a ").Should().Be("a");
        PathUtility.EscapeFileName("a b").Should().Be("a b");

        PathUtility.EscapeFileName(" a/b ").Should().Be("a%2Fb");
        PathUtility.EscapeFileName(" a\\b ").Should().Be("a%5Cb");
        PathUtility.EscapeFileName(" a\nb ").Should().Be("a%0Ab");
        PathUtility.EscapeFileName(" a:b ").Should().Be("a%3Ab");

        new Action(() => PathUtility.EscapeFileName("")).Should().Throw<Exception>();
        new Action(() => PathUtility.EscapeFileName(" ")).Should().Throw<Exception>();
    }

    [TestMethod()]
    public void EscapeRelativePath()
    {
        PathUtility.EscapeRelativePath("a").Should().Be("a");
        PathUtility.EscapeRelativePath("a.b").Should().Be("a.b");
        PathUtility.EscapeRelativePath(" a ").Should().Be("a");
        PathUtility.EscapeRelativePath("a b").Should().Be("a b");

        PathUtility.EscapeRelativePath(" a/b ").Should().Be("a/b");
        PathUtility.EscapeRelativePath(" a\\b ").Should().Be("a\\b");

        PathUtility.EscapeRelativePath(" a\nb ").Should().Be("a%0Ab");
        PathUtility.EscapeRelativePath(" a:b ").Should().Be("a%3Ab");

        new Action(() => PathUtility.EscapeRelativePath("")).Should().Throw<Exception>();
        new Action(() => PathUtility.EscapeRelativePath(" ")).Should().Throw<Exception>();
    }
}
