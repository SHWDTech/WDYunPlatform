using System.Collections.Generic;

namespace Lampblack_Platform.Models.Home
{
    /// <summary>
    /// 主页视图模型
    /// </summary>
    public class IndexViewModel
    {
        /// <summary>
        /// 酒店清洁度查询
        /// </summary>
        public List<HotelCleaness> HotelCleanessList { get; set; }
    }

    /// <summary>
    /// 酒店清洁度
    /// </summary>
    public class HotelCleaness
    {
        /// <summary>
        /// 清洁度级别
        /// </summary>
        public string CleanessRate { get; set; }

        /// <summary>
        /// 酒店名臣
        /// </summary>
        public string HotelName { get; set; }
    }
}