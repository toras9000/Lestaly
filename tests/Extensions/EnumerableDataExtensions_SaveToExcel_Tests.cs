using System.ComponentModel.DataAnnotations;
using System.Globalization;
using ClosedXML.Excel;
using FluentAssertions;
using LestalyTest._Test;
using TestCometFlavor._Test;

namespace LestalyTest.Extensions;

[TestClass()]
public class EnumerableDataExtensions_SaveToExcel_Tests
{
    private (int width, int height) detectDataArea<T>(IEnumerable<T> data, bool withField = false)
    {
        var width = typeof(T).GetProperties().Length;
        if (withField) width += typeof(T).GetFields().Length;
        var height = data.Count();
        return (width, height);
    }

    [TestMethod]
    public void SaveToExcel_Default()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDirectory();

        // テストファイル
        var target = tempDir.Info.GetRelativeFile("test.xlsx");

        // 保存データ
        var data = new[]
        {
            new { Text ="abc", Number = 12, Decimal = 0.12, Time = new DateTime(2222, 3, 4, 5, 6, 7, 8), },
            new { Text ="def", Number = 34, Decimal = 3.45, Time = new DateTime(2223, 4, 5, 6, 7, 8, 9), },
            new { Text ="ghi", Number = 56, Decimal = 6.78, Time = new DateTime(1111, 1, 1, 1, 1, 1, 1), },
        };

        // テスト対象実行
        data.SaveToExcel(target.FullName);

        // 期待値
        var expect = new List<object[]> { new object[] { "Text", "Number", "Decimal", "Time", } };
        expect.AddRange(data.Select(d => new object[] { d.Text, d.Number, d.Decimal, d.Time.Year < 1900 ? d.Time.ToString() : d.Time, }));

