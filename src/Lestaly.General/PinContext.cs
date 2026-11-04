using System.Runtime.InteropServices;

namespace Lestaly;

/// <summary>オブジェクトのピン止め管理</summary>
public sealed class PinContext : IDisposable
{
    // 構築
    #region コンストラクタ
    /// <summary>初期キャパシティを指定可能なコンストラクタ</summary>
    /// <param name="capacity">初期キャパシティ</param>
    public PinContext(int capacity = 4)
    {
        this.handles = new(capacity);
    }
    #endregion

    // 公開メソッド
    #region 追加
    /// <summary>ピン止めオブジェクト</summary>
    /// <param name="target">対象オブジェクト</param>
    /// <returns>ピン止めオブジェクトのポインタ</returns>
    public nint Add(object? target)
    {
        // 有効なオブジェクトでなければ管理追加しない
        if (target == null) return IntPtr.Zero;

        // ピン止めGCハンドルを作成
        var handle = GCHandle.Alloc(target, GCHandleType.Pinned);
        this.handles.Add(handle);

        // ピン止めポインタを返却
        return handle.AddrOfPinnedObject();
    }
    #endregion

    #region 破棄
    /// <summary>リソースを破棄する</summary>
    public void Dispose()
    {
        for (var i = 0; i < this.handles.Count; i++)
        {
            var handle = this.handles[i];
            if (handle.IsAllocated) handle.Free();
        }
        this.handles.Clear();
    }
    #endregion

    // 非公開フィールド
    #region 追加
    /// <summary>管理下のピン止めオブジェクトリスト</summary>
    private readonly List<GCHandle> handles;
    #endregion
}
