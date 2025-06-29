namespace Lestaly;

/// <summary>
/// ANSIエスケープシーケンスを利用した文字列構築用のクラス
/// </summary>
public static class Poster
{
    /// <summary>ハイパーリンク エスケープシーケンステキスト構築処理</summary>
    public static EscLinkBuilder Link { get; } = new EscLinkBuilder();
}

/// <summary>
/// ハイパーリンク エスケープシーケンステキスト構築処理
/// </summary>
public class EscLinkBuilder
{
    /// <summary>ハイパーリンク エスケープシーケンステキストを作成する</summary>
    /// <param name="uri">リンク先URI</param>
    /// <returns>ハイパーリンク エスケープシーケンステキスト</returns>
    public string this[string uri]
    {
        get => this[uri, uri];
    }

    /// <summary>ハイパーリンク エスケープシーケンステキストを作成する</summary>
    /// <param name="uri">リンク先URI</param>
    /// <param name="text">リンクテキスト</param>
    /// <returns>ハイパーリンク エスケープシーケンステキスト</returns>
    public string this[string uri, string text]
    {
        get
        {
            // 以下ページで紹介されているエスケープシーケンスを出力する。
            // ただしハイパーリンクとして機能するかどうかはターミナルソフトでの対応次第となる。
            // https://gist.github.com/egmontkob/eb114294efbcd5adb1944c9f3cb5feda

            const string ESC = "\x1b";
            const string OSC = $"{ESC}]";
            const string ST = $@"{ESC}\";
            if (string.IsNullOrEmpty(text)) text = uri;
            return $@"{OSC}8;;{uri}{ST}{text}{OSC}8;;{ST}";
        }
    }

}
