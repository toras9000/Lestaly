using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LestalyTest;

[TestClass]
public class FormUrlEncodedTests
{
    public record BaseItem(string Text);
    public record DerivedItem(string Text, int Value) : BaseItem(Text);

    [TestMethod()]
    public async Task CreateContent()
    {
        var value = new DerivedItem("abc", 123);
        var content = FormUrlEncoded.CreateContent(value);
        var contentString = await content.ReadAsStringAsync();
        contentString.Should().Contain("Text=abc").And.Contain("Value=123");
    }
}
