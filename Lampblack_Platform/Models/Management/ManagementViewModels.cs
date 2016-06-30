using PagedList;
using SHWDTech.Platform.Model.Model;

namespace Lampblack_Platform.Models.Management
{
    /// <summary>
    /// 餐饮企业列表模型
    /// </summary>
    public class CateringEnterpriseViewModel
    {
        /// <summary>
        /// 餐饮企业列表
        /// </summary>
        public IPagedList<CateringCompany> CateringCompanies { get; set; }

        /// <summary>
        /// 记录总数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 每页数量
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 页数总数
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// 当前页数
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 查询名称
        /// </summary>
        public string QueryName { get; set; }
    }

    /// <summary>
    /// 酒店（饭店）视图模型
    /// </summary>
    public class HotelViewModel
    {
        /// <summary>
        /// 酒店（饭店）列表
        /// </summary>
        public IPagedList<HotelRestaurant> HtoHotelRestaurants { get; set; }

        /// <summary>
        /// 记录总数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 每页数量
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 页数总数
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// 当前页数
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 查询名称
        /// </summary>
        public string QueryName { get; set; }
    }
}