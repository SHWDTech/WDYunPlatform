using System;
using System.Collections.Generic;
using SHWD.Platform.Process.IProcess;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Process.Process
{
    public class SysProcessBase<T> : ProcessBase<T>, ISysProcessBase<T> where T : class
    {
        public override T CreateDefaultModel()
        {
            var model = base.CreateDefaultModel() as ISysModel;
            if (model == null) throw new InvalidCastException();

            model.CreateDateTime = DateTime.Now;
            model.CreateUser = new User();

            return (T) model;
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
