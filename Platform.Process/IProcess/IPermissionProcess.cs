using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;

namespace Platform.Process.IProcess
{
    public interface IPermissionProcess
    {
        /// <summary>
        /// 获取所有权限
        /// </summary>
        /// <returns>所有权限</returns>
        IEnumerable<IPermission> GetAllModels();

        /// <summary>
        /// 获取指定权限
        /// </summary>
        /// <param name="exp">查询条件表达式</param>
        /// <returns>符合查询条件的权限</returns>
        IEnumerable<IPermission> GetModels(Func<IPermission, bool> exp);

        /// <summary>
        /// 获取符合条件的权限的数量
        /// </summary>
        /// <param name="exp">查询条件</param>
        /// <returns>符合条件的权限的数量</returns>
        int GetCount(Func<IPermission, bool> exp);

        /// <summary>
        /// 新建默认数据权限
        /// </summary>
        /// <returns>默认数据权限</returns>
        IPermission CreateDefaultModel();

        /// <summary>
        /// 从JSON字符串解析权限
        /// </summary>
        /// <param name="jsonString">包含权限信息的JSON字符串</param>
        /// <returns>解析后的权限</returns>
        IPermission ParseModel(string jsonString);

        /// <summary>
        /// 添加或修改权限
        /// </summary>
        /// <param name="model">被添加或修改的权限</param>
        /// <returns>操作成功返回True，失败返回False</returns>
        Guid AddOrUpdate(IPermission model);

        /// <summary>
        /// 批量添加权限
        /// </summary>
        /// <param name="models">被添加或修改的权限列表</param>
        /// <returns>成功添加或修改的权限数量</returns>
        int AddOrUpdate(IEnumerable<IPermission> models);

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="model">被删除的对象</param>
        void Delete(IPermission model);

        /// <summary>
        /// 批量删除权限
        /// </summary>
        /// <param name="models">被删除的权限列表</param>
        /// <returns>成功删除的权限数量</returns>
        int Delete(IEnumerable<IPermission> models);

        /// <summary>
        /// 判断权限是否存在
        /// </summary>
        /// <param name="model">被判断的权限</param>
        /// <returns>存在返回True，不存在返回False</returns>
        bool IsExists(IPermission model);

        /// <summary>
        /// 判断权限是否存在
        /// </summary>
        /// <param name="exp">判断条件</param>
        /// <returns>存在返回True，不存在返回False</returns>
        bool IsExists(Func<IPermission, bool> exp);

        /// <summary>
        /// 标记为删除
        /// </summary>
        /// <param name="model">标记为删除的权限</param>
        /// <returns>操作成功返回True，失败返回False</returns>
        void MarkDelete(IPermission model);

        /// <summary>
        /// 批量标记为删除
        /// </summary>
        /// <param name="models">标记为删除的的一项列表</param>
        /// <returns>成功标记为删除的权限数量</returns>
        void MarkDelete(IEnumerable<IPermission> models);

        /// <summary>
        /// 设置权限启用状态
        /// </summary>
        /// <param name="model">设置启用状态的权限</param>
        /// <param name="enableStatus">Ture设置权限状态为启用，False设置权限状态为未启用</param>
        void SetEnableStatus(IPermission model, bool enableStatus);

        /// <summary>
        /// 批量设置权限启用状态
        /// </summary>
        /// <param name="models">设置启用状态的权限列表</param>
        /// <param name="enableStatus">Ture设置权限状态为启用，False设置权限状态为未启用</param>
        void SetEnableStatus(IEnumerable<IPermission> models, bool enableStatus);
    }
}