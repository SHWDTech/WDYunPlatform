using System.Linq;
using Platform.Cache;
using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process.Process
{
    /// <summary>
    /// 通用处理程序
    /// </summary>
    public class GeneralProcess : ProcessBase
    {
        private static GeneralProcess Process 
            => _process ?? (_process = new GeneralProcess());

        private static GeneralProcess _process;

        /// <summary>
        /// 通过登录名获取用户信息
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public static WdUser GetUserByLoginName(string loginName)
            => Process.Repo<UserRepository>().GetUserByLoginName(loginName);

        /// <summary>
        /// 读取权限信息
        /// </summary>
        public static void LoadBaseInfomations()
        {
            using (var context = new RepositoryDbContext())
            {
                var users = context.Set<WdUser>().Include("Permissions").ToList();
                foreach (var wdUser in users)
                {
                    PlatformCaches.Add($"User[{wdUser.Id}]-Permissions", wdUser.Permissions.ToList(), false, "Permissions");
                }

                var roles = context.Set<WdRole>().Include("Permissions").ToList();
                foreach (var wdRole in roles)
                {
                    PlatformCaches.Add($"Role[{wdRole.Id}]-Permissions", wdRole.Permissions.ToList(), false, "Permissions");
                }

                var modules = context.Set<Module>().ToList();
                foreach (var module in modules)
                {
                    PlatformCaches.Add($"Module[{module.Id}]", module, false, "Modules");
                }
            }
        }
    }
}
