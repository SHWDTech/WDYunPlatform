using System;
using Platform.Process.IProcess;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.IModel;
using System.Web;
using System.Web.Security;
using Platform.Process.Enums;

namespace Platform.Process.Process
{
    /// <summary>
    /// 账户相关处理程序
    /// </summary>
    public class AccountProcess : IAccountProcess
    {
        private readonly UserRepository _userRepository;

        public AccountProcess()
        {
            _userRepository = new UserRepository();
        }

        public IWdUser GetCurrentUser(HttpContextBase context)
        {
            if((context.User as IWdUser) == null) throw new ArgumentException();

            var user = _userRepository.GetUserById(((IWdUser)context.User).Id);

            if (user == null) throw new InvalidOperationException("未找到用户");

            return user;
        }

        public void SignOut()
        {
            
        }

        public SignInStatus PasswordSignIn(string loginName, string password, bool rememberMe,
            bool shouldLockout = false)
        {
            SignInStatus signInStatus;

            var user = _userRepository.GetUserByName(loginName);

            if (user == null || !CheckPassword(user, password))
            {
                signInStatus = SignInStatus.Failure;
            }
            else
            {
                SetAuthCookie(user);
                signInStatus = SignInStatus.Success;
            }

            return signInStatus;
        }

        /// <summary>
        /// 检查用户输入的密码
        /// </summary>
        /// <param name="user">当前登录的用户</param>
        /// <param name="password">用户输入的密码</param>
        /// <returns></returns>
        private bool CheckPassword(IWdUser user, string password) => user.Password == password;

        /// <summary>
        /// 更新登陆时间
        /// </summary>
        /// <param name="user"></param>
        private void UpdateLoginDate(IWdUser user)
        {
            user.LastLoginDateTime = DateTime.Now;
            _userRepository.AddOrUpdate(user);
        }

        /// <summary>
        /// 设置登录用户Cookie缓存
        /// </summary>
        /// <param name="user"></param>
        private void SetAuthCookie(IWdUser user)
        {
            FormsAuthentication.SetAuthCookie(user.LoginName, false);
            
            user.LastLoginDateTime = DateTime.Now;
            UpdateLoginDate(user);
        }
    }
}