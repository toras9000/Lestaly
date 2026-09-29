using System.Numerics;

namespace Lestaly;

/// <summary>プリミティブ整数型に対するビット処理系拡張メソッド</summary>
public static class BitsExtensions
{
    /// <summary>整数 に対するビット操作</summary>
    /// <param name="self">対象値</param>
    extension<TBinary>(TBinary self) where TBinary : struct, IBinaryInteger<TBinary>, INumberBase<TBinary>, IBitwiseOperators<TBinary, TBinary, TBinary>, IShiftOperators<TBinary, int, TBinary>
    {
        /// <summary>指定のビット位置・範囲の値を取得する</summary>
        /// <param name="pos">ビット位置。下位桁からの0ベース値。</param>
        /// <param name="len">ビット範囲長</param>
        /// <returns>指定ビット位置・範囲の値</returns>
        public TBinary GetBits(int pos, int len)
        {
            if (len <= 0) return TBinary.Zero;
            var bits = TBinary.PopCount(TBinary.AllBitsSet);
            var mask = bits <= TBinary.CreateTruncating(len) ? TBinary.AllBitsSet : ~(TBinary.AllBitsSet << len);
            var result = (self >> pos) & mask;
            return result;
        }

        /// <summary>指定のビット位置・範囲の値を変更した値を取得する</summary>
        /// <param name="pos">ビット位置。下位桁からの0ベース値。</param>
        /// <param name="len">ビット範囲長</param>
        /// <param name="value">設定する値。有効ビット範囲外は無視。</param>
        /// <returns>変更された値</returns>
        public TBinary ModBits(int pos, int len, TBinary value)
            => self.SetBits(pos, len, value);
    }

    /// <summary>整数 に対するビット操作</summary>
    /// <param name="self">対象値</param>
    extension<TBinary>(ref TBinary self) where TBinary : struct, IBinaryInteger<TBinary>, INumberBase<TBinary>, IBitwiseOperators<TBinary, TBinary, TBinary>, IShiftOperators<TBinary, int, TBinary>
    {
        /// <summary>指定のビット位置・範囲の値を変更する</summary>
        /// <param name="pos">ビット位置。下位桁からの0ベース値。</param>
        /// <param name="len">ビット範囲長</param>
        /// <param name="value">設定する値。有効ビット範囲外は無視。</param>
        /// <returns>変更された値</returns>
        public TBinary SetBits(int pos, int len, TBinary value)
        {
            if (len <= 0) return self;
            var bits = TBinary.PopCount(TBinary.AllBitsSet);
            var mask = bits <= TBinary.CreateTruncating(len) ? TBinary.AllBitsSet : ~(TBinary.AllBitsSet << len);

            self &= ~(mask << pos);
            self |= ((value & mask) << pos);
            return self;
        }
    }


}
