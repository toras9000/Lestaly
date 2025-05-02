using System.Numerics;

namespace Lestaly;

/// <summary>環境変数に関する補助メソッド</summary>
public static class EnvVers
{
    /// <summary>環境変数を文字列で取得する</summary>
    /// <param name="name">環境変数名</param>
    /// <returns>環境変数の文字列</returns>
    public static string? String(string name)
        => Environment.GetEnvironmentVariable(name);

    /// <summary>環境変数を文字列で取得する</summary>
    /// <param name="name">環境変数名</param>
    /// <param name="alt">代替文字列</param>
    /// <returns>環境変数の文字列</returns>
    public static string String(string name, string alt)
        => Environment.GetEnvironmentVariable(name).WhenEmpty(alt);

    /// <summary>環境変数を数値として取得する</summary>
    /// <typeparam name="TNumber">数値の型</typeparam>
    /// <param name="name">環境変数名</param>
    /// <returns>環境変数の数値</returns>
    public static TNumber? Number<TNumber>(string name) where TNumber : struct, INumber<TNumber>
        => Environment.GetEnvironmentVariable(name)?.TryParseNumber<TNumber>();

    /// <summary>環境変数を数値として取得する</summary>
    /// <typeparam name="TNumber">数値の型</typeparam>
    /// <param name="name">環境変数名</param>
    /// <param name="alt">代替値</param>
    /// <returns>環境変数の数値</returns>
    public static TNumber Number<TNumber>(string name, TNumber alt) where TNumber : struct, INumber<TNumber>
        => Environment.GetEnvironmentVariable(name)?.TryParseNumber<TNumber>() ?? alt;
}

