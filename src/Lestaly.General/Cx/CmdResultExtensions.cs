namespace Lestaly.Cx;

/// <summary>コマンドをシンプルに実行するための拡張メソッド</summary>
[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:命名スタイル", Justification = "このクラスでは意図的に小文字メソッドを使っている")]
public static class CmdResultExtensions
{
    /// <summary>コマンド実行タスクに対する拡張メンバ</summary>
    /// <param name="self">コマンド実行結果を得るタスク</param>
    extension(Task<CmdResult> self)
    {
        /// <summary>コマンド実行結果に対し、指定の終了コードを正常とする検証を行う。</summary>
        /// <param name="codes">正常とみなす終了コード。指定しない場合はゼロのみを正常とみなす。正常以外の終了コードの場合は例外を送出する。</param>
        /// <returns>コマンドの出力と終了コードを得るタスク。正常終了コードの場合のみ結果を得られる。</returns>
        public Task<CmdResult> success(params int[] codes)
            => self.AsSuccessCode(codes.Length == 0 ? null : codes);

        /// <summary>コマンド実行結果から終了コードを取得する。</summary>
        /// <returns>終了コードを得るタスク。終了コードは検証されない。</returns>
        public async Task<int> code()
            => (await self.ConfigureAwait(false)).ExitCode;

        /// <summary>コマンド実行結果から出力テキストを取得する。</summary>
        /// <param name="trim">出力の前後空白をトリムするか否か</param>
        /// <returns>出力テキストを得るタスク。終了コードは検証されない。</returns>
        public async Task<string> output(bool trim = false)
        {
            var result = await self.ConfigureAwait(false);
            return trim ? result.Output.Trim() : result.Output;
        }
    }

}

