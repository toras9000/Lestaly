using System.Collections.ObjectModel;

namespace Lestaly;

/// <summary>
/// 配列 に対する拡張メソッド
/// </summary>
public static class ArrayExtensions
{
    /// <summary>配列のラッパー読み取り専用コレクションを作成する。</summary>
    /// <typeparam name="T">要素の型</typeparam>
    /// <param name="self">対象配列</param>
    /// <returns>読み取り専用コレクション</returns>
    public static ReadOnlyCollection<T> AsReadOnly<T>(this T[] self)
    {
        return Array.AsReadOnly(self);
    }

    /// <summary>配列内容を繰り返した新しい配列を生成する</summary>
    /// <typeparam name="T">要素型</typeparam>
    /// <param name="self">元にする配列</param>
    /// <param name="count">繰り返し回数</param>
    /// <returns>元の配列内容が繰り返された配列</returns>
    public static T[] CreateRepeat<T>(this T[] self, int count)
    {
        if (count < 0) throw new ArgumentException($"Invalid {nameof(count)}");
        var repeated = new T[self.Length * count];
        for (var i = 0; i < repeated.Length; i += self.Length)
        {
            self.CopyTo(repeated, i);
        }
        return repeated;
    }

    /// <summary>配列内容を指定の値で埋めるコードを1行で記述する</summary>
    /// <typeparam name="T">要素の型</typeparam>
    /// <param name="self">対象配列</param>
    /// <param name="value">埋める値</param>
    /// <returns>対象配列自身のインスタンス</returns>
    public static T[] FillBy<T>(this T[] self, T value)
    {
        self.AsSpan().Fill(value);
        return self;
    }
}
