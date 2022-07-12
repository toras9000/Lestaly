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
    public static string? ReadLineDirect(string caption)
    {
        if (Console.IsInputRedirected) throw new InvalidOperationException();
        Console.Write(caption);
        return Console.ReadLine();
    }

}
