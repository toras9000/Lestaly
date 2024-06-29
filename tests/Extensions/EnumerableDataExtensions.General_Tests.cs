using System.Globalization;

namespace LestalyTest.Extensions;

[TestClass()]
public class EnumerableDataExtensions_General_Tests
{
    [TestMethod]
    public async Task WriteAllLinesAsync()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // 保存データ
        await Enumerable.Range(10, 20)
            .Select(n => $"[[{n}]]")
            .WriteAllLinesAsync(target);

        // 検証
        File.ReadAllLines(target.FullName).Should().Equal(Enumerable.Range(10, 20).Select(n => $"[[{n}]]"));
    }

    [TestMethod]
    public async Task WriteAllLinesAsync_Cancel()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // キャンセルソース
        var canceller = new CancellationTokenSource();
        canceller.CancelAfter(1000);

        // 無限シーケンスを生成するイテレータ
        IEnumerable<string> infinitySequence()
        {
            var count = 0u;
            while (true)
            {
                yield return $"{count++}";
                Thread.Sleep(100);
            }
        }

        // 保存処理の中止
        await FluentActions.Awaiting(async () => await infinitySequence().WriteAllLinesAsync(target, canceller.Token))
            .Should().ThrowAsync<OperationCanceledException>();

    }
}
