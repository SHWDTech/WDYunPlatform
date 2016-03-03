using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;

namespace Platform.Process.IProcess
{
    public interface IMenuProcess
    {
        /// <summary>
        /// 获取所有菜单
        /// </summary>
        /// <returns>所有菜单</returns>
        IEnumerable<IMenu> GetAllModels();

        /// <summary>
        /// 获取指定菜单
        /// </summary>
        /// <param name="exp">查询条件表达式</param>
        /// <returns>符合查询条件的菜单</returns>
        IEnumerable<IMenu> GetModels(Func<IMenu, bool> exp);

        /// <summary>
        /// 获取符合条件的菜单的数量
        /// </summary>
        /// <param name="exp">查询条件</param>
        /// <returns>符合条件的菜单的数量</returns>
        int GetCount(Func<IMenu, bool> exp);

        /// <summary>
        /// 新建默认数据菜单
        /// </summary>
        /// <returns>默认数据菜单</returns>
        IMenu CreateDefaultModel();

        /// <summary>
        /// 从JSON字符串解析菜单
        /// </summary>
        /// <param name="jsonString">包含菜单信息的JSON字符串</param>
        /// <returns>解析后的菜单</returns>
        IMenu ParseModel(string jsonString);

        /// <summary>
        /// 添加或修改菜单
        /// </summary>
        /// <param name="model">被添加或修改的菜单</param>
        /// <returns>操作成功返回True，失败返回False</returns>
        Guid AddOrUpdate(IMenu model);

        /// <summary>
        /// 批量添加菜单
        /// </summary>
        /// <param name="models">被添加或修改的菜单列表</param>
        /// <returns>成功添加或修改的菜单数量</returns>
        int AddOrUpdate(IEnumerable<IMenu> models);

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="model">被删除的对象</param>
        void Delete(IMenu model);

        /// <summary>
        /// 批量删除菜单
        /// </summary>
        /// <param name="models">被删除的菜单列表</param>
        /// <returns>成功删除的菜单数量</returns>
        int Delete(IEnumerable<IMenu> models);

        /// <summary>
        /// 判断菜单是否存在
        /// </summary>
        /// <param name="model">被判断的菜单</param>
        /// <returns>存在返回True，不存在返回False</returns>
        bool IsExists(IMenu model);

        /// <summary>
        /// 判断菜单是否存在
        /// </summary>
        /// <param name="exp">判断条件</param>
        /// <returns>存在返回True，不存在返回False</returns>
        bool IsExists(Func<IMenu, bool> exp);

        /// <summary>
        /// 标记为删除
        /// </summary>
        /// <param name="model">标记为删除的菜单</param>
        /// <returns>操作成功返回True，失败返回False</returns>
        void MarkDelete(IMenu model);

        /// <summary>
        /// 批量标记为删除
        /// </summary>
        /// <param name="models">标记为删除的的一项列表</param>
        /// <returns>成功标记为删除的菜单数量</returns>
        void MarkDelete(IEnumerable<IMenu> models);

        /// <summary>
        /// 设置菜单启用状态
        /// </summary>
        /// <param name="model">设置启用状态的菜单</param>
        /// <param name="enableStatus">Ture设置菜单状态为启用，False设置菜单状态为未启用</param>
        void SetEnableStatus(IMenu model, bool enableStatus);

        /// <summary>
        /// 批量设置菜单启用状态
        /// </summary>
        /// <param name="models">设置启用状态的菜单列表</param>
        /// <param name="enableStatus">Ture设置菜单状态为启用，False设置菜单状态为未启用</param>
        void SetEnableStatus(IEnumerable<IMenu> models, bool enableStatus);
    }
}