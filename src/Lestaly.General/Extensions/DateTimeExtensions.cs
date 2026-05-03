namespace Lestaly;

/// <summary>日時に関する拡張メソッド</summary>
public static class DateTimeExtensions
{
    /// <summary>日時に関する拡張メソッド</summary>
    /// <param name="self"></param>
    extension(DateTime self)
    {
        /// <summary>同じ日の時刻が異なる日時を取得する。</summary>
        /// <param name="hour">時間</param>
        /// <param name="minute">分</param>
        /// <param name="second">秒</param>
        /// <param name="millisecond">ミリ秒</param>
        /// <returns>時刻の異なる日時</returns>
        public DateTime AtTime(int hour, int minute, int second = 0, int millisecond = 0)
        {
            return new DateTime(self.Year, self.Month, self.Day, hour, minute, second, millisecond, self.Kind);
        }

        /// <summary>同じ日の時刻が異なる日時を取得する</summary>
        /// <param name="time">時刻</param>
        /// <returns>時刻の異なる日時</returns>
        public DateTime AtTime(TimeOnly time)
        {
            return new DateTime(self.Year, self.Month, self.Day, time.Hour, time.Minute, time.Second, time.Millisecond, self.Kind);
        }

        /// <summary>日時から日付のみオブジェクトを取得する</summary>
        /// <returns>日付のみオブジェクト</returns>
        public DateOnly ToDateOnly()
        {
            return DateOnly.FromDateTime(self);
        }

        /// <summary>日時から時刻のみオブジェクトを取得する</summary>
        /// <returns>時刻のみオブジェクト</returns>
        public TimeOnly ToTimeOnly()
        {
            return TimeOnly.FromDateTime(self);
        }
    }
}
