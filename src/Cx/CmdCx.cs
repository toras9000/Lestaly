using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace Lestaly.Cx;

/// <summary>
/// プロセス実行準備クラス
/// </summary>
public class CmdCx
{
    // 構築
    #region コンストラクタ
    /// <summary>コマンドライン文字列を指定するコンストラクタ</summary>
    /// <param name="commandline">コマンドライン文字列</param>
    public CmdCx(string commandline)
    {
        // コマンド引数があるかチェック
        var sepaIdx = commandline.IndexOf(' ');
        if (sepaIdx < 0)
        {
            // 引数が無ければそのままコマンドとする
            this.command = commandline;
            this.argumenter = null;
        }
        else
        {
            // 引数があれば分割して引数として呼び出し
            this.command = commandline[..sepaIdx];
            this.argumenter = t => t.Arguments = commandline[(sepaIdx + 1)..];
        }
    }

    /// <summary>コマンドと引数リストを指定するコンストラクタ</summary>
    /// <param name="command">コマンド名</param>
    /// <param name="arguments">引数リスト</param>
    public CmdCx(string command, params string[] arguments)
    {
        this.command = command;
        this.argumenter = CmdProc.listArgumenter(arguments);
    }
    #endregion

    // 公開メソッド
    #region 実行準備
    /// <summary>呼び出しプロセスからのコンソール出力無しに構成する</summary>
    /// <remarks>
    /// このメソッドは <see cref="redirect(TextWriter)"/> の構成を上書きする。
    /// </remarks>
    /// <returns>自身のインスタンス</returns>
    public CmdCx silent()
    {
        this.outWriter = TextWriter.Null;
        return this;
    }

    /// <summary>呼び出しプロセスの標準出力/標準エラーを指定したライターにリダイレクトする構成を行う</summary>
    /// <remarks>
    /// このメソッドは <see cref="silent"/> の構成を上書きする。
    /// 指定したライターはプロセスの実行が終わってもクローズ/破棄されることはない。
    /// </remarks>
    /// <param name="writer">出力ライター</param>
    /// <returns>自身のインスタンス</returns>
    public CmdCx redirect(TextWriter writer)
    {
        this.outWriter = writer;
        return this;
    }

    /// <summary>コンソール入力を呼び出しプロセスの標準入力にリダイレクトする構成を行う</summary>
    /// <remarks>
    /// このメソッドは <see cref="input(TextReader)"/> の構成を上書きする。
    /// 指定したライターはプロセスの実行が終わってもクローズ/破棄されることはない。
    /// </remarks>
    /// <returns>自身のインスタンス</returns>
    public CmdCx interactive()
    {
        this.inReader = ConsoleWig.InReader;
        return this;
    }

    /// <summary>指定したリーダーから呼び出しプロセスの標準入力へリダイレクトする構成を行う</summary>
    /// <remarks>
    /// このメソッドは <see cref="interactive"/> の構成を上書きする。
    /// 指定したリーダーはプロセスの実行が終わってもクローズ/破棄されることはない。
    /// </remarks>
    /// <param name="reader">入力リーダー</param>
    /// <returns>自身のインスタンス</returns>
    public CmdCx input(TextReader reader)
    {
        this.inReader = reader;
        return this;
    }

    /// <summary>呼び出しプロセスの入出力エンコーディングを構成を行う</summary>
    /// <param name="encoding">プロセス入出力エンコーディング</param>
    /// <returns>自身のインスタンス</returns>
    public CmdCx encoding(Encoding encoding)
    {
        this.ioEnc = encoding;
        return this;
    }

    /// <summary>呼び出しプロセスの環境変数キー/値を構成する</summary>
    /// <remarks>
    /// このメソッドは複数回呼び出して変数定義を追加できる。
    /// </remarks>
    /// <param name="key">環境変数名</param>
    /// <param name="value">環境変数値</param>
    /// <returns>自身のインスタンス</returns>
    public CmdCx env(string key, string? value)
    {
        (this.envVars ??= new())[key] = value;
        return this;
    }

    /// <summary>呼び出しプロセスの環境変数キー/値を構成する</summary>
    /// <remarks>
    /// このメソッドは複数回呼び出して変数定義を追加できる。
    /// </remarks>
    /// <param name="environments">環境変数キー/値シーケンス</param>
    /// <returns>自身のインスタンス</returns>
    public CmdCx env(IEnumerable<KeyValuePair<string, string?>> environments)
    {
        var vars = this.envVars ??= new();
        foreach (var env in environments)
        {
            vars.Add(env.Key, env.Value);
        }
        return this;
    }

