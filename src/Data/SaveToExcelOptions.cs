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

    /// <summary>型による自動リンク設定を有効とするか否か</summary>
    public bool AutoLink { get; set; } = false;
}
