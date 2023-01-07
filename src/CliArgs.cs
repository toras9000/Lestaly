using CommandLine;
using CommandLine.Text;

namespace Lestaly;

/// <summary>コマンドライン引数処理のオプション</summary>
/// <param name="CaseSensitive">大文字/小文字を区別するか否か</param>
/// <param name="AllowUnknown">不明な引数を許容するか否か</param>
/// <param name="AllowMultiple">複数回の同じオプションを許容するか否か</param>
/// <param name="MaxDispWidth">ヘルプ表示時の最大表示幅</param>
/// <param name="Title">ヘルプ表示時のタイトル</param>
public record CliArgsOptions(bool CaseSensitive = false, bool AllowUnknown = false, bool AllowMultiple = true, int? MaxDispWidth = null, string? Title = null);

/// <summary>
/// コマンドライン引数処理
/// </summary>
public static class CliArgs
{
    /// <summary>コマンドライン引数を CommandLineParser でオブジェクトにマッピングする。</summary>
    /// <typeparam name="T">マッピング対象型</typeparam>
    /// <param name="args">コマンドライン引数</param>
    /// <returns>マップされた型のインスタンス</returns>
    public static T Parse<T>(IEnumerable<string> args)
    {
        return Parse<T>(args, new CliArgsOptions());
    }

    /// <summary>コマンドライン引数をCommandLineParser でオブジェクトにマッピングする。</summary>
    /// <typeparam name="T">マッピング対象型</typeparam>
    /// <param name="args">コマンドライン引数</param>
    /// <param name="configuration">パーサオプション構成デリゲート</param>
    /// <returns>マップされた型のインスタンス</returns>
    public static T Parse<T>(IEnumerable<string> args, Action<ParserSettings> configuration)
    {
        var parser = new Parser(configuration);
        var result = parser.ParseArguments<T>(args);
        if (result == null) throw new PavedMessageException("Argument error", fatal: false);

        var firstError = result.Errors.FirstOrDefault();
        if (firstError != null)
        {
            var builder = SentenceBuilder.Create();
            var sentence = builder.FormatError(firstError);
            throw new PavedMessageException("Argument error: " + sentence, fatal: false);
        }

        return result.Value ?? throw new PavedMessageException("Argument error", fatal: false);
    }

    /// <summary>コマンドライン引数をCommandLineParser でオブジェクトにマッピングする。</summary>
    /// <typeparam name="T">マッピング対象型</typeparam>
    /// <param name="args">コマンドライン引数</param>
    /// <param name="options">パーサオプション</param>
    /// <returns>マップされた型のインスタンス</returns>
    public static T Parse<T>(IEnumerable<string> args, CliArgsOptions options)
    {
        var parser = new Parser(settings =>
        {
            settings.AutoHelp = true;
            settings.AutoVersion = false;
            settings.HelpWriter = null;
            settings.IgnoreUnknownArguments = options.AllowUnknown;
            settings.CaseSensitive = options.CaseSensitive;
            settings.CaseInsensitiveEnumValues = !options.CaseSensitive;
            if (options.MaxDispWidth.HasValue) settings.MaximumDisplayWidth = options.MaxDispWidth.Value;
        });
        var result = parser.ParseArguments<T>(args);
        if (result == null) throw new PavedMessageException("Argument error", fatal: false);

        if (result.Errors.IsHelp())
        {
            var title = options.Title ?? "Parameters:";
            var help = new HelpText(title);
            help.AddDashesToOption = true;
            help.AutoVersion = false;
            help.AddOptions(result);
            var text = help.ToString();
            throw new PavedMessageException(text, PavedMessageKind.Information);
        }

        var firstError = result.Errors.FirstOrDefault();
        if (firstError != null)
        {
            var builder = SentenceBuilder.Create();
            var sentence = builder.FormatError(firstError);
            throw new PavedMessageException("Argument error: " + sentence, fatal: false);
        }

        return result.Value ?? throw new PavedMessageException("Argument error", fatal: false);
    }
}
