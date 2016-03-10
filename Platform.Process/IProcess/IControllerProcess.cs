using System.Web;
using SHWDTech.Platform.Model.IModel;

namespace Platform.Process.IProcess
{
    /// <summary>
    /// 控制器处理接口
    /// </summary>
    public interface IControllerProcess
    {
        /// <summary>
        /// 获取当前登录用户信息
        /// </summary>
        /// <param name="context">HTTP上下文</param>
        /// <returns>当前登录用户</returns>
        IWdUser GetCurrentUser(HttpContext context);
    }
}