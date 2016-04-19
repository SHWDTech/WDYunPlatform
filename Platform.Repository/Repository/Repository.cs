﻿using Newtonsoft.Json;
using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 数据仓库泛型基类
    /// </summary>
    /// <typeparam name="T">数据仓库对应的模型类型，必须继承自IModel</typeparam>
    public class Repository<T> : RepositoryBase, IRepository<T> where T : class, IModel
    {
        /// <summary>
        /// 数据库上下文
        /// </summary>
        protected RepositoryDbContext DbContext { get; }

        /// <summary>
        /// 进行操作的数据实体
        /// </summary>
        protected IEnumerable<T> EntitySet { get; set; }

        /// <summary>
        /// 数据检查条件
        /// </summary>
        protected Func<T, bool> CheckFunc { get; set; }

        /// <summary>
        /// 创建一个新的数据仓库泛型基类对象
        /// </summary>
        protected Repository()
        {
            DbContext = new RepositoryDbContext();
            EntitySet = CheckFunc == null ? DbContext.Set<T>() : DbContext.Set<T>().Where(CheckFunc);
        }

        public virtual IEnumerable<T> GetAllModels()
            => EntitySet;

        public virtual IList<T> GetAllModelList()
            => GetAllModels().ToList();

        public virtual IEnumerable<T> GetModels(Func<T, bool> exp)
            => EntitySet.Where(exp);

        public virtual IList<T> GetModelList(Func<T, bool> exp) 
            => GetModels(exp).ToList();

        public virtual int GetCount(Func<T, bool> exp) 
            => EntitySet.Where(exp).Count();

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
            CheckModel(model);

            if (!IsExists(model))
            {
                DbContext.Set<T>().Add(model);
            }

            return DbContext.SaveChanges() != 1 ? Guid.Empty : model.Id;
        }

        public virtual int AddOrUpdate(IEnumerable<T> models)
        {
            CheckModel(models);

            foreach (var model in models.Where(model => !IsExists(model)))
            {
                DbContext.Set<T>().Add(model);
            }

            return DbContext.SaveChanges();
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

            if (!checkList.Any(CheckFunc)) throw new ArgumentException("参数不符合要求");
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
    }
}