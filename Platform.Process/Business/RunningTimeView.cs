using System;

namespace Platform.Process.Business
{
    public class RunningTimeView
    {
        /// <summary>
        /// 酒店ID
        /// </summary>
        public long HotelId { get; set; }

        /// <summary>
        /// 酒店名称
        /// </summary>
        public string HotelName { get; set; }

        /// <summary>
        /// 设备运行时间
        /// </summary>
        public long? DeviceRunningTimeTicks { get; set; }

        public TimeSpan DeviceRunningTime 
            => DeviceRunningTimeTicks == null ? new TimeSpan(0) : new TimeSpan(DeviceRunningTimeTicks.Value);

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
        /// 数据更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
