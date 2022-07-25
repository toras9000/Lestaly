using System.Reflection;

namespace Lestaly;

/// <summary>
/// 型のカラム収集設定
/// </summary>
public class TypeColumnOptions
{
    /// <summary>出力対象にフィールドを含めるか否か</summary>
    public bool IncludeFields { get; set; } = false;

    /// <summary>カラムのキャプション文字列の取得デリゲート</summary>
    /// <remarks><see cref="UseDisplayAttribute"/> が有効な場合はそちらが優先される。</remarks>
    public Func<MemberInfo, string?>? CaptionSelector { get; set; } = null;

    /// <summary>出力するメンバをフィルタするデリゲート</summary>
    public Func<MemberInfo, bool>? MemberFilter { get; set; } = null;

    /// <summary>Display属性からカラム名と順序を利用するか否か</summary>
    public bool UseDisplayAttribute { get; set; } = false;

    /// <summary>キャプションで出力カラム順をソートするか否か</summary>
    public bool SortCaption { get; set; } = false;

    /// <summary>メンバ名で出力カラム順をソートするか否か</summary>
    public bool SortMemberName { get; set; } = false;
}
