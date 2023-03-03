using System.ComponentModel.DataAnnotations;
using System.Globalization;
using FluentAssertions;
using Lestaly;

namespace LestalyTest.Extensions;

[TestClass()]
public class EnumerableDataExtensions_SaveToCsv_Tests
{
    [TestMethod]
    public async Task SaveToCsvAsync_Default()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // 保存データ
        var data = new[]
        {
            new { Text ="abc", Number = 12, Decimal = 0.12, Time = new DateTime(2222, 3, 4, 5, 6, 7, 8), },
            new { Text ="def", Number = 34, Decimal = 3.45, Time = new DateTime(2223, 4, 5, 6, 7, 8, 9), },
            new { Text ="ghi", Number = 56, Decimal = 6.78, Time = new DateTime(1111, 1, 1, 1, 1, 1, 1), },
        };

        // テスト対象実行
        await data.SaveToCsvAsync(target.FullName);

        // 期待値
        var expect = "";
        expect += "Text,Number,Decimal,Time" + Environment.NewLine;
        expect += "abc,12,0.12,03/04/2222 05:06:07" + Environment.NewLine;
        expect += "def,34,3.45,04/05/2223 06:07:08" + Environment.NewLine;
        expect += "ghi,56,6.78,01/01/1111 01:01:01" + Environment.NewLine;

