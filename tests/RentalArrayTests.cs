namespace LestalyTest;

[TestClass()]
public class RentalArrayTests
{
    [TestMethod()]
    public void RentReturn()
    {
        // 1回貸し出しを受けて配列の参照を取っておき、返却を行う。
        var first = default(byte[]);
        using (var array = new RentalArray<byte>(100))
        {
            first = array.Instance;
        }

        // 同じサイズで何度か再貸出しを受けて、同じインスタンスが得られる(つまり上の処理で返却されている)ことを確認する。
        var rerent = false;
        for (var i = 0; i < 100; i++)
        {
            using var array = new RentalArray<byte>(100);
            if (object.ReferenceEquals(array.Instance, first))
            {
                rerent = true;
                break;
            }
        }

        // ちゃんと返却・再貸出しされたことを確認
        rerent.Should().BeTrue();
    }
}
