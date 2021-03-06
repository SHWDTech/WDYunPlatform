﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Lampblack_Platform.Models.System;
using MvcWebComponents.Attributes;
using MvcWebComponents.Controllers;
using MvcWebComponents.Filters;
using MvcWebComponents.Model;
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

            var userList = ProcessInvoke<LampblackUserProcess>().GetPagedLampblackUsers(page, pageSize, queryName, out count);

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
        [NamedAuth(Modules = "UsersManage")]
        public ActionResult EditUser(string guid)
        {
            GetUserRelatedItems();

            if (string.IsNullOrWhiteSpace(guid))
            {
                return DefaultView();
            }

            var model = ProcessInvoke<LampblackUserProcess>().GetLampblackUser(Guid.Parse(guid));
            return View(model);
        }

        [HttpPost]
        [NamedAuth(Modules = "UsersManage")]
        public ActionResult EditUser(LampblackUser model)
        {
            var propertyNames = Request.Form.AllKeys.Where(field => field != "Id").ToList();
            propertyNames.Add("UserName");

            model.Password = Globals.GetMd5(model.Password);

            model.UserName = model.LoginName;

            var roleList = string.IsNullOrWhiteSpace(Request["Roles"]) ? null : Request["Roles"].Split(',').ToList();

            GetUserRelatedItems();

            if (ProcessInvoke<LampblackUserProcess>().HasLoginName(model))
            {
                ModelState.AddModelError("LoginName", "登录名已经存在！");
                return View(model);
            }

            var exception = ProcessInvoke<LampblackUserProcess>().AddOrUpdateLampblackUser(model, propertyNames, roleList);

            if (exception != null)
            {
                return View(model);
            }

            return RedirectToAction("SubmitSuccess", "Common",
                new { targetAction = "EditUser", targetcontroller = "System", target = "slide-up-content", postform = "user" });
        }

        [HttpGet]
        [NamedAuth(Modules = "UsersManage")]
        public ActionResult DeleteUser(Guid guid)
        {
            var success = ProcessInvoke<LampblackUserProcess>().DeleteLampblackUser(guid);

            var json = new JsonStruct
            {
                Message = !success ? "尝试删除用户信息失败，请刷新后重新尝试。" : "删除成功！",
                PostForm = success ? "user" : ""
            };

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DepartmentManage()
        {
            var page = string.IsNullOrWhiteSpace(Request["page"]) ? 1 : int.Parse(Request["page"]);

            var pageSize = string.IsNullOrWhiteSpace(Request["pageSize"]) ? 10 : int.Parse(Request["pageSize"]);

            var queryName = Request["queryName"];

            int count;

            var departmentList = ProcessInvoke<DepartmentProcess>().GetPagedDepartments(page, pageSize, queryName, out count);

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
        [NamedAuth(Modules = "DepartmentManage")]
        public ActionResult EditDepartment(string guid)
        {
            GetDepartmentRelatedItems();

            if (string.IsNullOrWhiteSpace(guid))
            {
                return DefaultView();
            }

            var model = ProcessInvoke<DepartmentProcess>().GetDepartment(Guid.Parse(guid));
            return View(model);
        }

        [HttpPost]
        [NamedAuth(Modules = "DepartmentManage")]
        public ActionResult EditDepartment(Department model)
        {
            var propertyNames = Request.Form.AllKeys.Where(field => field != "Id" && field != "X-Requested-With").ToList();

            var exception = ProcessInvoke<DepartmentProcess>().AddOrUpdateDepartmentr(model, propertyNames);

            GetDepartmentRelatedItems();

            if (exception != null)
            {
                return View(model);
            }

            return RedirectToAction("SubmitSuccess", "Common",
                new { targetAction = "EditDepartment", targetcontroller = "System", target = "slide-up-content", postform = "department" });
        }

        [HttpGet]
        [NamedAuth(Modules = "DepartmentManage")]
        public ActionResult DeleteDepartment(Guid guid)
        {
            var sqlResult = ProcessInvoke<DepartmentProcess>().DeleteDepartment(guid);

            sqlResult.Message = sqlResult.ErrorNumber == 547
                ? "选中部门已经存在关联用户，请先删除用户后再删除此部门。"
                : "删除成功！";

            var json = new JsonStruct
            {
                Message = sqlResult.Message,
                PostForm = sqlResult.Success ? "department" : ""
            };

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RoleManage()
        {
            var page = string.IsNullOrWhiteSpace(Request["page"]) ? 1 : int.Parse(Request["page"]);

            var pageSize = string.IsNullOrWhiteSpace(Request["pageSize"]) ? 10 : int.Parse(Request["pageSize"]);

            var queryName = Request["queryName"];

            int count;

            var roleList = ProcessInvoke<WdRoleProcess>().GetPagedRoles(page, pageSize, queryName, out count);

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
        [NamedAuth(Modules = "RoleManage")]
        public ActionResult EditRole(string guid)
        {
            GetRoleRelatedItems();

            if (string.IsNullOrWhiteSpace(guid))
            {
                return DefaultView();
            }

            var model = ProcessInvoke<WdRoleProcess>().GetRole(Guid.Parse(guid));
            return View(model);
        }

        [HttpPost]
        [NamedAuth(Modules = "RoleManage")]
        public ActionResult EditRole(WdRole model)
        {
            var propertyNames = Request.Form.AllKeys.Where(field => field != "Id" && field != "X-Requested-With").ToList();

            var exception = ProcessInvoke<WdRoleProcess>().AddOrUpdateRole(model, propertyNames);

            GetRoleRelatedItems();

            if (exception != null)
            {
                return View(model);
            }

            return RedirectToAction("SubmitSuccess", "Common",
                new { targetAction = "EditRole", targetcontroller = "System", target = "slide-up-content", postform = "role" });
        }

        [HttpGet]
        [NamedAuth(Modules = "RoleManage")]
        public ActionResult DeleteRole(Guid guid)
        {
            var sqlResult = ProcessInvoke<WdRoleProcess>().DeleteRole(guid);

            sqlResult.Message = sqlResult.ErrorNumber == 547
                ? "选中角色已经存在关联用户，请先删除用户后再删除此角色。"
                : "删除成功！";

            var json = new JsonStruct
            {
                Message = sqlResult.Message,
                PostForm = sqlResult.Success ? "role" : ""
            };

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Authority(Guid guid)
        {
            var role = ProcessInvoke<WdRoleProcess>().GetRole(guid);
            if (role == null)
            {
                return Json(new JsonStruct() { Message = "没有找到指定系统角色，请重新尝试！" },
                    JsonRequestBehavior.AllowGet);
            }

            var model = new AuthorityViewModel()
            {
                Role = role,
                Permissions = GeneralProcess.GetSysPeremissions()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Authority()
        {
            var roleId = Guid.Parse(Request["RoleId"]);

            var permissions = Request["Permissions"]?.Split(',').ToList();

            ProcessInvoke<WdRoleProcess>().UpdatePermissions(roleId, permissions);

            return RedirectToAction("RoleManage");
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

            var cateringCompany = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = "",
                    Value = ""
                }
            };

            cateringCompany.AddRange(ProcessInvoke<CateringEnterpriseProcess>()
            .GetCateringCompanySelectList()
            .Select(obj => new SelectListItem() { Text = obj.Value, Value = obj.Key.ToString() })
            .ToList());

            ViewBag.CateringCompany = cateringCompany;

            ViewBag.Department = ProcessInvoke<DepartmentProcess>()
                .GetDepartmentSelectList()
                .Select(obj => new SelectListItem() { Text = obj.Value, Value = obj.Key.ToString() })
                .ToList();

            ViewBag.Roles = ProcessInvoke<WdRoleProcess>()
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