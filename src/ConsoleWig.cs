using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace Lestaly;

/// <summary>
/// コンソール関連のユーティリティ
/// </summary>
public sealed class ConsoleWig : IConsoleWig
{
    /// <summary>コンソール関連のユーティリティ I/F</summary>
    public static IConsoleWig Facade { get; }

    /// <summary>Console.In の 代替リーダー</summary>
    /// <remarks>
    /// このプロパティは主に Async 操作を利用する目的の、標準 Console.In を代替するリーダインスタンスを返す。
    /// </remarks>
    public static ConsoleInReader InReader => inReader.Value;

    /// <summary>指定したテキストを出力する。</summary>
    /// <param name="text">テキスト</param>
    /// <returns>呼び出し元インスタンス自身</returns>
    public static IConsoleWig Write(string text)
        => Facade.Write(text);

    /// <summary>指定したテキスト行を出力する。</summary>
    /// <param name="text">テキスト</param>
    /// <returns>呼び出し元インスタンス自身</returns>
    public static IConsoleWig WriteLine(string text = "")
        => Facade.WriteLine(text);

    /// <summary>改行を出力する。</summary>
    /// <returns>呼び出し元インスタンス自身</returns>
    public static IConsoleWig NewLine()
        => Facade.NewLine();

    /// <summary>指定したハイパーリンクを出力する</summary>
    /// <param name="uri">リンク先URI</param>
    /// <param name="text">リンクテキスト</param>
    /// <returns>呼び出し元インスタンス自身</returns>
    public static IConsoleWig WriteLink(string uri, string? text = null)
        => Facade.WriteLink(uri, text);

    /// <summary>行を読み取る</summary>
    /// <remarks>このメソッドは入力がリダイレクトされている場合には例外を発する。</remarks>
    /// <returns>入力されたテキスト</returns>
    public static string ReadLine()
        => Facade.ReadLine();

    /// <summary>行を読み取る</summary>
    /// <remarks>このメソッドは入力がリダイレクトされている場合には例外を発する。</remarks>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>入力されたテキストを得るタスク</returns>
    public static ValueTask<string?> ReadLineAsync(CancellationToken cancelToken)
        => Facade.ReadLineAsync(cancelToken);

    /// <summary>入力エコー無しで1行分のキー入力を読み取る</summary>
    /// <returns>読み取った行テキスト</returns>
    public static string ReadLineIntercepted()
        => Facade.ReadLineIntercepted();

    /// <summary>入力を行末または指定のキーワードが入力されるまで読み取る。</summary>
    /// <param name="keyword">入力を終了するキーワード</param>
    /// <param name="comparison">文字列比較方法</param>
    /// <param name="breakOnReturn">入力終了後に改行を出力するか否か</param>
    /// <returns>入力された文字列</returns>
    public static string ReadKeysLineReaction(string keyword, StringComparison comparison = StringComparison.Ordinal, bool breakOnReturn = true)
        => Facade.ReadKeysLineReaction(keyword, comparison, breakOnReturn);

    /// <summary>入力を行末または指定のキーワードが入力されるまで読み取る。(OrdinalIgnoreCase)</summary>
    /// <param name="keyword">入力を終了するキーワード</param>
    /// <param name="breakOnReturn">入力終了後に改行を出力するか否か</param>
    /// <returns>入力された文字列</returns>
    public static string ReadKeysLineReactionIgnoreCase(string keyword, bool breakOnReturn = true)
        => Facade.ReadKeysLineReactionIgnoreCase(keyword, breakOnReturn);

    /// <summary>入力を行末または指定の条件に適合した入力が行われるまで読み取る。</summary>
    /// <param name="completer">入力アイドル時に完了判定する処理</param>
    /// <param name="timeout">入力アイドル時間 [ms]</param>
    /// <param name="breakOnReturn">入力終了後に改行を出力するか否か</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>入力された文字列を得るタスク</returns>
    public static Task<string> ReadKeysLineIfAsync(Func<string, bool> completer, int timeout = 100, bool breakOnReturn = true, CancellationToken cancelToken = default)
        => Facade.ReadKeysLineIfAsync(completer, timeout, breakOnReturn, cancelToken);

    /// <summary>入力を行末または指定のパターンにマッチした入力が行われるまで読み取る。</summary>
    /// <param name="pattern">入力アイドル時に完了判定するパターン。マッチすれば</param>
    /// <param name="timeout">入力アイドル時間 [ms]</param>
    /// <param name="breakOnReturn">入力終了後に改行を出力するか否か</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>入力された文字列を得るタスク</returns>
    public static Task<string> ReadKeysLineMatchAsync(Regex pattern, int timeout = 100, bool breakOnReturn = true, CancellationToken cancelToken = default)
        => Facade.ReadKeysLineMatchAsync(pattern, timeout, breakOnReturn, cancelToken);

