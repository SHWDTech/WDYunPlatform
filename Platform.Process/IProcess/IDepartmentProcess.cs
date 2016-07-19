using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using PagedList;
using SHWDTech.Platform.Model.Model;
using SqlComponents.SqlExcute;

namespace Platform.Process.IProcess
{
    public interface IDepartmentProcess : IProcessBase
    {
        /// <summary>
        /// 获取用户部门数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="queryName"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        IPagedList<Department> GetPagedDepartments(int page, int pageSize, string queryName, out int count);

        /// <summary>
        /// 更新部门信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        DbEntityValidationException AddOrUpdateDepartmentr(Department model, List<string> propertyNames);

        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <returns></returns>
        Dictionary<Guid, string> GetDepartmentSelectList();

        /// <summary>
        /// 删除系统部门
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        SqlExcuteResult DeleteDepartment(Guid userId);

        /// <summary>
        /// 获取指定ID的部门
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        Department GetDepartment(Guid guid);
    }
}
