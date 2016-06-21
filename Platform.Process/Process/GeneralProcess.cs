using System;
using System.Linq;
using Platform.Cache;
using SHWD.Platform.Repository;
using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Model;
using SHWDTech.Platform.Utility;

namespace Platform.Process.Process
{
    /// <summary>
    /// 通用处理程序
    /// </summary>
    public static class GeneralProcess
    {
        /// <summary>
        /// 根据用户登录名获取用户
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public static WdUser DoLoginByLoginName(string loginName)
            => DbRepository.Repo<UserRepository>().GetUserByLoginName(loginName);

        public static WdUser GetUserByLoginName(string loginName)
            => DbRepository.Repo<UserRepository>().GetUserByLoginName(loginName);

        /// <summary>
        /// 读取权限信息
        /// </summary>
        public static void LoadBaseInfomations()
        {
            using (var context = new RepositoryDbContext())
            {
                try
                {
                    var users = context.Set<WdUser>().Include("Permissions").ToList();
                    foreach (var wdUser in users)
                    {
                        PlatformCaches.Add($"User[{wdUser.Id}]-Permissions", wdUser.Permissions, false, "Permissions");
                    }

                    var roles = context.Set<WdRole>().Include("Permissions").ToList();
                    foreach (var wdRole in roles)
                    {
                        PlatformCaches.Add($"Role[{wdRole.Id}]-Permissions", wdRole.Permissions, false, "Permissions");
                    }

                    var modules = context.Set<Module>().ToList();
                    foreach (var module in modules)
                    {
                        PlatformCaches.Add($"Module[{module.Id}]", module, false, "Modules");
                    }
                }
                catch (Exception ex)
                {
                    
                    LogService.Instance.Error("DbError", ex);
                }
                
            }
        }
    }
}