    /// <summary>キー入力を読み取る</summary>
    /// <param name="intercept">キー入力をインターセプトする(出力に出さない)かどうか。</param>
    /// <returns>押下されたキー情報</returns>
    public static ConsoleKeyInfo ReadKey(bool intercept)
        => Facade.ReadKey(intercept);

    /// <summary>キー入力を読み取る</summary>
    /// <param name="intercept">キー入力をインターセプトする(出力に出さない)かどうか。</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>押下されたキー情報</returns>
    public static Task<ConsoleKeyInfo> ReadKeyAsync(bool intercept, CancellationToken cancelToken = default)
        => Facade.ReadKeyAsync(intercept, cancelToken);

    /// <summary>キー入力を読み取る</summary>
    /// <param name="intercept">キー入力をインターセプトする(出力に出さない)かどうか。</param>
    /// <param name="timeout">タイムアウト時間[ms]</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>押下されたキー情報</returns>
    public static Task<ConsoleKeyInfo?> WaitKeyAsync(bool intercept, int timeout, CancellationToken cancelToken = default)
        => Facade.WaitKeyAsync(intercept, timeout, cancelToken);

    /// <summary>バッファ内のキー入力をスキップする。</summary>
    /// <param name="maxCount">最大スキップ数。継続的に入力される場合や</param>
    /// <returns>呼び出し元インスタンス自身</returns>
    public static IConsoleWig SkipInputChars(int maxCount = int.MaxValue)
        => Facade.SkipInputChars(maxCount);

    /// <summary>出力テキスト色を設定して区間を作成する</summary>
    /// <param name="color">色</param>
    /// <returns>設定区間。Disposeすると元の値を復元する。</returns>
    public static Period ForegroundColorPeriod(ConsoleColor color)
        => Facade.ForegroundColorPeriod(color);

    /// <summary>出力エンコーディングを設定して区間を作成する</summary>
    /// <param name="encoding">設定するエンコーディング</param>
    /// <returns>設定区間。Disposeすると元の値を復元する。</returns>
    public static Period OutputEncodingPeriod(Encoding encoding)
        => Facade.OutputEncodingPeriod(encoding);

    /// <summary>入力エンコーディングを設定して区間を作成する</summary>
    /// <param name="encoding">設定するエンコーディング</param>
    /// <returns>設定区間。Disposeすると元の値を復元する。</returns>
    public static Period InputEncodingPeriod(Encoding encoding)
        => Facade.InputEncodingPeriod(encoding);

    /// <summary>キャンセルキーイベントを指定のアクションでハンドルする区間を作成する</summary>
    /// <remarks>このメソッドではイベントハンドラで指定のアクションを呼び出すのみ。デフォルト動作(プロセス終了)の抑止などが必要であれば指定のキャンセル処理内で行う。</remarks>
    /// <param name="onCancel">キャンセル処理</param>
    /// <returns>設定区間。Disposeするとイベントハンドルを解除する。</returns>
    public static Period CancelKeyHandlePeriod(Action<ConsoleCancelEventArgs> onCancel)
        => Facade.CancelKeyHandlePeriod(onCancel);

    /// <summary>キャンセルキーイベントで指定のキャンセルソースをキャンセルする区間を作成する</summary>
    /// <remarks>このメソッドではイベントハンドラでデフォルト動作(プロセス終了)を抑止する。</remarks>
    /// <param name="cancelSource">キャンセルソース</param>
    /// <returns>設定区間。Disposeするとイベントハンドルを解除する。</returns>
    public static Period CancelKeyHandlePeriod(CancellationTokenSource cancelSource)
        => Facade.CancelKeyHandlePeriod(cancelSource);

    /// <summary>キャンセルキーイベントでキャンセルされるトークンを持つ区間を作成する</summary>
    /// <returns>キャンセルトークン保持区間。Disposeするとイベントハンドルを解除する。</returns>
    public static ICancelTokenPeriod CreateCancelKeyHandlePeriod()
        => Facade.CreateCancelKeyHandlePeriod();

    /// <summary>静的コンストラクタ</summary>
    static ConsoleWig()
    {
        Facade = new ConsoleWig();
    }

    /// <summary>ConsoleInWrapper の遅延生成</summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:命名スタイル")]
    private static readonly Lazy<ConsoleInReader> inReader = new(() => new ConsoleInReader());
}

