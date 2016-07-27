using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcWebComponents.Controllers;
using MvcWebComponents.Filters;
using Platform.Process;
using Platform.Process.Process;
using WebViewModels.ViewModel;

namespace Lampblack_Platform.Controllers
{
    [AjaxGet]
    public class AnalysisController : WdControllerBase
    {
        // GET: Analysis
        public ActionResult ExceptionData()
        {
            return View();
        }

        public ActionResult RunningStatus()
        {
            return View();
        }

        public ActionResult GeneralReport(GeneralReportViewModel model)
        {
            ProcessInvoke.GetInstance<ReportProcess>().GetGeneralReportViewModel(model);
            GetGeneralReport(model);

            return View(model);
        }

        public ActionResult GeneralComparison(GeneralComparisonViewModel model)
        {
            ProcessInvoke.GetInstance<ReportProcess>().GetGeneralComparisonViewModel(model);
            GetGeneralCompison(model);
            return View(model);
        }

        public ActionResult TrendAnalysis()
        {
            return View();
        }

        public ActionResult CleanlinessStatistics()
        {
            return View();
        }

        private void GetGeneralReport(GeneralReportViewModel model)
        {
            var areaList = new List<SelectListItem>
                {
                    new SelectListItem() {Text = "全部", Value = "" }
                };

            var streetList = new List<SelectListItem>
                {
                    new SelectListItem() {Text = "全部", Value = ""}
                };

            var addressList = new List<SelectListItem>
                {
                    new SelectListItem() {Text = "全部", Value = ""}
                };

            areaList.AddRange(ProcessInvoke.GetInstance<UserDictionaryProcess>()
                .GetDistrictSelectList()
                .Select(obj => new SelectListItem() { Text = obj.Value, Value = obj.Key.ToString() })
                .ToList());

            if (model.AreaGuid != Guid.Empty)
            {
                streetList.AddRange(ProcessInvoke.GetInstance<UserDictionaryProcess>()
                    .GetChildDistrict(model.AreaGuid)
                    .Select(obj => new SelectListItem() { Text = obj.Value, Value = obj.Key.ToString() })
                    .ToList());

                if (model.StreetGuid != Guid.Empty)
                {
                    addressList.AddRange(ProcessInvoke.GetInstance<UserDictionaryProcess>()
                    .GetChildDistrict(model.StreetGuid)
                    .Select(obj => new SelectListItem() { Text = obj.Value, Value = obj.Key.ToString() })
                    .ToList());

                }
            }

            model.AreaListItems = areaList;
            model.StreetListItems = streetList;
            model.AddressListItems = addressList;
        }

        private void GetGeneralCompison(GeneralComparisonViewModel model)
        {
            var areaList = new List<SelectListItem>
                {
                    new SelectListItem() {Text = "全部", Value = "" }
                };

            var streetList = new List<SelectListItem>
                {
                    new SelectListItem() {Text = "全部", Value = ""}
                };

            var addressList = new List<SelectListItem>
                {
                    new SelectListItem() {Text = "全部", Value = ""}
                };

            areaList.AddRange(ProcessInvoke.GetInstance<UserDictionaryProcess>()
                .GetDistrictSelectList()
                .Select(obj => new SelectListItem() { Text = obj.Value, Value = obj.Key.ToString() })
                .ToList());

            if (model.AreaGuid != Guid.Empty)
            {
                streetList.AddRange(ProcessInvoke.GetInstance<UserDictionaryProcess>()
                    .GetChildDistrict(model.AreaGuid)
                    .Select(obj => new SelectListItem() { Text = obj.Value, Value = obj.Key.ToString() })
                    .ToList());

                if (model.StreetGuid != Guid.Empty)
                {
                    addressList.AddRange(ProcessInvoke.GetInstance<UserDictionaryProcess>()
                    .GetChildDistrict(model.StreetGuid)
                    .Select(obj => new SelectListItem() { Text = obj.Value, Value = obj.Key.ToString() })
                    .ToList());

                }
            }

            model.AreaListItems = areaList;
            model.StreetListItems = streetList;
            model.AddressListItems = addressList;
        }
    }
}