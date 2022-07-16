using System.Text;

namespace Lestaly.Data;

/// <summary>
/// 複数のフィールドからなるテキストの蓄積を意図したバッファクラス
/// </summary>
/// <remarks>
/// 拡張メソッドの背後で内部的に利用する目的で作成したが、汎用に使えないこともないので public とした。
/// 用途としては以下のような想定。
/// <list>
/// <item>テキストの断片を <see cref="Append(char)"/> して蓄積</item>
/// <item>テキストが1つのフィールドとして分蓄積出来た時点で <see cref="Fetch"/> してフィールド化</item>
/// <item>フィールドの集まりが1まとまりの区切りになったら <see cref="Consume"/> にて消費。</item>
/// <item>消費するとインスタンス状態は初期状態となるので、上記を繰り返す。</item>
/// </list>
/// </remarks>
public class FieldCollector
{
    // 構築
    #region コンストラクタ
    /// <summary>
    /// デフォルトコンストラクタ
    /// </summary>
    public FieldCollector()
    {
        this.buffer = new();
        this.fields = new();
    }
    #endregion

    // 公開プロパティ
    #region 状態情報
    /// <summary>テキスト断片を蓄積中であるか否か</summary>
    public bool HasFragment => 0 < this.buffer.Length;

    /// <summary>フィールドを保持しているか否か</summary>
    public bool HasFields => 0 < this.fields.Count;
    #endregion

    // 公開メソッド
    #region テキスト蓄積
    /// <summary>テキスト断片を蓄積する</summary>
    /// <param name="elem">テキスト断片</param>
    public void Append(char elem)
    {
        this.buffer.Append(elem);
    }

    /// <summary>テキスト断片を蓄積する</summary>
    /// <param name="elem">テキスト断片</param>
    public void Append(string elem)
    {
        this.buffer.Append(elem);
    }

    /// <summary>テキスト断片を蓄積する</summary>
    /// <param name="span">テキスト断片</param>
    public void Append(ReadOnlySpan<char> span)
    {
        this.buffer.Append(span);
    }

    /// <summary>蓄積したテキストをフィールドとして保持する</summary>
    public void Fetch()
    {
        this.fields.Add(this.buffer.ToString());
        this.buffer.Clear();
    }
    #endregion

    #region フィールド
    /// <summary>蓄積したフィールド列を消費する</summary>
    /// <returns>フィールド列</returns>
    public string[] Consume()
    {
        // テキスト断片が蓄積中であればフィールドとして確立する。
        if (this.HasFragment)
        {
            this.Fetch();
        }

        // フィールドが1つも無い場合、新しいインスタンスを作る必要も無いので空の配列を返却。
        if (!this.HasFields)
        {
            return Array.Empty<string>();
        }

        // フィールド列を配列化し、保持しているフィールドはクリアする。
        var row = this.fields.ToArray();
        this.fields.Clear();
        return row;
    }
    #endregion

    // 非公開フィールド
    #region 蓄積用
    /// <summary>テキスト断片の蓄積バッファ</summary>
    private StringBuilder buffer;

    /// <summary>フィールドのリスト</summary>
    private List<string> fields;
    #endregion
}
