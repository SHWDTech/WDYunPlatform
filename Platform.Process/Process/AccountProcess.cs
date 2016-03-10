using System;
using System.Linq;
using Platform.Process.IProcess;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.IModel;
using System.Web;
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
            var user = _userRepository.GetUserByName(context.User.Identity.Name);
            if (user == null) throw new InvalidOperationException("未找到用户");

            return user;
        }

        public SignInStatus PasswordSignIn(string loginName, string password, bool rememberMe,
            bool shouldLockout = false)
        {
            SignInStatus signInStatus;

            if (_userRepository.GetUserByName(loginName) == null || !CheckPassword(loginName, password))
            {
                signInStatus = SignInStatus.Failure;
            }
            else
            {
                signInStatus = SignInStatus.Success;
            }

            return signInStatus;
        }

        public bool CheckPassword(string loginName, string password) => _userRepository.GetModels(obj => obj.UserName == loginName && obj.Password == password).Count() == 1;

        public void UpdateLoginDate(IWdUser user)
        {
            user.LastLoginDateTime = DateTime.Now;
            _userRepository.AddOrUpdate(user);
        }

        public void SetAuthCookie(IWdUser user)
        {

            UpdateLoginDate(user);
        }
    }
}