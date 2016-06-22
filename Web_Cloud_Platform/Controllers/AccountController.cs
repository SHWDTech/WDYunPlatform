using System.Web.Mvc;
using Platform.Process.Enums;
using Platform.Process.Process;
using Web_Cloud_Platform.Models;

namespace Web_Cloud_Platform.Controllers
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

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        /// <summary>
        /// 登陆处理
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
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

            switch (signresult.Status)
            {
                    case SignInStatus.Success:
                    if (string.IsNullOrWhiteSpace(returnUrl)) return RedirectToAction("Index", "Home");
                    return Redirect(returnUrl);
            }

            return View(model);
        }
    }
}