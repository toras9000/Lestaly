using System.Globalization;

namespace LestalyTest;

/// <summary>
/// カルチャ設定区間定義
/// </summary>
public class CulturePeriod : IDisposable
{
    // 構築
    #region コンストラクタ
    /// <summary>設定するカルチャ情報を指定するコンストラクタ</summary>
    /// <param name="info">設定するカルチャ</param>
    /// <param name="withUi">UIカルチャを指定するか否か</param>
    public CulturePeriod(CultureInfo info, bool withUi = false)
    {
        this.Culture = info;
        this.WithUI = withUi;
        this.Original = CultureInfo.CurrentCulture;
        this.UiOriginal = CultureInfo.CurrentUICulture;

        CultureInfo.CurrentCulture = this.Culture;
        if (this.WithUI) CultureInfo.CurrentUICulture = this.Culture;
    }

    /// <summary>設定するカルチャ情報を指定するコンストラクタ</summary>
    /// <param name="lcid">設定するカルチャのLCID</param>
    /// <param name="withUi">UIカルチャを指定するか否か</param>
    public CulturePeriod(int lcid, bool withUi = false) : this(CultureInfo.GetCultureInfo(lcid)) { }

    /// <summary>設定するカルチャ情報を指定するコンストラクタ</summary>
    /// <param name="culture">設定するカルチャ名</param>
    /// <param name="withUi">UIカルチャを指定するか否か</param>
    public CulturePeriod(string culture, bool withUi = false) : this(CultureInfo.GetCultureInfo(culture)) { }
    #endregion

    // 公開プロパティ
    #region 設定情報
    /// <summary>設定したカルチャ</summary>
    public CultureInfo Culture { get; }

    /// <summary>UIカルチャを指定するか否か</summary>
    public bool WithUI { get; }
    #endregion

    #region 元情報
    /// <summary>元のカルチャ</summary>
    public CultureInfo Original { get; }

    /// <summary>元のUIカルチャ</summary>
    public CultureInfo UiOriginal { get; }
    #endregion

    // 公開メソッド
    #region 破棄
    /// <summary>
    /// リソース破棄
    /// </summary>
    public void Dispose()
    {
        if (!this.disposed)
        {
            CultureInfo.CurrentCulture = this.Original;
            if (this.WithUI) CultureInfo.CurrentUICulture = this.UiOriginal;
            this.disposed = true;
        }
    }
    #endregion

    // 非公開フィールド
    #region 状態情報
    /// <summary>破棄済みフラグ</summary>
    private bool disposed;
    #endregion
}
