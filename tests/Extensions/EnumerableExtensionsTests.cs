﻿using FluentAssertions;

namespace LestalyTest.Extensions;

[TestClass()]
public class EnumerableExtensionsTests
{
    [TestMethod()]
    public void ErrorIfEmpty()
    {
        new[] { 1, }.ErrorIfEmpty().Should().BeEquivalentTo(new[] { 1, });

        FluentActions.Enumerating(() => Array.Empty<int>().ErrorIfEmpty(() => new ApplicationException("test-ex")))
            .Should().Throw<ApplicationException>().Where(ex => ex.Message == "test-ex");
    }

    [TestMethod()]
    public void Must()
    {
        new[] { 1, 2, 3, }.Must(n => n < 10).Should().BeEquivalentTo(new[] { 1, 2, 3, }, o => o.WithStrictOrdering());

        FluentActions.Enumerating(() => new[] { 1, 2, 10, }.Must(n => n < 10, () => new ApplicationException("test-ex")))
            .Should().Throw<ApplicationException>().Where(ex => ex.Message == "test-ex");
    }

    [TestMethod()]
    public void Deconstruct2()
    {
        (var ve1, var ve2) = Enumerable.Range(100, 100);
        ve1.Should().Be(100);
        ve2.Should().Be(101);

        (var vl1, var vl2) = Enumerable.Range(200, 1);
        vl1.Should().Be(200);
        vl2.Should().Be(0);

        (var re1, var re2) = Enumerable.Range(100, 100).Select(v => v.ToString());
        re1.Should().Be("100");
        re2.Should().Be("101");

        (var rl1, var rl2) = Enumerable.Range(100, 1).Select(v => v.ToString());
        rl1.Should().Be("100");
        rl2.Should().BeNull();
    }

    [TestMethod()]
    public void Deconstruct3()
    {
        (var ve1, var ve2, var ve3) = Enumerable.Range(100, 100);
        ve1.Should().Be(100);
        ve2.Should().Be(101);
        ve3.Should().Be(102);

        (var vl1, var vl2, var vl3) = Enumerable.Range(200, 1);
        vl1.Should().Be(200);
        vl2.Should().Be(0);
        vl3.Should().Be(0);

        (var re1, var re2, var re3) = Enumerable.Range(100, 100).Select(v => v.ToString());
        re1.Should().Be("100");
        re2.Should().Be("101");
        re3.Should().Be("102");

        (var rl1, var rl2, var rl3) = Enumerable.Range(100, 1).Select(v => v.ToString());
        rl1.Should().Be("100");
        rl2.Should().BeNull();
        rl3.Should().BeNull();
    }

    [TestMethod()]
    public void DeconstructM()
    {
        {
            (var v1, var v2, var v3, var v4) = Enumerable.Range(100, 100);
            v1.Should().Be(100);
            v2.Should().Be(101);
            v3.Should().Be(102);
            v4.Should().Be(103);
        }

        {
            (var v1, var v2, var v3, var v4, var v5) = Enumerable.Range(100, 100);
            v1.Should().Be(100);
            v2.Should().Be(101);
            v3.Should().Be(102);
            v4.Should().Be(103);
            v5.Should().Be(104);
        }
    }
}
