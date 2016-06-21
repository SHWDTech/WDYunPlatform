using System.Web.Mvc;

namespace MvcWebComponents.Controllers
{
    public class WdControllerBase : System.Web.Mvc.Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext ctx)
        {
            if (ctx.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                || ctx.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                base.OnActionExecuting(ctx);
            }
        }
    }
}
