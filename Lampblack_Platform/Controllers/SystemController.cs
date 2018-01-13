using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;
using Lampblack_Platform.Models.BootstrapTable;
using Lampblack_Platform.Models.Management;
using Lampblack_Platform.Models.System;
using MvcWebComponents.Attributes;
using MvcWebComponents.Controllers;
using MvcWebComponents.Filters;
using MvcWebComponents.Model;
using Platform.Cache;
using Platform.Process.Process;
using SHWD.Platform.Repository.Entities;
using SHWDTech.Platform.Model.Business;
using SHWDTech.Platform.Model.Enums;
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

            var userList = ProcessInvoke<LampblackUserProcess>().GetPagedLampblackUsers(page, pageSize, queryName, out var count);

            var model = new UserViewModel
            {
                Count = count,
                PageSize = pageSize,
                QueryName = queryName,
                PageCount = count % pageSize > 0 ? count / pageSize + 1 : count / pageSize,
                PageIndex = page,
                LampblackUsers = userList
            };

            return View(model);
        }

        [HttpGet]
        [NamedAuth(Modules = nameof(UsersManage))]
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
        [NamedAuth(Modules = nameof(UsersManage))]
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
                ModelState.AddModelError(nameof(LoginName), @"登录名已经存在！");
                return View(model);
            }

            var exception = ProcessInvoke<LampblackUserProcess>().AddOrUpdateLampblackUser(model, propertyNames, roleList);

            if (exception != null)
            {
                return View(model);
            }

            return RedirectToAction("SubmitSuccess", nameof(Common),
                new { targetAction = nameof(EditUser), targetcontroller = "System", target = "slide-up-content", postform = "user" });
        }

        [HttpGet]
        [NamedAuth(Modules = nameof(UsersManage))]
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

            var departmentList = ProcessInvoke<DepartmentProcess>().GetPagedDepartments(page, pageSize, queryName, out var count);

            var model = new DepartmentViewModel
            {
                Count = count,
                PageSize = pageSize,
                QueryName = queryName,
                PageCount = count % pageSize > 0 ? count / pageSize + 1 : count / pageSize,
                PageIndex = page,
                Departments = departmentList
            };

            return View(model);
        }

        [HttpGet]
        [NamedAuth(Modules = nameof(DepartmentManage))]
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
        [NamedAuth(Modules = nameof(DepartmentManage))]
        public ActionResult EditDepartment(Department model)
        {
            var propertyNames = Request.Form.AllKeys.Where(field => field != "Id" && field != "X-Requested-With").ToList();

            var exception = ProcessInvoke<DepartmentProcess>().AddOrUpdateDepartmentr(model, propertyNames);

            GetDepartmentRelatedItems();

            if (exception != null)
            {
                return View(model);
            }

            return RedirectToAction("SubmitSuccess", nameof(Common),
                new { targetAction = nameof(EditDepartment), targetcontroller = "System", target = "slide-up-content", postform = "department" });
        }

        [HttpGet]
        [NamedAuth(Modules = nameof(DepartmentManage))]
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

            var roleList = ProcessInvoke<WdRoleProcess>().GetPagedRoles(page, pageSize, queryName, out var count);

            var model = new RoleViewModel
            {
                Count = count,
                PageSize = pageSize,
                QueryName = queryName,
                PageCount = count % pageSize > 0 ? count / pageSize + 1 : count / pageSize,
                PageIndex = page,
                Roles = roleList
            };

            return View(model);
        }

        [HttpGet]
        [NamedAuth(Modules = nameof(RoleManage))]
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
        [NamedAuth(Modules = nameof(RoleManage))]
        public ActionResult EditRole(WdRole model)
        {
            var propertyNames = Request.Form.AllKeys.Where(field => field != "Id" && field != "X-Requested-With").ToList();

            var exception = ProcessInvoke<WdRoleProcess>().AddOrUpdateRole(model, propertyNames);

            GetRoleRelatedItems();

            if (exception != null)
            {
                return View(model);
            }

            return RedirectToAction("SubmitSuccess", nameof(Common),
                new { targetAction = nameof(EditRole), targetcontroller = "System", target = "slide-up-content", postform = "role" });
        }

        [HttpGet]
        [NamedAuth(Modules = nameof(RoleManage))]
        public ActionResult DeleteRole(Guid guid)
        {
            var sqlResult = ProcessInvoke<WdRoleProcess>().DeleteRole(guid);

            sqlResult.Message = sqlResult.ErrorNumber == 547
                ? "选中角色已经存在关联用户，请先删除用户后再删除此角色。"
                : "删除成功！";

            var json = new JsonStruct
            {
                Message = sqlResult.Message,
                PostForm = sqlResult.Success ? "role" : string.Empty
            };

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Authority(Guid guid)
        {
            var role = ProcessInvoke<WdRoleProcess>().GetRole(guid);
            if (role == null)
            {
                return Json(new JsonStruct { Message = "没有找到指定系统角色，请重新尝试！" },
                    JsonRequestBehavior.AllowGet);
            }

            var model = new AuthorityViewModel
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

            return RedirectToAction(nameof(RoleManage));
        }

        [HttpGet]
        public ActionResult DomainRegister() =>
            !WdContext.WdUser.IsInRole("Root")
            ? Redirect("/Error/UnAuthorized")
            : View();

        [HttpPost]
        public ActionResult DomainRegister(DomainRegisterModel model)
        {
            if (ProcessInvoke<LampblackUserProcess>().HasLoginName(model.LoginName))
            {
                ModelState.AddModelError(nameof(LoginName), @"登录名已经存在！");
                return View(model);
            }
            using (var scope = new TransactionScope())
            {
                try
                {
                    using (var ctx = new RepositoryDbContext())
                    {
                        var domain = new Domain
                        {
                            DomainName = model.DomainName,
                            DomianType = "UserDomain",
                            DomainStatus = DomainStatus.Enabled,
                            CreateDateTime = DateTime.Now,
                            CreateUserId = WdContext.WdUser.Id,
                            IsEnabled = true,
                            IsDeleted = false
                        };
                        ctx.SysDomains.Add(domain);
                        var sysManagerRole = ctx.Roles.First(r => r.RoleName == "系统管理员");
                        var user = new LampblackUser
                        {
                            UserName = model.LoginName,
                            LoginName = model.LoginName,
                            UserIdentityName = model.ManagerName,
                            Password = Globals.GetMd5(model.ManagerPassword),
                            Status = UserStatus.Enabled,
                            Domain = domain,
                            CreateUserId = WdContext.WdUser.Id,
                            CreateDateTime = DateTime.Now,
                            IsDeleted = false,
                            IsEnabled = true
                        };
                        user.Roles.Add(sysManagerRole);
                        ctx.LampblackUsers.Add(user);
                        var rootDeviceModel = ctx.LampblackDeviceModels.First(m => m.DomainId == WdContext.WdUser.DomainId);
                        var newModel = new LampblackDeviceModel
                        {
                            Name = rootDeviceModel.Name,
                            Fail = rootDeviceModel.Fail,
                            Worse = rootDeviceModel.Worse,
                            Qualified = rootDeviceModel.Qualified,
                            Good = rootDeviceModel.Good,
                            Domain = domain,
                            CreateUserId = WdContext.WdUser.Id,
                            CreateDateTime = DateTime.Now,
                            IsDeleted = false,
                            IsEnabled = true
                        };
                        ctx.LampblackDeviceModels.Add(newModel);
                        ctx.SaveChanges();
                        var deviceModels = GeneralProcess.GetDeviceModels();
                        foreach (var devModel in deviceModels)
                        {
                            var rate = new CleanessRate(devModel);
                            PlatformCaches.Add($"CleanessRate-{devModel.Id}", rate, false, "deviceModels");
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogService.Instance.Error("新增用户域失败。", ex);
                    ModelState.AddModelError("DomainName", @"新增用户域失败！");
                    return View(model);
                }

                scope.Complete();
            }

            ModelState.AddModelError("DomainName", @"新增用户域成功！");
            return View(model);
        }

        public ActionResult PlatformConfig() => View();

        public ActionResult HuangpuPlatfromConfig() => View();

        public ActionResult HuangpuPlatformTable(BootstrapTablePostParams post)
        {
            using (var ctx = new RepositoryDbContext())
            {
                var rows = new List<HuangpuPlatformConfigViewModel>();
                var total = ctx.SysDomains.Count();
                var domains = ctx.SysDomains.Where(d => d.DomianType == "UserDomain")
                    .OrderBy(d => d.Id)
                    .Skip(post.offset)
                    .Take(post.limit)
                    .ToList();
                var configs = ctx.SysDictionaries.Where(d => d.ItemName == "HuangpuPlatform").ToList();
                foreach (var domain in domains)
                {
                    var shortCodeItem = configs.FirstOrDefault(c => c.ItemValue == domain.Id.ToString());
                    var model = new HuangpuPlatformConfigViewModel
                    {
                        DomainId = domain.Id,
                        DomainName = domain.DomainName,
                        ShortCode = shortCodeItem?.ItemKey.Replace("DomainId", string.Empty) ?? "",
                    };
                    rows.Add(model);
                }

                return JsonTable(new
                {
                    total,
                    rows
                });
            }
        }

        [HttpPost]
        public ActionResult EditHuangpuPlatfrom(HuangputPlatfromEditViewModel model)
        {
            try
            {
                using (var ctx = new RepositoryDbContext())
                {
                    SysDictionary prefix;
                    var orignal = ctx.SysDictionaries.FirstOrDefault(s =>
                        s.ItemName == "HuangpuPlatform" && s.ItemValue == model.DomainId.ToString());
                    if (orignal == null)
                    {
                        orignal = new SysDictionary
                        {
                            ItemName = "HuangpuPlatform",
                            ItemKey = $"{model.ShortCode}DomainId",
                            ItemValue = model.DomainId.ToString(),
                            CreateDateTime = DateTime.Now,
                            CreateUserId = WdContext.WdUser.Id
                        };
                        prefix = new SysDictionary
                        {
                            ItemName = "HuangpuPlatform",
                            ItemKey = $"{model.ShortCode}Prefix",
                            ItemValue = model.ShortCode.ToUpper(),
                            CreateDateTime = DateTime.Now,
                            CreateUserId = WdContext.WdUser.Id
                        };
                    }
                    else
                    {
                        var findPrefix = orignal.ItemKey.Replace("DomainId", "Prefix");
                        prefix = ctx.SysDictionaries.First(s =>
                            s.ItemName == "HuangpuPlatform" &&
                            s.ItemKey == findPrefix);
                        orignal.ItemKey = $"{model.ShortCode}DomainId";
                        prefix.ItemKey = $"{model.ShortCode}Prefix";
                        prefix.ItemValue = model.ShortCode.ToUpper();
                    }

                    ctx.SysDictionaries.AddOrUpdate(orignal);
                    ctx.SysDictionaries.AddOrUpdate(prefix);
                    ctx.SaveChanges();
                    return RedirectToAction("SubmitSuccess", nameof(Common), null);
                }
            }
            catch (Exception ex)
            {
                LogService.Instance.Error("编辑黄浦区平台信息失败", ex);
                ModelState.AddModelError("", "编辑失败，请联系管理员");
                return View();
            }
        }

        [HttpGet]
        public ActionResult EditHuangpuPlatfrom(Guid domainId)
        {
            using (var ctx = new RepositoryDbContext())
            {
                var model = new HuangputPlatfromEditViewModel
                {
                    DomainId = domainId
                };
                var orignal = ctx.SysDictionaries.FirstOrDefault(s =>
                    s.ItemName == "HuangpuPlatform" && s.ItemValue == model.DomainId.ToString());
                if (orignal != null)
                {
                    model.ShortCode = orignal.ItemKey.Replace("DomainId", string.Empty);
                }
                return View();
            }
        }

        public ActionResult JinganPlatfromConfig()
        {
            return null;
        }

        private void GetDepartmentRelatedItems()
        {
            ViewBag.Enable = new List<SelectListItem>
            {
                new SelectListItem {Text = @"启用", Value = "true"},
                new SelectListItem {Text = @"停用", Value = "false"}
            };
        }

        private void GetUserRelatedItems()
        {
            ViewBag.Enable = new List<SelectListItem>
            {
                new SelectListItem {Text = @"启用", Value = "true"},
                new SelectListItem {Text = @"停用", Value = "false"}
            };

            var cateringCompany = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "",
                    Value = ""
                }
            };

            cateringCompany.AddRange(ProcessInvoke<CateringEnterpriseProcess>()
            .GetCateringCompanySelectList()
            .Select(obj => new SelectListItem { Text = obj.Value, Value = obj.Key.ToString() })
            .ToList());

            ViewBag.CateringCompany = cateringCompany;

            ViewBag.Department = ProcessInvoke<DepartmentProcess>()
                .GetDepartmentSelectList()
                .Select(obj => new SelectListItem { Text = obj.Value, Value = obj.Key.ToString() })
                .ToList();

            ViewBag.Roles = ProcessInvoke<WdRoleProcess>()
                .GetRoleSelectList();
        }

        private void GetRoleRelatedItems()
        {
            ViewBag.Enable = new List<SelectListItem>
            {
                new SelectListItem {Text = @"启用", Value = "true"},
                new SelectListItem {Text = @"停用", Value = "false"}
            };
        }
    }
}