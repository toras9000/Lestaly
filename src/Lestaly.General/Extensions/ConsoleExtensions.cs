using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace Lestaly;

/// <summary>
/// コンソール に関する拡張メソッド
/// </summary>
public static class ConsoleExtensions
{
    extension(Console)
    {
        /// <summary>Console.In の 代替リーダー</summary>
        /// <remarks>
        /// このプロパティは主に Async 操作を利用する目的の、標準 Console.In を代替するリーダインスタンスを返す。
        /// </remarks>
        public static ConsoleInReader InReader => inReader.Value;

        /// <summary>指定したハイパーリンクを出力する</summary>
        /// <param name="uri">リンク先URI</param>
        /// <param name="text">リンクテキスト</param>
        public static void WriteLink(string uri, string? text = null)
            => Console.Write(Poster.Link[uri, text ?? uri]);

        /// <summary>指定したハイパーリンクを出力する</summary>
        /// <param name="uri">リンク先URI</param>
        /// <param name="text">リンクテキスト</param>
        public static void WriteLink(Uri uri, string? text = null)
            => Console.Write(Poster.Link[uri.AbsolutePath, text ?? uri.AbsoluteUri]);

        /// <summary>行を読み取る</summary>
        /// <remarks>このメソッドは入力がリダイレクトされている場合には例外を発する。</remarks>
        /// <param name="cancelToken">キャンセルトークン</param>
        /// <returns>入力されたテキストを得るタスク</returns>
        public static ValueTask<string?> ReadLineAsync(CancellationToken cancelToken)
        {
            return Console.InReader.ReadLineAsync(cancelToken);
        }

        /// <summary>入力エコー無しで1行分のキー入力を読み取る</summary>
        /// <returns>読み取った行テキスト</returns>
        public static string ReadLineIntercepted()
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

        /// <summary>入力を行末または指定の条件に適合した入力が行われるまで読み取る。</summary>
        /// <param name="completer">入力アイドル時に完了判定する処理</param>
        /// <param name="timeout">入力アイドル時間 [ms]</param>
        /// <param name="breakOnReturn">入力終了後に改行を出力するか否か</param>
        /// <param name="cancelToken">キャンセルトークン</param>
        /// <returns>入力された文字列を得るタスク</returns>
        public static async Task<string> ReadKeysLineIfAsync(Func<string, bool> completer, int timeout = 100, bool breakOnReturn = true, CancellationToken cancelToken = default)
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
        public static Task<string> ReadKeysLineMatchAsync(Regex pattern, int timeout = 100, bool breakOnReturn = true, CancellationToken cancelToken = default)
            => ReadKeysLineIfAsync(pattern.IsMatch, timeout, breakOnReturn, cancelToken);

        /// <summary>キー入力を読み取る</summary>
        /// <param name="intercept">キー入力をインターセプトする(出力に出さない)かどうか。</param>
        /// <param name="cancelToken">キャンセルトークン</param>
        /// <returns>押下されたキー情報</returns>
        public static async Task<ConsoleKeyInfo> ReadKeyAsync(bool intercept, CancellationToken cancelToken = default)
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
        /// <param name="timeout">タイムアウト時間</param>
        /// <param name="cancelToken">キャンセルトークン</param>
        /// <returns>押下されたキー情報</returns>
        public static async Task<ConsoleKeyInfo?> WaitKeyAsync(bool intercept, TimeSpan timeout, CancellationToken cancelToken = default)
        {
            var watch = Stopwatch.StartNew();
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    return Console.ReadKey(intercept);
                }
                if (timeout.Ticks <= watch.ElapsedTicks)
                {
                    break;
                }
                await Task.Delay(30, cancelToken).ConfigureAwait(false);
            }
            return null;
        }

        /// <summary>キー入力を読み取る</summary>
        /// <param name="intercept">キー入力をインターセプトする(出力に出さない)かどうか。</param>
        /// <param name="timeout">タイムアウト時間[ms]</param>
        /// <param name="cancelToken">キャンセルトークン</param>
        /// <returns>押下されたキー情報</returns>
        public static Task<ConsoleKeyInfo?> WaitKeyAsync(bool intercept, int timeout, CancellationToken cancelToken = default)
            => WaitKeyAsync(intercept, TimeSpan.FromMicroseconds(timeout), cancelToken);

        /// <summary>バッファ内のキー入力をスキップする。</summary>
        /// <param name="maxCount">最大スキップ数。継続的に入力される場合や</param>
        /// <returns>呼び出し元インスタンス自身</returns>
        public static void SkipInputChars(int maxCount = int.MaxValue)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(maxCount);
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

        /// <summary>出力エンコーディングをUTF8に設定して区間を作成する</summary>
        /// <returns>設定区間。Disposeすると元の値を復元する。</returns>
        public static Period OutputUtf8EncodingPeriod()
            => OutputEncodingPeriod(Encoding.UTF8);

        /// <summary>入力エンコーディングをUTF8に設定して区間を作成する</summary>
        /// <returns>設定区間。Disposeすると元の値を復元する。</returns>
        public static Period InputUtf8EncodingPeriod()
            => InputEncodingPeriod(Encoding.UTF8);

        /// <summary>キャンセルキーイベントでキャンセルされるトークンを持つ区間を作成する</summary>
        /// <returns>キャンセルトークン保持区間。Disposeするとイベントハンドルを解除する。</returns>
        public static ICancelTokenPeriod CreateCancelKeyHandlePeriod()
            => new CancelKeyTokenPeriod();
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

    /// <summary>ConsoleInWrapper の遅延生成</summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:命名スタイル")]
    private static readonly Lazy<ConsoleInReader> inReader = new(() => new ConsoleInReader());

}