/// <summary>キャンセルトークンを保持する区間</summary>
public interface ICancelTokenPeriod : IDisposable
{
    /// <summary>キャンセルトークン</summary>
    CancellationToken Token { get; }
}

/// <summary>
/// コンソール関連のユーティリティ
/// </summary>
public interface IConsoleWig
{
    /// <summary>指定したテキストを出力する。</summary>
    /// <param name="text">テキスト</param>
    /// <returns>呼び出し元インスタンス自身</returns>
    public IConsoleWig Write(string text)
    {
        Console.Write(text);
        return this;
    }

    /// <summary>指定したテキスト行を出力する。</summary>
    /// <param name="text">テキスト</param>
    /// <returns>呼び出し元インスタンス自身</returns>
    public IConsoleWig WriteLine(string text = "")
    {
        Console.WriteLine(text);
        return this;
    }

    /// <summary>改行を出力する。</summary>
    /// <returns>呼び出し元インスタンス自身</returns>
    public IConsoleWig NewLine()
    {
        Console.WriteLine();
        return this;
    }

    /// <summary>指定したハイパーリンクを出力する</summary>
    /// <param name="uri">リンク先URI</param>
    /// <param name="text">リンクテキスト</param>
    /// <returns>呼び出し元インスタンス自身</returns>
    public IConsoleWig WriteLink(string uri, string? text = null)
    {
        Write(Poster.Link[uri, text ?? uri]);
        return this;
    }

    /// <summary>行を読み取る</summary>
    /// <remarks>このメソッドは入力がリダイレクトされている場合には例外を発する。</remarks>
    /// <returns>入力されたテキスト</returns>
    public string ReadLine()
    {
        return Console.ReadLine() ?? "";
    }

    /// <summary>行を読み取る</summary>
    /// <remarks>このメソッドは入力がリダイレクトされている場合には例外を発する。</remarks>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>入力されたテキストを得るタスク</returns>
    public ValueTask<string?> ReadLineAsync(CancellationToken cancelToken)
    {
        return ConsoleWig.InReader.ReadLineAsync(cancelToken);
    }

    /// <summary>入力エコー無しで1行分のキー入力を読み取る</summary>
    /// <returns>読み取った行テキスト</returns>
    public string ReadLineIntercepted()
    {
        var buff = new StringBuilder();
        while (true)
        {
            // キー入力読み取り
            var input = Console.ReadKey(intercept: true);

            // 特殊キーの処理
            if (input.Key == ConsoleKey.Enter) break;
            if (input.Key == ConsoleKey.Backspace)
            {
                if (0 < buff.Length) buff.Length--;
                continue;
            }

            // 無効なキャラクタならばスキップ
            if (input.KeyChar == 0) continue;

            // 有効キャラクタを蓄積
            buff.Append(input.KeyChar);
        }
        return buff.ToString();
    }

    /// <summary>入力を行末または指定のキーワードが入力されるまで読み取る。</summary>
    /// <param name="keyword">入力を終了するキーワード</param>
    /// <param name="comparison">文字列比較方法</param>
    /// <param name="breakOnReturn">入力終了後に改行を出力するか否か</param>
    /// <returns>入力された文字列</returns>
    public string ReadKeysLineReaction(string keyword, StringComparison comparison = StringComparison.Ordinal, bool breakOnReturn = true)
    {
        if (string.IsNullOrEmpty(keyword)) throw new ArgumentException($"Invalid {nameof(keyword)}");
        var buff = new StringBuilder();
        while (true)
        {
            // キー入力読み取り
            var input = Console.ReadKey(intercept: true);

            // 特殊キーの処理
            if (input.Key == ConsoleKey.Enter) break;
            if (input.Key == ConsoleKey.Backspace)
            {
                if (0 < buff.Length) buff.Length--;
                Console.Write(input.KeyChar);
                continue;
            }

            // 無効なキャラクタならばスキップ
            if (input.KeyChar == 0) continue;

            // 有効キャラクタを蓄積・出力
            buff.Append(input.KeyChar);
            Console.Write(input.KeyChar);

            // 蓄積文字列の末尾が指定のキーワードになったら完了とする
            if (buff.EndsWith(keyword, comparison)) break;
        }

        // 指定により改行出力してから終了
        if (breakOnReturn) Console.WriteLine();
        return buff.ToString();
    }

