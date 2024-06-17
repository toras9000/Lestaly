namespace LestalyTest;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060")]
public static class MemoryTestExtensions
{
    public static string OverloadTest<T>(this Span<T> self) => "Span";
    public static string OverloadTest<T>(this ReadOnlySpan<T> self) => "ReadOnlySpan";
    public static string OverloadTest<T>(this Memory<T> self) => "Memory";
    public static string OverloadTest<T>(this ReadOnlyMemory<T> self) => "ReadOnlyMemory";
}
