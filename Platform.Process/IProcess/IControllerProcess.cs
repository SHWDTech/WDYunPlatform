using System.Collections.Generic;
using SHWDTech.Platform.Model.Model;
using System.Web;
using SHWDTech.Platform.Model.IModel;

namespace Platform.Process.IProcess
{
    /// <summary>
    /// 控制器处理接口
    /// </summary>
    public interface IControllerProcess : IProcessBase
    {
        /// <summary>
        /// 获取当前登录用户信息
        /// </summary>
        /// <param name="context">HTTP上下文</param>
        /// <returns>当前登录用户</returns>
        WdUser GetCurrentUser(HttpContext context);

        /// <summary>
        /// 获取页面基础模型相关信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Dictionary<string, string> GetBasePageInformation(IWdUser user);
    }
}