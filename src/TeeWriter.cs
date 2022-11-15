using System.Text;
using CometFlavor.Collections;

namespace Lestaly;

/// <summary>
/// 複数のテキストライターに出力するクラス
/// </summary>
/// <remarks>
/// 登録された複数のテキストライター全てに出力を行うメソッドを提供する。
/// ライターの登録時はリソース管理対象としてこのクラスの破棄時に同時に破棄する方法と、単に出力の対象としてだけ登録する2種類の方法を提供する。
/// </remarks>
public class TeeWriter : IDisposable
{
    // 構築
    #region コンストラクタ
    /// <summary>デフォルトコンストラクタ</summary>
    public TeeWriter()
    {
        this.resources = new CombinedDisposables();
        this.companions = new List<TextWriter>();
    }
    #endregion

    // 公開メソッド
    #region 紐づけ
    /// <summary>ライターオブジェクトを出力対象に加え、リソース管理に含める。</summary>
    /// <param name="writer">追加するライターオブジェクト</param>
    /// <returns>自身のインスタンス</returns>
    public TeeWriter Bind(TextWriter writer)
    {
        if (this.disposedFlag) throw new ObjectDisposedException(this.GetType().FullName);

        if (!this.companions.Contains(writer)) this.companions.Add(writer);
        if (!this.resources.Contains(writer)) this.resources.Add(writer);
        return this;
    }

    /// <summary>ライターオブジェクトを出力対象に加え、リソース管理に含める。</summary>
    /// <param name="writers">追加するライターオブジェクト</param>
    /// <returns>自身のインスタンス</returns>
    public TeeWriter Bind(params TextWriter[] writers)
    {
        if (this.disposedFlag) throw new ObjectDisposedException(this.GetType().FullName);

        foreach (var writer in writers)
        {
            if (!this.companions.Contains(writer)) this.companions.Add(writer);
            if (!this.resources.Contains(writer)) this.resources.Add(writer);
        }
        return this;
    }

    /// <summary>ライターオブジェクトを出力対象に加えるが、リソース管理には含めない。</summary>
    /// <param name="writer">追加するライターオブジェクト</param>
    /// <returns>自身のインスタンス</returns>
    public TeeWriter With(TextWriter writer)
    {
        if (this.disposedFlag) throw new ObjectDisposedException(this.GetType().FullName);

        if (!this.companions.Contains(writer)) this.companions.Add(writer);
        return this;
    }

    /// <summary>ライターオブジェクトを出力対象に加えるが、リソース管理には含めない。</summary>
    /// <param name="writers">追加するライターオブジェクト</param>
    /// <returns>自身のインスタンス</returns>
    public TeeWriter With(params TextWriter[] writers)
    {
        if (this.disposedFlag) throw new ObjectDisposedException(this.GetType().FullName);

        foreach (var writer in writers)
        {
            if (!this.companions.Contains(writer)) this.companions.Add(writer);
        }
        return this;
    }

    /// <summary>ライターオブジェクトを出力対象およびリソース管理対象から除外する。</summary>
    /// <param name="writer">除外するライターオブジェクト</param>
    /// <returns>自身のインスタンス</returns>
    public TeeWriter Leave(TextWriter writer)
    {
        if (this.disposedFlag) throw new ObjectDisposedException(this.GetType().FullName);

        this.companions.Remove(writer);
        this.resources.Remove(writer);
        return this;
    }
    #endregion

    #region 出力
    /// <summary>各ライターに出力する</summary>
    /// <param name="value">出力対象</param>
    /// <returns>自身のインスタンス</returns>
    public virtual TeeWriter Write(string? value)
    {
        foreach (var writer in this.companions)
        {
            writer.Write(value);
        }
        return this;
    }

    /// <summary>各ライターに出力する</summary>
    /// <param name="value">出力対象</param>
    /// <returns>自身のインスタンス</returns>
    public virtual TeeWriter Write(ReadOnlySpan<char> value)
    {
        foreach (var writer in this.companions)
        {
            writer.Write(value);
        }
        return this;
    }

    /// <summary>各ライターに出力する</summary>
    /// <param name="value">出力対象</param>
    /// <returns>自身のインスタンス</returns>
    public virtual TeeWriter Write(StringBuilder? value)
    {
        foreach (var writer in this.companions)
        {
            writer.Write(value);
        }
        return this;
    }

    /// <summary>各ライターに出力する</summary>
    /// <param name="value">出力対象</param>
    /// <returns>自身のインスタンス</returns>
    public virtual TeeWriter Write(object? value)
    {
        foreach (var writer in this.companions)
        {
            writer.Write(value);
        }
        return this;
    }

    /// <summary>各ライターに改行を出力する</summary>
    /// <returns>自身のインスタンス</returns>
    public virtual TeeWriter WriteLine()
    {
        foreach (var writer in this.companions)
        {
            writer.WriteLine();
        }
        return this;
    }

    /// <summary>各ライターに改行付きで出力する</summary>
    /// <param name="value">出力対象</param>
    /// <returns>自身のインスタンス</returns>
    public virtual TeeWriter WriteLine(string? value)
    {
        foreach (var writer in this.companions)
        {
            writer.WriteLine(value);
        }
        return this;
    }

