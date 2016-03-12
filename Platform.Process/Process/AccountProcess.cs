using Platform.Process.IProcess;
using SHWD.Platform.Repository.Repository;
using System.Web.Security;
using Platform.Process.Enums;
using SHWDTech.Platform.Utility;

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

        public void SignOut()
        {
            
        }

        public SignInStatus PasswordSignIn(string loginName, string password, bool rememberMe,
            bool shouldLockout = false)
        {
            SignInStatus signInStatus;

            if (!_userRepository.IsExists(item => item.LoginName == loginName) || !CheckPassword(loginName, Globals.GetMd5(password)))
            {
                signInStatus = SignInStatus.Failure;
            }
            else
            {
                SetAuthCookie(loginName);
                signInStatus = SignInStatus.Success;
            }

            return signInStatus;
        }

        /// <summary>
        /// 检查用户输入的密码
        /// </summary>
        /// <param name="loginName">当前登录的用户</param>
        /// <param name="password">用户输入的密码</param>
        /// <returns></returns>
        private bool CheckPassword(string loginName, string password) => _userRepository.IsExists(user => user.LoginName == loginName && user.Password == password);

        /// <summary>
        /// 设置登录用户Cookie缓存
        /// </summary>
        /// <param name="loginName"></param>
        private void SetAuthCookie(string loginName)
        {
            FormsAuthentication.SetAuthCookie(loginName, false);
            
            _userRepository.UpdateLoginInfo(loginName);
        }
    }
}