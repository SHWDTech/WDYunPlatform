using System;
using System.Collections.Generic;
using System.Linq;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 系统数据仓库
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SysRepository<T> : Repository<T>, ISysRepository<T> where T : class, ISysModel
    {
        /// <summary>
        /// 初始化带有域的系统数据仓库基类
        /// </summary>
        protected SysRepository()
        {

        }

        public override T CreateDefaultModel()
        {
            var model = base.CreateDefaultModel();

            model.IsEnabled = true;
            model.CreateDateTime = DateTime.Now;
            model.CreateUser = ContextLocal.Value.CurrentUser;

            return model;
        }

        public override T ParseModel(string jsonString)
        {
            var model =  base.ParseModel(jsonString);
            model.IsEnabled = true;
            model.CreateDateTime = DateTime.Now;
            model.CreateUser = ContextLocal.Value.CurrentUser;

            return model;
        }

        public override Guid AddOrUpdate(T model)
        {
            model.LastUpdateDateTime = DateTime.Now;
            model.LastUpdateUser = ContextLocal.Value.CurrentUser;

            return base.AddOrUpdate(model);
        }

        public override int AddOrUpdate(IEnumerable<T> models)
        {
            var enumerable = models.ToList();
            foreach (var model in enumerable)
            {
                model.LastUpdateDateTime = DateTime.Now;
                model.LastUpdateUser = ContextLocal.Value.CurrentUser;
            }

            return base.AddOrUpdate(enumerable);
        }

        public virtual void MarkDelete(T model)
        {
            model.IsDeleted = true;
        }

        public virtual void MarkDelete(IEnumerable<T> models)
        {
            foreach (var model in models)
            {
                MarkDelete(model);
            }
        }

        public void SetEnableStatus(T model, bool enableStatus)
        {
            model.IsDeleted = enableStatus;
        }

        public void SetEnableStatus(IEnumerable<T> models, bool enableStatus)
        {
            foreach (var model in models)
            {
                SetEnableStatus(model, enableStatus);
            }
        }
    }
}
