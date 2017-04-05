using System;
using System.ComponentModel.DataAnnotations;
using MvcWebComponents.Model;
using PagedList;
using Platform.Process.Business;

namespace Lampblack_Platform.Models.Query
{
    /// <summary>
    /// 清洁度查询模型视图
    /// </summary>
    public class CleanRateViewModel : PagedListViewModelBase
    {
        /// <summary>
        /// 查询开始时间
        /// </summary>
        [Display(Name = "查询开始时间")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDateTime { get; set; }

        /// <summary>
        /// 查询结束时间
        /// </summary>
        [Display(Name = "查询结束时间")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDateTime { get; set; }

        /// <summary>
        /// 清洁度视图
        /// </summary>
        public IPagedList<CleanRateView> CleanRateView { get; set; }
    }

    public class RunningTimeViewModel : PagedListViewModelBase
    {
        /// <summary>
        /// 查询开始时间
        /// </summary>
        [Display(Name = "查询开始时间")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDateTime { get; set; }

        /// <summary>
        /// 查询结束时间
        /// </summary>
        [Display(Name = "查询结束时间")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDateTime { get; set; }

        /// <summary>
        /// 清洁度视图
        /// </summary>
        public IPagedList<RunningTimeView> RunningTimeView { get; set; }
    }

    public class LinkageViewModel : PagedListViewModelBase
    {
        /// <summary>
        /// 查询开始时间
        /// </summary>
        [Display(Name = "查询开始时间")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDateTime { get; set; }

        /// <summary>
        /// 查询结束时间
        /// </summary>
        [Display(Name = "查询结束时间")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDateTime { get; set; }

        /// <summary>
        /// 清洁度视图
        /// </summary>
        public IPagedList<LinkageView> LinkageView { get; set; }
    }

    public class AlarmViewModel : PagedListViewModelBase
    {
        /// <summary>
        /// 查询开始时间
        /// </summary>
        [Display(Name = "查询开始时间")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDateTime { get; set; }

        /// <summary>
        /// 查询结束时间
        /// </summary>
        [Display(Name = "查询结束时间")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDateTime { get; set; }

        /// <summary>
        /// 清洁度视图
        /// </summary>
        public IPagedList<AlarmView> AlarmView { get; set; }
    }

    public class HistoryDataViewModel : PagedListViewModelBase
    {
        /// <summary>
        /// 查询开始时间
        /// </summary>
        [Display(Name = "查询开始时间")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDateTime { get; set; }

        /// <summary>
        /// 查询结束时间
        /// </summary>
        [Display(Name = "查询结束时间")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDateTime { get; set; }

        /// <summary>
        /// 清洁度视图
        /// </summary>
        public IPagedList<HistoryData> HistoryData { get; set; }
    }

    public class HistoryDataTableRows
    {
        public string DistrictName { get; set; }

        public string HotelName { get; set; }

        public string DeviceName { get; set; }

        public int Channel { get; set; } = 1;

        public bool CleanerSwitch { get; set; }

        public double CleanerCurrent { get; set; }

        public bool FanSwitch { get; set; }

        public double FanCurrent { get; set; }

        public string DateTime { get; set; }
    }
}