using System;

namespace WebViewModels.ViewDataModel
{
    /// <summary>
    /// 综合统计
    /// </summary>
    public class GeneralReport
    {
        /// <summary>
        /// 酒店ID
        /// </summary>
        public Guid HotelGuid { get; set; }

        /// <summary>
        /// 酒店名称
        /// </summary>
        public string HotelName { get; set; }

        /// <summary>
        /// 净化器总运行时间
        /// </summary>
        public long TotalCleanerRunTimeTicks { get; set; }

        /// <summary>
        /// 净化器总运行时间
        /// </summary>
        public TimeSpan TotalCleanerRunTime
            => new TimeSpan(TotalCleanerRunTimeTicks);

        /// <summary>
        /// 风机总运行时间
        /// </summary>
        public long TotalFanRunTimeTicks { get; set; }

        /// <summary>
        /// 风机总运行时间
        /// </summary>
        public TimeSpan TotalFanRunTime 
            => new TimeSpan(TotalFanRunTimeTicks);

        /// <summary>
        /// 联动比
        /// </summary>
        public double TotalLinkage
        {
            get
            {
                if (TotalCleanerRunTimeTicks == 0) return 0.0;
                return (TotalFanRunTimeTicks * 1.0 / TotalCleanerRunTimeTicks * 1.0) * 100;
            }
        }
    }
}
