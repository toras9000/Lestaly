namespace Lestaly;

/// <summary>TimeSpan の拡張メンバ</summary>
public static class TimeSpanExtensions
{
    /// <summary>TimeSpan のインスタンス拡張メンバ</summary>
    extension(TimeSpan self)
    {
        /// <summary>シンプルな表現(hhhhhh:mm:ss)で文字列化する</summary>
        /// <returns>文字列表現</returns>
        public string ToSimple()
            => $"{(long)self.TotalHours}:{self.Minutes:D2}:{self.Seconds:D2}";
    }
}