    /// <summary>各ライターに改行付きで出力する</summary>
    /// <param name="value">出力対象</param>
    /// <returns>自身のインスタンス</returns>
    public virtual TeeWriter WriteLine(ReadOnlySpan<char> value)
    {
        foreach (var writer in this.companions)
        {
            writer.WriteLine(value);
        }
        return this;
    }

    /// <summary>各ライターに改行付きで出力する</summary>
    /// <param name="value">出力対象</param>
    /// <returns>自身のインスタンス</returns>
    public virtual TeeWriter WriteLine(StringBuilder? value)
    {
        foreach (var writer in this.companions)
        {
            writer.WriteLine(value);
        }
        return this;
    }

    /// <summary>各ライターに改行付きで出力する</summary>
    /// <param name="value">出力対象</param>
    /// <returns>自身のインスタンス</returns>
    public virtual TeeWriter WriteLine(object? value)
    {
        foreach (var writer in this.companions)
        {
            writer.WriteLine(value);
        }
        return this;
    }

    /// <summary>各ライターに非同期出力する</summary>
    /// <param name="value">出力対象</param>
    /// <returns>自身のインスタンス</returns>
    public virtual async Task<TeeWriter> WriteAsync(string? value)
    {
        foreach (var writer in this.companions)
        {
            await writer.WriteAsync(value).ConfigureAwait(false);
        }
        return this;
    }

    /// <summary>各ライターに非同期出力する</summary>
    /// <param name="value">出力対象</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>自身のインスタンス</returns>
    public virtual async Task<TeeWriter> WriteAsync(ReadOnlyMemory<char> value, CancellationToken cancelToken = default)
    {
        foreach (var writer in this.companions)
        {
            await writer.WriteAsync(value, cancelToken).ConfigureAwait(false);
        }
        return this;
    }

    /// <summary>各ライターに非同期出力する</summary>
    /// <param name="value">出力対象</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>自身のインスタンス</returns>
    public virtual async Task<TeeWriter> WriteAsync(StringBuilder? value, CancellationToken cancelToken = default)
    {
        foreach (var writer in this.companions)
        {
            await writer.WriteAsync(value, cancelToken).ConfigureAwait(false);
        }
        return this;
    }

    /// <summary>各ライターに改行を非同期出力する</summary>
    /// <returns>自身のインスタンス</returns>
    public virtual async Task<TeeWriter> WriteLineAsync()
    {
        foreach (var writer in this.companions)
        {
            await writer.WriteLineAsync().ConfigureAwait(false);
        }
        return this;
    }

    /// <summary>各ライターに改行付きで非同期出力する</summary>
    /// <param name="value">出力対象</param>
    /// <returns>自身のインスタンス</returns>
    public virtual async Task<TeeWriter> WriteLineAsync(string? value)
    {
        foreach (var writer in this.companions)
        {
            await writer.WriteLineAsync(value).ConfigureAwait(false);
        }
        return this;
    }

    /// <summary>各ライターに改行付きで非同期出力する</summary>
    /// <param name="value">出力対象</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>自身のインスタンス</returns>
    public virtual async Task<TeeWriter> WriteLineAsync(ReadOnlyMemory<char> value, CancellationToken cancelToken = default)
    {
        foreach (var writer in this.companions)
        {
            await writer.WriteLineAsync(value, cancelToken).ConfigureAwait(false);
        }
        return this;
    }

    /// <summary>各ライターに改行付きで非同期出力する</summary>
    /// <param name="value">出力対象</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>自身のインスタンス</returns>
    public virtual async Task<TeeWriter> WriteLineAsync(StringBuilder? value, CancellationToken cancelToken = default)
    {
        foreach (var writer in this.companions)
        {
            await writer.WriteLineAsync(value, cancelToken).ConfigureAwait(false);
        }
        return this;
    }

    /// <summary>各ライターをフラッシュする。</summary>
    /// <returns>自身のインスタンス</returns>
    public virtual TeeWriter Flush()
    {
        foreach (var writer in this.companions)
        {
            writer.Flush();
        }
        return this;
    }

    /// <summary>各ライターをフラッシュする。</summary>
    /// <returns>自身のインスタンス</returns>
    public virtual async Task<TeeWriter> FlushAsync()
    {
        foreach (var writer in this.companions)
        {
            await writer.FlushAsync().ConfigureAwait(false);
        }
        return this;
    }
    #endregion

    #region 破棄
    /// <summary>リソース破棄</summary>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
    #endregion

    // 公開メソッド
    #region 破棄
    /// <summary>リソース破棄</summary>
    /// <param name="disposing">マネージ破棄過程であるか</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposedFlag)
        {
            if (disposing)
            {
                this.resources.Dispose();
            }

            this.resources = null!;
            this.companions = null!;
            this.disposedFlag = true;
        }
    }
    #endregion

    // 非公開フィールド
    #region リソース管理
    /// <summary>破棄済みフラグ</summary>
    private bool disposedFlag;

    /// <summary>リソース管理対象コレクション</summary>
    private CombinedDisposables resources;

    /// <summary>出力対象コレクション</summary>
    private List<TextWriter> companions;
    #endregion
}
