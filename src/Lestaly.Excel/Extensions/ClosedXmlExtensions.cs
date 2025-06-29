using ClosedXML.Excel;

namespace Lestaly.Extensions;

/// <summary>
/// ClosedXMLに関する拡張メソッド
/// </summary>
public static class ClosedXmlExtensions
{
    /// <summary>データ型に応じたセル値を object 型の値として取得する。</summary>
    /// <remarks>
    /// このメソッドは ClosedXML の内部メソッド XLCellValue.ToObject() を元にしている。
    /// 強い型付けをして値を持っていても、object での値の取り出しは別に防ぐ必要はないと思うため、拡張メソッドで定義した。
    /// </remarks>
    /// <param name="self">対象となるセル値</param>
    /// <param name="numeric">日時関係の値を整数値として取得するか否か。</param>
    /// <returns>取得した値</returns>
    public static object? ToBox(this XLCellValue self, bool numeric = false)
        => self.Type switch
        {
            XLDataType.Blank => null,
            XLDataType.Boolean => self.GetBoolean(),
            XLDataType.Number => self.GetNumber(),
            XLDataType.Text => self.GetText(),
            XLDataType.DateTime => numeric ? self.GetUnifiedNumber() : self.GetDateTime(),
            XLDataType.TimeSpan => numeric ? self.GetUnifiedNumber() : self.GetTimeSpan(),
            XLDataType.Error => self.GetError(),
            _ => throw new InvalidCastException(),
        };

    /// <summary>データ型に応じたセル値を object 型の値として取得する。</summary>
    /// <param name="self">対象となるセル</param>
    /// <param name="numeric">日時関係の値を整数値として取得するか否か。</param>
    /// <returns>取得した値</returns>
    public static object? ToObjectValue(this IXLCell self, bool numeric = false)
        => self.Value.ToBox(numeric);
}
