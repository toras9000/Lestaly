namespace Lestaly;

/// <summary>
/// Span に対する拡張メソッド
/// </summary>
public static class SpanExtensions
{
    /// <summary>Span{T} を ReadOnlySpan{T} に変換する。</summary>
    /// <remarks>暗黙の型変換が評価されない場面用。たとえば拡張メソッドのオーバーロード解決など。</remarks>
    /// <param name="self">Span{T}</param>
    /// <returns>ReadOnlySpan{T}</returns>
    public static ReadOnlySpan<T> AsReadOnly<T>(this Span<T> self) => self;

    /// <summary>配列の読み取り専用スパンを作成する。</summary>
    /// <remarks>暗黙の型変換が評価されない場面用。たとえば拡張メソッドのオーバーロード解決など。</remarks>
    /// <typeparam name="T">要素の型</typeparam>
    /// <param name="self">対象配列</param>
    /// <returns>読み取り専用スパン</returns>
    public static ReadOnlySpan<T> AsReadOnlySpan<T>(this T[] self) => self;

}
