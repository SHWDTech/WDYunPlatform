using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 数据类型仓库基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataRepository<T> : Repository<T>, IDataRepository<T> where T : class, IDataModel, new()
    {
        public DataRepository()
        {
            CheckFunc = (obj => obj.DomainId == CurrentDomain.Id);
        }

        public DataRepository(RepositoryDbContext dbContext) : base(dbContext)
        {
            
        }

        public override void InitEntitySet()
        {
            base.InitEntitySet();
            EntitySet = EntitySet.Where(model => model.DomainId == CurrentDomain.Id);
        }

        public new T CreateDefaultModel(bool generateId = true)
        {
            var model = Repository<T>.CreateDefaultModel(generateId);

            model.DomainId = CurrentDomain.Id;

            return model;
        }

        public virtual T GetModelIncludeById(long id, List<string> includes)
        {
            var query = includes.Aggregate(EntitySet, (current, include) => current.Include(include));

            return query.SingleOrDefault(obj => obj.Id == id);
        }

        public virtual T GetModelById(long id)
            => EntitySet.SingleOrDefault(obj => obj.Id == id);

        public override T ParseModel(string jsonString)
        {
            var model = base.ParseModel(jsonString);
            model.DomainId = CurrentDomain.Id;

            return model;
        }

        public virtual long PartialUpdateDoCommit(T model, List<string> propertyNames)
        {
            DoPartialUpdate(model, propertyNames);

            return Submit() != 1 ? -1 : model.Id;
        }

        public virtual bool IsExists(T model) => EntitySet.Any(obj => obj.Id == model.Id);

        public virtual long AddOrUpdateDoCommit(T model)
        {
            DoAddOrUpdate(model);

            return Submit() != 1 ? -1 : model.Id;
        }
    }
}