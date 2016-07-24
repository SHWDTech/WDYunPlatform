using System;

namespace Platform.Process.Business
{
    public class RunningTimeView
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
        /// 设备运行时间
        /// </summary>
        public string DeviceRunningTime { get; set; }

        /// <summary>
        /// 净化器运行时间
        /// </summary>
        public string CleannerRunningTime { get; set; }

        /// <summary>
        /// 风机运行时间
        /// </summary>
        public string FunRunningTime { get; set; }
    }
}
