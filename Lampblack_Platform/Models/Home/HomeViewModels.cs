using System.Collections.Generic;
using MvcWebComponents.Model;

namespace Lampblack_Platform.Models.Home
{
    /// <summary>
    /// 主页视图模型
    /// </summary>
    public class IndexViewModel : ViewModelBase
    {
        /// <summary>
        /// 酒店清洁度查询
        /// </summary>
        public List<HotelCleaness> HotelCleanessList { get; set; } = new List<HotelCleaness>();

        /// <summary>
        /// 无数据酒店数量
        /// </summary>
        public int NoData { get; set; }

        /// <summary>
        /// 不合格酒店数量
        /// </summary>
        public int Faild { get; set; }

        /// <summary>
        /// 较差酒店数量
        /// </summary>
        public int Worse { get; set; }

        /// <summary>
        /// 合格酒店数量
        /// </summary>
        public int Qualified { get; set; }

        /// <summary>
        /// 良好酒店数量
        /// </summary>
        public int Good { get; set; }
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