using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SHWD.Platform.Repository.Entities;

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
        IQueryable<T> GetAllModels();

        /// <summary>
        /// 获取所有模型对象的列表
        /// </summary>
        /// <returns>所有模型对象的列表</returns>
        IList<T> GetAllModelList();

        /// <summary>
        /// 获取指定模型对象
        /// </summary>
        /// <param name="exp">查询条件表达式</param>
        /// <returns>符合查询条件的对象</returns>
        IQueryable<T> GetModels(Expression<Func<T, bool>> exp);

        /// <summary>
        /// 获取指定模型对象的列表
        /// </summary>
        /// <param name="exp">查询条件表达式</param>
        /// <returns>符合查询条件的对象列表</returns>
        IList<T> GetModelList(Expression<Func<T, bool>> exp);

        /// <summary>
        /// 获取符合指定条件的模型
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        T GetModel(Expression<Func<T, bool>> exp);

        /// <summary>
        /// 获取指定ID的模型
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        T GetModelById(Guid guid);

        /// <summary>
        /// 获取包含指定导航属性的模型
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        T GetModelInclude(Expression<Func<T, bool>> exp, List<string> includes);

        /// <summary>
        /// 获取包含指定导航属性的指定ID的模型
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        T GetModelIncludeById(Guid guid, List<string> includes);

        /// <summary>
        /// 获取符合条件的对象的数量
        /// </summary>
        /// <param name="exp">查询条件</param>
        /// <returns>符合条件的对象的数量</returns>
        int GetCount(Expression<Func<T, bool>> exp);

        /// <summary>
        /// 新建默认数据模型对象
        /// </summary>
        /// <returns>默认数据模型对象</returns>
        //T CreateDefaultModelFromDataBase();

        /// <summary>
        /// 从JSON字符串解析对象
        /// </summary>
        /// <param name="jsonString">包含对象信息的JSON字符串</param>
        /// <returns>解析后的对象</returns>
        T ParseModel(string jsonString);

        /// <summary>
        /// 添加或修改对象
        /// </summary>
        /// <param name="model"></param>
        void AddOrUpdate(T model);

        /// <summary>
        /// 批量添加对象
        /// </summary>
        /// <param name="models"></param>
        void AddOrUpdate(IEnumerable<T> models);

        /// <summary>
        /// 添加或修改对象
        /// </summary>
        /// <param name="model">被添加或修改的对象</param>
        /// <returns>操作成功返回True，失败返回False</returns>
        Guid AddOrUpdateDoCommit(T model);

        /// <summary>
        /// 批量添加对象
        /// </summary>
        /// <param name="models">被添加或修改的对象列表</param>
        /// <returns>成功添加或修改的对象数量</returns>
        int AddOrUpdateDoCommit(IEnumerable<T> models);

        /// <summary>
        /// 更新指定模型属性
        /// </summary>
        /// <param name="model"></param>
        /// <param name="propertyNames"></param>
        void PartialUpdate(T model, List<string> propertyNames);

        /// <summary>
        /// 批量更新指定模型属性
        /// </summary>
        /// <param name="models"></param>
        /// <param name="propertyNames"></param>
        void PartialUpdate(List<T> models, List<string> propertyNames);

        /// <summary>
        /// 更新指定模型属性
        /// </summary>
        /// <param name="model"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        Guid PartialUpdateDoCommit(T model, List<string> propertyNames);

        /// <summary>
        /// 批量更新指定模型属性
        /// </summary>
        /// <param name="models"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        int PartialUpdateDoCommit(List<T> models, List<string> propertyNames);

        /// <summary>
        /// 大量添加数据
        /// </summary>
        /// <param name="models">被添加或修改的对象列表</param>
        void BulkInsert(IEnumerable<T> models);

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="model"></param>
        void Delete(T model);

        /// <summary>
        /// 批量删除对象
        /// </summary>
        /// <param name="models"></param>
        void Delete(IEnumerable<T> models);

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="model">被删除的对象</param>
        bool DeleteDoCommit(T model);

        /// <summary>
        /// 批量删除对象
        /// </summary>
        /// <param name="models">被删除的对象列表</param>
        /// <returns>成功删除的对象数量</returns>
        int DeleteDoCommit(IEnumerable<T> models);

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

    public interface IRepository
    {
        /// <summary>
        /// 数据仓库上下文
        /// </summary>
        RepositoryDbContext DbContext { get; set; }

        /// <summary>
        /// 初始化数据实体对象
        /// </summary>
        void InitEntitySet();
    }
}