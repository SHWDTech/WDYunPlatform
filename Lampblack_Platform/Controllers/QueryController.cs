using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Lampblack_Platform.Models.BootstrapTable;
using Lampblack_Platform.Models.Query;
using MvcWebComponents.Attributes;
using MvcWebComponents.Controllers;
using MvcWebComponents.Filters;
using Platform.Process.Process;
using SHWDTech.Platform.Model.Enums;
using WebViewModels.Enums;
using System.Data.Entity;
using System.Drawing;
using Lampblack_Platform.Common;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Lampblack_Platform.Controllers
{
    [AjaxGet]
    public class QueryController : WdControllerBase
    {
        // GET: Query
        public ActionResult CleanRate() => View();

        [NamedAuth(Modules = "CleanRate", Required = true)]
        public ActionResult CleanRateTable(CleanRateDataTable post)
        {
            var query = ProcessInvoke<RestaurantDeviceProcess>().GetRestaurantDeviceByArea(post.Area, post.Street, post.Address, WdContext.UserDistricts);
            if (!string.IsNullOrWhiteSpace(post.Name))
            {
                query = query.Where(d => d.Project.ProjectName.Contains(post.Name));
            }
            var total = query.Count();
            var devs = query.Include("Hotel").Include("LampblackDeviceModel").OrderBy(d => new
            {
                d.ProjectId,
                d.Identity
            }).Skip(post.offset)
                .Take(post.limit).ToList();
            var merge = devs.GroupBy(d => d.Hotel.Id).Where(e => e.Count() > 1)
                .Select(v => new
                {
                    index = devs.IndexOf(devs.First(d => d.Hotel.Id == v.Key)),
                    count = v.Count()
                }).ToList();

            var rows = ProcessInvoke<HotelRestaurantProcess>().GetCleanRateTables(devs, post.StartDate, post.EndDate);


            return JsonTable(new
            {
                total,
                rows,
                merge
            });
        }

        public ActionResult LinkageRate() => View();

        [NamedAuth(Modules = "LinkageRate", Required = true)]
        public ActionResult LinkageRateTable(LinkageRateTable post)
        {
            var query = ProcessInvoke<RestaurantDeviceProcess>().GetRestaurantDeviceByArea(post.Area, post.Street, post.Address, WdContext.UserDistricts);
            if (!string.IsNullOrWhiteSpace(post.Name))
            {
                query = query.Where(d => d.Project.ProjectName.Contains(post.Name));
            }
            var total = query.Count();
            var devs = query.Include("Hotel").OrderBy(d => new
            {
                d.ProjectId,
                d.Identity
            }).Skip(post.offset)
                .Take(post.limit).ToList();
            var merge = devs.GroupBy(d => d.Hotel.Id).Where(e => e.Count() > 1)
                .Select(v => new
                {
                    index = devs.IndexOf(devs.First(d => d.Hotel.Id == v.Key)),
                    count = v.Count()
                }).ToList();

            var rows = ProcessInvoke<RunningTimeProcess>().GetLinkageRateTables(devs, post.QueryDateTime);

            return JsonTable(new
            {
                total,
                rows,
                merge
            });
        }

        public ActionResult RemovalRate() => View();

        public ActionResult Alarm() => View();

        public ActionResult AlarmDataTable(BootstrapTablePostParams post)
        {
            var rows = ProcessInvoke<AlarmProcess>().GetAlarmTableData(post.offset, post.limit, out int total);

            return Json(new
            {
                total,
                rows
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult HistoryData() => View();

        [NamedAuth(Modules = "HistoryData", Required = true)]
        public ActionResult HistoryDataTable(HistoryDataTable post)
        {
            if (post.Hotel == Guid.Empty) return null;
            var hotel = ProcessInvoke<HotelRestaurantProcess>().GetHotelById(post.Hotel);
            var query = ProcessInvoke<LampblackRecordProcess>().GetRecordRepo();
            post.EndDate = post.EndDate.AddDays(1);
            query = query.Where(obj => obj.ProjectIdentity == hotel.Identity && obj.RecordDateTime > post.StartDate && obj.RecordDateTime < post.EndDate);
            var total = query.Count();
            if (total == 0) return null;
            var records = query.OrderByDescending(o => o.RecordDateTime).Skip(post.offset).Take(post.limit).ToList();
            var devs = ProcessInvoke<RestaurantDeviceProcess>().AllDevices().Where(d => d.ProjectId == post.Hotel).ToList();
            var districtName = ProcessBase.GetDistrictName(hotel.DistrictId);
            var rows = WdContext.Domain.Id == Guid.Parse("C11B87A8-F4D7-4850-8000-C850953B2496") 
                ? (from record in records
                let dev = devs.First(d => d.Identity == record.DeviceIdentity)
                select new HistoryDataTableRows
                {
                    DistrictName = districtName,
                    HotelName = $"{dev.Hotel.RaletedCompany.CompanyName}({dev.Hotel.ProjectName})",
                    DeviceName = dev.DeviceName,
                    Channel = record.Channel,
                    CleanerSwitch = record.CleanerSwitch,
                    CleanerCurrent = record.CleanerCurrent,
                    FanSwitch = record.FanSwitch,
                    FanCurrent = record.FanCurrent,
                    Density = GetDensity(record.CleanerCurrent),
                    DateTime = $"{record.RecordDateTime:yyyy-MM-dd HH:mm:ss}"
                }).ToList()
                : (from record in records
                let dev = devs.First(d => d.Identity == record.DeviceIdentity)
                select new HistoryDataTableRows
                {
                    DistrictName = districtName,
                    HotelName = $"{dev.Hotel.RaletedCompany.CompanyName}({dev.Hotel.ProjectName})",
                    DeviceName = dev.DeviceName,
                    CleanerSwitch = record.CleanerSwitch,
                    CleanerCurrent = record.CleanerCurrent,
                    FanSwitch = record.FanSwitch,
                    FanCurrent = record.FanCurrent,
                    DateTime = $"{record.RecordDateTime:yyyy-MM-dd HH:mm:ss}"
                }).ToList();

            return JsonTable(new
            {
                total,
                rows
            });
        }

        private static double GetDensity(double currrent)
        {
            var calc = Math.Round((currrent - 4.0) / 1.6, 1);
            if (calc < 0) return 0;
            return Math.Round(calc, 2);
        }

        public ActionResult RunningTime() => View();

        [NamedAuth(Modules = "RunningTime", Required = true)]
        public ActionResult RunningTimeTable(RunngingDataTable post)
        {
            var query = ProcessInvoke<RestaurantDeviceProcess>().GetRestaurantDeviceByArea(post.Area, post.Street, post.Address, WdContext.UserDistricts);
            if (!string.IsNullOrWhiteSpace(post.Name))
            {
                query = query.Where(d => d.Project.ProjectName.Contains(post.Name));
            }
            var total = query.Count();
            var devs = query.Include("Hotel").OrderBy(d => new
            {
                d.ProjectId,
                d.Identity
            }).Skip(post.offset)
                .Take(post.limit).ToList();
            var merge = devs.GroupBy(d => d.Hotel.Id).Where(e => e.Count() > 1)
                .Select(v => new
                {
                    index = devs.IndexOf(devs.First(d => d.Hotel.Id == v.Key)),
                    count = v.Count()
                }).ToList();

            var rows = ProcessInvoke<RunningTimeProcess>().GetRunningTimeTables(devs, post.StartDate, post.EndDate);

            return JsonTable(new
            {
                total,
                rows,
                merge
            });
        }

        [NamedAuth(Modules = "RunningTime", Required = true)]
        public ActionResult HistoryLineChart(HistoryChartOption option)
        {
            if (option.Hotel == Guid.Empty) return null;
            if (option.DataType == 0)
            {
                return HourLineChart(option);
            }

            if (option.DataType == 1)
            {
                return DayLineChart(option);
            }

            if (option.DataType == 2)
            {
                return MonthLineChart(option);
            }

            return null;
        }

        public ActionResult HourLineChart(HistoryChartOption option)
        {
            var hotel = ProcessInvoke<HotelRestaurantProcess>().GetHotelById(option.Hotel);
            var dev = ProcessInvoke<RestaurantDeviceProcess>().GetDevicesByRestaurant(option.Hotel).First();
            var query = ProcessInvoke<LampblackRecordProcess>().GetRecordRepo();
            var startDate = option.EndDate.AddHours(-1);
            query = query.Where(q => q.ProjectIdentity == hotel.Identity 
            && q.DeviceIdentity == dev.Identity 
            && q.Channel == 1 
            && q.RecordDateTime < option.EndDate 
            && q.RecordDateTime > startDate);

            var ret = query.Select(q => new
            {
                value = Math.Round((q.CleanerCurrent - 4.0) / 1.6, 1),
                UpdateTime = q.RecordDateTime
            }).ToList();

            return Json(new
            {
                Values = ret.Select(q => q.value).ToList(),
                UpdateTimes = ret.Select(q => q.UpdateTime.ToString("HH:mm:ss")).ToList()
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DayLineChart(HistoryChartOption option)
        {
            var hotel = ProcessInvoke<HotelRestaurantProcess>().GetHotelById(option.Hotel);
            var dev = ProcessInvoke<RestaurantDeviceProcess>().GetDevicesByRestaurant(option.Hotel).First();
            var query = ProcessInvoke<DataStatisticsProcess>().GetDataStaitsticsRepo();
            var startDate = option.EndDate.AddDays(-1);
            query = query.Where(q => q.Type == StatisticsType.Hour
                                     && q.ProjectIdentity == hotel.Identity
                                     && q.DeviceIdentity == dev.Identity
                                     && q.DataChannel == 0
                                     && q.UpdateTime > startDate
                                     && q.UpdateTime < option.EndDate
                                     && q.CommandDataId == CommandDataId.CleanerCurrent);

            var ret = query.Select(q => new
            {
                value = Math.Round((q.DoubleValue.Value - 4.0) / 1.6, 1),
                q.UpdateTime
            }).ToList();

            return Json(new
            {
                Values = ret.Select(q => q.value).ToList(),
                UpdateTimes = ret.Select(q => q.UpdateTime.ToString("yyyy-MM-dd HH:mm")).ToList()
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MonthLineChart(HistoryChartOption option)
        {
            var hotel = ProcessInvoke<HotelRestaurantProcess>().GetHotelById(option.Hotel);
            var dev = ProcessInvoke<RestaurantDeviceProcess>().GetDevicesByRestaurant(option.Hotel).First();
            var query = ProcessInvoke<DataStatisticsProcess>().GetDataStaitsticsRepo();
            var startDate = option.EndDate.AddMonths(-1);
            query = query.Where(q => q.Type == StatisticsType.Day
                                     && q.ProjectIdentity == hotel.Identity
                                     && q.DeviceIdentity == dev.Identity
                                     && q.DataChannel == 0
                                     && q.UpdateTime > startDate
                                     && q.UpdateTime < option.EndDate
                                     && q.CommandDataId == CommandDataId.CleanerCurrent);

            var ret = query.Select(q => new
            {
                value = Math.Round((q.DoubleValue.Value - 4.0) / 1.6, 1),
                q.UpdateTime
            }).ToList();

            return Json(new
            {
                Values = ret.Select(q => q.value).ToList(),
                UpdateTimes = ret.Select(q => q.UpdateTime.ToString("yyyy-MM-dd")).ToList()
            }, JsonRequestBehavior.AllowGet);
        }

        [NamedAuth(Modules = "HistoryData", Required = true)]
        [NotAjaxGetAttribute]
        public ActionResult HistoryQeryExport(HistoryQueryExportModel model)
        {
            var dataSource = new List<WorkSheet>();
            var hotel = ProcessInvoke<HotelRestaurantProcess>().GetHotelById(model.Hotel);
            var query = ProcessInvoke<LampblackRecordProcess>().GetRecordRepo();
            model.EndDate = model.EndDate.AddDays(1);
            var records = query.Where(obj => obj.ProjectIdentity == hotel.Identity && obj.RecordDateTime > model.StartDate && obj.RecordDateTime < model.EndDate)
                .ToList();
            var districtName = ProcessBase.GetDistrictName(hotel.DistrictId);
            var dev = ProcessInvoke<RestaurantDeviceProcess>().AllDevices().First(d => d.ProjectId == model.Hotel);
            var rows = WdContext.Domain.Id == Guid.Parse("C11B87A8-F4D7-4850-8000-C850953B2496")
                ? (from record in records
                   select new HistoryDataTableRows
                    {
                        DistrictName = districtName,
                        HotelName = $"{dev.Hotel.RaletedCompany.CompanyName}({dev.Hotel.ProjectName})",
                        DeviceName = dev.DeviceName,
                        Channel = record.Channel,
                        CleanerSwitch = record.CleanerSwitch,
                        CleanerCurrent = record.CleanerCurrent,
                        FanSwitch = record.FanSwitch,
                        FanCurrent = record.FanCurrent,
                        Density = GetDensity(record.CleanerCurrent),
                        DateTime = $"{record.RecordDateTime:yyyy-MM-dd HH:mm:ss}"
                    }).ToList()
                : (from record in records
                   select new HistoryDataTableRows
                    {
                        DistrictName = districtName,
                        HotelName = $"{dev.Hotel.RaletedCompany.CompanyName}({dev.Hotel.ProjectName})",
                        DeviceName = dev.DeviceName,
                        CleanerSwitch = record.CleanerSwitch,
                        CleanerCurrent = record.CleanerCurrent,
                        FanSwitch = record.FanSwitch,
                        FanCurrent = record.FanCurrent,
                        DateTime = $"{record.RecordDateTime:yyyy-MM-dd HH:mm:ss}"
                    }).ToList();
            var sheet = new WorkSheet();
            foreach (var recordRow in rows)
            {
                var row = sheet.WorkSheetDatas.NewRow();
                row["设备名称"] = recordRow.DeviceName;
                row["通道号"] = recordRow.Channel;
                row["净化器开关"] = recordRow.CleanerSwitch ? "开" : "关";
                row["净化器电流"] = recordRow.CleanerCurrent;
                row["风机开关"] = recordRow.FanSwitch ? "开" : "关";
                row["风机电流"] = recordRow.FanCurrent;
                row["油烟浓度"] = recordRow.Density;
                row["数据时间"] = recordRow.DateTime;
                sheet.WorkSheetDatas.Rows.Add(row);
            }

            sheet.Title = $"{districtName} - {hotel.ProjectName}";
            dataSource.Add(sheet);

            var package = new ExcelPackage();
            foreach (var workSheet in dataSource)
            {
                var currentSheet = package.Workbook.Worksheets.Add(workSheet.Title);
                currentSheet.Column(1).Width = 35.0;
                currentSheet.Column(1).Style.Numberformat.Format = "yyyy-MM-dd hh:mm:ss";
                currentSheet.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                currentSheet.Column(1).Style.Font.Size = 14;
                currentSheet.Column(2).Width = 30.0;
                currentSheet.Column(2).Style.Font.Size = 14;
                currentSheet.Column(2).Style.Numberformat.Format = "0.00";
                currentSheet.Column(3).Width = 24.0;
                currentSheet.Column(3).Style.Font.Size = 14;
                currentSheet.Column(3).Style.Numberformat.Format = "0.00";
                currentSheet.Column(4).Width = 24.0;
                currentSheet.Column(4).Style.Font.Size = 14;
                currentSheet.Column(4).Style.Numberformat.Format = "0.00";
                currentSheet.Column(5).Width = 24.0;
                currentSheet.Column(5).Style.Font.Size = 14;
                currentSheet.Column(5).Style.Numberformat.Format = "0.00";
                currentSheet.Column(6).Width = 24.0;
                currentSheet.Column(6).Style.Font.Size = 14;
                currentSheet.Column(6).Style.Numberformat.Format = "0.00";
                currentSheet.Column(7).Width = 24.0;
                currentSheet.Column(7).Style.Font.Size = 14;
                currentSheet.Column(7).Style.Numberformat.Format = "0.00";
                currentSheet.Column(8).Width = 24.0;
                currentSheet.Column(8).Style.Font.Size = 14;
                currentSheet.Column(8).Style.Numberformat.Format = "0.00";

                using (var range = currentSheet.Cells["A1:H1"])
                {
                    range.Merge = true;
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    range.Value = workSheet.Title;
                    range.Style.Font.Size = 24;
                }

                using (var range = currentSheet.Cells["A2:H2"])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(26, 188, 156));
                    range.Style.Font.Color.SetColor(Color.White);
                }

                currentSheet.View.FreezePanes(3, 9);
                currentSheet.Cells["A2"].LoadFromDataTable(workSheet.WorkSheetDatas, true);
            }

            return new ExcelResult(package, $"餐饮油烟历史数据-{DateTime.Now.ToLongDateString()}.xlsx");
        }
    }
}