using Newtonsoft.Json;
using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using SHWDTech.Platform.Model.Enums;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 数据仓库泛型基类
    /// </summary>
    /// <typeparam name="T">数据仓库对应的模型类型，必须继承自IModel</typeparam>
    public class Repository<T> : RepositoryBase, IRepository<T> where T : class, IModel
    {
        private RepositoryDbContext DbContext { get; }
        /// <summary>
        /// 创建一个新的数据仓库泛型基类对象
        /// </summary>
        protected Repository()
        {
            DbContext = new RepositoryDbContext();
        }

        public virtual IEnumerable<T> GetAllModels() => DbContext.Set<T>().ToList();

        public virtual IEnumerable<T> GetModels(Func<T, bool> exp) => DbContext.Set<T>().Where(exp).ToList();

        public virtual int GetCount(Func<T, bool> exp) => DbContext.Set<T>().Where(exp).Count();

        public virtual T CreateDefaultModel()
        {
            var model = DbContext.Set<T>().Create();
            model.ModelState = ModelState.Added;

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
            if (!IsExists(model))
            {
                DbContext.Set<T>().Add(model);
            }

            return DbContext.SaveChanges() != 1 ? Guid.Empty : model.Id;
        }

        public virtual int AddOrUpdate(IEnumerable<T> models)
        {
            foreach (var model in models)
            {
                if (!IsExists(model))
                {
                    DbContext.Set<T>().Add(model);
                }
            }

            return DbContext.SaveChanges();
        }

        public virtual bool Delete(T model)
        {
            DbContext.Set<T>().Remove(model);

            return DbContext.SaveChanges() == 1;
        }

        public virtual int Delete(IEnumerable<T> models)
        {
            foreach (var model in models)
            {
                DbContext.Set<T>().Remove(model);
            }

            return DbContext.SaveChanges();
        }

        public virtual bool IsExists(T model) => DbContext.Set<T>().Find(model.Id) != null;

        public virtual bool IsExists(Func<T, bool> exp) => DbContext.Set<T>().Find(exp) != null;

        void IRepository<T>.Delete(T model)
        {
            DbContext.Set<T>().Remove(model);

            DbContext.SaveChanges();
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

        public static ThreadLocal<IRepositoryContext> ContextLocal { get; set; }
    }
}