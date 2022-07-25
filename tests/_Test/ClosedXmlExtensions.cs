using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;

namespace LestalyTest._Test;

public static class ClosedXmlExtensions
{
    public static object[][] GetRangeData(this IXLWorksheet self, int row, int col, int width, int height)
    {
        var range = self.Range(row, col, row + height - 1, col + width - 1);
        return range.Rows()
            .Select(row => row.Cells().Select(cell => cell.Value).ToArray())
            .ToArray();
    }

    public static string[][] GetRangeStrings(this IXLWorksheet self, int row, int col, int width, int height)
    {
        var range = self.Range(row, col, row + height - 1, col + width - 1);
        return range.Rows()
            .Select(row => row.Cells().Select(cell => cell.GetFormattedString()).ToArray())
            .ToArray();
    }
}
