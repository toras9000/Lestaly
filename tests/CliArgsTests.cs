using CommandLine;
using FluentAssertions;
using Moq;

namespace Lestaly.Tests;

[TestClass()]
public class CliArgsTests
{
    private enum Mode
    {
        Default,
        Alpha,
        Beta,
    }

    private class Options
    {
        [Value(0, Required = true)]
        public string? Target { get; set; }

        [Option('t', "text")]
        public string? Text { get; set; }

        [Option('n', "number")]
        public int? Number { get; set; }

        [Option('e', "enum")]
        public Mode Identify { get; set; }

        [Option('x', "extensions", Separator = '/')]
        public IReadOnlyList<string>? Extensions { get; set; }
    }

    [TestMethod()]
    public void Parse_Pos()
    {
        var arguments = new[] { "def", };
        var options = CliArgs.Parse<Options>(arguments);
        options.Target.Should().Be("def");
        options.Text.Should().BeNull();
        options.Number.Should().BeNull();
        options.Identify.Should().Be(Mode.Default);
        options.Extensions.Should().BeEquivalentTo();
    }

    [TestMethod()]
    public void Parse_Text()
    {
        var arguments = new[] { "--text", "asd", "def", };
        var options = CliArgs.Parse<Options>(arguments);
        options.Target.Should().Be("def");
        options.Text.Should().Be("asd");
        options.Number.Should().BeNull();
        options.Identify.Should().Be(Mode.Default);
        options.Extensions.Should().BeEquivalentTo();
    }

    [TestMethod()]
    public void Parse_TextCapital()
    {
        var arguments = new[] { "--TEXT", "asd", "def", };
        var options = CliArgs.Parse<Options>(arguments);
        options.Target.Should().Be("def");
        options.Text.Should().Be("asd");
        options.Number.Should().BeNull();
        options.Identify.Should().Be(Mode.Default);
        options.Extensions.Should().BeEquivalentTo();
    }

    [TestMethod()]
    public void Parse_Number()
    {
        var arguments = new[] { "def", "--number", "123", };
        var options = CliArgs.Parse<Options>(arguments);
        options.Target.Should().Be("def");
        options.Text.Should().BeNull();
        options.Number.Should().Be(123);
        options.Identify.Should().Be(Mode.Default);
        options.Extensions.Should().BeEquivalentTo();
    }

    [TestMethod()]
    public void Parse_Identify()
    {
        var arguments = new[] { "def", "--enum", "alpha", };
        var options = CliArgs.Parse<Options>(arguments);
        options.Target.Should().Be("def");
        options.Text.Should().BeNull();
        options.Number.Should().BeNull();
        options.Identify.Should().Be(Mode.Alpha);
        options.Extensions.Should().BeEquivalentTo();
    }

    [TestMethod()]
    public void Parse_Extensions_Sepa()
    {
        var arguments = new[] { "def", "--extensions", "aaa/bbb/ccc/ddd", };
        var options = CliArgs.Parse<Options>(arguments);
        options.Target.Should().Be("def");
        options.Text.Should().BeNull();
        options.Number.Should().BeNull();
        options.Identify.Should().Be(Mode.Default);
        options.Extensions.Should().BeEquivalentTo("aaa", "bbb", "ccc", "ddd");
    }

    [TestMethod()]
    public void Parse_Error_Unknown()
    {
        new Action(() => CliArgs.Parse<Options>(new[] { "--tekito", "aaa" }))
            .Should().Throw<Exception>();
    }

    [TestMethod()]
    public void Parse_Error_Required()
    {
        new Action(() => CliArgs.Parse<Options>(new[] { "--text", "asd" }))
            .Should().Throw<Exception>();
    }

    [TestMethod()]
    public void Parse_Help()
    {
        new Action(() => CliArgs.Parse<Options>(new[] { "--tekito", "--help" }))
            .Should().Throw<PavedMessageException>().Which.Kind.Should().Be(PavedMessageKind.Information);
    }

}