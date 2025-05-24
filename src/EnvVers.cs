using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Lestaly;

/// <summary>環境変数に関する補助メソッド</summary>
public static class EnvVers
{
    /// <summary>環境変数を文字列で取得する</summary>
    /// <param name="name">環境変数名</param>
    /// <param name="alt">代替文字列</param>
    /// <returns>環境変数の文字列または代替値</returns>
    [return: NotNullIfNotNull(nameof(alt))]
    public static string? String(string name, string? alt = default)
        => Environment.GetEnvironmentVariable(name).WhenEmpty(alt);

    /// <summary>環境変数を数値として取得する</summary>
    /// <typeparam name="TNumber">数値の型</typeparam>
    /// <param name="name">環境変数名</param>
    /// <param name="alt">代替値</param>
    /// <returns>環境変数の数値または代替値</returns>
    [return: NotNullIfNotNull(nameof(alt))]
    public static TNumber? Number<TNumber>(string name, TNumber? alt = default) where TNumber : struct, INumber<TNumber>
        => Environment.GetEnvironmentVariable(name)?.TryParseNumber<TNumber>() ?? alt;

    /// <summary>環境変数を現在ソースファイルからの相対パスとしてファイル情報を取得する</summary>
    /// <param name="name">環境変数名</param>
    /// <param name="alt">代替相対パス</param>
    /// <param name="path">この引数は省略する必要がある。</param>
    /// <returns>環境変数のファイル情報または代替値</returns>
    [return: NotNullIfNotNull(nameof(alt))]
    public static FileInfo? ThisSourceRelativeFile(string name, string? alt = default, [CallerFilePath] string path = "")
    {
        var variable = Environment.GetEnvironmentVariable(name);
        var directory = Path.GetDirectoryName(path)!;
        if (variable.IsNotWhite()) return new FileInfo(Path.Combine(directory, variable));
        if (alt.IsNotWhite()) return new FileInfo(Path.Combine(directory, alt));
        return default;
    }

    /// <summary>環境変数を現在ソースファイルからの相対パスとしてディレクトリ情報を取得する</summary>
    /// <param name="name">環境変数名</param>
    /// <param name="alt">代替相対パス</param>
    /// <param name="path">この引数は省略する必要がある。</param>
    /// <returns>環境変数のディレクトリ情報または代替値</returns>
    [return: NotNullIfNotNull(nameof(alt))]
    public static DirectoryInfo? ThisSourceRelativeDirectory(string name, string? alt = default, [CallerFilePath] string path = "")
    {
        var variable = Environment.GetEnvironmentVariable(name);
        var directory = Path.GetDirectoryName(path)!;
        if (variable.IsNotWhite()) return new DirectoryInfo(Path.Combine(directory, variable));
        if (alt.IsNotWhite()) return new DirectoryInfo(Path.Combine(directory, alt));
        return default;
    }

    /// <summary>環境変数をカレントディレクトリからの相対パスとしてファイル情報を取得する</summary>
    /// <param name="name">環境変数名</param>
    /// <param name="alt">代替相対パス</param>
    /// <returns>環境変数のファイル情報または代替値</returns>
    [return: NotNullIfNotNull(nameof(alt))]
    public static FileInfo? CurrentnRelativeFile(string name, string? alt = default)
        => CurrentDir.RelativeFileAt(Environment.GetEnvironmentVariable(name).WhenWhite(alt));

    /// <summary>環境変数をカレントディレクトリからの相対パスとしてディレクトリ情報を取得する</summary>
    /// <param name="name">環境変数名</param>
    /// <param name="alt">代替相対パス</param>
    /// <returns>環境変数のファイル情報または代替値</returns>
    [return: NotNullIfNotNull(nameof(alt))]
    public static DirectoryInfo? CurrentnRelativeDirectory(string name, string? alt = default)
        => CurrentDir.RelativeDirectoryAt(Environment.GetEnvironmentVariable(name).WhenWhite(alt));

}

