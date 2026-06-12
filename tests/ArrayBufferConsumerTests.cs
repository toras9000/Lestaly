namespace LestalyTest;

[TestClass]
public class ArrayBufferConsumerTests
{
    [TestMethod]
    public void Construct()
    {
        var writer = new ArrayBufferConsumer<byte>(capacity: 32);
        writer.Capacity.Should().Be(32);
        writer.FreeCapacity.Should().Be(32);
        writer.WrittenCount.Should().Be(0);
        writer.WrittenMemory.Length.Should().Be(0);
        writer.WrittenSpan.Length.Should().Be(0);
    }

    [TestMethod]
    public void GetMemory()
    {
        var writer = new ArrayBufferConsumer<byte>(capacity: 32);

        writer.GetMemory(10).Length.Should().Be(32);
        writer.Capacity.Should().Be(32);
        writer.FreeCapacity.Should().Be(32);
        writer.WrittenCount.Should().Be(0);
        writer.WrittenMemory.Length.Should().Be(0);
        writer.WrittenSpan.Length.Should().Be(0);

        writer.GetMemory(32).Length.Should().Be(32);
        writer.Capacity.Should().Be(32);
        writer.FreeCapacity.Should().Be(32);
        writer.WrittenCount.Should().Be(0);
        writer.WrittenMemory.Length.Should().Be(0);
        writer.WrittenSpan.Length.Should().Be(0);

        writer.GetMemory(80).Length.Should().Be(80);
        writer.Capacity.Should().Be(80);
        writer.FreeCapacity.Should().Be(80);
        writer.WrittenCount.Should().Be(0);
        writer.WrittenMemory.Length.Should().Be(0);
        writer.WrittenSpan.Length.Should().Be(0);

        writer.GetMemory(90).Length.Should().Be(160);
        writer.Capacity.Should().Be(160);
        writer.FreeCapacity.Should().Be(160);
        writer.WrittenCount.Should().Be(0);
        writer.WrittenMemory.Length.Should().Be(0);
        writer.WrittenSpan.Length.Should().Be(0);
    }

    [TestMethod]
    public void GetSpan()
    {
        var writer = new ArrayBufferConsumer<byte>(capacity: 32);

        writer.GetSpan(10).Length.Should().Be(32);
        writer.Capacity.Should().Be(32);
        writer.FreeCapacity.Should().Be(32);
        writer.WrittenCount.Should().Be(0);
        writer.WrittenMemory.Length.Should().Be(0);
        writer.WrittenSpan.Length.Should().Be(0);

        writer.GetSpan(32).Length.Should().Be(32);
        writer.Capacity.Should().Be(32);
        writer.FreeCapacity.Should().Be(32);
        writer.WrittenCount.Should().Be(0);
        writer.WrittenMemory.Length.Should().Be(0);
        writer.WrittenSpan.Length.Should().Be(0);

        writer.GetSpan(80).Length.Should().Be(80);
        writer.Capacity.Should().Be(80);
        writer.FreeCapacity.Should().Be(80);
        writer.WrittenCount.Should().Be(0);
        writer.WrittenMemory.Length.Should().Be(0);
        writer.WrittenSpan.Length.Should().Be(0);

        writer.GetSpan(90).Length.Should().Be(160);
        writer.Capacity.Should().Be(160);
        writer.FreeCapacity.Should().Be(160);
        writer.WrittenCount.Should().Be(0);
        writer.WrittenMemory.Length.Should().Be(0);
        writer.WrittenSpan.Length.Should().Be(0);
    }

    [TestMethod]
    public void Advance_Memory()
    {
        var writer = new ArrayBufferConsumer<byte>(capacity: 32);

        var memory1 = writer.GetMemory(10);

        var data1 = new byte[] { 12, 23, 34, 45, };
        data1.CopyTo(memory1.Span);
        writer.Advance(data1.Length);
        writer.Capacity.Should().Be(32);
        writer.FreeCapacity.Should().Be(32 - data1.Length);
        writer.WrittenCount.Should().Be(data1.Length);
        writer.WrittenMemory.ToArray().Should().Equal(data1);
        writer.WrittenSpan.ToArray().Should().Equal(data1);

        var memory2 = writer.GetMemory(80);
        writer.Capacity.Should().Be(data1.Length + 80);
        writer.FreeCapacity.Should().Be(80);
        writer.WrittenCount.Should().Be(data1.Length);
        writer.WrittenMemory.ToArray().Should().Equal(data1);
        writer.WrittenSpan.ToArray().Should().Equal(data1);

        var data2 = new byte[] { 56, 78, 90, 11, 22, 33, };
        data2.CopyTo(memory2.Span);
        writer.Advance(data2.Length);
        writer.Capacity.Should().Be(data1.Length + 80);
        writer.FreeCapacity.Should().Be(80 - data2.Length);
        writer.WrittenCount.Should().Be(data1.Length + data2.Length);
        writer.WrittenMemory.ToArray().Should().Equal(data1.Concat(data2));
        writer.WrittenSpan.ToArray().Should().Equal(data1.Concat(data2));
    }

