using System.Runtime.CompilerServices;

namespace Lestaly.Cx;

/// <summary>
/// コマンドをシンプルに実行するための拡張メソッド
/// </summary>
[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:命名スタイル", Justification = "このクラスでは意図的に小文字メソッドを使っている")]
public static class CmdFileInfoExtensions
{
    extension(FileInfo self)
    {
        /// <summary>ファイルを実行ファイルとみなしてプロセスを実行する</summary>
        /// <returns>コマンドの標準出力と標準エラーのテキストを得るAwaiter。出力テキストは正常終了コードの場合のみ得られる。</returns>
        public TaskAwaiter<string> GetAwaiter()
            => new CmdCx(self.FullName).result().success().output().GetAwaiter();

        /// <summary>文字列をコマンドラインとみなしてプロセスを実行する</summary>
        /// <returns>コマンドの実行結果(出力と終了コード)を得るタスク。終了コードは検証されない。</returns>
        public Task<CmdResult> launch()
            => new CmdCx(self.FullName).result();
    }
}

