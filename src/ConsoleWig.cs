﻿using System.Text;

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

    /// <summary>入力エコー無しで1行分のキー入力を読み取る</summary>
    /// <returns>読み取った行テキスト</returns>
    public static string ReadLineIntercepted()
    {
        var buff = new StringBuilder();
        while (true)
        {
            var info = Console.ReadKey(intercept: true);
            if (info.Key == ConsoleKey.Enter) break;
            buff.Append(info.KeyChar);
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
