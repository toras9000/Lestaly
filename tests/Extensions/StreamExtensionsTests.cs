using System.Buffers;
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

}
