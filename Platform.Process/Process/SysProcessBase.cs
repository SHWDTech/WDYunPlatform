using System;
using System.Collections.Generic;
using SHWD.Platform.Process.Entities;
using SHWD.Platform.Process.IProcess;
using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Process.Process
{
    public class SysProcessBase<T> : ProcessBase<T>, ISysProcessBase<T> where T : class
    {
        public virtual bool MarkDelete(T model)
        {
            using (var context = new ProcessContext())
            {
                var iModel = model as ISysModel;
                if (iModel == null) throw new InvalidCastException();
                iModel.IsDeleted = true;

                return context.SaveChanges() == 1;
            }
        }

        public virtual int MarkDelete(IEnumerable<T> models)
        {
            using (var context = new ProcessContext())
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
