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
    /// 酒店当前状态视图模型
    /// </summary>
    public class IndexHotelCurrentViewModel
    {
        /// <summary>
        /// 当前电流值
        /// </summary>
        public double Current { get; set; }

        /// <summary>
        /// 净化器状态
        /// </summary>
        public string CleanerStatus { get; set; }

        /// <summary>
        /// 风机状态
        /// </summary>
        public string FanStatus { get; set; }

        /// <summary>
        /// 进烟浓度
        /// </summary>
        public double LampblackIn { get; set; }

        /// <summary>
        /// 出烟浓度
        /// </summary>
        public double LampblackOut { get; set; }

        /// <summary>
        /// 净化器运行时间
        /// </summary>
        public string CleanerRunTime { get; set; }

        /// <summary>
        /// 风机运行时间
        /// </summary>
        public string FanRunTime { get; set; }

        public IndexHotelCurrentViewModel(Dictionary<string, object> source)
        {
            Current = (double) source["Current"];
            CleanerStatus = (bool) source["CleanerStatus"] ? "开启" : "关闭";
            FanStatus = (bool) source["FanStatus"] ? "开启" : "关闭";
            LampblackIn = (double) source["LampblackIn"];
            LampblackOut = (double) source["LampblackOut"];
            CleanerRunTime = source["CleanerRunTime"].ToString();
            FanRunTime = source["FanRunTime"].ToString();
        }
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