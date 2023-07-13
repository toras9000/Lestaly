namespace Lestaly;

/// <summary>
/// 日時に関するの拡張メソッド
/// </summary>
public static class DateTimeExtensions
{
    /// <summary>同じ日の時刻が異なる日時を取得する。</summary>
    /// <param name="self">ベースとなる日時</param>
    /// <param name="hour">時間</param>
    /// <param name="minute">分</param>
    /// <param name="second">秒</param>
    /// <param name="millisecond">ミリ秒</param>
    /// <returns>時刻の異なる日時</returns>
    public static DateTime AtTime(this DateTime self, int hour, int minute, int second = 0, int millisecond = 0)
    {
        return new DateTime(self.Year, self.Month, self.Day, hour, minute, second, millisecond, self.Kind);
    }

    /// <summary>同じ日の時刻が異なる日時を取得する</summary>
    /// <param name="self">ベースとなる日時</param>
    /// <param name="time">時刻</param>
    /// <returns>時刻の異なる日時</returns>
    public static DateTime AtTime(this DateTime self, TimeOnly time)
    {
        return new DateTime(self.Year, self.Month, self.Day, time.Hour, time.Minute, time.Second, time.Millisecond, self.Kind);
    }
}
