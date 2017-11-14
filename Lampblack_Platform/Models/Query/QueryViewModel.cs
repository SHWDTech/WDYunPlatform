using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using MvcWebComponents.Model;
using PagedList;
using Platform.Process.Business;

namespace Lampblack_Platform.Models.Query
{
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

        public double Density { get; set; }

        public string DateTime { get; set; }
    }

    public class HistoryChartOption
    {
        public Guid Hotel { get; set; }

        public int DataType { get; set; }

        public DateTime EndDate { get; set; }
    }

    public class HistoryQueryExportModel
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Guid Hotel { get; set; }
    }

    public class WorkSheet
    {
        public WorkSheet()
        {
            WorkSheetDatas.Columns.Add("设备名称", typeof(string));
            WorkSheetDatas.Columns.Add("通道号", typeof(int));
            WorkSheetDatas.Columns.Add("净化器开关", typeof(string));
            WorkSheetDatas.Columns.Add("净化器电流", typeof(double));
            WorkSheetDatas.Columns.Add("风机开关", typeof(string));
            WorkSheetDatas.Columns.Add("风机电流", typeof(double));
            WorkSheetDatas.Columns.Add("油烟浓度", typeof(double));
            WorkSheetDatas.Columns.Add("数据时间", typeof(string));
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 工作表数据
        /// </summary>
        public DataTable WorkSheetDatas { get; set; } = new DataTable();
    }
}