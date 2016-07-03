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
using SHWDTech.Platform.Utility;

namespace Lampblack_Platform.Controllers
{
    [AjaxGet]
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

        [HttpGet]
        public ActionResult EditUser(string guid)
        {
            GetUserRelatedItems();

            if (string.IsNullOrWhiteSpace(guid))
            {
                return DefaultView();
            }

            var model = ProcessInvoke.GetInstance<LampblackUserProcess>().GetLampblackUser(Guid.Parse(guid));
            return View(model);
        }

        [HttpPost]
        public ActionResult EditUser(LampblackUser model)
        {
            var propertyNames = Request.Form.AllKeys.Where(field => field != "Id").ToList();
            propertyNames.Add("UserName");

            model.Password = Globals.GetMd5(model.Password);

            model.UserName = model.LoginName;

            var roleList = Request["Roles"].Split(',').ToList();

            var exception = ProcessInvoke.GetInstance<LampblackUserProcess>().AddOrUpdateLampblackUser(model, propertyNames, roleList);

            GetUserRelatedItems();

            if (exception != null)
            {
                return View(model);
            }

            return RedirectToAction("SubmitSuccess", "Common",
                new { targetAction = "EditUser", targetcontroller = "System", target = "slide-up-content", postform = "user" });
        }

        [HttpGet]
        public ActionResult DeleteUser(Guid guid)
        {
            var areaId = Guid.Parse(Request["Id"]);

            var success = ProcessInvoke.GetInstance<LampblackUserProcess>().DeleteLampblackUser(areaId);

            var json = new JsonStruct
            {
                Success = success,
                Message = !success ? "尝试删除用户信息失败，请刷新后重新尝试。" : "删除成功！",
                PostForm = "user"
            };

            return Json(json, JsonRequestBehavior.AllowGet);
        }

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

        [HttpGet]
        public ActionResult DeleteDepartment(Guid guid)
        {
            var areaId = Guid.Parse(Request["Id"]);

            var sqlResult = ProcessInvoke.GetInstance<DepartmentProcess>().DeleteDepartment(areaId);

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

        public ActionResult RoleManage()
        {
            var page = string.IsNullOrWhiteSpace(Request["page"]) ? 1 : int.Parse(Request["page"]);

            var pageSize = string.IsNullOrWhiteSpace(Request["pageSize"]) ? 10 : int.Parse(Request["pageSize"]);

            var queryName = Request["queryName"];

            int count;

            var roleList = ProcessInvoke.GetInstance<WdRoleProcess>().GetPagedRoles(page, pageSize, queryName, out count);

            var model = new RoleViewModel
            {
                Count = count,
                PageSize = pageSize,
                QueryName = queryName,
                PageCount = (count % pageSize) > 0 ? (count / pageSize) + 1 : (count / pageSize),
                PageIndex = page,
                Roles = roleList
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult EditRole(string guid)
        {
            GetRoleRelatedItems();

            if (string.IsNullOrWhiteSpace(guid))
            {
                return DefaultView();
            }

            var model = ProcessInvoke.GetInstance<WdRoleProcess>().GetRole(Guid.Parse(guid));
            return View(model);
        }

        [HttpPost]
        public ActionResult EditRole(WdRole model)
        {
            var propertyNames = Request.Form.AllKeys.Where(field => field != "Id" && field != "X-Requested-With").ToList();

            var exception = ProcessInvoke.GetInstance<WdRoleProcess>().AddOrUpdateRole(model, propertyNames);

            GetRoleRelatedItems();

            if (exception != null)
            {
                return View(model);
            }

            return RedirectToAction("SubmitSuccess", "Common",
                new { targetAction = "EditRole", targetcontroller = "System", target = "slide-up-content", postform = "role" });
        }

        [HttpGet]
        public ActionResult DeleteRole(Guid guid)
        {
            var areaId = Guid.Parse(Request["Id"]);

            var sqlResult = ProcessInvoke.GetInstance<WdRoleProcess>().DeleteRole(areaId);

            sqlResult.Message = sqlResult.ErrorNumber == 547
                ? "选中部门已经存在关联用户，请先删除用户后再删除此角色。"
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

        private void GetUserRelatedItems()
        {
            ViewBag.Enable = new List<SelectListItem>()
            {
                new SelectListItem() {Text = "启用", Value = "true"},
                new SelectListItem() {Text = "停用", Value = "false"}
            };

            ViewBag.CateringCompany = ProcessInvoke.GetInstance<CateringEnterpriseProcess>()
                .GetCateringCompanySelectList()
                .Select(obj => new SelectListItem() { Text = obj.Key, Value = obj.Value })
                .ToList();

            ViewBag.Department = ProcessInvoke.GetInstance<DepartmentProcess>()
                .GetDepartmentSelectList()
                .Select(obj => new SelectListItem() { Text = obj.Key, Value = obj.Value })
                .ToList();

            ViewBag.Roles = ProcessInvoke.GetInstance<WdRoleProcess>()
                .GetRoleSelectList();
        }

        private void GetRoleRelatedItems()
        {
            ViewBag.Enable = new List<SelectListItem>()
            {
                new SelectListItem() {Text = "启用", Value = "true"},
                new SelectListItem() {Text = "停用", Value = "false"}
            };
        }
    }
}