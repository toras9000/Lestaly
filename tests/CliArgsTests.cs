using CommandLine;
using FluentAssertions;
using Moq;

namespace Lestaly.Tests;

[TestClass()]
public class CliArgsTests
{
    private class Options
    {
        [Value(0)]
        public string? Target { get; set; }

        [Option('p', "parameter")]
        public string? Param { get; set; }

    }

    [TestMethod()]
    public void Parse()
    {
        {
            var arguments = new[] { "def", };
            var options = CliArgs.Parse<Options>(arguments);
            options.Target.Should().Be("def");
            options.Param.Should().BeNull();
        }

        {
            var arguments = new[] { "--parameter", "asd", "def" };
            var options = CliArgs.Parse<Options>(arguments);
            options.Target.Should().Be("def");
            options.Param.Should().Be("asd");
        }

        {
            var arguments = new[] { "--parameter", "asd", };
            var options = CliArgs.Parse<Options>(arguments);
            options.Target.Should().BeNull();
            options.Param.Should().Be("asd");
        }

    }

    [TestMethod()]
    public void Parse_Error()
    {
        {
            var arguments = new[] { "--tekito", "aaa" };
            new Action(() => CliArgs.Parse<Options>(arguments))
                .Should().Throw<Exception>();
        }
    }
}