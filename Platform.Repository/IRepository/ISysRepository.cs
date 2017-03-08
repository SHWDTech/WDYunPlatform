using System;
using SHWDTech.Platform.Model.IModel;
using System.Collections.Generic;

namespace SHWD.Platform.Repository.IRepository
{
    /// <summary>
    /// 系统数据仓库基接口
    /// </summary>
    /// <typeparam name="T">数据仓库对应的模型类型，必须继承自ISysModel</typeparam>
    public interface ISysRepository<T> : IRepository<T> where T : class, ISysModel
    {
        /// <summary>
        /// 标记为删除
        /// </summary>
        /// <param name="model">标记为删除的对象</param>
        /// <returns>操作成功返回True，失败返回False</returns>
        void MarkDelete(T model);

        /// <summary>
        /// 批量标记为删除
        /// </summary>
        /// <param name="models">标记为删除的的一项列表</param>
        /// <returns>成功标记为删除的对象数量</returns>
        void MarkDelete(IEnumerable<T> models);

        /// <summary>
        /// 设置对象启用状态
        /// </summary>
        /// <param name="model">设置启用状态的对象</param>
        /// <param name="enableStatus">Ture设置对象状态为启用，False设置对象状态为未启用</param>
        void SetEnableStatus(T model, bool enableStatus);

        /// <summary>
        /// 批量设置对象启用状态
        /// </summary>
        /// <param name="models">设置启用状态的对象列表</param>
        /// <param name="enableStatus">Ture设置对象状态为启用，False设置对象状态为未启用</param>
        void SetEnableStatus(IEnumerable<T> models, bool enableStatus);

        /// <summary>
        /// 获取指定ID的模型
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        T GetModelById(Guid guid);

        /// <summary>
        /// 获取包含指定导航属性的指定ID的模型
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        T GetModelIncludeById(Guid guid, List<string> includes);

        /// <summary>
        /// 添加或修改对象
        /// </summary>
        /// <param name="model">被添加或修改的对象</param>
        /// <returns>操作成功返回True，失败返回False</returns>
        Guid AddOrUpdateDoCommit(T model);

        /// <summary>
        /// 更新指定模型属性
        /// </summary>
        /// <param name="model"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        Guid PartialUpdateDoCommit(T model, List<string> propertyNames);

        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="model">被判断的对象</param>
        /// <returns>存在返回True，不存在返回False</returns>
        bool IsExists(T model);
    }
}