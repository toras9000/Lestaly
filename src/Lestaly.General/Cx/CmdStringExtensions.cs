using System.Runtime.CompilerServices;
using System.Text;

namespace Lestaly.Cx;

/// <summary>
/// コマンドをシンプルに実行するための拡張メソッド
/// </summary>
[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:命名スタイル", Justification = "このクラスでは意図的に小文字メソッドを使っている")]
public static class CmdStringExtensions
{
    /// <summary>文字列をコマンドラインとみなしてプロセスを実行する</summary>
    /// <remarks>
    /// このメソッドは ProcessX.Zx にインスパイアされたものとなる。
    /// ただし互換性はない。google/zx は一切意識せず、ディレクトリ変更などもサポートしない。
    /// また、標準エラーに出力がある場合をエラーとは扱わない。
    /// 実行するプロセスの終了コードが 0 の場合のみを成功とみなし、それ以外のコード値はエラー(例外送出)とする。
    /// </remarks>
    /// <param name="commandline">コマンドライン文字列。最初の空白より後ろを引数とみなす。</param>
    /// <returns>コマンドの標準出力と標準エラーのテキストを得るAwaiter。出力テキストは正常終了コードの場合のみ得られる。</returns>
    public static TaskAwaiter<string> GetAwaiter(this string commandline)
        => new CmdCx(commandline).result().success().output().GetAwaiter();

    /// <summary>文字列をコマンドラインとみなしてプロセスを実行する</summary>
    /// <param name="commandline">コマンドライン文字列。最初の空白より後ろを引数とみなす。</param>
    /// <returns>コマンドの実行結果(出力と終了コード)を得るタスク。終了コードは検証されない。</returns>
    public static Task<CmdResult> result(this string commandline)
        => new CmdCx(commandline).result();

    /// <summary>文字列をコマンドラインとみなしてプロセスを実行し、指定の終了コードを正常とする検証を行う</summary>
    /// <param name="commandline">コマンドライン文字列。最初の空白より後ろを引数とみなす。</param>
    /// <param name="codes">正常とみなす終了コード。指定しない場合はゼロのみを正常とみなす。正常以外の終了コードの場合は例外を送出する。</param>
    /// <returns>コマンドの実行結果(出力と終了コード)を得るタスク。正常終了コードの場合のみ結果を得られる。</returns>
    public static Task<CmdResult> success(this string commandline, params int[] codes)
        => new CmdCx(commandline).result().success(codes);

    /// <summary>文字列をコマンドとみなして指定の引数でプロセス実行準備を行う</summary>
    /// <param name="command">コマンド文字列。全体を実行コマンドとみなす。</param>
    /// <param name="arguments">引数リスト</param>
    /// <returns>コマンド実行準備インスタンス</returns>
    public static CmdCx args(this string command, params ArgCx[] arguments)
        => new(command, arguments);

    /// <summary>文字列をコマンドラインとみなし、コンソール出力なしのプロセス実行準備を行う</summary>
    /// <param name="commandline">コマンドライン文字列。最初の空白より後ろを引数とみなす。</param>
    /// <returns>コマンド実行準備インスタンス</returns>
    public static CmdCx silent(this string commandline)
        => new CmdCx(commandline).silent();

    /// <summary>文字列をコマンドラインとみなし、出力を指定のリダイレクト先とするプロセス実行準備を行う</summary>
    /// <param name="commandline">コマンドライン文字列。最初の空白より後ろを引数とみなす。</param>
    /// <param name="writer">出力リダイレクト先ライター</param>
    /// <returns>コマンド実行準備インスタンス</returns>
    public static CmdCx redirect(this string commandline, TextWriter writer)
        => new CmdCx(commandline).redirect(writer);

    /// <summary>文字列をコマンドラインとみなし、コンソール入力をアタッチするプロセス実行準備を行う</summary>
    /// <param name="commandline">コマンドライン文字列。最初の空白より後ろを引数とみなす。</param>
    /// <returns>コマンド実行準備インスタンス</returns>
    public static CmdCx interactive(this string commandline)
        => new CmdCx(commandline).interactive();

    /// <summary>文字列をコマンドラインとみなし、指定のリーダを入力とするプロセス実行準備を行う</summary>
    /// <param name="commandline">コマンドライン文字列。最初の空白より後ろを引数とみなす。</param>
    /// <param name="reader">入力リダイレクト元リーダー</param>
    /// <returns>コマンド実行準備インスタンス</returns>
    public static CmdCx input(this string commandline, TextReader reader)
        => new CmdCx(commandline).input(reader);

    /// <summary>文字列をコマンドラインとみなし、指定の文字列を入力とするプロセス実行準備を行う</summary>
    /// <param name="commandline">コマンドライン文字列。最初の空白より後ろを引数とみなす。</param>
    /// <param name="text">入力テキスト</param>
    /// <returns>コマンド実行準備インスタンス</returns>
    public static CmdCx input(this string commandline, string text)
        => new CmdCx(commandline).input(text);

    /// <summary>文字列をコマンドラインとみなし、指定の入出力エンコーディングとするプロセス実行準備を行う</summary>
    /// <param name="commandline">コマンドライン文字列。最初の空白より後ろを引数とみなす。</param>
    /// <param name="encoding">プロセス入出力エンコーディング</param>
    /// <returns>コマンド実行準備インスタンス</returns>
    public static CmdCx encoding(this string commandline, Encoding encoding)
        => new CmdCx(commandline).encoding(encoding);

    /// <summary>文字列をコマンドラインとみなし、呼び出しコマンドラインのエコーを構成するプロセス実行準備を行う</summary>
    /// <param name="commandline">コマンドライン文字列。最初の空白より後ろを引数とみなす。</param>
    /// <param name="prompt">呼び出しコマンドラインエコーの先頭に付与するプロンプト文字列。nullを指定するとエコーなしとみなす。</param>
    /// <returns>コマンド実行準備インスタンス</returns>
    public static CmdCx echo(this string commandline, string prompt = ">")
        => new CmdCx(commandline).echo(prompt);

    /// <summary>文字列をコマンドラインとみなし、ウィンドウを作成なしとするプロセス実行準備を行う</summary>
    /// <param name="commandline">コマンドライン文字列。最初の空白より後ろを引数とみなす。</param>
    /// <returns>コマンド実行準備インスタンス</returns>
    public static CmdCx nowindow(this string commandline)
        => new CmdCx(commandline).nowindow();

    /// <summary>文字列をコマンドラインとみなし、プロセスの実行動詞を指定するプロセス実行準備を行う</summary>
    /// <returns>自身のインスタンス</returns>
    public static CmdCx verb(this string commandline, string verb)
        => new CmdCx(commandline).verb(verb);

    /// <summary>文字列をコマンドラインとみなし、指定の作業ディレクトリとするプロセス実行準備を行う</summary>
    /// <param name="commandline">コマンドライン文字列。最初の空白より後ろを引数とみなす。</param>
    /// <param name="dir">作業ディレクトリ</param>
    /// <returns>コマンド実行準備インスタンス</returns>
    public static CmdCx workdir(this string commandline, string dir)
        => new CmdCx(commandline).workdir(dir);

    /// <summary>文字列をコマンドラインとみなし、指定の作業ディレクトリとするプロセス実行準備を行う</summary>
    /// <param name="commandline">コマンドライン文字列。最初の空白より後ろを引数とみなす。</param>
    /// <param name="dir">作業ディレクトリ</param>
    /// <returns>コマンド実行準備インスタンス</returns>
    public static CmdCx workdir(this string commandline, DirectoryInfo dir)
        => new CmdCx(commandline).workdir(dir);

    /// <summary>文字列をコマンドラインとみなし、指定の環境変数を追加するプロセス実行準備を行う</summary>
    /// <param name="commandline">コマンドライン文字列。最初の空白より後ろを引数とみなす。</param>
    /// <param name="key">環境変数名</param>
    /// <param name="value">環境変数値</param>
    /// <returns>コマンド実行準備インスタンス</returns>
    public static CmdCx env(this string commandline, string key, string? value)
        => new CmdCx(commandline).env(key, value);

    /// <summary>文字列をコマンドラインとみなし、指定のトークンでプロセス中止するプロセス実行準備を行う</summary>
    /// <param name="commandline">コマンドライン文字列。最初の空白より後ろを引数とみなす。</param>
    /// <param name="token">プロセス実行中止トークン</param>
    /// <returns>コマンド実行準備インスタンス</returns>
    public static CmdCx killby(this string commandline, CancellationToken token)
        => new CmdCx(commandline).killby(token);

    /// <summary>文字列をコマンドラインとみなし、指定のトークンでのプロセス中止と中止時キャンセル例外変換のプロセス実行準備を行う</summary>
    /// <param name="commandline">コマンドライン文字列。最初の空白より後ろを引数とみなす。</param>
    /// <param name="token">プロセス実行中止トークン</param>
    /// <returns>コマンド実行準備インスタンス</returns>
    public static CmdCx cancelby(this string commandline, CancellationToken token)
        => new CmdCx(commandline).cancelby(token);
}

