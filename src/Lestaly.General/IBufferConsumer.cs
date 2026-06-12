using System.Buffers;

namespace Lestaly;

/// <summary>消費も可能なバッファライター</summary>
/// <typeparam name="T">要素型</typeparam>
public interface IBufferConsumer<T> : IBufferWriter<T>
{
    /// <summary>書き込み済みデータMemory</summary>
    ReadOnlyMemory<T> WrittenMemory { get; }

    /// <summary>書き込み済みデータSpan</summary>
    ReadOnlySpan<T> WrittenSpan { get; }

    /// <summary>バッファを内容を消費する</summary>
    /// <param name="count">消費サイズ</param>
    void Consume(int count);
}
