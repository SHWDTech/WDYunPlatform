using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            => new GeneralProcess();

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
            RefreashUserPermissionsCache();

            RefreashRolePermissionsCache();

            RefreashModules();

            RefreashPermissions();
        }

        /// <summary>
        /// 刷新用户权限缓存
        /// </summary>
        public static void RefreashUserPermissionsCache()
        {
            using (var context = new RepositoryDbContext())
            {
                PlatformCaches.DeleteCachesByType("UserPermissions");
                var users = context.Set<WdUser>().Include("Permissions").Where(obj => obj.IsEnabled).ToList();
                foreach (var wdUser in users)
                {
                    PlatformCaches.Add($"User[{wdUser.Id}]-Permissions", wdUser.Permissions.ToList(), false, "UserPermissions");
                }
            }
        }

        /// <summary>
        /// 刷新角色权限缓存
        /// </summary>
        public static void RefreashRolePermissionsCache()
        {
            using (var context = new RepositoryDbContext())
            {
                PlatformCaches.DeleteCachesByType("RolePermissions");
                var roles = context.Set<WdRole>().Include("Permissions").Where(obj => obj.IsEnabled).ToList();
                foreach (var wdRole in roles)
                {
                    PlatformCaches.Add($"Role[{wdRole.Id}]-Permissions", wdRole.Permissions.ToList(), false, "RolePermissions");
                }
            }
        }

        /// <summary>
        /// 刷新系功能单缓存
        /// </summary>
        public static void RefreashModules()
        {
            using (var context = new RepositoryDbContext())
            {
                PlatformCaches.DeleteCachesByType("Modules");
                var modules = context.Set<Module>().Where(obj => obj.IsEnabled).ToList();
                foreach (var module in modules)
                {
                    PlatformCaches.Add($"Module[{module.Id}]", module, false, "Modules");
                }
            }
        }

        /// <summary>
        /// 刷新权限信息缓存
        /// </summary>
        public static void RefreashPermissions()
        {
            using (var context = new RepositoryDbContext())
            {
                PlatformCaches.DeleteCachesByName("Permissions");
                var permissions = context.Set<Permission>().ToList();
                PlatformCaches.Add("Permissions", permissions, false, "System");
            }
        }

        /// <summary>
        /// 获取系统权限列表
        /// </summary>
        /// <returns></returns>
        public static List<Permission> GetSysPeremissions()
        {
            var cache = PlatformCaches.GetCache("Permissions");

            if (cache == null)
            {
                using (var context = new RepositoryDbContext())
                {
                    var sysPeremissions = context.Set<Permission>().ToList();
                    PlatformCaches.Add("Permissions", sysPeremissions, false, "System");
                    return sysPeremissions;
                }
            }

            var permissions = cache.CacheItem as List<Permission>;

            return permissions;
        }

        /// <summary>
        /// 获取油烟设备型号信息
        /// </summary>
        /// <returns></returns>
        public static List<LampblackDeviceModel> GetDeviceModels()
        {
            using (var context = new RepositoryDbContext())
            {
                return context.Set<LampblackDeviceModel>().ToList();
            }
        }

        /// <summary>
        /// 获取系统配置信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static object GetConfig<T>(Expression<Func<T, bool>> exp) where T : class
        {
            using (var context = new RepositoryDbContext())
            {
                return context.Set<T>().Where(exp).ToList();
            }
        }
    }
}