    [TestMethod]
    public void Advance_Span()
    {
        var writer = new ArrayBufferConsumer<byte>(capacity: 32);

        var span1 = writer.GetSpan(10);

        var data1 = new byte[] { 12, 23, 34, 45, };
        data1.CopyTo(span1);
        writer.Advance(data1.Length);
        writer.Capacity.Should().Be(32);
        writer.FreeCapacity.Should().Be(32 - data1.Length);
        writer.WrittenCount.Should().Be(data1.Length);
        writer.WrittenMemory.ToArray().Should().Equal(data1);
        writer.WrittenSpan.ToArray().Should().Equal(data1);

        var span2 = writer.GetSpan(80);
        writer.Capacity.Should().Be(data1.Length + 80);
        writer.FreeCapacity.Should().Be(80);
        writer.WrittenCount.Should().Be(data1.Length);
        writer.WrittenMemory.ToArray().Should().Equal(data1);
        writer.WrittenSpan.ToArray().Should().Equal(data1);

        var data2 = new byte[] { 56, 78, 90, 11, 22, 33, };
        data2.CopyTo(span2);
        writer.Advance(data2.Length);
        writer.Capacity.Should().Be(data1.Length + 80);
        writer.FreeCapacity.Should().Be(80 - data2.Length);
        writer.WrittenCount.Should().Be(data1.Length + data2.Length);
        writer.WrittenMemory.ToArray().Should().Equal(data1.Concat(data2));
        writer.WrittenSpan.ToArray().Should().Equal(data1.Concat(data2));
    }

    [TestMethod]
    public void Advance_Limit()
    {
        var writer = new ArrayBufferConsumer<byte>(capacity: 32);

        var span = writer.GetSpan(32);
        span.Fill(123);

        writer.Advance(span.Length);
        writer.Capacity.Should().Be(32);
        writer.FreeCapacity.Should().Be(0);
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
        var writer = new ArrayBufferConsumer<byte>(capacity: 32);

        var span1 = writer.GetSpan(32);

        span1[..30].Fill(11);
        writer.Advance(30);
        writer.Capacity.Should().Be(32);
        writer.FreeCapacity.Should().Be(2);
        writer.WrittenCount.Should().Be(30);
        writer.WrittenMemory.Length.Should().Be(30);
        writer.WrittenSpan.Length.Should().Be(30);

        writer.Consume(25);
        writer.Capacity.Should().Be(32);
        writer.FreeCapacity.Should().Be(2);
        writer.WrittenCount.Should().Be(5);
        writer.WrittenMemory.Length.Should().Be(5);
        writer.WrittenSpan.Length.Should().Be(5);

        var span2 = writer.GetSpan(20);
        writer.Capacity.Should().Be(32);
        writer.FreeCapacity.Should().Be(32 - 5);
        writer.WrittenCount.Should().Be(5);
        writer.WrittenMemory.Length.Should().Be(5);
        writer.WrittenMemory[..5].ToArray().Should().OnlyContain(v => v == 11);
        writer.WrittenSpan.Length.Should().Be(5);
        writer.WrittenSpan[..5].ToArray().Should().OnlyContain(v => v == 11);

        writer.Consume(5);
        writer.Capacity.Should().Be(32);
        writer.FreeCapacity.Should().Be(32);
        writer.WrittenCount.Should().Be(0);
        writer.WrittenMemory.Length.Should().Be(0);
        writer.WrittenSpan.Length.Should().Be(0);

        var span3 = writer.GetSpan(32);
        writer.Capacity.Should().Be(32);
        writer.FreeCapacity.Should().Be(32);
        writer.WrittenCount.Should().Be(0);
        writer.WrittenMemory.Length.Should().Be(0);
        writer.WrittenSpan.Length.Should().Be(0);
    }

    [TestMethod]
    public void Consume_Limit()
    {
        var writer = new ArrayBufferConsumer<byte>(capacity: 32);

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
        var writer = new ArrayBufferConsumer<byte>(capacity: 32);

        writer.GetSpan(32).Fill(11);
        writer.Advance(30);
        writer.Clear();

        writer.GetSpan(32).ToArray().Should().OnlyContain(v => v == 0);
    }

    [TestMethod]
    public void ResetWrittenCount()
    {
        var writer = new ArrayBufferConsumer<byte>(capacity: 32);

        writer.GetSpan(32).Fill(11);
        writer.Advance(30);
        writer.ResetWrittenCount();

        writer.GetSpan(32).ToArray().Should().OnlyContain(v => v == 11);
    }

}
