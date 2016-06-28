﻿using System.Collections.Generic;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process.IProcess
{
    /// <summary>
    /// 餐饮企业、饭店处理程序接口
    /// </summary>
    public interface ICateringEnterpriseProcess
    {
        /// <summary>
        /// 获取指定页的餐饮企业列表
        /// </summary>
        /// <param name="offset">跳过的数量</param>
        /// <param name="limit">选取的数量</param>
        /// <param name="queryName">查询名称</param>
        /// <param name="count">餐饮企业总数</param>
        /// <returns></returns>
        List<CateringCompany> GetPagedCateringCompanies(int offset, int limit, string queryName, out int count);

        /// <summary>
        /// 添加或更新餐饮企业
        /// </summary>
        /// <param name="model"></param>
        void AddOrUpdateCateringEnterprise(CateringCompany model);
    }
}
