using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using SHWDTech.Platform.StorageConstrains.Model;

namespace SHWDTech.Platform.StorageConstrains.Repository
{
    /// <summary>
    /// 数据仓库接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class, IModel, new()
    {
        /// <summary>
        /// 数据仓库数据上下文
        /// </summary>
        DbContext RepositoryDbContext { get; set; }

        /// <summary>
        /// 对应数据模型的数据集
        /// </summary>
        IQueryable<T> EntitySet { get; }

        /// <summary>
        /// 是否自动提交
        /// </summary>
        bool AutoCommit { get; set; }

        /// <summary>
        /// 获取数据集的第一个实例。
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        T First(Expression<Func<T, bool>> exp);

        /// <summary>
        /// 获取包含导航属性的第一个实例
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        T FirstInclude(Expression<Func<T, bool>> exp, string[] includes);

        /// <summary>
        /// 获取数据集的第一个实例的查询表达式。
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        IQueryable<T> FirstQueryable(Expression<Func<T, bool>>  exp);

        /// <summary>
        /// 获取包含导航属性的第一个实例的查询表达式。
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        IQueryable<T> FirstIncludeQueryable(Expression<Func<T, bool>> exp, string[] includes);

        /// <summary>
        /// 获取符合查询条件的数据集。
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        IList<T> Where(Expression<Func<T, bool>> exp);

        /// <summary>
        /// 获取包含导航属性的符合查询条件的数据集。
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        IList<T> WhereInclude(Expression<Func<T, bool>> exp, string[] includes);

        /// <summary>
        /// 获取符合查询条件的查询表达式。
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        IQueryable<T> WhereQueryable(Expression<Func<T, bool>> exp);

        /// <summary>
        /// 获取包含导航属性的符合查询条件的查询表达式。
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        IQueryable<T> WhereIncludeQueryable(Expression<Func<T, bool>> exp, string[] includes);

        /// <summary>
        /// 从JSON字符串解析对象
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        T ParseModel(string jsonString);

        /// <summary>
        /// 从JSON字符串解析对象集合
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        IList<T> ParseModelList(string jsonString);


    }
}
