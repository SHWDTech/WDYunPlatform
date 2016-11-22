using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Platform.Process.Process;
using SHWD.Platform.Repository.Repository;

namespace MvcWebComponents.Controllers
{
    public class WdApiControllerBase : ApiController
    {
        /// <summary>
        /// 控制器处理程序
        /// </summary>
        private readonly ControllerProcess _controllerProcess = new ControllerProcess();

        /// <summary>
        /// 控制器上下文信息
        /// </summary>
        protected WdContext WdContext { get; set; }

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            var currentUser = _controllerProcess.GetCurrentUser("Manager");
            WdContext = new WdContext(currentUser);
            if (!HttpContext.Current.Items.Contains("WdContext"))
            {
                HttpContext.Current.Items.Add("WdContext", WdContext);
            }

            base.Initialize(controllerContext);
        }

        protected T ProcessInvoke<T> () where T : ProcessBase, new() => new T()
        {
            RepositoryContext = new RepositoryContext
            {
                CurrentUser = WdContext.WdUser,
                CurrentDomain = WdContext.Domain
            }
        };
    }
}
