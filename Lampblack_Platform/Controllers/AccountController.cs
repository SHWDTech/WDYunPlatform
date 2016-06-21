using System.Web.Mvc;
using System.Web.Security;
using Lampblack_Platform.Models.Account;
using MvcWebComponents.Controllers;
using Platform.Process.Enums;
using Platform.Process.Process;

namespace Lampblack_Platform.Controllers
{
    public class AccountController : WdControllerBase
    {
        private AccountProcess Process { get; } = new AccountProcess();

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.LoginTitle = "餐饮油烟在线监控平台";
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = Process.PasswordSignIn(model.LoginName, model.Password, model.RememberMe);

            if (result.Status == SignInStatus.Failure)
            {
                ViewBag.LoginTitle = "餐饮油烟在线监控平台";
                ModelState.AddModelError(result.ErrorElement, result.ErrorMessage);
                return View(model);
            }

            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                return RedirectToAction("Index", "Home");
            }

            return Redirect(returnUrl);
        }

        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}