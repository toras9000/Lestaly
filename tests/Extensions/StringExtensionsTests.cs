using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace LestalyTest.Extensions;

[TestClass()]
public class StringExtensionsTests
{
    [TestMethod()]
    public void IsEmpty()
    {
        "".IsEmpty().Should().BeTrue();
        default(string).IsEmpty().Should().BeTrue();
        " ".IsEmpty().Should().BeFalse();
        "a".IsEmpty().Should().BeFalse();
    }

    [TestMethod()]
    public void IsNotEmpty()
    {
        "".IsNotEmpty().Should().BeFalse();
        default(string).IsNotEmpty().Should().BeFalse();
        " ".IsNotEmpty().Should().BeTrue();
        "a".IsNotEmpty().Should().BeTrue();
    }

    [TestMethod()]
    public void IsWhite()
    {
        "".IsWhite().Should().BeTrue();
        default(string).IsWhite().Should().BeTrue();
        " ".IsWhite().Should().BeTrue();
        "a".IsWhite().Should().BeFalse();
    }

    [TestMethod()]
    public void IsNotWhite()
    {
        "".IsNotWhite().Should().BeFalse();
        default(string).IsNotWhite().Should().BeFalse();
        " ".IsNotWhite().Should().BeFalse();
        "a".IsNotWhite().Should().BeTrue();
    }

    [TestMethod()]
    public void WhenEmpty()
    {
        "".WhenEmpty("x").Should().Be("x");
        default(string).WhenEmpty("x").Should().Be("x");
        " ".WhenEmpty("x").Should().Be(" ");
        "a".WhenEmpty("x").Should().Be("a");

        "".WhenEmpty(() => "x").Should().Be("x");
        default(string).WhenEmpty(() => "x").Should().Be("x");
        " ".WhenEmpty(() => "x").Should().Be(" ");
        "a".WhenEmpty(() => "x").Should().Be("a");
    }

    [TestMethod()]
    public void WhenWhite()
    {
        "".WhenWhite("x").Should().Be("x");
        default(string).WhenWhite("x").Should().Be("x");
        " ".WhenWhite("x").Should().Be("x");
        "a".WhenWhite("x").Should().Be("a");

        "".WhenWhite(() => "x").Should().Be("x");
        default(string).WhenWhite(() => "x").Should().Be("x");
        " ".WhenWhite(() => "x").Should().Be("x");
        "a".WhenWhite(() => "x").Should().Be("a");
    }
}
