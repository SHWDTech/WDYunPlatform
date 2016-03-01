using System.Collections.Generic;
using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Repository.IRepository
{
    public interface ISysRepository<T> : IRepository<T> where T: class, ISysModel
    {
        /// <summary>
        /// 标记为删除
        /// </summary>
        /// <param name="model">标记为删除的模型</param>
        /// <returns>操作成功返回True，失败返回False</returns>
        bool MarkDelete(T model);

        /// <summary>
        /// 批量标记为删除
        /// </summary>
        /// <param name="models">标记为删除的模型列表</param>
        /// <returns>成功标记为删除的对象数量</returns>
        int MarkDelete(IEnumerable<T> models);
    }
}
