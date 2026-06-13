namespace Lestaly;

/// <summary>
/// CSV(Character Separated Field)テキスト保存オプション
/// </summary>
public class SaveToCsvOptions : TypeColumnOptions
{
    /// <summary>区切り文字</summary>
    public char Separator { get; set; } = ',';

    /// <summary>クォート文字</summary>
    public char Quote { get; set; } = '"';
}
