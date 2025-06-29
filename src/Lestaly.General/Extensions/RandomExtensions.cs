namespace Lestaly;

/// <summary>
/// Random に対する拡張メソッド
/// </summary>
public static class RandomExtensions
{
    /// <summary>ランダム値のバイト配列を作成する。</summary>
    /// <param name="self">ランダム値を生成するインスタンス</param>
    /// <param name="length">生成するバイト列の長さ</param>
    /// <returns>生成したランダム値配列</returns>
    public static byte[] GetBytes(this Random self, int length)
    {
        var data = new byte[length];
        self.NextBytes(data);
        return data;
    }
}
