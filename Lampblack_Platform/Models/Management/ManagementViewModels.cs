using MvcWebComponents.Model;
using PagedList;
using SHWDTech.Platform.Model.Model;

namespace Lampblack_Platform.Models.Management
{
    /// <summary>
    /// 餐饮企业列表模型
    /// </summary>
    public class CateringEnterpriseViewModel : PagedListViewModelBase
    {
        /// <summary>
        /// 餐饮企业列表
        /// </summary>
        public IPagedList<CateringCompany> CateringCompanies { get; set; }
    }

    /// <summary>
    /// 酒店（饭店）视图模型
    /// </summary>
    public class HotelViewModel : PagedListViewModelBase
    {
        /// <summary>
        /// 酒店（饭店）列表
        /// </summary>
        public IPagedList<HotelRestaurant> HtoHotelRestaurants { get; set; }
    }

    /// <summary>
    /// 酒店设备视图模型
    /// </summary>
    public class DeviceViewModel : PagedListViewModelBase
    {
        /// <summary>
        /// 酒店设备列表
        /// </summary>
        public IPagedList<RestaurantDevice> RestaurantDevices { get; set; }
    }
}