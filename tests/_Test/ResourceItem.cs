namespace LestalyTest._Test;

internal class ResourceItem(Action cleanup) : IDisposable
{
    public void Dispose()
    {
        cleanup?.Invoke();
        cleanup = null!;
    }
}
