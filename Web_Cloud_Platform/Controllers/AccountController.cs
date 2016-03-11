using SHWDTech.Web_Cloud_Platform.Models;
using System.Web.Mvc;
using Platform.Process.Enums;
using Platform.Process.Process;

namespace SHWDTech.Web_Cloud_Platform.Controllers
{
    public class AccountController : WdControllerBase
    {
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

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(AccountLoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var signResult = _accountProcess.PasswordSignIn(model.LoginName, model.Password, false);
            switch (signResult)
            {
                    case SignInStatus.Success:
                    return Redirect(returnUrl);
            }

            return View();
        }
    }
}