﻿using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;

namespace Platform.Process.IProcess
{
    /// <summary>
    /// 角色处理接口
    /// </summary>
    public interface IRoleProcess
    {
        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <returns>所有角色</returns>
        IEnumerable<IRole> GetAllModels();

        /// <summary>
        /// 获取指定角色
        /// </summary>
        /// <param name="exp">查询条件表达式</param>
        /// <returns>符合查询条件的角色</returns>
        IEnumerable<IRole> GetModels(Func<IRole, bool> exp);

        /// <summary>
        /// 获取符合条件的角色的数量
        /// </summary>
        /// <param name="exp">查询条件</param>
        /// <returns>符合条件的角色的数量</returns>
        int GetCount(Func<IRole, bool> exp);

        /// <summary>
        /// 新建默认数据角色
        /// </summary>
        /// <returns>默认数据角色</returns>
        IRole CreateDefaultModel();

        /// <summary>
        /// 从JSON字符串解析角色
        /// </summary>
        /// <param name="jsonString">包含角色信息的JSON字符串</param>
        /// <returns>解析后的角色</returns>
        IRole ParseModel(string jsonString);

        /// <summary>
        /// 添加或修改角色
        /// </summary>
        /// <param name="model">被添加或修改的角色</param>
        /// <returns>操作成功返回True，失败返回False</returns>
        Guid AddOrUpdate(IRole model);

        /// <summary>
        /// 批量添加角色
        /// </summary>
        /// <param name="models">被添加或修改的角色列表</param>
        /// <returns>成功添加或修改的角色数量</returns>
        int AddOrUpdate(IEnumerable<IRole> models);

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="model">被删除的对象</param>
        void Delete(IRole model);

        /// <summary>
        /// 批量删除角色
        /// </summary>
        /// <param name="models">被删除的角色列表</param>
        /// <returns>成功删除的角色数量</returns>
        int Delete(IEnumerable<IRole> models);

        /// <summary>
        /// 判断角色是否存在
        /// </summary>
        /// <param name="model">被判断的角色</param>
        /// <returns>存在返回True，不存在返回False</returns>
        bool IsExists(IRole model);

        /// <summary>
        /// 判断角色是否存在
        /// </summary>
        /// <param name="exp">判断条件</param>
        /// <returns>存在返回True，不存在返回False</returns>
        bool IsExists(Func<IRole, bool> exp);

        /// <summary>
        /// 标记为删除
        /// </summary>
        /// <param name="model">标记为删除的角色</param>
        /// <returns>操作成功返回True，失败返回False</returns>
        void MarkDelete(IRole model);

        /// <summary>
        /// 批量标记为删除
        /// </summary>
        /// <param name="models">标记为删除的的一项列表</param>
        /// <returns>成功标记为删除的角色数量</returns>
        void MarkDelete(IEnumerable<IRole> models);

        /// <summary>
        /// 设置角色启用状态
        /// </summary>
        /// <param name="model">设置启用状态的角色</param>
        /// <param name="enableStatus">Ture设置角色状态为启用，False设置角色状态为未启用</param>
        void SetEnableStatus(IRole model, bool enableStatus);

        /// <summary>
        /// 批量设置角色启用状态
        /// </summary>
        /// <param name="models">设置启用状态的角色列表</param>
        /// <param name="enableStatus">Ture设置角色状态为启用，False设置角色状态为未启用</param>
        void SetEnableStatus(IEnumerable<IRole> models, bool enableStatus);
    }
}