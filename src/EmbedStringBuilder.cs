using System.Text;

namespace Lestaly;

/// <summary>簡易な埋め込み文字列構築処理</summary>
/// <remarks>
/// あらかじめ定義した変数とその変数名を埋め込んだテンプレート文字列から、変数の埋め込み済み文字列を構築する処理。
/// 埋め込みは単純な文字列置き換えのみであり、書式設定などはサポートされない。
/// 
/// 変数はテンプレート文字列内で $ で先導された名称で記述する。
/// 変数が NAME=foo と定義されている場合、テンプレート文字列 "ABC $NAME DEF" に埋め込みを行うと "ABC foo DEF" のような結果となる。
/// 変数名は名称として有効なキャラクタが連続する限り最長で認識される。
/// また、変数として有効なキャラクタはコンストラクタで判別デリゲートを指定することができる。
/// 
/// テンプレート文字列 "ABC$NAMEDEF" であれば、変数「NAMEDEF」の埋め込みが試みられる。(変数 NAME が定義されていても利用されない。)
/// 変数名が他の文字列と連続する場合のために、カーリーブレースで ${xxx} のように囲んで変数名の範囲を明示することができる。
/// この場合、カーリーブレースで囲まれる部分はどのようなキャラクタも変数名の一部とみなされる。(変数名として有効なキャラクタ判定は無関係となる)
/// </remarks>
public class EmbedStringBuilder
{
    // 構築
    #region コンストラクタ
    /// <summary>コンストラクタ</summary>
    /// <param name="variableValidater">変数名として有効なキャラクタを判定するデリゲート。省略時はASCIIのアルファベットと数字のみが有効となる。</param>
    public EmbedStringBuilder(Func<char, bool>? variableValidater = null)
    {
        this.validater = variableValidater ?? (c => char.IsAsciiLetter(c) || char.IsAsciiDigit(c));

        var vars = new Dictionary<string, string>();
        this.Variables = vars;
        this.Default = "";

#if NET9_0_OR_GREATER
        this.varsLookup = vars.GetAlternateLookup<ReadOnlySpan<char>>();
#endif
    }
    #endregion

    // 公開プロパティ
    #region 変数
    /// <summary>変数名とその値を表す辞書</summary>
    public IDictionary<string, string> Variables { get; }

    /// <summary>辞書にない変数名を置き換えるデフォルト文字列</summary>
    public string Default { get; set; }
    #endregion

    // 公開メソッド
    #region 構築
    /// <summary>埋め込み文字列を構築する</summary>
    /// <param name="template">テンプレート文字列</param>
    /// <param name="buffer">構築結果の書き込み先</param>
    public void Write(ReadOnlySpan<char> template, StringBuilder buffer)
    {
        var scan = template;
        while (!scan.IsEmpty)
        {
            if (scan[0] == '$')
            {
                scan = scan[1..];
                if (scan[0] == '$')
                {
                    buffer.Append('$');
                    scan = scan[1..];
                }
                else
                {
                    var token = takeVariable(ref scan);
                    if (!token.IsEmpty)
                    {
                        var value = lookupVariable(token);
                        buffer.Append(value);
                    }
                }
            }
            else
            {
                buffer.Append(scan[0]);
                scan = scan[1..];
            }
        }
    }

    /// <summary>埋め込み文字列を構築する</summary>
    /// <param name="template">テンプレート文字列</param>
    /// <returns>構築された文字列</returns>
    public string Build(ReadOnlySpan<char> template)
    {
        var buffer = new StringBuilder();
        this.Write(template, buffer);
        return buffer.ToString();
    }
    #endregion

    // 非公開フィールド
    #region 変数
    /// <summary>有効な変数名キャラクタ判定デリゲート</summary>
    private readonly Func<char, bool> validater;

#if NET9_0_OR_GREATER
    /// <summary>変数の参照アダプタ</summary>
    private Dictionary<string, string>.AlternateLookup<ReadOnlySpan<char>> varsLookup;
#endif
    #endregion

    // 非公開メソッド
    #region 文字列処理
    /// <summary>変数名を抽出する</summary>
    /// <param name="scan">テンプレート文字列。読み取られた長さだけ後ろにスライスされる。</param>
    /// <returns>変数名</returns>
    private ReadOnlySpan<char> takeVariable(ref ReadOnlySpan<char> scan)
    {
        if (scan.IsEmpty) return ReadOnlySpan<char>.Empty;

        if (scan[0] == '{')
        {
            scan = scan[1..];
            var end = scan.IndexOf('}');
            if (end < 0)
            {
                var variable = scan;
                scan = scan[scan.Length..];
                return variable;
            }
            else
            {
                var variable = scan[..end];
                scan = scan[(end + 1)..];
                return variable;
            }
        }
        else
        {
            var len = 0;
            var begin = scan;
            while (!scan.IsEmpty && this.validater(scan[0]))
            {
                scan = scan[1..];
                len++;
            }

            var variable = begin[..len];
            return variable;
        }

    }

    /// <summary>変数名に対する埋め込み文字列を取得する</summary>
    /// <param name="key">変数名</param>
    /// <returns>埋め込み文字列</returns>
    private string? lookupVariable(ReadOnlySpan<char> key)
    {
#if NET9_0_OR_GREATER
        return this.varsLookup.TryGetValue(key, out var value) ? value : this.Default;
#else
        return this.Variables.TryGetValue(key.ToString(), out var value) ? value : this.Default;
#endif
    }
    #endregion

}
