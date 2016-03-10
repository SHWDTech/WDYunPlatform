using System.Web.Mvc;
using SHWDTech.Web_Cloud_Platform.Models;

namespace SHWDTech.Web_Cloud_Platform.Controllers
{
    public class AccountController : WdControllerBase
    {
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost]
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