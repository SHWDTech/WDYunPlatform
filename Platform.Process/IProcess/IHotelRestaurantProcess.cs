using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq.Expressions;
using PagedList;
using SHWDTech.Platform.Model.Model;
using WebViewModels.ViewDataModel;

namespace Platform.Process.IProcess
{
    public interface IHotelRestaurantProcess : IProcessBase
    {
        /// <summary>
        /// 获取分页后的酒店信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="queryName"></param>
        /// <param name="count"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        IPagedList<HotelRestaurant> GetPagedHotelRestaurant(int page, int pageSize, string queryName, out int count, List<Expression<Func<HotelRestaurant, bool>>> conditions = null);

        /// <summary>
        /// 获取指定ID的酒店信息
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        HotelRestaurant GetHotelRestaurant(Guid guid);

        /// <summary>
        /// 更新酒店（饭店）信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        DbEntityValidationException AddOrUpdateHotelRestaurant(HotelRestaurant model, List<string> propertyNames);

        /// <summary>
        /// 删除指定的酒店（饭店）
        /// </summary>
        /// <param name="componyId"></param>
        /// <returns></returns>
        bool DeleteHotelRestaurant(Guid componyId);

        /// <summary>
        /// 获取酒店下拉菜单列表
        /// </summary>
        /// <returns></returns>
        Dictionary<Guid, string> GetHotelRestaurantSelectList();

        /// <summary>
        /// 获取酒店清洁度情况列表
        /// </summary>
        /// <returns></returns>
        List<HotelCleaness> GetHotelCleanessList(Guid domainId);

        /// <summary>
        /// 获取地图上的酒店清洁度列表
        /// </summary>
        /// <param name="hotelGuid"></param>
        /// <returns></returns>
        object GetMapHotelCurrentStatus(Guid hotelGuid);

        /// <summary>
        /// 根据区域信息获取酒店列表
        /// </summary>
        /// <param name="districtGuid"></param>
        /// <returns></returns>
        List<HotelRestaurant> GetHotels(Guid districtGuid);

        /// <summary>
        /// 获取酒店位置信息
        /// </summary>
        /// <returns></returns>
        List<HotelLocations> GetHotelLocations(Guid domainId);

        /// <summary>
        /// 获取所有酒店的ID列表
        /// </summary>
        /// <returns></returns>
        List<Guid> GetAllHotelGuids();
    }
}
