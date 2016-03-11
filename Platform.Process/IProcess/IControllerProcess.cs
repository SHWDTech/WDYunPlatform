using SHWDTech.Platform.Model.Model;
using System.Web;

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
        WdUser GetCurrentUser(HttpContext context);
    }
}