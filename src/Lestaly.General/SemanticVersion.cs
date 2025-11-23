using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.RegularExpressions;

namespace Lestaly;

/// <summary>セマンティックバージョンを解釈するデータ型</summary>
/// <remarks>
/// このクラスはセマンティック バージョニング書式を厳密に解釈するものではない。
/// ある程度、それらしく、の精神で利用者(つまり作成者)が必要十分な扱いをする。
/// </remarks>
public class SemanticVersion : IEquatable<SemanticVersion>, IComparable<SemanticVersion>
{
    // 構築
    #region コンストラクタ
    /// <summary>解釈結果値を受け取るコンストラクタ</summary>
    /// <param name="major">メジャーバージョン</param>
    /// <param name="minor">マイナーバージョン</param>
    /// <param name="patch">パッチバージョン</param>
    /// <param name="filum">細かなバージョン</param>
    /// <param name="pre">プレリリースバージョン</param>
    /// <param name="build">ビルドメタデータ</param>
    public SemanticVersion(int major, int? minor = default, int? patch = default, int? filum = default, string? pre = default, string? build = default)
        : this(buildString(major, minor, patch, filum, pre, build), major, minor, patch, filum, pre, build)
    { }
    #endregion

    #region 静的：パース
    /// <summary>文字列をセマンティックバージョンとして解釈</summary>
    /// <param name="text">対象文字列</param>
    /// <param name="version">解釈結果。結果が真の場合に有効</param>
    /// <returns>解釈に成功したか否か</returns>
    public static bool TryParse(string text, [NotNullWhen(true)] out SemanticVersion? version)
    {
        version = default;

        // 文字列パターンにマッチ
        var match = VersionPattern.Match(text);
        if (!match.Success) return false;

        // メジャーバージョン解釈
        if (!int.TryParse(match.Groups["major"].ValueSpan, out var major)) return false;

        // サブバージョンパート解釈
        var subver = match.Groups["subver"];
        var minor = (0 < subver.Captures.Count && int.TryParse(subver.Captures[0].Value, out var s0)) ? s0 : default(int?);
        var patch = (1 < subver.Captures.Count && int.TryParse(subver.Captures[1].Value, out var s1)) ? s1 : default(int?);
        var filum = (2 < subver.Captures.Count && int.TryParse(subver.Captures[2].Value, out var s2)) ? s2 : default(int?);

        // 追加情報解釈
        var captPre = match.Groups["pre"];
        var captBuild = match.Groups["build"];
        var pre = captPre.Success ? captPre.Value : default;
        var build = captBuild.Success ? captBuild.Value : default;

        // インスタンス構築
        version = new SemanticVersion(text, major, minor, patch, filum, pre, build);

        return true;
    }

    /// <summary>文字列をセマンティックバージョンとして解釈</summary>
    /// <param name="text">対象文字列</param>
    /// <returns>解釈結果</returns>
    public static SemanticVersion Parse(string text)
        => TryParse(text, out var version) ? version : throw new FormatException("Unrecognized semver string");
    #endregion

    // 公開プロパティ
    #region バージョン情報
    /// <summary>解釈元の文字列</summary>
    public string Original { get; }

    /// <summary>メジャーバージョン</summary>
    public int Major { get; }

    /// <summary>マイナーバージョン</summary>
    public int? Minor { get; }

    /// <summary>パッチバージョン</summary>
    public int? Patch { get; }

    /// <summary>細かなバージョン</summary>
    public int? Filum { get; }

    /// <summary>プレリリースバージョン</summary>
    public string? PreRelease { get; }

    /// <summary>ビルドメタデータ</summary>
    public string? Build { get; }
    #endregion

    // 公開メソッド
    #region 文字列化
    /// <summary>文字列表現を得る</summary>
    /// <returns>解釈元の文字列</returns>
    public override string ToString() => this.Original;

    /// <summary>文字列表現を得る</summary>
    /// <returns>再構築した文字列</returns>
    public string ReformString()
    {
        if (this.reformed == null)
        {
            // 未構築ならば構築する。構築結果は保持しておく
            this.reformed = buildString(this.Major, this.Minor, this.Patch, this.Filum, this.PreRelease, this.Build);
        }
        return this.reformed;
    }
    #endregion