        // 検証
        using var book = new XLWorkbook(target.FullName);
        var sheet = book.Worksheets.First();
        var area = detectDataArea(data);
        sheet.GetRangeData(row: 1, col: 1, area.width, 1 + area.height).Should().BeEquivalentTo(expect);
    }

    [TestMethod]
    public void SaveToExcel_Primitive()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDirectory();

        // テストファイル
        var target = tempDir.Info.GetRelativeFile("test.xlsx");

        // 保存データ
        var data = new[]
        {
            new { SNum1 = (sbyte)1,       UNum1 = (byte)2,       SNum4 = (int)3,       UNum4 = (uint)4,       },
            new { SNum1 = sbyte.MaxValue, UNum1 = byte.MaxValue, SNum4 = int.MaxValue, UNum4 = uint.MaxValue, },
            new { SNum1 = sbyte.MinValue, UNum1 = byte.MinValue, SNum4 = int.MinValue, UNum4 = uint.MinValue, },
        };

        // テスト対象実行
        data.SaveToExcel(target.FullName);

        // 期待値
        var expect = new List<object?[]> { new object?[] { "SNum1", "UNum1", "SNum4", "UNum4", } };
        expect.AddRange(data.Select(d => new object?[] { d.SNum1, d.UNum1, d.SNum4, d.UNum4, }));

        // 検証
        using var book = new XLWorkbook(target.FullName);
        var sheet = book.Worksheets.First();
        var area = detectDataArea(data);
        sheet.GetRangeData(row: 1, col: 1, area.width, 1 + area.height).Should().BeEquivalentTo(expect);
    }

    [TestMethod]
    public void SaveToExcel_Nullable()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDirectory();

        // テストファイル
        var target = tempDir.Info.GetRelativeFile("test.xlsx");

        // 保存データ
        var data = new[]
            {
                new { SNum1 = (sbyte?)1,              UNum1 = (byte?)2,             SNum4 = (int?)3,            UNum4 = (uint?)4,             },
                new { SNum1 = (sbyte?)null,           UNum1 = (byte?)null,          SNum4 = (int?)null,         UNum4 = (uint?)null,          },
                new { SNum1 = (sbyte?)sbyte.MaxValue, UNum1 = (byte?)byte.MaxValue, SNum4 = (int?)int.MaxValue, UNum4 = (uint?)uint.MaxValue, },
                new { SNum1 = (sbyte?)sbyte.MinValue, UNum1 = (byte?)byte.MinValue, SNum4 = (int?)int.MinValue, UNum4 = (uint?)uint.MinValue, },
            };

        // テスト対象実行
        data.SaveToExcel(target.FullName);

        // 期待値
        var expect = new List<object?[]> { new object?[] { "SNum1", "UNum1", "SNum4", "UNum4", } };
        expect.AddRange(data.Select(d => new object?[] { (object?)d.SNum1 ?? "", (object?)d.UNum1 ?? "", (object?)d.SNum4 ?? "", (object?)d.UNum4 ?? "", }));

        // 検証
        using var book = new XLWorkbook(target.FullName);
        var sheet = book.Worksheets.First();
        var area = detectDataArea(data);
        sheet.GetRangeData(row: 1, col: 1, area.width, 1 + area.height).Should().BeEquivalentTo(expect);
    }

    [TestMethod]
    public void SaveToExcel_Min()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDirectory();

        // テストファイル
        var target = tempDir.Info.GetRelativeFile("test.xlsx");

        // 保存データ
        var data = Enumerable.Repeat(new { Value = 1, }, 0);

        // テスト対象実行
        data.SaveToExcel(target.FullName);

        // 期待値
        var expect = new List<object?[]> { new object?[] { "Value", } };

        // 検証
        using var book = new XLWorkbook(target.FullName);
        var sheet = book.Worksheets.First();
        var area = detectDataArea(data);
        sheet.GetRangeData(row: 1, col: 1, area.width, 1 + area.height).Should().BeEquivalentTo(expect);
    }

    [TestMethod]
    public void SaveToExcel_Offset()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDirectory();

        // テストファイル
        var target = tempDir.Info.GetRelativeFile("test.xlsx");

        // 保存データ
        var data = new[]
        {
            new { Text ="abc", Number = 12, Decimal = 0.12, Time = new DateTime(2222, 3, 4, 5, 6, 7, 8), },
            new { Text ="def", Number = 34, Decimal = 3.45, Time = new DateTime(2223, 4, 5, 6, 7, 8, 9), },
            new { Text ="ghi", Number = 56, Decimal = 6.78, Time = new DateTime(1111, 1, 1, 1, 1, 1, 1), },
        };

        // 実行オプション
        var options = new SaveToExcelOptions()
        {
            RowOffset = 4,
            ColumnOffset = 2,
        };

        // テスト対象実行
        data.SaveToExcel(target.FullName, options);

        // 期待値
        var expect = new List<object[]> { new object[] { "Text", "Number", "Decimal", "Time", } };
        expect.AddRange(data.Select(d => new object[] { d.Text, d.Number, d.Decimal, d.Time.Year < 1900 ? d.Time.ToString() : d.Time, }));

        // 検証
        using var book = new XLWorkbook(target.FullName);
        var sheet = book.Worksheets.First();
        var area = detectDataArea(data);
        sheet.GetRangeData(row: 1 + 4, col: 1 + 2, area.width, 1 + area.height).Should().BeEquivalentTo(expect);
    }

    [TestMethod]
    public void SaveToExcel_SheetName()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDirectory();

        // テストファイル
        var target = tempDir.Info.GetRelativeFile("test.xlsx");

        // 保存データ
        var data = new[]
        {
            new { Text ="abc", Number = 12, Decimal = 0.12, Time = new DateTime(2222, 3, 4, 5, 6, 7, 8), },
            new { Text ="def", Number = 34, Decimal = 3.45, Time = new DateTime(2223, 4, 5, 6, 7, 8, 9), },
            new { Text ="ghi", Number = 56, Decimal = 6.78, Time = new DateTime(1111, 1, 1, 1, 1, 1, 1), },
        };

        // 実行オプション
        var options = new SaveToExcelOptions()
        {
            Sheet = "abc",
        };

        // テスト対象実行
        data.SaveToExcel(target.FullName, options);

        // 検証
        using var book = new XLWorkbook(target.FullName);
        var sheet = book.Worksheets.First();
        sheet.Name.Should().Be("abc");
    }

    [TestMethod]
    public void SaveToExcel_TableName()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDirectory();

        // テストファイル
        var target = tempDir.Info.GetRelativeFile("test.xlsx");

        // 保存データ
        var data = new[]
        {
            new { Text ="abc", Number = 12, Decimal = 0.12, Time = new DateTime(2222, 3, 4, 5, 6, 7, 8), },
            new { Text ="def", Number = 34, Decimal = 3.45, Time = new DateTime(2223, 4, 5, 6, 7, 8, 9), },
            new { Text ="ghi", Number = 56, Decimal = 6.78, Time = new DateTime(1111, 1, 1, 1, 1, 1, 1), },
        };

        // 実行オプション
        var options = new SaveToExcelOptions()
        {
            TableName = "xyz",
        };

        // テスト対象実行
        data.SaveToExcel(target.FullName, options);

        // 検証
        using var book = new XLWorkbook(target.FullName);
        var sheet = book.Worksheets.First();
        sheet.Tables.First().Name.Should().Be("xyz");
    }

    [TestMethod]
    public void SaveToExcel_AutoLink()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDirectory();

        // テストファイル
        var target = tempDir.Info.GetRelativeFile("test.xlsx");

        // 保存データ
        var data = new[]
        {
            new { Text ="abc", File = target, Url = new Uri(@"https://example.com/"), },
        };

        // 実行オプション
        var options = new SaveToExcelOptions()
        {
            AutoLink = true,
        };

        // テスト対象実行
        data.SaveToExcel(target.FullName, options);

        // 検証
        using var book = new XLWorkbook(target.FullName);
        var sheet = book.Worksheets.First();
        var fileLink = sheet.Cell(row: 2, column: 2).GetHyperlink();
        fileLink.ExternalAddress.Should().Be(new Uri(target.FullName));
        fileLink.Cell.GetFormattedString().Should().Be(target.FullName);
        var urlLink = sheet.Cell(row: 2, column: 3).GetHyperlink();
        urlLink.ExternalAddress.Should().Be(new Uri(@"https://example.com/"));
        urlLink.Cell.GetFormattedString().Should().Be(@"https://example.com/");
    }

    [TestMethod]
    public void SaveToExcel_CaptionSelector()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDirectory();

        // テストファイル
        var target = tempDir.Info.GetRelativeFile("test.xlsx");

        // 保存データ
        var data = new[]
        {
            new { Text ="abc", Number = 12, },
            new { Text ="def", Number = 34, },
            new { Text ="ghi", Number = 56, },
        };

        // 実行オプション
        var options = new SaveToExcelOptions()
        {
            CaptionSelector = m => m.Name.StartsWith("T") ? $"<{m.Name}>" : null,
        };

        // テスト対象実行
        data.SaveToExcel(target.FullName, options);

        // 期待値
        var expect = new List<object?[]> { new object?[] { "<Text>", "Number", } };
        expect.AddRange(data.Select(d => new object?[] { d.Text, d.Number, }));

        // 検証
        using var book = new XLWorkbook(target.FullName);
        var sheet = book.Worksheets.First();
        var area = detectDataArea(data);
        sheet.GetRangeData(row: 1, col: 1, area.width, 1 + area.height).Should().BeEquivalentTo(expect);
    }

    [TestMethod]
    public void SaveToExcel_MemberFilter()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDirectory();

        // テストファイル
        var target = tempDir.Info.GetRelativeFile("test.xlsx");

        // 保存データ
        var data = new[]
        {
            new { Text ="abc", Number = 12, },
            new { Text ="def", Number = 34, },
            new { Text ="ghi", Number = 56, },
        };

        // 実行オプション
        var options = new SaveToExcelOptions()
        {
            MemberFilter = m => m.Name.StartsWith("T"),
        };

        // テスト対象実行
        data.SaveToExcel(target.FullName, options);

        // 期待値
        var expect = new List<object?[]> { new object?[] { "Text", } };
        expect.AddRange(data.Select(d => new object?[] { d.Text, }));

        // 検証
        using var book = new XLWorkbook(target.FullName);
        var sheet = book.Worksheets.First();
        var area = detectDataArea(data);
        sheet.GetRangeData(row: 1, col: 1, 1, 1 + area.height).Should().BeEquivalentTo(expect);
    }

    class TestItem
    {
        public string? PropRef { get; set; }
        public int PropVal { get; set; }
        public string? FieldRef;
        public int FieldVal;
    }

    [TestMethod]
    public void SaveToExcel_NoField()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDirectory();

        // テストファイル
        var target = tempDir.Info.GetRelativeFile("test.xlsx");

        // 保存データ
        var data = new[]
        {
            new TestItem{ PropRef = "a1", PropVal = 11, FieldRef = "b1", FieldVal = 101, },
            new TestItem{ PropRef = "a2", PropVal = 12, FieldRef = "b2", FieldVal = 102, },
        };

        // 実行オプション
        var options = new SaveToExcelOptions()
        {
            IncludeFields = false,
        };

        // テスト対象実行
        data.SaveToExcel(target.FullName, options);

        // 期待値
        var expect = new List<object?[]> { new object?[] { "PropRef", "PropVal", } };
        expect.AddRange(data.Select(d => new object?[] { d.PropRef, d.PropVal, }));

        // 検証
        using var book = new XLWorkbook(target.FullName);
        var sheet = book.Worksheets.First();
        var area = detectDataArea(data);
        sheet.GetRangeData(row: 1, col: 1, area.width, 1 + area.height).Should().BeEquivalentTo(expect);
    }

    [TestMethod]
    public void SaveToExcel_IncludeField()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDirectory();

        // テストファイル
        var target = tempDir.Info.GetRelativeFile("test.xlsx");

        // 保存データ
        var data = new[]
        {
            new TestItem{ PropRef = "a1", PropVal = 11, FieldRef = "b1", FieldVal = 101, },
            new TestItem{ PropRef = "a2", PropVal = 12, FieldRef = "b2", FieldVal = 102, },
        };

        // 実行オプション
        var options = new SaveToExcelOptions()
        {
            IncludeFields = true,
        };

        // テスト対象実行
        data.SaveToExcel(target.FullName, options);

        // 期待値
        var expect = new List<object?[]> { new object?[] { "FieldRef", "FieldVal", "PropRef", "PropVal", } };
        expect.AddRange(data.Select(d => new object?[] { d.FieldRef, d.FieldVal, d.PropRef, d.PropVal, }));

        // 検証
        using var book = new XLWorkbook(target.FullName);
        var sheet = book.Worksheets.First();
        var area = detectDataArea(data, withField: true);
        sheet.GetRangeData(row: 1, col: 1, area.width, 1 + area.height).Should().BeEquivalentTo(expect);
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
    public void SaveToExcel_DisplayOrder()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDirectory();

        // テストファイル
        var target = tempDir.Info.GetRelativeFile("test.xlsx");

        // 保存データ
        var data = new[]
        {
            //                5                                      -2             1
            new ColOrderItem{ Prop1 = "a1", Prop2 = 11, Prop3 = 21,  Field1 = "b1", Field2 = 101, },
        };

        // 実行オプション
        var options = new SaveToExcelOptions()
        {
            IncludeFields = true,
            UseDisplayAttribute = true,
        };

        // テスト対象実行
        data.SaveToExcel(target.FullName, options);

        // 期待値
        var expect = new List<object?[]> { new object?[] { "Field1", "Prop2", "Prop3", "Field2", "Prop1", } };
        expect.AddRange(data.Select(d => new object?[] { d.Field1, d.Prop2, d.Prop3, d.Field2, d.Prop1, }));

        // 検証
        using var book = new XLWorkbook(target.FullName);
        var sheet = book.Worksheets.First();
        var area = detectDataArea(data, withField: true);
        sheet.GetRangeData(row: 1, col: 1, area.width, 1 + area.height).Should().BeEquivalentTo(expect);
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
    public void SaveToExcel_DisplayName()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDirectory();

        // テストファイル
        var target = tempDir.Info.GetRelativeFile("test.xlsx");

        // 保存データ
        var data = new[]
        {
            //               Omega         Alpha                                   Beta
            new ColNameItem{ Prop1 = "a1", Prop2 = 11, Prop3 = 21,  Field1 = "b1", Field2 = 101, },
        };

        // 実行オプション
        var options = new SaveToExcelOptions()
        {
            IncludeFields = true,
            UseDisplayAttribute = true,
        };

        // テスト対象実行
        data.SaveToExcel(target.FullName, options);

        // 期待値
        var expect = new List<object?[]> { new object?[] { "Field1", "Beta", "Omega", "Alpha", "Prop3", } };
        expect.AddRange(data.Select(d => new object?[] { d.Field1, d.Field2, d.Prop1, d.Prop2, d.Prop3, }));

        // 検証
        using var book = new XLWorkbook(target.FullName);
        var sheet = book.Worksheets.First();
        var area = detectDataArea(data, withField: true);
        sheet.GetRangeData(row: 1, col: 1, area.width, 1 + area.height).Should().BeEquivalentTo(expect);
    }

    [TestMethod]
    public void SaveToExcel_SortCaption_CaptionSelector()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDirectory();

        // テストファイル
        var target = tempDir.Info.GetRelativeFile("test.xlsx");

        // 保存データ
        var data = new[]
        {
            new ColNameItem{ Prop1 = "a1", Prop2 = 11, Prop3 = 21,  Field1 = "b1", Field2 = 101, },
        };

        // 実行オプション
        var options = new SaveToExcelOptions()
        {
            IncludeFields = true,
            UseDisplayAttribute = false,
            SortCaption = true,
            CaptionSelector = m => m.Name switch
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
        data.SaveToExcel(target.FullName, options);

        // 期待値
        var expect = new List<object?[]> { new object?[] { "aaa", "bbb", "ccc", "Field1", "xxx", } };
        expect.AddRange(data.Select(d => new object?[] { d.Field2, d.Prop2, d.Prop1, d.Field1, d.Prop3, }));

        // 検証
        using var book = new XLWorkbook(target.FullName);
        var sheet = book.Worksheets.First();
        var area = detectDataArea(data, withField: true);
        sheet.GetRangeData(row: 1, col: 1, area.width, 1 + area.height).Should().BeEquivalentTo(expect);
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
    public void SaveToExcel_SortMemberName()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDirectory();

        // テストファイル
        var target = tempDir.Info.GetRelativeFile("test.xlsx");

        // 保存データ
        var data = new[]
        {
            new FieldNameItem{ Echo = "a1", Alpha = 11, Charlie = 21,  Delta = "b1", Bravo = 101, },
        };

        // 実行オプション
        var options = new SaveToExcelOptions()
        {
            IncludeFields = true,
            UseDisplayAttribute = false,
            SortCaption = false,
            SortMemberName = true,
        };

        // テスト対象実行
        data.SaveToExcel(target.FullName, options);

        // 期待値
        var expect = new List<object[]> { new object[] { "Alpha", "Bravo", "Charlie", "Delta", "Echo", } };
        expect.AddRange(data.Select(d => new object[] { d.Alpha, d.Bravo, d.Charlie, d.Delta ?? "", d.Echo ?? "", }));

        // 検証
        using var book = new XLWorkbook(target.FullName);
        var sheet = book.Worksheets.First();
        var area = detectDataArea(data, withField: true);
        sheet.GetRangeData(row: 1, col: 1, area.width, 1 + area.height).Should().BeEquivalentTo(expect);
    }

    [TestMethod]
    public async Task SaveToExcelAsync_AsyncEnum()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDirectory();

        // テストファイル
        var target = tempDir.Info.GetRelativeFile("test.xlsx");

        // 保存データ
        var data = new[]
        {
            new { Text ="abc", Number = 12, Real = 0.12, },
            new { Text ="def", Number = 34, Real = 3.45, },
            new { Text ="ghi", Number = 56, Real = 6.78, },
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
        await asyncEnumerate(data).SaveToExcelAsync(target.FullName);

        // 期待値
        var expect = new List<object[]> { new object[] { "Text", "Number", "Real", } };
        expect.AddRange(data.Select(d => new object[] { d.Text, d.Number, d.Real, }));

        // 検証
        using var book = new XLWorkbook(target.FullName);
        var sheet = book.Worksheets.First();
        var area = detectDataArea(data, withField: true);
        sheet.GetRangeData(row: 1, col: 1, area.width, 1 + area.height).Should().BeEquivalentTo(expect);
    }


}
