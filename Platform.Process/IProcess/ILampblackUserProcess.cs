﻿using System;
using System.Collections.Generic;
using PagedList;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process.IProcess
{
    /// <summary>
    /// 油烟用户处理程序接口
    /// </summary>
    public interface ILampblackUserProcess
    {
        /// <summary>
        /// 获取用户分页数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="queryName"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        IPagedList<LampblackUser> GetPagedLampblackUsers(int page, int pageSize, string queryName, out int count);

        /// <summary>
        /// 更新油烟系统用户
        /// </summary>
        /// <param name="model"></param>
        /// <param name="propertyNames"></param>
        /// <param name="roleList"></param>
        /// <returns></returns>
        Exception AddOrUpdateLampblackUser(LampblackUser model, List<string> propertyNames, List<string> roleList);

        /// <summary>
        /// 删除油烟系统用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool DeleteLampblackUser(Guid userId);

        /// <summary>
        /// 获取指定ID的油烟系统用户
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        LampblackUser GetLampblackUser(Guid guid);
    }
}
