namespace LestalyTest;

[TestClass]
public class SemanticVersionTests
{
    [TestMethod()]
    public void Parse()
    {
        SemanticVersion.Parse("1234.2345.3456.4567-pre+build").Should().Match<SemanticVersion>(
            v => v.Major == 1234
              && v.Minor == 2345
              && v.Patch == 3456
              && v.Filum == 4567
              && v.PreRelease == "pre"
              && v.Build == "build"
        );

        SemanticVersion.Parse("1234.2345.3456-pre+build").Should().Match<SemanticVersion>(
            v => v.Major == 1234
              && v.Minor == 2345
              && v.Patch == 3456
              && v.Filum == null
              && v.PreRelease == "pre"
              && v.Build == "build"
        );

        SemanticVersion.Parse("1234.2345.3456+build").Should().Match<SemanticVersion>(
            v => v.Major == 1234
              && v.Minor == 2345
              && v.Patch == 3456
              && v.Filum == null
              && v.PreRelease == ""
              && v.Build == "build"
        );

        SemanticVersion.Parse("1234.2345").Should().Match<SemanticVersion>(
            v => v.Major == 1234
              && v.Minor == 2345
              && v.Patch == null
              && v.Filum == null
              && v.PreRelease == ""
              && v.Build == ""
        );

        SemanticVersion.Parse("1234").Should().Match<SemanticVersion>(
            v => v.Major == 1234
              && v.Minor == null
              && v.Patch == null
              && v.Filum == null
              && v.PreRelease == ""
              && v.Build == ""
        );
    }

    [TestMethod()]
    public void TryParse()
    {
        SemanticVersion.TryParse("1234.2345.3456.4567-pre+build", out _).Should().BeTrue();
        SemanticVersion.TryParse("aaaa", out _).Should().BeFalse();
    }


    [TestMethod()]
    public void String()
    {
        var version = SemanticVersion.Parse("001.002.003-pre1+build2");
        version.Original.Should().Be("001.002.003-pre1+build2");
        version.ToString().Should().Be("001.002.003-pre1+build2");
        version.ReformString().Should().Be("1.2.3-pre1+build2");
    }

    [TestMethod()]
    public void Equals()
    {
        var a = SemanticVersion.Parse("1.2.3.4-pre+build");
        var b = SemanticVersion.Parse("1.2.3.4-pre+build");
        var c = SemanticVersion.Parse("001.002.003.004-pre+build");

        a.Equals(b).Should().BeTrue();
        a.Equals(c).Should().BeTrue();
        (a == b).Should().BeTrue();
        (a == c).Should().BeTrue();

        a.Equals(SemanticVersion.Parse("0.2.3.4-pre+build")).Should().BeFalse();
        a.Equals(SemanticVersion.Parse("1.1.3.4-pre+build")).Should().BeFalse();
        a.Equals(SemanticVersion.Parse("1.2.2.4-pre+build")).Should().BeFalse();
        a.Equals(SemanticVersion.Parse("1.2.3.3-pre+build")).Should().BeFalse();
        a.Equals(SemanticVersion.Parse("1.2.3.4-pre2+build")).Should().BeFalse();
        a.Equals(SemanticVersion.Parse("1.2.3.4-pre+build2")).Should().BeFalse();
    }

    [TestMethod()]
    public void CompareTo()
    {
        var a = SemanticVersion.Parse("1.2.3.4-pre+build");

        a.CompareTo(SemanticVersion.Parse("1.2.3.4-pre+build")).Should().Be(0);
        a.CompareTo(SemanticVersion.Parse("001.002.003.004-pre+build")).Should().Be(0);

        a.CompareTo(SemanticVersion.Parse("1.2.3.3-pre+build")).Should().BeGreaterThan(0);
        a.CompareTo(SemanticVersion.Parse("1.2.3.5-pre+build")).Should().BeLessThan(0);
    }

}
