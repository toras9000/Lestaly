using System.Diagnostics.CodeAnalysis;

namespace Lestaly;

/// <summary>ファイル処理オプション</summary>
/// <param name="Recurse">再帰検索を行うか否か</param>
/// <param name="Handling">処理対象設定</param>
/// <param name="Sort">ファイル名/ディレクトリ名でソートするか否か。</param>
/// <param name="Buffered">検索結果をバッファリングしてから列挙するか否か</param>
/// <param name="SkipInaccessible">アクセスできないファイル/ディレクトリをスキップするか否か</param>
/// <param name="SkipAttributes">スキップ対象のファイル属性</param>
/// <param name="FileFilter">ファイル列挙フィルタ</param>
/// <param name="DirectoryFilter">ディレクトリ列挙フィルタ</param>
public record VisitFilesOptions(
    bool Recurse = true,
    VisitFilesHandling? Handling = null,
    bool Sort = true,
    bool Buffered = true,
    bool SkipInaccessible = false,
    FileAttributes SkipAttributes = FileAttributes.Hidden | FileAttributes.System,
    Func<FileInfo, bool>? FileFilter = default,
    Func<DirectoryInfo, bool>? DirectoryFilter = default
);

/// <summary>ファイル検索時の処理対象設定</summary>
/// <param name="File">ファイルを処理対象にするか否か</param>
/// <param name="Directory">ディレクトリを処理対象にするか否か</param>
public record VisitFilesHandling(bool File = true, bool Directory = false)
{
    /// <summary>デフォルトの処理対象設定</summary>
    public static readonly VisitFilesHandling Default = new();

    /// <summary>ファイルとディレクトリを処理対象とする設定</summary>
    public static readonly VisitFilesHandling All = new(File: true, Directory: true);

    /// <summary>ファイルのみを処理対象とする設定</summary>
    public static readonly VisitFilesHandling OnlyFile = new(File: true, Directory: false);

    /// <summary>ディレクトリのみを処理対象とする設定</summary>
    public static readonly VisitFilesHandling OnlyDirectory = new(File: false, Directory: true);
}

/// <summary>
/// 列挙処理でのファイル/ディレクトリに対する処理情報
/// </summary>
/// <remarks>
/// この情報はファイル列挙処理にて、ファイルまたはディレクトリに対する処理デリゲートのパラメータとして渡される。
/// 処理デリゲートではこのオブジェクトに対してフラグを設定することで列挙動作の中断を指示することができる。
/// 列挙オプションでディレクトリに対するハンドリングが有効な場合、処理デリゲートはディレクトリに対しても呼び出される。
/// ディレクトリに対する処理デリゲートの呼び出しでは<see cref="File"/>プロパティがnullとなるため、それによってディレクトリ対象であるかを判断できる。
/// </remarks>
public interface IVisitFilesContext
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

    /// <summary>ファイルが対称であるか否か。</summary>
    [MemberNotNullWhen(true, nameof(File))]
    bool IsFile => this.File != null;
}

/// <summary>
/// 列挙処理でのファイル/ディレクトリに対する処理情報と結果設定
/// </summary>
public interface IVisitFilesContext<TResult> : IVisitFilesContext
{
    /// <summary>列挙対象のファイル/ディレクトリに対する処理結果を設定する。</summary>
    /// <param name="value">結果値</param>
    void SetResult(TResult value);
}

/// <summary>列挙したファイル/ディレクトリに対する変換処理デリゲート型</summary>
/// <typeparam name="TResult">変換結果の型</typeparam>
/// <param name="context">変換コンテキストデータ</param>
public delegate void VisitFilesWalker<TResult>(IVisitFilesContext<TResult> context);

/// <summary>列挙したファイル/ディレクトリに対する非同期変換処理デリゲート型</summary>
/// <typeparam name="TResult">変換結果の型</typeparam>
/// <param name="context">変換コンテキストデータ</param>
/// <returns>処理タスク</returns>
public delegate ValueTask AsyncVisitFilesWalker<TResult>(IVisitFilesContext<TResult> context);
