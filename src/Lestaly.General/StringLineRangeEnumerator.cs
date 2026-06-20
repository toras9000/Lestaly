namespace Lestaly;

/// <summary>文字列の行スパンを列挙する</summary>
public ref struct StringLineSpanEnumerator
{
    /// <summary>対象文字列を指定するコンストラクタ</summary>
    /// <param name="span">対象文字列</param>
    /// <param name="raw">行末文字を含めて列挙するか否か</param>
    public StringLineSpanEnumerator(ReadOnlySpan<char> span, bool raw = false)
    {
        this.source = span;
        this.consumed = 0;
        this.raw = raw;
        this.current = span[0..0];
    }

    /// <summary>現在位置の行スパンを取得する</summary>
    public ReadOnlySpan<char> Current => this.current;

    /// <summary>列挙子を取得する</summary>
    public StringLineSpanEnumerator GetEnumerator() => this;

    /// <summary>現在位置の行スパンを取得する</summary>
    public bool MoveNext()
    {
        // 末尾まで消化している場合は有効でない
        if (this.source.Length <= this.consumed) return false;

        // 残りの文字列の改行位置を取得
        var next = this.source[this.consumed..];
        var term = next.IndexOfAny(['\r', '\n']);
        if (term < 0)
        {
            // 見つからなければ末尾までが現在行
            this.current = next;
            this.consumed += next.Length;
        }
        else
        {
            // 見つかったら改行までが現在行
            // 設定により、改行前か改行込みで現在行とする
            var termLen = (next[term..] is ['\r', '\n', ..]) ? 2 : 1;
            this.current = this.raw ? next[..(term + termLen)] : next[..term];
            // 改行後までを消化済みにする
            this.consumed += term + termLen;
        }
        return true;
    }

    /// <summary>対象文字列</summary>
    private readonly ReadOnlySpan<char> source;

    /// <summary>行末文字を含めて列挙するか否か</summary>
    private readonly bool raw;

    /// <summary>消化済みの長さ</summary>
    private int consumed;

    /// <summary>現在行スパン</summary>
    private ReadOnlySpan<char> current;
}
