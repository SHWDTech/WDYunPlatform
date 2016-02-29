using System;
using System.Collections.Generic;
using System.Linq;
using SHWD.Platform.Process.IProcess;
using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Process.Process
{
    public class ProcessBase<T> : IProcessBase<T> where T : class
    {
        public IProcessContext Context { get; set; }

        public virtual IEnumerable<T> GetModels()
        {
            using (var context = new Entities.ProcessContext())
            {
                return context.Set<T>().ToList();
            }
        }

        public virtual IEnumerable<T> GetModels(Func<T, bool> exp)
        {
            using (var context = new Entities.ProcessContext())
            {
                return context.Set<T>().Where(exp).ToList();
            }
        }

        public virtual int GetCount(Func<T, bool> exp)
        {
            using (var context = new Entities.ProcessContext())
            {
                return context.Set<T>().Where(exp).Count();
            }
        }

        public virtual T CreateDefaultModel()
        {
            using (var context = new Entities.ProcessContext())
            {
                var model = context.Set<T>().Create() as IModel;
                if (model == null) throw new InvalidCastException();
                model.Guid = Guid.Empty;

                return (T) model;
            }
        }

        public virtual Guid AddOrUpdate(T model)
        {

            using (var context = new Entities.ProcessContext())
            {
                var iModel = model as IModel;

                if (iModel == null)
                    return Guid.Empty;

                if (context.Set<T>().Find(model) == null)
                    context.Set<T>().Add(model);

                return context.SaveChanges() != 1 ? Guid.Empty : iModel.Guid;
            }

        }

        public virtual int AddOrUpdate(IEnumerable<T> models)
        {
            using (var context = new Entities.ProcessContext())
            {
                foreach (var model in models)
                {
                    AddOrUpdate(model);
                }

                return context.SaveChanges();
            }
        }

        public virtual bool Delete(T model)
        {
            using (var context = new Entities.ProcessContext())
            {
                context.Set<T>().Remove(model);

                return context.SaveChanges() == 1;
            }
        }

        public virtual int Delete(IEnumerable<T> models)
        {
            using (var context = new Entities.ProcessContext())
            {
                foreach (var model in models)
                {
                    Delete(model);
                }

                return context.SaveChanges();
            }
        }

        public virtual bool IsExists(T model)
        {
            using (var context = new Entities.ProcessContext())
            {
                return context.Set<T>().Find(model) != null;
            }
        }

        public virtual bool IsExists(Func<T, bool> exp)
        {
            using (var context = new Entities.ProcessContext())
            {
                return context.Set<T>().Find(exp) != null;
            }
        }
    }
}
