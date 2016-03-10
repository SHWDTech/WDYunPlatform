using System.Web;
using SHWDTech.Platform.Model.IModel;

namespace Platform.Process.IProcess
{
    /// <summary>
    /// 账户相关处理程序接口
    /// </summary>
    public interface IAccountProcess
    {
        /// <summary>
        /// 获取当前登录用户
        /// </summary>
        /// <param name="context">当前HttpContext</param>
        /// <returns>当前登录用户信息</returns>
        IWdUser GetCurrentUser(HttpContextBase context);
    }
}