using Newtonsoft.Json;
using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using EntityFramework.BulkInsert.Extensions;
using System.Transactions;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Utility;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 数据仓库泛型基类
    /// </summary>
    /// <typeparam name="T">数据仓库对应的模型类型，必须继承自IModel</typeparam>
    public class Repository<T> : RepositoryBase, IDisposable, IRepository<T> where T : class, IModel, new()
    {
        /// <summary>
        /// 数据库上下文
        /// </summary>
        protected RepositoryDbContext DbContext { get; }

        /// <summary>
        /// 进行操作的数据实体
        /// </summary>
        protected IQueryable<T> EntitySet { get; set; }

        /// <summary>
        /// 数据检查条件
        /// </summary>
        protected Expression<Func<T, bool>> CheckFunc { get; set; }

        /// <summary>
        /// 创建一个新的数据仓库泛型基类对象
        /// </summary>
        protected Repository()
        {
            if (DbContext == null)
            {
                DbContext = string.IsNullOrWhiteSpace(DbRepository.ConnectionString)
                    ? new RepositoryDbContext()
                    : new RepositoryDbContext(DbRepository.ConnectionString);
            }
            EntitySet = CheckFunc == null ? DbContext.Set<T>() : DbContext.Set<T>().Where(CheckFunc);
        }

        protected Repository(string connString) : this()
        {
            DbContext = new RepositoryDbContext(connString);
        }

        protected Repository(RepositoryDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual IQueryable<T> GetAllModels()
            => EntitySet;

        public virtual IList<T> GetAllModelList()
            => GetAllModels().ToList();

        public virtual IQueryable<T> GetModels(Expression<Func<T, bool>> exp)
            => EntitySet.Where(exp);

        public virtual IList<T> GetModelList(Expression<Func<T, bool>> exp)
            => GetModels(exp).ToList();

        public virtual T GetModel(Expression<Func<T, bool>> exp)
            => EntitySet.SingleOrDefault(exp);

        public virtual T GetModelIncludeById(Guid guid, List<string> includes)
        {
            var query = includes.Aggregate(EntitySet, (current, include) => current.Include(include));

            return query.SingleOrDefault(obj => obj.Id == guid);
        }

        public virtual T GetModelInclude(Expression<Func<T, bool>> exp, List<string> includes)
        {
            var query = includes.Aggregate(EntitySet, (current, include) => current.Include(include));

            return query.SingleOrDefault(exp);
        }

        public virtual T GetModelById(Guid guid)
            => EntitySet.SingleOrDefault(obj => obj.Id == guid);

        public virtual int GetCount(Expression<Func<T, bool>> exp) 
            => exp == null ? EntitySet.Count() : EntitySet.Where(exp).Count();

        /// <summary>
        /// 创建默认数据模型
        /// </summary>
        /// <returns></returns>
        public static T CreateDefaultModel()
        {
            var model = new T
            {
                Id = Globals.NewCombId(),
                ModelState = ModelState.Added
            };

            return model;
        }

        public virtual T ParseModel(string jsonString)
        {
            var model = JsonConvert.DeserializeObject<T>(jsonString);
            model.ModelState = ModelState.Added;

            return model;
        }

        public virtual Guid AddOrUpdate(T model)
        {
            CheckModel(model);

            if (model.IsNew)
            {
                DbContext.Set<T>().Add(model);
            }
            else
            {
                DbContext.Set<T>().Attach(model);
                DbContext.Entry(model).State = EntityState.Modified;
            }

            return DbContext.SaveChanges() != 1 ? Guid.Empty : model.Id;
        }

        public virtual int AddOrUpdate(IEnumerable<T> models)
        {
            CheckModel(models);

            foreach (var model in models.Where(model => model.IsNew))
            {
                DbContext.Set<T>().Add(model);
            }

            return DbContext.SaveChanges();
        }

        public virtual Guid PartialUpdate(T model, List<string> propertyNames)
        {
            DbContext.Set<T>().Attach(model);
            foreach (var propertyName in propertyNames)
            {
                DbContext.Entry(model).Property(propertyName).IsModified = true;
            }

            return DbContext.SaveChanges() != 1 ? Guid.Empty : model.Id;
        }

        public virtual int PartialUpdate(IEnumerable<T> models, List<string> propertyNames)
        {
            foreach (var model in models)
            {
                DbContext.Set<T>().Attach(model);
                foreach (var propertyName in propertyNames)
                {
                    DbContext.Entry(model).Property(propertyName).IsModified = true;
                }
            }

            return DbContext.SaveChanges();
        }

        public virtual void BulkInsert(IEnumerable<T> models)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    DbContext.BulkInsert(models);

                }
                catch (System.Exception ex)
                {
                    LogService.Instance.Debug("", ex);
                    return;
                }
                DbContext.SaveChanges();
                scope.Complete();
            }
        }

        public virtual bool Delete(T model)
        {
            CheckModel(model);

            DbContext.Set<T>().Remove(model);

            return DbContext.SaveChanges() == 1;
        }

        public virtual int Delete(IEnumerable<T> models)
        {
            CheckModel(models);

            foreach (var model in models)
            {
                DbContext.Set<T>().Remove(model);
            }

            return DbContext.SaveChanges();
        }

        public virtual bool IsExists(T model) => EntitySet.Any(obj => obj.Id == model.Id);

        public virtual bool IsExists(Func<T, bool> exp) => EntitySet.Any(exp);

        /// <summary>
        /// 检查模型是否符合要求
        /// </summary>
        /// <param name="models"></param>
        private void CheckModel(object models)
        {
            if (CheckFunc == null) return;

            if (models == null) throw new ArgumentNullException(nameof(models));

            var checkList = new List<T>();
            var item = models as T;
            if (item != null) checkList.Add(item);

            var items = models as IEnumerable<T>;
            if (items != null) checkList.AddRange(items);

            if (!checkList.Any(CheckFunc.Compile())) throw new ArgumentException("参数不符合要求");
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }

    /// <summary>
    /// 数据仓库基类
    /// </summary>
    public class RepositoryBase
    {
        /// <summary>
        /// 初始化数据仓库基类
        /// </summary>
        protected RepositoryBase()
        {
        }

        /// <summary>
        /// 数据仓库上下文
        /// </summary>
        public static IRepositoryContext RepositoryContext => ContextLocal == null ? ContextGlobal : ContextLocal.Value;

        /// <summary>
        /// 当前线程的用户
        /// </summary>
        public static IWdUser CurrentUser => RepositoryContext.CurrentUser;

        /// <summary>
        /// 当前线程用户所属域
        /// </summary>
        public static IDomain CurrentDomain => RepositoryContext.CurrentDomain;

        /// <summary>
        /// 数据仓库上下文线程对象
        /// </summary>
        public static ThreadLocal<IRepositoryContext> ContextLocal { get; set; }

        /// <summary>
        /// 全局数据仓库上下文线程对象
        /// </summary>
        public static IRepositoryContext ContextGlobal { get; set; }

        public static RepositoryDbContext BaseContext = string.IsNullOrWhiteSpace(DbRepository.ConnectionString)
                                                        ? new RepositoryDbContext()
                                                        : new RepositoryDbContext(DbRepository.ConnectionString);
    }
}