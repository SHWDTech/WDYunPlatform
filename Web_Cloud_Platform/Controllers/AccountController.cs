using SHWDTech.Web_Cloud_Platform.Models;
using System.Web.Mvc;
using Platform.Process.Enums;
using Platform.Process.Process;

namespace SHWDTech.Web_Cloud_Platform.Controllers
{
    public class AccountController : WdControllerBase
    {
        /// <summary>
        /// 账号信息处理类
        /// </summary>
        private readonly AccountProcess _accountProcess;

        public AccountController()
        {
            _accountProcess = new AccountProcess();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return DynamicView();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(AccountLoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return DynamicView(model);
            }

            var signresult = _accountProcess.PasswordSignIn(model.LoginName, model.Password, false);

            switch (signresult)
            {
                    case SignInStatus.Success:
                    if (string.IsNullOrWhiteSpace(returnUrl)) return RedirectToAction("Index", "Home");
                    return Redirect(returnUrl);
            }

            return DynamicView();
        }
    }
}