    /// <summary>呼び出しプロセスの中止トークンを構成する</summary>
    /// <param name="token">中止トークン</param>
    /// <returns>自身のインスタンス</returns>
    public CmdCx killby(CancellationToken token)
    {
        this.cancelToken = token;
        return this;
    }
    #endregion

    #region 実行
    /// <summary>準備した構成に従ってプロセスを実行する</summary>
    /// <returns>コマンドの実行結果(出力と終了コード)を得るタスク</returns>
    public Task<CmdResult> result()
        => execAsync();

    /// <summary>非同期待機オブジェクトを取得する</summary>
    /// <remarks>オブジェクトを await するためのメソッド</remarks>
    /// <returns>非同期待機オブジェクト</returns>
    public TaskAwaiter<CmdResult> GetAwaiter()
        => execAsync().GetAwaiter();
    #endregion

    // 非公開フィールド
    #region 実行準備情報
    /// <summary>呼び出しコマンド</summary>
    private string command;

    /// <summary>コマンド引数</summary>
    private Action<ProcessStartInfo>? argumenter;

    /// <summary>入力リダイレクト元リーダ</summary>
    private TextReader? inReader;

    /// <summary>出力リダイレクト先ライタ</summary>
    private TextWriter? outWriter;

    /// <summary>入出力エンコーディング</summary>
    private Encoding? ioEnc;

    /// <summary>環境変数</summary>
    private Dictionary<string, string?>? envVars;

    /// <summary>中止トークン</summary>
    private CancellationToken cancelToken;
    #endregion

    // 非公開型
    #region リソース管理
    /// <summary>
    /// リダイレクト用リソース管理データ型
    /// </summary>
    private sealed class RedirectWriter : IDisposable
    {
        /// <summary>管理リソースを受け取るコンストラクタ</summary>
        /// <param name="stdout">標準出力のリダイレクト用ライター</param>
        /// <param name="stderr">標準エラーのリダイレクト用ライター</param>
        public RedirectWriter(TeeWriter stdout, TeeWriter stderr)
        {
            this.stdout = stdout;
            this.stderr = stderr;
        }

        /// <summary>標準出力のリダイレクト用ライター</summary>
        public TeeWriter stdout { get; private set; }

        /// <summary>標準エラーのリダイレクト用ライター</summary>
        public TeeWriter stderr { get; private set; }

        /// <summary>リソースを破棄</summary>
        public void Dispose()
        {
            this.stdout?.Dispose();
            this.stderr?.Dispose();
            this.stdout = null!;
            this.stderr = null!;
        }
    }
    #endregion

    // 非公開メソッド
    #region 実行
    /// <summary>プロセスを実行する</summary>
    /// <returns>マンドの実行結果(出力と終了コード)を得るタスク。</returns>
    private async Task<CmdResult> execAsync()
    {
        // プロセス出力のリダイレクト先ライターを生成する
        using var redirect = createRedirectWriter(out var sniffer);

        // 準備した情報でプロセス実行する
        var exit = await CmdProc.execAsync(
            this.command,
            this.argumenter,
            environments: this.envVars,
            stdIn: this.inReader,
            inEncoding: this.ioEnc,
            stdOut: redirect.stdout,
            stdErr: redirect.stderr,
            outEncoding: this.ioEnc,
            cancelToken: this.cancelToken
        ).ConfigureAwait(false);

        // リダイレクトされた出力を文字列化
        var outputText = sniffer.ToString();

        return new(exit.Code, outputText);
    }

    /// <summary>リダイレクト先ライターを生成する</summary>
    /// <param name="sniffer">リダイレクトを横取りするための文字列ライター</param>
    /// <returns></returns>
    private RedirectWriter createRedirectWriter(out StringWriter sniffer)
    {
        // 出力を拾うための文字列ライタを生成
        var syncStrWriter = TextWriter.Synchronized(sniffer = new StringWriter());

        // 標準出力/標準エラーのリダイレクト先ライターを生成
        // これは Dispose するので Bind() する。
        var stdout = new TeeWriter().Bind(syncStrWriter);
        var stderr = new TeeWriter().Bind(syncStrWriter);

        // 出力先が指定されているか
        if (this.outWriter == null)
        {
            // 出力先指定が無い場合はデフォルトのコンソールをアタッチする。これは Dispose しないので With() する。
            stdout.With(Console.Out);
            stderr.With(Console.Error);
        }
        else if (this.outWriter != TextWriter.Null)
        {
            // TextWriter.Null はリダイレクト無しのフラグとして利用している。
            // それ以外のインスタンスが指定されていればそれをリダイレクト先とする。これは Dispose しないので With() する。
            var syncOutWriter = TextWriter.Synchronized(this.outWriter);
            stdout.With(syncOutWriter);
            stderr.With(syncOutWriter);
        }

        return new(stdout, stderr);
    }
    #endregion
}