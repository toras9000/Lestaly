using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace Lestaly.Cx;

/// <summary>プロセス実行用の引数</summary>
public readonly struct ArgCx
{
    /// <summary>コンストラクタ</summary>
    /// <param name="str">引数値</param>
    public ArgCx(string str) => this.Value = str;

    /// <summary>文字列から ArgCx への暗黙変換オペレータ</summary>
    /// <param name="str">文字列</param>
    public static implicit operator ArgCx(string? str) => new ArgCx(str ?? "");

    /// <summary>文字列スパンから ArgCx への暗黙変換オペレータ</summary>
    /// <param name="span">文字列スパン</param>
    public static implicit operator ArgCx(ReadOnlySpan<char> span) => new ArgCx(span.ToString());

    /// <summary>ファイルシステム項目から ArgCx への暗黙変換オペレータ</summary>
    /// <param name="info">ファイルシステム項目。フルパスとして評価される</param>
    public static implicit operator ArgCx(FileSystemInfo info) => new ArgCx(info.FullName);

    /// <summary>引数値</summary>
    public string Value { get; }
}

/// <summary>
/// プロセス実行準備クラス
/// </summary>
[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:命名スタイル", Justification = "このクラスでは意図的に小文字メソッドを使っている")]
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
    public CmdCx(string command, params ArgCx[] arguments)
    {
        this.command = command;
        this.argumenter = target =>
        {
            foreach (var arg in arguments)
            {
                target.ArgumentList.Add(arg.Value);
            }
        };
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

    /// <summary>呼び出しプロセス標準出力/標準エラーから指定したライターへのリダイレクトを構成する</summary>
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

    /// <summary>コンソール入力から呼び出しプロセス標準入力へのリダイレクトを構成する</summary>
    /// <remarks>
    /// このメソッドは <see cref="input(TextReader)"/> の構成を上書きする。
    /// 指定したライターはプロセスの実行が終わってもクローズ/破棄されることはない。
    /// </remarks>
    /// <returns>自身のインスタンス</returns>
    public CmdCx interactive()
        => this.input(ConsoleWig.InReader);

    /// <summary>指定したリーダーから呼び出しプロセス標準入力へのリダイレクトを構成する</summary>
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

    /// <summary>指定したテキストから呼び出しプロセス標準入力へのリダイレクトを構成する</summary>
    /// <remarks>
    /// このメソッドは <see cref="interactive"/> の構成を上書きする。
    /// </remarks>
    /// <param name="text">入力テキストー</param>
    /// <returns>自身のインスタンス</returns>
    public CmdCx input(string text)
        => this.input(new StringReader(text));

    /// <summary>呼び出しコマンドラインのエコーを構成する</summary>
    /// <param name="prompt">呼び出しコマンドラインエコーの先頭に付与するプロンプト文字列。nullを指定するとエコーなしとみなす。</param>
    /// <returns>自身のインスタンス</returns>
    public CmdCx echo(string? prompt = ">")
    {
        this.echoPrompt = prompt;
        return this;
    }

    /// <summary>ウィンドウを作成せずに実行する</summary>
    /// <remarks>
    /// この指定が効果を発する。
    /// </remarks>
    /// <returns>自身のインスタンス</returns>
    public CmdCx nowindow()
    {
        this.noWindow = true;
        return this;
    }

    /// <summary>呼び出しプロセスの作業ディレクトリを構成する</summary>
    /// <param name="dir">作業ディレクトリ</param>
    /// <returns>自身のインスタンス</returns>
    public CmdCx workdir(DirectoryInfo dir)
        => this.workdir(dir.FullName);

    /// <summary>呼び出しプロセスの作業ディレクトリを構成する</summary>
    /// <param name="dir">作業ディレクトリ</param>
    /// <returns>自身のインスタンス</returns>
    public CmdCx workdir(string dir)
    {
        this.workDir = dir;
        return this;
    }

    /// <summary>呼び出しプロセスの入出力エンコーディングを構成する</summary>
    /// <param name="encoding">プロセス入出力エンコーディング</param>
    /// <returns>自身のインスタンス</returns>
    public CmdCx encoding(Encoding encoding)
    {
        this.ioEnc = encoding;
        return this;
    }

    /// <summary>呼び出しプロセスの入出力エンコーディングをUTF8に構成する</summary>
    /// <returns>自身のインスタンス</returns>
    public CmdCx encoding_utf8()
        => encoding(Encoding.UTF8);

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
        this.generalCancel = false;
        return this;
    }

    /// <summary>呼び出しプロセスの中止トークンと中止時のキャンセル例外変換を構成する</summary>
    /// <param name="token">中止トークン</param>
    /// <returns>自身のインスタンス</returns>
    public CmdCx cancelby(CancellationToken token)
    {
        this.cancelToken = token;
        this.generalCancel = true;
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
    private readonly string command;

    /// <summary>コマンド引数</summary>
    private readonly Action<ProcessStartInfo>? argumenter;

    /// <summary>ウィンドウを作らずに実行するか</summary>
    private bool noWindow;

    /// <summary>作業ディレクトリ</summary>
    private string? echoPrompt;

    /// <summary>作業ディレクトリ</summary>
    private string? workDir;

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

    /// <summary>中止例外を一般的なキャンセル例外に変換するか否か</summary>
    private bool generalCancel;
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
        var execTask = execAsync(
            stdOut: redirect.stdout,
            stdErr: redirect.stderr,
            outEncoding: this.ioEnc,
            stdIn: this.inReader,
            inEncoding: this.ioEnc
        );

        // キャンセル例外変換が構成されている場合、例外変換
        if (this.generalCancel)
        {
            execTask = converCancel(execTask);
        }

        // プロセス実行の終了を待機
        var exit = await execTask.ConfigureAwait(false);

        // リダイレクトされた出力を文字列化
        var outputText = sniffer.ToString();

        return new(exit.Code, outputText);
    }

    /// <summary>コマンドを実行して終了コードを取得する</summary>
    /// <param name="stdOut">標準出力のリダイレクト書き込み先ライター。nullの場合はリダイレクトなし。</param>
    /// <param name="stdErr">標準エラーのリダイレクト書き込み先ライター。nullの場合はリダイレクトなし。</param>
    /// <param name="stdIn">標準入力のリダイレクト読み込み元リーダー。nullの場合はリダイレクトなし。</param>
    /// <param name="outEncoding">プロセスの出力テキスト読み取り時のエンコーディング</param>
    /// <param name="inEncoding">プロセスの入力テキストを書き込む際のエンコーディング</param>
    /// <returns>呼び出しプロセスの終了コード</returns>
    private async Task<CmdExit> execAsync(TextWriter? stdOut, TextWriter? stdErr, Encoding? outEncoding, TextReader? stdIn, Encoding? inEncoding)
    {
        // 実行するコマンドの情報を設定
        var target = new ProcessStartInfo();
        target.UseShellExecute = false;
        target.FileName = this.command;
        this.argumenter?.Invoke(target);
        foreach (var env in this.envVars ?? [])
        {
            target.Environment[env.Key] = env.Value;
        }
        if (this.workDir != null)
        {
            target.WorkingDirectory = this.workDir;
        }
        if (this.noWindow)
        {
            target.CreateNoWindow = true;
        }
        if (stdOut != null)
        {
            target.RedirectStandardOutput = true;
            if (outEncoding != null) target.StandardOutputEncoding = outEncoding;
        }
        if (stdErr != null)
        {
            target.RedirectStandardError = true;
            if (outEncoding != null) target.StandardErrorEncoding = outEncoding;
        }
        if (stdIn != null)
        {
            target.RedirectStandardInput = true;
            if (inEncoding != null) target.StandardInputEncoding = inEncoding;
        }

        // エコーが有効に設定されていればコマンドラインを出力
        if (this.echoPrompt != null && stdOut != null)
        {
            echoCommandline(stdOut, target);
        }

        // コマンドを実行
        var proc = Process.Start(target) ?? throw new CmdProcException("Cannot execute command.");

        // 出力ストリームのリダイレクトタスクを生成するローカル関数
        static async Task redirectProcStream(TextReader reader, TextWriter writer, CancellationToken breaker, bool terminate = false)
        {
            await Task.Yield();
            try
            {
                var buffers = new[] { new char[1024], new char[1024], };
                var phase = 0;
                var redirectTask = Task.CompletedTask;
                while (true)
                {
                    // 利用するバッファを選択
                    var buff = buffers[phase];

                    // 次に利用するバッファを切替え
                    phase++;
                    phase %= buffers.Length;

                    // プロセスの出力を読み取り
                    var length = await reader.ReadAsync(buff.AsMemory(), breaker).ConfigureAwait(false);

                    // 前回のリダイレクト書き込みが完了している事を確認
                    await redirectTask.ConfigureAwait(false);

                    // 読み取りデータが無い場合はここで終える
                    if (length <= 0)
                    {
                        if (terminate) writer.Close();
                        break;
                    }

                    // 読み取ったデータをリダイレクト先に書き込み
                    redirectTask = writer.WriteAsync(buff.AsMemory(0, length), breaker);
                }
            }
            catch (OperationCanceledException) { }
        }

        // プロセスの終了時にリダイレクトを中止するための
        using var completeCanceller = new CancellationTokenSource();

        // 指定に応じたリダイレクトタスクを生成
        // 出力はすべて読み取れるようにキャンセルなし。入力はプロセス終了したらもう無意味なので中断させる。
        var stdoutRedirector = stdOut == null ? Task.CompletedTask : redirectProcStream(proc.StandardOutput, stdOut, CancellationToken.None);
        var stderrRedirector = stdErr == null ? Task.CompletedTask : redirectProcStream(proc.StandardError, stdErr, CancellationToken.None);
        var stdinRedirector = stdIn == null ? Task.CompletedTask : redirectProcStream(stdIn, proc.StandardInput, completeCanceller.Token, terminate: true);

        // コマンドの終了を待機
        var killed = false;
        try
        {
            await proc.WaitForExitAsync(this.cancelToken).ConfigureAwait(false);
        }
        catch (OperationCanceledException ex) when (ex.CancellationToken == this.cancelToken)
        {
            // キャンセルされたらプロセスをキル
            proc.Kill();
            killed = true;
        }
        finally
        {
            completeCanceller.Cancel();
        }

        // 出力読み取りタスクの完了を待機
        await Task.WhenAll(stdoutRedirector, stderrRedirector, stdinRedirector).ConfigureAwait(false);

        // 実行を中止した場合はそれを示す例外を送出
        if (killed)
        {
            throw new CmdProcCancelException(null, $"The process was killed by cancellation.");
        }

        return new(proc.ExitCode);
    }

    /// <summary>リダイレクト先ライターを生成する</summary>
    /// <param name="sniffer">リダイレクトを横取りするための文字列ライター</param>
    /// <returns>リダイレクト先ライター</returns>
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

    /// <summary>プロセス中止時キャンセル例外を変換するラッパー</summary>
    /// <param name="execTask">プロセス実行タスク</param>
    /// <returns>プロセス終了コード情報</returns>
    private async Task<CmdExit> converCancel(Task<CmdExit> execTask)
    {
        try
        {
            return await execTask.ConfigureAwait(false);
        }
        catch (CmdProcCancelException ex)
        {
            throw new OperationCanceledException(ex.Message, ex, this.cancelToken);
        }
    }

    /// <summary>実行対象のコマンドラインをエコー出力する</summary>
    /// <param name="stdOut">出力先ライター</param>
    /// <param name="target">実行対象コマンド情報</param>
    private void echoCommandline(TextWriter stdOut, ProcessStartInfo target)
    {
        var arguments = default(string);
        try
        {
            // 非公開のコマンドライン文字列構築メソッドへのアクセサ
            [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "BuildArguments")]
            static extern string BuildArguments(ProcessStartInfo target);

            // コマンドライン文字列構築
            arguments = BuildArguments(target);
        }
        catch
        {
            // 内部メソッドで構築できなかった場合は簡易的に構築しておく。クォートなどは省略。
            if (0 < target.ArgumentList?.Count)
            {
                arguments = target.ArgumentList.JoinString(" ");
            }
            else
            {
                arguments = target.Arguments;
            }
        }

        // コマンドラインエコーを出力
        stdOut.Write(this.echoPrompt);
        stdOut.Write(target.FileName);
        stdOut.Write(' ');
        stdOut.Write(arguments);
        stdOut.WriteLine();
    }
    #endregion
}