using System;

namespace Platform.Process.Business
{
    /// <summary>
    /// 清洁度视图
    /// </summary>
    public class CleanRateView
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
        /// 失效总数
        /// </summary>
        public int Failed { get; set; }

        /// <summary>
        /// 较差总数
        /// </summary>
        public int Worse { get; set; }

        /// <summary>
        /// 合格总数
        /// </summary>
        public int Qualified { get; set; }

        /// <summary>
        /// 良好总数
        /// </summary>
        public int Good { get; set; }
    }
}
