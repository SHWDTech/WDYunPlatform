using System.Collections.Generic;
using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Repository.IRepository
{
    /// <summary>
    /// 数据类型仓库基接口
    /// </summary>
    /// <typeparam name="T">数据仓库对应的模型类型，必须继承自IDataModel</typeparam>
    public interface IDataRepository<T> : IRepository<T> where T : class, IDataModel
    {
        /// <summary>
        /// 获取指定ID的模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetModelById(long id);

        /// <summary>
        /// 获取包含指定导航属性的指定ID的模型
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        T GetModelIncludeById(long id, List<string> includes);

        /// <summary>
        /// 添加或修改对象
        /// </summary>
        /// <param name="model">被添加或修改的对象</param>
        /// <returns>操作成功返回True，失败返回False</returns>
        long AddOrUpdateDoCommit(T model);

        /// <summary>
        /// 更新指定模型属性
        /// </summary>
        /// <param name="model"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        long PartialUpdateDoCommit(T model, List<string> propertyNames);

        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="model">被判断的对象</param>
        /// <returns>存在返回True，不存在返回False</returns>
        bool IsExists(T model);
    }
}