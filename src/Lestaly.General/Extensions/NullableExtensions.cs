using System.Numerics;

namespace Lestaly;

/// <summary>
/// Nullable に対する拡張メソッド
/// </summary>
public static class NullableExtensions
{
    /// <summary>Nullable{T} で有効値を優先した加算を行う。</summary>
    /// <remarks>nullのみならばnull維持したいが、有効値があればそちらを優先したい場合用。</remarks>
    /// <typeparam name="T">数値型</typeparam>
    /// <param name="self">加算オペランド1</param>
    /// <param name="other">加算オペランド2</param>
    /// <returns>オペランドの両方が有効値であればその合計値を、一方のみが有効値であれば有効値を、両方がnullの場合はnullを返却。</returns>
    public static T? AddPrefer<T>(this T? self, T? other) where T : struct, INumber<T>
    {
        if (other.HasValue) return self.HasValue ? self + other : other;
        return self;
    }
}
