using System.Runtime.CompilerServices;
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

    #region スクランブル化：ファイル保存
    /// <summary>テキストをスクランブルしてファイルに保存する。</summary>
    /// <param name="self">利用するスクランブル化処理インスタンス</param>
    /// <param name="file">保存先ファイル情報</param>
    /// <param name="text">スクランブルする文字列</param>
    /// <param name="options">ファイルストリームを開くオプション。Access プロパティは無視する。</param>
    /// <param name="ignoreErr">保存エラーを無視するか否か</param>
    public static void ScrambleTextToFile(this RoughScrambler self, FileInfo file, string text, FileStreamOptions? options = null, bool ignoreErr = false)
    {
        ArgumentNullException.ThrowIfNull(file);
        try
        {
            var bin = self.ScrambleText(text);
            file.WriteAllBytes(bin, options);
        }
        catch when (ignoreErr) { }
    }

    /// <summary>テキストをスクランブルしてファイルに保存する。</summary>
    /// <param name="self">利用するスクランブル化処理インスタンス</param>
    /// <param name="file">保存先ファイル情報</param>
    /// <param name="text">スクランブルする文字列</param>
    /// <param name="options">ファイルストリームを開くオプション。Access プロパティは無視する。</param>
    /// <param name="ignoreErr">保存エラーを無視するか否か</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    public static async ValueTask ScrambleTextToFileAsync(this RoughScrambler self, FileInfo file, string text, FileStreamOptions? options = null, bool ignoreErr = false, CancellationToken cancelToken = default)
    {
        ArgumentNullException.ThrowIfNull(file);
        try
        {
            var bin = self.ScrambleText(text);
            await file.WriteAllBytesAsync(bin, options, cancelToken).ConfigureAwait(false);
        }
        catch when (ignoreErr) { }
    }

    /// <summary>オブジェクトをJSONシリアライズを介してスクランブルしファイルに保存する。</summary>
    /// <typeparam name="T">対象オブジェクト型</typeparam>
    /// <param name="self">利用するスクランブル化処理インスタンス</param>
    /// <param name="file">保存先ファイル情報</param>
    /// <param name="value">スクランブルするオブジェクト</param>
    /// <param name="options">ファイルストリームを開くオプション。Access プロパティは無視する。</param>
    /// <param name="ignoreErr">保存エラーを無視するか否か</param>
    public static void ScrambleObjectToFile<T>(this RoughScrambler self, FileInfo file, T value, FileStreamOptions? options = null, bool ignoreErr = false)
    {
        ArgumentNullException.ThrowIfNull(file);
        try
        {
            var bin = self.ScrambleObject(value);
            file.WriteAllBytes(bin, options);
        }
        catch when (ignoreErr) { }
    }

    /// <summary>オブジェクトをJSONシリアライズを介してスクランブルしファイルに保存する。</summary>
    /// <typeparam name="T">対象オブジェクト型</typeparam>
    /// <param name="self">利用するスクランブル化処理インスタンス</param>
    /// <param name="file">保存先ファイル情報</param>
    /// <param name="value">スクランブルするオブジェクト</param>
    /// <param name="options">ファイルストリームを開くオプション。Access プロパティは無視する。</param>
    /// <param name="ignoreErr">保存エラーを無視するか否か</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    public static async ValueTask ScrambleObjectToFileAsync<T>(this RoughScrambler self, FileInfo file, T value, FileStreamOptions? options = null, bool ignoreErr = false, CancellationToken cancelToken = default)
    {
        ArgumentNullException.ThrowIfNull(file);
        try
        {
            var bin = self.ScrambleObject(value);
            await file.WriteAllBytesAsync(bin, options, cancelToken).ConfigureAwait(false);
        }
        catch when (ignoreErr) { }
    }
    #endregion

    #region スクランブル解除：ファイル読込
    /// <summary>ファイルから読み込んでテキストのスクランブル解除を行う。</summary>
    /// <param name="self">利用するスクランブル化処理インスタンス</param>
    /// <param name="file">読込元ファイル情報</param>
    /// <returns>スクランブル解除したテキスト。失敗時はnullを返却。</returns>
    public static string? DescrambleTextFromFile(this RoughScrambler self, FileInfo file)
    {
        ArgumentNullException.ThrowIfNull(file);
        try
        {
            var bin = file.ReadAllBytes();
            var text = self.DescrambleText(bin);
            return text;
        }
        catch
        {
            return default;
        }
    }

    /// <summary>ファイルから読み込んでテキストのスクランブル解除を行う。</summary>
    /// <param name="self">利用するスクランブル化処理インスタンス</param>
    /// <param name="file">読込元ファイル情報</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>スクランブル解除したテキスト。失敗時はnullを返却。</returns>
    public static async ValueTask<string?> DescrambleTextFromFileAsync(this RoughScrambler self, FileInfo file, CancellationToken cancelToken = default)
    {
        ArgumentNullException.ThrowIfNull(file);
        try
        {
            var bin = await file.ReadAllBytesAsync(cancelToken).ConfigureAwait(false);
            var text = self.DescrambleText(bin);
            return text;
        }
        catch
        {
            return default;
        }
    }

    /// <summary>ファイルから読み込んでJSONデシリアライズを介してオブジェクトのスクランブル解除を行う</summary>
    /// <typeparam name="T">対象オブジェクト型</typeparam>
    /// <param name="self">利用するスクランブル化処理インスタンス</param>
    /// <param name="file">読込元ファイル情報</param>
    /// <returns>スクランブル解除したオブジェクト。失敗時はnullを返却。</returns>
    public static T? DescrambleObjectFromFile<T>(this RoughScrambler self, FileInfo file)
    {
        ArgumentNullException.ThrowIfNull(file);
        try
        {
            var bin = file.ReadAllBytes();
            var value = self.DescrambleObject<T>(bin);
            return value;
        }
        catch
        {
            return default;
        }
    }

    /// <summary>ファイルから読み込んでJSONデシリアライズを介してオブジェクトのスクランブル解除を行う</summary>
    /// <typeparam name="T">対象オブジェクト型</typeparam>
    /// <param name="self">利用するスクランブル化処理インスタンス</param>
    /// <param name="file">読込元ファイル情報</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>スクランブル解除したオブジェクト。失敗時はnullを返却。</returns>
    public static async ValueTask<T?> DescrambleObjectFromFileAsync<T>(this RoughScrambler self, FileInfo file, CancellationToken cancelToken = default)
    {
        ArgumentNullException.ThrowIfNull(file);
        try
        {
            var bin = await file.ReadAllBytesAsync(cancelToken).ConfigureAwait(false);
            var value = self.DescrambleObject<T>(bin);
            return value;
        }
        catch
        {
            return default;
        }
    }
    #endregion

    #region 紐づけ
    /// <summary>ファイルと紐づけた雑スクランブル処理を生成する</summary>
    /// <param name="self">読み書き対象ファイル</param>
    /// <param name="purpose">任意の目的文字列</param>
    /// <param name="context">コンテキスト文字列。指定しても良いが、通常は指定省略して呼び出し元のファイルパスを渡す形を想定している。</param>
    /// <returns>ファイルと紐づけた雑スクランブル処理</returns>
    public static FileRoughScrambler CreateScrambler(this FileInfo self, string purpose = "", [CallerFilePath] string context = "")
        => new FileRoughScrambler(self, purpose, context);
    #endregion
}
