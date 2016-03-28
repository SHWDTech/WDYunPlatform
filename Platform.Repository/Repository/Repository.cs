﻿using Newtonsoft.Json;
using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.Model;
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
            EntitySet = DbContext.Set<T>();
        }

        public virtual IEnumerable<T> GetAllModels()
            => CheckFunc == null ? EntitySet : EntitySet.Where(CheckFunc);

        public virtual IList<T> GetAllModelList()
            => CheckFunc == null ? GetAllModels().ToList() : GetAllModels().Where(CheckFunc).ToList();

        public virtual IEnumerable<T> GetModels(Func<T, bool> exp) 
            => CheckFunc == null ? EntitySet.Where(exp) : EntitySet.Where(exp).Where(CheckFunc);

        public virtual IList<T> GetModelList(Func<T, bool> exp) 
            => CheckFunc == null ? GetModels(exp).ToList() : GetModels(exp).Where(CheckFunc).ToList();

        public virtual int GetCount(Func<T, bool> exp) 
            => CheckFunc == null ? EntitySet.Where(exp).Count() : EntitySet.Where(exp).Where(CheckFunc).Count();

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
        /// 数据仓库上下文（线程唯一）
        /// </summary>
        public static IRepositoryContext RepositoryContext => ContextLocal.Value;

        /// <summary>
        /// 当前线程的用户
        /// </summary>
        public static WdUser CurrentUser => RepositoryContext.CurrentUser;

        /// <summary>
        /// 当前线程用户所属域
        /// </summary>
        public static Domain CurrentDomain => RepositoryContext.CurrentDomain;

        /// <summary>
        /// 数据仓库上下文线程对象
        /// </summary>
        public static ThreadLocal<IRepositoryContext> ContextLocal { get; set; }
    }
}