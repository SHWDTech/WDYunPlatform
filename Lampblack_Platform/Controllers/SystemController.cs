using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Lampblack_Platform.Models.System;
using MvcWebComponents.Controllers;
using MvcWebComponents.Filters;
using MvcWebComponents.Model;
using Platform.Process;
using Platform.Process.Process;
using SHWDTech.Platform.Model.Model;

namespace Lampblack_Platform.Controllers
{
    public class SystemController : WdControllerBase
    {
        // GET: System
        public ActionResult UsersManage()
        {
            var page = string.IsNullOrWhiteSpace(Request["page"]) ? 1 : int.Parse(Request["page"]);

            var pageSize = string.IsNullOrWhiteSpace(Request["pageSize"]) ? 10 : int.Parse(Request["pageSize"]);

            var queryName = Request["queryName"];

            int count;

            var userList = ProcessInvoke.GetInstance<LampblackUserProcess>().GetPagedLampblackUsers(page, pageSize, queryName, out count);

            var model = new UserViewModel
            {
                Count = count,
                PageSize = pageSize,
                QueryName = queryName,
                PageCount = (count % pageSize) > 0 ? (count / pageSize) + 1 : (count / pageSize),
                PageIndex = page,
                LampblackUsers = userList
            };

            return View(model);
        }

        public ActionResult DeleteUser()
            => View();

        public ActionResult DepartmentManage()
        {
            var page = string.IsNullOrWhiteSpace(Request["page"]) ? 1 : int.Parse(Request["page"]);

            var pageSize = string.IsNullOrWhiteSpace(Request["pageSize"]) ? 10 : int.Parse(Request["pageSize"]);

            var queryName = Request["queryName"];

            int count;

            var departmentList = ProcessInvoke.GetInstance<DepartmentProcess>().GetPagedDepartments(page, pageSize, queryName, out count);

            var model = new DepartmentViewModel
            {
                Count = count,
                PageSize = pageSize,
                QueryName = queryName,
                PageCount = (count % pageSize) > 0 ? (count / pageSize) + 1 : (count / pageSize),
                PageIndex = page,
                Departments = departmentList
            };

            return View(model);
        }

        [AjaxGet]
        [HttpGet]
        public ActionResult EditDepartment(string guid)
        {
            GetDepartmentRelatedItems();

            if (string.IsNullOrWhiteSpace(guid))
            {
                return DefaultView();
            }

            var model = ProcessInvoke.GetInstance<DepartmentProcess>().GetDepartment(Guid.Parse(guid));
            return View(model);
        }

        [AjaxGet]
        [HttpPost]
        public ActionResult EditDepartment(Department model)
        {
            var propertyNames = Request.Form.AllKeys.Where(field => field != "Id" && field != "X-Requested-With").ToList();

            var exception = ProcessInvoke.GetInstance<DepartmentProcess>().AddOrUpdateDepartmentr(model, propertyNames);

            GetDepartmentRelatedItems();

            if (exception != null)
            {
                return View(model);
            }

            return RedirectToAction("SubmitSuccess", "Common",
                new { targetAction = "EditDepartment", targetcontroller = "System", target = "slide-up-content", postform = "department" });
        }

        [AjaxGet]
        [HttpGet]
        public ActionResult DeleteDepartment(Guid guid)
        {
            var areaId = Guid.Parse(Request["Id"]);

            var sqlResult = ProcessInvoke.GetInstance<UserDictionaryProcess>().DeleteArea(areaId);

            sqlResult.Message = sqlResult.ErrorNumber == 547
                ? "选中部门已经存在关联用户，请先删除用户后再删除此部门。"
                : "删除成功！";

            var json = new JsonStruct
            {
                Success = sqlResult.Success,
                Message = sqlResult.Message
            };

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        private void GetDepartmentRelatedItems()
        {
            ViewBag.Enable = new List<SelectListItem>()
            {
                new SelectListItem() {Text = "启用", Value = "true"},
                new SelectListItem() {Text = "停用", Value = "false"}
            };
        }
    }
}