        // 検証
        File.ReadAllText(target.FullName).Should().Be(expect);
    }

    [TestMethod]
    public async Task SaveToCsvAsync_NullValue()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // 保存データ
        var data = new[]
        {
            new { Text = (string?)null,   Number = (int?)12,   },
            new { Text = (string?)"def",  Number = (int?)null, },
        };

        // テスト対象実行
        await data.SaveToCsvAsync(target.FullName);

        // 期待値
        var expect = "";
        expect += "Text,Number" + Environment.NewLine;
        expect += ",12" + Environment.NewLine;
        expect += "def," + Environment.NewLine;

        // 検証
        File.ReadAllText(target.FullName).Should().Be(expect);
    }

    [TestMethod]
    public async Task SaveToCsvAsync_SingleColumn()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // 保存データ
        var data = new[]
        {
            new { Text ="abc", },
            new { Text ="def", },
            new { Text ="ghi", },
        };

        // テスト対象実行
        await data.SaveToCsvAsync(target.FullName);

        // 期待値
        var expect = "";
        expect += "Text" + Environment.NewLine;
        expect += "abc" + Environment.NewLine;
        expect += "def" + Environment.NewLine;
        expect += "ghi" + Environment.NewLine;

        // 検証
        File.ReadAllText(target.FullName).Should().Be(expect);
    }

    [TestMethod]
    public async Task SaveToCsvAsync_Separator()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // 保存データ
        var data = new[]
        {
            new { Text ="abc", Number = 12, Decimal = 0.12, Time = new DateTime(2222, 3, 4, 5, 6, 7, 8), },
            new { Text ="def", Number = 34, Decimal = 3.45, Time = new DateTime(2223, 4, 5, 6, 7, 8, 9), },
            new { Text ="ghi", Number = 56, Decimal = 6.78, Time = new DateTime(1111, 1, 1, 1, 1, 1, 1), },
        };

        // 実行オプション
        var options = new SaveToCsvOptions()
        {
            Separator = '&'
        };

        // テスト対象実行
        await data.SaveToCsvAsync(target.FullName, options);

        // 期待値
        var expect = "";
        expect += "Text&Number&Decimal&Time" + Environment.NewLine;
        expect += "abc&12&0.12&03/04/2222 05:06:07" + Environment.NewLine;
        expect += "def&34&3.45&04/05/2223 06:07:08" + Environment.NewLine;
        expect += "ghi&56&6.78&01/01/1111 01:01:01" + Environment.NewLine;

        // 検証
        File.ReadAllText(target.FullName).Should().Be(expect);
    }

    [TestMethod]
    public async Task SaveToCsvAsync_Quote()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // 保存データ
        var data = new[]
        {
            new { Text ="a,bc",  Decimal = 0.12, },
            new { Text ="de,f",  Decimal = 3.45, },
            new { Text =",ghi,", Decimal = 6.78, },
        };

        // テスト対象実行
        await data.SaveToCsvAsync(target.FullName);

        // 期待値
        var expect = "";
        expect += "Text,Decimal" + Environment.NewLine;
        expect += "\"a,bc\",0.12" + Environment.NewLine;
        expect += "\"de,f\",3.45" + Environment.NewLine;
        expect += "\",ghi,\",6.78" + Environment.NewLine;

        // 検証
        File.ReadAllText(target.FullName).Should().Be(expect);
    }

    [TestMethod]
    public async Task SaveToCsvAsync_CaptionSelector()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // 保存データ
        var data = new[]
        {
            new { Text ="abc", Number = 12, Decimal = 0.12, Time = new DateTime(2222, 3, 4, 5, 6, 7, 8), },
            new { Text ="def", Number = 34, Decimal = 3.45, Time = new DateTime(2223, 4, 5, 6, 7, 8, 9), },
            new { Text ="ghi", Number = 56, Decimal = 6.78, Time = new DateTime(1111, 1, 1, 1, 1, 1, 1), },
        };

        // 実行オプション
        var options = new SaveToCsvOptions()
        {
            CaptionSelector = (m, i) => m.Name.StartsWith("T") ? $"<{m.Name}>" : null,
        };

        // テスト対象実行
        await data.SaveToCsvAsync(target.FullName, options);

        // 期待値
        var expect = "<Text>,Number,Decimal,<Time>";

        // 検証
        File.ReadAllLines(target.FullName).First().Should().Be(expect);
    }

    [TestMethod]
    public async Task SaveToCsvAsync_MemberFilter()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // 保存データ
        var data = new[]
        {
            new { Text ="abc", Number = 12, Decimal = 0.12, Time = new DateTime(2222, 3, 4, 5, 6, 7, 8), },
            new { Text ="def", Number = 34, Decimal = 3.45, Time = new DateTime(2223, 4, 5, 6, 7, 8, 9), },
            new { Text ="ghi", Number = 56, Decimal = 6.78, Time = new DateTime(1111, 1, 1, 1, 1, 1, 1), },
        };

        // 実行オプション
        var options = new SaveToCsvOptions()
        {
            MemberFilter = m => m.Name.StartsWith("T"),
        };

        // テスト対象実行
        await data.SaveToCsvAsync(target.FullName, options);

        // 期待値
        var expect = "";
        expect += "Text,Time" + Environment.NewLine;
        expect += "abc,03/04/2222 05:06:07" + Environment.NewLine;
        expect += "def,04/05/2223 06:07:08" + Environment.NewLine;
        expect += "ghi,01/01/1111 01:01:01" + Environment.NewLine;

        // 検証
        File.ReadAllText(target.FullName).Should().Be(expect);
    }

    [TestMethod]
    public async Task SaveToCsvAsync_EmptyRecords()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // 保存データ
        var data = new[]
        {
            new { Text ="abc", Number = 12, Decimal = 0.12, Time = new DateTime(2222, 3, 4, 5, 6, 7, 8), },
            new { Text ="def", Number = 34, Decimal = 3.45, Time = new DateTime(2223, 4, 5, 6, 7, 8, 9), },
            new { Text ="ghi", Number = 56, Decimal = 6.78, Time = new DateTime(1111, 1, 1, 1, 1, 1, 1), },
        };

        // テスト対象実行
        await data.Take(0).SaveToCsvAsync(target.FullName);

        // 期待値
        var expect = "Text,Number,Decimal,Time" + Environment.NewLine;

        // 検証
        File.ReadAllText(target.FullName).Should().Be(expect);
    }

    class TestItem
    {
        public string? PropRef { get; set; }
        public int PropVal { get; set; }
        public string? FieldRef;
        public int FieldVal;
    }

    [TestMethod]
    public async Task SaveToCsvAsync_NoField()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // 保存データ
        var data = new[]
        {
            new TestItem{ PropRef = "a1", PropVal = 11, FieldRef = "b1", FieldVal = 101, },
            new TestItem{ PropRef = "a2", PropVal = 12, FieldRef = "b2", FieldVal = 102, },
        };

        // 実行オプション
        var options = new SaveToCsvOptions()
        {
            IncludeFields = false,
        };

        // テスト対象実行
        await data.SaveToCsvAsync(target.FullName, options);

        // 期待値
        var expect = "";
        expect += "PropRef,PropVal" + Environment.NewLine;
        expect += "a1,11" + Environment.NewLine;
        expect += "a2,12" + Environment.NewLine;

        // 検証
        File.ReadAllText(target.FullName).Should().Be(expect);
    }

    [TestMethod]
    public async Task SaveToCsvAsync_IncludeField()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // 保存データ
        var data = new[]
        {
            new TestItem{ PropRef = "a1", PropVal = 11, FieldRef = "b1", FieldVal = 101, },
            new TestItem{ PropRef = "a2", PropVal = 12, FieldRef = "b2", FieldVal = 102, },
        };

        // 実行オプション
        var options = new SaveToCsvOptions()
        {
            IncludeFields = true,
        };

        // テスト対象実行
        await data.SaveToCsvAsync(target.FullName, options);

        // 期待値
        var expect = "";
        expect += "FieldRef,FieldVal,PropRef,PropVal" + Environment.NewLine;
        expect += "b1,101,a1,11" + Environment.NewLine;
        expect += "b2,102,a2,12" + Environment.NewLine;

        // 検証
        File.ReadAllText(target.FullName).Should().Be(expect);
    }

    class ColOrderItem
    {
        [Display(Order = 5)] public string? Prop1 { get; set; }
        [Display()] public int Prop2 { get; set; }
        public int Prop3 { get; set; }
        [Display(Order = -2)] public string? Field1;
        [Display(Order = 1)] public int Field2;
    }

    [TestMethod]
    public async Task SaveToCsvAsync_DisplayOrder()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // 保存データ
        var data = new[]
        {
            //                5                                      -2             1
            new ColOrderItem{ Prop1 = "a1", Prop2 = 11, Prop3 = 21,  Field1 = "b1", Field2 = 101, },
        };

        // 実行オプション
        var options = new SaveToCsvOptions()
        {
            IncludeFields = true,
            UseCaptionAttribute = true,
        };

        // テスト対象実行
        await data.SaveToCsvAsync(target.FullName, options);

        // 期待値
        var expect = "";
        expect += "Field1,Prop2,Prop3,Field2,Prop1" + Environment.NewLine;
        expect += "b1,11,21,101,a1" + Environment.NewLine;

        // 検証
        File.ReadAllText(target.FullName).Should().Be(expect);
    }

    class ColNameItem
    {
        [Display(Name = "Omega")] public string? Prop1 { get; set; }
        [Display(Name = "Alpha")] public int Prop2 { get; set; }
        public int Prop3 { get; set; }
        [Display()] public string? Field1;
        [Display(Name = "Beta")] public int Field2;
    }

    [TestMethod]
    public async Task SaveToCsvAsync_DisplayName()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // 保存データ
        var data = new[]
        {
            //               Omega         Alpha                                   Beta
            new ColNameItem{ Prop1 = "a1", Prop2 = 11, Prop3 = 21,  Field1 = "b1", Field2 = 101, },
        };

        // 実行オプション
        var options = new SaveToCsvOptions()
        {
            IncludeFields = true,
            UseCaptionAttribute = true,
        };

        // テスト対象実行
        await data.SaveToCsvAsync(target.FullName, options);

        // 期待値
        var expect = "";
        expect += "Field1,Beta,Omega,Alpha,Prop3" + Environment.NewLine;
        expect += "b1,101,a1,11,21" + Environment.NewLine;

        // 検証
        File.ReadAllText(target.FullName).Should().Be(expect);
    }

    [TestMethod]
    public async Task SaveToCsvAsync_SortCaption_CaptionSelector()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // 保存データ
        var data = new[]
        {
            new ColNameItem{ Prop1 = "a1", Prop2 = 11, Prop3 = 21,  Field1 = "b1", Field2 = 101, },
        };

        // 実行オプション
        var options = new SaveToCsvOptions()
        {
            IncludeFields = true,
            UseCaptionAttribute = false,
            SortCaption = true,
            CaptionSelector = (m, i) => m.Name switch
            {
                nameof(ColNameItem.Prop1) => "ccc",
                nameof(ColNameItem.Prop2) => "bbb",
                nameof(ColNameItem.Prop3) => "xxx",
                nameof(ColNameItem.Field1) => null,
                nameof(ColNameItem.Field2) => "aaa",
                _ => null
            }
        };

        // テスト対象実行
        await data.SaveToCsvAsync(target.FullName, options);

        // 期待値
        var expect = "";
        expect += "aaa,bbb,ccc,Field1,xxx" + Environment.NewLine;
        expect += "101,11,a1,b1,21" + Environment.NewLine;

        // 検証
        File.ReadAllText(target.FullName).Should().Be(expect);
    }

    class FieldNameItem
    {
        [Display(Name = "Group1")] public string? Echo { get; set; }
        [Display(Name = "Group1")] public int Alpha { get; set; }
        [Display(Name = "Group2")] public int Charlie { get; set; }
        [Display(Name = "Group2")] public string? Delta;
        [Display(Name = "Group3")] public int Bravo;
    }

    [TestMethod]
    public async Task SaveToCsvAsync_SortMemberName()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // 保存データ
        var data = new[]
        {
            new FieldNameItem{ Echo = "a1", Alpha = 11, Charlie = 21,  Delta = "b1", Bravo = 101, },
        };

        // 実行オプション
        var options = new SaveToCsvOptions()
        {
            IncludeFields = true,
            UseCaptionAttribute = false,
            SortCaption = false,
            SortMemberName = true,
        };

        // テスト対象実行
        await data.SaveToCsvAsync(target.FullName, options);

        // 期待値
        var expect = "";
        expect += "Alpha,Bravo,Charlie,Delta,Echo" + Environment.NewLine;
        expect += "11,101,21,b1,a1" + Environment.NewLine;

        // 検証
        File.ReadAllText(target.FullName).Should().Be(expect);
    }

    [TestMethod]
    public async Task SaveToCsvAsync_SortMemberAndCaption()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // 保存データ
        var data = new[]
        {
            //                 Group1       Group1      Group2         Group2        Group3
            new FieldNameItem{ Echo = "a1", Alpha = 11, Charlie = 21,  Delta = "b1", Bravo = 101, },
        };

        // 実行オプション
        var options = new SaveToCsvOptions()
        {
            IncludeFields = true,
            UseCaptionAttribute = true,
            SortCaption = true,
            SortMemberName = true,
        };

        // テスト対象実行
        await data.SaveToCsvAsync(target.FullName, options);

        // 期待値
        var expect = "";
        expect += "Group1,Group1,Group2,Group2,Group3" + Environment.NewLine;
        expect += "11,a1,21,b1,101" + Environment.NewLine;

        // 検証
        File.ReadAllText(target.FullName).Should().Be(expect);
    }

    [TestMethod]
    public async Task SaveToCsvAsync_AsyncEnum()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.txt");

        // 保存データ
        var data = new[]
        {
            new { Text ="abc", Number = 12, Decimal = 0.12, Time = new DateTime(2222, 3, 4, 5, 6, 7, 8), },
            new { Text ="def", Number = 34, Decimal = 3.45, Time = new DateTime(2223, 4, 5, 6, 7, 8, 9), },
            new { Text ="ghi", Number = 56, Decimal = 6.78, Time = new DateTime(1111, 1, 1, 1, 1, 1, 1), },
        };

        // シーケンスを非同期シーケンスにラップする
        async IAsyncEnumerable<T> asyncEnumerate<T>(IEnumerable<T> source)
        {
            foreach (var elem in source)
            {
                await Task.Delay(1).ConfigureAwait(false);
                yield return elem;
            }
        }

        // テスト対象実行
        await asyncEnumerate(data).SaveToCsvAsync(target.FullName);

        // 期待値
        var expect = "";
        expect += "Text,Number,Decimal,Time" + Environment.NewLine;
        expect += "abc,12,0.12,03/04/2222 05:06:07" + Environment.NewLine;
        expect += "def,34,3.45,04/05/2223 06:07:08" + Environment.NewLine;
        expect += "ghi,56,6.78,01/01/1111 01:01:01" + Environment.NewLine;

        // 検証
        File.ReadAllText(target.FullName).Should().Be(expect);
    }
}