    /// <summary>入力を行末または指定のキーワードが入力されるまで読み取る。(OrdinalIgnoreCase)</summary>
    /// <param name="keyword">入力を終了するキーワード</param>
    /// <param name="breakOnReturn">入力終了後に改行を出力するか否か</param>
    /// <returns>入力された文字列</returns>
    public string ReadKeysLineReactionIgnoreCase(string keyword, bool breakOnReturn = true)
        => ConsoleWig.ReadKeysLineReaction(keyword, StringComparison.OrdinalIgnoreCase, breakOnReturn);

    /// <summary>入力を行末または指定の条件に適合した入力が行われるまで読み取る。</summary>
    /// <param name="completer">入力アイドル時に完了判定する処理</param>
    /// <param name="timeout">入力アイドル時間 [ms]</param>
    /// <param name="breakOnReturn">入力終了後に改行を出力するか否か</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>入力された文字列を得るタスク</returns>
    public async Task<string> ReadKeysLineIfAsync(Func<string, bool> completer, int timeout = 100, bool breakOnReturn = true, CancellationToken cancelToken = default)
    {
        ArgumentNullException.ThrowIfNull(completer);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(timeout);

        var buff = new StringBuilder();
        while (true)
        {
            // ずっと入力がAvailableな場合への対処として、キャンセル状態をチェック
            cancelToken.ThrowIfCancellationRequested();

            // キー入力読み取り
            var input = Console.ReadKey(intercept: true);

            // 特殊キーの処理
            if (input.Key == ConsoleKey.Enter) break;
            if (input.Key == ConsoleKey.Backspace)
            {
                if (0 < buff.Length) buff.Length--;
                Console.Write(input.KeyChar);
            }
            else if (input.KeyChar != 0)
            {
                // 有効キャラクタを蓄積・出力
                buff.Append(input.KeyChar);
                Console.Write(input.KeyChar);
            }

            // 次のキー入力が有効であれば続けて読み取り
            if (Console.KeyAvailable) { continue; }

            // 入力が無い場合は一定時間待機
            await Task.Delay(timeout, cancelToken).ConfigureAwait(false);

            // コールバックで完了判定
            var complete = completer(buff.ToString());
            if (complete) break;
        }

        // 指定により改行出力してから終了
        if (breakOnReturn) Console.WriteLine();
        return string.Concat(buff);
    }

    /// <summary>入力を行末または指定のパターンにマッチした入力が行われるまで読み取る。</summary>
    /// <param name="pattern">入力アイドル時に完了判定するパターン。マッチすれば</param>
    /// <param name="timeout">入力アイドル時間 [ms]</param>
    /// <param name="breakOnReturn">入力終了後に改行を出力するか否か</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>入力された文字列を得るタスク</returns>
    public Task<string> ReadKeysLineMatchAsync(Regex pattern, int timeout = 100, bool breakOnReturn = true, CancellationToken cancelToken = default)
        => ReadKeysLineIfAsync(pattern.IsMatch, timeout, breakOnReturn, cancelToken);

    /// <summary>キー入力を読み取る</summary>
    /// <param name="intercept">キー入力をインターセプトする(出力に出さない)かどうか。</param>
    /// <returns>押下されたキー情報</returns>
    public ConsoleKeyInfo ReadKey(bool intercept)
        => Console.ReadKey(intercept);

    /// <summary>キー入力を読み取る</summary>
    /// <param name="intercept">キー入力をインターセプトする(出力に出さない)かどうか。</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>押下されたキー情報</returns>
    public async Task<ConsoleKeyInfo> ReadKeyAsync(bool intercept, CancellationToken cancelToken = default)
    {
        while (true)
        {
            if (Console.KeyAvailable)
            {
                return Console.ReadKey(intercept);
            }
            await Task.Delay(30, cancelToken).ConfigureAwait(false);
        }
    }

    /// <summary>キー入力を読み取る</summary>
    /// <param name="intercept">キー入力をインターセプトする(出力に出さない)かどうか。</param>
    /// <param name="timeout">タイムアウト時間[ms]</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>押下されたキー情報</returns>
    public async Task<ConsoleKeyInfo?> WaitKeyAsync(bool intercept, int timeout, CancellationToken cancelToken = default)
    {
        var watch = Stopwatch.StartNew();
        while (true)
        {
            if (Console.KeyAvailable)
            {
                return Console.ReadKey(intercept);
            }
            if (timeout <= watch.ElapsedMilliseconds)
            {
                break;
            }
            await Task.Delay(30, cancelToken).ConfigureAwait(false);
        }
        return null;
    }

