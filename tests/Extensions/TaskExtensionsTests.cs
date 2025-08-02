namespace LestalyTest.Extensions;

[TestClass]
public class TaskExtensionsTests
{
    [TestMethod]
    public async Task TrimAsync()
    {
        (await Task.FromResult("  abc  ").AsNullable().TrimAsync()).Should().Be("abc");
        (await ValueTask.FromResult("  abc  ").AsNullable().TrimAsync()).Should().Be("abc");
    }
}
