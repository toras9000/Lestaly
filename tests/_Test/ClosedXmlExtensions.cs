using System.Data;
using ClosedXML.Excel;
using Lestaly.Extensions;

namespace LestalyTest;

public static class ClosedXmlExtensions
{
    public static object?[][] GetRangeData(this IXLWorksheet self, int row, int col, int width, int height)
    {
        var range = self.Range(row, col, row + height - 1, col + width - 1);
        return range.Rows()
            .Select(row => row.Cells().Select(c => c.ToObjectValue()).ToArray())
            .ToArray();
    }

    public static string[][] GetRangeStrings(this IXLWorksheet self, int row, int col, int width, int height)
    {
        var range = self.Range(row, col, row + height - 1, col + width - 1);
        return range.Rows()
            .Select(row => row.Cells().Select(cell => cell.GetFormattedString()).ToArray())
            .ToArray();
    }

    public static object?[][] GetUsedData(this IXLWorksheet self)
    {
        var range = self.RangeUsed();
        return range.Rows()
            .Select(row => row.Cells().Select(c => c.ToObjectValue()).ToArray())
            .ToArray();
    }

}
