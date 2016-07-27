using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using WebViewModels.Enums;
using WebViewModels.ViewDataModel;

namespace WebViewModels.ViewModel
{
    public class GeneralComparisonViewModel
    {
        /// <summary>
        /// 区域集合
        /// </summary>
        public List<SelectListItem> AreaListItems { get; set; }

        /// <summary>
        /// 街道集合
        /// </summary>
        public List<SelectListItem> StreetListItems { get; set; }

        /// <summary>
        /// 地址集合
        /// </summary>
        public List<SelectListItem> AddressListItems { get; set; }

        /// <summary>
        /// 查询名称
        /// </summary>
        public string QueryName { get; set; }

        /// <summary>
        /// 报表类型
        /// </summary>
        public ReportType ReportType { get; set; } = ReportType.Month;

        /// <summary>
        /// 截止日期
        /// </summary>
        [Display(Name = "截止日期：")]
        public DateTime DueDateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 区域ID
        /// </summary>
        public Guid AreaGuid { get; set; }

        /// <summary>
        /// 街道ID
        /// </summary>
        public Guid StreetGuid { get; set; }

        /// <summary>
        /// 详细地址ID
        /// </summary>
        public Guid AddressGuid { get; set; }

        /// <summary>
        /// 通用报表数据
        /// </summary>
        public List<GeneralCompasion> GeneralReports { get; set; } = new List<GeneralCompasion>();
    }
}
