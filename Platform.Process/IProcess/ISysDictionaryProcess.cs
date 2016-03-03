using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;

namespace Platform.Process.IProcess
{
    /// <summary>
    /// 系统自定义字典处理接口
    /// </summary>
    public interface ISysDictionaryProcess
    {
        /// <summary>
        /// 获取所有系统自定义词典
        /// </summary>
        /// <returns>所有系统自定义词典</returns>
        IEnumerable<ISysDictionary> GetAllModels();

        /// <summary>
        /// 获取指定系统自定义词典
        /// </summary>
        /// <param name="exp">查询条件表达式</param>
        /// <returns>符合查询条件的系统自定义词典</returns>
        IEnumerable<ISysDictionary> GetModels(Func<ISysDictionary, bool> exp);

        /// <summary>
        /// 获取符合条件的系统自定义词典的数量
        /// </summary>
        /// <param name="exp">查询条件</param>
        /// <returns>符合条件的系统自定义词典的数量</returns>
        int GetCount(Func<ISysDictionary, bool> exp);

        /// <summary>
        /// 新建默认数据系统自定义词典
        /// </summary>
        /// <returns>默认数据系统自定义词典</returns>
        ISysDictionary CreateDefaultModel();

        /// <summary>
        /// 从JSON字符串解析系统自定义词典
        /// </summary>
        /// <param name="jsonString">包含系统自定义词典信息的JSON字符串</param>
        /// <returns>解析后的系统自定义词典</returns>
        ISysDictionary ParseModel(string jsonString);

        /// <summary>
        /// 添加或修改系统自定义词典
        /// </summary>
        /// <param name="model">被添加或修改的系统自定义词典</param>
        /// <returns>操作成功返回True，失败返回False</returns>
        Guid AddOrUpdate(ISysDictionary model);

        /// <summary>
        /// 批量添加系统自定义词典
        /// </summary>
        /// <param name="models">被添加或修改的系统自定义词典列表</param>
        /// <returns>成功添加或修改的系统自定义词典数量</returns>
        int AddOrUpdate(IEnumerable<ISysDictionary> models);

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="model">被删除的对象</param>
        void Delete(ISysDictionary model);

        /// <summary>
        /// 批量删除系统自定义词典
        /// </summary>
        /// <param name="models">被删除的系统自定义词典列表</param>
        /// <returns>成功删除的系统自定义词典数量</returns>
        int Delete(IEnumerable<ISysDictionary> models);

        /// <summary>
        /// 判断系统自定义词典是否存在
        /// </summary>
        /// <param name="model">被判断的系统自定义词典</param>
        /// <returns>存在返回True，不存在返回False</returns>
        bool IsExists(ISysDictionary model);

        /// <summary>
        /// 判断系统自定义词典是否存在
        /// </summary>
        /// <param name="exp">判断条件</param>
        /// <returns>存在返回True，不存在返回False</returns>
        bool IsExists(Func<ISysDictionary, bool> exp);

        /// <summary>
        /// 标记为删除
        /// </summary>
        /// <param name="model">标记为删除的系统自定义词典</param>
        /// <returns>操作成功返回True，失败返回False</returns>
        void MarkDelete(ISysDictionary model);

        /// <summary>
        /// 批量标记为删除
        /// </summary>
        /// <param name="models">标记为删除的的一项列表</param>
        /// <returns>成功标记为删除的系统自定义词典数量</returns>
        void MarkDelete(IEnumerable<ISysDictionary> models);

        /// <summary>
        /// 设置系统自定义词典启用状态
        /// </summary>
        /// <param name="model">设置启用状态的系统自定义词典</param>
        /// <param name="enableStatus">Ture设置系统自定义词典状态为启用，False设置系统自定义词典状态为未启用</param>
        void SetEnableStatus(ISysDictionary model, bool enableStatus);

        /// <summary>
        /// 批量设置系统自定义词典启用状态
        /// </summary>
        /// <param name="models">设置启用状态的系统自定义词典列表</param>
        /// <param name="enableStatus">Ture设置系统自定义词典状态为启用，False设置系统自定义词典状态为未启用</param>
        void SetEnableStatus(IEnumerable<ISysDictionary> models, bool enableStatus);
    }
}