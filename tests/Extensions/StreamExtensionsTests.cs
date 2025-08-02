using System.Buffers;
using System.IO;
using System.IO.Pipelines;

namespace LestalyTest.Extensions;

[TestClass]
public class StreamExtensionsTests
{
    [TestMethod]
    public async Task ReadToIdleAsync_Array_Full()
    {
        // テスト用パイプ
        var pipe = new Pipe();

        // テスト用データ
        var dummy = Random.Shared.GetBytes(5);
        var pad = (byte)0xA5;
        var buffer = new byte[32].FillBy<byte>(pad);

        // テスト用パイプからの読み取りタスクを開始
        using var output = pipe.Reader.AsStream();
        var idleTime = 3000;
        var readTask = output.ReadToIdleAsync(buffer, idleTime);

        // パイプに書き込み
        using var input = pipe.Writer.AsStream();
        var writeCount = buffer.Length / dummy.Length + 1;
        for (var i = 0; i < writeCount; i++)
        {
            await input.WriteAsync(dummy);
            await input.FlushAsync();
            await Task.Delay(1000);
        }

        // アイドル時間を超えるよう待機
        await Task.Delay(idleTime);
        input.Write(dummy);

        // 読み取りタスク完了を待機
        var actualSize = await readTask;

        // 期待値
        var expect = dummy.AsEnumerable().Repetition(writeCount)
            .Take(buffer.Length)
            .ToArray();

        // 結果値の評価
        buffer[..actualSize].Should().Equal(expect);
        buffer[actualSize..].Should().AllBeEquivalentTo(pad);
    }

    [TestMethod]
    public async Task ReadToIdleAsync_Array_Idle()
    {
        // テスト用パイプ
        var pipe = new Pipe();

        // テスト用データ
        var dummy = Random.Shared.GetBytes(5);
        var pad = (byte)0xA5;
        var buffer = new byte[32].FillBy<byte>(pad);

        // テスト用パイプからの読み取りタスクを開始
        using var output = pipe.Reader.AsStream();
        var idleTime = 3000;
        var readTask = output.ReadToIdleAsync(buffer, idleTime);

        // パイプに書き込み
        using var input = pipe.Writer.AsStream();
        var writeCount = 3;
        for (var i = 0; i < writeCount; i++)
        {
            await input.WriteAsync(dummy);
            await input.FlushAsync();
            await Task.Delay(1000);
        }

        // アイドル時間を超えるよう待機
        await Task.Delay(idleTime);
        input.Write(dummy);

        // 読み取りタスク完了を待機
        var actualSize = await readTask;

        // 期待値
        var expect = dummy.AsEnumerable().Repetition(writeCount)
            .Take(buffer.Length)
            .ToArray();

        // 結果値の評価
        buffer[..actualSize].Should().Equal(expect);
        buffer[actualSize..].Should().AllBeEquivalentTo(pad);
    }

    [TestMethod]
    public async Task ReadToIdleAsync_Writer()
    {
        // テスト用パイプ
        var pipe = new Pipe();

        // テスト用データ
        var dummy = Random.Shared.GetBytes(5);
        var writer = new ArrayBufferWriter<byte>();

        // テスト用パイプからの読み取りタスクを開始
        using var output = pipe.Reader.AsStream();
        var idleTime = 3000;
        var readTask = output.ReadToIdleAsync(writer, idleTime);

        // パイプに書き込み
        using var input = pipe.Writer.AsStream();
        var writeCount = 5;
        for (var i = 0; i < writeCount; i++)
        {
            await input.WriteAsync(dummy);
            await input.FlushAsync();
            await Task.Delay(1000);
        }

        // アイドル時間を超えるよう待機
        await Task.Delay(idleTime);
        input.Write(dummy);

        // 読み取りタスク完了を待機
        var actualSize = await readTask;

        // 期待値
        var expect = dummy.AsEnumerable().Repetition(writeCount)
            .ToArray();

        // 結果値の評価
        writer.WrittenMemory.ToArray().Should().Equal(expect);
    }

    [TestMethod]
    public async Task EnumerateLinesAsync()
    {
        using var tempDir = new TempDir();
        var file = tempDir.Info.RelativeFile("test.txt");
        await file.WriteAllLinesAsync(["abc", "def", "ghi"]);

        using var stream = file.OpenRead();
        var lines = await stream.EnumerateLinesAsync().ToArrayAsync();
        lines.Should().Equal(["abc", "def", "ghi"]);
    }

    [TestMethod]
    public async Task ReadAllLinesAsync()
    {
        using var tempDir = new TempDir();
        var file = tempDir.Info.RelativeFile("test.txt");
        await file.WriteAllLinesAsync(["abc", "def", "ghi"]);

        using var stream = file.OpenRead();
        var lines = await stream.ReadAllLinesAsync();
        lines.Should().Equal(["abc", "def", "ghi"]);
    }

    [TestMethod]
    public async Task WriteAllLinesAsync()
    {
        using var tempDir = new TempDir();
        var file = tempDir.Info.RelativeFile("test.txt");
        using (var stream = file.OpenWrite())
        {
            await stream.WriteAllLinesAsync(["abc", "def", "ghi"]);
        }
        var lines = file.ReadAllLines();
        lines.Should().Equal(["abc", "def", "ghi"]);
    }

    [TestMethod]
    public async Task WriteToFileAsync()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストデータ
        var data = Enumerable.Range(30, 256).Select(n => (byte)n).ToArray();
        using var stream = new MemoryStream(data);

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // テスト対象実行
        stream.Position = 10;
        await stream.WriteToFileAsync(target);

        // 検証
        File.ReadAllBytes(target.FullName).Should().Equal(data[10..]);
    }

    [TestMethod]
    public async Task WriteToFileAsync_task()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストデータ
        var data = Enumerable.Range(30, 256).Select(n => (byte)n).ToArray();
        using var stream = new MemoryStream(data);

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // テスト対象実行
        stream.Position = 10;
        await Task.FromResult(stream).WriteToFileAsync(target);

        // 検証
        File.ReadAllBytes(target.FullName).Should().Equal(data[10..]);
    }

    [TestMethod]
    public async Task ToMemoryAsync()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストデータ
        var data = Enumerable.Range(30, 256).Select(n => (byte)n).ToArray();
        using var stream = new MemoryStream(data);

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // テスト対象実行
        stream.Position = 10;
        var memory = await stream.ToMemoryAsync();

        // 検証
        memory.ToArray().Should().Equal(data[10..]);
    }

}
