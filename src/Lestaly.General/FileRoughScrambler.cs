using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Metadata;

namespace Lestaly;

/// <summary>
/// ファイルと紐づけて雑なデータスクランブル処理クラス
/// </summary>
/// <remarks>
/// このクラスは単純にファイルと <see cref="RoughScrambler"/> をペアで扱いシンプル化するためのものとなる。
/// </remarks>
public class FileRoughScrambler
{
    // 構築
    #region コンストラクタ
    /// <summary>ファイルと暗号化の情報を指定するコンストラクタ</summary>
    /// <param name="file">読み書き対象ファイル</param>
    /// <param name="purpose">任意の目的文字列</param>
    /// <param name="context">コンテキスト文字列。指定しても良いが、通常は指定省略して呼び出し元のファイルパスを渡す形を想定している。</param>
    /// <param name="envtokens">暗号キーを作るために利用する環境固有文字列のリスト。省略するとマシン名(ホスト名)とユーザ名を利用する</param>
    public FileRoughScrambler(FileInfo file, string purpose = "", [CallerFilePath] string context = "", string[]? envtokens = default)
    {
        ArgumentNullException.ThrowIfNull(file);

        this.File = file;
        this.Scrambler = new RoughScrambler(purpose, context, envtokens);
    }
    #endregion

    // 公開プロパティ
    #region 紐づけ情報
    /// <summary>読み書き対象ファイル</summary>
    public FileInfo File { get; }

    /// <summary>スクランブル処理</summary>
    public RoughScrambler Scrambler { get; }
    #endregion

    // 公開メソッド
    #region スクランブル化
    /// <summary>テキストをスクランブルしてファイルに保存する。</summary>
    /// <param name="text">スクランブルする文字列</param>
    /// <param name="options">ファイルストリームを開くオプション。Access プロパティは無視する。</param>
    /// <param name="ignoreErr">保存エラーを無視するか否か</param>
    public void ScrambleText(string text, FileStreamOptions? options = null, bool ignoreErr = false)
        => this.Scrambler.ScrambleTextToFile(this.File, text, options, ignoreErr);

    /// <summary>テキストをスクランブルしてファイルに保存する。</summary>
    /// <param name="text">スクランブルする文字列</param>
    /// <param name="options">ファイルストリームを開くオプション。Access プロパティは無視する。</param>
    /// <param name="ignoreErr">保存エラーを無視するか否か</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    public ValueTask ScrambleTextAsync(string text, FileStreamOptions? options = null, bool ignoreErr = false, CancellationToken cancelToken = default)
        => this.Scrambler.ScrambleTextToFileAsync(this.File, text, options, ignoreErr, cancelToken);

    /// <summary>オブジェクトをJSONシリアライズを介してスクランブルしファイルに保存する。</summary>
    /// <typeparam name="T">対象オブジェクト型</typeparam>
    /// <param name="value">スクランブルするオブジェクト</param>
    /// <param name="options">ファイルストリームを開くオプション。Access プロパティは無視する。</param>
    /// <param name="ignoreErr">保存エラーを無視するか否か</param>
    public void ScrambleObject<T>(T value, FileStreamOptions? options = null, bool ignoreErr = false)
        => this.Scrambler.ScrambleObjectToFile(this.File, value, options, ignoreErr);

    /// <summary>オブジェクトをJSONシリアライズを介してスクランブルしファイルに保存する。</summary>
    /// <typeparam name="T">対象オブジェクト型</typeparam>
    /// <param name="value">スクランブルするオブジェクト</param>
    /// <param name="typeInfo">変換メタデータ</param>
    /// <param name="options">ファイルストリームを開くオプション。Access プロパティは無視する。</param>
    /// <param name="ignoreErr">保存エラーを無視するか否か</param>
    public void ScrambleObject<T>(T value, JsonTypeInfo<T> typeInfo, FileStreamOptions? options = null, bool ignoreErr = false)
        => this.Scrambler.ScrambleObjectToFile(this.File, value, typeInfo, options, ignoreErr);

