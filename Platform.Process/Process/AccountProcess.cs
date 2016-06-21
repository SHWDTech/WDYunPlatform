using System;
using System.Linq;
using Platform.Process.IProcess;
using System.Web.Security;
using Platform.Process.Enums;
using SHWD.Platform.Repository.Entities;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process.Process
{
    /// <summary>
    /// 账户相关处理程序
    /// </summary>
    public class AccountProcess : IAccountProcess
    {
        public void SignOut()
        {

        }

        public SignInResult PasswordSignIn(string loginName, string password, bool rememberMe,
            bool shouldLockout = false)
        {

            var result = new SignInResult();
            using (var context = new RepositoryDbContext())
            {
                var user = context.Set<WdUser>()
                        .FirstOrDefault(obj => obj.LoginName == loginName && obj.Password == password && obj.IsDeleted);

                if (user == null)
                {
                    result.Status = SignInStatus.Failure;
                    result.ErrorMessage = "无效的用户名或登陆密码";
                    return result;
                }

                FormsAuthentication.SetAuthCookie(loginName, false);

                user.LastLoginDateTime = DateTime.Now;
                context.SaveChanges();
            }
            result.Status = SignInStatus.Success;

            return result;
        }
    }
}