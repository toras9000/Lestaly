using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;

namespace Lestaly;

/// <summary>
/// スクランブル処理クラスに対する拡張メソッド
/// </summary>
public static class RoughScramblerExtensions
{
    #region スクランブル化
    /// <summary>テキストをスクランブルする。</summary>
    /// <remarks>このメソッドでスクランブルされたデータは <see cref="DescrambleText(RoughScrambler, ReadOnlySpan{byte})"/> で解除することを想定している。</remarks>
    /// <param name="self">利用するスクランブル化処理インスタンス</param>
    /// <param name="text">スクランブルする文字列</param>
    /// <returns>スクランブルされたバイト列</returns>
    public static byte[] ScrambleText(this RoughScrambler self, string text)
    {
        var bin = Encoding.UTF8.GetBytes(text);
        return self.Scramble(bin);
    }

    /// <summary>オブジェクトをJSONシリアライズを介してスクランブルする。</summary>
    /// <remarks>このメソッドでスクランブルされたデータは <see cref="DescrambleObject(RoughScrambler, ReadOnlySpan{byte})"/> で解除することを想定している。</remarks>
    /// <typeparam name="T">対象オブジェクト型</typeparam>
    /// <param name="self">利用するスクランブル化処理インスタンス</param>
    /// <param name="value">スクランブルするオブジェクト</param>
    /// <returns>スクランブルされたバイト列</returns>
    public static byte[] ScrambleObject<T>(this RoughScrambler self, T value)
    {
        var json = JsonSerializer.Serialize(value);
        return self.ScrambleText(json);
    }
    #endregion 

    #region スクランブル解除
    /// <summary>テキストのスクランブル解除を行う</summary>
    /// <remarks>このメソッドは <see cref="ScrambleText(RoughScrambler, string)"/> でスクランブルされたデータを元に戻す目的のものとなる。</remarks>
    /// <param name="self">利用するスクランブル化処理インスタンス</param>
    /// <param name="bin">スクランブルされたデータ</param>
    /// <returns>スクランブル解除された文字列。解除失敗時は null を返却する。</returns>
    public static string? DescrambleText(this RoughScrambler self, ReadOnlySpan<byte> bin)
    {
        try
        {
            var decoded = self.Descramble(bin);
            return Encoding.UTF8.GetString(decoded);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>オブジェクトをJSONデシリアライズを介してスクランブル解除を行う</summary>
    /// <remarks>このメソッドは <see cref="ScrambleObject{T}(RoughScrambler, T)"/> でスクランブルされたデータを元に戻す目的のものとなる。</remarks>
    /// <typeparam name="T">対象オブジェクト型</typeparam>
    /// <param name="self">利用するスクランブル化処理インスタンス</param>
    /// <param name="bin">スクランブルされたデータ</param>
    /// <returns>スクランブル解除されたオブジェクト。解除失敗時は デフォルト値 を返却する。</returns>
    public static T? DescrambleObject<T>(this RoughScrambler self, ReadOnlySpan<byte> bin)
    {
        try
        {
            var json = self.DescrambleText(bin);
            if (json == null) return default;
            return JsonSerializer.Deserialize<T>(json);
        }
        catch
        {
            return default;
        }
    }
    #endregion 

}
