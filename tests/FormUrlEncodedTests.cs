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
        {
            var value = new DerivedItem("abc", 123);
            var content = FormUrlEncoded.CreateContent(value);
            var contentString = await content.ReadAsStringAsync();
            contentString.Should().Contain("Text=abc").And.Contain("Value=123");
        }
        {
            var value = new { text = "xyz", num = 987, array = new[] { 3, 4, 5, }, };
            var content = FormUrlEncoded.CreateContent(value);
            var contentString = await content.ReadAsStringAsync();
            contentString.Should().Contain("text=xyz")
                .And.Contain("num=987")
                .And.Contain("array%5B%5D=3")
                .And.Contain("array%5B%5D=4")
                .And.Contain("array%5B%5D=5");
        }
    }
}
