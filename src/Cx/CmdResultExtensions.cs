namespace Lestaly.Cx;

/// <summary>
/// コマンド実行タスクに対する拡張メソッド
/// </summary>
[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:命名スタイル", Justification = "このクラスでは意図的に小文字メソッドを使っている")]
public static class CmdResultExtensions
{
    /// <summary>コマンド実行結果に対し、指定の終了コードを正常とする検証を行う。</summary>
    /// <param name="self">コマンド実行結果を得るタスク</param>
    /// <param name="codes">正常とみなす終了コード。指定しない場合はゼロのみを正常とみなす。正常以外の終了コードの場合は例外を送出する。</param>
    /// <returns>コマンドの出力と終了コードを得るタスク。正常終了コードの場合のみ結果を得られる。</returns>
    public static Task<CmdResult> success(this Task<CmdResult> self, params int[] codes)
        => self.AsSuccessCode(codes.Length == 0 ? null : codes);

    /// <summary>コマンド実行結果から終了コードを取得する。</summary>
    /// <param name="self">コマンド実行結果を得るタスク</param>
    /// <returns>終了コードを得るタスク。終了コードは検証されない。</returns>
    public static async Task<int> code(this Task<CmdResult> self)
        => (await self.ConfigureAwait(false)).ExitCode;

    /// <summary>コマンド実行結果から出力テキストを取得する。</summary>
    /// <param name="self">コマンド実行結果を得るタスク</param>
    /// <returns>終出力テキストを得るタスク。終了コードは検証されない。</returns>
    public static async Task<string> output(this Task<CmdResult> self)
        => (await self.ConfigureAwait(false)).Output;
}

