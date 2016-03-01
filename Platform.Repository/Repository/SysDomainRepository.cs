using System;
using System.Collections.Generic;
using System.Linq;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Repository.Repository
{
    public class SysDomainRepository <T> : SysRepository<T>, ISysDomainRepository<T> where T: class, ISysDomainModel
    {
        protected SysDomainRepository()
        {
            
        } 

        public override T CreateDefaultModel()
        {
            var model = base.CreateDefaultModel();
            model.Domain = RepositoryContext.CurrentDomain;

            return model;
        }

        public override T ParseModel(string jsonString)
        {
            var model = base.ParseModel(jsonString);
            model.Domain = RepositoryContext.CurrentDomain;

            return model;
        }

        public override Guid AddOrUpdate(T model)
        {
            if (model.Domain == null) model.Domain = RepositoryContext.CurrentDomain;

            return base.AddOrUpdate(model);
        }

        public override int AddOrUpdate(IEnumerable<T> models)
        {
            var enumerable = models.ToList();
            foreach (var model in enumerable.Where(model => model.Domain == null))
            {
                model.Domain = RepositoryContext.CurrentDomain;
            }

            return base.AddOrUpdate(enumerable);
        }
    }
}
