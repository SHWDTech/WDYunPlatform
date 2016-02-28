using System;
using System.Collections.Generic;
using System.Linq;
using SHWD.Platform.Process.Entities;
using SHWD.Platform.Process.IProcess;

namespace SHWD.Platform.Process.Process
{
    public class ProcessBase<T> : IProcessBase<T> where T : class
    {
        public IEnumerable<T> GetModels()
        {
            using (var context = new ProcessContext())
            {
                return context.Set<T>().ToList();
            }
        }

        public IEnumerable<T> GetModels(Func<T, bool> exp)
        {
            using (var context = new ProcessContext())
            {
                return context.Set<T>().Where(exp).ToList();
            }
        }

        public bool AddOrUpdate(T model)
        {
            using (var context = new ProcessContext())
            {
                if (context.Set<T>().Find(model) == null)
                {
                    context.Set<T>().Add(model);
                }

                return context.SaveChanges() == 1;
            }
        }

        public int AddOrUpdate(IEnumerable<T> models)
        {
            using (var context = new ProcessContext())
            {
                foreach (var model in models)
                {
                    AddOrUpdate(model);
                }

                return context.SaveChanges();
            }  
        }

        public bool Delete(T model)
        {
            using (var context = new ProcessContext())
            {
                context.Set<T>().Remove(model);

                return context.SaveChanges() == 1;
            }
        }

        public int Delete(IEnumerable<T> models)
        {
            using (var context = new ProcessContext())
            {
                foreach (var model in models)
                {
                    Delete(model);
                }

                return context.SaveChanges();
            }
        }
    }
}
