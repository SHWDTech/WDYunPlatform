using System.Web;
using Platform.Process.Enums;
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

        /// <summary>
        /// 使用密码登陆
        /// </summary>
        /// <param name="loginName">登录名</param>
        /// <param name="password">密码</param>
        /// <param name="rememberMe">前端是否记住用户</param>
        /// <param name="shouldLockout">是否触发账户锁</param>
        /// <returns>登陆结果</returns>
        SignInStatus PasswordSignIn(string loginName, string password, bool rememberMe,
            bool shouldLockout = false);
    }
}