using System;

namespace SHWDTech.Platform.Utility.ExtensionMethod
{
    public static class DateTimeExtensionMethod
    {
        /// <summary>
        /// 删除指定的Ticks
        /// </summary>
        /// <param name="date"></param>
        /// <param name="roundTicks"></param>
        /// <returns></returns>
        public static DateTime Trim(this DateTime date, long roundTicks) 
            => new DateTime(date.Ticks - date.Ticks % roundTicks);

        /// <summary>
        /// 获取上个小时正点时间
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetPreviousHour(this DateTime date)
            => GetCurrentHour(date).AddHours(-1);

        /// <summary>
        /// 获取当前正点小时时间
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetCurrentHour(this DateTime date)
            => date.Trim(TimeSpan.TicksPerSecond).Trim(TimeSpan.TicksPerMinute).Trim(TimeSpan.TicksPerHour);

        /// <summary>
        /// 获取下个小时正点时间
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetNextHour(this DateTime date)
            => GetCurrentHour(date).AddHours(1);

        /// <summary>
        /// 获取昨天零点时间
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetPrevioudDay(this DateTime date)
            => GetToday(date).AddDays(-1);

        /// <summary>
        /// 获取今天零时时间
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetToday(this DateTime date)
            => GetCurrentHour(date).Trim(TimeSpan.TicksPerDay);

        /// <summary>
        /// 获取明天零时时间
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetTomorrow(this DateTime date)
            => GetToday(date).AddDays(1);


        public static DateTime GetCurrentMonth(this DateTime date)
            => GetToday(date).Trim(TimeSpan.TicksPerDay * DateTime.DaysInMonth(date.Year, date.Month));

        public static int MonthDifference(this DateTime lValue, DateTime rValue) 
            => Math.Abs((lValue.Month - rValue.Month) + 12 * (lValue.Year - rValue.Year));
    }
}
