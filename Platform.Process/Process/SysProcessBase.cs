using System;
using System.Collections.Generic;
using System.Linq;
using SHWD.Platform.Process.IProcess;
using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Process.Process
{
    public class SysProcessBase<T> : ProcessBase<T>, ISysProcessBase<T> where T : class
    {
        public override T CreateDefaultModel()
        {
            var model = base.CreateDefaultModel() as ISysModel;
            if (model == null) throw new InvalidCastException();

            model.CreateDateTime = DateTime.Now;
            model.CreateUser = Context.CurrentUser;

            return (T) model;
        }

        public override Guid AddOrUpdate(T model)
        {
            var iModel = model as ISysModel;
            if(iModel == null) throw new InvalidCastException();

            iModel.LastUpdateDateTime = DateTime.Now;
            iModel.LastUpdateUser = Context.CurrentUser;

            return base.AddOrUpdate((T) iModel);
        }

        public override int AddOrUpdate(IEnumerable<T> models)
        {
            var enumerable = models as T[] ?? models.ToArray();
            foreach (var model in enumerable)
            {
                var iModel = model as ISysModel;
                if(iModel == null) throw new InvalidCastException();
                iModel.LastUpdateDateTime = DateTime.Now;
                iModel.LastUpdateUser = Context.CurrentUser;
            }

            return base.AddOrUpdate(enumerable);
        }

        public virtual bool MarkDelete(T model)
        {
            using (var context = new Entities.ProcessContext())
            {
                var iModel = model as ISysModel;
                if (iModel == null) throw new InvalidCastException();
                iModel.IsDeleted = true;

                return context.SaveChanges() == 1;
            }
        }

        public virtual int MarkDelete(IEnumerable<T> models)
        {
            using (var context = new Entities.ProcessContext())
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