    /// <summary>バッファ内のキー入力をスキップする。</summary>
    /// <param name="maxCount">最大スキップ数。継続的に入力される場合や</param>
    /// <returns>呼び出し元インスタンス自身</returns>
    public IConsoleWig SkipInputChars(int maxCount = int.MaxValue)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(maxCount);
        for (var i = 0; i < maxCount; i++)
        {
            if (!Console.KeyAvailable) break;
            Console.ReadKey(intercept: true);
        }
        return this;
    }

    /// <summary>出力テキスト色を設定して区間を作成する</summary>
    /// <param name="color">色</param>
    /// <returns>設定区間。Disposeすると元の値を復元する。</returns>
    public Period ForegroundColorPeriod(ConsoleColor color)
    {
        var original = Console.ForegroundColor;
        Console.ForegroundColor = color;
        return new Period(() => Console.ForegroundColor = original);
    }

    /// <summary>出力エンコーディングを設定して区間を作成する</summary>
    /// <param name="encoding">設定するエンコーディング</param>
    /// <returns>設定区間。Disposeすると元の値を復元する。</returns>
    public Period OutputEncodingPeriod(Encoding encoding)
    {
        var original = Console.OutputEncoding;
        Console.OutputEncoding = encoding;
        return new Period(() => Console.OutputEncoding = original);
    }

    /// <summary>入力エンコーディングを設定して区間を作成する</summary>
    /// <param name="encoding">設定するエンコーディング</param>
    /// <returns>設定区間。Disposeすると元の値を復元する。</returns>
    public Period InputEncodingPeriod(Encoding encoding)
    {
        var original = Console.InputEncoding;
        Console.InputEncoding = encoding;
        return new Period(() => Console.InputEncoding = original);
    }

    /// <summary>キャンセルキーイベントを指定のアクションでハンドルする区間を作成する</summary>
    /// <remarks>このメソッドではイベントハンドラで指定のアクションを呼び出すのみ。デフォルト動作(プロセス終了)の抑止などが必要であれば指定のキャンセル処理内で行う。</remarks>
    /// <param name="onCancel">キャンセル処理</param>
    /// <returns>設定区間。Disposeするとイベントハンドルを解除する。</returns>
    public Period CancelKeyHandlePeriod(Action<ConsoleCancelEventArgs> onCancel)
    {
        var handler = new ConsoleCancelEventHandler((_, args) => { try { onCancel?.Invoke(args); } catch { } });
        Console.CancelKeyPress += handler;
        return new Period(() => Console.CancelKeyPress -= handler);
    }

    /// <summary>キャンセルキーイベントで指定のキャンセルソースをキャンセルする区間を作成する</summary>
    /// <remarks>このメソッドではイベントハンドラでデフォルト動作(プロセス終了)を抑止する。</remarks>
    /// <param name="cancelSource">キャンセルソース</param>
    /// <returns>設定区間。Disposeするとイベントハンドルを解除する。</returns>
    public Period CancelKeyHandlePeriod(CancellationTokenSource cancelSource)
    {
        var handler = new ConsoleCancelEventHandler((_, args) => { try { cancelSource.Cancel(); args.Cancel = true; } catch { } });
        Console.CancelKeyPress += handler;
        return new Period(() => Console.CancelKeyPress -= handler);
    }

    /// <summary>キャンセルキーイベントでキャンセルされるトークンを持つ区間を作成する</summary>
    /// <returns>キャンセルトークン保持区間。Disposeするとイベントハンドルを解除する。</returns>
    public ICancelTokenPeriod CreateCancelKeyHandlePeriod()
    {
        return new CancelKeyTokenPeriod();
    }

    /// <summary>
    /// キャンセルキーイベントによってキャンセルされるキャンセルトークンを保持する区間
    /// </summary>
    private sealed class CancelKeyTokenPeriod : ICancelTokenPeriod
    {
        /// <summary>デフォルトコンストラクタ</summary>
        public CancelKeyTokenPeriod()
        {
            this.canceller = new CancellationTokenSource();
            Console.CancelKeyPress += cancelKeyHandler;
        }

        /// <summary>キャンセルトークン</summary>
        public CancellationToken Token => this.canceller.Token;

        /// <summary>インスタンスを破棄する</summary>
        public void Dispose()
        {
            Console.CancelKeyPress -= cancelKeyHandler;
            this.canceller.Dispose();
        }

        /// <summary>キャンセルトークンソース</summary>
        private readonly CancellationTokenSource canceller;

        /// <summary>キャンセルキーイベントハンドラ</summary>
        private void cancelKeyHandler(object? sender, ConsoleCancelEventArgs e)
        {
            this.canceller.Cancel();
            e.Cancel = true;
        }
    }
}
