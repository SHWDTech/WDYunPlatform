using System;
using System.Collections.Generic;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.IModel;
using System.Linq;
using SHWD.Platform.Repository.Entities;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 带有域的系统数据仓库
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SysDomainRepository<T> : SysRepository<T>, ISysDomainRepository<T> where T : class, ISysDomainModel, new()
    {
        /// <summary>
        /// 初始化带有域的系统数据仓库基类
        /// </summary>
        protected SysDomainRepository()
        {
            CheckFunc = (obj => obj.DomainId == CurrentDomain.Id);
        }

        protected SysDomainRepository(RepositoryDbContext dbContext) : base(dbContext)
        {

        }

        public override void InitEntitySet()
        {
            base.InitEntitySet();
            EntitySet = EntitySet?.Where(model => model.DomainId == CurrentDomain.Id);
        }

        public override void AddOrUpdate(T model)
        {
            if (model.DomainId == Guid.Empty)
            {
                model.DomainId = CurrentDomain.Id;
            }

            base.AddOrUpdate(model);
        }

        public override void AddOrUpdate(IEnumerable<T> models)
        {
            var modelList = models.ToList();
            foreach (var model in modelList)
            {
                if (model.DomainId == Guid.Empty)
                {
                    model.DomainId = CurrentDomain.Id;
                }
            }

            base.AddOrUpdate(modelList);
        }

        public override Guid AddOrUpdateDoCommit(T model)
        {
            if (model.DomainId == Guid.Empty)
            {
                model.DomainId = CurrentDomain.Id;
            }

            return base.AddOrUpdateDoCommit(model);
        }

        public override int AddOrUpdateDoCommit(IEnumerable<T> models)
        {
            var modelList = models.ToList();
            foreach (var model in modelList)
            {
                if (model.DomainId == Guid.Empty)
                {
                    model.DomainId = CurrentDomain.Id;
                }
            }

            return base.AddOrUpdateDoCommit(modelList);
        }

        public override void PartialUpdate(T model, List<string> propertyNames)
        {
            if (model.DomainId == Guid.Empty)
            {
                model.DomainId = CurrentDomain.Id;
                propertyNames.Add("DomainId");
            }

            base.PartialUpdate(model, propertyNames);
        }

        public override void PartialUpdate(List<T> models, List<string> propertyNames)
        {
            foreach (var model in models)
            {
                if (model.DomainId == Guid.Empty)
                {
                    model.DomainId = CurrentDomain.Id;
                }
            }
            propertyNames.Add("DomainId");

            base.PartialUpdate(models, propertyNames);
        }

        public override Guid PartialUpdateDoCommit(T model, List<string> propertyNames)
        {
            if (model.DomainId == Guid.Empty)
            {
                model.DomainId = CurrentDomain.Id;
                propertyNames.Add("DomainId");
            }

            return base.PartialUpdateDoCommit(model, propertyNames);
        }

        public override int PartialUpdateDoCommit(List<T> models, List<string> propertyNames)
        {
            foreach (var model in models)
            {
                if (model.DomainId == Guid.Empty)
                {
                    model.DomainId = CurrentDomain.Id;
                }
            }
            propertyNames.Add("DomainId");

            return base.PartialUpdateDoCommit(models, propertyNames);
        }

        /// <summary>
        /// 创建默认数据模型
        /// </summary>
        /// <returns></returns>
        public new static T CreateDefaultModel(bool generateId = true)
        {
            var model = SysRepository<T>.CreateDefaultModel(generateId);
            model.DomainId = CurrentDomain.Id;

            return model;
        }

        public override T ParseModel(string jsonString)
        {
            var model = base.ParseModel(jsonString);
            model.DomainId = CurrentDomain.Id;

            return model;
        }
    }
}