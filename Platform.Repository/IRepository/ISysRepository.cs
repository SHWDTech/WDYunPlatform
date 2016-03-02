using System.Collections.Generic;
using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Repository.IRepository
{
    /// <summary>
    /// 系统数据仓库基接口
    /// </summary>
    /// <typeparam name="T">数据仓库对应的模型类型，必须继承自ISysModel</typeparam>
    public interface ISysRepository<T> : IRepository<T> where T: class, ISysModel
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
    }
}
