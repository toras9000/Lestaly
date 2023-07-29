using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using CometFlavor.Collections;

namespace Lestaly;

/// <summary>
/// 複数のテキストライターへの出力インタフェース
/// </summary>
public interface ITeeWriter
{
    #region 出力
    /// <summary>各ライターに出力する</summary>
    /// <param name="value">出力対象</param>
    /// <returns>自身のインスタンス</returns>
    ITeeWriter Write(char value);

    /// <summary>各ライターに出力する</summary>
    /// <param name="value">出力対象</param>
    /// <returns>自身のインスタンス</returns>
    ITeeWriter Write(string? value);

    /// <summary>各ライターに出力する</summary>
    /// <param name="value">出力対象</param>
    /// <returns>自身のインスタンス</returns>
    ITeeWriter Write(ReadOnlySpan<char> value);

    /// <summary>各ライターに出力する</summary>
    /// <param name="value">出力対象</param>
    /// <returns>自身のインスタンス</returns>
    ITeeWriter Write(StringBuilder? value);

    /// <summary>各ライターに出力する</summary>
    /// <param name="value">出力対象</param>
    /// <returns>自身のインスタンス</returns>
    ITeeWriter Write(object? value);

    /// <summary>各ライターに改行を出力する</summary>
    /// <returns>自身のインスタンス</returns>
    ITeeWriter WriteLine();

    /// <summary>各ライターに改行付きで出力する</summary>
    /// <param name="value">出力対象</param>
    /// <returns>自身のインスタンス</returns>
    ITeeWriter WriteLine(string? value);

    /// <summary>各ライターに改行付きで出力する</summary>
    /// <param name="value">出力対象</param>
    /// <returns>自身のインスタンス</returns>
    ITeeWriter WriteLine(ReadOnlySpan<char> value);

    /// <summary>各ライターに改行付きで出力する</summary>
    /// <param name="value">出力対象</param>
    /// <returns>自身のインスタンス</returns>
    ITeeWriter WriteLine(StringBuilder? value);

    /// <summary>各ライターに改行付きで出力する</summary>
    /// <param name="value">出力対象</param>
    /// <returns>自身のインスタンス</returns>
    ITeeWriter WriteLine(object? value);

    /// <summary>各ライターをフラッシュする。</summary>
    /// <returns>自身のインスタンス</returns>
    ITeeWriter Flush();

    /// <summary>各ライターに非同期出力する</summary>
    /// <param name="value">出力対象</param>
    /// <returns>自身のインスタンス</returns>
    Task<ITeeWriter> WriteAsync(string? value);

    /// <summary>各ライターに非同期出力する</summary>
    /// <param name="value">出力対象</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>自身のインスタンス</returns>
    Task<ITeeWriter> WriteAsync(ReadOnlyMemory<char> value, CancellationToken cancelToken = default);

    /// <summary>各ライターに非同期出力する</summary>
    /// <param name="value">出力対象</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>自身のインスタンス</returns>
    Task<ITeeWriter> WriteAsync(StringBuilder? value, CancellationToken cancelToken = default);

    /// <summary>各ライターに改行を非同期出力する</summary>
    /// <returns>自身のインスタンス</returns>
    Task<ITeeWriter> WriteLineAsync();

    /// <summary>各ライターに改行付きで非同期出力する</summary>
    /// <param name="value">出力対象</param>
    /// <returns>自身のインスタンス</returns>
    Task<ITeeWriter> WriteLineAsync(string? value);

    /// <summary>各ライターに改行付きで非同期出力する</summary>
    /// <param name="value">出力対象</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>自身のインスタンス</returns>
    Task<ITeeWriter> WriteLineAsync(ReadOnlyMemory<char> value, CancellationToken cancelToken = default);

    /// <summary>各ライターに改行付きで非同期出力する</summary>
    /// <param name="value">出力対象</param>
    /// <param name="cancelToken">キャンセルトークン</param>
    /// <returns>自身のインスタンス</returns>
    Task<ITeeWriter> WriteLineAsync(StringBuilder? value, CancellationToken cancelToken = default);

    /// <summary>各ライターをフラッシュする。</summary>
    /// <returns>自身のインスタンス</returns>
    Task<ITeeWriter> FlushAsync();
    #endregion
}

