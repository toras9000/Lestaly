namespace Lestaly;

/// <summary>重み付き選択肢データ型</summary>
/// <typeparam name="TOption">値の型</typeparam>
/// <param name="Value">値</param>
/// <param name="Weight">重み</param>
public readonly record struct OptionWeight<TOption>(TOption Value, ushort Weight);

/// <summary>乱数範囲</summary>
public readonly struct RandomRange
{
    /// <summary>コンストラクタ</summary>
    /// <param name="start">開始値</param>
    /// <param name="count">範囲数</param>
    public RandomRange(int start, int count)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(count, 0);
        checked { _ = start + count; }
        this.Start = start;
        this.Count = count;
    }

    /// <summary>範囲の大きさ(個数)によってインスタンスを作成する</summary>
    /// <param name="start">開始値</param>
    /// <param name="count">範囲数</param>
    /// <returns>乱数範囲</returns>
    public RandomRange ByCount(int start, int count) => new(start, count);

    /// <summary>範囲の終端値によってインスタンスを作成する</summary>
    /// <param name="start">開始値</param>
    /// <param name="end">終端値</param>
    /// <returns>乱数範囲</returns>
    public RandomRange ByEnd(int start, int end)
    {
        if (end <= start) throw new ArgumentException("end must be grater than begin.");
        return new(start, end - start);
    }

    /// <summary>開始値</summary>
    public int Start { get; }

    /// <summary>範囲数</summary>
    public int Count { get; }

    /// <summary>範囲の最後の値</summary>
    public int Last => this.Start + this.Count - 1;

    /// <summary>範囲の終端()</summary>
    public int End => this.Start + this.Count;
}

/// <summary>重み付き選択肢データ型のヘルパ型</summary>
public static class OptionWeight
{
    /// <summary>重み付き選択肢データ型</summary>
    /// <typeparam name="TOption">値の型</typeparam>
    /// <param name="value">値</param>
    /// <param name="weight">重み</param>
    /// <returns>重み付き選択肢データ</returns>
    public static OptionWeight<TOption> Create<TOption>(TOption value, ushort weight) => new(value, weight);
}

/// <summary>Random に対する拡張メソッド</summary>
public static class RandomExtensions
{
    /// <summary>Random に対する拡張メソッド</summary>
    /// <param name="self">ランダム値を生成するインスタンス</param>
    extension(Random self)
    {
        /// <summary>ランダム値のバイト配列を作成する。</summary>
        /// <param name="length">生成するバイト列の長さ</param>
        /// <returns>生成したランダム値配列</returns>
        public byte[] GetBytes(int length)
        {
            var data = new byte[length];
            self.NextBytes(data);
            return data;
        }

        /// <summary>範囲内のランダムな整数を1つ取得する</summary>
        /// <param name="range">ランダム範囲</param>
        /// <returns>範囲内のランダムな整数</returns>
        public int GetNumber(RandomRange range)
            => Random.Shared.Next(range.Start, range.End);

        /// <summary>コレクションからランダムに1つの要素を取得する</summary>
        /// <typeparam name="TItem">要素の型</typeparam>
        /// <param name="items">対象コレクション</param>
        /// <returns>ランダムに選択された要素</returns>
        public TItem Pick<TItem>(IReadOnlyList<TItem> items)
        {
            ArgumentNullException.ThrowIfNull(items);
            var index = self.Next(items.Count);
            return items[index];
        }

        /// <summary>コレクションからランダムに重複無しで指定数の要素を取得する</summary>
        /// <typeparam name="TItem">要素の型</typeparam>
        /// <param name="items">対象コレクション</param>
        /// <param name="count">取得数。ただしコレクション長でキャップされる。</param>
        /// <returns>ランダムに取得された要素の配列</returns>
        public TItem[] PickItems<TItem>(IReadOnlyList<TItem> items, int count)
        {
            ArgumentNullException.ThrowIfNull(items);
            ArgumentOutOfRangeException.ThrowIfNegative(count);

            if (count == 0) return Array.Empty<TItem>();
            var pickCount = Math.Min(count, items.Count);
            var picked = new TItem[pickCount];
            var used = new SortedSet<int>();
            var restCount = items.Count;
            for (var i = 0; i < pickCount; i++)
            {
                var index = self.Next(restCount);
                foreach (var usedIdx in used)
                {
                    if (usedIdx <= index) index++;
                    else break;
                }
                picked[i] = items[index];
                used.Add(index);
                restCount--;
            }
            return picked;
        }

        /// <summary>コレクションからランダムに要素を取り除き取得する</summary>
        /// <typeparam name="TItem">要素の型</typeparam>
        /// <param name="items">対象コレクション</param>
        /// <param name="count">取得数。ただしコレクション長でキャップされる。</param>
        /// <returns>取り除いた要素の配列</returns>
        public TItem[] RemoveItems<TItem>(IList<TItem> items, int count)
        {
            ArgumentNullException.ThrowIfNull(items);
            ArgumentOutOfRangeException.ThrowIfNegative(count);

            if (count == 0) return Array.Empty<TItem>();
            var pickCount = Math.Min(count, items.Count);
            var picked = new TItem[pickCount];
            for (var i = 0; i < pickCount; i++)
            {
                var index = self.Next(items.Count);
                picked[i] = items[index];
                items.RemoveAt(index);
            }
            return picked;
        }

        /// <summary>シーケンスに一定の確率で要素を通過させるフィルタをかける</summary>
        /// <typeparam name="TItem">要素の型</typeparam>
        /// <param name="sequence">元のシーケンス</param>
        /// <param name="rate">通過率</param>
        /// <returns>フィルタをかけたシーケンス</returns>
        public IEnumerable<TItem> PassRate<TItem>(IEnumerable<TItem> sequence, double rate)
        {
            foreach (var item in sequence)
            {
                if (self.NextDouble() < rate)
                {
                    yield return item;
                }
            }
        }

        /// <summary>重みづけした選択肢の中からランダムで1つ値を選択する</summary>
        /// <typeparam name="TOption">選択肢データ型</typeparam>
        /// <param name="options">重みづけされた選択肢のスパン</param>
        /// <returns>ランダムに選択された値</returns>
        public TOption Choice<TOption>(params ReadOnlySpan<OptionWeight<TOption>> options)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(options.Length);

            var total = 0;
            for (var i = 0; i < options.Length; i++)
            {
                total = checked(total + options[i].Weight);
            }

            var amp = self.Next(total);
            for (var i = 0; i < options.Length; i++)
            {
                var item = options[i];
                if (amp < item.Weight) return item.Value;
                amp -= item.Weight;
            }

            return options[^1].Value;
        }
    }

    /// <summary>シーケンス に対する拡張メソッド</summary>
    /// <typeparam name="TItem">要素の型</typeparam>
    /// <param name="self">元のシーケンス</param>
    extension<TItem>(IEnumerable<TItem> self)
    {
        /// <summary>シーケンスに一定の確率で要素を通過させるフィルタをかける</summary>
        /// <param name="rate">通過率</param>
        /// <param name="random">ランダム値を生成するインスタンス。省略時は Random.Shared を利用する</param>
        /// <returns>フィルタをかけたシーケンス</returns>
        public IEnumerable<TItem> PassRate(double rate, Random? random = default)
            => (random ?? Random.Shared).PassRate(self, rate);
    }

}
