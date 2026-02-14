using System.Text;

namespace Lestaly;

/// <summary>テキスト書き込み専用I/F</summary>
public interface IStringWriter
{
    /// <summary>文字を書き込む</summary>
    /// <param name="ch">文字</param>
    void Write(char ch);

    /// <summary>テキストを書き込む</summary>
    /// <param name="text">テキスト</param>
    void Write(ReadOnlySpan<char> text);

    /// <summary>テキストを書き込む</summary>
    /// <param name="text">テキスト</param>
    void Write(ReadOnlyMemory<char> text);

    /// <summary>補完テキストを書き込む</summary>
    /// <param name="handler">補完テキストハンドラ</param>
    void Write(ref StringBuilder.AppendInterpolatedStringHandler handler);
}

/// <summary>StringBuilderテキスト書き込みI/F</summary>
public class BuilderStringWriter : IStringWriter
{
    // 構築
    #region コンストラクタ
    /// <summary>初期キャパシタを指定するコンストラクタ</summary>
    /// <param name="capacity"></param>
    public BuilderStringWriter(int capacity)
    {
        this.Builder = new StringBuilder(capacity);
    }
    #endregion

    // 公開メソッド
    #region バックエンド
    /// <summary>文字列ビルダ</summary>
    public StringBuilder Builder { get; }
    #endregion

    // 公開メソッド
    #region テキスト書き込み
    /// <inheritdoc />
    public void Write(char ch) => this.Builder.Append(ch);

    /// <inheritdoc />
    public void Write(ReadOnlySpan<char> text) => this.Builder.Append(text);

    /// <inheritdoc />
    public void Write(ReadOnlyMemory<char> text) => this.Builder.Append(text);

    /// <inheritdoc />
    public void Write(ref StringBuilder.AppendInterpolatedStringHandler handler) => this.Builder.Append(ref handler);
    #endregion
}
