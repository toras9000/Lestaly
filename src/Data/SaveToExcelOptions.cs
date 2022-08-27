namespace Lestaly;

/// <summary>
/// Excelテキスト保存オプション
/// </summary>
public class SaveToExcelOptions : TypeColumnOptions
{
    /// <summary>シート名</summary>
    public string? Sheet { get; set; } = null;

    /// <summary>出力オフセット行数</summary>
    public int RowOffset { get; set; } = 0;

    /// <summary>出力オフセット列数</summary>
    public int ColumnOffset { get; set; } = 0;

    /// <summary>出力範囲をテーブル定義するか否か</summary>
    public bool TableDefine { get; set; } = true;

    /// <summary>テーブル定義時のテーブル名</summary>
    public string? TableName { get; set; } = null;

    /// <summary>出力範囲にオートフィルタを設定するか否か。テーブル定義設定が無効の場合に利用可能。</summary>
    public bool AutoFilter { get; set; } = true;

    /// <summary>出力範囲のカラム幅を調整するか否か</summary>
    public bool AdjustToContents { get; set; } = true;

    /// <summary>型による自動リンク設定を有効とするか否か。trueの場合UriおよびFileInfo/DirectoryInfo型の列をリンク設定する。</summary>
    public bool AutoLink { get; set; } = false;
}

/// <summary>リンクデータを表すデータ型</summary>
/// <param name="Target">リンク先</param>
/// <param name="Display">表示名</param>
/// <param name="Tooltip">ツールチップ</param>
public record ExcelHyperlink(string Target, string? Display = null, string? Tooltip = null);

/// <summary>式を表すデータ型</summary>
/// <param name="Expression">式</param>
/// <param name="IsR1C1">式</param>
public record ExcelFormula(string Expression, bool IsR1C1 = false);
