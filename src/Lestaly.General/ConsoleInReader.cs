using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Lestaly;

/// <summary>
/// Console.In の 代替リーダー
/// </summary>
/// <remarks>
/// このクラスは主に Async 操作を利用する目的の Console.In 代替リーダーとなる。
/// 直接のインスタンス生成は不可で、ConsoleWig.InReader 経由のシングルトンインスタンスを利用する。
/// </remarks>
public class ConsoleInReader : TextReader
{
    // 公開プロパティ
    #region 設定
    /// <summary>入力エンコーディングを取得または設定する</summary>
    public Encoding InputEncoding
    {
        get => this.encoding;
        set
        {
            if (this.encoding != value)
            {
                this.encoding = value;
                remakeReader();
            }
        }
    }
    #endregion

    // 公開メソッド
    #region I/O
    /// <inheritdoc />
    public override int Peek() => this.reader.Peek();

    /// <inheritdoc />
    public override int Read()
        => this.reader.Read();

    /// <inheritdoc />
    public override int Read(char[] buffer, int index, int count)
        => this.reader.Read(buffer, index, count);

    /// <inheritdoc />
    public override int Read(Span<char> buffer)
        => this.reader.Read(buffer);

    /// <inheritdoc />
    public override string? ReadLine()
        => this.reader.ReadLine();

    /// <inheritdoc />
    public override int ReadBlock(char[] buffer, int index, int count)
        => this.reader.ReadBlock(buffer, index, count);

    /// <inheritdoc />
    public override int ReadBlock(Span<char> buffer)
        => this.reader.ReadBlock(buffer);

    /// <inheritdoc />
    public override string ReadToEnd()
        => this.reader.ReadToEnd();

    /// <inheritdoc />
    public override Task<int> ReadAsync(char[] buffer, int index, int count)
        => readAsync(buffer?.AsMemory(index, count), CancellationToken.None).AsTask();

    /// <inheritdoc />
    public override ValueTask<int> ReadAsync(Memory<char> buffer, CancellationToken cancellationToken = default)
        => readAsync(buffer, cancellationToken);

    /// <inheritdoc />
    public override Task<string?> ReadLineAsync()
        => readLineAsync(CancellationToken.None).AsTask();

    /// <inheritdoc />
    public override ValueTask<string?> ReadLineAsync(CancellationToken cancellationToken)
        => readLineAsync(cancellationToken);

    /// <inheritdoc />
    public override Task<int> ReadBlockAsync(char[] buffer, int index, int count)
        => readBlockAsync(buffer?.AsMemory(index, count), CancellationToken.None).AsTask();

    /// <inheritdoc />
    public override ValueTask<int> ReadBlockAsync(Memory<char> buffer, CancellationToken cancellationToken = default)
        => readBlockAsync(buffer, cancellationToken);

    /// <inheritdoc />
    public override Task<string> ReadToEndAsync()
        => readToEndAsync(CancellationToken.None);

    /// <inheritdoc />
    public override Task<string> ReadToEndAsync(CancellationToken cancellationToken)
        => readToEndAsync(cancellationToken);
    #endregion

    #region 破棄
    /// <inheritdoc />
    public override void Close()
    {
        // シングルトンとしての利用が前提なので、なにもしない。
    }

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        // シングルトンとしての利用が前提なので、なにもしない。
    }
    #endregion

    // 構築(内部)
    #region コンストラクタ
    /// <summary>デフォルトコンストラクタ</summary>
    internal ConsoleInReader()
    {
        this.stdin = Console.OpenStandardInput() ?? throw new InvalidOperationException();
        this.encoding = Console.InputEncoding;
        remakeReader();
    }
    #endregion

    // 非公開フィールド
    #region 標準入力
    /// <summary>標準入力ストリーム</summary>
    private readonly Stream stdin;

    /// <summary>標準入力の読み取りエンコーディング</summary>
    private Encoding encoding;

    /// <summary>標準入力の読み取りリーダー</summary>
    private StreamReader reader;
    #endregion

    #region  排他
    /// <summary>排他用キーオブジェクト</summary>
#if NET9_0_OR_GREATER
    private readonly Lock callSync = new();
#else
    private readonly object callSync = new();
#endif
    #endregion

    #region 読み取り処理
    /// <summary>ReadAsync を呼び出すタスク</summary>
    private Task<int>? readTask;

    /// <summary>ReadLineAsync を呼び出すタスク</summary>
    private Task<string?>? readLineTask;

    /// <summary>ReadBlockAsync を呼び出すタスク</summary>
    private Task<int>? readBlockTask;

    /// <summary>ReadToEndAsync を呼び出すタスク</summary>
    private Task<string>? readToEndTask;
    #endregion

    // 非公開メソッド
    #region 状態更新
    /// <summary>標準入力の読み取りリーダーを再構築する</summary>
    [MemberNotNull(nameof(reader))]
    private void remakeReader()
    {
        lock (this.callSync)
        {
            try { this.reader?.Dispose(); } catch { }
            this.reader = new StreamReader(this.stdin, this.encoding ?? Encoding.Default, leaveOpen: true);
        }
    }
    #endregion

    #region 状態更新
    private async ValueTask<int> readAsync(Memory<char>? buffer, CancellationToken cancelToken)
    {
        if (!buffer.HasValue) throw new ArgumentNullException(nameof(buffer));

        lock (this.callSync)
        {
            this.readTask ??= this.reader.ReadAsync(buffer.Value, CancellationToken.None).AsTask();
        }

        var result = await this.readTask.WaitAsync(cancelToken).ConfigureAwait(false);

        lock (this.callSync)
        {
            this.readTask = null;
        }

        return result;
    }

    private async ValueTask<string?> readLineAsync(CancellationToken cancelToken)
    {
        lock (this.callSync)
        {
            this.readLineTask ??= this.reader.ReadLineAsync(CancellationToken.None).AsTask();
        }

        var result = await this.readLineTask.WaitAsync(cancelToken).ConfigureAwait(false);

        lock (this.callSync)
        {
            this.readLineTask = null;
        }

        return result;
    }

    private async ValueTask<int> readBlockAsync(Memory<char>? buffer, CancellationToken cancelToken)
    {
        if (!buffer.HasValue) throw new ArgumentNullException(nameof(buffer));

        lock (this.callSync)
        {
            this.readBlockTask ??= this.reader.ReadBlockAsync(buffer.Value, CancellationToken.None).AsTask();
        }

        var result = await this.readBlockTask.WaitAsync(cancelToken).ConfigureAwait(false);

        lock (this.callSync)
        {
            this.readBlockTask = null;
        }

        return result;
    }

    private async Task<string> readToEndAsync(CancellationToken cancelToken)
    {
        lock (this.callSync)
        {
            this.readToEndTask ??= this.reader.ReadToEndAsync(CancellationToken.None);
        }

        var result = await this.readToEndTask.WaitAsync(cancelToken).ConfigureAwait(false);

        lock (this.callSync)
        {
            this.readToEndTask = null;
        }

        return result;
    }
    #endregion
}