/// <summary>
/// 複数のテキストライターに出力するクラス
/// </summary>
/// <remarks>
/// 登録された複数のテキストライター全てに出力を行うメソッドを提供する。
/// ライターの登録時はリソース管理対象としてこのクラスの破棄時に同時に破棄する方法と、単に出力の対象としてだけ登録する2種類の方法を提供する。
/// </remarks>
public class TeeWriter : TextWriter, ITeeWriter, IDisposable
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

    // 公開プロパティ
    #region 
    /// <summary>出力を書き込む文字エンコーディングを返却する</summary>
    /// <remarks>
    /// このメソッドは紐づけられている最初の TextWriter インスタンスの値を返却する。
    /// </remarks>
    public override Encoding Encoding => this.companions.FirstOrDefault()?.Encoding ?? Encoding.UTF8;

    /// <summary>書式を制御するオブジェクトを返却する</summary>
    /// <remarks>
    /// このメソッドは紐づけられている最初の TextWriter インスタンスの値を返却する。
    /// </remarks>
    public override IFormatProvider FormatProvider => this.companions.FirstOrDefault()?.FormatProvider ?? CultureInfo.CurrentCulture;

    /// <summary>行終端文字列を返却する</summary>
    /// <remarks>
    /// このメソッドは設定時には全ての紐づけ TextWriter に反映し、取得時は最初の TextWriter インスタンスの値を返却する。
    /// </remarks>
    [AllowNull]
    public override string NewLine
    {
        get => this.companions.FirstOrDefault()?.NewLine ?? Environment.NewLine;
        set
        {
            foreach (var writer in this.companions)
            {
                writer.NewLine = base.NewLine = value;
            }
        }
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

    #region インタフェース
    /// <summary></summary>
    /// <returns></returns>
    public ITeeWriter AsFacade() => this;
    #endregion

    #region 出力
    /// <inheritdoc />
    public override void Write(char value) => this.AsFacade().Write(value);

    /// <inheritdoc />
    public override void Write(string? value) => this.AsFacade().Write(value);

    /// <inheritdoc />
    public override void Write(ReadOnlySpan<char> value) => this.AsFacade().Write(value);

    /// <inheritdoc />
    public override void Write(StringBuilder? value) => this.AsFacade().Write(value);

    /// <inheritdoc />
    public override void Write(object? value) => this.AsFacade().Write(value);

    /// <inheritdoc />
    public override void WriteLine() => this.AsFacade().WriteLine();

    /// <inheritdoc />
    public override void WriteLine(string? value) => this.AsFacade().WriteLine(value);

    /// <inheritdoc />
    public override void WriteLine(ReadOnlySpan<char> value) => this.AsFacade().WriteLine(value);

    /// <inheritdoc />
    public override void WriteLine(StringBuilder? value) => this.AsFacade().WriteLine(value);

    /// <inheritdoc />
    public override void WriteLine(object? value) => this.AsFacade().WriteLine(value);

    /// <inheritdoc />
    public override void Flush() => this.AsFacade().Flush();

    /// <inheritdoc />
    public override Task WriteAsync(string? value) => this.AsFacade().WriteAsync(value);

    /// <inheritdoc />
    public override Task WriteAsync(ReadOnlyMemory<char> value, CancellationToken cancelToken = default) => this.AsFacade().WriteAsync(value, cancelToken);

    /// <inheritdoc />
    public override Task WriteAsync(StringBuilder? value, CancellationToken cancelToken = default) => this.AsFacade().WriteAsync(value, cancelToken);

    /// <inheritdoc />
    public override Task WriteLineAsync() => this.AsFacade().WriteLineAsync();

    /// <inheritdoc />
    public override Task WriteLineAsync(string? value) => this.AsFacade().WriteLineAsync(value);

    /// <inheritdoc />
    public override Task WriteLineAsync(ReadOnlyMemory<char> value, CancellationToken cancelToken = default) => this.AsFacade().WriteLineAsync(value, cancelToken);

    /// <inheritdoc />
    public override Task WriteLineAsync(StringBuilder? value, CancellationToken cancelToken = default) => this.AsFacade().WriteLineAsync(value, cancelToken);

    /// <inheritdoc />
    public override Task FlushAsync() => this.AsFacade().FlushAsync();
    #endregion

    #region 出力 (インタフェース)
    /// <inheritdoc cref="ITeeWriter.Write(char)">
    ITeeWriter ITeeWriter.Write(char value)
    {
        foreach (var writer in this.companions)
        {
            writer.Write(value);
        }
        return this;
    }

    /// <inheritdoc cref="ITeeWriter.Write(string?)">
    ITeeWriter ITeeWriter.Write(string? value)
    {
        foreach (var writer in this.companions)
        {
            writer.Write(value);
        }
        return this;
    }

    /// <inheritdoc cref="ITeeWriter.Write(ReadOnlySpan{char})">
    ITeeWriter ITeeWriter.Write(ReadOnlySpan<char> value)
    {
        foreach (var writer in this.companions)
        {
            writer.Write(value);
        }
        return this;
    }

    /// <inheritdoc cref="ITeeWriter.Write(StringBuilder?)">
    ITeeWriter ITeeWriter.Write(StringBuilder? value)
    {
        foreach (var writer in this.companions)
        {
            writer.Write(value);
        }
        return this;
    }

    /// <inheritdoc cref="ITeeWriter.Write(object?)">
    ITeeWriter ITeeWriter.Write(object? value)
    {
        foreach (var writer in this.companions)
        {
            writer.Write(value);
        }
        return this;
    }

    /// <inheritdoc cref="ITeeWriter.WriteLine()">
    ITeeWriter ITeeWriter.WriteLine()
    {
        foreach (var writer in this.companions)
        {
            writer.WriteLine();
        }
        return this;
    }

    /// <inheritdoc cref="ITeeWriter.WriteLine(string?)">
    ITeeWriter ITeeWriter.WriteLine(string? value)
    {
        foreach (var writer in this.companions)
        {
            writer.WriteLine(value);
        }
        return this;
    }

    /// <inheritdoc cref="ITeeWriter.WriteLine(ReadOnlySpan{char})">
    ITeeWriter ITeeWriter.WriteLine(ReadOnlySpan<char> value)
    {
        foreach (var writer in this.companions)
        {
            writer.WriteLine(value);
        }
        return this;
    }

    /// <inheritdoc cref="ITeeWriter.WriteLine(StringBuilder?)">
    ITeeWriter ITeeWriter.WriteLine(StringBuilder? value)
    {
        foreach (var writer in this.companions)
        {
            writer.WriteLine(value);
        }
        return this;
    }

    /// <inheritdoc cref="ITeeWriter.WriteLine(object?)">
    ITeeWriter ITeeWriter.WriteLine(object? value)
    {
        foreach (var writer in this.companions)
        {
            writer.WriteLine(value);
        }
        return this;
    }

    /// <inheritdoc cref="ITeeWriter.Flush()">
    ITeeWriter ITeeWriter.Flush()
    {
        foreach (var writer in this.companions)
        {
            writer.Flush();
        }
        return this;
    }

    /// <inheritdoc cref="ITeeWriter.WriteAsync(string?)">
    async Task<ITeeWriter> ITeeWriter.WriteAsync(string? value)
    {
        foreach (var writer in this.companions)
        {
            await writer.WriteAsync(value).ConfigureAwait(false);
        }
        return this;
    }

    /// <inheritdoc cref="ITeeWriter.WriteAsync(ReadOnlyMemory{char}, CancellationToken)">
    async Task<ITeeWriter> ITeeWriter.WriteAsync(ReadOnlyMemory<char> value, CancellationToken cancelToken)
    {
        foreach (var writer in this.companions)
        {
            await writer.WriteAsync(value, cancelToken).ConfigureAwait(false);
        }
        return this;
    }

    /// <inheritdoc cref="ITeeWriter.WriteAsync(StringBuilder?, CancellationToken)">
    async Task<ITeeWriter> ITeeWriter.WriteAsync(StringBuilder? value, CancellationToken cancelToken)
    {
        foreach (var writer in this.companions)
        {
            await writer.WriteAsync(value, cancelToken).ConfigureAwait(false);
        }
        return this;
    }

    /// <inheritdoc cref="ITeeWriter.WriteLineAsync()">
    async Task<ITeeWriter> ITeeWriter.WriteLineAsync()
    {
        foreach (var writer in this.companions)
        {
            await writer.WriteLineAsync().ConfigureAwait(false);
        }
        return this;
    }

    /// <inheritdoc cref="ITeeWriter.WriteLineAsync(string?)">
    async Task<ITeeWriter> ITeeWriter.WriteLineAsync(string? value)
    {
        foreach (var writer in this.companions)
        {
            await writer.WriteLineAsync(value).ConfigureAwait(false);
        }
        return this;
    }

    /// <inheritdoc cref="ITeeWriter.WriteLineAsync(ReadOnlyMemory{char}, CancellationToken)">
    async Task<ITeeWriter> ITeeWriter.WriteLineAsync(ReadOnlyMemory<char> value, CancellationToken cancelToken)
    {
        foreach (var writer in this.companions)
        {
            await writer.WriteLineAsync(value, cancelToken).ConfigureAwait(false);
        }
        return this;
    }

    /// <inheritdoc cref="ITeeWriter.WriteLineAsync(StringBuilder?, CancellationToken)">
    async Task<ITeeWriter> ITeeWriter.WriteLineAsync(StringBuilder? value, CancellationToken cancelToken)
    {
        foreach (var writer in this.companions)
        {
            await writer.WriteLineAsync(value, cancelToken).ConfigureAwait(false);
        }
        return this;
    }

    /// <inheritdoc cref="ITeeWriter.FlushAsync()">
    async Task<ITeeWriter> ITeeWriter.FlushAsync()
    {
        foreach (var writer in this.companions)
        {
            await writer.FlushAsync().ConfigureAwait(false);
        }
        return this;
    }
    #endregion

    // 保護メソッド
    #region 破棄
    /// <inheritdoc />
    protected override void Dispose(bool disposing)
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
