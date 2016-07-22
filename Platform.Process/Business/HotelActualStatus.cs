using System;
using System.Collections.Generic;

namespace Platform.Process.Business
{
    public class HotelActualStatus
    {
        /// <summary>
        /// 酒店名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 酒店ID
        /// </summary>
        public Guid HotelGuid { get; set; }

        /// <summary>
        /// 各通道状态
        /// </summary>
        public List<ChannelStatus> ChannelStatus { get; set; }
    }
}
