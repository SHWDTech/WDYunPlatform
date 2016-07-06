using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq.Expressions;
using PagedList;
using SHWDTech.Platform.Model.Model;

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
        /// <param name="condition"></param>
        /// <returns></returns>
        IPagedList<HotelRestaurant> GetPagedHotelRestaurant(int page, int pageSize, string queryName, out int count, Expression<Func<HotelRestaurant, bool>> condition = null);

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
    }
}
