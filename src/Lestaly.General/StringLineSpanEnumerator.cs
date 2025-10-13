namespace Lestaly;

/// <summary>文字列の行範囲を列挙する</summary>
public ref struct StringLineSpanEnumerator
{
    /// <summary>対象文字列を指定するコンストラクタ</summary>
    /// <param name="span">対象文字列</param>
    public StringLineSpanEnumerator(ReadOnlySpan<char> span)
    {
        this.source = span;
        this.consumed = 0;
        this.current = ^0..;
    }

    /// <summary>現在位置の行範囲を取得する</summary>
    public Range Current => this.current;

    /// <summary>列挙子を取得する</summary>
    public StringLineSpanEnumerator GetEnumerator() => this;

    /// <summary>現在位置の行範囲を取得する</summary>
    public bool MoveNext()
    {
        // 末尾まで消化している場合は有効でない
        if (this.source.Length <= this.consumed) return false;

        // 残りの文字列の改行位置を取得
        var next = this.source[this.consumed..];
        var term = next.IndexOfAny(['\r', '\n']);
        if (term < 0)
        {
            // 見つからなければ末尾までが現在行の範囲
            this.current = this.consumed..;
            this.consumed += next.Length;
        }
        else
        {
            // 見つかったら改行までが現在行の範囲
            this.current = this.consumed..(this.consumed + term);
            this.consumed += term;
            // 改行文字分を消化。CRLFなら2文字分。
            var termLen = (this.source[this.consumed..] is ['\r', '\n', ..]) ? 2 : 1;
            this.consumed += termLen;
        }
        return true;
    }

    /// <summary>対象文字列</summary>
    private readonly ReadOnlySpan<char> source;

    /// <summary>消化済みの長さ</summary>
    private int consumed;

    /// <summary>現在行範囲</summary>
    private Range current;
}
