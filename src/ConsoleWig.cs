using System.Text;

namespace Lestaly;

/// <summary>
/// コンソール関連のユーティリティ
/// </summary>
public static class ConsoleWig
{
    /// <summary>指定したカラーでテキストを出力する。</summary>
    /// <param name="color">色</param>
    /// <param name="text">テキスト</param>
    public static void WriteColord(ConsoleColor color, string text)
    {
        var original = Console.ForegroundColor;
        try
        {
            Console.ForegroundColor = color;
            Console.Write(text);
        }
        finally
        {
            Console.ForegroundColor = original;
        }
    }

    /// <summary>指定したカラーでテキスト行を出力する。</summary>
    /// <param name="color">色</param>
    /// <param name="text">テキスト</param>
    public static void WriteLineColord(ConsoleColor color, string text)
    {
        var original = Console.ForegroundColor;
        try
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
        }
        finally
        {
            Console.ForegroundColor = original;
        }
    }

    /// <summary>指定のキャプションを出力した後に行を読み取る</summary>
    /// <remarks>このメソッドは入力がリダイレクトされている場合には例外を発する。</remarks>
    /// <param name="caption">キャプション文字列</param>
    /// <returns>入力されたテキスト</returns>
    public static string ReadLine(string? caption = null)
    {
        if (caption.IsNotEmpty()) Console.Write(caption);
        return Console.ReadLine() ?? "";
    }

    /// <summary>入力エコー無しで1行分のキー入力を読み取る</summary>
    /// <param name="caption">キャプション文字列</param>
    /// <returns>読み取った行テキスト</returns>
    public static string ReadLineIntercepted(string? caption = null)
    {
        if (caption.IsNotEmpty()) Console.Write(caption);
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
    public static string ReadKeysLineReaction(string keyword, StringComparison comparison = StringComparison.Ordinal, bool breakOnReturn = true)
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

    /// <summary>入力を行末または一定時間アイドル指定のキーワードが入力されるまで読み取る。</summary>
    /// <param name="completer">入力アイドル時に完了判定する処理</param>
    /// <param name="timeout">入力アイドル時間 [ms]</param>
    /// <param name="breakOnReturn">入力終了後に改行を出力するか否か</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>入力された文字列を得るタスク</returns>
    public static async Task<string> ReadKeysLineIfAsync(Func<string, bool> completer, int timeout = 100, bool breakOnReturn = true, CancellationToken cancelToken = default)
    {
        if (completer == null) throw new ArgumentNullException(nameof(completer));
        if (timeout <= 0) throw new ArgumentOutOfRangeException(nameof(timeout));

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

    /// <summary>バッファ内のキー入力をスキップする。</summary>
    /// <param name="maxCount">最大スキップ数。継続的に入力される場合や</param>
    public static void SkipInputChars(int maxCount = int.MaxValue)
    {
        if (maxCount < 0) throw new ArgumentOutOfRangeException(nameof(maxCount));
        for (var i = 0; i < maxCount; i++)
        {
            if (!Console.KeyAvailable) break;
            Console.ReadKey(intercept: true);
        }
    }

    /// <summary>出力テキスト色を設定して区間を作成する</summary>
    /// <param name="color">色</param>
    /// <returns>設定区間。Disposeすると元の値を復元する。</returns>
    public static Period ForegroundColorPeriod(ConsoleColor color)
    {
        var original = Console.ForegroundColor;
        Console.ForegroundColor = color;
        return new Period(() => Console.ForegroundColor = original);
    }

    /// <summary>出力エンコーディングを設定して区間を作成する</summary>
    /// <param name="encoding">設定するエンコーディング</param>
    /// <returns>設定区間。Disposeすると元の値を復元する。</returns>
    public static Period OutputEncodingPeriod(Encoding encoding)
    {
        var original = Console.OutputEncoding;
        Console.OutputEncoding = encoding;
        return new Period(() => Console.OutputEncoding = original);
    }

    /// <summary>入力エンコーディングを設定して区間を作成する</summary>
    /// <param name="encoding">設定するエンコーディング</param>
    /// <returns>設定区間。Disposeすると元の値を復元する。</returns>
    public static Period InputEncodingPeriod(Encoding encoding)
    {
        var original = Console.InputEncoding;
        Console.InputEncoding = encoding;
        return new Period(() => Console.InputEncoding = original);
    }

    /// <summary>キャンセルキーイベントを指定のアクションでハンドルする区間を作成する</summary>
    /// <remarks>このメソッドではイベントハンドラで指定のアクションを呼び出すのみ。デフォルト動作(プロセス終了)の抑止などが必要であれば指定のキャンセル処理内で行う。</remarks>
    /// <param name="onCancel">キャンセル処理</param>
    /// <returns>設定区間。Disposeするとイベントハンドルを解除する。</returns>
    public static Period CancelKeyHandlePeriod(Action<ConsoleCancelEventArgs> onCancel)
    {
        var handler = new ConsoleCancelEventHandler((_, args) => { try { onCancel?.Invoke(args); } catch { } });
        Console.CancelKeyPress += handler;
        return new Period(() => Console.CancelKeyPress -= handler);
    }

    /// <summary>キャンセルキーイベントで指定のキャンセルソースをキャンセルする区間を作成する</summary>
    /// <remarks>このメソッドではイベントハンドラでデフォルト動作(プロセス終了)を抑止する。</remarks>
    /// <param name="cancelSource">キャンセルソース</param>
    /// <returns>設定区間。Disposeするとイベントハンドルを解除する。</returns>
    public static Period CancelKeyHandlePeriod(CancellationTokenSource cancelSource)
    {
        var handler = new ConsoleCancelEventHandler((_, args) => { try { cancelSource.Cancel(); args.Cancel = true; } catch { } });
        Console.CancelKeyPress += handler;
        return new Period(() => Console.CancelKeyPress -= handler);
    }
}
