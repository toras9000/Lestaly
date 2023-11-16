namespace LestalyTest;

[TestClass]
public class CombinedDisposablesTest
{
    [TestMethod]
    public void Constructor_Default()
    {
        var target = new CombinedDisposables();
        target.ReverseDispose.Should().Be(true);
        target.DisposeOnRemove.Should().Be(false);
        target.LatestException.Should().BeNull();
        target.IsDisposed.Should().Be(false);
        target.IsReadOnly.Should().Be(false);
        target.Count.Should().Be(0);
        target.Should().BeEmpty();
    }

    [TestMethod]
    public void Constructor_Param1()
    {
        {
            var target = new CombinedDisposables(true);
            target.ReverseDispose.Should().Be(true);
            target.DisposeOnRemove.Should().Be(false);
            target.LatestException.Should().BeNull();
            target.IsDisposed.Should().Be(false);
            target.IsReadOnly.Should().Be(false);
            target.Count.Should().Be(0);
            target.Should().BeEmpty();
        }
        {
            var target = new CombinedDisposables(false);
            target.ReverseDispose.Should().Be(false);
            target.DisposeOnRemove.Should().Be(false);
            target.LatestException.Should().BeNull();
            target.IsDisposed.Should().Be(false);
            target.IsReadOnly.Should().Be(false);
            target.Count.Should().Be(0);
            target.Should().BeEmpty();
        }
    }

    [TestMethod]
    public void Constructor_Param2()
    {
        {
            var target = new CombinedDisposables(true, true);
            target.ReverseDispose.Should().Be(true);
            target.DisposeOnRemove.Should().Be(true);
            target.LatestException.Should().BeNull();
            target.IsDisposed.Should().Be(false);
            target.IsReadOnly.Should().Be(false);
            target.Count.Should().Be(0);
            target.Should().BeEmpty();
        }
        {
            var target = new CombinedDisposables(true, false);
            target.ReverseDispose.Should().Be(true);
            target.DisposeOnRemove.Should().Be(false);
            target.LatestException.Should().BeNull();
            target.IsDisposed.Should().Be(false);
            target.IsReadOnly.Should().Be(false);
            target.Count.Should().Be(0);
            target.Should().BeEmpty();
        }
        {
            var target = new CombinedDisposables(false, true);
            target.ReverseDispose.Should().Be(false);
            target.DisposeOnRemove.Should().Be(true);
            target.LatestException.Should().BeNull();
            target.IsDisposed.Should().Be(false);
            target.IsReadOnly.Should().Be(false);
            target.Count.Should().Be(0);
            target.Should().BeEmpty();
        }
        {
            var target = new CombinedDisposables(false, false);
            target.ReverseDispose.Should().Be(false);
            target.DisposeOnRemove.Should().Be(false);
            target.LatestException.Should().BeNull();
            target.IsDisposed.Should().Be(false);
            target.IsReadOnly.Should().Be(false);
            target.Count.Should().Be(0);
            target.Should().BeEmpty();
        }
    }

    [TestMethod]
    public void Add()
    {
        var item1 = new Mock<IDisposable>();
        var item2 = new Mock<IDisposable>();

        var target = new CombinedDisposables();
        target.Add(item1.Object);
        target.Count.Should().Be(1);
        target.Add(item2.Object);
        target.Count.Should().Be(2);
        target.Should().Equal(item1.Object, item2.Object);
    }

