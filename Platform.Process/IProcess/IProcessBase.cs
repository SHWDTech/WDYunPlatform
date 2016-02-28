using System;
using System.Collections.Generic;

namespace SHWD.Platform.Process.IProcess
{
    public interface IProcessBase<T> where T: class 
    {
        /// <summary>
        /// 获取所有模型对象
        /// </summary>
        /// <returns>所有模型对象</returns>
        IEnumerable<T> GetModels();

        /// <summary>
        /// 获取指定模型对象
        /// </summary>
        /// <param name="exp">查询条件表达式</param>
        /// <returns>符合查询条件的对象</returns>
        IEnumerable<T> GetModels(Func<T, bool> exp);

        /// <summary>
        /// 添加或修改对象
        /// </summary>
        /// <param name="model">被添加或修改的对象</param>
        /// <returns>操作成功返回True，失败返回False</returns>
        bool AddOrUpdate(T model);

        /// <summary>
        /// 批量添加对象
        /// </summary>
        /// <param name="models">被添加或修改的对象列表</param>
        /// <returns>成功添加或修改的对象数量</returns>
        int AddOrUpdate(IEnumerable<T> models);

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="model">被删除的对象</param>
        /// <returns>操作成功返回True，失败返回False</returns>
        bool Delete(T model);

        /// <summary>
        /// 批量删除对象
        /// </summary>
        /// <param name="models">被删除的对象列表</param>
        /// <returns>成功删除的对象数量</returns>
        int Delete(IEnumerable<T> models);
    }
}
