using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using Lampblack_Platform.Models.Account;
using MvcWebComponents.Attributes;
using MvcWebComponents.Controllers;
using MvcWebComponents.Filters;
using Platform.Process.Enums;
using Platform.Process.Process;

namespace Lampblack_Platform.Controllers
{
    [NamedAuth(Modules = "Ignore")]
    public class AccountController : WdControllerBase
    {
        private AccountProcess Process { get; } = new AccountProcess();

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.LoginTitle = "餐饮油烟在线监控平台";
            return DefaultView();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(HttpAntiForgeryException), View = "Login")]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.LoginTitle = "餐饮油烟在线监控平台";
                return DefaultView(model);
            }

            var result = Process.PasswordSignIn(model.LoginName, model.Password, model.RememberMe);

            if (result.Status == SignInStatus.Failure)
            {
                ViewBag.LoginTitle = "餐饮油烟在线监控平台";
                ModelState.AddModelError(result.ErrorElement, result.ErrorMessage);
                return DefaultView(model);
            }

            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                return RedirectToAction("Index", "Home");
            }

            return Redirect(returnUrl);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        [NamedAuth(Modules = "Ignore")]
        public ActionResult SetUp()
        {
            var user = ProcessInvoke<LampblackUserProcess>().GetLampblackUser(WdContext.WdUser.Id);
            var model = new SetUpViewModel
            {
                UserId = user.Id,
                LoginName = user.LoginName,
                UserIdentityName = user.UserIdentityName
            };

            return View(model);
        }

        [NamedAuth(Modules = "Ignore")]
        [HttpPost]
        [AjaxGet]
        public ActionResult SetUp(SetUpViewModel model)
        {
            var propertys = new Dictionary<string, string>()
            {
                {"UserId", model.UserId.ToString() },
                {"UserIdentityName", model.UserIdentityName},
                {"Password", model.Password }
            };
            model.UpdateSuccessed = ProcessInvoke<LampblackUserProcess>().UpdateUserInfo(propertys);

            return View(model);
        }
    }
}