using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;

namespace Platform.Process.IProcess
{
    /// <summary>
    /// 用具配置处理接口
    /// </summary>
    public interface IUserConfigConfigProcess
    {
        /// <summary>
        /// 获取所有用户配置
        /// </summary>
        /// <returns>所有用户配置</returns>
        IEnumerable<IUserConfig> GetAllModels();

        /// <summary>
        /// 获取指定用户配置
        /// </summary>
        /// <param name="exp">查询条件表达式</param>
        /// <returns>符合查询条件的用户配置</returns>
        IEnumerable<IUserConfig> GetModels(Func<IUserConfig, bool> exp);

        /// <summary>
        /// 获取符合条件的用户配置的数量
        /// </summary>
        /// <param name="exp">查询条件</param>
        /// <returns>符合条件的用户配置的数量</returns>
        int GetCount(Func<IUserConfig, bool> exp);

        /// <summary>
        /// 新建默认数据用户配置
        /// </summary>
        /// <returns>默认数据用户配置</returns>
        IUserConfig CreateDefaultModel();

        /// <summary>
        /// 从JSON字符串解析用户配置
        /// </summary>
        /// <param name="jsonString">包含用户配置信息的JSON字符串</param>
        /// <returns>解析后的用户配置</returns>
        IUserConfig ParseModel(string jsonString);

        /// <summary>
        /// 添加或修改用户配置
        /// </summary>
        /// <param name="model">被添加或修改的用户配置</param>
        /// <returns>操作成功返回True，失败返回False</returns>
        Guid AddOrUpdate(IUserConfig model);

        /// <summary>
        /// 批量添加用户配置
        /// </summary>
        /// <param name="models">被添加或修改的用户配置列表</param>
        /// <returns>成功添加或修改的用户配置数量</returns>
        int AddOrUpdate(IEnumerable<IUserConfig> models);

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="model">被删除的对象</param>
        void Delete(IUserConfig model);

        /// <summary>
        /// 批量删除用户配置
        /// </summary>
        /// <param name="models">被删除的用户配置列表</param>
        /// <returns>成功删除的用户配置数量</returns>
        int Delete(IEnumerable<IUserConfig> models);

        /// <summary>
        /// 判断用户配置是否存在
        /// </summary>
        /// <param name="model">被判断的用户配置</param>
        /// <returns>存在返回True，不存在返回False</returns>
        bool IsExists(IUserConfig model);

        /// <summary>
        /// 判断用户配置是否存在
        /// </summary>
        /// <param name="exp">判断条件</param>
        /// <returns>存在返回True，不存在返回False</returns>
        bool IsExists(Func<IUserConfig, bool> exp);

        /// <summary>
        /// 标记为删除
        /// </summary>
        /// <param name="model">标记为删除的用户配置</param>
        /// <returns>操作成功返回True，失败返回False</returns>
        void MarkDelete(IUserConfig model);

        /// <summary>
        /// 批量标记为删除
        /// </summary>
        /// <param name="models">标记为删除的的一项列表</param>
        /// <returns>成功标记为删除的用户配置数量</returns>
        void MarkDelete(IEnumerable<IUserConfig> models);

        /// <summary>
        /// 设置用户配置启用状态
        /// </summary>
        /// <param name="model">设置启用状态的用户配置</param>
        /// <param name="enableStatus">Ture设置用户配置状态为启用，False设置用户配置状态为未启用</param>
        void SetEnableStatus(IUserConfig model, bool enableStatus);

        /// <summary>
        /// 批量设置用户配置启用状态
        /// </summary>
        /// <param name="models">设置启用状态的用户配置列表</param>
        /// <param name="enableStatus">Ture设置用户配置状态为启用，False设置用户配置状态为未启用</param>
        void SetEnableStatus(IEnumerable<IUserConfig> models, bool enableStatus);
    }
}