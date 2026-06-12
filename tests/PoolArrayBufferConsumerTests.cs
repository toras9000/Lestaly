namespace LestalyTest;

[TestClass]
public class PoolArrayBufferConsumerTests
{
    [TestMethod]
    public void Construct()
    {
        using var writer = new PoolArrayBufferConsumer<byte>(capacity: 32);
        writer.Capacity.Should().BeGreaterThanOrEqualTo(32);
        writer.FreeCapacity.Should().BeGreaterThanOrEqualTo(32);
        writer.WrittenCount.Should().Be(0);
        writer.WrittenMemory.Length.Should().Be(0);
        writer.WrittenSpan.Length.Should().Be(0);
    }

    [TestMethod]
    public void GetMemory()
    {
        using var writer = new PoolArrayBufferConsumer<byte>(capacity: 32);

        writer.GetMemory(10).Length.Should().BeGreaterThanOrEqualTo(10);
        writer.Capacity.Should().BeGreaterThanOrEqualTo(10);
        writer.FreeCapacity.Should().BeGreaterThanOrEqualTo(10);
        writer.WrittenCount.Should().Be(0);
        writer.WrittenMemory.Length.Should().Be(0);
        writer.WrittenSpan.Length.Should().Be(0);

        writer.GetMemory(32).Length.Should().BeGreaterThanOrEqualTo(32);
        writer.Capacity.Should().BeGreaterThanOrEqualTo(32);
        writer.FreeCapacity.Should().BeGreaterThanOrEqualTo(32);
        writer.WrittenCount.Should().Be(0);
        writer.WrittenMemory.Length.Should().Be(0);
        writer.WrittenSpan.Length.Should().Be(0);

        writer.GetMemory(80).Length.Should().BeGreaterThanOrEqualTo(80);
        writer.Capacity.Should().BeGreaterThanOrEqualTo(80);
        writer.FreeCapacity.Should().BeGreaterThanOrEqualTo(80);
        writer.WrittenCount.Should().Be(0);
        writer.WrittenMemory.Length.Should().Be(0);
        writer.WrittenSpan.Length.Should().Be(0);

        var memory = writer.GetMemory();
        writer.GetMemory(memory.Length + 100).Length.Should().BeGreaterThanOrEqualTo(memory.Length + 100);
        writer.Capacity.Should().BeGreaterThanOrEqualTo(memory.Length + 100);
        writer.FreeCapacity.Should().BeGreaterThanOrEqualTo(memory.Length + 100);
        writer.WrittenCount.Should().Be(0);
        writer.WrittenMemory.Length.Should().Be(0);
        writer.WrittenSpan.Length.Should().Be(0);
    }

    [TestMethod]
    public void GetSpan()
    {
        using var writer = new PoolArrayBufferConsumer<byte>(capacity: 32);

        writer.GetSpan(10).Length.Should().BeGreaterThanOrEqualTo(10);
        writer.Capacity.Should().BeGreaterThanOrEqualTo(10);
        writer.FreeCapacity.Should().BeGreaterThanOrEqualTo(10);
        writer.WrittenCount.Should().Be(0);
        writer.WrittenMemory.Length.Should().Be(0);
        writer.WrittenSpan.Length.Should().Be(0);

        writer.GetSpan(32).Length.Should().BeGreaterThanOrEqualTo(32);
        writer.Capacity.Should().BeGreaterThanOrEqualTo(32);
        writer.FreeCapacity.Should().BeGreaterThanOrEqualTo(32);
        writer.WrittenCount.Should().Be(0);
        writer.WrittenMemory.Length.Should().Be(0);
        writer.WrittenSpan.Length.Should().Be(0);

        writer.GetSpan(80).Length.Should().BeGreaterThanOrEqualTo(80);
        writer.Capacity.Should().BeGreaterThanOrEqualTo(80);
        writer.FreeCapacity.Should().BeGreaterThanOrEqualTo(80);
        writer.WrittenCount.Should().Be(0);
        writer.WrittenMemory.Length.Should().Be(0);
        writer.WrittenSpan.Length.Should().Be(0);

        var span = writer.GetMemory();
        writer.GetSpan(span.Length + 100).Length.Should().BeGreaterThanOrEqualTo(span.Length + 100);
        writer.Capacity.Should().BeGreaterThanOrEqualTo(span.Length + 100);
        writer.FreeCapacity.Should().BeGreaterThanOrEqualTo(span.Length + 100);
        writer.WrittenCount.Should().Be(0);
        writer.WrittenMemory.Length.Should().Be(0);
        writer.WrittenSpan.Length.Should().Be(0);
    }

