using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;

namespace Platform.Process.IProcess
{
    /// <summary>
    /// 项目处理接口
    /// </summary>
    public interface IProjectProcess
    {
        /// <summary>
        /// 获取所有项目
        /// </summary>
        /// <returns>所有项目</returns>
        IEnumerable<IProject> GetAllModels();

        /// <summary>
        /// 获取指定项目
        /// </summary>
        /// <param name="exp">查询条件表达式</param>
        /// <returns>符合查询条件的项目</returns>
        IEnumerable<IProject> GetModels(Func<IProject, bool> exp);

        /// <summary>
        /// 获取符合条件的项目的数量
        /// </summary>
        /// <param name="exp">查询条件</param>
        /// <returns>符合条件的项目的数量</returns>
        int GetCount(Func<IProject, bool> exp);

        /// <summary>
        /// 新建默认数据项目
        /// </summary>
        /// <returns>默认数据项目</returns>
        IProject CreateDefaultModel();

        /// <summary>
        /// 从JSON字符串解析项目
        /// </summary>
        /// <param name="jsonString">包含项目信息的JSON字符串</param>
        /// <returns>解析后的项目</returns>
        IProject ParseModel(string jsonString);

        /// <summary>
        /// 添加或修改项目
        /// </summary>
        /// <param name="model">被添加或修改的项目</param>
        /// <returns>操作成功返回True，失败返回False</returns>
        Guid AddOrUpdate(IProject model);

        /// <summary>
        /// 批量添加项目
        /// </summary>
        /// <param name="models">被添加或修改的项目列表</param>
        /// <returns>成功添加或修改的项目数量</returns>
        int AddOrUpdate(IEnumerable<IProject> models);

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="model">被删除的对象</param>
        void Delete(IProject model);

        /// <summary>
        /// 批量删除项目
        /// </summary>
        /// <param name="models">被删除的项目列表</param>
        /// <returns>成功删除的项目数量</returns>
        int Delete(IEnumerable<IProject> models);

        /// <summary>
        /// 判断项目是否存在
        /// </summary>
        /// <param name="model">被判断的项目</param>
        /// <returns>存在返回True，不存在返回False</returns>
        bool IsExists(IProject model);

        /// <summary>
        /// 判断项目是否存在
        /// </summary>
        /// <param name="exp">判断条件</param>
        /// <returns>存在返回True，不存在返回False</returns>
        bool IsExists(Func<IProject, bool> exp);

        /// <summary>
        /// 标记为删除
        /// </summary>
        /// <param name="model">标记为删除的项目</param>
        /// <returns>操作成功返回True，失败返回False</returns>
        void MarkDelete(IProject model);

        /// <summary>
        /// 批量标记为删除
        /// </summary>
        /// <param name="models">标记为删除的的一项列表</param>
        /// <returns>成功标记为删除的项目数量</returns>
        void MarkDelete(IEnumerable<IProject> models);

        /// <summary>
        /// 设置项目启用状态
        /// </summary>
        /// <param name="model">设置启用状态的项目</param>
        /// <param name="enableStatus">Ture设置项目状态为启用，False设置项目状态为未启用</param>
        void SetEnableStatus(IProject model, bool enableStatus);

        /// <summary>
        /// 批量设置项目启用状态
        /// </summary>
        /// <param name="models">设置启用状态的项目列表</param>
        /// <param name="enableStatus">Ture设置项目状态为启用，False设置项目状态为未启用</param>
        void SetEnableStatus(IEnumerable<IProject> models, bool enableStatus);
    }
}