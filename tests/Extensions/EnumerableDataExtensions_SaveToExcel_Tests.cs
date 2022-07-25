using System.Globalization;
using ClosedXML.Excel;
using FluentAssertions;
using LestalyTest._Test;
using TestCometFlavor._Test;

namespace LestalyTest.Extensions;

[TestClass()]
public class EnumerableDataExtensions_SaveToExcel_Tests
{
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
        sheet.GetRangeData(row: 1, col: 1, width: 4, height: 4).Should().BeEquivalentTo(expect);
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
            new { Number = 1, Nullable = new int?(9),   },
            new { Number = 2, Nullable = default(int?), },
            new { Number = 3, Nullable = new int?(7),   },
        };

        // テスト対象実行
        data.SaveToExcel(target.FullName);

        // 期待値
        var expect = new List<object?[]> { new object?[] { "Number", "Nullable", } };
        expect.AddRange(data.Select(d => new object?[] { d.Number, (object?)d.Nullable ?? "", }));

        // 検証
        using var book = new XLWorkbook(target.FullName);
        var sheet = book.Worksheets.First();
        sheet.GetRangeData(row: 1, col: 1, width: 2, height: 4).Should().BeEquivalentTo(expect);
    }
}
