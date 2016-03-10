using System.Web.Mvc;
using Platform.Process.Process;
using SHWDTech.Web_Cloud_Platform.Models;

namespace SHWDTech.Web_Cloud_Platform.Controllers
{
    public class AccountController : WdControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            var x = new AccountProcess();
            x.PasswordSignIn("admin", "21232f297a57a5a743894a0e4a801fc3", false);

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

            return View();
        }
    }
}