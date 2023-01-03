using System.Reflection;

namespace Lestaly;

/// <summary>
/// Excelテキスト保存オプション
/// </summary>
public class SaveToExcelOptions : TypeColumnOptions
{
    /// <summary>フォント名</summary>
    public string? FontName { get; set; } = null;

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

/// <summary>書式付きの値を表すデータ型</summary>
/// <param name="Value">出力する値</param>
/// <param name="BackColor">背景色。#で開始するARGB HEX表記か名称による指定が可能。</param>
/// <param name="ForeColor">テキスト色。#で開始するARGB HEX表記か名称による指定が可能。</param>
/// <param name="Extra">追加の書式情報</param>
/// <param name="DynamicValue">
/// 出力する値を動的に評価するか否か。
/// 動的評価では ExcelHyperlink/ExcelFormula が有効となる。
/// 動的評価を有効にした場合、パフォーマンスは大きく落ちるため注意。
/// </param>
public record ExcelStyle(object Value, string? BackColor = null, string? ForeColor = null, ExcelStyleExtra? Extra = null, bool DynamicValue = false);

/// <summary>書式付き値を型への追加書式情報</summary>
/// <param name="Font">利用するフォント名</param>
/// <param name="FontSize">フォントサイズ。非数の場合は指定なしを示す。</param>
/// <param name="Bold">フォントをボールドにするか否か</param>
/// <param name="Italic">フォントをイタリックにするか否か</param>
/// <param name="Strike">テキストに打消し線を装飾するか否か</param>
/// <param name="Comment">コメント(メモ)</param>
public record ExcelStyleExtra(string? Font = null, double FontSize = double.NaN, bool Bold = false, bool Italic = false, bool Strike = false, string? Comment = null);

/// <summary>複数列に展開するデータ型</summary>
/// <param name="Values">
/// カラム方向に展開出力するデータ。
/// 展開出力するためには、この型のメンバに対して属性または<see cref="TypeColumnOptions.ColumnSpanSelector"/>にて最大カラム数を指定しておく必要がある。
/// 出力先の幅を超えた場合の挙動は<see cref="TypeColumnOptions.DropSpanOver"/>の影響を受ける。
/// </param>
/// <param name="DynamicValue">
/// 出力する値を動的に評価するか否か。
/// 動的評価では ExcelHyperlink/ExcelFormula/ExcelStyle が有効となる。
/// 動的評価を有効にした場合、パフォーマンスは大きく落ちるため注意。
/// </param>
public record ExcelExpand(IReadOnlyCollection<object> Values, bool DynamicValue = false);
