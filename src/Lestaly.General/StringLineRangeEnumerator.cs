namespace Lestaly;

/// <summary>文字列の行範囲を列挙する</summary>
public ref struct StringLineRangeEnumerator
{
    /// <summary>対象文字列を指定するコンストラクタ</summary>
    /// <param name="span">対象文字列</param>
    /// <param name="raw">行末文字を含めて列挙するか否か</param>
    public StringLineRangeEnumerator(ReadOnlySpan<char> span, bool raw = false)
    {
        this.source = span;
        this.consumed = 0;
        this.raw = raw;
        this.current = ^0..;
    }

    /// <summary>現在位置の行範囲を取得する</summary>
    public Range Current => this.current;

    /// <summary>列挙子を取得する</summary>
    public StringLineRangeEnumerator GetEnumerator() => this;

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
            // 設定により、改行前か改行込みで現在範囲とする
            var termLen = (next[term..] is ['\r', '\n', ..]) ? 2 : 1;
            this.current = this.raw ? (this.consumed..(this.consumed + term + termLen)) : (this.consumed..(this.consumed + term));
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

    /// <summary>現在行範囲</summary>
    private Range current;
}
