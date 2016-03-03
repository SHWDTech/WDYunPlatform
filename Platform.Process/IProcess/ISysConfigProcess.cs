using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;

namespace Platform.Process.IProcess
{
    public interface ISysConfigProcess
    {
        /// <summary>
        /// 获取所有系统配置
        /// </summary>
        /// <returns>所有系统配置</returns>
        IEnumerable<ISysConfig> GetAllModels();

        /// <summary>
        /// 获取指定系统配置
        /// </summary>
        /// <param name="exp">查询条件表达式</param>
        /// <returns>符合查询条件的系统配置</returns>
        IEnumerable<ISysConfig> GetModels(Func<ISysConfig, bool> exp);

        /// <summary>
        /// 获取符合条件的系统配置的数量
        /// </summary>
        /// <param name="exp">查询条件</param>
        /// <returns>符合条件的系统配置的数量</returns>
        int GetCount(Func<ISysConfig, bool> exp);

        /// <summary>
        /// 新建默认数据系统配置
        /// </summary>
        /// <returns>默认数据系统配置</returns>
        ISysConfig CreateDefaultModel();

        /// <summary>
        /// 从JSON字符串解析系统配置
        /// </summary>
        /// <param name="jsonString">包含系统配置信息的JSON字符串</param>
        /// <returns>解析后的系统配置</returns>
        ISysConfig ParseModel(string jsonString);

        /// <summary>
        /// 添加或修改系统配置
        /// </summary>
        /// <param name="model">被添加或修改的系统配置</param>
        /// <returns>操作成功返回True，失败返回False</returns>
        Guid AddOrUpdate(ISysConfig model);

        /// <summary>
        /// 批量添加系统配置
        /// </summary>
        /// <param name="models">被添加或修改的系统配置列表</param>
        /// <returns>成功添加或修改的系统配置数量</returns>
        int AddOrUpdate(IEnumerable<ISysConfig> models);

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="model">被删除的对象</param>
        void Delete(ISysConfig model);

        /// <summary>
        /// 批量删除系统配置
        /// </summary>
        /// <param name="models">被删除的系统配置列表</param>
        /// <returns>成功删除的系统配置数量</returns>
        int Delete(IEnumerable<ISysConfig> models);

        /// <summary>
        /// 判断系统配置是否存在
        /// </summary>
        /// <param name="model">被判断的系统配置</param>
        /// <returns>存在返回True，不存在返回False</returns>
        bool IsExists(ISysConfig model);

        /// <summary>
        /// 判断系统配置是否存在
        /// </summary>
        /// <param name="exp">判断条件</param>
        /// <returns>存在返回True，不存在返回False</returns>
        bool IsExists(Func<ISysConfig, bool> exp);

        /// <summary>
        /// 标记为删除
        /// </summary>
        /// <param name="model">标记为删除的系统配置</param>
        /// <returns>操作成功返回True，失败返回False</returns>
        void MarkDelete(ISysConfig model);

        /// <summary>
        /// 批量标记为删除
        /// </summary>
        /// <param name="models">标记为删除的的一项列表</param>
        /// <returns>成功标记为删除的系统配置数量</returns>
        void MarkDelete(IEnumerable<ISysConfig> models);

        /// <summary>
        /// 设置系统配置启用状态
        /// </summary>
        /// <param name="model">设置启用状态的系统配置</param>
        /// <param name="enableStatus">Ture设置系统配置状态为启用，False设置系统配置状态为未启用</param>
        void SetEnableStatus(ISysConfig model, bool enableStatus);

        /// <summary>
        /// 批量设置系统配置启用状态
        /// </summary>
        /// <param name="models">设置启用状态的系统配置列表</param>
        /// <param name="enableStatus">Ture设置系统配置状态为启用，False设置系统配置状态为未启用</param>
        void SetEnableStatus(IEnumerable<ISysConfig> models, bool enableStatus);
    }
}