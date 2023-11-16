namespace Lestaly;

/// <summary>ファイル処理オプション</summary>
/// <param name="Recurse">再帰検索を行うか否か</param>
/// <param name="DirectoryHandling">ディレクトリに対する処理呼び出しを行うか否か</param>
/// <param name="Sort">ファイル名/ディレクトリ名でソートするか否か。</param>
/// <param name="Buffered">検索結果をバッファリングしてから列挙するか否か</param>
public record SelectFilesOptions(bool Recurse = true, bool DirectoryHandling = false, bool Sort = true, bool Buffered = true);

/// <summary>
/// 列挙処理でのファイル/ディレクトリに対する処理情報
/// </summary>
/// <remarks>
/// この情報はファイル列挙処理にて、ファイルまたはディレクトリに対する処理デリゲートのパラメータとして渡される。
/// 処理デリゲートではこのオブジェクトに対してフラグを設定することで列挙動作の中断を指示することができる。
/// 列挙オプションでディレクトリに対するハンドリングが有効な場合、処理デリゲートはディレクトリに対しても呼び出される。
/// ディレクトリに対する処理デリゲートの呼び出しでは<see cref="File"/>プロパティがnullとなるため、それによってディレクトリ対象であるかを判断できる。
/// </remarks>
public interface IFileWalker
{
    /// <summary>列挙対象ディレクトリまたはファイル (列挙対象アイテム)</summary>
    FileSystemInfo Item { get; }
    /// <summary>列挙対象ディレクトリ、あるいは列挙対象ファイルの格納ディレクトリ</summary>
    DirectoryInfo Directory { get; }
    /// <summary>列挙対象ファイル。ディレクトリ対象の場合は null となる。</summary>
    FileInfo? File { get; }
    /// <summary>「現在の階層」の列挙中断を指定するフラグ。対象がファイルかディレクトリかによって動作は異なる。</summary>
    /// <remarks>
    /// ファイルに対する処理デリゲート呼び出しで中断フラグが立てられた場合、同階層の残りのファイル列挙を中断し、(存在するならば)サブディレクトリ列挙に進む。
    /// ディレクトリに対する処理デリゲート呼び出しで中断フラグが立てられた場合、そのディレクトリ配下の列挙に入らずに同一階層の次ディレクトリの列挙に進む。
    /// </remarks>
    bool Break { get; set; }
    /// <summary>列挙処理自体の終了を指定するフラグ。</summary>
    bool Exit { get; set; }
}

/// <summary>
/// 列挙処理でのファイル/ディレクトリに対する処理情報と結果設定
/// </summary>
public interface IFileConverter<TResult> : IFileWalker
{
    /// <summary>列挙対象のファイル/ディレクトリに対する処理結果を設定する。</summary>
    /// <param name="value">結果値</param>
    void SetResult(TResult value);
}

/// <summary>列挙したファイル/ディレクトリに対する変換処理デリゲート型</summary>
/// <typeparam name="TResult">変換結果の型</typeparam>
/// <param name="context">変換コンテキストデータ</param>
public delegate void SelectFilesWalker<TResult>(IFileConverter<TResult> context);

/// <summary>列挙したファイル/ディレクトリに対する非同期変換処理デリゲート型</summary>
/// <typeparam name="TResult">変換結果の型</typeparam>
/// <param name="context">変換コンテキストデータ</param>
/// <returns>処理タスク</returns>
public delegate ValueTask AsyncSelectFilesWalker<TResult>(IFileConverter<TResult> context);

/// <summary>列挙したファイル/ディレクトリに対する処理デリゲート型</summary>
/// <param name="context">列挙コンテキストデータ</param>
public delegate void DoFilesWalker(IFileWalker context);

/// <summary>列挙したファイル/ディレクトリに対する非同期処理デリゲート型</summary>
/// <param name="context">列挙コンテキストデータ</param>
/// <returns>処理タスク</returns>
public delegate ValueTask AsyncDoFilesWalker(IFileWalker context);
