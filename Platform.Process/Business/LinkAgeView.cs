using System;

namespace Platform.Process.Business
{
    public class LinkageView
    {
        /// <summary>
        /// 酒店ID
        /// </summary>
        public Guid HotelId { get; set; }

        /// <summary>
        /// 酒店名称
        /// </summary>
        public string HotelName { get; set; }

        /// <summary>
        /// 净化器运行时间
        /// </summary>
        public long? CleannerRunningTimeTicks { get; set; }

        public TimeSpan CleannerRunningTime
            => CleannerRunningTimeTicks == null ? new TimeSpan(0) : new TimeSpan(CleannerRunningTimeTicks.Value);

        /// <summary>
        /// 风机运行时间
        /// </summary>
        public long? FanRunningTimeTicks { get; set; }

        public TimeSpan FanRunningTime
            => FanRunningTimeTicks == null ? new TimeSpan(0) : new TimeSpan(FanRunningTimeTicks.Value);

        /// <summary>
        /// 联动比
        /// </summary>
        public bool CompleteLinkaged => CleannerRunningTimeTicks == FanRunningTimeTicks;

        /// <summary>
        /// 数据更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
