using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;

namespace SHWD.Platform.Repository.IRepository
{
    /// <summary>
    /// 数据仓库基接口
    /// </summary>
    /// <typeparam name="T">数据仓库对应的模型类型，必须继承自IModel</typeparam>
    public interface IRepository<T> where T : class, IModel
    {
        /// <summary>
        /// 获取所有模型对象
        /// </summary>
        /// <returns>所有模型对象</returns>
        IEnumerable<T> GetAllModels();

        /// <summary>
        /// 获取指定模型对象
        /// </summary>
        /// <param name="exp">查询条件表达式</param>
        /// <returns>符合查询条件的对象</returns>
        IEnumerable<T> GetModels(Func<T, bool> exp);

        /// <summary>
        /// 获取符合条件的对象的数量
        /// </summary>
        /// <param name="exp">查询条件</param>
        /// <returns>符合条件的对象的数量</returns>
        int GetCount(Func<T, bool> exp);

        /// <summary>
        /// 新建默认数据模型对象
        /// </summary>
        /// <returns>默认数据模型对象</returns>
        T CreateDefaultModel();

        /// <summary>
        /// 从JSON字符串解析对象
        /// </summary>
        /// <param name="jsonString">包含对象信息的JSON字符串</param>
        /// <returns>解析后的对象</returns>
        T ParseModel(string jsonString);

        /// <summary>
        /// 添加或修改对象
        /// </summary>
        /// <param name="model">被添加或修改的对象</param>
        /// <returns>操作成功返回True，失败返回False</returns>
        Guid AddOrUpdate(T model);

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
        bool Delete(T model);

        /// <summary>
        /// 批量删除对象
        /// </summary>
        /// <param name="models">被删除的对象列表</param>
        /// <returns>成功删除的对象数量</returns>
        int Delete(IEnumerable<T> models);

        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="model">被判断的对象</param>
        /// <returns>存在返回True，不存在返回False</returns>
        bool IsExists(T model);

        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="exp">判断条件</param>
        /// <returns>存在返回True，不存在返回False</returns>
        bool IsExists(Func<T, bool> exp);
    }
}