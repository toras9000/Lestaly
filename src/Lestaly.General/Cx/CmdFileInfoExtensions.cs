using System.Text;

namespace Lestaly.Cx;

/// <summary>
/// コマンドをシンプルに実行するための拡張メソッド
/// </summary>
[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:命名スタイル", Justification = "このクラスでは意図的に小文字メソッドを使っている")]
public static class CmdFileInfoExtensions
{
    // コンパイラのバグ？により、拡張メンバ書式だと nullable 警告が出てしまうので旧形式の記述をしている。
    /// <summary>ファイルを実行ファイルとみなして指定の引数でプロセス実行準備を行う</summary>
    /// <param name="self">コマンド実行結果を得るファイル</param>
    /// <param name="arguments">引数リスト</param>
    /// <returns>コマンド実行準備インスタンス</returns>
    public static CmdCx args(this FileInfo self, params ArgCx[] arguments)
        => new(self.FullName, arguments);

    /// <summary>ファイルを実行ファイルとみなしてプロセスを実行し、指定の終了コードを正常とする検証を行う</summary>
    /// <param name="self">コマンド実行結果を得るファイル</param>
    /// <param name="codes">正常とみなす終了コード。指定しない場合はゼロのみを正常とみなす。正常以外の終了コードの場合は例外を送出する。</param>
    /// <returns>コマンドの実行結果(出力と終了コード)を得るタスク。正常終了コードの場合のみ結果を得られる。</returns>
    public static Task<CmdResult> success(this FileInfo self, params int[] codes)
        => new CmdCx(self.FullName, []).result().success(codes);

    extension(FileInfo self)
    {
        /// <summary>ファイルを実行ファイルとみなしてプロセスを実行する</summary>
        /// <returns>コマンドの実行結果(出力と終了コード)を得るタスク。終了コードは検証されない。</returns>
        public Task<CmdResult> launch()
            => new CmdCx(self.FullName, []).result();

        /// <summary>ファイルを実行ファイルとみなし、コンソール出力なしのプロセス実行準備を行う</summary>
        /// <returns>コマンド実行準備インスタンス</returns>
        public CmdCx silent()
            => new CmdCx(self.FullName, []).silent();

        /// <summary>ファイルを実行ファイルとみなし、出力を指定のリダイレクト先とするプロセス実行準備を行う</summary>
        /// <param name="writer">出力リダイレクト先ライター</param>
        /// <returns>コマンド実行準備インスタンス</returns>
        public CmdCx redirect(TextWriter writer)
            => new CmdCx(self.FullName, []).redirect(writer);

        /// <summary>ファイルを実行ファイルとみなし、コンソール入力をアタッチするプロセス実行準備を行う</summary>
        /// <returns>コマンド実行準備インスタンス</returns>
        public CmdCx interactive()
            => new CmdCx(self.FullName, []).interactive();

        /// <summary>ファイルを実行ファイルとみなし、指定のリーダを入力とするプロセス実行準備を行う</summary>
        /// <param name="reader">入力リダイレクト元リーダー</param>
        /// <returns>コマンド実行準備インスタンス</returns>
        public CmdCx input(TextReader reader)
            => new CmdCx(self.FullName, []).input(reader);

        /// <summary>ファイルを実行ファイルとみなし、指定の文字列を入力とするプロセス実行準備を行う</summary>
        /// <param name="text">入力テキスト</param>
        /// <returns>コマンド実行準備インスタンス</returns>
        public CmdCx input(string text)
            => new CmdCx(self.FullName, []).input(text);

        /// <summary>ファイルを実行ファイルとみなし、指定の入出力エンコーディングとするプロセス実行準備を行う</summary>
        /// <param name="encoding">プロセス入出力エンコーディング</param>
        /// <returns>コマンド実行準備インスタンス</returns>
        public CmdCx encoding(Encoding encoding)
            => new CmdCx(self.FullName, []).encoding(encoding);

        /// <summary>ファイルを実行ファイルとみなし、呼び出しコマンドラインのエコーを構成するプロセス実行準備を行う</summary>
        /// <param name="prompt">呼び出しコマンドラインエコーの先頭に付与するプロンプト文字列。nullを指定するとエコーなしとみなす。</param>
        /// <returns>コマンド実行準備インスタンス</returns>
        public CmdCx echo(string prompt = ">")
            => new CmdCx(self.FullName, []).echo(prompt);

        /// <summary>ファイルを実行ファイルとみなし、ウィンドウを作成なしとするプロセス実行準備を行う</summary>
        /// <returns>コマンド実行準備インスタンス</returns>
        public CmdCx nowindow()
            => new CmdCx(self.FullName, []).nowindow();

        /// <summary>ファイルを実行ファイルとみなし、プロセスの実行動詞を指定するプロセス実行準備を行う</summary>
        /// <returns>自身のインスタンス</returns>
        public CmdCx verb(string verb)
            => new CmdCx(self.FullName, []).verb(verb);

        /// <summary>ファイルを実行ファイルとみなし、指定の作業ディレクトリとするプロセス実行準備を行う</summary>
        /// <param name="dir">作業ディレクトリ</param>
        /// <returns>コマンド実行準備インスタンス</returns>
        public CmdCx workdir(string dir)
            => new CmdCx(self.FullName, []).workdir(dir);

        /// <summary>ファイルを実行ファイルとみなし、指定の作業ディレクトリとするプロセス実行準備を行う</summary>
        /// <param name="dir">作業ディレクトリ</param>
        /// <returns>コマンド実行準備インスタンス</returns>
        public CmdCx workdir(DirectoryInfo dir)
            => new CmdCx(self.FullName, []).workdir(dir);

        /// <summary>ファイルを実行ファイルとみなし、指定の環境変数を追加するプロセス実行準備を行う</summary>
        /// <param name="key">環境変数名</param>
        /// <param name="value">環境変数値</param>
        /// <returns>コマンド実行準備インスタンス</returns>
        public CmdCx env(string key, string? value)
            => new CmdCx(self.FullName, []).env(key, value);

        /// <summary>ファイルを実行ファイルとみなし、指定のトークンでプロセス中止するプロセス実行準備を行う</summary>
        /// <param name="token">プロセス実行中止トークン</param>
        /// <returns>コマンド実行準備インスタンス</returns>
        public CmdCx killby(CancellationToken token)
            => new CmdCx(self.FullName, []).killby(token);

        /// <summary>ファイルを実行ファイルとみなし、指定のトークンでのプロセス中止と中止時キャンセル例外変換のプロセス実行準備を行う</summary>
        /// <param name="token">プロセス実行中止トークン</param>
        /// <returns>コマンド実行準備インスタンス</returns>
        public CmdCx cancelby(CancellationToken token)
            => new CmdCx(self.FullName, []).cancelby(token);
    }
}

