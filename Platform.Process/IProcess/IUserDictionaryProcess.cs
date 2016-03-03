using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;

namespace Platform.Process.IProcess
{
    /// <summary>
    /// 用户自定义字典处理接口
    /// </summary>
    public interface IUserDictionaryDictionaryProcess
    {
        /// <summary>
        /// 获取所有用户自定义词典
        /// </summary>
        /// <returns>所有用户自定义词典</returns>
        IEnumerable<IUserDictionary> GetAllModels();

        /// <summary>
        /// 获取指定用户自定义词典
        /// </summary>
        /// <param name="exp">查询条件表达式</param>
        /// <returns>符合查询条件的用户自定义词典</returns>
        IEnumerable<IUserDictionary> GetModels(Func<IUserDictionary, bool> exp);

        /// <summary>
        /// 获取符合条件的用户自定义词典的数量
        /// </summary>
        /// <param name="exp">查询条件</param>
        /// <returns>符合条件的用户自定义词典的数量</returns>
        int GetCount(Func<IUserDictionary, bool> exp);

        /// <summary>
        /// 新建默认数据用户自定义词典
        /// </summary>
        /// <returns>默认数据用户自定义词典</returns>
        IUserDictionary CreateDefaultModel();

        /// <summary>
        /// 从JSON字符串解析用户自定义词典
        /// </summary>
        /// <param name="jsonString">包含用户自定义词典信息的JSON字符串</param>
        /// <returns>解析后的用户自定义词典</returns>
        IUserDictionary ParseModel(string jsonString);

        /// <summary>
        /// 添加或修改用户自定义词典
        /// </summary>
        /// <param name="model">被添加或修改的用户自定义词典</param>
        /// <returns>操作成功返回True，失败返回False</returns>
        Guid AddOrUpdate(IUserDictionary model);

        /// <summary>
        /// 批量添加用户自定义词典
        /// </summary>
        /// <param name="models">被添加或修改的用户自定义词典列表</param>
        /// <returns>成功添加或修改的用户自定义词典数量</returns>
        int AddOrUpdate(IEnumerable<IUserDictionary> models);

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="model">被删除的对象</param>
        void Delete(IUserDictionary model);

        /// <summary>
        /// 批量删除用户自定义词典
        /// </summary>
        /// <param name="models">被删除的用户自定义词典列表</param>
        /// <returns>成功删除的用户自定义词典数量</returns>
        int Delete(IEnumerable<IUserDictionary> models);

        /// <summary>
        /// 判断用户自定义词典是否存在
        /// </summary>
        /// <param name="model">被判断的用户自定义词典</param>
        /// <returns>存在返回True，不存在返回False</returns>
        bool IsExists(IUserDictionary model);

        /// <summary>
        /// 判断用户自定义词典是否存在
        /// </summary>
        /// <param name="exp">判断条件</param>
        /// <returns>存在返回True，不存在返回False</returns>
        bool IsExists(Func<IUserDictionary, bool> exp);

        /// <summary>
        /// 标记为删除
        /// </summary>
        /// <param name="model">标记为删除的用户自定义词典</param>
        /// <returns>操作成功返回True，失败返回False</returns>
        void MarkDelete(IUserDictionary model);

        /// <summary>
        /// 批量标记为删除
        /// </summary>
        /// <param name="models">标记为删除的的一项列表</param>
        /// <returns>成功标记为删除的用户自定义词典数量</returns>
        void MarkDelete(IEnumerable<IUserDictionary> models);

        /// <summary>
        /// 设置用户自定义词典启用状态
        /// </summary>
        /// <param name="model">设置启用状态的用户自定义词典</param>
        /// <param name="enableStatus">Ture设置用户自定义词典状态为启用，False设置用户自定义词典状态为未启用</param>
        void SetEnableStatus(IUserDictionary model, bool enableStatus);

        /// <summary>
        /// 批量设置用户自定义词典启用状态
        /// </summary>
        /// <param name="models">设置启用状态的用户自定义词典列表</param>
        /// <param name="enableStatus">Ture设置用户自定义词典状态为启用，False设置用户自定义词典状态为未启用</param>
        void SetEnableStatus(IEnumerable<IUserDictionary> models, bool enableStatus);
    }
}