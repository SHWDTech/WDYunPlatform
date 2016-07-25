using System;

namespace Platform.Process.Business
{
    public class AlarmView
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
        /// 报警总次数
        /// </summary>
        public int AlarmCount { get; set; }

        /// <summary>
        /// 数据更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
