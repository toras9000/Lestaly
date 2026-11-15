using System.Text;
using System.Text.RegularExpressions;

namespace LestalyTest.Extensions;

[TestClass()]
public class StringBuilderExtensionsTests
{
    [TestMethod()]
    public void AppendNormalized()
    {
        static void validateNormalize(string input, string result)
        {
            var builder = new StringBuilder();
            builder.AppendNormalized(input);
            builder.ToString().Should().Be(result);
        }

        validateNormalize("がぎぐげご", "がぎぐげご");
        validateNormalize("ｶﾞｷﾞｸﾞｹﾞｺﾞ １２３ ① ㈱", "ガギグゲゴ 123 1 (株)");
        validateNormalize(string.Concat(Enumerable.Repeat("がぎぐげご", 10)), string.Concat(Enumerable.Repeat("がぎぐげご", 10)));

        validateNormalize("", "");

        validateNormalize(new string('a', 31), new string('a', 31));
        validateNormalize(new string('a', 32), new string('a', 32));
        validateNormalize(string.Concat(Enumerable.Repeat("が", 20)), new string('が', 20));

        validateNormalize("🍣🍺🚀", "🍣🍺🚀");
        validateNormalize(string.Concat(Enumerable.Repeat("🍣", 15)), string.Concat(Enumerable.Repeat("🍣", 15)));
        validateNormalize(string.Concat(Enumerable.Repeat("🍣", 16)), string.Concat(Enumerable.Repeat("🍣", 16)));
        validateNormalize("👍🏽", "👍🏽");
        validateNormalize("𝐀𝐁𝐂 𝟏𝟐𝟑", "ABC 123");
    }

    [TestMethod()]
    public void AppendNormalized_Form()
    {
        static void validateNormalize(string input, string result, NormalizationForm form)
        {
            var builder = new StringBuilder();
            builder.AppendNormalized(input, form);
            builder.ToString().Should().Be(result);
        }

        validateNormalize("がぎぐげご", "がぎぐげご", NormalizationForm.FormKC);
        validateNormalize("がぎぐげご", "がぎぐげご", NormalizationForm.FormD);
        validateNormalize("がぎぐげご", "がぎぐげご", NormalizationForm.FormC);

        validateNormalize("葛󠄀飾", "葛󠄀飾", NormalizationForm.FormC);
        validateNormalize("葛󠄀飾", "葛󠄀飾", NormalizationForm.FormKC);

    }

    [TestMethod()]
    public void AppendNormalized_Exists()
    {
        var builder = new StringBuilder("Prefix:");
        builder.AppendNormalized("がぎぐげご");
        builder.Append(":Suffix");
        builder.ToString().Should().Be("Prefix:がぎぐげご:Suffix");
    }
}
