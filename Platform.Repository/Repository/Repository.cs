using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Enum;
using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Repository.Repository
{
    public class Repository<T> : RepositoryBase, IRepository<T> where T : class, IModel
    {
        protected Repository()
        {
            
        } 

        public virtual IEnumerable<T> GetAllModels()
        {
            using (var context = new RepositoryDbContext())
            {
                return context.Set<T>().ToList();
            }
        }

        public virtual IEnumerable<T> GetModels(Func<T, bool> exp)
        {
            using (var context = new RepositoryDbContext())
            {
                return context.Set<T>().Where(exp).ToList();
            }
        }

        public virtual int GetCount(Func<T, bool> exp)
        {
            using (var context = new RepositoryDbContext())
            {
                return context.Set<T>().Where(exp).Count();
            }
        }

        public virtual T CreateDefaultModel()
        {
            using (var context = new RepositoryDbContext())
            {
                var model = context.Set<T>().Create();
                model.ModelState = ModelState.Added;

                return model;
            }
        }

        public virtual T ParseModel(string jsonString)
        {
            var model = JsonConvert.DeserializeObject<T>(jsonString);
            model.ModelState = ModelState.Added;

            return model;
        }

        public virtual Guid AddOrUpdate(T model)
        {

            using (var context = new RepositoryDbContext())
            {
                if (!IsExists(model))
                {
                    context.Set<T>().Add(model);
                }

                return context.SaveChanges() != 1 ? Guid.Empty : model.Guid;
            }

        }

        public virtual int AddOrUpdate(IEnumerable<T> models)
        {
            using (var context = new RepositoryDbContext())
            {
                foreach (var model in models)
                {
                    if (!IsExists(model))
                    {
                        context.Set<T>().Add(model);
                    }
                }

                return context.SaveChanges();
            }
        }

        public virtual bool Delete(T model)
        {
            using (var context = new RepositoryDbContext())
            {
                context.Set<T>().Remove(model);

                return context.SaveChanges() == 1;
            }
        }

        public virtual int Delete(IEnumerable<T> models)
        {
            using (var context = new RepositoryDbContext())
            {
                foreach (var model in models)
                {
                    context.Set<T>().Remove(model);
                }

                return context.SaveChanges();
            }
        }

        public virtual bool IsExists(T model)
        {
            using (var context = new RepositoryDbContext())
            {
                return context.Set<T>().Find(model) != null;
            }
        }

        public virtual bool IsExists(Func<T, bool> exp)
        {
            using (var context = new RepositoryDbContext())
            {
                return context.Set<T>().Find(exp) != null;
            }
        }
    }

    public class RepositoryBase
    {
        protected RepositoryBase()
        {
            
        }

        /// <summary>
        /// Process所属Invoker
        /// </summary>
        public RepositoryInvoke Invoker { get; internal set; }

        /// <summary>
        /// Process操作必须的上下文信息
        /// </summary>
        public IRepositoryContext RepositoryContext => Invoker.InvokeContext;
    }
}
