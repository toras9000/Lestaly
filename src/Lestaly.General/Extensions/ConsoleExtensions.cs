using System.Text;

namespace Lestaly;

/// <summary>
/// コンソール に関する拡張メソッド
/// </summary>
public static class ConsoleExtensions
{
    extension(Console)
    {
        /// <summary>指定したハイパーリンクを出力する</summary>
        /// <param name="uri">リンク先URI</param>
        /// <param name="text">リンクテキスト</param>
        public static void WriteLink(string uri, string? text = null)
            => Console.Write(Poster.Link[uri, text ?? uri]);

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

}
