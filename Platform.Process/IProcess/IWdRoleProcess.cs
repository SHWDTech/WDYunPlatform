using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using PagedList;
using SHWDTech.Platform.Model.Model;
using SqlComponents.SqlExcute;

namespace Platform.Process.IProcess
{
    public interface IWdRoleProcess
    {
        /// <summary>
        /// 获取角色分页数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="queryName"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        IPagedList<WdRole> GetPagedRoles(int page, int pageSize, string queryName, out int count);

        /// <summary>
        /// 更新角色信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        DbEntityValidationException AddOrUpdateRole(WdRole model, List<string> propertyNames);

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        Dictionary<string, string> GetRoleSelectList();

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        SqlExcuteResult DeleteRole(Guid userId);

        /// <summary>
        /// 获取指定ID的角色信息
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        WdRole GetRole(Guid guid);
    }
}
