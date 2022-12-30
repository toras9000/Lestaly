using CommandLine;
using CommandLine.Text;

namespace Lestaly;

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
        return Parse<T>(args, settings =>
        {
            settings.IgnoreUnknownArguments = false;
            settings.AutoVersion = false;
        });
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

}
