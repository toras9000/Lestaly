using System.Reflection;

namespace Lestaly;

/// <summary>
/// 型のカラム収集設定
/// </summary>
public class TypeColumnOptions
{
    /// <summary>出力対象にフィールドを含めるか否か</summary>
    public bool IncludeFields { get; set; } = false;

    /// <summary>出力するメンバをフィルタするデリゲート</summary>
    public Func<MemberInfo, bool>? MemberFilter { get; set; } = null;

    /// <summary>カラムのキャプション文字列の取得デリゲート</summary>
    /// <remarks><see cref="UseCaptionAttribute"/> が有効で属性から名称が得られる場合でも、このデリゲートで取得された名称を優先して使用する。</remarks>
    public Func<MemberInfo, string?>? CaptionSelector { get; set; } = null;

    /// <summary>属性からカラム名と順序を利用するか否か</summary>
    /// <remarks>プロパティに付与された <see cref="System.ComponentModel.DataAnnotations.DisplayAttribute"/> を参照する。</remarks>
    public bool UseCaptionAttribute { get; set; } = false;

    /// <summary>キャプションで出力カラム順をソートするか否か</summary>
    public bool SortCaption { get; set; } = false;

    /// <summary>メンバ名で出力カラム順をソートするか否か</summary>
    public bool SortMemberName { get; set; } = false;
}
