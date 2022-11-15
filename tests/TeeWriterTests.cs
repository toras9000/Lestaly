using System.Buffers.Text;
using System.Reactive.Disposables;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using FluentAssertions;

namespace Lestaly.Tests;

[TestClass()]
public class TeeWriterTests
{
    private StreamWriter createWriter(ICollection<MemoryStream> backends)
    {
        var memory = new MemoryStream();
        backends!.Add(memory);
        return new StreamWriter(memory);
    }

    private string readMemory(MemoryStream memory, Encoding? encoding = null)
    {
        return (encoding ?? Encoding.UTF8).GetString(memory.ToArray());
    }

    [TestMethod()]
    public void Bind()
    {
        var resources = new List<MemoryStream>();
        StreamWriter createWriter() => this.createWriter(resources);

        using var writer = new TeeWriter();

        writer.Bind(createWriter());
        writer.Bind(createWriter(), createWriter(), createWriter());

        writer.Dispose();

        resources.Should().AllSatisfy(w => w.CanWrite.Should().BeFalse());
    }

    [TestMethod()]
    public void With()
    {
        var resources = new List<MemoryStream>();
        StreamWriter createWriter() => this.createWriter(resources);

        using var writer = new TeeWriter();

        writer.With(createWriter());
        writer.With(createWriter(), createWriter(), createWriter());

        writer.Dispose();

        resources.Should().AllSatisfy(w => w.CanWrite.Should().BeTrue());
    }

    [TestMethod()]
    public void Write()
    {
        var resources = new List<MemoryStream>();
        StreamWriter createWriter() => this.createWriter(resources);

        using var writer = new TeeWriter();

        writer.Bind(createWriter());
        writer.With(createWriter());

        writer.Write("abc");
        writer.Write("def".AsSpan());
        writer.Write(new StringBuilder("ghi"));
        writer.Write(999);

        writer.Flush();

        resources.Should().AllSatisfy(m => readMemory(m).Should().Be("abcdefghi999"));
    }

    [TestMethod()]
    public async Task WriteAsync()
    {
        var resources = new List<MemoryStream>();
        StreamWriter createWriter() => this.createWriter(resources);

        using var writer = new TeeWriter();

        writer.Bind(createWriter());
        writer.With(createWriter());

        await writer.WriteAsync("abc");
        await writer.WriteAsync("def".AsMemory());
        await writer.WriteAsync(new StringBuilder("ghi"));

        await writer.FlushAsync();

        resources.Should().AllSatisfy(m => readMemory(m).Should().Be("abcdefghi"));
    }

    [TestMethod()]
    public void WriteLine()
    {
        var resources = new List<MemoryStream>();
        StreamWriter createWriter() => this.createWriter(resources);

        using var writer = new TeeWriter();

        writer.Bind(createWriter());
        writer.With(createWriter());

        writer.WriteLine("abc");
        writer.WriteLine("def".AsSpan());
        writer.WriteLine();
        writer.WriteLine(new StringBuilder("ghi"));
        writer.WriteLine(999);

        writer.Flush();

        var expect = new[] { "abc", "def", "", "ghi", "999", }.Select(t => t + Environment.NewLine).JoinString();
        resources.Should().AllSatisfy(m => readMemory(m).Should().Be(expect));
    }

    [TestMethod()]
    public async Task WriteLineAsync()
    {
        var resources = new List<MemoryStream>();
        StreamWriter createWriter() => this.createWriter(resources);

        using var writer = new TeeWriter();

        writer.Bind(createWriter());
        writer.With(createWriter());

        await writer.WriteLineAsync("abc");
        await writer.WriteLineAsync("def".AsMemory());
        await writer.WriteLineAsync();
        await writer.WriteLineAsync(new StringBuilder("ghi"));

        await writer.FlushAsync();

        var expect = new[] { "abc", "def", "", "ghi", }.Select(t => t + Environment.NewLine).JoinString();
        resources.Should().AllSatisfy(m => readMemory(m).Should().Be(expect));
    }
}