    [TestMethod]
    public void Advance_Memory()
    {
        using var writer = new PoolArrayBufferConsumer<byte>(capacity: 32);

        var memory1 = writer.GetMemory(10);

        var data1 = new byte[] { 12, 23, 34, 45, };
        data1.CopyTo(memory1.Span);
        writer.Advance(data1.Length);
        writer.Capacity.Should().BeGreaterThanOrEqualTo(32);
        writer.FreeCapacity.Should().BeGreaterThanOrEqualTo(32 - data1.Length);
        writer.WrittenCount.Should().Be(data1.Length);
        writer.WrittenMemory.ToArray().Should().Equal(data1);
        writer.WrittenSpan.ToArray().Should().Equal(data1);

        var memory2 = writer.GetMemory(80);
        writer.Capacity.Should().BeGreaterThanOrEqualTo(data1.Length + 80);
        writer.FreeCapacity.Should().BeGreaterThanOrEqualTo(80);
        writer.WrittenCount.Should().Be(data1.Length);
        writer.WrittenMemory.ToArray().Should().Equal(data1);
        writer.WrittenSpan.ToArray().Should().Equal(data1);

        var data2 = new byte[] { 56, 78, 90, 11, 22, 33, };
        data2.CopyTo(memory2.Span);
        writer.Advance(data2.Length);
        writer.Capacity.Should().BeGreaterThanOrEqualTo(data1.Length + 80);
        writer.FreeCapacity.Should().BeGreaterThanOrEqualTo(80 - data2.Length);
        writer.WrittenCount.Should().Be(data1.Length + data2.Length);
        writer.WrittenMemory.ToArray().Should().Equal(data1.Concat(data2));
        writer.WrittenSpan.ToArray().Should().Equal(data1.Concat(data2));
    }

    [TestMethod]
    public void Advance_Span()
    {
        using var writer = new PoolArrayBufferConsumer<byte>(capacity: 32);

        var span1 = writer.GetSpan(10);

        var data1 = new byte[] { 12, 23, 34, 45, };
        data1.CopyTo(span1);
        writer.Advance(data1.Length);
        writer.Capacity.Should().BeGreaterThanOrEqualTo(32);
        writer.FreeCapacity.Should().BeGreaterThanOrEqualTo(32 - data1.Length);
        writer.WrittenCount.Should().Be(data1.Length);
        writer.WrittenMemory.ToArray().Should().Equal(data1);
        writer.WrittenSpan.ToArray().Should().Equal(data1);

        var span2 = writer.GetSpan(80);
        writer.Capacity.Should().BeGreaterThanOrEqualTo(data1.Length + 80);
        writer.FreeCapacity.Should().BeGreaterThanOrEqualTo(80);
        writer.WrittenCount.Should().Be(data1.Length);
        writer.WrittenMemory.ToArray().Should().Equal(data1);
        writer.WrittenSpan.ToArray().Should().Equal(data1);

        var data2 = new byte[] { 56, 78, 90, 11, 22, 33, };
        data2.CopyTo(span2);
        writer.Advance(data2.Length);
        writer.Capacity.Should().BeGreaterThanOrEqualTo(data1.Length + 80);
        writer.FreeCapacity.Should().BeGreaterThanOrEqualTo(80 - data2.Length);
        writer.WrittenCount.Should().Be(data1.Length + data2.Length);
        writer.WrittenMemory.ToArray().Should().Equal(data1.Concat(data2));
        writer.WrittenSpan.ToArray().Should().Equal(data1.Concat(data2));
    }

    [TestMethod]
    public void Advance_Limit()
    {
        using var writer = new PoolArrayBufferConsumer<byte>(capacity: 32);

        var span = writer.GetSpan(32);
        span.Fill(123);

        writer.Advance(span.Length);
        writer.Capacity.Should().BeGreaterThanOrEqualTo(32);
        writer.FreeCapacity.Should().BeGreaterThanOrEqualTo(0);
        writer.WrittenCount.Should().Be(32);
        writer.WrittenMemory.Length.Should().Be(32);
        writer.WrittenMemory.ToArray().Should().OnlyContain(v => v == 123);
        writer.WrittenSpan.Length.Should().Be(32);
        writer.WrittenSpan.ToArray().Should().OnlyContain(v => v == 123);

        FluentActions.Invoking(() => writer.Advance(1)).Should().Throw<Exception>();
    }

