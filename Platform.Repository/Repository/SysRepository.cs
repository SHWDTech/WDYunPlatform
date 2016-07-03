using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.IModel;
using System;
using System.Collections.Generic;
using System.Linq;
using SHWD.Platform.Repository.Entities;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 系统数据仓库
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SysRepository<T> : Repository<T>, ISysRepository<T> where T : class, ISysModel, new()
    {
        /// <summary>
        /// 初始化带有域的系统数据仓库基类
        /// </summary>
        protected SysRepository()
        {
        }

        protected SysRepository(RepositoryDbContext dbContext) : base(dbContext)
        {
            
        }

        /// <summary>
        /// 创建默认数据模型
        /// </summary>
        /// <returns></returns>
        public new static T CreateDefaultModel()
        {
            var model = Repository<T>.CreateDefaultModel();

            model.IsEnabled = true;
            model.CreateDateTime = DateTime.Now;
            model.CreateUserId = CurrentUser.Id;

            return model;
        }

        public override T ParseModel(string jsonString)
        {
            var model = base.ParseModel(jsonString);
            model.IsEnabled = true;
            model.CreateDateTime = DateTime.Now;
            model.CreateUserId = CurrentUser.Id;

            return model;
        }

        public override Guid AddOrUpdate(T model)
        {
            model.LastUpdateDateTime = DateTime.Now;
            model.LastUpdateUserId = CurrentUser.Id;

            return base.AddOrUpdate(model);
        }

        public override int AddOrUpdate(IEnumerable<T> models)
        {
            var enumerable = models.ToList();
            foreach (var model in enumerable)
            {
                model.LastUpdateDateTime = DateTime.Now;
                model.LastUpdateUserId = CurrentUser.Id;
            }

            return base.AddOrUpdate(enumerable);
        }

        public override Guid PartialUpdate(T model, List<string> propertyNames)
        {
            model.LastUpdateDateTime = DateTime.Now;
            model.LastUpdateUserId = CurrentUser.Id;
            propertyNames.Add("LastUpdateDateTime");
            propertyNames.Add("LastUpdateUserId");

            return base.PartialUpdate(model, propertyNames);
        }

        public override int PartialUpdate(List<T> models, List<string> propertyNames)
        {
            foreach (var model in models)
            {
                model.LastUpdateDateTime = DateTime.Now;
                model.LastUpdateUserId = CurrentUser.Id;
            }
            propertyNames.Add("LastUpdateDateTime");
            propertyNames.Add("LastUpdateUserId");

            return base.PartialUpdate(models, propertyNames);
        }

        public virtual void MarkDelete(T model)
        {
            model.IsDeleted = true;
            AddOrUpdate(model);
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
            AddOrUpdate(model);
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