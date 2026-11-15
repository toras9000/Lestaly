namespace LestalyTest;

[TestClass]
public class FuzzyStringTests
{
    [TestMethod]
    public void Prune()
    {
        FuzzyString.Prune(" aaa bbb  ccc  ").Should().Be("aaabbbccc");
        FuzzyString.Prune("\raaa\r\nbbb\n\rccc\n").Should().Be("aaabbbccc");
        FuzzyString.Prune("aaa\tbbb\0ccc").Should().Be("aaabbbccc");

        FuzzyString.Prune(" がぎぐげご ｶﾞｷﾞｸﾞｹﾞｺﾞ １２３ ① ㈱  ").Should().Be("がぎぐげごガギグゲゴ1231(株)");
    }

    [TestMethod]
    public void Equivalent()
    {
        FuzzyString.Equivalent(" aaa bbb  ccc  ", "aaabbbccc").Should().Be(true);
        FuzzyString.Equivalent("\raaa\r\nbbb\n\rccc\n", "aaabbbccc").Should().Be(true);
        FuzzyString.Equivalent("aaa\tbbb\0ccc", "aaabbbccc").Should().Be(true);

        FuzzyString.Equivalent(" がぎぐげご ｶﾞｷﾞｸﾞｹﾞｺﾞ １２３ ① ㈱  ", "がぎぐげごガギグゲゴ1231(株)").Should().Be(true);
    }
}