    [TestMethod]
    public void Add_ArgNull()
    {
        var target = new CombinedDisposables();

        new Action(() => target.Add(null!)).Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void Remove_NoDispose()
    {
        var item1 = new Mock<IDisposable>();
        var item2 = new Mock<IDisposable>();
        var item3 = new Mock<IDisposable>();
        var item4 = new Mock<IDisposable>();
        var item5 = new Mock<IDisposable>();

        var target = new CombinedDisposables(reverse: true, removeDispose: false);
        target.Add(item1.Object);
        target.Add(item2.Object);
        target.Add(item3.Object);
        target.Add(item4.Object);
        target.Add(item5.Object);
        target.Should().HaveCount(5);

        target.DisposeOnRemove.Should().Be(false, "念のため前提を確認");

        target.Remove(item2.Object).Should().Be(true);
        target.Should().Equal(
            item1.Object,
            item3.Object,
            item4.Object,
            item5.Object
        );
        item2.Verify(o => o.Dispose(), Times.Never());

        target.Remove(item2.Object).Should().Be(false, "除去済みオブジェクトを再指定");
        target.Should().Equal(
            item1.Object,
            item3.Object,
            item4.Object,
            item5.Object
        );
        item2.Verify(o => o.Dispose(), Times.Never());

        target.Remove(item1.Object).Should().Be(true);
        target.Remove(item3.Object).Should().Be(true);
        target.Remove(item4.Object).Should().Be(true);
        target.Remove(item5.Object).Should().Be(true);
        target.Should().HaveCount(0);
        item1.Verify(o => o.Dispose(), Times.Never());
        item3.Verify(o => o.Dispose(), Times.Never());
        item4.Verify(o => o.Dispose(), Times.Never());
        item5.Verify(o => o.Dispose(), Times.Never());
    }

    [TestMethod]
    public void Remove_WithDispose()
    {
        var item1 = new Mock<IDisposable>();
        var item2 = new Mock<IDisposable>();
        var item3 = new Mock<IDisposable>();
        var item4 = new Mock<IDisposable>();
        var item5 = new Mock<IDisposable>();

        var target = new CombinedDisposables(reverse: true, removeDispose: true);
        target.Add(item1.Object);
        target.Add(item2.Object);
        target.Add(item3.Object);
        target.Add(item4.Object);
        target.Add(item5.Object);
        target.Should().HaveCount(5);

        target.DisposeOnRemove.Should().Be(true, "念のため前提を確認");

        target.Remove(item2.Object).Should().Be(true);
        target.Should().Equal(
            item1.Object,
            item3.Object,
            item4.Object,
            item5.Object
        );
        item2.Verify(o => o.Dispose(), Times.Once());

        target.Remove(item2.Object).Should().Be(false, "除去済みオブジェクトを再指定");
        target.Should().Equal(
            item1.Object,
            item3.Object,
            item4.Object,
            item5.Object
        );
        item2.Verify(o => o.Dispose(), Times.Exactly(2));

        target.Remove(item1.Object).Should().Be(true);
        target.Remove(item3.Object).Should().Be(true);
        target.Remove(item4.Object).Should().Be(true);
        target.Remove(item5.Object).Should().Be(true);
        target.Should().HaveCount(0);
        item1.Verify(o => o.Dispose(), Times.Once());
        item3.Verify(o => o.Dispose(), Times.Once());
        item4.Verify(o => o.Dispose(), Times.Once());
        item5.Verify(o => o.Dispose(), Times.Once());

        var item6 = new Mock<IDisposable>();
        target.Remove(item6.Object).Should().Be(false);
        item6.Verify(o => o.Dispose(), Times.Once());
    }

    [TestMethod]
    public void Remove_ArgNull()
    {
        var target = new CombinedDisposables();

        new Action(() => target.Remove(null!)).Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void Clear_NoDispose()
    {
        var item1 = new Mock<IDisposable>();
        var item2 = new Mock<IDisposable>();
        var item3 = new Mock<IDisposable>();
        var item4 = new Mock<IDisposable>();
        var item5 = new Mock<IDisposable>();

        var target = new CombinedDisposables();
        target.Add(item1.Object);
        target.Add(item2.Object);
        target.Add(item3.Object);
        target.Add(item4.Object);
        target.Add(item5.Object);
        target.Should().HaveCount(5);

        target.DisposeOnRemove.Should().Be(false, "念のため前提を確認");
        target.Clear();
        target.Should().HaveCount(0);

        item1.Verify(o => o.Dispose(), Times.Never());
        item2.Verify(o => o.Dispose(), Times.Never());
        item3.Verify(o => o.Dispose(), Times.Never());
        item4.Verify(o => o.Dispose(), Times.Never());
        item5.Verify(o => o.Dispose(), Times.Never());
    }

    [TestMethod]
    public void Clear_WithDispose_Forward()
    {
        var item1 = new Mock<IDisposable>();
        var item2 = new Mock<IDisposable>();
        var item3 = new Mock<IDisposable>();
        var item4 = new Mock<IDisposable>();
        var item5 = new Mock<IDisposable>();

        var log = new List<IDisposable>();
        item1.Setup(o => o.Dispose()).Callback(() => log.Add(item1.Object));
        item2.Setup(o => o.Dispose()).Callback(() => log.Add(item2.Object));
        item3.Setup(o => o.Dispose()).Callback(() => log.Add(item3.Object));
        item4.Setup(o => o.Dispose()).Callback(() => log.Add(item4.Object));
        item5.Setup(o => o.Dispose()).Callback(() => log.Add(item5.Object));

        var target = new CombinedDisposables(reverse: false, removeDispose: true);
        target.Add(item1.Object);
        target.Add(item2.Object);
        target.Add(item3.Object);
        target.Add(item4.Object);
        target.Add(item5.Object);
        target.Should().HaveCount(5);

        target.DisposeOnRemove.Should().Be(true, "念のため前提を確認");
        target.Clear();
        target.Should().HaveCount(0);
        target.IsDisposed.Should().Be(false);

        item1.Verify(o => o.Dispose(), Times.Once());
        item2.Verify(o => o.Dispose(), Times.Once());
        item3.Verify(o => o.Dispose(), Times.Once());
        item4.Verify(o => o.Dispose(), Times.Once());
        item5.Verify(o => o.Dispose(), Times.Once());

        log.Should().Equal(
            item1.Object,
            item2.Object,
            item3.Object,
            item4.Object,
            item5.Object
        );
    }

    [TestMethod]
    public void Clear_WithDispose_Reverse()
    {
        var item1 = new Mock<IDisposable>();
        var item2 = new Mock<IDisposable>();
        var item3 = new Mock<IDisposable>();
        var item4 = new Mock<IDisposable>();
        var item5 = new Mock<IDisposable>();

        var log = new List<IDisposable>();
        item1.Setup(o => o.Dispose()).Callback(() => log.Add(item1.Object));
        item2.Setup(o => o.Dispose()).Callback(() => log.Add(item2.Object));
        item3.Setup(o => o.Dispose()).Callback(() => log.Add(item3.Object));
        item4.Setup(o => o.Dispose()).Callback(() => log.Add(item4.Object));
        item5.Setup(o => o.Dispose()).Callback(() => log.Add(item5.Object));

        var target = new CombinedDisposables(reverse: true, removeDispose: true);
        target.Add(item1.Object);
        target.Add(item2.Object);
        target.Add(item3.Object);
        target.Add(item4.Object);
        target.Add(item5.Object);
        target.Should().HaveCount(5);

        target.DisposeOnRemove.Should().Be(true, "念のため前提を確認");
        target.Clear();
        target.Should().HaveCount(0);
        target.IsDisposed.Should().Be(false);

        item1.Verify(o => o.Dispose(), Times.Once());
        item2.Verify(o => o.Dispose(), Times.Once());
        item3.Verify(o => o.Dispose(), Times.Once());
        item4.Verify(o => o.Dispose(), Times.Once());
        item5.Verify(o => o.Dispose(), Times.Once());

        log.Should().Equal(
            item5.Object,
            item4.Object,
            item3.Object,
            item2.Object,
            item1.Object
        );
    }

    [TestMethod]
    public void Contains()
    {
        var item1 = new Mock<IDisposable>();
        var item2 = new Mock<IDisposable>();
        var item3 = new Mock<IDisposable>();
        var item4 = new Mock<IDisposable>();
        var item5 = new Mock<IDisposable>();

        var target = new CombinedDisposables(reverse: true, removeDispose: false);
        target.Add(item1.Object);
        target.Add(item2.Object);
        target.Add(item3.Object);
        target.Add(item4.Object);
        target.Add(item5.Object);
        target.Should().HaveCount(5);

        target.Remove(item2.Object);
        target.Contains(item1.Object).Should().Be(true);
        target.Contains(item2.Object).Should().Be(false);
        target.Contains(item3.Object).Should().Be(true);
        target.Contains(item4.Object).Should().Be(true);
        target.Contains(item5.Object).Should().Be(true);

        target.Remove(item2.Object);    // 同じオブジェクトを再度指定
        target.Contains(item1.Object).Should().Be(true);
        target.Contains(item2.Object).Should().Be(false);
        target.Contains(item3.Object).Should().Be(true);
        target.Contains(item4.Object).Should().Be(true);
        target.Contains(item5.Object).Should().Be(true);

        target.Remove(item1.Object);
        target.Remove(item3.Object);
        target.Remove(item4.Object);
        target.Remove(item5.Object);
        target.Contains(item1.Object).Should().Be(false);
        target.Contains(item2.Object).Should().Be(false);
        target.Contains(item3.Object).Should().Be(false);
        target.Contains(item4.Object).Should().Be(false);
        target.Contains(item5.Object).Should().Be(false);
    }

    [TestMethod]
    public void Contains_ArgNull()
    {
        var target = new CombinedDisposables();

        new Action(() => target.Contains(null!)).Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void Enumerate()
    {
        var item1 = new Mock<IDisposable>();
        var item2 = new Mock<IDisposable>();
        var item3 = new Mock<IDisposable>();
        var item4 = new Mock<IDisposable>();
        var item5 = new Mock<IDisposable>();

        var target = new CombinedDisposables(reverse: true, removeDispose: false);
        target.Add(item1.Object);
        target.Add(item2.Object);
        target.Add(item3.Object);
        target.Add(item4.Object);
        target.Add(item5.Object);

        target.AsEnumerable().Should().Equal(
            item1.Object,
            item2.Object,
            item3.Object,
            item4.Object,
            item5.Object
        );

        // for Un generic enumeration
        var ungeneric = new List<object>();
        foreach (var obj in ((System.Collections.IEnumerable)target))
        {
            ungeneric.Add(obj);
        }
        ungeneric.Should().Equal(
            item1.Object,
            item2.Object,
            item3.Object,
            item4.Object,
            item5.Object
        );
    }

    [TestMethod]
    public void CopyTo()
    {
        var item1 = new Mock<IDisposable>();
        var item2 = new Mock<IDisposable>();
        var item3 = new Mock<IDisposable>();
        var item4 = new Mock<IDisposable>();
        var item5 = new Mock<IDisposable>();

        var target = new CombinedDisposables(reverse: true, removeDispose: false);
        target.Add(item1.Object);
        target.Add(item2.Object);
        target.Add(item3.Object);
        target.Add(item4.Object);
        target.Add(item5.Object);

        var source = (ICollection<IDisposable>)target;

        var dest1 = new IDisposable[5];
        source.CopyTo(dest1, 0);
        dest1.Should().Equal(
            item1.Object,
            item2.Object,
            item3.Object,
            item4.Object,
            item5.Object
        );

        var dest2 = new IDisposable[10];
        source.CopyTo(dest2, 3);
        dest2.Should().Equal(
            null!,
            null!,
            null!,
            item1.Object,
            item2.Object,
            item3.Object,
            item4.Object,
            item5.Object,
            null!,
            null!
        );
    }

    [TestMethod]
    public void Dispose_Forward()
    {
        var item1 = new Mock<IDisposable>();
        var item2 = new Mock<IDisposable>();
        var item3 = new Mock<IDisposable>();
        var item4 = new Mock<IDisposable>();
        var item5 = new Mock<IDisposable>();

        var log = new List<IDisposable>();
        item1.Setup(o => o.Dispose()).Callback(() => log.Add(item1.Object));
        item2.Setup(o => o.Dispose()).Callback(() => log.Add(item2.Object));
        item3.Setup(o => o.Dispose()).Callback(() => log.Add(item3.Object));
        item4.Setup(o => o.Dispose()).Callback(() => log.Add(item4.Object));
        item5.Setup(o => o.Dispose()).Callback(() => log.Add(item5.Object));

        var target = new CombinedDisposables(reverse: false, removeDispose: false);
        target.Add(item1.Object);
        target.Add(item2.Object);
        target.Add(item3.Object);
        target.Add(item4.Object);
        target.Add(item5.Object);

        target.Dispose();

        target.Should().HaveCount(0);
        target.IsDisposed.Should().Be(true);
        item1.Verify(o => o.Dispose(), Times.Once());
        item2.Verify(o => o.Dispose(), Times.Once());
        item3.Verify(o => o.Dispose(), Times.Once());
        item4.Verify(o => o.Dispose(), Times.Once());
        item5.Verify(o => o.Dispose(), Times.Once());

        log.Should().Equal(
            item1.Object,
            item2.Object,
            item3.Object,
            item4.Object,
            item5.Object
        );

        var item6 = new Mock<IDisposable>();
        target.Add(item6.Object);
        item6.Verify(o => o.Dispose(), Times.Once());
    }

    [TestMethod]
    public void Dispose_Reverse()
    {
        var item1 = new Mock<IDisposable>();
        var item2 = new Mock<IDisposable>();
        var item3 = new Mock<IDisposable>();
        var item4 = new Mock<IDisposable>();
        var item5 = new Mock<IDisposable>();

        var log = new List<IDisposable>();
        item1.Setup(o => o.Dispose()).Callback(() => log.Add(item1.Object));
        item2.Setup(o => o.Dispose()).Callback(() => log.Add(item2.Object));
        item3.Setup(o => o.Dispose()).Callback(() => log.Add(item3.Object));
        item4.Setup(o => o.Dispose()).Callback(() => log.Add(item4.Object));
        item5.Setup(o => o.Dispose()).Callback(() => log.Add(item5.Object));

        var target = new CombinedDisposables(reverse: true, removeDispose: false);
        target.Add(item1.Object);
        target.Add(item2.Object);
        target.Add(item3.Object);
        target.Add(item4.Object);
        target.Add(item5.Object);

        target.Dispose();

        target.Should().HaveCount(0);
        target.IsDisposed.Should().Be(true);
        item1.Verify(o => o.Dispose(), Times.Once());
        item2.Verify(o => o.Dispose(), Times.Once());
        item3.Verify(o => o.Dispose(), Times.Once());
        item4.Verify(o => o.Dispose(), Times.Once());
        item5.Verify(o => o.Dispose(), Times.Once());

        log.Should().Equal(
            item5.Object,
            item4.Object,
            item3.Object,
            item2.Object,
            item1.Object
        );

        var item6 = new Mock<IDisposable>();
        target.Add(item6.Object);
        item6.Verify(o => o.Dispose(), Times.Once());
    }

    [TestMethod]
    public void LatestException_Remove()
    {
        var item1 = new Mock<IDisposable>();
        var item2 = new Mock<IDisposable>();
        var item3 = new Mock<IDisposable>();
        var item4 = new Mock<IDisposable>();
        var item5 = new Mock<IDisposable>();

        var ex2 = new Exception("2");
        item2.Setup(o => o.Dispose()).Callback(() => throw ex2);

        var ex4 = new Exception("4");
        item4.Setup(o => o.Dispose()).Callback(() => throw ex4);

        var target = new CombinedDisposables(reverse: true, removeDispose: true);
        target.Add(item1.Object);
        target.Add(item2.Object);
        target.Add(item3.Object);
        target.Add(item4.Object);
        target.Add(item5.Object);

        target.Remove(item1.Object).Should().Be(true);
        target.LatestException.Should().BeNull();

        target.Remove(item2.Object).Should().Be(true);
        target.LatestException.Should().BeSameAs(ex2);

        target.Remove(item3.Object).Should().Be(true);
        target.LatestException.Should().BeNull();

        target.Remove(item4.Object).Should().Be(true);
        target.LatestException.Should().BeSameAs(ex4);

        target.Remove(item5.Object).Should().Be(true);
        target.LatestException.Should().BeNull();
    }

    [TestMethod]
    public void LatestException_Clear_Single()
    {
        var item1 = new Mock<IDisposable>();
        var item2 = new Mock<IDisposable>();
        var item3 = new Mock<IDisposable>();
        var item4 = new Mock<IDisposable>();
        var item5 = new Mock<IDisposable>();

        var ex4 = new Exception("4");
        item4.Setup(o => o.Dispose()).Callback(() => throw ex4);

        var target = new CombinedDisposables(reverse: false, removeDispose: true);
        target.Add(item1.Object);
        target.Add(item2.Object);
        target.Add(item3.Object);
        target.Add(item4.Object);
        target.Add(item5.Object);

        target.Clear();
        target.LatestException.Should()
            .BeOfType<Exception>()
            .Which.Should().BeSameAs(ex4);
    }

    [TestMethod]
    public void LatestException_Clear_Forward()
    {
        var item1 = new Mock<IDisposable>();
        var item2 = new Mock<IDisposable>();
        var item3 = new Mock<IDisposable>();
        var item4 = new Mock<IDisposable>();
        var item5 = new Mock<IDisposable>();

        var ex2 = new Exception("2");
        item2.Setup(o => o.Dispose()).Callback(() => throw ex2);

        var ex4 = new Exception("4");
        item4.Setup(o => o.Dispose()).Callback(() => throw ex4);

        var target = new CombinedDisposables(reverse: false, removeDispose: true);
        target.Add(item1.Object);
        target.Add(item2.Object);
        target.Add(item3.Object);
        target.Add(item4.Object);
        target.Add(item5.Object);

        target.Clear();
        target.LatestException.Should()
            .BeOfType<AggregateException>()
            .Which.InnerExceptions.Should().Equal(ex2, ex4);
    }

    [TestMethod]
    public void LatestException_Clear_Reverse()
    {
        var item1 = new Mock<IDisposable>();
        var item2 = new Mock<IDisposable>();
        var item3 = new Mock<IDisposable>();
        var item4 = new Mock<IDisposable>();
        var item5 = new Mock<IDisposable>();

        var ex2 = new Exception("2");
        item2.Setup(o => o.Dispose()).Callback(() => throw ex2);

        var ex4 = new Exception("4");
        item4.Setup(o => o.Dispose()).Callback(() => throw ex4);

        var target = new CombinedDisposables(reverse: true, removeDispose: true);
        target.Add(item1.Object);
        target.Add(item2.Object);
        target.Add(item3.Object);
        target.Add(item4.Object);
        target.Add(item5.Object);

        target.Clear();
        target.LatestException.Should()
            .BeOfType<AggregateException>()
            .Which.InnerExceptions.Should().Equal(ex4, ex2);
    }

    [TestMethod]
    public void LatestException_Dispose_Forward()
    {
        var item1 = new Mock<IDisposable>();
        var item2 = new Mock<IDisposable>();
        var item3 = new Mock<IDisposable>();
        var item4 = new Mock<IDisposable>();
        var item5 = new Mock<IDisposable>();

        var ex2 = new Exception("2");
        item2.Setup(o => o.Dispose()).Callback(() => throw ex2);

        var ex4 = new Exception("4");
        item4.Setup(o => o.Dispose()).Callback(() => throw ex4);

        var target = new CombinedDisposables(reverse: false, removeDispose: true);
        target.Add(item1.Object);
        target.Add(item2.Object);
        target.Add(item3.Object);
        target.Add(item4.Object);
        target.Add(item5.Object);

        target.Dispose();
        target.LatestException.Should()
            .BeOfType<AggregateException>()
            .Which.InnerExceptions.Should().Equal(ex2, ex4);
    }

    [TestMethod]
    public void LatestException_Dispose_Reserve()
    {
        var item1 = new Mock<IDisposable>();
        var item2 = new Mock<IDisposable>();
        var item3 = new Mock<IDisposable>();
        var item4 = new Mock<IDisposable>();
        var item5 = new Mock<IDisposable>();

        var ex2 = new Exception("2");
        item2.Setup(o => o.Dispose()).Callback(() => throw ex2);

        var ex4 = new Exception("4");
        item4.Setup(o => o.Dispose()).Callback(() => throw ex4);

        var target = new CombinedDisposables(reverse: true, removeDispose: true);
        target.Add(item1.Object);
        target.Add(item2.Object);
        target.Add(item3.Object);
        target.Add(item4.Object);
        target.Add(item5.Object);

        target.Dispose();
        target.LatestException.Should()
            .BeOfType<AggregateException>()
            .Which.InnerExceptions.Should().Equal(ex4, ex2);
    }
}

