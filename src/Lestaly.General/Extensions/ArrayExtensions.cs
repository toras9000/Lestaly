using System.Collections.ObjectModel;

namespace Lestaly;

/// <summary>配列 に対する拡張メソッド</summary>
public static class ArrayExtensions
{
    /// <summary>配列 に対する拡張メソッド</summary>
    /// <typeparam name="T">要素の型</typeparam>
    /// <param name="self">対象配列</param>
    extension<T>(T[] self)
    {
        /// <summary>配列のラッパー読み取り専用コレクションを作成する。</summary>
        /// <returns>読み取り専用コレクション</returns>
        public ReadOnlyCollection<T> AsReadOnly()
        {
            return Array.AsReadOnly(self);
        }

        /// <summary>配列内容を繰り返した新しい配列を生成する</summary>
        /// <param name="count">繰り返し回数</param>
        /// <returns>元の配列内容が繰り返された配列</returns>
        public T[] CreateRepeat(int count)
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
        /// <param name="value">埋める値</param>
        /// <returns>対象配列自身のインスタンス</returns>
        public T[] FillBy(T value)
        {
            self.AsSpan().Fill(value);
            return self;
        }
    }

}