    [TestMethod]
    public void Consume()
    {
        using var writer = new PoolArrayBufferConsumer<byte>(capacity: 32);

        var span1 = writer.GetSpan(32);

        span1[..30].Fill(11);
        writer.Advance(30);
        writer.Capacity.Should().BeGreaterThanOrEqualTo(32);
        writer.FreeCapacity.Should().BeGreaterThanOrEqualTo(2);
        writer.WrittenCount.Should().Be(30);
        writer.WrittenMemory.Length.Should().Be(30);
        writer.WrittenSpan.Length.Should().Be(30);

        writer.Consume(25);
        writer.Capacity.Should().BeGreaterThanOrEqualTo(32);
        writer.FreeCapacity.Should().BeGreaterThanOrEqualTo(2);
        writer.WrittenCount.Should().Be(5);
        writer.WrittenMemory.Length.Should().Be(5);
        writer.WrittenSpan.Length.Should().Be(5);

        var span2 = writer.GetSpan(20);
        writer.Capacity.Should().BeGreaterThanOrEqualTo(32);
        writer.FreeCapacity.Should().BeGreaterThanOrEqualTo(32 - 5);
        writer.WrittenCount.Should().Be(5);
        writer.WrittenMemory.Length.Should().Be(5);
        writer.WrittenMemory[..5].ToArray().Should().OnlyContain(v => v == 11);
        writer.WrittenSpan.Length.Should().Be(5);
        writer.WrittenSpan[..5].ToArray().Should().OnlyContain(v => v == 11);

        writer.Consume(5);
        writer.Capacity.Should().BeGreaterThanOrEqualTo(32);
        writer.FreeCapacity.Should().BeGreaterThanOrEqualTo(32);
        writer.WrittenCount.Should().Be(0);
        writer.WrittenMemory.Length.Should().Be(0);
        writer.WrittenSpan.Length.Should().Be(0);

        var span3 = writer.GetSpan(32);
        writer.Capacity.Should().BeGreaterThanOrEqualTo(32);
        writer.FreeCapacity.Should().BeGreaterThanOrEqualTo(32);
        writer.WrittenCount.Should().Be(0);
        writer.WrittenMemory.Length.Should().Be(0);
        writer.WrittenSpan.Length.Should().Be(0);
    }

    [TestMethod]
    public void Consume_Limit()
    {
        using var writer = new PoolArrayBufferConsumer<byte>(capacity: 32);

        var span = writer.GetSpan(32);
        writer.Advance(span.Length - 1);
        writer.WrittenCount.Should().Be(span.Length - 1);
        writer.WrittenMemory.Length.Should().Be(span.Length - 1);
        writer.WrittenSpan.Length.Should().Be(span.Length - 1);

        FluentActions.Invoking(() => writer.Consume(writer.WrittenCount + 1)).Should().Throw<Exception>();

        writer.WrittenCount.Should().Be(span.Length - 1);
        writer.WrittenMemory.Length.Should().Be(span.Length - 1);
        writer.WrittenSpan.Length.Should().Be(span.Length - 1);

        writer.Consume(span.Length - 2);
        writer.WrittenCount.Should().Be(1);
        writer.WrittenMemory.Length.Should().Be(1);
        writer.WrittenSpan.Length.Should().Be(1);
    }

    [TestMethod]
    public void Clear()
    {
        using var writer = new PoolArrayBufferConsumer<byte>(capacity: 32);

        writer.GetSpan(32).Fill(11);
        writer.Advance(30);
        writer.Clear();

        writer.GetSpan(32).ToArray().Should().OnlyContain(v => v == 0);
    }

    [TestMethod]
    public void ResetWrittenCount()
    {
        using var writer = new PoolArrayBufferConsumer<byte>(capacity: 32);

        writer.GetSpan(32).Fill(11);
        writer.Advance(30);
        writer.ResetWrittenCount();

        writer.GetSpan(32).ToArray().Should().OnlyContain(v => v == 11);
    }

    [TestMethod]
    public void Dispose()
    {
        using var writer = new PoolArrayBufferConsumer<byte>(capacity: 32);

        writer.Dispose();

        FluentActions.Invoking(() => writer.GetMemory()).Should().Throw<ObjectDisposedException>();
        FluentActions.Invoking(() => writer.GetSpan()).Should().Throw<ObjectDisposedException>();
        FluentActions.Invoking(() => writer.Advance(1)).Should().Throw<ObjectDisposedException>();
        FluentActions.Invoking(() => writer.Consume(1)).Should().Throw<ObjectDisposedException>();
    }

}
