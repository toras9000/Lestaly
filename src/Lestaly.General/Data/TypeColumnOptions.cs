using System.Reflection;

namespace Lestaly;

/// <summary>型のカラムに対する補足情報属性</summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class TypeColumnAttribute : Attribute
{
    /// <summary>カラムのキャプション</summary>
    public string? Caption { get; set; }

    /// <summary>複数カラムにまたがる(ColumnSpan が1より大きい)場合にキャプションを分割解釈するためのセパレータ</summary>
    public string? Separator { get; set; }

    /// <summary>メンバのカラム順序</summary>
    public int Order { get; set; }

    /// <summary>メンバに対応するカラムの数</summary>
    public int ColumnSpan { get; set; }
}

/// <summary>型のカラム収集設定</summary>
public class TypeColumnOptions
{
    /// <summary>出力対象にフィールドを含めるか否か</summary>
    public bool IncludeFields { get; set; } = false;

    /// <summary>出力するメンバをフィルタするデリゲート</summary>
    public Func<MemberInfo, bool>? MemberFilter { get; set; } = null;

    /// <summary>カラムのキャプション文字列の取得デリゲート</summary>
    /// <remarks><see cref="UseColumnAttribute"/> が有効で属性から名称が得られる場合でも、このデリゲートで取得された名称を優先して使用する。</remarks>
    public Func<MemberInfo, int, string?>? CaptionSelector { get; set; } = null;

    /// <summary>キャプションで出力カラム順をソートするか否か</summary>
    public bool SortCaption { get; set; } = false;

    /// <summary>メンバ名で出力カラム順をソートするか否か</summary>
    public bool SortMemberName { get; set; } = false;

    /// <summary>メンバが使用するカラム数の取得デリゲート</summary>
    /// <remarks><see cref="UseColumnAttribute"/> が有効で属性からカラム数を得た場合でも、このデリゲートで数が得られた場合は優先して使用する。</remarks>
    public Func<MemberInfo, int?>? ColumnSpanSelector { get; set; } = null;

    /// <summary>属性からカラム設定を利用するか否か</summary>
    /// <remarks>プロパティに付与された <see cref="Lestaly.TypeColumnAttribute"/> を参照する。</remarks>
    public bool UseColumnAttribute { get; set; } = true;

    /// <summary>展開データが最大カラム数を超えた場合にサイレントに破棄するか否かを示す。</summary>
    public bool DropSpanOver { get; set; } = false;

    /// <summary>動的コンパイルベースのメンバアクセサを利用するか否か</summary>
    /// <remarks>処理データ数が少ない場合、これを指定するとオーバヘッドによりかえってパフォーマンスを落とす事になるので注意。10万件以上が目安。</remarks>
    public bool UseCompiledGetter { get; set; } = false;
}
