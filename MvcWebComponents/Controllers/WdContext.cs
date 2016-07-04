using System.Collections.Generic;
using System.Linq;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.Model;
using Platform.Cache;

namespace MvcWebComponents.Controllers
{
    /// <summary>
    /// HTTP请求自定义上下文
    /// </summary>
    public class WdContext
    {
        private WdContext()
        {
        }

        public WdContext(WdUser user) : this()
        {
            WdUser = user;
            Roles = user.Roles.ToList();
            GetPermissions();
        }

        /// <summary>
        /// 当前登录用户
        /// </summary>
        public WdUser WdUser { get; }

        /// <summary>
        /// 当前登录用户所属域
        /// </summary>
        public IDomain Domain => WdUser?.Domain;

        /// <summary>
        /// 当前登录用户所属角色
        /// </summary>
        public List<WdRole> Roles { get; }

        /// <summary>
        /// 当前登录用户拥有的权限
        /// </summary>
        public List<Permission> Permissions { get; private set; }

        /// <summary>
        /// 当前登录用户拥有的授权模块
        /// </summary>
        public List<Module> Modules { get; private set; }

        private void GetPermissions()
        {
            Permissions = (List<Permission>)PlatformCaches.GetCache($"User[{WdUser.Id}]-Permissions").CacheItem;
            foreach (var wdRole in Roles)
            {
                var rolePermissions = (List<Permission>)PlatformCaches.GetCache($"Role[{wdRole.Id}]-Permissions").CacheItem;
                foreach (var rolePermission in rolePermissions)
                {
                    if (!Permissions.Contains(rolePermission))
                    {
                        Permissions.Add(rolePermission);
                    }
                }
            }
        }

        /// <summary>
        /// 获取拥有授权的模块列表
        /// </summary>
        public List<Module> GetAuthorizedModules()
        {
            var modules = PlatformCaches.GetCachesByType("Modules");

            Modules = modules.Select(obj => (Module)obj.CacheItem)
                    .Where(item => item.DomainId == Domain.Id)
                    .OrderBy(obj => obj.ModuleIndex)
                    .ToList();

            if (!WdUser.IsInRole("Root") && !WdUser.IsInRole("SuperAdmin"))
            {
                Modules = Modules
                    .Where(item => Permissions.Any(per => per.Id == item.PermissionId))
                    .OrderBy(obj => obj.ModuleIndex)
                    .ToList();
            }

            return Modules;
        }
    }
}
