using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using WebViewModels.Enums;

namespace WebViewModels.ViewModel
{
    public class TrendAnalisysViewModel
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
        /// 查询名称
        /// </summary>
        public string QueryName { get; set; }

        /// <summary>
        /// 截止日期
        /// </summary>
        [Display(Name = "开始日期：")]
        public DateTime StartDateTime { get; set; } = DateTime.Now.AddMonths(-1);

        /// <summary>
        /// 截止日期
        /// </summary>
        [Display(Name = "截止日期：")]
        public DateTime DueDateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 报表类型
        /// </summary>
        public ReportType ReportType { get; set; } = ReportType.Month;

        public ReportSeason StartSeason { get; set; } = ReportSeason.FirstSeason;

        public ReportSeason EndSeason { get; set; } = ReportSeason.FourthSeason;

        public ReportHalfYear StartHalfYear { get; set; } = ReportHalfYear.FirstHalfYear;

        public ReportHalfYear EndHalfYear { get; set; } = ReportHalfYear.SecondHalfYear;
    }
}
