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
        public new static T CreateDefaultModel(bool genereteId = true)
        {
            var model = Repository<T>.CreateDefaultModel(genereteId);

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

        public override void AddOrUpdate(T model)
        {
            model.LastUpdateDateTime = DateTime.Now;
            model.LastUpdateUserId = CurrentUser.Id;

            base.AddOrUpdate(model);
        }

        public override void AddOrUpdate(IEnumerable<T> models)
        {
            var modelList = models.ToList();
            foreach (var model in modelList)
            {
                model.LastUpdateDateTime = DateTime.Now;
                model.LastUpdateUserId = CurrentUser.Id;
            }

            base.AddOrUpdate(modelList);
        }

        public override Guid AddOrUpdateDoCommit(T model)
        {
            model.LastUpdateDateTime = DateTime.Now;
            model.LastUpdateUserId = CurrentUser.Id;

            return base.AddOrUpdateDoCommit(model);
        }

        public override int AddOrUpdateDoCommit(IEnumerable<T> models)
        {
            var modelList = models.ToList();
            foreach (var model in modelList)
            {
                model.LastUpdateDateTime = DateTime.Now;
                model.LastUpdateUserId = CurrentUser.Id;
            }

            return base.AddOrUpdateDoCommit(modelList);
        }

        public override void PartialUpdate(T model, List<string> propertyNames)
        {
            model.LastUpdateDateTime = DateTime.Now;
            model.LastUpdateUserId = CurrentUser.Id;
            propertyNames.Add("LastUpdateDateTime");
            propertyNames.Add("LastUpdateUserId");

            base.PartialUpdate(model, propertyNames);
        }

        public override void PartialUpdate(List<T> models, List<string> propertyNames)
        {
            foreach (var model in models)
            {
                model.LastUpdateDateTime = DateTime.Now;
                model.LastUpdateUserId = CurrentUser.Id;
            }
            propertyNames.Add("LastUpdateDateTime");
            propertyNames.Add("LastUpdateUserId");

            base.PartialUpdate(models, propertyNames);
        }

        public override Guid PartialUpdateDoCommit(T model, List<string> propertyNames)
        {
            model.LastUpdateDateTime = DateTime.Now;
            model.LastUpdateUserId = CurrentUser.Id;
            propertyNames.Add("LastUpdateDateTime");
            propertyNames.Add("LastUpdateUserId");

            return base.PartialUpdateDoCommit(model, propertyNames);
        }

        public override int PartialUpdateDoCommit(List<T> models, List<string> propertyNames)
        {
            foreach (var model in models)
            {
                model.LastUpdateDateTime = DateTime.Now;
                model.LastUpdateUserId = CurrentUser.Id;
            }
            propertyNames.Add("LastUpdateDateTime");
            propertyNames.Add("LastUpdateUserId");

            return base.PartialUpdateDoCommit(models, propertyNames);
        }

        public virtual void MarkDelete(T model)
        {
            model.IsDeleted = true;
            AddOrUpdateDoCommit(model);
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
            AddOrUpdateDoCommit(model);
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