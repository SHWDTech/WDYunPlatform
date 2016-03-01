using System;
using System.Collections.Generic;
using System.Linq;
using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Repository.Repository
{
    public class SysRepository<T> : Repository<T>, ISysRepository<T> where T : class, ISysModel
    {
        protected SysRepository()
        {
            
        } 

        public override T CreateDefaultModel()
        {
            var model = base.CreateDefaultModel() as ISysModel;
            if (model == null) throw new InvalidCastException();

            model.CreateDateTime = DateTime.Now;
            model.CreateUser = RepositoryContext.CurrentUser;

            return (T) model;
        }

        public override Guid AddOrUpdate(T model)
        {
            model.LastUpdateDateTime = DateTime.Now;
            model.LastUpdateUser = RepositoryContext.CurrentUser;

            return base.AddOrUpdate(model);
        }

        public override int AddOrUpdate(IEnumerable<T> models)
        {
            var enumerable = models as T[] ?? models.ToArray();
            foreach (var model in enumerable)
            {
                model.LastUpdateDateTime = DateTime.Now;
                model.LastUpdateUser = RepositoryContext.CurrentUser;
            }

            return base.AddOrUpdate(enumerable);
        }

        public virtual bool MarkDelete(T model)
        {
            using (var context = new RepositoryDbContext())
            {
                var iModel = model as ISysModel;
                if (iModel == null) throw new InvalidCastException();
                iModel.IsDeleted = true;

                return context.SaveChanges() == 1;
            }
        }

        public virtual int MarkDelete(IEnumerable<T> models)
        {
            using (var context = new RepositoryDbContext())
            {
                foreach (var model in models)
                {
                    MarkDelete(model);
                }

                return context.SaveChanges();
            }
        }
    }
}