    #region 比較
    /// <inheritdoc />
    public int CompareTo(SemanticVersion? other)
    {
        if (object.ReferenceEquals(other, null)) return 1;

        if (this.Major != other.Major) return this.Major - other.Major;

        if (this.Minor.HasValue != other.Minor.HasValue) return this.Minor.HasValue ? 1 : -1;
        if (this.Minor.HasValue && other.Minor.HasValue && this.Minor.Value != other.Minor.Value) return this.Minor.Value - other.Minor.Value;

        if (this.Patch.HasValue != other.Patch.HasValue) return this.Patch.HasValue ? 1 : -1;
        if (this.Patch.HasValue && other.Patch.HasValue && this.Patch.Value != other.Patch.Value) return this.Patch.Value - other.Patch.Value;

        if (this.Filum.HasValue != other.Filum.HasValue) return this.Filum.HasValue ? 1 : -1;
        if (this.Filum.HasValue && other.Filum.HasValue && this.Filum.Value != other.Filum.Value) return this.Filum.Value - other.Filum.Value;

        if (string.CompareOrdinal(this.PreRelease, other.PreRelease) is var c1 && c1 != 0) return c1;
        if (string.CompareOrdinal(this.Build, other.Build) is var c2 && c2 != 0) return c2;

        return 0;
    }
    #endregion

    #region 等価性
    /// <inheritdoc />
    public override int GetHashCode()
        => HashCode.Combine(this.Major, this.Minor, this.Patch, this.Filum, this.PreRelease, this.Build);

    /// <inheritdoc />
    public override bool Equals(object? obj)
        => base.Equals(obj as SemanticVersion);

    /// <inheritdoc />
    public bool Equals(SemanticVersion? other)
    {
        if (object.ReferenceEquals(other, null)) return false;
        return this.Major == other.Major
            && this.Minor == other.Minor
            && this.Patch == other.Patch
            && this.Filum == other.Filum
            && this.PreRelease == other.PreRelease
            && this.Build == other.Build
            ;
    }
    #endregion

    #region 演算子のオーバロード
    /// <inheritdoc />
    public static bool operator ==(SemanticVersion? r1, SemanticVersion? r2) => object.ReferenceEquals(r1, r2) || (r1 is not null && r1.Equals(r2));

    /// <inheritdoc />
    public static bool operator !=(SemanticVersion? r1, SemanticVersion? r2) => !(r1 == r2);

    /// <inheritdoc />
    public static bool operator <(SemanticVersion r1, SemanticVersion r2) => r1.CompareTo(r2) < 0;

    /// <inheritdoc />
    public static bool operator <=(SemanticVersion r1, SemanticVersion r2) => r1.CompareTo(r2) <= 0;

    /// <inheritdoc />
    public static bool operator >(SemanticVersion r1, SemanticVersion r2) => 0 < r1.CompareTo(r2);

    /// <inheritdoc />
    public static bool operator >=(SemanticVersion r1, SemanticVersion r2) => 0 <= r1.CompareTo(r2);
    #endregion

    // 構築 (非公開)
    #region コンストラクタ
    /// <summary>解釈結果値を受け取るコンストラクタ</summary>
    /// <param name="original">解釈元の文字列</param>
    /// <param name="major">メジャーバージョン</param>
    /// <param name="minor">マイナーバージョン</param>
    /// <param name="patch">パッチバージョン</param>
    /// <param name="filum">細かなバージョン</param>
    /// <param name="pre">プレリリースバージョン</param>
    /// <param name="build">ビルドメタデータ</param>
    private SemanticVersion(string original, int major, int? minor, int? patch, int? filum, string? pre, string? build)
    {
        this.Original = original;
        this.Major = major;
        this.Minor = minor;
        this.Patch = patch;
        this.Filum = filum;
        this.PreRelease = pre;
        this.Build = build;
    }
    #endregion

    // 非公開プロパティ
    #region 解釈
    /// <summary>バージョン書式を解釈するパターン</summary>
    private static readonly Regex VersionPattern = new(@"^(?<major>\d+)(?:\.(?<subver>\d+)){0,3}(?:\-(?<pre>[^\+]+))?(?:\+(?<build>.+))?$");
    #endregion

    #region キャッシュ
    /// <summary>再構築文字列キャッシュ</summary>
    private string? reformed;
    #endregion

    // 非公開メソッド
    #region 静的：文字列化
    /// <summary>バージョン文字列を構築する</summary>
    /// <param name="major">メジャーバージョン</param>
    /// <param name="minor">マイナーバージョン</param>
    /// <param name="patch">パッチバージョン</param>
    /// <param name="filum">細かなバージョン</param>
    /// <param name="pre">プレリリースバージョン</param>
    /// <param name="build">ビルドメタデータ</param>
    private static string buildString(int major, int? minor = default, int? patch = default, int? filum = default, string? pre = default, string? build = default)
    {
        var builder = new StringBuilder();
        builder.Append(major);
        if (minor.HasValue) builder.Append('.').Append(minor);
        if (patch.HasValue) builder.Append('.').Append(patch);
        if (filum.HasValue) builder.Append('.').Append(filum);
        if (!string.IsNullOrWhiteSpace(pre)) builder.Append('-').Append(pre);
        if (!string.IsNullOrWhiteSpace(build)) builder.Append('+').Append(build);
        return builder.ToString();
    }
    #endregion

}
