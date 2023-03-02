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
    public Func<MemberInfo, int, string?>? CaptionSelector { get; set; } = null;

    /// <summary>属性からカラム名と順序を利用するか否か</summary>
    /// <remarks>プロパティに付与された <see cref="System.ComponentModel.DataAnnotations.DisplayAttribute"/> を参照する。</remarks>
    public bool UseCaptionAttribute { get; set; } = false;

    /// <summary>キャプションで出力カラム順をソートするか否か</summary>
    public bool SortCaption { get; set; } = false;

    /// <summary>メンバ名で出力カラム順をソートするか否か</summary>
    public bool SortMemberName { get; set; } = false;

    /// <summary>メンバが使用するカラム数の取得デリゲート</summary>
    /// <remarks><see cref="UseColumnSpanAttribute"/> が有効で属性からカラム数を得た場合でも、このデリゲートで数が得られた場合は優先して使用する。</remarks>
    public Func<MemberInfo, int?>? ColumnSpanSelector { get; set; } = null;

    /// <summary>属性から使用するカラム数を参照するか否か</summary>
    /// <remarks>プロパティに付与された <see cref="System.ComponentModel.DataAnnotations.MaxLengthAttribute"/> を参照する。</remarks>
    public bool UseColumnSpanAttribute { get; set; } = false;

    /// <summary>展開データが最大カラム数を超えた場合にサイレントに破棄するか否かを示す。</summary>
    public bool DropSpanOver { get; set; } = false;
}
