using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;

namespace Platform.Process.IProcess
{
    public interface ITaskProcess
    {
        /// <summary>
        /// 获取所有通讯任务
        /// </summary>
        /// <returns>所有通讯任务</returns>
        IEnumerable<ITask> GetAllModels();

        /// <summary>
        /// 获取指定通讯任务
        /// </summary>
        /// <param name="exp">查询条件表达式</param>
        /// <returns>符合查询条件的通讯任务</returns>
        IEnumerable<ITask> GetModels(Func<ITask, bool> exp);

        /// <summary>
        /// 获取符合条件的通讯任务的数量
        /// </summary>
        /// <param name="exp">查询条件</param>
        /// <returns>符合条件的通讯任务的数量</returns>
        int GetCount(Func<ITask, bool> exp);

        /// <summary>
        /// 新建默认数据通讯任务
        /// </summary>
        /// <returns>默认数据通讯任务</returns>
        ITask CreateDefaultModel();

        /// <summary>
        /// 从JSON字符串解析通讯任务
        /// </summary>
        /// <param name="jsonString">包含通讯任务信息的JSON字符串</param>
        /// <returns>解析后的通讯任务</returns>
        ITask ParseModel(string jsonString);

        /// <summary>
        /// 添加或修改通讯任务
        /// </summary>
        /// <param name="model">被添加或修改的通讯任务</param>
        /// <returns>操作成功返回True，失败返回False</returns>
        Guid AddOrUpdate(ITask model);

        /// <summary>
        /// 批量添加通讯任务
        /// </summary>
        /// <param name="models">被添加或修改的通讯任务列表</param>
        /// <returns>成功添加或修改的通讯任务数量</returns>
        int AddOrUpdate(IEnumerable<ITask> models);

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="model">被删除的对象</param>
        void Delete(ITask model);

        /// <summary>
        /// 批量删除通讯任务
        /// </summary>
        /// <param name="models">被删除的通讯任务列表</param>
        /// <returns>成功删除的通讯任务数量</returns>
        int Delete(IEnumerable<ITask> models);

        /// <summary>
        /// 判断通讯任务是否存在
        /// </summary>
        /// <param name="model">被判断的通讯任务</param>
        /// <returns>存在返回True，不存在返回False</returns>
        bool IsExists(ITask model);

        /// <summary>
        /// 判断通讯任务是否存在
        /// </summary>
        /// <param name="exp">判断条件</param>
        /// <returns>存在返回True，不存在返回False</returns>
        bool IsExists(Func<ITask, bool> exp);

        /// <summary>
        /// 标记为删除
        /// </summary>
        /// <param name="model">标记为删除的通讯任务</param>
        /// <returns>操作成功返回True，失败返回False</returns>
        void MarkDelete(ITask model);

        /// <summary>
        /// 批量标记为删除
        /// </summary>
        /// <param name="models">标记为删除的的一项列表</param>
        /// <returns>成功标记为删除的通讯任务数量</returns>
        void MarkDelete(IEnumerable<ITask> models);

        /// <summary>
        /// 设置通讯任务启用状态
        /// </summary>
        /// <param name="model">设置启用状态的通讯任务</param>
        /// <param name="enableStatus">Ture设置通讯任务状态为启用，False设置通讯任务状态为未启用</param>
        void SetEnableStatus(ITask model, bool enableStatus);

        /// <summary>
        /// 批量设置通讯任务启用状态
        /// </summary>
        /// <param name="models">设置启用状态的通讯任务列表</param>
        /// <param name="enableStatus">Ture设置通讯任务状态为启用，False设置通讯任务状态为未启用</param>
        void SetEnableStatus(IEnumerable<ITask> models, bool enableStatus);
    }
}