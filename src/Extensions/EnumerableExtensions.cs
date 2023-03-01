﻿namespace Lestaly;

/// <summary>
/// IEnumerable{T} に対する拡張メソッド
/// </summary>
public static class EnumerableExtensions
{
    /// <summary>
    /// シーケンスのフィルタと条件に適合しない場合のアクションを指定するオペレータ。
    /// </summary>
    /// <typeparam name="TSource">シーケンスの要素型</typeparam>
    /// <param name="self">対象シーケンス</param>
    /// <param name="predicate">要素の通過判定処理</param>
    /// <param name="skipped">要素が通過しない場合に呼び出される処理</param>
    /// <returns>フィルタされたシーケンス</returns>
    public static IEnumerable<TSource> WhereElse<TSource>(this IEnumerable<TSource> self, Func<TSource, bool> predicate, Action<TSource> skipped)
        => CometFlavor.Extensions.Linq.EnumerableExtensions.WhereElse(self, predicate, skipped);

    /// <summary>シーケンスインスタンスがnullならば空のシーケンスを代替とする</summary>
    /// <typeparam name="TSource">シーケンスの要素型</typeparam>
    /// <param name="self">対象シーケンス</param>
    /// <returns>元のシーケンスまたは空のシーケンス</returns>
    public static IEnumerable<TSource> CoalesceEmpty<TSource>(this IEnumerable<TSource>? self)
        => CometFlavor.Extensions.Linq.EnumerableExtensions.CoalesceEmpty(self);

    /// <summary>シーケンスが空の場合にエラー(例外発行)とする</summary>
    /// <typeparam name="TSource">シーケンスの要素型</typeparam>
    /// <param name="self">対象シーケンス</param>
    /// <param name="errorGenerator">空の場合のエラー(例外)インスタンス生成デリゲート</param>
    /// <returns>エラーの無い場合は元と同じシーケンス</returns>
    public static IEnumerable<TSource> ErrorIfEmpty<TSource>(this IEnumerable<TSource>? self, Func<Exception>? errorGenerator = null)
    {
        var exists = false;
        if (self != null)
        {
            foreach (var elem in self)
            {
                yield return elem;
                exists = true;
            }
        }
        if (!exists)
        {
            throw errorGenerator?.Invoke() ?? new InvalidDataException();
        }
    }

    /// <summary>シーケンス要素が条件を満たさない場合にエラー(例外発行)とする</summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="self">シーケンスの要素型</param>
    /// <param name="predicator">条件を満たすかを判定するデリゲート</param>
    /// <param name="errorGenerator">条件を満たさない場合のエラー(例外)インスタンス生成デリゲート</param>
    /// <returns>エラーの無い場合は元と同じシーケンス</returns>
    public static IEnumerable<TSource> Must<TSource>(this IEnumerable<TSource> self, Func<TSource, bool> predicator, Func<Exception>? errorGenerator = null)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));
        if (predicator == null) throw new ArgumentNullException(nameof(predicator));

        IEnumerable<TSource> mustEnumerator()
        {
            foreach (var elem in self)
            {
                if (!predicator(elem))
                {
                    throw errorGenerator?.Invoke() ?? new InvalidDataException();
                }
                yield return elem;
            }
        }
        return mustEnumerator();
    }

    /// <summary>シーケンスの先頭から要素を分解する</summary>
    /// <typeparam name="TSource">要素の型</typeparam>
    /// <param name="self">対象シーケンス</param>
    /// <param name="value1">シーケンス要素1</param>
    /// <param name="value2">シーケンス要素2</param>
    public static void Deconstruct<TSource>(this IEnumerable<TSource>? self, out TSource? value1, out TSource? value2)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));

        using var enumerator = self.GetEnumerator();
        value1 = enumerator.MoveNext() ? enumerator.Current : default;
        value2 = enumerator.MoveNext() ? enumerator.Current : default;
    }

    /// <summary>シーケンスの先頭から要素を分解する</summary>
    /// <typeparam name="TSource">要素の型</typeparam>
    /// <param name="self">対象シーケンス</param>
    /// <param name="value1">シーケンス要素1</param>
    /// <param name="value2">シーケンス要素2</param>
    /// <param name="value3">シーケンス要素3</param>
    public static void Deconstruct<TSource>(this IEnumerable<TSource>? self, out TSource? value1, out TSource? value2, out TSource? value3)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));

        using var enumerator = self.GetEnumerator();
        value1 = enumerator.MoveNext() ? enumerator.Current : default;
        value2 = enumerator.MoveNext() ? enumerator.Current : default;
        value3 = enumerator.MoveNext() ? enumerator.Current : default;
    }

    /// <summary>シーケンスの先頭から要素を分解する</summary>
    /// <typeparam name="TSource">要素の型</typeparam>
    /// <param name="self">対象シーケンス</param>
    /// <param name="value1">シーケンス要素1</param>
    /// <param name="value2">シーケンス要素2</param>
    /// <param name="value3">シーケンス要素3</param>
    /// <param name="value4">シーケンス要素4</param>
    public static void Deconstruct<TSource>(this IEnumerable<TSource>? self, out TSource? value1, out TSource? value2, out TSource? value3, out TSource? value4)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));

        using var enumerator = self.GetEnumerator();
        value1 = enumerator.MoveNext() ? enumerator.Current : default;
        value2 = enumerator.MoveNext() ? enumerator.Current : default;
        value3 = enumerator.MoveNext() ? enumerator.Current : default;
        value4 = enumerator.MoveNext() ? enumerator.Current : default;
    }

    /// <summary>シーケンスの先頭から要素を分解する</summary>
    /// <typeparam name="TSource">要素の型</typeparam>
    /// <param name="self">対象シーケンス</param>
    /// <param name="value1">シーケンス要素1</param>
    /// <param name="value2">シーケンス要素2</param>
    /// <param name="value3">シーケンス要素3</param>
    /// <param name="value4">シーケンス要素4</param>
    /// <param name="value5">シーケンス要素5</param>
    public static void Deconstruct<TSource>(this IEnumerable<TSource>? self, out TSource? value1, out TSource? value2, out TSource? value3, out TSource? value4, out TSource? value5)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));

        using var enumerator = self.GetEnumerator();
        value1 = enumerator.MoveNext() ? enumerator.Current : default;
        value2 = enumerator.MoveNext() ? enumerator.Current : default;
        value3 = enumerator.MoveNext() ? enumerator.Current : default;
        value4 = enumerator.MoveNext() ? enumerator.Current : default;
        value5 = enumerator.MoveNext() ? enumerator.Current : default;
    }

    /// <summary>シーケンスを非同期シーケンス型に変換する</summary>
    /// <remarks>型は IAsyncEnumerable となるが、列挙は同期的であるため型合わせだけの意味の変換となる。</remarks>
    /// <typeparam name="TSource">シーケンスの要素型</typeparam>
    /// <param name="self">元になるシーケンス</param>
    /// <returns>非同期シーケンス</returns>
    public static IAsyncEnumerable<TSource> ToPseudoAsyncEnumerable<TSource>(this IEnumerable<TSource> self)
    {
        if (self == null) throw new ArgumentNullException(nameof(self));

        static async IAsyncEnumerable<T> enumerateAsync<T>(IEnumerable<T> source)
        {
            foreach (var item in source)
            {
                yield return await Task.FromResult(item).ConfigureAwait(false);
            }
        }

        return enumerateAsync(self);
    }

}
