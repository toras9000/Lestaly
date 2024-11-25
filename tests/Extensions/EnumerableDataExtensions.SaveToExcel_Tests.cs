using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Globalization;
using ClosedXML.Excel;
using Lestaly.Extensions;

namespace LestalyTest.Extensions;

[TestClass()]
public class EnumerableDataExtensions_SaveToExcel_Tests
{
    [TestMethod]
    public void SaveToExcel_Default()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.xlsx");

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
        expect.AddRange(data.Select(d => new object[] { d.Text, d.Number, d.Decimal, d.Time, }));

        // 検証
        using var book = new XLWorkbook(target.FullName);
        var sheet = book.Worksheets.First();
        sheet.GetUsedData().Should().BeEquivalentTo(expect);
    }

    [TestMethod]
    public void SaveToExcel_Primitive()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.xlsx");

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
        sheet.GetUsedData().Should().BeEquivalentTo(expect);
    }

    [TestMethod]
    public void SaveToExcel_Nullable()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.xlsx");

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
        expect.AddRange(data.Select(d => new object?[] { (object?)d.SNum1, (object?)d.UNum1, (object?)d.SNum4, (object?)d.UNum4, }));

        // 検証
        using var book = new XLWorkbook(target.FullName);
        var sheet = book.Worksheets.First();
        sheet.GetUsedData().Should().BeEquivalentTo(expect);
    }

    [TestMethod]
    public void SaveToExcel_Hyperlink()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.xlsx");

        // 保存データ
        var data = Enumerable.Empty<ExcelHyperlink?>()
            .Concat(new[]
            {
                new ExcelHyperlink("https://www.google.com/", "Google", "Google検索"),
                null,
                new ExcelHyperlink(@"C:\Windows\Temp", "Temporary", "テンポラリディレクトリ"),
            })
            .Select(l => new { Link = l, });

        // テスト対象実行
        data.SaveToExcel(target.FullName);

        // 検証
        using var book = new XLWorkbook(target.FullName);
        var sheet = book.Worksheets.First();
        sheet.Row(2).Cell(1).ToObjectValue().Should().Be("Google");
        sheet.Row(2).Cell(1).GetHyperlink().Should().Match<XLHyperlink>(link => link.ExternalAddress.OriginalString == "https://www.google.com/" && link.Tooltip == "Google検索");
        sheet.Row(3).Cell(1).IsEmpty().Should().BeTrue();
        sheet.Row(3).Cell(1).HasHyperlink.Should().BeFalse();
        sheet.Row(4).Cell(1).ToObjectValue().Should().Be("Temporary");
        sheet.Row(4).Cell(1).GetHyperlink().Should().Match<XLHyperlink>(link => link.ExternalAddress.OriginalString == @"C:\Windows\Temp" && link.Tooltip == "テンポラリディレクトリ");
    }

    [TestMethod]
    public void SaveToExcel_Fomula()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.xlsx");

        // 保存データ
        var data = new[]
        {
            new { Value = 12, Fomula = (ExcelFormula?)new ExcelFormula("A2", IsR1C1: false), },
            new { Value = 34, Fomula = (ExcelFormula?)null,    },
            new { Value = 56, Fomula = (ExcelFormula?)new ExcelFormula("R4C1", IsR1C1: true), },
        };

        // テスト対象実行
        data.SaveToExcel(target.FullName);

        // 検証
        using var book = new XLWorkbook(target.FullName);
        var sheet = book.Worksheets.First();
        sheet.Row(2).Cell(2).ToObjectValue().Should().Be(12);
        sheet.Row(3).Cell(2).IsEmpty().Should().BeTrue();
        sheet.Row(4).Cell(2).ToObjectValue().Should().Be(56);
    }

    [TestMethod]
    public void SaveToExcel_Style()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.xlsx");

        // 保存データ
        var data = new[]
        {
            new { Caption = "Color", Style = new ExcelStyle("aaa", BackColor: "red", ForeColor: "#00ff00"), },
            new { Caption = "Font",  Style = new ExcelStyle("bbb", Extra: new(FontSize: 12, Bold: true)), },
            new { Caption = "Font",  Style = new ExcelStyle("ccc", Extra: new(Italic: true, Strike: true, Comment: "コメント")), },
            new { Caption = "Align", Style = new ExcelStyle("D", Extra: new(HorzAlign: "Center", VertAlign: "Justify")), },
            new { Caption = "Format", Style = new ExcelStyle(123, Extra: new(Format: "000000")), },
        };

        // テスト対象実行
        data.SaveToExcel(target.FullName);

        // 検証
        using var book = new XLWorkbook(target.FullName);
        var sheet = book.Worksheets.First();
        var look = sheet.Row(1).Cell(2);

        look = look.CellBelow(1);
        {
            look.ToObjectValue().Should().Be("aaa");
            look.Style.Fill.BackgroundColor.Color.ToArgb().Should().Be(Color.FromArgb(0xFF, 0x00, 0x00).ToArgb());
            look.Style.Font.FontColor.Color.ToArgb().Should().Be(Color.FromArgb(0x00, 0xFF, 0x00).ToArgb());
            look.Style.Font.Bold.Should().BeFalse();
            look.Style.Font.Italic.Should().BeFalse();
            look.Style.Font.Strikethrough.Should().BeFalse();
            look.HasComment.Should().BeFalse();
        }

        look = look.CellBelow(1);
        {
            look.ToObjectValue().Should().Be("bbb");
            look.Style.Fill.BackgroundColor.Color.ToArgb().Should().Be(Color.FromArgb(0x00, 0xFF, 0xFF, 0xFF).ToArgb());
            look.Style.Font.FontSize.Should().Be(12);
            look.Style.Font.FontColor.Color.ToArgb().Should().Be(Color.FromArgb(0x00, 0x00, 0x00).ToArgb());
            look.Style.Font.Bold.Should().BeTrue();
            look.Style.Font.Italic.Should().BeFalse();
            look.Style.Font.Strikethrough.Should().BeFalse();
            look.HasComment.Should().BeFalse();
        }

        look = look.CellBelow(1);
        {
            look.ToObjectValue().Should().Be("ccc");
            look.Style.Fill.BackgroundColor.Color.ToArgb().Should().Be(Color.FromArgb(0x00, 0xFF, 0xFF, 0xFF).ToArgb());
            look.Style.Font.FontColor.Color.ToArgb().Should().Be(Color.FromArgb(0x00, 0x00, 0x00).ToArgb());
            look.Style.Font.Bold.Should().BeFalse();
            look.Style.Font.Italic.Should().BeTrue();
            look.Style.Font.Strikethrough.Should().BeTrue();
            look.HasComment.Should().BeTrue();
            look.GetComment().Text.Should().Be("コメント");
        }

        look = look.CellBelow(1);
        {
            look.ToObjectValue().Should().Be("D");
            look.Style.Alignment.Horizontal.Should().Be(XLAlignmentHorizontalValues.Center);
            look.Style.Alignment.Vertical.Should().Be(XLAlignmentVerticalValues.Justify);
        }

        look = look.CellBelow(1);
        {
            look.GetFormattedString().Should().Be("000123");
        }


    }

    [TestMethod]
    public void SaveToExcel_Style_Dynamic()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.xlsx");

        // フィールドデータ
        var style1 = new ExcelStyle(new ExcelFormula("1+2"), DynamicValue: false);
        var style2 = new ExcelStyle(new ExcelFormula("3+4"), DynamicValue: true);

        // 保存データ
        var data = new[]
        {
            new { Style1 = style1, Style2 = style2, },
        };

        // テスト対象実行
        data.SaveToExcel(target.FullName);

        // 検証
        using var book = new XLWorkbook(target.FullName, new LoadOptions { RecalculateAllFormulas = true, });
        var sheet = book.Worksheets.First();
        sheet.Row(2).Cell(1).ToObjectValue().Should().BeOfType<string>().Which.Contains("ExcelFormula");
        sheet.Row(2).Cell(2).ToObjectValue().Should().Be(7);
    }

    class ExpandItem
    {
        public int Number { get; set; }
        [MaxLength(3)] public ExcelExpand? ExpandAttr { get; set; }
        public ExcelExpand? ExpandNoSpan { get; set; }
        public string? Text { get; set; }
        public ExcelExpand? ExpandDelegate { get; set; }
    }

    [TestMethod]
    public void SaveToExcel_Expand()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.xlsx");

        // 保存データ
        var data = new[]
        {
            new ExpandItem
            {
                Number = 100,
                ExpandAttr = new ExcelExpand(new object[]
                {
                    "xyz",
                    200,
                }),
                ExpandNoSpan = new ExcelExpand(new object[]
                {
                    "AAA",
                }),
                Text = "abc",
                ExpandDelegate = new ExcelExpand(new object[]
                {
                    new DateTime(2222, 3, 4, 5, 6, 7, 8),
                    "BBB",
                }),
            },
        };

        // 実行オプション
        var options = new SaveToExcelOptions()
        {
            ColumnSpanSelector = m => m.Name == nameof(ExpandItem.ExpandDelegate) ? 4 : null,
            UseColumnSpanAttribute = true,
        };

        // テスト対象実行
        data.SaveToExcel(target.FullName, options);

        // 検証
        using var book = new XLWorkbook(target.FullName);
        var sheet = book.Worksheets.First();
        sheet.Row(1).Cells(1, 11).Select(c => c.ToObjectValue()).Should().Equal(new[]
        {
            "Number",
            "ExpandAttr[0]",
            "ExpandAttr[1]",
            "ExpandAttr[2]",
            "ExpandNoSpan",
            "Text",
            "ExpandDelegate[0]",
            "ExpandDelegate[1]",
            "ExpandDelegate[2]",
            "ExpandDelegate[3]",
            null,
        });
        sheet.Row(2).Cells(1, 11).Select(c => c.ToObjectValue()).Should().Equal(new object?[]
        {
            // Number
            100,
            // ExpandAttr
            "xyz",
            200,
            default,
            // ExpandNoSpan
            "AAA",
            // Text
            "abc",
            // ExpandDelegate
            new DateTime(2222, 3, 4, 5, 6, 7, 8),
            "BBB",
            default,
            default,
            // (nothing)
            default,
        });
    }

    [TestMethod]
    public void SaveToExcel_Expand_ColmnOverDrop()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.xlsx");

        // 保存データ
        var data = new[]
        {
            new
            {
                Expand = new ExcelExpand(new object[]
                {
                    "abc",
                    200,
                    "def",
                    300,
                }),
            },
        };

        // 実行オプション：オーバーしたデータをドロップする
        var options = new SaveToExcelOptions()
        {
            ColumnSpanSelector = m => 3,
            DropSpanOver = true,
        };

        // テスト対象実行
        data.SaveToExcel(target.FullName, options);

        // 検証
        using var book = new XLWorkbook(target.FullName);
        var sheet = book.Worksheets.First();
        sheet.Row(1).Cells(1, 4).Select(c => c.ToObjectValue()).Should().Equal(new[]
        {
            "Expand[0]",
            "Expand[1]",
            "Expand[2]",
            null,
        });
        sheet.Row(2).Cells(1, 4).Select(c => c.ToObjectValue()).Should().Equal(new object?[]
        {
            "abc",
            200,
            "def",
            default,
        });
    }

    [TestMethod]
    public void SaveToExcel_Expand_ColmnOverFail()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.xlsx");

        // 保存データ
        var data = new[]
        {
            new
            {
                Expand = new ExcelExpand(new object[]
                {
                    "abc",
                    200,
                    "def",
                    300,
                }),
            },
        };

        // 実行オプション：オーバーしたデータをドロップしない
        var options = new SaveToExcelOptions()
        {
            ColumnSpanSelector = m => 3,
            DropSpanOver = false,
        };

        // テスト対象実行
        new Action(() => data.SaveToExcel(target.FullName, options))
            .Should().Throw<Exception>();

    }

    [TestMethod]
    public void SaveToExcel_Expand_CaptionSelector()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // 保存データ
        var data = new[]
        {
            new { Expand =new ExcelExpand(new object[0]), },
        };

        // テストヘルパメソッド
        void validateExpandCaptions(int span, Func<int, string?> selector, params string[] expects)
        {
            // テスト用のファイル名作成
            var tempFile = tempDir.Info.RelativeFile($"{Guid.NewGuid()}.xlsx");

            // テスト対象実行
            data.SaveToExcel(tempFile.FullName, new SaveToExcelOptions()
            {
                ColumnSpanSelector = m => span,
                CaptionSelector = (m, i) => selector(i),
            });

            // 検証
            using var book = new XLWorkbook(tempFile.FullName);
            var sheet = book.Worksheets.First();
            var actuals = sheet.RowsUsed().First().CellsUsed().Select(c => c.GetText());
            actuals.Should().Equal(expects);
        }

        // テストパターン
        validateExpandCaptions(4,
            i => i switch { 0 => "aaa", 1 => "bbb", 2 => "ccc", 3 => "ddd", _ => "xxx", },
            "aaa", "bbb", "ccc", "ddd"
        );

        validateExpandCaptions(4,
            i => i switch { 0 => "aaa", 3 => "ddd", _ => null, },
            "aaa", "Expand[1]", "Expand[2]", "ddd"
        );

        validateExpandCaptions(4,
            i => i switch { 2 => "aaa", 3 => "ddd", _ => null, },
            "Expand[0]", "Expand[1]", "aaa", "ddd"
        );

        validateExpandCaptions(4,
            i => null,
            "Expand[0]", "Expand[1]", "Expand[2]", "Expand[3]"
        );

    }

    class ExpandCaptionItem
    {
        [MaxLength(4)]
        [Display(Name = "aaa|bbb|ccc|ddd", GroupName = "|")]
        public ExcelExpand? All { get; set; }

        [MaxLength(4)]
        [Display(Name = "eee|||hhh", GroupName = "|")]
        public ExcelExpand? Side { get; set; }

        [MaxLength(7)]
        [Display(Name = "||iii|jjj||", GroupName = "|")]
        public ExcelExpand? Middle { get; set; }

        [MaxLength(3)]
        [Display(Name = "asd|qwe")]
        public ExcelExpand? Single { get; set; }

        [MaxLength(2)]
        [Display()]
        public ExcelExpand? NoName { get; set; }
    }

    [TestMethod]
    public void SaveToExcel_Expand_CaptionAttr()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // 保存データ
        var data = new[]
        {
            new ExpandCaptionItem
            {
                All = new ExcelExpand(new object[0]),
                Side = new ExcelExpand(new object[0]),
                Middle = new ExcelExpand(new object[0]),
                Single = new ExcelExpand(new object[0]),
                NoName = new ExcelExpand(new object[0]),
            },
        };

        // テスト用のファイル名作成
        var tempFile = tempDir.Info.RelativeFile($"{Guid.NewGuid()}.xlsx");

        // テスト対象実行
        data.SaveToExcel(tempFile.FullName, new SaveToExcelOptions()
        {
            UseColumnSpanAttribute = true,
            UseCaptionAttribute = true,
        });

        // 検証
        using var book = new XLWorkbook(tempFile.FullName);
        var sheet = book.Worksheets.First();
        var actuals = sheet.RowsUsed().First().CellsUsed().Select(c => c.GetText());
        actuals.Should().Equal(
            "aaa", "bbb", "ccc", "ddd",
            "eee", "Side[1]", "Side[2]", "hhh",
            "Middle[0]", "Middle[1]", "iii", "jjj", "Middle[4]", "Middle[5]", "Middle[6]",
            "asd|qwe[0]", "asd|qwe[1]", "asd|qwe[2]",
            "NoName[0]", "NoName[1]"
        );
    }

    [TestMethod]
    public void SaveToExcel_Expand_CaptionAttrAndSelector()
    {
        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // 保存データ
        var data = new[]
        {
            new ExpandCaptionItem
            {
                All = new ExcelExpand(new object[0]),
                Side = new ExcelExpand(new object[0]),
                Middle = new ExcelExpand(new object[0]),
                Single = new ExcelExpand(new object[0]),
                NoName = new ExcelExpand(new object[0]),
            },
        };

        // テスト用のファイル名作成
        var tempFile = tempDir.Info.RelativeFile($"{Guid.NewGuid()}.xlsx");

        // テスト対象実行
        data.SaveToExcel(tempFile.FullName, new SaveToExcelOptions()
        {
            UseColumnSpanAttribute = true,
            UseCaptionAttribute = true,
            CaptionSelector = (m, i) => m.Name switch
            {
                nameof(ExpandCaptionItem.All) => i switch { 2 => "zzz", _ => null, },
                nameof(ExpandCaptionItem.Side) => i switch { 1 => "yyy", 3 => "xxx", _ => null, },
                nameof(ExpandCaptionItem.Middle) => i switch { 3 => "www", 6 => "vvv", _ => null, },
                nameof(ExpandCaptionItem.Single) => i switch { 1 => "uuu", _ => null, },
                nameof(ExpandCaptionItem.NoName) => i switch { 0 => "ttt", _ => null, },
                _ => null,
            },
        });

        // 検証
        using var book = new XLWorkbook(tempFile.FullName);
        var sheet = book.Worksheets.First();
        var actuals = sheet.RowsUsed().First().CellsUsed().Select(c => c.GetText());
        actuals.Should().Equal(
            "aaa", "bbb", "zzz", "ddd",
            "eee", "yyy", "Side[2]", "xxx",
            "Middle[0]", "Middle[1]", "iii", "www", "Middle[4]", "Middle[5]", "vvv",
            "asd|qwe[0]", "uuu", "asd|qwe[2]",
            "ttt", "NoName[1]"
        );
    }

    [TestMethod]
    public void SaveToExcel_Min()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.xlsx");

        // 保存データ
        var data = Enumerable.Repeat(new { Value = 1, }, 0);

        // テスト対象実行
        data.SaveToExcel(target.FullName);

        // 期待値
        var expect = new List<object?[]> { new object?[] { "Value", } };

        // 検証
        using var book = new XLWorkbook(target.FullName);
        var sheet = book.Worksheets.First();
        sheet.GetUsedData().Should().BeEquivalentTo(expect);
    }

    [TestMethod]
    public void SaveToExcel_Offset()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.xlsx");

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
        expect.AddRange(data.Select(d => new object[] { d.Text, d.Number, d.Decimal, d.Time, }));

        // 検証
        using var book = new XLWorkbook(target.FullName);
        var sheet = book.Worksheets.First();
        var used = sheet.RangeUsed();
        Assert.IsNotNull(used);
        used.FirstCell().WorksheetRow().RowNumber().Should().Be(1 + 4);
        used.FirstCell().WorksheetColumn().ColumnNumber().Should().Be(1 + 2);
        sheet.GetUsedData().Should().BeEquivalentTo(expect);
    }

    [TestMethod]
    public void SaveToExcel_SheetName()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.xlsx");

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
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.xlsx");

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
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.xlsx");

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
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.xlsx");

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
            CaptionSelector = (m, i) => m.Name.StartsWith("T") ? $"<{m.Name}>" : null,
        };

        // テスト対象実行
        data.SaveToExcel(target.FullName, options);

        // 期待値
        var expect = new List<object?[]> { new object?[] { "<Text>", "Number", } };
        expect.AddRange(data.Select(d => new object?[] { d.Text, d.Number, }));

        // 検証
        using var book = new XLWorkbook(target.FullName);
        var sheet = book.Worksheets.First();
        sheet.GetUsedData().Should().BeEquivalentTo(expect);
    }

    [TestMethod]
    public void SaveToExcel_MemberFilter()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.xlsx");

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
        sheet.GetUsedData().Should().BeEquivalentTo(expect);
    }

    class TestItem
    {
        public string? PropRef { get; set; }
        public int PropVal { get; set; }
        public string? FieldRef;
        public int FieldVal;
    }

    [TestMethod]
    public void SaveToExcel_Compiled()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.xlsx");

        // 保存データ
        var data = new[]
        {
            new TestItem{ PropRef = "a1", PropVal = 11, FieldRef = "b1", FieldVal = 101, },
            new TestItem{ PropRef = "a2", PropVal = 12, FieldRef = "b2", FieldVal = 102, },
        };

        // 実行オプション
        var options = new SaveToExcelOptions()
        {
            UseCompiledGetter = false,
        };

        // テスト対象実行
        data.SaveToExcel(target.FullName, options);

        // 期待値
        var expect = new List<object?[]> { new object?[] { "PropRef", "PropVal", } };
        expect.AddRange(data.Select(d => new object?[] { d.PropRef, d.PropVal, }));

        // 検証
        using var book = new XLWorkbook(target.FullName);
        var sheet = book.Worksheets.First();
        sheet.GetUsedData().Should().BeEquivalentTo(expect);
    }

    [TestMethod]
    public void SaveToExcel_NoField()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.xlsx");

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
        sheet.GetUsedData().Should().BeEquivalentTo(expect);
    }

    [TestMethod]
    public void SaveToExcel_IncludeField()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.xlsx");

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
        sheet.GetUsedData().Should().BeEquivalentTo(expect);
    }

    [TestMethod]
    public void SaveToExcel_IncludeFieldCompiled()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.xlsx");

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
            UseCompiledGetter = true,
        };

        // テスト対象実行
        data.SaveToExcel(target.FullName, options);

        // 期待値
        var expect = new List<object?[]> { new object?[] { "FieldRef", "FieldVal", "PropRef", "PropVal", } };
        expect.AddRange(data.Select(d => new object?[] { d.FieldRef, d.FieldVal, d.PropRef, d.PropVal, }));

        // 検証
        using var book = new XLWorkbook(target.FullName);
        var sheet = book.Worksheets.First();
        sheet.GetUsedData().Should().BeEquivalentTo(expect);
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
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.xlsx");

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
            UseCaptionAttribute = true,
        };

        // テスト対象実行
        data.SaveToExcel(target.FullName, options);

        // 期待値
        var expect = new List<object?[]> { new object?[] { "Field1", "Prop2", "Prop3", "Field2", "Prop1", } };
        expect.AddRange(data.Select(d => new object?[] { d.Field1, d.Prop2, d.Prop3, d.Field2, d.Prop1, }));

        // 検証
        using var book = new XLWorkbook(target.FullName);
        var sheet = book.Worksheets.First();
        sheet.GetUsedData().Should().BeEquivalentTo(expect);
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
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.xlsx");

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
            UseCaptionAttribute = true,
        };

        // テスト対象実行
        data.SaveToExcel(target.FullName, options);

        // 期待値
        var expect = new List<object?[]> { new object?[] { "Field1", "Beta", "Omega", "Alpha", "Prop3", } };
        expect.AddRange(data.Select(d => new object?[] { d.Field1, d.Field2, d.Prop1, d.Prop2, d.Prop3, }));

        // 検証
        using var book = new XLWorkbook(target.FullName);
        var sheet = book.Worksheets.First();
        sheet.GetUsedData().Should().BeEquivalentTo(expect);
    }

    [TestMethod]
    public void SaveToExcel_SortCaption_CaptionSelector()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.xlsx");

        // 保存データ
        var data = new[]
        {
            new ColNameItem{ Prop1 = "a1", Prop2 = 11, Prop3 = 21,  Field1 = "b1", Field2 = 101, },
        };

        // 実行オプション
        var options = new SaveToExcelOptions()
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
        data.SaveToExcel(target.FullName, options);

        // 期待値
        var expect = new List<object?[]> { new object?[] { "aaa", "bbb", "ccc", "Field1", "xxx", } };
        expect.AddRange(data.Select(d => new object?[] { d.Field2, d.Prop2, d.Prop1, d.Field1, d.Prop3, }));

        // 検証
        using var book = new XLWorkbook(target.FullName);
        var sheet = book.Worksheets.First();
        sheet.GetUsedData().Should().BeEquivalentTo(expect);
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
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.xlsx");

        // 保存データ
        var data = new[]
        {
            new FieldNameItem{ Echo = "a1", Alpha = 11, Charlie = 21,  Delta = "b1", Bravo = 101, },
        };

        // 実行オプション
        var options = new SaveToExcelOptions()
        {
            IncludeFields = true,
            UseCaptionAttribute = false,
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
        sheet.GetUsedData().Should().BeEquivalentTo(expect);
    }

    [TestMethod]
    public async Task SaveToExcelAsync_AsyncEnum()
    {
        using var localized = new CulturePeriod(CultureInfo.InvariantCulture);

        // テスト用に一時ディレクトリ
        using var tempDir = new TempDir();

        // テストファイル
        var target = tempDir.Info.RelativeFile("test.xlsx");

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
        sheet.GetUsedData().Should().BeEquivalentTo(expect);
    }

    [TestMethod]
    public void SaveToExcelOptions_DataTypes()
    {
        var hyperlink = new ExcelHyperlink("https://google.com", "disptext", "tooltip");
        hyperlink.ToString().Should().Be("disptext|https://google.com");

        var formula = new ExcelFormula("A1 & A2", false);
        formula.ToString().Should().Be("A1 & A2");

        var style = new ExcelStyle(123, "backcolor", "forecolor");
        style.ToString().Should().Be("123");

        var expand = new ExcelExpand(new object[] { "aaa", 100, "bbb", 200 });
        expand.ToString().Should().Be("aaa|100|bbb|200");
    }

}