    /// <summary>オブジェクトをJSONシリアライズを介してスクランブルしファイルに保存する。</summary>
    /// <typeparam name="T">対象オブジェクト型</typeparam>
    /// <param name="value">スクランブルするオブジェクト</param>
    /// <param name="options">ファイルストリームを開くオプション。Access プロパティは無視する。</param>
    /// <param name="ignoreErr">保存エラーを無視するか否か</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    public ValueTask ScrambleObjectAsync<T>(T value, FileStreamOptions? options = null, bool ignoreErr = false, CancellationToken cancelToken = default)
        => this.Scrambler.ScrambleObjectToFileAsync(this.File, value, options, ignoreErr, cancelToken);

    /// <summary>オブジェクトをJSONシリアライズを介してスクランブルしファイルに保存する。</summary>
    /// <typeparam name="T">対象オブジェクト型</typeparam>
    /// <param name="value">スクランブルするオブジェクト</param>
    /// <param name="typeInfo">変換メタデータ</param>
    /// <param name="options">ファイルストリームを開くオプション。Access プロパティは無視する。</param>
    /// <param name="ignoreErr">保存エラーを無視するか否か</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    public ValueTask ScrambleObjectAsync<T>(T value, JsonTypeInfo<T> typeInfo, FileStreamOptions? options = null, bool ignoreErr = false, CancellationToken cancelToken = default)
        => this.Scrambler.ScrambleObjectToFileAsync(this.File, value, typeInfo, options, ignoreErr, cancelToken);
    #endregion

    #region スクランブル解除
    /// <summary>ファイルから読み込んでテキストのスクランブル解除を行う。</summary>
    /// <returns>スクランブル解除したテキスト。失敗時はnullを返却。</returns>
    public string? DescrambleText()
        => this.Scrambler.DescrambleTextFromFile(this.File);

    /// <summary>ファイルから読み込んでテキストのスクランブル解除を行う。</summary>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>スクランブル解除したテキスト。失敗時はnullを返却。</returns>
    public ValueTask<string?> DescrambleTextAsync(CancellationToken cancelToken = default)
        => this.Scrambler.DescrambleTextFromFileAsync(this.File, cancelToken);

    /// <summary>ファイルから読み込んでJSONデシリアライズを介してオブジェクトのスクランブル解除を行う</summary>
    /// <typeparam name="T">対象オブジェクト型</typeparam>
    /// <returns>スクランブル解除したオブジェクト。失敗時はnullを返却。</returns>
    public T? DescrambleObject<T>()
        => this.Scrambler.DescrambleObjectFromFile<T>(this.File);

    /// <summary>ファイルから読み込んでJSONデシリアライズを介してオブジェクトのスクランブル解除を行う</summary>
    /// <typeparam name="T">対象オブジェクト型</typeparam>
    /// <param name="typeInfo">変換メタデータ</param>
    /// <returns>スクランブル解除したオブジェクト。失敗時はnullを返却。</returns>
    public T? DescrambleObject<T>(JsonTypeInfo<T> typeInfo)
        => this.Scrambler.DescrambleObjectFromFile<T>(this.File, typeInfo);

    /// <summary>ファイルから読み込んでJSONデシリアライズを介してオブジェクトのスクランブル解除を行う</summary>
    /// <typeparam name="T">対象オブジェクト型</typeparam>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>スクランブル解除したオブジェクト。失敗時はnullを返却。</returns>
    public ValueTask<T?> DescrambleObjectAsync<T>(CancellationToken cancelToken = default)
        => this.Scrambler.DescrambleObjectFromFileAsync<T>(this.File, cancelToken);

    /// <summary>ファイルから読み込んでJSONデシリアライズを介してオブジェクトのスクランブル解除を行う</summary>
    /// <typeparam name="T">対象オブジェクト型</typeparam>
    /// <param name="typeInfo">変換メタデータ</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>スクランブル解除したオブジェクト。失敗時はnullを返却。</returns>
    public ValueTask<T?> DescrambleObjectAsync<T>(JsonTypeInfo<T> typeInfo, CancellationToken cancelToken = default)
        => this.Scrambler.DescrambleObjectFromFileAsync<T>(this.File, typeInfo, cancelToken);
    #endregion
}
