﻿using System.Collections.Generic;
using SHWDTech.Platform.Model.Model;

namespace Lampblack_Platform.Models.Management
{
    /// <summary>
    /// 餐饮企业编辑模型
    /// </summary>
    public class EditCateringEnterpriseViewModel : CateringCompany
    {

    }

    /// <summary>
    /// 餐饮企业列表模型
    /// </summary>
    public class CateringEnterpriseViewModel
    {
        /// <summary>
        /// 餐饮企业列表
        /// </summary>
        public List<CateringCompany> CateringCompanies { get; set; }

        /// <summary>
        /// 记录总数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 每页数量
        /// </summary>
        public int Limit { get; set; }

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