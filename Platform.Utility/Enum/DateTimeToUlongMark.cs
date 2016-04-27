namespace SHWDTech.Platform.Utility.Enum
{
    /// <summary>
    /// 时间和无符号长整形互相转换的标志位
    /// </summary>
    public static class DateTimeToUlongMark
    {
        /// <summary>
        /// 年
        /// </summary>
        public const byte YearMark = 36;

        /// <summary>
        /// 月
        /// </summary>
        public const byte MonthMark = 32;

        /// <summary>
        /// 日
        /// </summary>
        public const byte DayMark = 27;

        /// <summary>
        /// 时
        /// </summary>
        public const byte HourMark = 22;

        /// <summary>
        /// 分
        /// </summary>
        public const byte MinuteMark = 16;

        /// <summary>
        /// 秒
        /// </summary>
        public const byte SecondMark = 10;

        /// <summary>
        /// 毫秒
        /// </summary>
        public const byte MillisecondMark = 0;
    }

    /// <summary>
    /// 无符号长整形转换为时间时的标志位
    /// </summary>
    public class UlongToDateTimeFlag
    {
        /// <summary>
        /// 年
        /// </summary>
        public const ulong YearFlag = 0x0000FFF000000000;

        /// <summary>
        /// 月
        /// </summary>
        public const ulong MonthFlag = 0x0000000F00000000;

        /// <summary>
        /// 日
        /// </summary>
        public const ulong DayFlag = 0x00000000F8000000;

        /// <summary>
        /// 时
        /// </summary>
        public const ulong HourFlag = 0x0000000007C00000;

        /// <summary>
        /// 分
        /// </summary>
        public const ulong MinuteFlag = 0x00000000003F0000;

        /// <summary>
        /// 秒
        /// </summary>
        public const ulong SecondFlag = 0x000000000000FC00;

        /// <summary>
        /// 毫秒
        /// </summary>
        public const ulong MillisecondFlag = 0x00000000000003FF;
    }
}
